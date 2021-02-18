using ListApp.Enums;
using ListApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ListApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        private static string DatabaseFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "data.db");
        private readonly IAppPreferences _appPreferences;
        private readonly IDatabase _database;
        private readonly ILog _log;

        private SemaphoreSlim readSemaphore = new SemaphoreSlim(1, 1);

        public DatabaseService(IAppPreferences appPreferences,
            IDatabase database,
            ILog log)
        {
            _database = database ?? throw new InvalidOperationException("Database cannot be null");
            _appPreferences = appPreferences ?? throw new InvalidOperationException("AppPreferences cannot be null");
            _log = log ?? throw new InvalidOperationException("Log cannot be null");
        }

        public async Task InitAsync()
        {
            _database.CreateDatabase(DatabaseFileName);

            if (!_appPreferences.HasDatabaseBeenInitialized)
            {
                await InitializeDatabaseOnFirstTimeRunAsync();
            }
        }

        #region CRUD operations

        public async Task<T> FirstAsync<T>() where T : BaseRecordableRecord, new()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await readSemaphore.WaitAsync();
                    return _database.First<T>();
                }
                finally
                {
                    readSemaphore.Release();
                }
            });
        }

        public async Task<CrudResult> DeleteAsync<T>(T objectToDelete) where T : BaseRecordableRecord, new()
        {
            return await Task.Run(async () =>
            {
                try 
                { 
                    await readSemaphore.WaitAsync();
                    var rowDeleted = _database.Delete(objectToDelete);

                    return GetCrudResultFromDbResult(rowDeleted);
                }
                finally
                {
                    readSemaphore.Release();
                }
            });
        }

        public async Task<List<T>> GetAllAsync<T>() where T : BaseRecordableRecord, new()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await readSemaphore.WaitAsync();
                    return _database.GetAll<T>();
                }
                finally
                {
                    readSemaphore.Release();
                }
            });
        }

        public async Task<CrudResult> InsertAsync<T>(T objectToInsert) where T : BaseRecordableRecord, new()
        {
            return await Task.Run(async () =>
            {
                try 
                { 
                    await readSemaphore.WaitAsync();
                    UpdateModifiedFields(objectToInsert);
                    var result = _database.Insert(objectToInsert);
                    return GetCrudResultFromDbResult(result);
                }
                finally
                {
                    readSemaphore.Release();
                }
        });
        }

        public async Task<CrudResult> UpdateAsync<T>(T objectToUpdate) where T : BaseRecordableRecord, new()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await readSemaphore.WaitAsync();
                    UpdateModifiedFields(objectToUpdate);
                    var result = _database.Update(objectToUpdate);
                    return GetCrudResultFromDbResult(result);
                }
                finally
                {
                    readSemaphore.Release();
                }
            });
        }

        #endregion

        public async Task InitializeDatabaseOnFirstTimeRunAsync()
        {
            bool creationSuccessful = false;

            try
            {
                creationSuccessful = await CreateAllTables();
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                creationSuccessful = false;
            }
            finally
            {
                if (creationSuccessful)
                {
                    _appPreferences.HasDatabaseBeenInitialized = true;
                }
            }
        }

        private async Task<bool> CreateAllTables()
        {
            bool success;
            try
            {
                await readSemaphore.WaitAsync();
                await Task.Run(() =>
                {
                    _database.CreateTable<User>();
                    _database.CreateTable<Item>();
                    _database.CreateTable<ItemList>();
                });
                success = true;
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                success = false;
            }
            finally
            {
                readSemaphore.Release();
            }

            return success;
        }

        private CrudResult GetCrudResultFromDbResult(bool success)
        {
            return success ? CrudResult.Success : CrudResult.Failure;
        }

        private void UpdateModifiedFields<T>(T record) where T : BaseRecordableRecord
        {
            if (record.DateCreated is null)
            {
                record.DateCreated = DateTime.UtcNow;
            }
            else
            {
                record.DateModified = DateTime.UtcNow;
            }
        }
    }
}
