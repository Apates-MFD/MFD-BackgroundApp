using BackgroundLibrary.ControllInput;
using System;
using System.ComponentModel;

namespace BackgroundLibrary.Menu
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
