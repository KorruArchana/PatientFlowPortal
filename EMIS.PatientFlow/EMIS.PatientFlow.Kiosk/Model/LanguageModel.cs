using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace EMIS.PatientFlow.Kiosk.Model
{
	public class LanguageModel
	{
		public int LanguageId { get; set; }

		public string LanguageName { get; set; }

		public string DisplayText { get; set; }

		public string LanguageFullSizePhotoPath { get; set; }

		public string LanguageSquarePhotoPath { get; set; }
	}

	[DataContract]
	public class LanguagesList
	{
		[DataMember(Name = "Languages")]
		public List<LanguageSettingsModel> Languages { get; set; }
	}

	[DataContract]
	public class LanguageSettingsModel
	{
		[DataMember(Name = "Id")]
		public int Id { get; set; }

		[DataMember(Name = "Order")]
		public int Order { get; set; }
	}
}
