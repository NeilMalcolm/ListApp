using ListApp.Pages;
using ListApp.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ListApp.Services
{
    public class ManualPageViewModelService : IPageViewModelService
    {
        readonly IDependencyService _dependencyService;

        static Dictionary<Type, Type> pageViewModelCache { get => lazyPageViewModelCache.Value; }
        static Lazy<Dictionary<Type, Type>> lazyPageViewModelCache;

        public ManualPageViewModelService(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        public BaseViewModel GetViewModelForPage<T>() where T : Page
        {
            if (lazyPageViewModelCache is null || pageViewModelCache is null)
            {
                throw new InvalidOperationException($"You must register the Page and ViewModels first. Please register with {nameof(RegisterPagesToViewModels)}.");
            }

            var pageType = typeof(T);

            if (!pageViewModelCache.ContainsKey(pageType))
            {
                throw new InvalidOperationException($"Page of type {nameof(T)} not registered. Please register first with {nameof(RegisterPagesToViewModels)}");
            }

            var viewModelType = pageViewModelCache[pageType];
            return (BaseViewModel)_dependencyService.Get(viewModelType);
        }

        public void RegisterPagesToViewModels(Func<Dictionary<Type, Type>> pageViewModelDict)
        {
            lazyPageViewModelCache = new Lazy<Dictionary<Type, Type>>(pageViewModelDict);
        }
    }
}
