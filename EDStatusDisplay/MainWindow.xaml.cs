using EDLibrary;
using EDLibrary.EDStatusInput;
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
            _ = MFD_TWO.Instance;
            _ = MainController.Instance;
            MainController.Instance.AssignPanel(MFD_TWO.Instance, MFDType.MFD_TWO);
            MFD_TWO.Instance.Show();
        }

        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
         
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MFD_TWO.Instance.Close();
            MainController.Instance.Quit();
            base.OnClosing(e);
        }
    }
}
