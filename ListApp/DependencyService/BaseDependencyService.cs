using ListApp.Services;
using SimpleInjector;
using System;

namespace ListApp.DependencyService
{
    public abstract class BaseDependencyService : IDependencyService
    {
        private Container container;

        public abstract void RegisterNativeDependencies();

        public IDependencyService Init()
        {
            container = new Container();
            return this;
        }

        public T Get<T>() where T : class
        {
            try
            {
                return container.GetInstance<T>();
            }
            catch (Exception ex)
            {
                // issue
            }

            return null;
        }

        public object Get(Type type)
        {
            try
            {
                return container.GetInstance(type);
            }
            catch (Exception ex)
            {
                // issue
            }

            return null;
        }

        public IDependencyService Register<T>(bool isSingleton) where T : class
        {
            if (isSingleton)
            {
                container.Register<T>(Lifestyle.Singleton);
            }
            else
            {
                container.Register<T>();
            }

            return this;
        }

        public IDependencyService Register<TInterface, TConcrete>(bool isSingleton) where TInterface : class
                                                                      where TConcrete : class, TInterface
        {
            if (isSingleton)
            {
                container.Register<TInterface, TConcrete>(Lifestyle.Singleton);
            }
            else
            {
                container.Register<TInterface, TConcrete>();
            }
            return this;
        }

        public IDependencyService Register<T>(T instance) where T : class
        {
            container.RegisterInstance<T>(instance);
            return this;
        }

        public IDependencyService Register<T>(Func<T> creator) where T : class
        {
            container.Register(creator);
            return this;
        }
    }
}
