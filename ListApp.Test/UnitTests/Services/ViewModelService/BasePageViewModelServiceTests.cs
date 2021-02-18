﻿using ListApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ListApp.Test.UnitTests.Services
{
    [TestClass]
    public abstract class BasePageViewModelServiceTests
    {
        protected IPageViewModelService PageViewModelService;

        protected abstract Dictionary<Type, Type> PageViewModelDictionary { get; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            CreateMocks();
            SetUpMocks();
            PageViewModelService = CreatePageViewModelService();
        }

        protected virtual void CreateMocks()
        {

        }

        protected virtual void SetUpMocks()
        {

        }

        [TestCleanup]
        public void AfterEachTest()
        {
            CreatePageViewModelService();
        }


        protected virtual void RegisterPageViewModelDictionary()
        {
            PageViewModelService.RegisterPagesToViewModels(() => PageViewModelDictionary);
        }

        protected abstract IPageViewModelService CreatePageViewModelService();

        public abstract void WhenGetViewModelForPageIsCalled_AndCacheIsNull_ThenInvalidOperationExceptionIsThrown();

        public abstract void WhenGetViewModelForPageIsCalled_AndPageIsNotInCache_ThenInvalidOperationExceptionIsThrown();

        public abstract void WhenGetViewModelForPageIsCalled_AndPageAndViewModelAreRegistered_ThenViewModelIsReturned();
    }
}
