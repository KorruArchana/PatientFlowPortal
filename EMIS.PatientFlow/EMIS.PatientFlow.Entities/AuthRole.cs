namespace EMIS.PatientFlow.Entities
{
    public class AuthRole : Entity
    {
        public string UniqueId { get; set; }

		public string DisplayText { get { return GetDisplayText(); }}

	    private string GetDisplayText()
	    {
			return Name.Contains("EMIS Super User") ? "Egton Super User" : Name;
	    }
    }
}
