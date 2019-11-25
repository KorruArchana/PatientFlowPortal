using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
    public interface IPushServiceAlertRepository
    {
        void AddAlert(List<Alerts> alert);
        void UpdateAlert(Alerts alert);
        void DeleteAlert(int alertId);
    }
}
