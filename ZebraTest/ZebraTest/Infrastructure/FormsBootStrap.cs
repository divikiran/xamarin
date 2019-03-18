using System;
namespace ZebraTest.Infrastructure
{
    public class FormsBootStrap
    {
        public void SerViewModelLocator(IViewModelLocator viewModelLocator)
        {
            Resolver.Locator = viewModelLocator;
        }
    }
}
