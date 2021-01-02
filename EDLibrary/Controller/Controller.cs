using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using EDLibrary.ControllInput;
using EDLibrary.CommandFactory;
using EDLibrary.Menu;
using EDLibrary.Handlers;
using EDLibrary.UI;

namespace EDLibrary
{
    /// <summary>
    /// Controller Class
    /// </summary>
    public class Controller
    {
        private ConfigurationHandler configurationHandler = new ConfigurationHandler();
        private ControlHandler controlHandler = new ControlHandler();
        private StatusHandler statusHandler = new StatusHandler();
        private List<ActiveMenuInfo> activeMenus = new List<ActiveMenuInfo>();
       
        /// <summary>
        /// Init of Controller
        /// </summary>
        private void init()
        {      
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
                        executeCommand(Factory.GetCommand(command));                       
                    }
                }));
                EnableMenu(configurationHandler.GetMainMenu(), dev);
            }      
        }

        /// <summary>
        /// Enables menu
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="input"></param>
        public void EnableMenu(MFDMenu menu, InputDeviceNames input)
        {
            if (menu == null) throw new ArgumentException("Menu cannot be null");

            if (activeMenus.Find(e => e.Menu.Equals(menu) || e.AssignedInput.Equals(input)) != null) return;
            
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
        }

        /// <summary>
        /// Changes Menu
        /// </summary>
        /// <param name="oldMenu"></param>
        /// <param name="newMenu"></param>
        public void ChangeMenu(string oldMenuName, string newMenuName)
        {
            if (oldMenuName == newMenuName) return;
            ActiveMenuInfo menuInfo = activeMenus.Find(e => e.Menu.MenuInfo.MenuName.Equals(oldMenuName));
            if (menuInfo == null) return;

            MFDMenu oldMenu = menuInfo.Menu;
            MFDMenu newMenu = configurationHandler.GetMenu(newMenuName);         

            DisableMenu(oldMenu);
            EnableMenu(newMenu, menuInfo.AssignedInput);
            //AssignPanel(menuInfo.Menu.GetPanel(), menuInfo.AssignedInput);
        }

        //public void AssignPanel(IPanel panel, InputDeviceNames type)
        //{
        //    ActiveMenuInfo info = activeMenus.Find(e => e.AssignedInput.Equals(type));
        //    if (info != null)
        //    {
        //        info.Menu.SetPanel(panel);
        //    }
        //}

        /// <summary>
        /// Disables menu
        /// <para>Essentially removes all callbacks</para>
        /// </summary>
        /// <param name="menu"></param>
        public void DisableMenu(MFDMenu menu)
        {
            ActiveMenuInfo menuInfo = activeMenus.Find(e => e.Menu.Equals(menu));
            if (menuInfo == null) return;

            List<string> list = menu.getLinkedProperties();

            foreach (string property in list)
            {
                statusHandler.UnsubscribeFrom(property, menuInfo.Callback);
            }
            //IPanel panel = menu.GetPanel();
            //if (panel != null) new ModifyPanelCommand() { Panel = panel, ChangeType = ModifyPanelChangeType.CLEAR }.Execute();
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
