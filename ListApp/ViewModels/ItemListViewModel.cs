using ListApp.Logic;
using ListApp.Models;
using ListApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ListApp.Pages;
using Xamarin.Forms;

namespace ListApp.ViewModels
{
    public class ItemListViewModel : BaseViewModel
    {
        readonly IItemListLogic _itemListLogic;

        private bool isNavigating = false;

        private List<ItemList> allItemLists;

        public List<ItemList> AllItemLists
        {
            get => allItemLists; 
            set 
            { 
                allItemLists = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoToAddItemCommand { get; set; }
        public ICommand GoToListCommand { get; set; }

        public ItemListViewModel(INavigationService navigationService, 
            IItemListLogic itemListLogic) : base (navigationService)
        {
            _itemListLogic = itemListLogic;
            SetUpCommands();

            AlwaysReloadOnSubsequentAppearing = true;
            Title = "Lists";
        }

        private void SetUpCommands()
        {
            GoToAddItemCommand = new Command
            (
                async () => await GoTo<ItemPage>(),
                () => !isNavigating
            );

            GoToListCommand = new Command<ItemList>
            (
                async (item) => await GoTo<ItemPage>(item),
                (item) => !isNavigating
            );
        }

        public override async Task LoadData()
        {
            try
            {
                IsLoading = true;
                AllItemLists = await _itemListLogic.GetAllItemLists();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task GoTo<T>(object param = null) where T : Page
        {
            try
            {
                if (isNavigating == true)
                {
                    return;
                }

                isNavigating = true;
                await NavigationService.PushPageAsync<T>(param);
            }
            finally
            {
                // to prevent button being tapped right as the modal animation
                // finishes. More of a possibility on android.
                await Task.Delay(100);
                isNavigating = false;
            }
        }
    }
}
