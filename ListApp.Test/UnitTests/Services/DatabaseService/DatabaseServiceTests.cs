using ListApp.Models;
using ListApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ListApp.Test.UnitTests.Services
{
    [TestClass]
    public class DatabaseServiceTests : BaseServiceTests
    {
        Mock<IAppPreferences> AppPreferencesMock;
        Mock<IDatabase> DatabaseMock;
        Mock<ILog> LogMock;

        DatabaseService databaseService;

        protected override void CreateMocks()
        {
            AppPreferencesMock = new Mock<IAppPreferences>();
            DatabaseMock = new Mock<IDatabase>();
            LogMock = new Mock<ILog>();
        }

        protected override void SetUpMocks()
        {
            AppPreferencesMock.SetupProperty(m => m.HasDatabaseBeenInitialized);

            databaseService = new DatabaseService
            (
                AppPreferencesMock.Object,
                DatabaseMock.Object,
                LogMock.Object
            );
        }

        [TestMethod]
        public async Task WhenRecordIsWrittenToDbForFirstTime_ThenDateCreatedIsSet()
        {
            var objectToInsert = new Item();
            var dateBeforeInsert = objectToInsert.DateCreated;
            await databaseService.InsertAsync(objectToInsert);

            Assert.IsNotNull(objectToInsert.DateCreated);
            Assert.IsNull(dateBeforeInsert);
        }

        [TestMethod]
        public async Task WhenRecordIsWrittenToDbAgain_ThenDateModifiedIsSet()
        {
            var objectToInsert = new Item();
            var dateBeforeInsert = objectToInsert.DateModified;
            await databaseService.InsertAsync(objectToInsert);
            await databaseService.UpdateAsync(objectToInsert);

            Assert.IsNotNull(objectToInsert.DateModified);
            Assert.IsNull(dateBeforeInsert);
        }

        [TestMethod]
        public async Task WhenInitAsyncIsCalled_AndIsFirstTimeInstall_DatabaseIsInitialized()
        {
            AppPreferencesMock.Object.HasDatabaseBeenInitialized = false;
            await databaseService.InitAsync();

            DatabaseMock.Verify(m => m.CreateDatabase(It.Is<string>(s => s.EndsWith("data.db"))));
            DatabaseMock.Verify(m => m.CreateTable<BaseRecordableRecord>(), Times.AtLeastOnce);
            Assert.IsTrue(AppPreferencesMock.Object.HasDatabaseBeenInitialized);
        }

        [TestMethod]
        public async Task WhenInitAsyncIsCalled_AndDatabaseExists_ThenDatabaseIsNotInitialized()
        {
            AppPreferencesMock.Object.HasDatabaseBeenInitialized = true; // simulate having been called already
            await databaseService.InitAsync();

            DatabaseMock.Verify(m => m.CreateDatabase(It.Is<string>(s => s.EndsWith("data.db"))));
            DatabaseMock.Verify(m => m.CreateTable<BaseRecordableRecord>(), Times.Never);
        }

        [TestMethod]
        public async Task WhenDeleteAsyncIsCalled_ThenDatabaseDeleteIsCalled()
        {
            await databaseService.InitAsync();

            var objectToDelete = new Item();
            await databaseService.DeleteAsync<Item>(objectToDelete);

            DatabaseMock.Verify(m => m.Delete<Item>(objectToDelete), Times.Once);
        }

        [TestMethod]
        public async Task WhenFirstAsyncIsCalled_ThenDatabaseFirstIsCalled()
        {
            await databaseService.InitAsync();

            await databaseService.FirstAsync<Item>();
            DatabaseMock.Verify(m => m.First<Item>(), Times.Once);
        }
    }
}
