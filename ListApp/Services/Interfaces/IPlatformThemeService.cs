namespace ListApp.Services
{
    public interface IPlatformThemeService
    {
        void SetDarkTheme();
        void SetLightTheme();
        void SetSystemDefaultTheme();
        void SetCustomTheme(string themeName);
    }
}
