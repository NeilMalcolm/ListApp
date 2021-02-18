using ListApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListApp.Helpers.PageHelpers
{
    public static class PageExtensions
    {
        public static async Task LoadDataOnAppearing(this Page page)
        {
            if (page.BindingContext is BaseViewModel baseViewModel)
            {
                await baseViewModel.BeginLoadData();
            }
        }
    }
}
