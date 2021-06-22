
using LeagueHashes.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LeagueHashes.Views
{
    public sealed partial class TroyPage : Page
    {
        public TroyViewModel ViewModel { get; } = new TroyViewModel();

        public TroyPage()
        {
            InitializeComponent();
        }
    }
}
