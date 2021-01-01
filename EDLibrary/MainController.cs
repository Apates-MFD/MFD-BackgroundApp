using EDLibrary.EDControllService.CommandFactory;
using EDLibrary.EDControllService.Services;
using EDLibrary.EDStatusInput;
using System;
using System.Collections.Generic;
using System.IO;

namespace EDLibrary
{
    public class MainController
    {
        private Dictionary<MFDType, UI.IPanel> activePanels = new Dictionary<MFDType, UI.IPanel>();
        private void init()
        {
            _ = FileService.Instance;
            _ = InputService.Instance;
            _ = MenuService.Instance;

            InputService.Instance.ReadMFD(MFDType.MFD_TWO);
            InputService.Instance.EventAction("+=", "ButtonReleased", MFDType.MFD_TWO, ButtonReleased);
            InputService.Instance.MenuEventAction("+=", MFDType.MFD_TWO, MenuOptionPressed);

            if (!Directory.Exists("menus//")) throw new Exception("Menu folder not found");

            string[] menus = Directory.GetFiles("menus//");
            foreach (string menupath in menus)
            {
                MenuService.Instance.LoadMenu(menupath);
            }

            MenuService.Instance.EnableMenu("MAINMENU", MFDType.MFD_TWO);
        }
        
        public void AssignPanel(UI.IPanel panel, MFDType joystick)
        {
            MenuService.Instance.AssignPanel(panel, joystick);
            activePanels.Add(joystick, panel);
        }

        private void MenuOptionPressed(object sender, MFDMenuButtonEventArgs e)
        {          
            MFDInput input = (MFDInput)sender;
            UI.IPanel panel = activePanels[input.SelectedMFD];
            if(panel != null)
            {
                //TODO Implement Brightness, Contrasst & Symbology
            }
        }
        private void ButtonReleased(object sender, MFDButtonEventArgs e)
        {
            ICommand command = MenuService.Instance.ForwardButtonClick(e.Button.ButtonNum, ((MFDInput)sender).SelectedMFD);           

            if (command != null)
            {
                //As of now, just execute but if there are combinations of key releases this needs to be changed
                command.Execute();
            }
        }
        public void Quit()
        {
            FileService.Instance.Quit();
            InputService.Instance.Quit();
            MenuService.Instance.Quit();
        }
        #region Singelton
        private static readonly MainController instance = new MainController();

        private MainController()
        {
            init();
        }
        public static MainController Instance
        {
            get
            {
                return instance;
            }
        }


        #endregion
    }
}
