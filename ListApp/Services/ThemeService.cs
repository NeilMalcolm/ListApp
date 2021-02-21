using ListApp.Constants;

namespace ListApp.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IAppPreferences _appPreferences;
        private readonly IPlatformThemeService _platformThemeService;

        public ThemeService(IAppPreferences appPreferences,
            IPlatformThemeService platformThemeService)
        {
            _appPreferences = appPreferences;
            _platformThemeService = platformThemeService;
        }

        public void SetInitialTheme()
        {
            SetTheme(_appPreferences.UserPreferredAppTheme);
        }

        public string GetCurrentlySelectedTheme()
        {
            return _appPreferences.UserPreferredAppTheme;
        }

        public void ChangeTheme(string themeName)
        {
            _appPreferences.UserPreferredAppTheme = SetTheme(themeName);
        }

        private string SetTheme(string themeName)
        {
            switch (themeName)
            {
                case SettingsConstants.Themes.ThemeDark:
                    _platformThemeService.SetDarkTheme();
                    break;
                case SettingsConstants.Themes.ThemeLight:
                    _platformThemeService.SetLightTheme();
                    break;
                case SettingsConstants.Themes.ThemeSystem:
                    _platformThemeService.SetSystemDefaultTheme();
                    break;
                default:
                    _platformThemeService.SetSystemDefaultTheme();
                    themeName = SettingsConstants.Themes.ThemeSystem;
                    break;
            }

            return themeName;
        }
    }
}
