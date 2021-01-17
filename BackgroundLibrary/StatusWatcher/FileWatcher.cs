using System.IO;
using System;

namespace BackgroundLibrary.StatusWatcher
{

    /// <summary>
    /// FileWatcher, raises event every time status.json file has changed
    /// </summary>
    public class FileWatcher
    {
        private static bool running;
        private static string pathToStatusFolder;

        /// <summary>
        /// Sets path
        /// </summary>
        /// <param name="pathToStatusFolder"></param>
        public static void SetPathToStatusFolder(string pathToStatusFolder)
        {
            FileWatcher.pathToStatusFolder = pathToStatusFolder;
        }

        /// <summary>
        /// Starts <see cref="FileSystemWatcher"/>
        /// </summary>
        public static void Run()
        {         
            if (running) return;
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = Environment.ExpandEnvironmentVariables(pathToStatusFolder);
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
            Status.Parse(pathToStatusFolder + @"\Status.json");
        }

    }
}
