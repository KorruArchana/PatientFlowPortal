using System.Collections.Generic;
using System.Windows;
using EMIS.PatientFlow.Kiosk.Enum;

namespace EMIS.PatientFlow.Kiosk.Model
{
	public class Alerts : Entity
	{
		public string AlertText { get; set; }
		public string Gender { get; set; }
		public int Age1 { get; set; }
		public int Age2 { get; set; }
		public AgeOperations Operation { get; set; }
		public DisplayType AlertsDisplayType { get; set; }
		public List<int> OrganisationIds { get; set; }
		public List<int> SessionHolderIdList { get; set; }
		public List<string> LinkedKiosk { get; set; }
		public int OrganisationId { get; set; }
	}

	public class AlertDisplay : Alerts
	{
		public bool IsImportantAlert
		{
			get { return AlertsDisplayType == DisplayType.Important; }
		}

        public string ImportantText
        {
            get { return IsImportantAlert ? "IMPORTANT:" : string.Empty; }
        }

        public string FontWeight
		{
			get { return IsImportantAlert ? "Bold" : null; }
		}

		public string FontColor
		{
			get { return IsImportantAlert ? "#D8403C" : "#535353"; }
		}

		public string InsetBorderFontColor
		{
			get { return IsImportantAlert ? "#D8403C" : "#848484"; }
		}

		public string AlertImagepath
		{
			get
			{
				return IsImportantAlert ?
					   ("pack://application:,,,/Assets/Icons/UIICons/ImportantAlert.png") :
					   ("pack://application:,,,/Assets/Icons/UIICons/StandardAlert.png");
			}
		}

		public Thickness ImageMargin
		{
			get
			{
				return IsImportantAlert ?
						new Thickness(0, 0, 0, 2) :
						new Thickness(0, -4, 0, -4);
			}
		}
	}
}
