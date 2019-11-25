using Newtonsoft.Json.Serialization;

namespace EMIS.PatientFlow.Web.Helper
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}