namespace ListApp.Services
{
    public interface IThemeService
    {
        void SetInitialTheme();
        void ChangeTheme(string themeName);
        string GetCurrentlySelectedTheme();
    }
}
