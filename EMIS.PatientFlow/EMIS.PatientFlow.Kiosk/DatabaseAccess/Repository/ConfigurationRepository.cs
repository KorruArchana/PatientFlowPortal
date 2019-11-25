using System.Collections.Generic;
using System.Windows.Media.Imaging;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
    public class ConfigurationRepository : BaseRepository, IConfigurationRepository
    {
        public T GetKioskConfiguration<T>(string configType)
        {
            return DbAccess.GetKioskConfiguration<T>(configType);
        }

        public Credential GetPatientFlowUser()
        {
            return DbAccess.GetPatientFlowUser();
        }

        public Options GetModule(List<Options> moduleOptions, int moduleId)
        {
            return DbAccess.GetModule(moduleOptions, moduleId);
        }

        public BitmapImage GetLogoImage()
        {
            return DbAccess.GetLogoImage();
        }

		public List<SiteMapImage> GetSiteMapImage()
		{
			return DbAccess.GetSiteMapImage();
		}

		public void SaveWebClientConfiguration(ApiUser user)
		{
			DbAccess.SaveWebClientConfiguration(user);
		}
	}
}
