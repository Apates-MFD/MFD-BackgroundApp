using EDLibrary.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using EDLibrary.ControllInput;
using System.Windows.Threading;
using System.Threading;

namespace EDLibrary.Handlers
{
    /// <summary>
    /// Handels the gui
    /// </summary>
    public class InterfaceHandler
    {
        public Application Application { get; private set; }
        public event EventHandler ReloadConfig;
        public event EventHandler SaveConfig;

        private Dictionary<InputDeviceNames, MfdDisplay> linkedPanels = new Dictionary<InputDeviceNames, MfdDisplay>();
        private List<MfdDisplay> notShowedDisplays = new List<MfdDisplay>();
        private bool applicationRunning = false;

        public InterfaceHandler()
        {
            Application = new Application();
            Application.Startup += Application_Startup;
        }

        /// <summary>
        /// activates all displays that got called while application wasn't ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Dispatcher.Invoke(new Action(() =>
            {
                notShowedDisplays.ForEach(d => d.Show());
            }));
            applicationRunning = true;
        }

        /// <summary>
        /// Returns panel
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public IPanel GetPanel(InputDeviceNames device)
        {
            if (linkedPanels.ContainsKey(device)) return linkedPanels[device];
            
            MfdDisplay newDisplay = initDisplay();           
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
        private MfdDisplay initDisplay()
        {
            MfdDisplay mfdDisplay = null;
            Application.Dispatcher.Invoke(new Action(() =>
            {
                mfdDisplay = new MfdDisplay();
            }));
            mfdDisplay.Closed += MfdDisplay_Closed;
            mfdDisplay.ReloadConfig += ReloadConfig;
            mfdDisplay.SaveConfig += SaveConfig;
            showDisplay(mfdDisplay);
            return mfdDisplay;
        }

        /// <summary>
        /// Closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MfdDisplay_Closed(object sender, EventArgs e)
        {
            foreach(var pair in linkedPanels)
            {
                if (pair.Value.Equals((MfdDisplay)sender))
                {
                    linkedPanels.Remove(pair.Key);
                }
            }
        }

        /// <summary>
        /// showing display command
        /// </summary>
        /// <param name="mfdDisplay"></param>
        private void showDisplay(MfdDisplay mfdDisplay)
        {

            if (applicationRunning)
            {
                Application.Dispatcher.Invoke(new Action(() =>
                {
                    mfdDisplay.Show();
                }));
            }
            else
            {
                notShowedDisplays.Add(mfdDisplay);
            }
        }
    }
}
