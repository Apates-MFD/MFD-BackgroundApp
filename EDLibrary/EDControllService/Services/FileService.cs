using EDLibrary.EDStatusWatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;


namespace EDLibrary.EDControllService.Services
{
    public class FileService : Service
    {
        private Dictionary<string, EventHandler<SingelPropertyChangedEventArgs>> subscriptions = new Dictionary<string, EventHandler<SingelPropertyChangedEventArgs>>();

        private void init()
        {
            Status.Instance.PropertyChanged += StatusPropertyChanged;
            new Thread(new ThreadStart(FileWatcher.Run)).Start();
        }

        public void Quit()
        {
            FileWatcher.Stop();
        }

        private void StatusPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var prop = Status.Instance.GetType().GetProperty(e.PropertyName);

            if (subscriptions.ContainsKey(prop.Name) && subscriptions[prop.Name] != null)
            {
                subscriptions[prop.Name].Invoke(this, new SingelPropertyChangedEventArgs() { PropertyInfo = prop });
            }
        }
    
        public void SubscribeTo(string propertyName, EventHandler<SingelPropertyChangedEventArgs> callback)
        {
            if (!subscriptions.ContainsKey(propertyName))
            {
                subscriptions.Add(propertyName, callback);
            }
            else
            {
                subscriptions[propertyName] += callback;
            }
        }

        public void UnsubscribteTo(string propertyName, EventHandler<SingelPropertyChangedEventArgs> callback)
        {
            if (subscriptions.ContainsKey(propertyName))
            {
                subscriptions[propertyName] -= callback;
            }
        }

        #region Singelton
        private static readonly FileService instance = new FileService();

        private FileService()
        {
            init();
        }
        public static FileService Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
    }
}
