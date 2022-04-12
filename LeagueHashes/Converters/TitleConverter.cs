using LeagueHashes.Helpers;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml.Data;

namespace LeagueHashes.Converters
{
    public class TitleConverter : IValueConverter
    {
        private static readonly string SeparatorChar = " - ";
        private static readonly string Title = "AppDisplayName".GetLocalized();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            item = value;

            if (value is NavigationViewItem navigationViewItem && navigationViewItem.Content is string content)
                return Title + SeparatorChar + content;
            else
                return Title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return item;
        }

        private object item;
    }
}
