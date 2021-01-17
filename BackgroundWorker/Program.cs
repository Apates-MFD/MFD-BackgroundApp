using System;
using System.Windows;

namespace BackgroundWorker
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            _ = BackgroundLibrary.Controller.Instance;
            //Application app = BackgroundLibrary.Controller.Instance.GetApplication();
            // app.Run(); 
            while (true) ;
        }
    }
}
