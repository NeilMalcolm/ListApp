using Xamarin.Forms;
using ListApp.Helpers.PageHelpers;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;
using ListApp.ViewModels;

namespace ListApp.Pages
{
    public class BasePage : ContentPage
    {
        public BaseViewModel ViewModel { get; private set; }

        public BasePage()
        {
            On<iOS>().SetUseSafeArea(true);
            Shell.SetNavBarHasShadow(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.LoadDataOnAppearing();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is BaseViewModel viewModel)
            {
                ViewModel = viewModel;
                Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
                {
                    Command = ViewModel.BackCommand
                });
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel?.BackCommand != null)
            {
                ViewModel.BackCommand.Execute(null);
            }
            return true;
        }
    }
}
