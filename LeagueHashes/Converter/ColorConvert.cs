using System;
using Windows.UI.Xaml.Data;

namespace LeagueHashes.Converter
{
    public class ColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((byte)value / (float)255).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
