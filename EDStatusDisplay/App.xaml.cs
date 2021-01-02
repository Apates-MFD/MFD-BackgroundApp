using System.Diagnostics;
using System.Windows;

namespace EDStatusDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Debug.WriteLine("Hellow");
        }

        protected override void OnExit(ExitEventArgs e)
        {

            Debug.WriteLine("Byee");
            base.OnExit(e);
        }
    }
}
