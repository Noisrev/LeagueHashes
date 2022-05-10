using LeagueHashes.ViewModels;
using Windows.UI.Xaml.Controls;


namespace LeagueHashes.Views
{
    public sealed partial class XXHash64Page : Page
    {
        public XXHash64ViewModel ViewModel { get; } = new XXHash64ViewModel();
        public XXHash64Page()
        {
            InitializeComponent();
            NavigationCacheMode = Services.SettingsService.Instance.NavigationCacheMode;
        }
    }
}
