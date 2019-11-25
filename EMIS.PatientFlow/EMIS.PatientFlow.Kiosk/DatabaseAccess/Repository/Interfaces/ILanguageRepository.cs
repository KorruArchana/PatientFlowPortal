using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
    interface ILanguageRepository
    {
        List<LanguageModel> GetLanguageList();
        string GetControlText(string screenCode, string controlUniqueId);
    }
}
