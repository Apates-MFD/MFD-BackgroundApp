using EDLibrary.ControllInput;
using System;
using System.ComponentModel;

namespace EDLibrary.Menu
{
    /// <summary>
    /// Active menu info
    /// </summary>
    public class ActiveMenuInfo
    {
        public MFDMenu Menu { get; set; }
        public InputDeviceNames AssignedInput { get; set; }
        public EventHandler<PropertyChangedEventArgs> Callback {get; set; }
    }
}
