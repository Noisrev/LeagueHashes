using LeagueHashes.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LeagueHashes.Views
{
    public sealed partial class Fnv32Page : Page
    {
        public Fnv32ViewModel ViewModel { get; } = new Fnv32ViewModel();

        public Fnv32Page()
        {
            InitializeComponent();
            NavigationCacheMode = Services.SettingsService.Instance.NavigationCacheMode;
        }
    }
}
