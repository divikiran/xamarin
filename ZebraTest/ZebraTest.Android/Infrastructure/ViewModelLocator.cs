using System;
using ZebraTest.Infrastructure;

namespace ZebraTest.Droid.Infrastructure
{
    public class ViewModelLocator : IViewModelLocator
    {
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public T Resolve<T>(ZebraTest.Services.IScannerService scanner)
        {
            return Container.Resolve<T>();
        }
    }
}
