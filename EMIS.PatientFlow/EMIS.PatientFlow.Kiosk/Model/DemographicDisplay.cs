namespace EMIS.PatientFlow.Kiosk.Model
{
    public class DemographicDisplay
    {
        public int DemographicId { get; set; }

        public string DemographicDetail { get; set; }

        public string DemographicValue { get; set; }

        public string DemographicDisplayValue { get; set; }

        public string ButtonYesImagePath { get; set; }

        public string YesText { get; set; }

        public string ButtonNoImagePath { get; set; }

        public string NoText { get; set; }

        public bool IsChecked { get; set; }
    }
}
