using EDLibrary;
using EDLibrary.ControllInput;
using System.ComponentModel;
using System.Windows;

namespace EDStatusDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSpawnMFD_Click(object sender, RoutedEventArgs e)
        {
            InputDevice device = new InputDevice(InputDeviceNames.MFD_TWO);
            device.Observe();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }
}
