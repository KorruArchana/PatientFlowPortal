using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {
        public List<LanguageModel> GetLanguageList()
        {
            return DbAccess.GetLanguageList();
        }

        public string GetControlText(string screenCode, string controlUniqueId)
        {
            return DbAccess.GetControlText(screenCode, controlUniqueId);
        }
    }
}
