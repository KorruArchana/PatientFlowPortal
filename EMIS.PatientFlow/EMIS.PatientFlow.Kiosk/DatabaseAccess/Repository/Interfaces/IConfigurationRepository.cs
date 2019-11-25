using System.Collections.Generic;
using System.Windows.Media.Imaging;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
    interface IConfigurationRepository
    {
        T GetKioskConfiguration<T>(string configType);
        Credential GetPatientFlowUser();
        Options GetModule(List<Options> moduleOptions, int moduleId);
        BitmapImage GetLogoImage();
		List<SiteMapImage> GetSiteMapImage();
		void SaveWebClientConfiguration(ApiUser user);
	}
}
