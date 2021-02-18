using ListApp.Pages;
using ListApp.Services;
using System.Threading.Tasks;

namespace ListApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        readonly IPageViewModelService _pageViewModelService;

        BaseViewModel listViewModel;
        public BaseViewModel ListViewModel
        {
            get => listViewModel;
            set
            {
                if (value != listViewModel)
                {
                    listViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public AppShellViewModel
        (
            IPageViewModelService pageViewModelService,
            INavigationService navigationService
        ) : base(navigationService)
        {
            _pageViewModelService = pageViewModelService;
        }

        public override Task LoadData()
        {
            ListViewModel = _pageViewModelService.GetViewModelForPage<ItemListPage>();

            return Task.CompletedTask;
        }
    }
}
