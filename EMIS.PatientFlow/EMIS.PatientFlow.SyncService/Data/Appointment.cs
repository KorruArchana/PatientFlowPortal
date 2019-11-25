using System;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.SyncService.Data
{
	public class Appointment
	{
		[Required]
		public Patient BookedPatient { get; set; }
		[Required]
		public Member SessionHolder { get; set; }
		[Required]
		public DateTime AppointmentTime { get; set; }
		public long Id { get; set; }
		public string TPPAppointmentId { get; set; }
		public string AppointmentStatus { get; set; }
		public string AppointmentTimeString
		{
			get
			{
				return AppointmentTime.ToString("dd-MM-yyyy hh:mm");
			}
		}

		public long SiteId { get; set; }
		public string Reception { get; set; }
		public int Duration { get; set; }
	}


	public class KioskProductDetails
	{
		public string KioskGuid { get; set; }
		public string SyncServiceGuid { get; set; }
	}

	//[{"Id":"1","OrganisationName":"Emis Health India","DatabaseName":"55555","SystemType":"EMIS - Web","SiteId":null,"MainLocation":false}]
	//{"KioskGuid":"8e12edac-4da4-46af-8095-85a8d30ac501"}

	public class Organisation
	{
		public string Id { get; set; }
		public string OrganisationName { get; set; }
		public string DatabaseName { get; set; }
		public string SystemType { get; set; }
		public long? SiteId { get; set; }
		public bool MainLocation { get; set; }
	}
}
