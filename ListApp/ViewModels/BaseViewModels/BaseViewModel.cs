using ListApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IValidatable
    {
        protected bool popInProgress = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackCommand { get; set; }

        public int LoadDataCalls { get; private set; } = 0;

        protected INavigationService NavigationService;

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        private string title;

        public string Title
        {
            get => title;
            set 
            { 
                title = value;
                OnPropertyChanged();
            }
        }

        public bool AlwaysReloadOnSubsequentAppearing { get; protected set; }
        public bool HasLoadedData { get; protected set; }

        public async Task BeginLoadData()
        {
            if (HasLoadedData && !AlwaysReloadOnSubsequentAppearing)
            {
                return;
            }

            await LoadData();
            HasLoadedData = true;
            LoadDataCalls++;
        }

        public abstract Task LoadData();

        public virtual void Init(object parameter)
        {

        }

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            BackCommand = new Command(async () => await OnPopAsync(), () => !popInProgress);  
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsValid()
        {
            return RunValidation();
        }

        public async Task<bool> IsValidAsync()
        {
            return await RunValidationAsync();
        }

        public virtual bool RunValidation()
        {
            return true;
        }

        public virtual Task<bool> RunValidationAsync()
        {
            return Task.FromResult(true);
        }

        protected async Task OnPopAsync()
        {
            popInProgress = true;

            try
            {
                if (await BeforePopAsync())
                {
                    await PopAsync();
                }
            }
            finally
            {
                popInProgress = false;
            }
        }

        protected virtual async Task PopAsync()
        {
            await NavigationService.PopPageAsync();
        }

        protected virtual Task<bool> BeforePopAsync()
        {
            return Task.FromResult(true);
        }
    }
}
