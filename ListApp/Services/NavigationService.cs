using ListApp.Pages;
using ListApp.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinNav = Xamarin.Forms.INavigation;

namespace ListApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ILog _log;
        private readonly IPageViewModelService _pageViewModelService;

        XamarinNav XamarinNavigation => App.Current.MainPage.Navigation;

        public NavigationService(IPageViewModelService pageViewModelService,
            ILog log)
        {
            _pageViewModelService = pageViewModelService;
            _log = log;
        }

        public async Task PopModalAsync()
        {
            await XamarinNavigation.PopModalAsync();
        }

        public async Task PopPageAsync()
        {
            await XamarinNavigation.PopAsync();
        }

        public async Task PushModalAsync<T>() where T : Page
        {
            await XamarinNavigation.PushModalAsync(GetInstanceOfPageWithViewModel<T>(null));
        }

        public async Task PushPageAsync<T>() where T : Page
        {
            await XamarinNavigation.PushAsync(GetInstanceOfPageWithViewModel<T>(null));
        }

        public async Task PushPageAsync<T>(object parameter) where T : Page
        {
            await XamarinNavigation.PushAsync(GetInstanceOfPageWithViewModel<T>(parameter));
        }

        public async Task PushModalAsync<T>(object parameter) where T : Page
        {
            await XamarinNavigation.PushModalAsync(GetInstanceOfPageWithViewModel<T>(parameter));
        }

        public void SetHomePage<T>() where T : Page
        {
            App.Current.MainPage = GetInstanceOfPageWithViewModel<T>(null);

        }

        #region Private Methods

        Page GetInstanceOfPageWithViewModel<T>(object parameter) where T : Page
        {
            var pageInstance = Activator.CreateInstance<T>();
            var viewModel = GetViewModelForPage<T>();
            pageInstance.BindingContext = viewModel;

            if (parameter != null)
            {
                viewModel.Init(parameter);
            }

            viewModel.LoadData();
            return pageInstance;
        }

        BaseViewModel GetViewModelForPage<T>() where T : Page
        {
            return _pageViewModelService.GetViewModelForPage<T>();
        }

        #endregion
    }
}
