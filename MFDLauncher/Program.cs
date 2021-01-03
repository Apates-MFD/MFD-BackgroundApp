using System;
using System.IO;
using System.Windows;
namespace MFDLauncher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            Console.WriteLine(Directory.GetCurrentDirectory());
            Application app = EDLibrary.Controller.Instance.GetApplication();
            app.Run();           
        }
    }
}
