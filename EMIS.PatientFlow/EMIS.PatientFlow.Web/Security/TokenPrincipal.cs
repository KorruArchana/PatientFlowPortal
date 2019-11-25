using System.Linq;
using System.Security.Principal;

namespace EMIS.PatientFlow.Web.Security
{
    public class TokenGenericPrincipal : GenericPrincipal
    {
        public TokenPrincipal Instance { get; set; }
        public TokenGenericPrincipal(TokenPrincipal identity)
            : base(identity.Identity, identity.Roles)
        {
            Instance = identity;
        }
    }
    public class TokenPrincipal : IPrincipal
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
        public IIdentity Identity { get; private set; }
        public TokenPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }
        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}