using System.IO;

namespace EDLibrary.EDStatusWatcher
{

    /// <summary>
    /// FileWatcher, raises event every time status.json file has changed
    /// </summary>
    public class FileWatcher
    {
        private static bool running;
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

        public static void Stop()
        {
            running = false;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            StatusParser.Parse();
        }

    }
}
