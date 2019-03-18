using System;
namespace ZebraTest.Infrastructure
{
    public interface IViewModelLocator
    {
        T Resolve<T>();
        T Resolve<T>(Services.IScannerService scanner);

    }
}
