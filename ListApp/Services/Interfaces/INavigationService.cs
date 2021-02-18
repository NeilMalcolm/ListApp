using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListApp.Services
{
    public interface INavigationService
    {
        void SetHomePage<T>() where T : Page;
        Task PushPageAsync<T>() where T : Page;
        Task PushModalAsync<T>() where T : Page;
        Task PushPageAsync<T>(object parameter) where T : Page;
        Task PushModalAsync<T>(object parameter) where T : Page;
        Task PopPageAsync();
        Task PopModalAsync();
    }
}
