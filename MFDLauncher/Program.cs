using System;
using System.Windows;
namespace MFDLauncher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            _ = EDLibrary.Controller.Instance;
            //Application app = EDLibrary.Controller.Instance.GetApplication();
            // app.Run(); 
            while (true) ;
        }
    }
}
