using ListApp.Services;
using System.Threading.Tasks;

namespace ListApp.ViewModels
{
    public abstract class BaseModalViewModel : BaseViewModel
    {
        protected BaseModalViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
        }

        protected override async Task PopAsync()
        {
            await NavigationService.PopModalAsync();
        }
    }
}
