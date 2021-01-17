﻿using BackgroundLibrary.StatusWatcher;
using System;
using System.ComponentModel;
using System.Threading;

namespace BackgroundLibrary.PipeSystem
{
    /// <summary>
    /// Reads updated info
    /// </summary>
    class InfoRead : PipeRead
    {
        public override event EventHandler DataReceived;

        public InfoRead(string PathToStatusFolder)
        {
            Status.Instance.PropertyChanged += PropertyChanged;
            FileWatcher.SetPathToStatusFolder(PathToStatusFolder);
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
    }
}
