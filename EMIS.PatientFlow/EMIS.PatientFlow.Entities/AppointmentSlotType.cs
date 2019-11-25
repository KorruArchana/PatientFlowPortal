namespace EMIS.PatientFlow.Entities
{
    public class AppointmentSlotType
    {
        public string SlotTypeId { get; set; }
        public string Description { get; set; }
        public int OrganisationId { get; set; }
		public string OrganisationName { get; set; }
		public bool IsSelected { get; set; }
	}

	public class KioskSystemInformation
	{
		public string PcName { get; set; }
		public string IpAddress { get; set; }
		public string Version { get; set; }

	}
}
