
using LeagueHashes.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LeagueHashes.Views
{
    public sealed partial class XXHash32Page : Page
    {
        public XXHash32ViewModel ViewModel { get; } = new XXHash32ViewModel();

        public XXHash32Page()
        {
            InitializeComponent();
            NavigationCacheMode = Services.SettingsService.Instance.NavigationCacheMode;
        }
    }
}
