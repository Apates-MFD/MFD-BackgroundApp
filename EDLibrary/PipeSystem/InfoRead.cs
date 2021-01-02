using EDLibrary.StatusWatcher;
using System;
using System.ComponentModel;
using System.Threading;

namespace EDLibrary.PipeSystem
{
    /// <summary>
    /// Reads updated info
    /// </summary>
    class InfoRead : PipeRead
    {
        public override event EventHandler DataReceived;

        public InfoRead()
        {
            Status.Instance.PropertyChanged += PropertyChanged;
            Thread t = new Thread(new ThreadStart(FileWatcher.Run));
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// Property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (DataReceived != null) DataReceived.Invoke(sender, e);
        }

        /// <summary>
        /// Quits Filewatcher thread
        /// </summary>
        public override void Exit()
        {
            FileWatcher.Stop();
        }
    }
}
