using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
	public interface IPushServiceRepository
	{
		void UpdateKioskStatus(int status);
		void UpdateKioskConfig(Model.Kiosk kiosk);
		void UpdateOrganisation(Organisation organisation);
		void UpdateQuestionnaire(List<int> questionnaireId);
		void SaveAdditionalInformation(
			List<LinkedMember> delayedMembers,
			List<PatientMessage> patientMessages,
			List<Alerts> alertsList,
			List<Divert> divertList);
		void SaveLinkedMembers(List<LinkedMember> linkedMembers);
		void AddPatientMessage(PatientMessage patientMessage);
		void DeletePatientMessage(int patientId);
		void SaveKioskLogo(byte[] logo);
		void SetDivert(bool status, int sessionHolderId, int organisationId);
		void SetTPPDivert(bool status,int sessionHolderId, string LoginId, int organisationId);
		void DeleteKiosk();
        void DeleteKioskLogo();
        void UpdateMessage(string message);
	}
}
