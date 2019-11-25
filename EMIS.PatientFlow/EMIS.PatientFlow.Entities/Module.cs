namespace EMIS.PatientFlow.Entities
{
    public class Module : Entity
    {
        public string ModuleName { get; set; }
        public string ModuleNameToDisplay { get; set; }
        public string TranslatedText { get; set; }
        public Language Language { get; set; }
        public int TranslationRefId { get; set; }
        public int TranslationTypeId { get; set; }
        public string ModifiedBy { get; set; }
		public int ModuleId { get; set; }
		public bool IsSelected { get; set; }
	}
}
