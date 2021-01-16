using System;

namespace NetworkLibrary
{
    public class CommandReceivedEventArgs : EventArgs
    {
        public COMMAND_TYPES Command_type { get; set; }
        public int Command { get; set; }
        public object[] Args { get; set; }
    }
}
