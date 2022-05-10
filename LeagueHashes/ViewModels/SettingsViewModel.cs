using LeagueHashes.Helpers;
using LeagueHashes.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LeagueHashes.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings.md
    public class SettingsViewModel : ObservableObject
    {
        public ElementTheme ElementTheme
        {
            get => _elementTheme;
            set => SetProperty(ref _elementTheme, value);
        }

        public NavigationCacheMode NavigationCacheMode
        {
            get => _navigationCacheMode;
            set => SetProperty(ref _navigationCacheMode, value);
        }

        public string VersionDescription
        {
            get => _versionDescription;
            set => SetProperty(ref _versionDescription, value);
        }

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        public ICommand SwitchNavigationCacheModeCommand
        {
            get
            {
                if (_switchNavigationCacheModeCommand == null)
                {
                    _switchNavigationCacheModeCommand = new RelayCommand<NavigationCacheMode>(
                        async (param) =>
                        {
                            NavigationCacheMode = param;
                            await SettingsService.Instance.SetNavigationCacheModeAsync(param);

                            if (!_valueHasBeenChanged)
                            {
                                _valueHasBeenChanged = true;
                                ContentDialog contentDialog = new ContentDialog()
                                {
                                    Title = "Restart to apply the change",
                                    Content = "You need to restart the application to apply the change.",
                                    PrimaryButtonText = "Restart",
                                    CloseButtonText = "Cancel",
                                    DefaultButton = ContentDialogButton.Primary
                                };

                                var result = await contentDialog.ShowAsync();
                                if (result == ContentDialogResult.Primary)
                                {
                                    await CoreApplication.RequestRestartAsync(string.Empty);
                                }
                            }
                        });
                }
                return _switchNavigationCacheModeCommand;
            }
        }

        public SettingsViewModel()
        {
        }

        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private NavigationCacheMode _navigationCacheMode = SettingsService.Instance.NavigationCacheMode;

        private bool _valueHasBeenChanged;
        private string _versionDescription;
        private ICommand _switchThemeCommand;
        private ICommand _switchNavigationCacheModeCommand;

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            string appName = "AppDisplayName".GetLocalized();
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
