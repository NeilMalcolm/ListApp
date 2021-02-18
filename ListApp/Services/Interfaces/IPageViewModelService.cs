using ListApp.Pages;
using ListApp.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ListApp.Services
{
    public interface IPageViewModelService
    {
        BaseViewModel GetViewModelForPage<T>() where T : Page;
        void RegisterPagesToViewModels(Func<Dictionary<Type, Type>> pageViewModelDict);
    }
}
