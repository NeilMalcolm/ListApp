using ListApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;

namespace ListApp.Services
{
    public class LiteDbDatabase : IDatabase
    {
        private ILiteDatabase Connection { get; set; }

        public void CreateDatabase(string fileName)
        {
            Connection = new LiteDatabase(fileName);

            BsonMapper.Global.Entity<ItemList>()
                .Ignore(x => x.DisplayTitle);
        }

        public void CreateTable<T>() where T : BaseRecordableRecord
        {
            Connection.GetCollection<User>();
        }

        public bool Delete<T>(T objectToDelete) where T : BaseRecordableRecord
        {
            var result = Connection.GetCollection<T>()
                       .Delete(objectToDelete.Id);
            return result;
        }

        public T First<T>() where T : BaseRecordableRecord
        {
            return Connection.GetCollection<T>()
                   .Query()
                   .FirstOrDefault();
        }

        public List<T> GetAll<T>() where T : BaseRecordableRecord
        {
            return Connection.GetCollection<T>()
                    .Query()
                    .ToList();
        }

        public bool Insert<T>(T objectToInsert) where T : BaseRecordableRecord
        {
            var result = Connection.GetCollection<T>()
                        .Insert(objectToInsert);

            return result != null;
        }

        public bool Update<T>(T objectToUpdate) where T : BaseRecordableRecord
        {
            var result = Connection.GetCollection<T>()
                        .Update(objectToUpdate);

            return result;
        }
    }
}
