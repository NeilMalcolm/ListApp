using System;
using Xamarin.Forms;

namespace ListApp.Services
{
    public class XamarinFormsThemeService : IPlatformThemeService
    {
        public void SetCustomTheme(string themeName)
        {
            throw new NotImplementedException();
        }

        public void SetDarkTheme()
        {
            Application.Current.UserAppTheme = OSAppTheme.Dark;
        }

        public void SetLightTheme()
        {
            Application.Current.UserAppTheme = OSAppTheme.Light;
        }

        public void SetSystemDefaultTheme()
        {
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
        }
    }
}
