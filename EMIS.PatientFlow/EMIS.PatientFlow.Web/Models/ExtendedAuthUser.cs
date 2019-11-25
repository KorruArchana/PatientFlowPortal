using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Models
{
    public class ExtendedAuthUser : AuthUser
    {
        public string RolesAsCommaSeparatedString { get; set; }
    }
}