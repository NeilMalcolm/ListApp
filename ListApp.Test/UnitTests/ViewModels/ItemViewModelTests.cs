using ListApp.Models;
using ListApp.Services;
using ListApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListApp.Test.UnitTests.ViewModels
{
    [TestClass]
    public class ItemViewModelTests : BaseViewModelTests<ItemViewModel>
    {
        Mock<INavigationService> navigationServiceMock;
        DatabaseService databaseService;

        public ItemList BlankItemList = new ItemList();

        public override void CreateMocks()
        {
            base.CreateMocks();

            navigationServiceMock = new Mock<INavigationService>();
            databaseService = new DatabaseService
            (
                AppPreferencesMock.Object,
                DatabaseMock.Object,
                LogMock.Object
            );
        }

        public override void SetUpMocks()
        {
            base.SetUpMocks();
        }

        protected override ItemViewModel SetUpViewModel()
        {
            return new ItemViewModel
            (
                navigationServiceMock.Object,
                databaseService
            );
        }

        [TestMethod]
        public async Task WhenCurrentItemListIsNullOnLoadData_ThenCurrentItemListIsInitialized()
        {
            var startingItemListValue = ViewModel.CurrentItemList;
            await ViewModel.BeginLoadData();

            Assert.IsNull(startingItemListValue);
            Assert.IsNotNull(ViewModel.CurrentItemList);
        }

        [TestMethod]
        public void WhenInitIsCalled_AndParameterIsANonNullObjectOfWrongType_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<InvalidOperationException>
            (
                () => ViewModel.Init("This is an object which should throw an exception")
            );
        }

        [TestMethod]
        public void WhenInitIsCalled_AndParameterIsNull_ThenNoExceptionIsThrown()
        {
            ViewModel.Init(null);
        }

        [TestMethod]

        public void WhenInitIsCalled_AndParameterIsCorrectType_ThenNoExceptionIsThrown()
        {
            ViewModel.Init(BlankItemList);
        }

        [TestMethod]
        public async Task WhenCurrentItemListIsMissingData_ThenRunValidationReturnsFalse()
        {
            await ViewModel.LoadData();
            var isValid = ViewModel.RunValidation();

            Assert.IsFalse(isValid);
        }

        [DataTestMethod]
        [DataRow(null, "2021-01-01", false)]
        [DataRow("Example", null, false)]
        [DataRow(null, null, true)]
        public void WhenCurrentItemListDataIsValid_ThenRunValidationReturnsTrue(string title, string dateCreatedString, bool hasItems)
        {
            DateTime? date = null;

            if (!string.IsNullOrWhiteSpace(dateCreatedString)) { date = DateTime.Parse(dateCreatedString); }

            var itemList = new ItemList
            {
                DateCreated = date,
                Title = title
            };

            if (hasItems)
            {
                itemList.Items = new List<Item> 
                { 
                    new Item { Text = "Example" } 
                };
            }

            ViewModel.CurrentItemList = itemList;
            var isValid = ViewModel.RunValidation();

            Assert.IsTrue(isValid);
        }
    }
}
