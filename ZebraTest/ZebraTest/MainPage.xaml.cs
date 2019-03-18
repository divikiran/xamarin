using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xamarin.Forms;
using ZebraTest.Infrastructure;
using ZebraTest.Services;
using ZebraTest.ViewModels;

namespace ZebraTest
{
    public partial class MainPage : ContentPage
    {
        MainViewModel vm;

        public MainPage()
        {
            InitializeComponent();
            vm = Resolver.Locator.Resolve<MainViewModel>();
            this.BindingContext = vm;
            //BindingContext = new MainViewModel(App.Scanner);
        }
    }
}
