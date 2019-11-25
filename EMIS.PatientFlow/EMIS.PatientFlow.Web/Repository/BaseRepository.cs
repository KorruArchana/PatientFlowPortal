using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Security;
using Newtonsoft.Json.Linq;

namespace EMIS.PatientFlow.Web.Repository
{
    public class BaseRepository
    {
        private readonly string _baseUri = Config.ConfigSiteUrl;

        public TokenPrincipal CurrentUser
        {
            get
            {
                if ((HttpContext.Current.User as TokenGenericPrincipal) == null)
                    return null;

                return (HttpContext.Current.User as TokenGenericPrincipal).Instance;
            }
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                
                if (CurrentUser != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);
                
                var response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();

                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                if (CurrentUser != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);

                var response = await client.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();

                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> GetAsync<T>(
            string uri,
            string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();

                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PostAsync<T>(
            string uri,
            List<KeyValuePair<string, string>> data)
        {
            var content = new FormUrlEncodedContent(data);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                if (CurrentUser != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);
                
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();

                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PostAsJsonAsync<T>(
            string uri,
            string data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                if (CurrentUser != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);

                var response = await client.PostAsJsonAsync(uri, data);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();
                    
                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PostAsJsonAsync<T>(
            string uri,
            object data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (CurrentUser != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);

                var response = await client.PostAsJsonAsync(uri, data);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();

                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> PostAsync<T>(
            string uri,
            List<KeyValuePair<string, string>> data,
            string token)
        {
            var content = new FormUrlEncodedContent(data);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                        response.EnsureSuccessStatusCode();
                    
                    JToken error = await response.Content.ReadAsAsync<JToken>();

                    throw new HttpRequestException(GetErrorMessage(error));
                }

                return await response.Content.ReadAsAsync<T>();
            }
        }

        private string GetErrorMessage(JToken error)
        {
            string customError = string.Empty;
            if (error["ModelState"] != null && error["ModelState"].Children().Any())
                customError = error["ModelState"].Children().First().First()[0].ToString();

            return string.Format(
                "Request failed: {0} {1} {2}",
                error.Value<string>("error"),
                error.Value<string>("error_description"),
                           customError);
        }
    }
}