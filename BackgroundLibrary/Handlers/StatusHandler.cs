using BackgroundLibrary.PipeSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BackgroundLibrary.Handlers
{
    /// <summary>
    /// Status Handler Class
    /// <para>Reads all status updates and forwards the update to it's subscribers</para>
    /// </summary>
    class StatusHandler
    {
        private InfoRead infoPipe;
        private Dictionary<string, EventHandler<PropertyChangedEventArgs>> subscriber = new Dictionary<string, EventHandler<PropertyChangedEventArgs>>();

        /// <summary>
        /// Constructor
        /// </summary>
        public StatusHandler(string pathToStatusFolder)
        {
            infoPipe = new InfoRead(pathToStatusFolder);
            infoPipe.DataReceived += InfoReceived;
        }

        /// <summary>
        /// Info Received event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoReceived(object sender, EventArgs e)
        {
            PropertyChangedEventArgs args = (PropertyChangedEventArgs)e;
            if (subscriber.ContainsKey(args.PropertyName))
            {
                subscriber[args.PropertyName].Invoke(sender, args);
            }
        }

        /// <summary>
        /// Subscribes to <param name="propertyName"/>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="callback"></param>
        public void SubscribeTo(string propertyName,EventHandler<PropertyChangedEventArgs> callback)
        {
            if (subscriber.ContainsKey(propertyName))
            {
                subscriber[propertyName] += callback;
            }
            else
            {
                subscriber.Add(propertyName, callback);
            }
        }

        /// <summary>
        /// Unsubscribes to <param name="propertyName"/>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="callback"></param>
        public void UnsubscribeFrom(string propertyName, EventHandler<PropertyChangedEventArgs> callback)
        {

            if (subscriber.ContainsKey(propertyName))
            {
                subscriber[propertyName] -= callback;
                if (subscriber[propertyName] == null) subscriber.Remove(propertyName);
            }

        }
    }
}
