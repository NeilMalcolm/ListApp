using ListApp.Services;
using System.Threading.Tasks;

namespace ListApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IAppPreferences _appPreferences;

        public SettingsViewModel(INavigationService navigationService,
            IAppPreferences appPreferences) 
            : base(navigationService)
        {
            _appPreferences = appPreferences;
        }

        public override async Task LoadData()
        {
            // Load each setting and add to Collection View.
        }
    }
}
