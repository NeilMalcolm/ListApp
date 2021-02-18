using ListApp.Services;
using ListApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ListApp.Test.UnitTests.ViewModels
{
    public abstract class BaseViewModelTests<T> where T : BaseViewModel
    {
        protected Mock<IAppPreferences> AppPreferencesMock;
        protected Mock<IDatabase> DatabaseMock;
        protected Mock<INavigationService> NavigationServiceMock;
        protected Mock<ILog> LogMock;

        protected T ViewModel { get; set; }

        [TestInitialize]
        public virtual void SetUp()
        {
            CreateMocks();
            SetUpMocks();
            ViewModel = SetUpViewModel();
        }

        public virtual void SetUpMocks()
        {
        }
        public virtual void CreateMocks()
        {
            LogMock = new Mock<ILog>();
            AppPreferencesMock = new Mock<IAppPreferences>();
            DatabaseMock = new Mock<IDatabase>();
            NavigationServiceMock = new Mock<INavigationService>();
        }

        protected abstract T SetUpViewModel();


        [TestMethod]
        public async Task WhenBeginLoadDataIsCalled_ThenHasLoadedDataIsTrue()
        {
            await ViewModel.BeginLoadData();

            Assert.IsTrue(ViewModel.HasLoadedData);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task WhenBeginLoadIsCalledMultipleTimes_AndAlwaysReloadOnSubsequentAppearing_ThenGetAllItemListsIsCalledThatSameNumberOfTimes(int times)
        {
            if (!ViewModel.AlwaysReloadOnSubsequentAppearing)
            {
                return;
            }

            for (int i = 0; i < times; i++)
            {
                await ViewModel.BeginLoadData();
            }

            Assert.AreEqual(times, ViewModel.LoadDataCalls);
        }
    }
}
