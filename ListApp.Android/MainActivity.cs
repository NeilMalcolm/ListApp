using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using ListApp.Services;
using System.Diagnostics;
using XFColor = Xamarin.Forms.Color;
using Xamarin.Forms.Platform.Android;

namespace ListApp.Droid
{
    [Activity(Label = "ListApp", 
        Icon = "@mipmap/icon",
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation 
        | ConfigChanges.UiMode | ConfigChanges.ScreenLayout 
        | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            SetAppThemeForSystemTheme();

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("Brush_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            IDependencyService dependencyService = Xamarin.Forms.DependencyService.Get<IDependencyService>();
            var sw = new Stopwatch();
            sw.Start();
            LoadApplication(new App(dependencyService, sw));

            Xamarin.Forms.Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        private void Current_RequestedThemeChanged(object sender, Xamarin.Forms.AppThemeChangedEventArgs e)
        {
            SetAppThemeForSystemTheme();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SetAppThemeForSystemTheme()
        {
            XFColor color;
            if (Xamarin.Essentials.AppInfo.RequestedTheme == Xamarin.Essentials.AppTheme.Dark)
            {
                color = XFColor.Black;
            }
            else
            {
                color = XFColor.Gray;
            }

            SetStatusBarColor(color.ToAndroid());
        }
    }
}