using ListApp.Constants;
using ListApp.Models.Settings;
using ListApp.Pages;
using ListApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private const string GeneralTitle = "General";

        private readonly IThemeService _themeService;

        private List<SettingGroup> allSettingGroups = new List<SettingGroup>();
        public List<SettingGroup> AllSettingGroups
        {
            get => allSettingGroups;
            set
            {
                allSettingGroups = value;
                OnPropertyChanged();
            }
        }

        public ICommand SystemThemeCommand { get; set; }
        public ICommand AboutCommand { get; set; }

        public SettingsViewModel(INavigationService navigationService,
            IThemeService themeService)
            : base(navigationService)
        {
            _themeService = themeService;

            Title = "Settings";
        }

        public override async Task LoadData()
        {
            SetUpCommands();
            AllSettingGroups = await BuildSettings();
        }

        private void SetUpCommands()
        {
            SystemThemeCommand = new Command<string>(SetTheme);
            AboutCommand = new Command(async () => await GoToAboutPage());
        }

        private Task<List<SettingGroup>> BuildSettings()
        {
            List<BaseSetting> generalGroup = new List<BaseSetting>
            {
                GetSystemThemeSetting(),
                new ActionSetting
                {
                    Title = SettingsConstants.Settings.About,
                    OnActionCommand = AboutCommand
                }
            };

            allSettingGroups.Add(new SettingGroup(GeneralTitle, generalGroup));
            return Task.FromResult(allSettingGroups);
        }

        #region Theme 

        public BaseSetting GetSystemThemeSetting()
        {
            var userThemeSetting = GetPreferredSystemTheme();

            return new RadioSetting
            {
                Title = SettingsConstants.Settings.AppTheme,
                Options = new RadioOption[]
                {
                    new RadioOption
                    {
                        Title = SettingsConstants.Themes.ThemeSystem,
                        IsChecked = SettingsConstants.Themes.ThemeSystem == userThemeSetting

                    },
                    new RadioOption
                    {
                        Title = SettingsConstants.Themes.ThemeDark,
                        IsChecked = SettingsConstants.Themes.ThemeDark == userThemeSetting

                    },
                    new RadioOption
                    {
                        Title = SettingsConstants.Themes.ThemeLight,
                        IsChecked = SettingsConstants.Themes.ThemeLight == userThemeSetting
                    }
                },
                OnSelectionChangedCommand = SystemThemeCommand
            };
        }

        private string GetPreferredSystemTheme()
        {
            return _themeService.GetCurrentlySelectedTheme();
        }

        private void SetTheme(string theme)
        {
            if (string.IsNullOrWhiteSpace(theme))
            {
                return;
            }

            _themeService.ChangeTheme(theme);
        }

        #endregion

        private async Task GoToAboutPage()
        {
            await NavigationService.PushModalAsync<AboutPage>();
        }
    }
}
