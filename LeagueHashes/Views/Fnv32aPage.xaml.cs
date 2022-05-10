using LeagueHashes.ViewModels;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LeagueHashes.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Fnv32aPage : Page
    {
        public Fnv32aViewModel ViewModel { get; } = new Fnv32aViewModel();
        public Fnv32aPage()
        {
            InitializeComponent();
            NavigationCacheMode = Services.SettingsService.Instance.NavigationCacheMode;
        }
    }
}
