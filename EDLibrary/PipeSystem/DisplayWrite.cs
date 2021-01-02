using EDLibrary.UI;
using System;

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

        public override void Write(object data)
        {
            throw new NotImplementedException();
        }
    }
}
