using System.Text;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.Common.Extensions;

namespace EMIS.PatientFlow.API
{
    public class PatientClient
    {
        private readonly string _appId;
        private readonly string _apiUrl;
        public PatientClient(string appId, string apiUrl)
        {
            _appId = appId;
            _apiUrl = apiUrl;
        }

        public bool SubscribePatientNewsletter(string email)
        {
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add("AppId", _appId);
                client.Headers.Add("content-type", "application/json");
                string inputParm = "{\"Email\": \"" + email + "\"}";
                string apiResult = Encoding.ASCII.GetString(client.UploadData(_apiUrl, "POST", Encoding.Default.GetBytes(inputParm)));
                var response = apiResult.ConvertFromJsonString<PatientNewsletterResponse>();
                return response.Success;
            }
        }
    }
}
