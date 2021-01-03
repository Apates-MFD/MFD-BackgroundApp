using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using EDLibrary.ControllInput;
using EDLibrary.CommandFactory;
using EDLibrary.Menu;
using EDLibrary.Handlers;
using EDLibrary.UI;
using System.Windows;

namespace EDLibrary
{
    /// <summary>
    /// Controller Class
    /// </summary>
    public class Controller
    {
        private ConfigurationHandler configurationHandler = new ConfigurationHandler();
        private InterfaceHandler interfaceHandler = new InterfaceHandler();
        private ControlHandler controlHandler;
        private StatusHandler statusHandler;
        private List<ActiveMenuInfo> activeMenus = new List<ActiveMenuInfo>();
       
        /// <summary>
        /// Init of Controller
        /// </summary>
        private void init()
        {           
            Debug.Write("Controller init... ");
            interfaceHandler.ReloadConfig += InterfaceHandler_ReloadConfig;
            interfaceHandler.SaveConfig += InterfaceHandler_SaveConfig;
            controlHandler = new ControlHandler(configurationHandler.GetPathToKeybindings());
            controlHandler.MenuButtonPressed += ControlHandler_MenuButtonPressed;
            statusHandler = new StatusHandler(configurationHandler.GetPathToStatusFolder());
            //Aquires Input Devices and setting mainmenu foreach device
            foreach(var dev in configurationHandler.GetInputDevices())
            {
                //Acquires input and creates callback delegate
                controlHandler.Acquire(dev, new EventHandler(delegate (object sender, EventArgs e)
                {
                    InputEventArgs args = (InputEventArgs)e;
                    if (args.Button.ButtonState == configurationHandler.GetTriggerState())
                    {
                        InputDevice inputDevice = (InputDevice)sender;
                        ActiveMenuInfo info = activeMenus.Find(e => e.AssignedInput.Equals(inputDevice.InputDeviceName));
                        SerializableCommand command = info.Menu.ButtonClick(args.Button.ButtonNum);
                        executeCommand(Factory.GetCommand(command, inputDevice.InputDeviceName));                       
                    }
                }));
                EnableMenu(configurationHandler.GetMainMenu(), dev);
            }
            Debug.WriteLine("done.");
        }

        /// <summary>
        /// Handels Menu Buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlHandler_MenuButtonPressed(object sender, EventArgs e)
        {
            InputEventArgs args = (InputEventArgs)e;
            InputDevice inputDevice = (InputDevice)sender;

            if (!args.Button.ButtonState == true) return;

            IPanel panel = interfaceHandler.GetPanel(inputDevice.InputDeviceName);

            switch (args.Button.ButtonNum)
            {
                case 22:
                    panel.IncreaseContrast();
                    break;

                case 23:
                    panel.DecreaseContrast();
                    break;

                case 24:
                    panel.IncreaseBrightness();
                    break;

                case 25:
                    panel.DecreaseBrightness();
                    break;
            }


        }

        /// <summary>
        /// Saves the current window position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterfaceHandler_SaveConfig(object sender, EventArgs e)
        {
            IPanel panel = (MfdDisplay) sender;
            configurationHandler.SaveWindowPosition(interfaceHandler.GetDeviceName(panel), panel.GetWindowProperty());
        }

        /// <summary>
        /// Reloads the position config from window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterfaceHandler_ReloadConfig(object sender, EventArgs e)
        {
            IPanel panel = (MfdDisplay)sender;
            panel.SetWindowProperty(configurationHandler.GetWindowPosition(interfaceHandler.GetDeviceName(panel)));
        }

        /// <summary>
        /// Enables menu
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="input"></param>
        public void EnableMenu(MFDMenu menu, InputDeviceNames input)
        {
            if (menu == null) throw new ArgumentException("Menu cannot be null");

            if (activeMenus.Find(e => e.AssignedInput.Equals(input)) != null) return;
            
            List<string> list = menu.getLinkedProperties();

            ActiveMenuInfo activeMenuInfo = new ActiveMenuInfo()
            {
                Menu = menu,
                AssignedInput = input
            };

            foreach (string property in list)
            {
                activeMenuInfo.Callback = new EventHandler<PropertyChangedEventArgs>(delegate (object sender, PropertyChangedEventArgs e)
                {
                    Type propertyType = sender.GetType();
                    dynamic property = propertyType.GetProperty(e.PropertyName).GetValue(sender);
                    if (!propertyType.Equals(typeof(bool))) throw new Exception("Property is not boolean. Only booleans are accepted for button states.");
                    List< ModifyPanelCommand> commands = menu.UpdateState(e.PropertyName, property);
                    commands.ForEach(c => this.executeCommand(c));
                });

                statusHandler.SubscribeTo(property, activeMenuInfo.Callback);
            }           
            activeMenus.Add(activeMenuInfo);
            AssignPanel(menu, activeMenuInfo.AssignedInput);
        }

        /// <summary>
        /// Changes Menu
        /// </summary>
        /// <param name="oldMenu"></param>
        /// <param name="newMenu"></param>
        public void ChangeMenu(string newMenuName, object sender)
        {
            InputDeviceNames name = (InputDeviceNames)sender;
            ActiveMenuInfo menuInfo = activeMenus.Find(e => e.AssignedInput.Equals(name));
            if (menuInfo == null) return;

            MFDMenu oldMenu = menuInfo.Menu;
            MFDMenu newMenu = configurationHandler.GetMenu(newMenuName);

            DisableMenu(menuInfo);
            EnableMenu(newMenu, menuInfo.AssignedInput);
        }

        /// <summary>
        /// Assign mfd input to panel
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="type"></param>
        public void AssignPanel(MFDMenu menu, InputDeviceNames type)
        {
            menu.SetPanel(interfaceHandler.GetPanel(type)).ForEach(c => executeCommand(c));
        }

        /// <summary>
        /// Disables menu
        /// <para>Essentially removes all callbacks</para>
        /// </summary>
        /// <param name="menu"></param>
        public void DisableMenu(ActiveMenuInfo menuInfo)
        {          
            List<string> list = menuInfo.Menu.getLinkedProperties();

            foreach (string property in list)
            {
                statusHandler.UnsubscribeFrom(property, menuInfo.Callback);
            }
            interfaceHandler.GetPanel(menuInfo.AssignedInput).Clear();
            activeMenus.Remove(menuInfo);
        }

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="command"></param>
        private void executeCommand(Command command)
        {
            if (command != null)
            {
                Debug.WriteLine("Executing command", DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
                command.Execute(this);
            }
        }


        /// <summary>
        /// Redirects action to control handler
        /// </summary>
        /// <param name="action"></param>
        public void ExecuteAction(Actions action)
        {
            controlHandler.Write(action);
        }

        /// <summary>
        /// Return Applicaton
        /// </summary>
        /// <returns></returns>
        public Application GetApplication()
        {
            return interfaceHandler.Application;
        }

        /// <summary>
        /// Swaps the two displays
        /// </summary>
        public void Swap()
        {
            if(activeMenus.Count == 2)
            {
                ActiveMenuInfo info1 = activeMenus[0];
                ActiveMenuInfo info2 = activeMenus[1];

                MFDMenu menu1 = info1.Menu;
                MFDMenu menu2 = info2.Menu;

                ChangeMenu(menu2.MenuInfo.MenuName, info1.AssignedInput);
                ChangeMenu(menu1.MenuInfo.MenuName, info2.AssignedInput);
            }
        }

        #region Singelton
        private static readonly Controller instance = new Controller();

        private Controller()
        {
            init();
        }
        public static Controller Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
    }
}
