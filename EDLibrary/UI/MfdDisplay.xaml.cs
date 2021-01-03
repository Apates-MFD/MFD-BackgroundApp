﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private double brightnessFactor = 1.0;
        private double brightnessStep = 0.2;
        private double contrastFactor = 1.0;
        private double contrastStep = 8;

        private TextBlock[] buttonTexts;
        private SolidColorBrush foregroundBrush;
        private SolidColorBrush backgroundBrush;
        private Color defaultForegroundColor;
        private Color defaultBackgroundColor;
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

            var keys = this.Resources.Keys.GetEnumerator();
            var values = this.Resources.Values.GetEnumerator();
           
            while (keys.MoveNext()) 
            {
                values.MoveNext();
                if ((string)keys.Current == "BackgroundBrush")
                {
                    backgroundBrush = (SolidColorBrush)values.Current;
                    defaultBackgroundColor = backgroundBrush.Color;
                }
                if ((string)keys.Current == "ForegroundBrush")
                {
                    foregroundBrush  = (SolidColorBrush)values.Current;
                    defaultForegroundColor = foregroundBrush.Color;
                }

            } 
            
            //mediaElement.Source = new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
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

        private void clear()
        {
            foreach (TextBlock block in buttonTexts)
            {
                block.Foreground = foregroundBrush;
                block.Background = backgroundBrush;
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
                buttonTexts[position].Foreground = inverted ? backgroundBrush : foregroundBrush;
                buttonTexts[position].Background = inverted ? foregroundBrush : backgroundBrush;
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

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// IncreaseBrightness
        /// </summary>
        public void IncreaseBrightness()
        {
            brightnessFactor += brightnessStep;
            changeBrightness(defaultForegroundColor,foregroundBrush);
            changeBrightness(defaultBackgroundColor, backgroundBrush);
        }

        /// <summary>
        /// DecreaseBrightness
        /// </summary>
        public void DecreaseBrightness()
        {
            brightnessFactor -= brightnessStep;
            if (brightnessFactor < 0) brightnessFactor = 0;
            changeBrightness(defaultForegroundColor, foregroundBrush);
            changeBrightness(defaultBackgroundColor, backgroundBrush);
        }

        /// <summary>
        /// IncreaseContrast
        /// </summary>
        public void IncreaseContrast()
        {
            contrastFactor += contrastStep;
            changeContrast(defaultForegroundColor, foregroundBrush);
            changeContrast(defaultBackgroundColor, backgroundBrush);
        }

        /// <summary>
        /// DecreaseContrast
        /// </summary>
        public void DecreaseContrast()
        {
            contrastFactor -= contrastStep;
            if (contrastFactor < 0) contrastFactor = 0;
            changeContrast(defaultForegroundColor, foregroundBrush);
            changeContrast(defaultBackgroundColor, backgroundBrush);
        }

        /// <summary>
        /// Changes Brightness
        /// </summary>
        /// <param name="defaultC"></param>
        /// <param name="brush"></param>
        private void changeBrightness(Color defaultC, SolidColorBrush brush)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                Color color = defaultC;
                color.R = (Byte)Math.Clamp(color.R * brightnessFactor, 0, 255);
                color.G = (Byte)Math.Clamp(color.G * brightnessFactor, 0, 255);
                color.B = (Byte)Math.Clamp(color.B * brightnessFactor, 0, 255);
                Debug.WriteLine("{0} {1} {2}", color.R, color.G, color.B);
                brush.Color = color;
            }));
        }

        /// <summary>
        /// changes contrast
        /// </summary>
        /// <param name="defaultC"></param>
        /// <param name="brush"></param>
        private void changeContrast(Color defaultC, SolidColorBrush brush)
        {
            var fac = (259 * (contrastFactor + 255)) / (255 * (259 - contrastFactor));
            Dispatcher.Invoke(new Action(() =>
            {
                Color color = defaultC;
                color.R = (Byte)Math.Clamp(Math.Truncate(fac * (color.R - 128) + 128), 0,255);
                color.G = (Byte)Math.Clamp(Math.Truncate(fac * (color.G - 128) + 128), 0, 255);
                color.B = (Byte)Math.Clamp(Math.Truncate(fac * (color.B - 128) + 128), 0, 255);
                 
                Debug.WriteLine("{0} {1} {2}", color.R, color.G, color.B);
                brush.Color = color;
            }));
        }
    }
}

