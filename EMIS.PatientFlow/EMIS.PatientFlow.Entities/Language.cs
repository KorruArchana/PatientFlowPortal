namespace EMIS.PatientFlow.Entities
{
    public class Language : Entity
    {
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string LanguageText { get; set; }
        public int Order { get; set; }
        public int TranslationRefId { get; set; }
    }
}
