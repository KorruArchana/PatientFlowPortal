namespace EMIS.PatientFlow.Kiosk.Model
{
    public class Member
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public int WaitingTime { get; set; }
        public string PracticeName { get; set; }
		public string Code { get; set; }

        public bool? IsWaitingTimeVisible { get; set; }
		public string MemberIdentifiers { get; set; }

		public string DisplayName
        {
            get
            {
                return string.IsNullOrEmpty(Title) ?
                   string.Format("{0} {1}", FirstName, LastName) :
                   string.Format("{0}. {1} {2}", Title, FirstName, LastName);
            }
        }

    }
}
