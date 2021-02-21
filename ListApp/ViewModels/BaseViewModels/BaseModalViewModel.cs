using ListApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListApp.ViewModels
{
    public abstract class BaseModalViewModel : BaseViewModel
    {
        protected bool modalPopInProgress = false;

        public ICommand PopModalCommand { get; set; }

        public bool IsTitleReadOnly { get; set; } = true;

        protected BaseModalViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            PopModalCommand = new Command(async () => await PopModal(), () => !modalPopInProgress);
        }

        public async Task PopModal()
        {
            modalPopInProgress = true;

            try
            {
                await OnPopModal();
                await NavigationService.PopModalAsync();
            }
            finally
            {
                modalPopInProgress = false;
            }
        }

        protected virtual Task OnPopModal()
        {
            return Task.CompletedTask;
        }
    }
}
