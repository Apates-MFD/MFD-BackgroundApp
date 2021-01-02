using EDLibrary.Menu;
using System;

namespace EDLibrary.CommandFactory
{
    public class InGameCommand : Command
    {
        public Actions Action { get; set; }

        public override void Execute(object sender)
        {
            if (Action == Actions.NULL) throw new ArgumentNullException("Action cannot be null");
            ((Controller)sender).ExecuteAction(Action);
        }
    }
}
