namespace EMIS.PatientFlow.Entities
{
    public class Translation : Entity
    {
        public int LanguageId { get; set; }
        public int TranslationRefId { get; set; }
        public string TranslationText { get; set; }
    }
}
