using EDLibrary.ControllInput;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EDLibrary.PipeSystem
{
    /// <summary>
    /// Reads Input from all defines sources
    /// </summary>
    public class ControllRead : PipeRead
    {
        private List<InputDevice> devices = new List<InputDevice>();
       
        public override event EventHandler DataReceived;

        /// <summary>
        /// Constructor
        /// Acquires all possible inputs
        /// </summary>
        public ControllRead()
        {
            var possibleDevices = InputDeviceNames.GetAll(); /* Try all devices */
            InputDevice dev = null;
            foreach (var possibleDevice in possibleDevices)
            {
                try
                {
                    dev = null;
                    dev = new InputDevice(possibleDevice);
                }
                catch (ArgumentException)
                {
                    //Ignore 
                }
                finally
                {
                    if (dev != null)
                    {
                        dev.ButtonEvent += ButtonEvent;
                        devices.Add(dev);
                        Thread t = new Thread(new ThreadStart(dev.Observe));
                        t.IsBackground = true;
                        t.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Event gets triggered by a button press or release
        /// </summary>
        /// <param name="sender"><see cref="InputDevice"/></param>
        /// <param name="e"><see cref="InputButton"/></param>
        private void ButtonEvent(object sender, InputEventArgs e)
        {
            if (DataReceived != null) DataReceived.Invoke(sender, e);
        }

    }
}
