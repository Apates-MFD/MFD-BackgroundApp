using EDLibrary.EDControllService.Services;
using System;

namespace EDLibrary.EDControllService.CommandFactory
{
    public class ChangeMenuCommand : Command
    {
        public string MenuName { get; set; }
        public string Caller { get; set; }
        public void Execute()
        {
            if (MenuName == null || MenuName == "")
            {
                throw new ArgumentNullException("MenuName cannot be null");
            }
            if (Caller == null || Caller == "")
            {
                throw new ArgumentNullException("Caller cannot be null");
            }
            MenuService.Instance.SwapMenu(Caller, MenuName);
        }
    }
}
