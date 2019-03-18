using System;
using System.Collections.Generic;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using Xamarin.Forms;
using ZebraTest.Droid.Services;
using ZebraTest.Services;
using ZebraTest.Services.Models;

[assembly: Dependency(typeof(ScannerService))]
namespace ZebraTest.Droid.Services
{
    public class ScannerService : Java.Lang.Object, EMDKManager.IEMDKListener, IScannerService
    {
        private EMDKManager _emdkManager;
        private BarcodeManager _barcodeManager;
        private Scanner _scanner;

        public ScannerService()
        {
        }

        public event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;

        public void OnClosed()
        {
            if(_emdkManager != null)
            {
                _emdkManager.Release();
                _emdkManager = null;
            }
        }

        public void OnOpened(EMDKManager emdkManager)
        {
            _emdkManager = emdkManager;
            InitScanner();
        }

        public void InitScanner()
        {
            try
            {
                if (_emdkManager == null)
                    return;

                if(_barcodeManager == null)
                {
                    _barcodeManager = (BarcodeManager)_emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);
                    _scanner = _barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

                    if (_scanner == null)
                        return;

                    _scanner.Data += _scanner_Data;
                    _scanner.Status += _scanner_Status;

                    _scanner.Enable();

                    //set the configuration
                    ScannerConfig scannerConfig = _scanner.GetConfig();
                    scannerConfig.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                    scannerConfig.ScanParams.DecodeLEDFeedback = true;
                    scannerConfig.DecoderParams.Code11.Enabled = true;
                    scannerConfig.DecoderParams.Code39.Enabled = true;
                    scannerConfig.DecoderParams.Code93.Enabled = true;
                    scannerConfig.DecoderParams.Code128.Enabled = true;
                    _scanner.SetConfig(scannerConfig);
 
                }


            }
            catch (Exception ex)
            {

            }
        }

        void _scanner_Status(object sender, Scanner.StatusEventArgs e)
        {
            StatusData.ScannerStates state = e.P0.State;
            if(state == StatusData.ScannerStates.Idle)
            {
                try
                {
                    if(_scanner.IsEnabled && !_scanner.IsReadPending)
                    {
                        _scanner.Read();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }


        void _scanner_Data(object sender, Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if(scanDataCollection != null && scanDataCollection.Result == ScannerResults.Success)
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();
                List<ScannedBarCodes> scannedBarCodes = new List<ScannedBarCodes>();
                foreach (var data in scanData)
                {
                    string barCode = data.Data;
                    string symbology = data.LabelType.Name();

                    scannedBarCodes.Add(new ScannedBarCodes(barCode, symbology));
                }
                this.OnBarcodeScanned?.Invoke(this, new OnBarcodeScannedEventArgs(scannedBarCodes));
            }

        }

        internal void DeinitScanner()
        {
            if(_emdkManager != null)
            {
                if(_scanner != null)
                {
                    try
                    {
                        _scanner.Data -= _scanner_Data;
                        _scanner.Status -= _scanner_Status;
                        _scanner.Disable();

                    }
                    catch (ScannerException ex)
                    {

                    }
                }

                if(_barcodeManager != null)
                {
                    _emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }
                _barcodeManager = null;
                _scanner = null;
            }
        }

        public void Destroy()
        {
            //clean up the emdk manager 
            if(_emdkManager != null)
            {
                //EMDK: release the emdk manager object
                _emdkManager.Release();
                _emdkManager = null;
            }
        }

    }

}
