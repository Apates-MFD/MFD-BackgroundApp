using EDLibrary.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EDStatusDisplay
{
    public partial class MFD_TWO : Window, IPanel
    {
        #region Singelton
        private static readonly MFD_TWO instance = new MFD_TWO();

        private MFD_TWO()
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
        }
        public static MFD_TWO Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

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


        public void Clear()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.clear();
            }));
        }

        public void SetInverted(int position, bool inverted)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                buttonTexts[position].Foreground = inverted ? Background : Foreground;
                buttonTexts[position].Foreground = inverted ? Foreground : Background;
            }));
        }

        public void SetText(int position, string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                buttonTexts[position].Text = text;
            }));
        }

        public void SetEnabled(int position, bool enabled)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                buttonTexts[position].Opacity = enabled ? 1.0 : 0.0;
            }));
        }
    }
}
