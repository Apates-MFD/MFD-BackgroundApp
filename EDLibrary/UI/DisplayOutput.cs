using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.UI
{
    class DisplayOutput : IPanel
    {
        public void Show()
        {
            Console.WriteLine("Show");
        }
        public void Clear()
        {
            Console.WriteLine("Clear");
        }

        public void DecreaseBrightness()
        {
            Console.WriteLine("DecreaseBrightness");
        }

        public void DecreaseContrast()
        {
            Console.WriteLine("DecreaseContrast");
        }

        public double[] GetDisplaySetting()
        {
            Console.WriteLine("GetDisplaySetting");
            return new[] { 0.0, 0.0, 0.0, 0.0 };
        }

        public double[] GetWindowProperty()
        {
            Console.WriteLine("GetWindowProperty");
            return new[] { 0.0, 0.0, 0.0, 0.0 };
        }

        public void IncreaseBrightness()
        {
            Console.WriteLine("IncreaseBrightness");
        }

        public void IncreaseContrast()
        {
            Console.WriteLine("IncreaseContrast");
        }

        public void SetDisplaySetting(double[] settings)
        {
            Console.WriteLine("SetDisplaySettings:{0},{1},{2},{3}", settings[0], settings[1],settings[2], settings[3]);
        }

        public void SetEnabled(int position, bool enabled)
        {
            Console.WriteLine("SetEnable:{0},{1}",position,enabled);
        }

        public void SetInverted(int position, bool inverted)
        {
            Console.WriteLine("SetInverted:{0},{1}",position,inverted);
        }

        public void SetText(int position, string text)
        {
            Console.WriteLine("SetText:{0},{1}",position,text);
        }

        public void SetWindowProperty(double[] porperty)
        {
            Console.WriteLine("SetWindowProperty:{0},{1},{2}", porperty[0], porperty[1], porperty[2]);
        }
    }
}
