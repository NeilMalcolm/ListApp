using ListApp.Enums;
using ListApp.Models;
using ListApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListApp.Logic
{
    public class ItemListLogic : IItemListLogic
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILog _log;

        public ItemListLogic(IDatabaseService databaseService,
            ILog log)
        {
            _databaseService = databaseService;
            _log = log;
        }

        public async Task<bool> CreateItemList(ItemList itemList)
        {
            var result = await _databaseService.InsertAsync(itemList);
            if (result == CrudResult.Failure)
            {
                _log.Error($"Failed to create item list {itemList.Id}");
                return false;
            }

            return true;
        }

        public async Task<List<ItemList>> GetAllItemLists()
        {
            return await _databaseService.GetAllAsync<ItemList>();
        }

        public Task<ItemList> GetItemList(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemList(ItemList itemList)
        {
            var result = await _databaseService.UpdateAsync(itemList);
            if (result == CrudResult.Failure)
            {
                _log.Error($"Failed to update item list {itemList.Id}");
                return false;
            }

            return true;
        }
    }
}
