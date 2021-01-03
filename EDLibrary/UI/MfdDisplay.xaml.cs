using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EDLibrary.UI
{
    /// <summary>
    /// Interaction logic for MfdDisplay.xaml
    /// </summary>
    public partial class MfdDisplay : Window, IPanel
    {
        public event EventHandler ReloadConfig;
        public event EventHandler SaveConfig;

        public MfdDisplay()
        {
            InitializeComponent();
            buttonTexts = new[]
           {
                txt_Btn_0,
                txt_Btn_1,
                txt_Btn_2,
                txt_Btn_3,
                txt_Btn_4,
                txt_Btn_5,
                txt_Btn_6,
                txt_Btn_7,
                txt_Btn_8,
                txt_Btn_9,
                txt_Btn_10,
                txt_Btn_11,
                txt_Btn_12,
                txt_Btn_13,
                txt_Btn_14,
                txt_Btn_15,
                txt_Btn_16,
                txt_Btn_17,
                txt_Btn_18,
                txt_Btn_19
            };
            this.Activated += MfdDisplay_Activated;
        }

        /// <summary>
        /// Triggers when shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MfdDisplay_Activated(object sender, EventArgs e)
        {
            ReloadConfig.Invoke(this, new EventArgs());
        }

        private TextBlock[] buttonTexts;
        private static new Brush Foreground = Brushes.Lime;
        private static new Brush Background = Brushes.Black;
        private void clear()
        {
            foreach (TextBlock block in buttonTexts)
            {
                block.Foreground = Foreground;
                block.Background = Background;
                block.Text = "";
                block.Opacity = 1.0;
            }
        }

        /// <summary>
        /// Clears display
        /// </summary>
        public void Clear()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.clear();
            }));
        }

        /// <summary>
        /// Sets invert for a textblock
        /// </summary>
        /// <param name="position"></param>
        /// <param name="inverted"></param>
        public void SetInverted(int position, bool inverted)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                buttonTexts[position].Foreground = inverted ? Background : Foreground;
                buttonTexts[position].Background = inverted ? Foreground : Background;
            }));
        }

        /// <summary>
        /// Sets text for textblock
        /// </summary>
        /// <param name="position"></param>
        /// <param name="text"></param>
        public void SetText(int position, string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                buttonTexts[position].Text = text;
            }));
        }

        /// <summary>
        /// hides textblock
        /// </summary>
        /// <param name="position"></param>
        /// <param name="enabled"></param>
        public void SetEnabled(int position, bool enabled)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                buttonTexts[position].Opacity = enabled ? 1.0 : 0.0;
            }));
        }

        private void EditModeClick(object sender, RoutedEventArgs e)
        {
            WindowStyle = WindowStyle.ToolWindow;
            ResizeMode = ResizeMode.CanResize;
        }
        private void FixedModeClick(object sender, RoutedEventArgs e)
        {
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
        }
        private void SaveConfigClick(object sender, RoutedEventArgs e)
        {
            if (SaveConfig != null) SaveConfig.Invoke(this, e);
        }
        private void ReloadConfigClick(object sender, RoutedEventArgs e)
        {
            if (ReloadConfig != null) ReloadConfig.Invoke(this, e);
        }

        /// <summary>
        /// Return the current position
        /// </summary>
        /// <returns></returns>
        public double[] GetWindowProperty()
        {
            return new []{Height, Width, Top, Left };        
        }

        /// <summary>
        /// Sets the current position
        /// </summary>
        /// <param name="properties"></param>
        public  void SetWindowProperty(double[] properties)
        {
            if (properties == null) return;
            Height = properties[0];
            Width = properties[1];
            Top = properties[2];
            Left = properties[3];
        }

        
    }
}
