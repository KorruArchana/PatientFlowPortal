namespace EMIS.PatientFlow.Web.Helper
{
    public class CustomSelectList
    {
        public CustomSelectList(
            string dataValueField,
            string dataTextField,
            bool? selectedValue)
        {
            DataTextField = dataTextField;
            DataValueField = dataValueField;
            SelectedValue = selectedValue;
        }

        public string DataValueField { get; set; }
        public string DataTextField { get; set; }
        public bool? SelectedValue { get; set; }
    }
}
