using EDLibrary.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.PipeSystem
{
    //TODO Implement
    class DisplayWrite : PipeWrite
    {
        private IPanel panel;
        public DisplayWrite(IPanel panel)
        {
            this.panel = panel;
        }
        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Write(object data)
        {
            throw new NotImplementedException();
        }
    }
}
