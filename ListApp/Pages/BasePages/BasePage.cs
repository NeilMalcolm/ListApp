using Xamarin.Forms;
using ListApp.Helpers.PageHelpers;

namespace ListApp.Pages
{
    public class BasePage : ContentPage
    {
        protected override async void OnAppearing()
        {
            Shell.SetNavBarHasShadow(this, false);
            base.OnAppearing();
            await this.LoadDataOnAppearing();
        }
    }
}
