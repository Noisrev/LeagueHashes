using LeagueHashes.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LeagueHashes.Views
{
    public sealed partial class RSTPage : Page
    {
        public RSTViewModel ViewModel { get; } = new RSTViewModel();

        public RSTPage()
        {
            InitializeComponent();
            NavigationCacheMode = Services.SettingsService.Instance.NavigationCacheMode;
        }
    }
}
