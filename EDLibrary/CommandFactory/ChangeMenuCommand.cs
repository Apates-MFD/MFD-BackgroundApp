using System;

namespace EDLibrary.CommandFactory
{
    public class ChangeMenuCommand : Command
    {
        public string MenuName { get; set; }
        public string Caller { get; set; }
        public override void Execute(object sender)
        {
            if (MenuName == null || MenuName == "")
            {
                throw new ArgumentNullException("MenuName cannot be null");
            }
            if (Caller == null || Caller == "")
            {
                throw new ArgumentNullException("Caller cannot be null");
            }
            ((Controller)sender).ChangeMenu(Caller, MenuName);
        }
    }
}
