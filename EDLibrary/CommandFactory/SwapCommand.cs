using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.CommandFactory
{
    class SwapCommand : Command
    {
        public override void Execute(object sender)
        {
            ((Controller)sender).Swap();
        }
    }
}
