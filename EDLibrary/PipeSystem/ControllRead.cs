using System;
using System.Collections.Generic;
using System.Threading;
using EDLibrary.ControllInput;

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
                        dev.ButtonPressed += ButtonEvent;
                        dev.ButtonReleased += ButtonEvent;
                        devices.Add(dev);
                        new Thread(new ThreadStart(dev.Observe));
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

        /// <summary>
        /// Command to stop reading inputs
        /// <para>Importand for exit since all inputs are runnnig in seperate threads</para>
        /// </summary>
        public override void Exit()
        {
            devices.ForEach(d => d.Stop());
        }

    }
}
