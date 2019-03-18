using System;
using ZebraTest.Infrastructure;

namespace ZebraTest.Droid.Infrastructure
{
    public class Setup
    {
        private void BootStrap()
        {
            var bootStrap = new FormsBootStrap();
            bootStrap.SerViewModelLocator(new ViewModelLocator());
        }

        public void Initialize()
        {
            BootStrap();
            Container.Initialize();
        }
    }
}
