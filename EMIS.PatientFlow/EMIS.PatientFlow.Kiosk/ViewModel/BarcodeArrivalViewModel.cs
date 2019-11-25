using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class BarCodeMessage
    {
        public string BarCode { get; set; }
        public int Status { get; set; }
    }

    public class BarcodeArrivalViewModel : ViewModelBase
    {
        private string _barcodeSelectionText;
        private RelayCommand<string> _loadedCommand;

        public BarcodeArrivalViewModel()
        {
            InitializeControls();
            Messenger.Default.Register<BarCodeMessage>(this, BarcodeDetected);
        }

        private void BarcodeDetected(BarCodeMessage obj)
        {
            int slotid;

            if (obj.Status == 1 && int.TryParse(obj.BarCode, out slotid))
            {
                GlobalVariables.IsBarCodeArrivalDone = true;
                GlobalVariables.SelectedSlotId = slotid;
                UnRegisterBarcodeMessage();
                Messenger.Default.Send(AppPages.ArrivalRouting);
            }
            else
            {
                UnRegisterBarcodeMessage();
                Messenger.Default.Send(AppPages.ExceptionDivert);
            }
        }

        public string BarcodeSelectionText
        {
            get
            {
                return _barcodeSelectionText;
            }
            set
            {
                _barcodeSelectionText = value;
                RaisePropertyChanged("BarcodeSelectionText");
            }
        }

        public RelayCommand<string> LoadedCommand
        {
            get
            {
                return _loadedCommand
                       ?? (_loadedCommand = new RelayCommand<string>(
                           p =>
                           {
                               if (!GlobalVariables.IsDbConnected)
                               {
                                   UnRegisterBarcodeMessage();
                                   Messenger.Default.Send(AppPages.ExceptionDivert);
                               }
                           }));
            }
        }

		private void UnRegisterBarcodeMessage()
        {
            Messenger.Default.Unregister<BarCodeMessage>(this, BarcodeDetected);
        }

        /// <summary>
        /// Method to initialize controls of UI.
        /// </summary>
        private void InitializeControls()
        {
            BarcodeSelectionText = GlobalVariables.SelectedLanguageIdText[LanguageText.BarcodeSelectionText];
        }
    }
}