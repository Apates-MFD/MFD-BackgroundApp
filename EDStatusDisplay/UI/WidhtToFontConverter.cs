using System;
using System.Globalization;
using System.Windows.Data;

namespace EDStatusDisplay
{
    public class WidhtToFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return (Double)value / 2.8;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
