using System;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Translation
    {
        public int LanguageId { get; set; }
        public int TranslationRefId { get; set; }
        public string TranslationText { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
