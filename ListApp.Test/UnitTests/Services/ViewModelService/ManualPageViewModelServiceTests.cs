using ListApp.Services;
using ListApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListApp.Test.UnitTests.Services
{
    [TestClass]
    public class ManualPageViewModelServiceTests : BasePageViewModelServiceTests
    {
        private TestPage testPage;
        private TestViewModel testViewModel;
        private Mock<INavigationService> NavigationServiceMock;
        private Mock<IDependencyService> DependencyServiceMock;

        protected override Dictionary<Type, Type> PageViewModelDictionary =>
            new Dictionary<Type, Type>
            {
                { typeof(TestPage), typeof(TestViewModel) }
            };

        protected override void CreateMocks()
        {
            base.CreateMocks();

            NavigationServiceMock = new Mock<INavigationService>();
            DependencyServiceMock = new Mock<IDependencyService>();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            DependencyServiceMock.Setup(m => m.Get(typeof(TestViewModel)))
                .Returns(() => testViewModel);

            testPage = new TestPage();
            testViewModel = new TestViewModel(NavigationServiceMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void WhenGetViewModelForPageIsCalled_AndCacheIsNull_ThenInvalidOperationExceptionIsThrown()
        {
            PageViewModelService.GetViewModelForPage<Page>();
        }

        [TestMethod]
        public override void WhenGetViewModelForPageIsCalled_AndPageAndViewModelAreRegistered_ThenViewModelIsReturned()
        {
            RegisterPageViewModelDictionary();            
            var viewModel = PageViewModelService.GetViewModelForPage<TestPage>();
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void WhenGetViewModelForPageIsCalled_AndPageIsNotInCache_ThenInvalidOperationExceptionIsThrown()
        {
            RegisterPageViewModelDictionary();
            var viewModel = PageViewModelService.GetViewModelForPage<UnregisteredPage>();
            Assert.IsNotNull(viewModel);
        }

        protected override IPageViewModelService CreatePageViewModelService()
        {
            return new ManualPageViewModelService(DependencyServiceMock.Object);
        }

        public class TestPage : Page
        {

        }

        public class UnregisteredPage : Page
        {

        }

        public class TestViewModel : BaseViewModel
        {
            public TestViewModel(INavigationService navigationService) : base(navigationService)
            {
            }

            public override Task LoadData()
            {
                // required. Do nothing.
                return Task.CompletedTask;
            }
        }

    }
}
