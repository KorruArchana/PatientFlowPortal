using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EMIS.PatientFlow.Web.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private IAuthenticationRepository repository;

        public TestController(IAuthenticationRepository repository)
        {
            repository = repository;
            BaseRepository tests = new BaseRepository();
             Organisation org=new Organisation();

             var jsonDepartment = new JavaScriptSerializer().Serialize(org);

             List<KeyValuePair<string, string>> x=new List<KeyValuePair<string,string>>();
             x.Add(new KeyValuePair<string, string>("value", jsonDepartment));
             x.Add(new KeyValuePair<string, string>("id", "1"));

             var x1 = tests.PostAsync<int>("api/Test/Test2", x);
        }


        public async Task<ActionResult> Index(string connectionId = "abc", int status = 1)
        {
            using (var httpClient = new HttpClient())
            {
                string uri = ConfigurationManager.AppSettings["SignalRURI"].ToString();
                httpClient.BaseAddress = new Uri(uri);

                HttpResponseMessage response = await httpClient.GetAsync("api/SignalRTest/UpdateKiosk?connectionId=" + connectionId + "&status=" + status);

            }

            return View();
        }

    }
}