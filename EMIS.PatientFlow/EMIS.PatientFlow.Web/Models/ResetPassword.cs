namespace EMIS.PatientFlow.Web.Models
{
    public class ResetPassword
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}