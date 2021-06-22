using LeagueHashes.ViewModels;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LeagueHashes.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CCITT32Page : Page
    {
        public CCITT32ViewModel ViewModel = new CCITT32ViewModel();
        public CCITT32Page()
        {
            InitializeComponent();
        }
    }
}
