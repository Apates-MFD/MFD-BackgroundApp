using System;
using System.Globalization;
using System.Windows.Data;

namespace BackgroundLibrary.UI
{
    public class WidhtToFontConverter : IValueConverter
    {

        /// <summary>
        /// Automaticly adjust text size
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return (Double)value / 2.8;
        }

        /// <summary>
        /// There is no convert back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
