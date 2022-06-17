using System;
using System.Globalization;
using Xamarin.Forms;

namespace JLOFT.Mobile.Converters
{
    public class StringIsEmptyBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                return !string.IsNullOrEmpty(strValue);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

