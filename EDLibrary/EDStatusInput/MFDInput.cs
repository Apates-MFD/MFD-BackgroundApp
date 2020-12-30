using SharpDX.DirectInput;
using System;
using System.Threading;

namespace EDLibrary.EDStatusInput
{
    public class MFDInput
    {
        public MFDType SelectedMFD { get; private set; }

        private bool running;

        private Joystick mfd = null;

        private bool[] buttonState;

        public MFDInput(MFDType type)
        {
            this.SelectedMFD = type;
            var directInput = new DirectInput();
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Supplemental, DeviceEnumerationFlags.AllDevices))
            {
                if (deviceInstance.InstanceName == SelectedMFD.Value)
                {
                    mfd = new Joystick(directInput, deviceInstance.InstanceGuid);
                    try
                    {
                        mfd.Acquire();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    buttonState = new bool[mfd.GetCurrentState().Buttons.Length];
                }
            }

            if (mfd == null) throw new Exception("Device not found");
        }

        private void updateStates()
        {
            bool[] currentState = mfd.GetCurrentState().Buttons;
            for (int i = 0; i < currentState.Length; i++)
            {
                if (buttonState[i] != currentState[i])
                {
                    buttonState[i] = !buttonState[i];
                    if (ButtonPressed != null && buttonState[i]) ButtonPressed.Invoke(this, new MFDButtonEventArgs { Button = new MFDButton { ButtonNum = i } });
                    if (ButtonReleased != null && !buttonState[i]) ButtonReleased.Invoke(this, new MFDButtonEventArgs { Button = new MFDButton { ButtonNum = i } });
                }
            }
        }

        public void Observe()
        {
            running = true;
            while (running)
            {
                updateStates();
                Thread.Sleep(10);
            }
        }

        public void Stop()
        {
            running = false;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            MFDInput other = (MFDInput)obj;

            return this.SelectedMFD == other.SelectedMFD;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SelectedMFD);
        }

        public event EventHandler<MFDButtonEventArgs> ButtonPressed;
        
        public event EventHandler<MFDButtonEventArgs> ButtonReleased;

    }
}
