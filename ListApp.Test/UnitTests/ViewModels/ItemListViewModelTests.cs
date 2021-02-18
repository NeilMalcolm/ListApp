using ListApp.Logic;
using ListApp.Models;
using ListApp.Pages;
using ListApp.Services;
using ListApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ListApp.Test.UnitTests.ViewModels
{
    [TestClass]
    public class ItemListViewModelTests : BaseViewModelTests<ItemListViewModel>
    {
        protected ItemListLogic ItemListLogic;
        protected DatabaseService DatabaseService;

        public override void CreateMocks()
        {
            base.CreateMocks();
        }

        public override void SetUpMocks()
        {
            base.SetUpMocks();

            DatabaseService = new DatabaseService
            (
                AppPreferencesMock.Object,
                DatabaseMock.Object,
                LogMock.Object
            );

            ItemListLogic = new ItemListLogic(DatabaseService, LogMock.Object);
        }

        protected override ItemListViewModel SetUpViewModel()
        {
            return new ItemListViewModel
            (
                NavigationServiceMock.Object,
                ItemListLogic
            );
        }

        [TestMethod]
        public void WhenInitialized_ThenCommandsAreSetup()
        {
            Assert.IsNotNull(ViewModel.GoToAddItemCommand);
            Assert.IsNotNull(ViewModel.GoToListCommand);
        }

        [TestMethod]
        public void WhenInitialized_ThenTitleIsSet()
        {
            Assert.IsNotNull(ViewModel.Title = "Lists");
        }

        [TestMethod]
        public void WhenGoToAddItemCommandIsExecuted_ThenNavServicePushModalIsCalled()
        {
            ViewModel.GoToAddItemCommand?.Execute(null);

            NavigationServiceMock.Verify
            (
                m => m.PushModalAsync<ItemPage>(),
                Times.Once
            );
        }

        [TestMethod]
        public void WhenGoToListCommandIsExecuted_ThenNavServicePushModalIsCalledWithObject()
        {
            var itemToGoTo = new ItemList()
            {
                Title = "e.g."
            };

            ViewModel.GoToListCommand?.Execute(itemToGoTo);

            NavigationServiceMock.Verify
            (
                m => m.PushModalAsync<ItemPage>(itemToGoTo),
                Times.Once
            );
        }
    }
}
