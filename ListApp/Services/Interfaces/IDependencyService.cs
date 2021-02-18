using System;

namespace ListApp.Services
{
    public interface IDependencyService
    {
        void RegisterNativeDependencies();
        IDependencyService Init();
        T Get<T>() where T : class;
        object Get(Type type);
        IDependencyService Register<T>(bool isSingleton) where T : class;
        IDependencyService Register<TInterface, TConcrete>(bool isSingleton) where TInterface : class
                                                               where TConcrete : class, TInterface;
        IDependencyService Register<T>(T instance) where T : class;
        IDependencyService Register<T>(Func<T> creator) where T : class;
    }
}
