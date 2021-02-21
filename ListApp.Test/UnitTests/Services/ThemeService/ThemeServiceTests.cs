using ListApp.Constants;
using ListApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ListApp.Test.UnitTests.Services
{
    [TestClass]
    public class ThemeServiceTests : BaseServiceTests
    {
        protected Mock<IPlatformThemeService> PlatformThemeServiceMock;
        protected Mock<IAppPreferences> AppPreferencesMock;
        protected ThemeService ThemeService;

        protected override void CreateMocks()
        {
            base.CreateMocks();
            AppPreferencesMock = new Mock<IAppPreferences>();
            PlatformThemeServiceMock = new Mock<IPlatformThemeService>();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            AppPreferencesMock.SetupProperty(m => m.UserPreferredAppTheme);
            
            ThemeService = new ThemeService
            (
                AppPreferencesMock.Object,
                PlatformThemeServiceMock.Object
            );
        }

        [TestMethod]
        public void WhenSetInitialThemeIsCalled_ThenUserPreferredAppThemeGetterIsCalled()
        {
            ThemeService.SetInitialTheme();

            AppPreferencesMock.Verify(m => m.UserPreferredAppTheme, Times.Once);
        }

        [TestMethod]
        public void WhenGetCurrentlySelectedThemeIsCalled_ThenUserPreferredAppThemeGetterIsCalled()
        {
            ThemeService.GetCurrentlySelectedTheme();

            AppPreferencesMock.Verify(m => m.UserPreferredAppTheme, Times.Once);
        }

        [TestMethod,
            DataRow(SettingsConstants.Themes.ThemeSystem),
            DataRow(SettingsConstants.Themes.ThemeDark),
            DataRow(SettingsConstants.Themes.ThemeLight)
        ]
        public void WhenChangeThemeIsCalled_ThenUserPreferredAppThemeIsSetToValue(string themeName)
        {
            ThemeService.ChangeTheme(themeName);

            Assert.AreEqual(themeName, AppPreferencesMock.Object.UserPreferredAppTheme);
        }

        [TestMethod,
            DataRow(""),
            DataRow(null),
            DataRow("example incorrect string")
        ]
        public void WhenChangeThemeIsCalledWithInvalidString_ThenUserPreferredAppThemeIsSetToThemeSystem(string themeName)
        {
            ThemeService.ChangeTheme(themeName);

            Assert.AreEqual
            (
                SettingsConstants.Themes.ThemeSystem,
                AppPreferencesMock.Object.UserPreferredAppTheme
            );
        }
    }
}
