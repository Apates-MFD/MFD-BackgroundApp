using EDLibrary.EDControllService.Menu;
using System;

namespace EDLibrary.EDControllService.CommandFactory
{
    public class InGameCommand : ICommand
    {
        public Actions Action { get; set; }

        public void Execute()
        {
            if (Action == Actions.NULL) throw new ArgumentNullException("Action cannot be null");

            EDOutput.Keyboard.Execute(Action);
        }
    }
}
