using Xamarin.Essentials;

namespace ListApp.Services
{
    public class PreferenceService : IPreferenceService
    {
        public bool Get(string preferenceName, bool defaultValue = false)
        {
            return Preferences.Get(preferenceName, defaultValue);
        }

        public string Get(string preferenceName, string defaultValue = null)
        {
            return Preferences.Get(preferenceName, defaultValue);
        }

        public int Get(string preferenceName, int defaultValue = -1)
        {
            return Preferences.Get(preferenceName, defaultValue);
        }

        public void Set(string preferenceName, bool value)
        {
            Preferences.Set(preferenceName, value);
        }

        public void Set(string preferenceName, string value)
        {
            Preferences.Set(preferenceName, value);
        }

        public void Set(string preferenceName, int value)
        {
            Preferences.Set(preferenceName, value);
        }
    }
}
