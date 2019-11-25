using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.HubClients;
using System.Threading;
using System.Windows;

namespace EMIS.PatientFlow.Kiosk
{
    public partial class App : Application
    {
        public static readonly ManualResetEvent StopConnection = new ManualResetEvent(false);

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Logger.Instance.WriteLog(LogType.Debug, "Kiosk Closed", null, GlobalVariables.RegistrationKey);
            KioskHubClient.Instance.CloseHub();
        }
    }
}
