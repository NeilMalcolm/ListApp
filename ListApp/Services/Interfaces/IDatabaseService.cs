using ListApp.Enums;
using ListApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListApp.Services
{
    public interface IDatabaseService
    {
        Task InitAsync();
        Task<T> FirstAsync<T>() where T : BaseRecordableRecord, new();
        Task<List<T>> GetAllAsync<T>() where T : BaseRecordableRecord, new();
        Task<CrudResult> InsertAsync<T>(T objectToInsert) where T : BaseRecordableRecord, new();
        Task<CrudResult> DeleteAsync<T>(T objectToDelete) where T : BaseRecordableRecord, new();
        Task<CrudResult> UpdateAsync<T>(T objectToUpdate) where T : BaseRecordableRecord, new();
        Task InitializeDatabaseOnFirstTimeRunAsync();
    }
}
