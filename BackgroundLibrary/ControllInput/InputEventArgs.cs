using System;

namespace BackgroundLibrary.ControllInput
{
    /// <summary>
    /// Input Event Argument
    /// </summary>
    public class InputEventArgs : EventArgs
    {
        public InputButton Button { get; set; }
    }
}
