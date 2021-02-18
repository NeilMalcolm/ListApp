using ListApp.Models;
using System.Collections.Generic;

namespace ListApp.Services
{
    public interface IDatabase
    {
        void CreateDatabase(string fileName);
        void CreateTable<T>() where T : BaseRecordableRecord;
        T First<T>() where T : BaseRecordableRecord;
        bool Delete<T>(T objectToDelete) where T : BaseRecordableRecord;
        List<T> GetAll<T>() where T : BaseRecordableRecord;
        bool Insert<T>(T objectToInsert) where T : BaseRecordableRecord;
        bool Update<T>(T objectToUpdate) where T : BaseRecordableRecord;
    }
}
