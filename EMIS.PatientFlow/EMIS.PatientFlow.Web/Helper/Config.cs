using System;
using System.Configuration;
using System.Linq;

namespace EMIS.PatientFlow.Web.Helper
{
    public static class Config
    {
        public static string ConfigSiteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SignalRURI"];
            }
        }

        public static int BranchSiteKey {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["BranchSiteKey"]);
            }
        }

        public static int PageSize
        {
            get
            {
                int pageSize;
                if (!Int32.TryParse(ConfigurationManager.AppSettings["PageSize"], out pageSize))
                    return 50;

                return pageSize;
            }
        }

        public static string GoogleApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["GoogleApiKey"];
            }
        }

        public static int KioskLogoMaxContentLength
        {
            get
            {
                int kioskLogoMaxContentLength;
                if (!Int32.TryParse(
                    ConfigurationManager.AppSettings["KioskLogoMaxContentLength"],
                    out kioskLogoMaxContentLength))
                    return 1024 * 50;

                return kioskLogoMaxContentLength;
            }
        }

        public static int KioskLogoImageHeight
        {
            get
            {
                int kioskLogoImageHeight;
                if (!Int32.TryParse(ConfigurationManager.AppSettings["KioskLogoImageHeight"], out kioskLogoImageHeight))
                    return 100;

                return kioskLogoImageHeight;
            }
        }

        public static int KioskLogoImageWidth
        {
            get
            {
                int kioskLogoImageWidth;
                if (!Int32.TryParse(ConfigurationManager.AppSettings["KioskLogoImageWidth"], out kioskLogoImageWidth))
                    return 300;

                return kioskLogoImageWidth;
            }
        }

        public static string[] KioskLogoAllowedFileExtensions
        {
            get
            {
                if (!ConfigurationManager.AppSettings["KioskLogoAllowedFileExtensions"].Any())
                    return new[] { ".jpg", ".gif", ".png" };

                return ConfigurationManager.AppSettings["KioskLogoAllowedFileExtensions"].Split(',');
            }
        }

        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

		public static string GetAppSettingSupplierIdValue
		{
			get
			{
				if (!ConfigurationManager.AppSettings.AllKeys.Contains("SupplierId"))
					return "84e23aa3-68d6-461c-88e0-5fdb48a37c96";

				return ConfigurationManager.AppSettings["SupplierId"];
			}
		}

        public static string GetAppSettingHomePageHeaderValue
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("HomePageHeader"))
                    return "Welcome!";

                return ConfigurationManager.AppSettings["HomePageHeader"];
            }
        }

        public static string GetAppSettingHomePageBodyValue
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("HomePageBody"))
                    return "Use this portal to config your check-in kiosks.";

                return ConfigurationManager.AppSettings["HomePageBody"];
            }
        }

        public static bool IsEmisWebPortal
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("SystemType"))
                    return true;

                return ConfigurationManager.AppSettings["SystemType"] == "EMIS - Web";
            }
        }
    }
}