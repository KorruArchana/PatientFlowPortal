using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class KioskRepository : BaseRepository,  IKioskRepository
    {
       
		public async Task<List<Kiosk>> GetKioskDetailListForOrganisation(int organisationId)
		{
			return await GetAsync<List<Kiosk>>("api/Kiosk/GetKioskDetailListForOrganisation?organisationId=" + organisationId);
		}
   
        public async Task<Kiosk> GetKioskDetails(int kioskId)
        {
            return await GetAsync<Kiosk>("api/Kiosk/GetKioskDetails?kioskId=" + kioskId);
        }

		public async Task<List<Kiosk>> GetKiosksWithUsageLog()
		{
			return await GetAsync<List<Kiosk>>("api/Kiosk/GetKiosksWithUsageLog");
		}

		public async Task<string> UpdateKioskStatus(int kioskId, string connectionId, int status = 1)
        {
	        if (string.IsNullOrEmpty(connectionId))
		        throw new Exception("Error");
            return await GetAsync<string>("api/Kiosk/UpdateKiosk?connectionId=" + connectionId + "&status=" + status + "&kioskId=" + kioskId);
        }

        public async Task<int> AddKiosk(Kiosk kiosk)
        {
            int result = 0;
            try
            {
                var jsonKiosk = new JavaScriptSerializer().Serialize(kiosk);
                result = await PostAsJsonAsync<int>("api/Kiosk/AddKioskDetails", jsonKiosk);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, CurrentUser.Name);
            }

            return result;
        }

        public async Task<int> EditKiosk(Kiosk kiosk)
        {
            int result = 0;
            try
            {
				var jsonKiosk = kiosk.ConvertToJsonString();
                result = await PostAsJsonAsync<int>("api/Kiosk/EditKiosk", jsonKiosk);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(
                    LogType.Error, 
                    ex.Message, 
                    ex, 
                    CurrentUser.Name);
            }

            return result;
        }

        public async Task<int> SaveAppointmentSlotType(int organisationId, List<AppointmentSlotType> appointmentSlotTypes)
        {
            var jsonSlotTypes = appointmentSlotTypes.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Kiosk/SaveAppointmentSlotType", jsonSlotTypes);
        }

        public async Task<List<AppointmentSlotType>> GetAppointmentSlotTypes(int organisationId)
        {
            return
                await
                    GetAsync<List<AppointmentSlotType>>(
                        "api/Kiosk/GetAppointmentSlotTypes?organisationId=" + organisationId);
        }

        public async Task<bool> DeleteKiosk(int kioskId)
        {
            return await GetAsync<bool>("api/Kiosk/DeleteKiosk?kioskId=" + kioskId);
        }

        public async Task<KioskSyncKeys> GetKioskSyncKeys(int kioskId)
        {
            return await GetAsync<KioskSyncKeys>("api/Kiosk/GetKioskSyncKeys?kioskId=" + kioskId);
        }


		public async Task<List<Kiosk>> GetKioskList()
		{
			return await GetAsync<List<Kiosk>>("api/Kiosk/GetKioskList");
		}

		public async Task<List<Kiosk>> GetKioskListForOrganisation(int organisationId)
		{
			return await GetAsync<List<Kiosk>>("api/Kiosk/GetKioskListForOrganisation?organisationId=" + organisationId);
		}

		public async Task<List<Kiosk>> GetKiosks()
		{
			return await GetAsync<List<Kiosk>>("api/Kiosk/GetKiosks");
		}

		public async Task<bool> ValidateKioskName(string kioskName, int kioskId, List<int> organisationList)
		{
			return
				await
					GetAsync<bool>(
						"api/Kiosk/ValidateKioskName?kioskName=" + kioskName + "&kioskId=" + kioskId
						+ "&organisationList="
						+ organisationList.ConvertToJsonString());
		}
	}
}
