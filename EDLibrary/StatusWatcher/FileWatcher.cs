using System.IO;

namespace EDLibrary.StatusWatcher
{

    /// <summary>
    /// FileWatcher, raises event every time status.json file has changed
    /// </summary>
    public class FileWatcher
    {
        private static bool running;

        /// <summary>
        /// Starts <see cref="FileSystemWatcher"/>
        /// <para>Watches over <see cref="Constants.PathToStatus"/></para>
        /// </summary>
        public static void Run()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = Constants.PathToStatusFolder;
                watcher.NotifyFilter = NotifyFilters.LastAccess;
                watcher.Filter = "Status.json";
                watcher.Changed += OnChanged;
                watcher.EnableRaisingEvents = true;

                running = true;
                while (running) ;
            }
        }

        /// <summary>
        /// Stops the thread
        /// </summary>
        public static void Stop()
        {
            running = false;
        }

        /// <summary>
        /// Event that gets invoked if timestamp on file changes
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            StatusParser.Parse();
        }

    }
}
