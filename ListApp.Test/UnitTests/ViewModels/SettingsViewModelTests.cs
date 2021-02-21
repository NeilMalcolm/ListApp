using ListApp.Constants;
using ListApp.Models.Settings;
using ListApp.Services;
using ListApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ListApp.Test.UnitTests.ViewModels
{
    [TestClass]
    public class SettingsViewModelTests : BaseViewModelTests<SettingsViewModel>
    {
        protected Mock<IThemeService> ThemeServiceMock;

        public override void CreateMocks()
        {
            base.CreateMocks();
            ThemeServiceMock = new Mock<IThemeService>();
        }

        public override void SetUpMocks()
        {
            base.SetUpMocks();
        }

        protected override SettingsViewModel SetUpViewModel()
        {
            return new SettingsViewModel
            (
                NavigationServiceMock.Object,
                ThemeServiceMock.Object
            );
        }

        [TestInitialize]
        public void BeforeEachTest()
        {

        }

        [TestCleanup]
        public void AfterEachTest()
        {

        }

        [TestMethod]
        public async Task WhenBeginLoadDataIsCalled_ThenAllSettingsGroupIsPopulated()
        {
            await ViewModel.BeginLoadData();

            Assert.IsTrue(ViewModel.AllSettingGroups.Count > 0);
        }

        [TestMethod]
        public void WhenBuildSystemThemeSettingIsCalled_ThenRadioSettingIsReturned()
        {
            var result = ViewModel.GetSystemThemeSetting();

            var radioSetting = (RadioSetting)result;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RadioSetting));
            Assert.IsTrue(radioSetting.Options.Length > 0);
            Assert.AreEqual(radioSetting.Title, SettingsConstants.Settings.AppTheme);
            Assert.AreEqual(ViewModel.SystemThemeCommand, radioSetting.OnSelectionChangedCommand);
        }

        [TestMethod,
            DataRow(SettingsConstants.Themes.ThemeDark),
            DataRow(SettingsConstants.Themes.ThemeSystem),
            DataRow(SettingsConstants.Themes.ThemeDark)
        ]
        public async Task WhenRadioSettingIsChanged_ThenOnSelectionChangedCommandIsCalled(string selectedTheme)
        {
            // Act
            await ViewModel.BeginLoadData();
            var radioSetting = ViewModel.GetSystemThemeSetting() as RadioSetting;
            radioSetting.SelectedOption = selectedTheme;

            // Assert
            ThemeServiceMock.Verify(m => m.ChangeTheme(selectedTheme), Times.Once);
        }

        [TestMethod,
            DataRow(""),
            DataRow(null)
        ]
        public async Task WhenRadioSettingIsChangedToNullOrWhitespace_ThenOnSelectionChangedCommandIsNotCalled(string selectedTheme)
        {
            // Act
            await ViewModel.BeginLoadData();
            var radioSetting = ViewModel.GetSystemThemeSetting() as RadioSetting;
            radioSetting.SelectedOption = selectedTheme;

            // Assert
            ThemeServiceMock.Verify(m => m.ChangeTheme(selectedTheme), Times.Never);
        }
    }
}
