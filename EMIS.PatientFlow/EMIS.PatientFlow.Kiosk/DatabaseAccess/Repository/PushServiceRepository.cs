using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
	public class PushServiceRepository : BaseRepository, IPushServiceRepository
	{
		public void UpdateKioskStatus(int status)
		{
			DbAccess.UpdateKioskStatus(status);
		}

		public void UpdateKioskConfig(Model.Kiosk kiosk)
		{
			UpdateKioskConfiguration(kiosk);

			if (kiosk.KioskSiteMapList != null)
			{
				DbAccess.SaveKioskSiteMap(kiosk.KioskSiteMapList);
			}

			if (kiosk.SelectedQuestionnaires != null)
			{
				if (SaveQuestionnaire(kiosk.QuestionnaireList))
					DbAccess.UpdateQuestionnaire(kiosk.SelectedQuestionnaires);
			}
		}

		private void UpdateKioskConfiguration(Model.Kiosk kiosk)
		{
			if (kiosk != null)
			{
				var kioskSettings = new KioskSettings
				{
					Title = kiosk.Title,
					ScreenTimeOut = kiosk.ScreenTimeOut,
					DisplayLanguageField = true,
					ShowDateTime = true,
					ShowSysInfo = true,
					AdminPassword = kiosk.AdminPassword,
					AppointmentReason = kiosk.AppointmentReason
				};
				var kioskArrival = new KioskArrival
				{
					EarlyArrival = kiosk.EarlyArrival,
					LateArrival = kiosk.LateArrival,
					//DelayInMinutes = kiosk.DelayInMinutes,
					AutoConfirmArrival = kiosk.AutoConfirmArrival,
					ForceSurvey = kiosk.ForceSurvey,
					SkipSurveyQuestion = kiosk.SkipSurveyQuestion,
					QOFKioskUser = kiosk.FileasKioskUser,
					ShowDoctorDelay = kiosk.ShowDoctorDelay,
					AutoConfirmMultipleArrival = kiosk.AutoConfirmMultipleArrival,
					AllowUntimed = kiosk.AllowUntimed
				};
				var message = new Message
				{
					GeneralMessage = string.Empty,
					GeneralMessageDuration = 0,
					PatientMatchTitle = kiosk.PatientMatchTitle,
					AppointmentMatchTitle = kiosk.AppointmentMatchTitle
				};
				var bookingAppointment = new BookingAppointment
				{
					AppointmentReason = kiosk.AppointmentReason,
					SelectedMemberList = kiosk.SelectedMemberList,
				};
				var userDemographicDetails = new UserDemographicSetting
				{
					ShowDetails = kiosk.ShowDemographicDetails,
					Duration = kiosk.DemographicDetailsDuration,
					UserDemographicLists = kiosk.SelectedDemographicDetailsList,
					ScrambleDemographicDetails = kiosk.ScrambleDemographicDetails
				};
				DbAccess.UpdateKioskConfig(kiosk, kioskSettings, kioskArrival, message, bookingAppointment, userDemographicDetails);
			}
		}

		public void SaveKioskLogo(byte[] logo)
		{
			DbAccess.SaveKioskLogo(logo);
		}

		public void UpdateOrganisation(Organisation organisation)
		{
			DbAccess.UpdateOrganisation(organisation);
		}

		public void UpdateQuestionnaire(List<int> questionnaireId)
		{
			DbAccess.UpdateQuestionnaire(questionnaireId);
		}

		public void SaveAdditionalInformation(
			List<LinkedMember> linkedMembers,
			List<PatientMessage> patientMessages,
			List<Alerts> alertsList,
			List<Divert> divertList)
		{
			DbAccess.SaveAdditionalInformation(linkedMembers, patientMessages, alertsList, divertList);
		}

		public void SaveLinkedMembers(List<LinkedMember> linkedMembers)
		{
			DbAccess.SaveLinkedMembers(linkedMembers);
		}

		public void AddPatientMessage(PatientMessage patientMessage)
		{
			DbAccess.SavePatientMessage(patientMessage);
		}

		public void DeletePatientMessage(int patientId)
		{
			DbAccess.DeletePatientMessage(patientId);
		}

		public void SetDivert(bool status, int sessionHolderId, int organisationId)
		{
			DbAccess.SetDivert(status, sessionHolderId, organisationId);
		}

		public void SetTPPDivert(bool status, int sessionHolderId, string LoginId, int organisationId)
		{
			DbAccess.SetTppDivert(status, sessionHolderId, LoginId, organisationId);
		}

		public void DeleteKiosk()
		{
			DbAccess.DeleteKiosk();
		}
        public void DeleteKioskLogo()
        {
            DbAccess.DeleteKioskLogo();
        }

        private bool SaveQuestionnaire(List<Questionnaire> Questionnaires)
		{
			try
			{
				DbAccess.SaveQuestionnaireInitialData(Questionnaires, DateTime.UtcNow);
				foreach (var Questionnaire in Questionnaires)
				{
					DbAccess.SaveQuestionInitialData(Questionnaire.Questions);
					DbAccess.SaveQuestionOptionInitialData(Questionnaire.QuestionOptions);
				}
			}
			catch (Exception ex)
			{				
				return false;
			}

			return true;
		}

		public void UpdateMessage(string message)
		{
			DbAccess.UpdateMessage(message);
		}
	}
}
