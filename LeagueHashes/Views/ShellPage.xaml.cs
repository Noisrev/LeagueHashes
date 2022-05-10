using LeagueHashes.Services;
using LeagueHashes.ViewModels;
using System.Collections.Generic;
using System.Linq;
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
            /** I tried to determine the SelectedItems of the ListView by binding,
             *  but I couldn't bind it, nor could I get elements in the DataTemplate via ElementName.
             *  Why does UWP not have this capability?
             *  So I have to do this in the CS file
             *  Maybe!?
             */
            var menuFlyout = (MenuFlyout)FlyoutBase.GetAttachedFlyout((FrameworkElement)sender);
            var copyMenuItem = menuFlyout.Items[0];
            var deleteMenuItem = menuFlyout.Items[1];
            var clearMenuItem = menuFlyout.Items[3];

            copyMenuItem.IsEnabled = deleteMenuItem.IsEnabled = HistoryListView.SelectedItems.Any();
            clearMenuItem.IsEnabled = HistoryListView.Items.Any();
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Copy_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (HistoryListView.SelectedItems.Any())
            {
                CoerceCopy(HistoryListView.SelectedItems.Cast<KeyValuePair<string, HistoryEntry>>());
            }
        }

        private void Delete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (HistoryListView.SelectedItems.Any())
            {
                ViewModel.RemoveHistory(HistoryListView.SelectedItems.Cast<KeyValuePair<string, HistoryEntry>>());
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearAllHistory();
        }


        private void CoerceCopy(IEnumerable<KeyValuePair<string, HistoryEntry>> historyEntries)
        {
            DataPackage dataPackage = new DataPackage
            {
                RequestedOperation = DataPackageOperation.Copy
            };

            string finalString = string.Empty;
            foreach (var entry in historyEntries)
                finalString += entry.Key + "\n";

            dataPackage.SetText(finalString);
            Clipboard.SetContent(dataPackage);
        }
    }
}
