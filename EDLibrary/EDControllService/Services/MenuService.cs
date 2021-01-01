using EDLibrary.EDControllService.CommandFactory;
using EDLibrary.EDControllService.Menu;
using EDLibrary.ControllInput;
using EDLibrary.UI;
using System;
using System.Collections.Generic;

namespace EDLibrary.EDControllService.Services
{
    public class MenuService : Service
    {       
        private List<MFDMenu> loadedMenus = new List<MFDMenu>();
        private List<ActiveMenuInfo> activeMenus = new List<ActiveMenuInfo>();

        private void propertiesCallback(object sender, SingelPropertyChangedEventArgs e)
        {
            var info = e.PropertyInfo;
            foreach (ActiveMenuInfo menuInfo in activeMenus)
            {
                if (!info.PropertyType.Equals(typeof(bool))) throw new Exception("Property is not boolean. Only booleans are accepted for button states.");
                menuInfo.Menu.UpdateState(info.Name, (bool)info.GetValue(StatusWatcher.Status.Instance));
            }
        }

        public ICommand ForwardButtonClick(int position, InputDeviceNames origin)
        {
            List<ICommand> commands = new List<ICommand>();

            activeMenus.ForEach(e =>
            {
                if (e.AssignedInput.Equals(origin))
                {
                    ICommand cmd = Factory.Instance.getCommand(e.Menu.ButtonClick(position));
                    if(cmd != null) commands.Add(cmd);
                }
            });
            if (commands.Count > 1) throw new Exception("Multiple Menus answered to click");
            if (commands.Count == 0) return null;
            return commands[0];
        }

        public void EnableMenu(string menuName, InputDeviceNames input)
        {
            if (activeMenus.Find(e => e.Menu.MenuInfo.MenuText == menuName || e.AssignedInput.Equals(input)) != null) return;

            
            MFDMenu menu = loadedMenus.Find(e => e.MenuInfo.MenuText == menuName);

            if (menu == null) throw new ArgumentException("Menu not found");

            List<string> list = menu.getLinkedProperties();
            foreach (string property in list)
            {
                FileService.Instance.SubscribeTo(property, propertiesCallback);
            }

            activeMenus.Add(new ActiveMenuInfo { Menu = menu, AssignedInput = input });
        }

        public void SwapMenu(string oldM, string newM)
        {
            if (oldM == newM) return;
            ActiveMenuInfo menuInfo = activeMenus.Find(e => e.Menu.MenuInfo.MenuText == oldM);

            if (menuInfo == null) return;

            DisableMenu(oldM);
            EnableMenu(newM, menuInfo.AssignedInput);
            AssignPanel(menuInfo.Menu.GetPanel(), menuInfo.AssignedInput);
        }
        public void AssignPanel(IPanel panel, InputDeviceNames type)
        {
            ActiveMenuInfo info = activeMenus.Find(e => e.AssignedInput.Equals(type));
            if(info != null)
            {
                info.Menu.SetPanel(panel);
            }
        }
        public void DisableMenu(string menuName)
        {
            ActiveMenuInfo menuInfo = activeMenus.Find(e => e.Menu.MenuInfo.MenuText == menuName);

            if (menuInfo == null) return;

            MFDMenu menu = menuInfo.Menu;

            List<string> list = menu.getLinkedProperties();

            foreach (string property in list)
            {
                FileService.Instance.UnsubscribteTo(property, propertiesCallback);
            }
            IPanel panel = menu.GetPanel();
            if (panel != null) new ModifyPanelCommand() { Panel = panel, ChangeType =  ModifyPanelChangeType.CLEAR }.Execute();
            activeMenus.RemoveAll(e => e.Menu == menu);
        }

        public void LoadMenu(string path)
        {
            loadedMenus.Add(MFDMenuParser.Deserialize(path));
        }

        //TODO Remove (When Menu Creater Project is finished)
        public void SaveMenu(string path, string menuName)
        {
            MFDMenu menu = loadedMenus.Find(e => e.MenuInfo.MenuText == menuName);
            if (menu == null) throw new ArgumentException("menu not found");
            MFDMenuParser.Serialize(menu, path);
        }

        public void Quit()
        {
        }

        #region Singelton
        private static readonly MenuService instance = new MenuService();

        private MenuService()
        {

        }
        public static MenuService Instance
        {
            get
            {
                return instance;
            }
        }


        #endregion
    }
}
