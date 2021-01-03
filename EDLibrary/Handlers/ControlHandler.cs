using System;
using System.Collections.Generic;
using System.Text;
using EDLibrary.ControllInput;
using EDLibrary.Menu;
using EDLibrary.PipeSystem;


namespace EDLibrary.Handlers
{
    /// <summary>
    /// Control Handler manages all Controls.
    /// <para>Input from MFD and action output</para>
    /// </summary>
    class ControlHandler
    {
        private static Dictionary<InputDeviceNames, EventHandler> acquiredDevices = new Dictionary<InputDeviceNames, EventHandler>();
        private ControllRead readPipe;
        private ControllWrite writePipe;
        public event EventHandler MenuButtonPressed;
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlHandler(string pathToKeybindings)
        {
            readPipe = (ControllRead)PipeController.Instance.GetPipe(nameof(ControllRead));
            if(readPipe == null)
            {
                readPipe = new ControllRead();
                PipeController.Instance.register(readPipe, nameof(ControllRead));
            }

            writePipe = (ControllWrite)PipeController.Instance.GetPipe(nameof(ControllWrite));
            if (writePipe == null)
            {
                writePipe = new ControllWrite(pathToKeybindings);
                PipeController.Instance.register(readPipe, nameof(ControllWrite));
            }

            readPipe.DataReceived += DataReceived;
        }

        /// <summary>
        /// Receiver af Data event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, EventArgs e)
        {
            var device = (InputDevice)sender;
            InputEventArgs args = (InputEventArgs)e;
            if (args.Button.ButtonNum >= 20 && args.Button.ButtonNum <= 27)
            {
                if (MenuButtonPressed != null) MenuButtonPressed.Invoke(sender, e);
            }
            else
            {
                if (acquiredDevices.ContainsKey(device.InputDeviceName))
                {
                    acquiredDevices[device.InputDeviceName].Invoke(sender, e);
                }
            }
        }

        /// <summary>
        /// Write action to device
        /// </summary>
        /// <param name="actions"></param>
        public void Write(Actions actions)
        {
            writePipe.Write(actions);
        }

        /// <summary>
        /// Aquires device for reading
        /// </summary>
        /// <param name="device"></param>
        /// <param name="callback"></param>
        /// <exception cref="ArgumentException">When device is already use from another entity</exception>
        public void Acquire(InputDeviceNames device, EventHandler callback)
        {
            if (acquiredDevices.ContainsKey(device)) throw new ArgumentException("Device already aquired");
            acquiredDevices.Add(device, callback);
        }

        /// <summary>
        /// Releases device 
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="ArgumentException">When device isn't aquiered</exception>
        public void Release(InputDeviceNames device)
        {
            if (!acquiredDevices.ContainsKey(device)) throw new ArgumentException("Device not found");
            acquiredDevices.Remove(device);
        }
    }
}
