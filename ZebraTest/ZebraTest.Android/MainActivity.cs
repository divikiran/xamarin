using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZebraTest.Droid.Services;
using Xamarin.Forms;
using Symbol.XamarinEMDK;
using ZebraTest.Droid.Infrastructure;
using ZebraTest.Services;
using ZebraTest.Infrastructure;

namespace ZebraTest.Droid
{
    [Activity(Label = "ZebraTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //public ScannerService Scanner { get; set; }
        ScannerService Scanner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var setup = new Setup();
            setup.Initialize();

            Scanner = Resolver.Locator.Resolve<ScannerService>();
            EMDKManager.GetEMDKManager(Android.App.Application.Context, Scanner);
            App.Scanner = Scanner;
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            Scanner.InitScanner();
        }

        protected override void OnPause()
        {
            base.OnPause();
            Scanner.DeinitScanner();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Scanner.Destroy();
        }
    }
}