using LeagueHashes.Helpers;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;

namespace LeagueHashes.Services
{
    public class SettingsService
    {
        private const string NavigationCacheModeKey = "NavigationCacheMode";

        public static SettingsService Instance
        {
            get
            {
                if (instance == null)
                    instance = new SettingsService();
                return instance;
            }
        }
        private static SettingsService instance;

        public NavigationCacheMode NavigationCacheMode { get; set; }


        public SettingsService()
        {
        }

        public async Task InitializedAsync()
        {
            NavigationCacheMode = await ApplicationData.Current.LocalSettings.ReadAsync<NavigationCacheMode>(NavigationCacheModeKey);
            await Task.CompletedTask;
        }

        public async Task SetNavigationCacheModeAsync(NavigationCacheMode navigationCacheMode)
        {
            NavigationCacheMode = navigationCacheMode;

            await SaveSettingsAsync();
        }
        public async Task SaveSettingsAsync()
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(NavigationCacheModeKey, NavigationCacheMode);

            await Task.CompletedTask;
        }
    }
}
