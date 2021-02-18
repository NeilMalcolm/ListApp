namespace ListApp.Services
{
    public interface IPreferenceService
    {
        bool Get(string preferenceName, bool defaultValue = false);
        string Get(string preferenceName, string defaultValue = null);
        int Get(string preferenceName, int defaultValue = -1);

        void Set(string preferenceName, bool value);
        void Set(string preferenceName, string value);
        void Set(string preferenceName, int value);
    }
}
