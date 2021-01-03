using System;

namespace EDLibrary.CommandFactory
{
    public class ChangeMenuCommand : Command
    {
        public string MenuName { get; set; }
        public object Device { get; set; }
        public override void Execute(object sender)
        {
            if (MenuName == null || MenuName == "")
            {
                throw new ArgumentNullException("MenuName cannot be null");
            }
            
            ((Controller)sender).ChangeMenu(MenuName, Device);
        }
    }
}
