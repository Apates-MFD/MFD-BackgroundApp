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

        private void init()
        {
            _ = FileService.Instance;
            _ = InputService.Instance;
            _ = MenuService.Instance;

            InputService.Instance.ReadMFD(MFDType.MFD_TWO);
            InputService.Instance.EventAction("+=", "ButtonReleased", MFDType.MFD_TWO, ButtonReleased);

            if (!Directory.Exists("menus//")) throw new Exception("Menu folder not found");

            string[] menus = Directory.GetFiles("menus//");
            foreach (string menupath in menus)
            {
                MenuService.Instance.LoadMenu(menupath);
            }

            MenuService.Instance.EnableMenu("MEN1", MFDType.MFD_TWO);
        }
        
        public void AssignPanel(UI.IPanel panel, MFDType joystick)
        {
            MenuService.Instance.AssignPanel(panel, joystick);
        }

        private static void ButtonReleased(object sender, MFDButtonEventArgs e)
        {
            List<Command> commands = MenuService.Instance.ForwardButtonClick(e.Button.ButtonNum, ((MFDInput)sender).SelectedMFD);

            if (commands.Count >= 2)
            {
                throw new Exception("Multiple Menus listen on same device. Too many choices");
            }

            if (commands.Count == 1)
            {
                //As of now, just execute but if there are combinations uf key releases this needs to be changed
                commands[0].Execute();
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
