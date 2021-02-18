using ListApp.Models;
using ListApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListApp.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private bool modalPopInProgress = false;
        private bool addInProgress = false;

        private readonly IDatabaseService _databaseService;

        private ItemList currentItemList;
        public ItemList CurrentItemList
        {
            get => currentItemList;
            set
            {
                if (value != currentItemList)
                {
                    currentItemList = value;
                    OnCurrentItemListChanged();
                }
            }
        }

        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand PopModalCommand { get; set;}

        public ItemViewModel(INavigationService navigationService,
            IDatabaseService databaseService)
            : base (navigationService)
        {
            _databaseService = databaseService;

            AddItemCommand = new Command(async () => await AddNewItemToItemList(), () => !addInProgress);
            PopModalCommand = new Command(async () => await PopModal(),  () => !modalPopInProgress);
        }

        public override void Init(object parameter)
        {
            base.Init(parameter);
            HandleParameters(parameter);
        }

        private void HandleParameters(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            if (parameter is ItemList itemList)
            {
                CurrentItemList = itemList;
            }
            else
            {
                throw new InvalidOperationException($"BaseViewModel must be of type {typeof(ItemList).Name}");
            }
        }

        public override Task LoadData()
        {
            if (CurrentItemList is null)
            {
                CurrentItemList = new ItemList()
                {
                    Items = new List<Item>()
                };
            }

            return Task.CompletedTask;
        }

        public override bool RunValidation()
        {
            return CurrentItemList != null 
                && (CurrentItemList.DateCreated !=  null
                || !string.IsNullOrWhiteSpace(CurrentItemList.Title)
                || CurrentItemList.Items?.Count > 0);
        }

        private void OnCurrentItemListChanged()
        {
            SetItemsFromItemList();
        }

        private void SetItemsFromItemList()
        {
            Items = new ObservableCollection<Item>(CurrentItemList.Items ??= new List<Item>());
        }

        private async Task AddNewItemToItemList()
        {
            addInProgress = true;
            try
            {
                var newItem = new Item();
                Items.Add(newItem);
                await UpdateList();
            }
            finally
            {
                addInProgress = false;
            }
        }

        public async Task PopModal()
        {
            modalPopInProgress = true;

            try
            {
                // ensure we only save if the 
                // title has been set (and previously was not)
                // or we have items in the list
                if (RunValidation())
                {
                    await UpdateList();
                }
                await NavigationService.PopModalAsync();
            }
            finally
            {
                modalPopInProgress = false;
            }
        }

        private async Task UpdateList()
        {
            CurrentItemList.Items = Items.ToList();
            if (CurrentItemList.DateCreated == null)
            {
                CurrentItemList.DateCreated = DateTime.UtcNow;
                await _databaseService.InsertAsync(CurrentItemList);
            }
            else
            {
                await _databaseService.UpdateAsync(CurrentItemList);
            }
        }
    }
}
