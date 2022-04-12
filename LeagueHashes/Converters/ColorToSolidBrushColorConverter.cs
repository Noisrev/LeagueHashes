using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace LeagueHashes.Converters
{
    public class ColorToSolidBrushColorConverter : IValueConverter
    {
        private static SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            return TransparentBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
