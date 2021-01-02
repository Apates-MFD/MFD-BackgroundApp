using SharpDX.DirectInput;
using System;
using System.Threading;

namespace EDLibrary.ControllInput
{
    /// <summary>
    /// DirectInput implementation for Thrustmaster MFD
    /// </summary>
    public class InputDevice
    {
        public InputDeviceNames InputDeviceName { get; private set; }

        public event EventHandler<InputEventArgs> ButtonEvent;

        private bool running;

        private Joystick joystick = null;

        private bool[] buttonStates;

        /// <summary>
        /// InputDevice Constructor
        /// </summary>
        /// <param name="name">Name of the device</param>
        /// <exception cref="ArgumentException">Throws when no device is found with given name</exception>
        public InputDevice(InputDeviceNames name)
        {
            this.InputDeviceName = name;
            var directInput = new DirectInput();
            var devices = directInput.GetDevices(DeviceType.Supplemental, DeviceEnumerationFlags.AllDevices);

            foreach (DeviceInstance device in devices)
            {
                if (device.InstanceName == InputDeviceName.Value)
                {
                    joystick = new Joystick(directInput, device.InstanceGuid);
                    joystick.Acquire();
                    buttonStates = new bool[joystick.GetCurrentState().Buttons.Length];
                }
            }

            if (joystick == null) throw new ArgumentException("Device not found");
        }

        /// <summary>
        /// Updates button states and invokes event if button states has changed
        /// </summary>
        private void updateStates()
        {
            bool[] currentState = joystick.GetCurrentState().Buttons;
            for (int i = 0; i < currentState.Length; i++)
            {
                if (buttonStates[i] != currentState[i])
                {
                    buttonStates[i] = !buttonStates[i];
                    if (ButtonEvent != null) ButtonEvent.Invoke(this, new InputEventArgs { Button = new InputButton { ButtonNum = i, ButtonState = buttonStates[i] } });
                   
                }
            }
        }

        /// <summary>
        /// Starts observing device
        /// </summary>
        public void Observe()
        {
            running = true;
            while (running)
            {
                updateStates();
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Stops observing
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// Overriden Equels Method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns><see langword="false"/> if object type does not match with
        /// <see cref="InputDevice"/> or <see cref="InputDeviceName"/> does not match; otherwise, <see langword="true"/>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            InputDevice other = (InputDevice)obj;

            return this.InputDeviceName == other.InputDeviceName;
        }

        /// <summary>
        /// Overriden hashcode method
        /// </summary>
        /// <returns>Hash calculated from <see cref="InputDeviceName"/></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(InputDeviceName);
        }
    }
}
