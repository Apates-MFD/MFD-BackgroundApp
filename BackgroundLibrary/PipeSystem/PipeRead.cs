using System;

namespace BackgroundLibrary.PipeSystem
{
    /// <summary>
    /// Abstract PipeRead Class.
    /// </summary>
    public abstract class PipeRead : Pipe
    {
        public abstract event EventHandler DataReceived;
    }
}
