using System;
using Unity;

namespace ZebraTest.Infrastructure
{
    public static class IOCResolver
    {
        public static IUnityContainer Unity;
        public static T Resolve<T>()
        {
            return Unity.Resolve<T>();
        }
    }
}
