using ListApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListApp.Logic
{
    public interface IItemListLogic
    {
        Task<List<ItemList>> GetAllItemLists();
        Task<ItemList> GetItemList(int id);
        Task<bool> CreateItemList(ItemList itemList);
        Task<bool> UpdateItemList(ItemList itemList);
    }
}
