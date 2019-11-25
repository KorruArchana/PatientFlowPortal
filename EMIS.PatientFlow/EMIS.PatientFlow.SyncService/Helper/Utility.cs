using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.SyncService.Helper
{
    public class Utility
    {
        public static string GetAppSettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static T ConvertFromJsonString<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static bool TryValidate(object obj, out List<ValidationResult> results)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);

            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
        }       
    }
}
