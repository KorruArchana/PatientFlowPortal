using System.ComponentModel;
using EMIS.PatientFlow.Kiosk.Helper;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public string KioskId = Utilities.GetAppSettingValue("RegistrationKey");

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
