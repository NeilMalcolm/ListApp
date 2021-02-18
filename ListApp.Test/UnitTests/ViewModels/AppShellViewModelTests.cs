using ListApp.Services;
using ListApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListApp.Test.UnitTests.ViewModels
{
    [TestClass]
    public class AppShellViewModelTests : BaseViewModelTests<AppShellViewModel>
    {
        protected Mock<IPageViewModelService> PageViewModelService;

        public override void CreateMocks()
        {
            base.CreateMocks();
            PageViewModelService = new Mock<IPageViewModelService>();
        }

        public override void SetUpMocks()
        {
            base.SetUpMocks();

            PageViewModelService.Setup(m => m.GetViewModelForPage<Page>())
                .Returns(() => new ExampleViewModel(NavigationServiceMock.Object));
        }

        protected override AppShellViewModel SetUpViewModel()
        {
            return new AppShellViewModel    
            (
                PageViewModelService.Object, 
                NavigationServiceMock.Object
            );
        }

        [TestMethod]
        public async Task WhenBeginLoadDataIsCalled_ThenListViewModelIsSet()
        {
            await ViewModel.BeginLoadData();

            Assert.IsNotNull(ViewModel.ListViewModel);
        }


        public class ExampleViewModel : BaseViewModel
        {
            public ExampleViewModel(INavigationService navigationService) 
                : base(navigationService)
            {
            }

            public override Task LoadData()
            {
                return Task.CompletedTask;
            }
        }
    }
}
