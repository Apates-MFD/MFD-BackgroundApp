using NetworkLibrary.NetworkPackage.Commands;
using System;

namespace NetworkLibrary
{
    public class CommandReceivedEventArgs : EventArgs
    {
        public Command_Types Command_type { get; set; }
        public int Command { get; set; }
        public object[] Args { get; set; }
    }
}
