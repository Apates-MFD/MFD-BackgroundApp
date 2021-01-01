using EDLibrary.StatusWatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace EDLibrary.PipeSystem
{
    //TODO Implement
    class InfoRead : PipeRead
    {
        public override event EventHandler DataReceived;

        public InfoRead()
        {
            Status.Instance.PropertyChanged += PropertyChanged;
            new Thread(new ThreadStart(FileWatcher.Run)).Start();
        }

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
