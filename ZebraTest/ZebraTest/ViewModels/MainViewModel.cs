using System;
using Xamarin.Forms;
using ZebraTest.Infrastructure;
using ZebraTest.Services;

namespace ZebraTest.ViewModels
{
    public class MainViewModel : ScannerViewModel
    {
        IScannerService _scannerService;

        public MainViewModel(IScannerService scannerService) : base(scannerService)
        {
            ScannedBacodeText = "From Main View Model";

        }
    }
}
