using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EDLibrary.UI;

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

        public void Clear()
        {
            foreach(TextBlock block in buttonTexts)
            {
                block.Foreground = Brushes.Lime;
                block.Background = Brushes.Black;
                block.Text = "";
            }
        }
        public void InvertState(int position)
        {
            textBlockInvert(buttonTexts[position]);
        }

        public void SetText(int position, string text, bool invert)
        {
            buttonTexts[position].Text = text;
            if (invert) textBlockInvert(buttonTexts[position]);
        }
            
        private void textBlockInvert(TextBlock block)
        {
            Brush tmp = block.Foreground;
            block.Foreground = block.Background;
            block.Background = tmp;
        }

        public void SetTextThreadSafe(int position, string text, bool invert)
        {
            this.Dispatcher.Invoke(new Action(() =>{
                this.SetText(position, text, invert);         
           }));
        }

        public void InvertStateThreadSafe(int position)
        {
            this.Dispatcher.Invoke(new Action(() => {
                this.InvertState(position);
            }));
        }

        public void ClearThreadSafe()
        {
            this.Dispatcher.Invoke(new Action(() => {
                this.Clear();
            }));
        }
    }
}
