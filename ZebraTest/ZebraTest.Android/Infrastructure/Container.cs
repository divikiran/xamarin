using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using ZebraTest.Droid.Services;
using ZebraTest.Infrastructure;
using ZebraTest.Services;
using ZebraTest.ViewModels;

namespace ZebraTest.Droid.Infrastructure
{
    public class Container
    {
        private static UnityContainer _unity;

        internal static void Initialize()
        {
            _unity = new UnityContainer();
            IOCResolver.Unity = _unity;
            RegisterTypes();

        }

        private static void RegisterTypes()
        {
            #region Services
            _unity.RegisterType<IScannerService, ScannerService>(new ContainerControlledLifetimeManager());
            #endregion

            #region Views
            _unity.RegisterType<MainPage, MainPage>(new ContainerControlledLifetimeManager());
            #endregion

            #region ViewModels
            _unity.RegisterType<ScannerViewModel, ScannerViewModel>(new ContainerControlledLifetimeManager());
            _unity.RegisterType<MainViewModel, MainViewModel>(new InjectionConstructor(_unity.Resolve<IScannerService>()));
            _unity.RegisterType<BaseViewModel, BaseViewModel>(new ContainerControlledLifetimeManager());
            #endregion
        }

        public static T Resolve<T>()
        {
            return _unity.Resolve<T>();
        }

    }
}
