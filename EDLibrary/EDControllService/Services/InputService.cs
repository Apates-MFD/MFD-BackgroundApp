using EDLibrary.EDStatusInput;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EDLibrary.EDControllService.Services
{
    public class InputService : Service
    {
        private List<MFDInput> inputs = new List<MFDInput>();     

        public void EventAction(string _type, string _event, MFDType origin, EventHandler<MFDButtonEventArgs> handler)
        {
            MFDInput input = inputs.Find(e => e.SelectedMFD.Equals(origin));
            if (input == null) throw new ArgumentException("MFD Input not aquierd");
                         
            if (_type == "+=" && _event == "ButtonPressed")  input.ButtonPressed  += handler;
            if (_type == "+=" && _event == "ButtonReleased") input.ButtonReleased += handler;
            if (_type == "-=" && _event == "ButtonPressed")  input.ButtonPressed  -= handler;
            if (_type == "-=" && _event == "ButtonReleased") input.ButtonReleased += handler;    
        }

        public bool ReadMFD(MFDType type)
        {
            if (inputs.Find(e => e.SelectedMFD.Equals(type)) != null) return true;

            try
            {
                MFDInput input = new MFDInput(type);
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
            foreach (MFDInput i in inputs)
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
