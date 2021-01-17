using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundLibrary.CommandFactory
{
    class SwapCommand : Command
    {
        public override void Execute(object sender)
        {
            ((Controller)sender).Swap();
        }
    }
}
