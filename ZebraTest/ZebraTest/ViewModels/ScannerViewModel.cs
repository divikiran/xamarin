using System;
using System.Linq;
using ZebraTest.Services;

namespace ZebraTest.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        private string _scannedBarcodeText;
        public string ScannedBacodeText 
        { 
            get { return _scannedBarcodeText; }
            set
            {
                _scannedBarcodeText = value;
                NotifyPropertyChanged(nameof(ScannedBacodeText));
            }
        }

        public IScannerService _scannerService;

        public ScannerViewModel(IScannerService scannerService)
        {

            ScannedBacodeText = "From Scanner View Model";

            _scannerService = scannerService;
            _scannerService.OnBarcodeScanned += _scannerService_OnBarcodeScanned;

        }

        void _scannerService_OnBarcodeScanned(object sender, Services.Models.OnBarcodeScannedEventArgs e)
        {
            var scanneBarcode = e?.BarCodes?.FirstOrDefault();
            if (scanneBarcode == null)
            {
                ScannedBacodeText = "Invalid bar code";
                return;
            }


            var barCodeScanned = scanneBarcode.BarCode;
            var symbology = scanneBarcode.Symbology;

            ScannedBacodeText = barCodeScanned;

            if(!string.IsNullOrEmpty(ScannedBacodeText) && ScannedBacodeText != "From Main View Model")
            {
                ScannedBacodeText = ReverseString(ScannedBacodeText);
            }
        }

        public string ReverseString(string barcode)
        {
            char[] charArray = barcode.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
