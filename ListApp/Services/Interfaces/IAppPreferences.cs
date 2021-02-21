namespace ListApp.Services
{
    public interface IAppPreferences
    {
        bool HasDatabaseBeenInitialized { get; set; }
        string UserPreferredAppTheme { get; set; }
    }
}
