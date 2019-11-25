using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
    public class PushServiceAlertRepository : BaseRepository, IPushServiceAlertRepository
    {
        public void AddAlert(List<Alerts> alert)
        {
            DbAccess.AddAlert(alert);
        }

        public void UpdateAlert(Alerts alert)
        {
            DbAccess.UpdateAlert(alert);
        }

        public void DeleteAlert(int alertId)
        {
            DbAccess.DeleteAlert(alertId);
        }
    }
}
