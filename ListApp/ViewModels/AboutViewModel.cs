using ListApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListApp.ViewModels
{
    public class AboutViewModel : BaseModalViewModel
    {
        public string AboutUrl { get => "https://neilmalcolm.dev"; }

        public ICommand OpenAboutUrlCommand { get; set; }

        public AboutViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "About";
        }

        public override Task LoadData()
        {
            OpenAboutUrlCommand = new Command
            (
                async () => await OpenAboutUrl()
            );

            return Task.CompletedTask;
        }

        public async Task OpenAboutUrl()
        {
            await Xamarin.Essentials.Browser.OpenAsync(AboutUrl);
        }
    }
}
