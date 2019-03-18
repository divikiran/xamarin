using System.Collections;
using System.Collections.Generic;

namespace ZebraTest.Services.Models
{
    public class OnBarcodeScannedEventArgs
    {
        public IEnumerable<ScannedBarCodes> BarCodes { get; set; }

        public OnBarcodeScannedEventArgs(IEnumerable<ScannedBarCodes> barCodes)
        {
            BarCodes = barCodes;
        }
    }
}