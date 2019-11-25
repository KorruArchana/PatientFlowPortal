using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface ILanguageRepository
    {
        IEnumerable<Language> GetLanguageList();
        IEnumerable<Module> GetModules(int pageNo, int pageSize, out int recordCount);
        IEnumerable<Module> GetModuleDetails(int moduleId, string languageCode);
        int SaveModuleTranslations(List<Module> model);
        IEnumerable<Translation> GetTranslation(int pageSize, DateTime syncedTranslationDate, out DateTime lastRowModifiedDate);
        IEnumerable<ScreenControl> GetScreenControl(int pageSize, DateTime lastSyncedDate, out DateTime lastRowModifiedDate);
    }
}
