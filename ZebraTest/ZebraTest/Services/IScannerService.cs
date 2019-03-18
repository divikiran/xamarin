using System;
using ZebraTest.Services.Models;

namespace ZebraTest.Services
{
    public interface  IScannerService
    {
        event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;
    }
}
