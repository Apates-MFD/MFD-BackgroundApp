using System;

namespace EDLibrary.ControllInput
{
    /// <summary>
    /// Input Event Argument
    /// </summary>
    public class InputEventArgs : EventArgs
    {
        public InputButton Button { get; set; }
    }
}
