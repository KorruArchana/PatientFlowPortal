using System.ComponentModel.DataAnnotations;
namespace EMIS.PatientFlow.Common.Enums
{
    public enum SystemType
    {
        [Display(Name = "EMIS - Web")]
        EmisWeb = 1,

        [Display(Name = "EMIS - PCS")]
        EmisPcs = 2,

        [Display(Name = "None")]
        None = 4,

		[Display(Name = "TOPAS")]
        Topas = 5,

        [Display(Name = "EMIS - PCS (Local Configuration)")]

        EmisPcsLan = 6,

		[Display(Name = "TPP - SystmOne")]
		TPPSystmOne = 7
	}
}
