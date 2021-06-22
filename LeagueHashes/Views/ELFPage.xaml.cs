
using LeagueHashes.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LeagueHashes.Views
{
    public sealed partial class ELFPage : Page
    {
        public ELFViewModel ViewModel { get; } = new ELFViewModel();

        public ELFPage()
        {
            InitializeComponent();
        }
    }
}
