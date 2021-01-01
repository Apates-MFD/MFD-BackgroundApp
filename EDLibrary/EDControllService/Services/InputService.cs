using EDLibrary.ControllInput;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EDLibrary.EDControllService.Services
{
    public class InputService : Service
    {
        private List<InputDevice> inputs = new List<InputDevice>();     

        public void EventAction(string _type, string _event, InputDeviceNames origin, EventHandler<InputEventArgs> handler)
        {
            InputDevice input = inputs.Find(e => e.InputDeviceName.Equals(origin));
            if (input == null) throw new ArgumentException("MFD Input not aquierd");
                         
            if (_type == "+=" && _event == "ButtonPressed")     input.ButtonPressed     += handler;
            if (_type == "+=" && _event == "ButtonReleased")    input.ButtonReleased    += handler;          
            if (_type == "-=" && _event == "ButtonPressed")     input.ButtonPressed     -= handler;
            if (_type == "-=" && _event == "ButtonReleased")    input.ButtonReleased    -= handler;          
        }

        /*public void MenuEventAction(string _type, InputDeviceNames origin, EventHandler<MFDMenuButtonEventArgs> handler)
        {
            InputDevice input = inputs.Find(e => e.InputDeviceName.Equals(origin));
            if (input == null) throw new ArgumentException("MFD Input not aquierd");
            if (_type == "+=") input.MenuOptionPressed += handler;
            if (_type == "-=") input.MenuOptionPressed -= handler;
        }*/
        public bool ReadMFD(InputDeviceNames type)
        {
            if (inputs.Find(e => e.InputDeviceName.Equals(type)) != null) return true;

            try
            {
                InputDevice input = new InputDevice(type);
                inputs.Add(input);
                new Thread(new ThreadStart(input.Observe)).Start();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void Quit()
        {
            foreach (InputDevice i in inputs)
            {
                i.Stop();
            }
        }
        #region Singelton
        private static readonly InputService instance = new InputService();

        private InputService()
        {
        }

        public static InputService Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion        
    }
}
