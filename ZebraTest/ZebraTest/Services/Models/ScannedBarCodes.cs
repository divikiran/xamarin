using System;
namespace ZebraTest.Services.Models
{
    public class ScannedBarCodes
    {
        public string BarCode { get; set; }
        public string Symbology { get; set; }

        public ScannedBarCodes(string bardcode, string symbology)
        {
            BarCode = bardcode;
            Symbology = symbology;
        }
    }
}
