using ListApp.Logic;
using ListApp.Pages;
using ListApp.Services;
using ListApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListApp
{
    public partial class App : Application
    {
        readonly IDependencyService _dependencyService;
        readonly IDatabaseService _databaseService;
        readonly INavigationService _navigationService;

        public App(IDependencyService dependencyService, Stopwatch sw)
        {
            _dependencyService = dependencyService;
            var swInner = new Stopwatch();
            swInner.Start();
            SetupDependencies(_dependencyService);
            _databaseService = _dependencyService.Get<IDatabaseService>();
            Task.Run
            (
                async () =>
                {
                    await _databaseService.InitAsync();
                }
            );
            RegisterViewModelAndPageRelationships();
            swInner.Stop();

            Debug.WriteLine($"Took {swInner.Elapsed.TotalSeconds} seconds to register dependencies");
            InitializeComponent();

            var themeService = _dependencyService.Get<IThemeService>();
            themeService.SetInitialTheme();

            _navigationService = _dependencyService.Get<INavigationService>();
            _navigationService.SetHomePage<AppShell>();
            sw.Stop();

            Debug.WriteLine($"Took {sw.Elapsed.TotalSeconds} seconds to register everything");
        }

        private void SetupDependencies(IDependencyService ds)
        {
            ds.Init()
            .Register<ILog, Log>(true)
            .Register<IDependencyService>(() => _dependencyService)
            .Register<IPageViewModelService, ManualPageViewModelService>(false)
            .Register<INavigationService, NavigationService>(false)
            .Register<IPreferenceService, PreferenceService>(true)
            .Register<IAppPreferences, AppPreferences>(true)
            .Register<IDatabase, LiteDbDatabase>(true)
            .Register<IDatabaseService, DatabaseService>(true)
            .Register<IUserLogic, UserLogic>(true)
            .Register<IItemListLogic, ItemListLogic>(true)
            .Register<IPlatformThemeService, XamarinFormsThemeService>(true)
            .Register<IThemeService, ThemeService>(false)
            .RegisterNativeDependencies();

            RegisterViewModelDependencies(ds);
        }

        private void RegisterViewModelAndPageRelationships()
        {
            var _pageViewModelService = _dependencyService.Get<IPageViewModelService>();
            _pageViewModelService.RegisterPagesToViewModels(() =>
            {
                return new Dictionary<Type, Type>
                {
                    { typeof(AppShell), typeof(AppShellViewModel) },
                    { typeof(ItemListPage), typeof(ItemListViewModel) },
                    { typeof(ItemPage), typeof(ItemViewModel) },
                    { typeof(SettingsPage), typeof(SettingsViewModel) },
                    { typeof(AboutPage), typeof(AboutViewModel) }
                };
            });
        }

        private IDependencyService RegisterViewModelDependencies(IDependencyService ds)
        {
            ds.Register<AppShellViewModel>(false)
            .Register<ItemListViewModel>(false)
            .Register<ItemViewModel>(false)
            .Register<SettingsViewModel>(false)
            .Register<AboutViewModel>(false);

            return ds;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
