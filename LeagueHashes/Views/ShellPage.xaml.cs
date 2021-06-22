using LeagueHashes.Core.Models;
using LeagueHashes.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace LeagueHashes.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

        private void NavigationViewItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void Grid_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void Copy_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item && item.DataContext is HistoryEntry historyEntry)
            {
                Copy(historyEntry);
            }
        }
        private void Copy(HistoryEntry historyEntry)
        {
            DataPackage dataPackage = new DataPackage
            {
                RequestedOperation = DataPackageOperation.Copy
            };
            dataPackage.SetText(historyEntry.Text);
            Clipboard.SetContent(dataPackage);
        }
        private void Delete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item && item.DataContext is HistoryEntry historyEntry)
            {
                ViewModel.History.Remove(historyEntry);
                if (ViewModel.History.Count == 0)
                {
                    ViewModel.HistoryViewVisibility = Visibility.Collapsed;
                    ViewModel.NoHistoricalContentPromptsVisibility = Visibility.Visible;
                }
            }
        }
    }
}
