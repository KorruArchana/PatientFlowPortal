using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Kiosk.Enum
{
    public enum AgeOperations
    {
        [Display(Name = "None")]
        None,

        [Display(Name = "LessThan")]
        LessThan,

        [Display(Name = "GreaterThan")]
        GreaterThan,

        [Display(Name = "Between")]
        Between,
    }

	public enum DisplayType
	{
		Standard = 1,
		Directional = 2,
		Important = 3
	}
}
