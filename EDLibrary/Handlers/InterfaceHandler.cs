using EDLibrary.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using EDLibrary.ControllInput;
//using System.Windows.Threading;
using System.Threading;

namespace EDLibrary.Handlers
{
    /// <summary>
    /// Handels the gui
    /// </summary>
    public class InterfaceHandler
    {
       // public Application Application { get; private set; }
        public event EventHandler ReloadConfig;
        public event EventHandler SaveConfig;

        private Dictionary<InputDeviceNames, DisplayOutput> linkedPanels = new Dictionary<InputDeviceNames, DisplayOutput>();
        private List<DisplayOutput> notShowedDisplays = new List<DisplayOutput>();
        private bool applicationRunning = false;

        public InterfaceHandler()
        {
           /* Application = new Application();
            Application.Startup += Application_Startup;*/
        }

        /// <summary>
        /// activates all displays that got called while application wasn't ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Dispatcher.Invoke(new Action(() =>
            {
                notShowedDisplays.ForEach(d => d.Show());
            }));
            applicationRunning = true;
        }*/

        /// <summary>
        /// Returns panel
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public IPanel GetPanel(InputDeviceNames device)
        {
            if (linkedPanels.ContainsKey(device)) return linkedPanels[device];
            
            DisplayOutput newDisplay = initDisplay();           
            linkedPanels.Add(device, newDisplay);
            return newDisplay;
        }

        /// <summary>
        /// Returns name to panel
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public InputDeviceNames GetDeviceName(IPanel panel)
        {
            foreach(var pair in linkedPanels)
            {
                if (pair.Value == panel) return pair.Key;
            }
            return null;
        }

        /// <summary>
        /// Initial method for any display
        /// </summary>
        /// <returns></returns>
        private DisplayOutput initDisplay()
        {
            DisplayOutput DisplayOutput = null;
            DisplayOutput = new DisplayOutput();
            /*Application.Dispatcher.Invoke(new Action(() =>
            {
                DisplayOutput = new DisplayOutput();
            }));*/
            //DisplayOutput.Closed += DisplayOutput_Closed;
            //DisplayOutput.ReloadConfig += ReloadConfig;
            //DisplayOutput.SaveConfig += SaveConfig;
            showDisplay(DisplayOutput);
            return DisplayOutput;
        }

        /// <summary>
        /// Closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayOutput_Closed(object sender, EventArgs e)
        {
            foreach(var pair in linkedPanels)
            {
                if (pair.Value.Equals((DisplayOutput)sender))
                {
                    linkedPanels.Remove(pair.Key);
                }
            }
        }

        /// <summary>
        /// showing display command
        /// </summary>
        /// <param name="DisplayOutput"></param>
        private void showDisplay(DisplayOutput DisplayOutput)
        {

            if (applicationRunning)
            {
                /*Application.Dispatcher.Invoke(new Action(() =>
                {
                    DisplayOutput.Show();
                }));*/
            }
            else
            {
                notShowedDisplays.Add(DisplayOutput);
            }
        }
    }
}
