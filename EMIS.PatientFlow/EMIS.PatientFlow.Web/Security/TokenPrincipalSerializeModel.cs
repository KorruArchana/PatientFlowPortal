
namespace EMIS.PatientFlow.Web.Security
{
    public class TokenPrincipalSerializeModel
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}