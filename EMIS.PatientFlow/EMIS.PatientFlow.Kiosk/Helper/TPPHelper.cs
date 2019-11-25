using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.API.Utilities;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public class TPPHelper
	{
		private readonly IConfigurationRepository _configRepository;
		private readonly string _kioskId = Utilities.GetAppSettingValue("RegistrationKey");

		public TPPHelper()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
		}

		public string TPPPatientMedicalRecord(string nhsNumber, List<Questions> questions, Credential credential)
		{
			var clinicalCodes = new List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCode>();
			var eventNarratives = new List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventNarrative>();

			questions = questions.Where(a => a.SelectedAnswer != Constants.SkippedAnswer).ToList();

			int count1 = questions.Count(d => (d.AnswerControlType == AnswerControlType.Textbox.ToString() ||
												d.AnswerControlType == AnswerControlType.NumericTextBox.ToString() ||
												d.SelectedAnswer == Constants.SkippedAnswer ||
												d.SelectedAnswer == Constants.NotAnswered));
			int count2 = questions.Sum(d => d.AnswerOptions.Count(e => e.IsChecked));
			int totalQnCount = count1 + count2;

			int consultationIndex = 0, questionIndex = 0;
			while (consultationIndex < totalQnCount)
			{
				if (questions[questionIndex].AnswerOptions.Any(d => d.IsChecked))
				{
					foreach (var userOptions in questions[questionIndex].AnswerOptions.Where(d => d.IsChecked))
					{
						string optionCode = userOptions.OptionCode;
						long snomedCode = userOptions.SnomedCode;
						if (!string.IsNullOrEmpty(optionCode))
						{
							SetCodeTermSchemeToClinicalCode(clinicalCodes, userOptions.AnswerOptionText, optionCode, null);
						}
						else if (snomedCode > 0)
						{
							SetCodeTermSchemeToClinicalCode(clinicalCodes, userOptions.AnswerOptionText, null, snomedCode.ToString());
						}
						else
						{
							string displayTerm = questions[questionIndex].QuestionText + " " + userOptions.AnswerOptionText;
							SetNarrativesToClinicalEvent(eventNarratives, displayTerm);
						}
						consultationIndex++;
					}
					questionIndex++;
				}
				else
				{
					string displayTerm = questions[questionIndex].QuestionText + " " + questions[questionIndex].SelectedAnswer;
					SetNarrativesToClinicalEvent(eventNarratives, displayTerm);

					consultationIndex++; questionIndex++;
				}
			}

			UpdatePatientRecord.ClientIntegrationRequest request = CreateClientIntegrationRequest(nhsNumber, credential, clinicalCodes, eventNarratives);

			string requeststring = Extension.Serialize(request);
			return requeststring;
		}

		public string TPPBookAppointment(string NHSNumber, DateTime slotdatetime, API.Data.Appointment slots, Credential credential)
		{
			UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinicalAppointment appointment = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinicalAppointment()
			{
				Comments = slots.Reason,
				Flag = "Booked via Touch Screen",
				Clinic = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinicalAppointmentClinic()
				{
					Slot = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinicalAppointmentSlot()
					{
						SlotType = slots.SlotName,
						DateTimeStart = slotdatetime,
						Duration = Convert.ToString(slots.Duration)
					},
					UserName = slots.UserName,
					SiteName = slots.SiteName,
					ClinicType = slots.ClinicType
				}
			};

			UpdatePatientRecord.ClientIntegrationRequest request = new UpdatePatientRecord.ClientIntegrationRequest
			{
				APIKey = credential.UserName,
				DeviceID = credential.Password,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID",
				FunctionParameters = new UpdatePatientRecord.ClientIntegrationRequestFunctionParameters()
				{
					Identity = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersIdentity()
					{
						NHSNumber = NHSNumber
					},
					NonClinical = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinical()
					{
						Appointment = new[]
						{
							appointment
						}
					}
				}
			};

			string requeststring = Extension.Serialize(request);
			return requeststring;
		}

		public List<AppointmentSession> GetSlots(DateTime date)
		{
			Credential credential = _configRepository.GetPatientFlowUser();

			List<AppointmentSession> sessionDetails = new List<AppointmentSession>();

			using (TppClient client = new TppClient(credential))
			{
				GetAppointmentSlots.ClientIntegrationResponseResponseClinic[] response = client.GetAppointmentSlotsList(date, date.AddDays(1).AddMinutes(-1));
				var tppSession = new List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic>();
				if (string.IsNullOrEmpty(GlobalVariables.BookAppointmentSettings.SelectedMemberList))
				{
					tppSession = response.ToList();
				}
				else
				{
					tppSession = response.ToList();
				}
                tppSession = GetMatchTppSite(tppSession);
                tppSession = GetMatchMember(tppSession);

				int slotId = 100;
				var organisation = client.GetOrganisation();
				
				foreach (var appSess in tppSession)
				{
					AppointmentSession sessDetails = new AppointmentSession();
					sessDetails.SiteName = appSess.SiteName;
					sessDetails.ClinicType = appSess.ClinicType;
					sessDetails.Member = new Member { FirstName = organisation[0].User.Where(x => x.UserName == appSess.UserName).Select(x => x.FirstName + " " + x.Surname).FirstOrDefault() ?? appSess.UserName, };
					sessDetails.AppointmentSlotsList = new List<AppointmentSlots>();

					foreach (var sessSlot in appSess.Slot)
					{
						if (DateTime.Now < DateTime.Parse(sessSlot.DateTimeStart) && IsTPPSlotAvailable(sessSlot.SlotType))
						{
							AppointmentSlots slot = new AppointmentSlots
							{
								StartDateTime = sessSlot.DateTimeStart,
								ClinicType = appSess.ClinicType,
								SlotId = slotId,
								DoctorName = organisation[0].User.Where(x => x.UserName == appSess.UserName).Select(x => x.FirstName + " " + x.Surname).FirstOrDefault() ?? appSess.UserName,
								SlotStatus = true
							};
							slot.StartDateTime = sessSlot.DateTimeStart;
							slot.SlotLength = Convert.ToInt32(sessSlot.Duration);
							slot.SlotTypeDescription = sessSlot.SlotType;
							slot.SiteName = appSess.SiteName;
							slot.UserName = appSess.UserName;
							sessDetails.AppointmentSlotsList.Add(slot);

                            slotId++;
						}
					}

					sessionDetails.Add(sessDetails);
				}
			}
			return sessionDetails;
		}

        private static bool IsTPPSlotAvailable(string slotType)
        {
			if (GlobalVariables.SlotTypeList == null || GlobalVariables.SlotTypeList.Count() == 0)
				return true;

			return GlobalVariables.SlotTypeList != null
				&& slotType != string.Empty
				&& GlobalVariables.SlotTypeList.Any(i => i.Description == slotType);
        }

        internal List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> GetMatchTppSite(List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> tppSlots)
        {
            var siteName = GlobalVariables.SelectedOrganisation.SiteName;
            return !string.IsNullOrEmpty(siteName) ? tppSlots.Where(x => x.SiteName == siteName).ToList() : tppSlots;
        }

        internal List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> GetMatchMember(List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> tppSlots)
		{
			List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> tppSlotList = new List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic>();
			try
			{
				var memberList = _configRepository.GetKioskConfiguration<List<LinkedMember>>(KioskConfigType.LinkedMember.ToString());
				if (memberList != null && memberList.Count > 0)
				{
					memberList = memberList.Where(linkedMember => linkedMember.OrganisationId.ToString().Equals(
							GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();
					foreach (var item in memberList)
					{
						tppSlotList.AddRange(tppSlots.Where(x => x.UserName == item.LoginId));
					}
					tppSlots = tppSlotList;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}
			return tppSlots;

		}

		private static void SetCodeTermSchemeToClinicalCode(
			List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCode> clinicalCodes, 
			string answerOptionText, string optionCode, string snomedCode)
		{
			var clinicalCode = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCode()
			{
				FreeText = answerOptionText,
				Code = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCodeCode()
				{
					Scheme = snomedCode == null ? UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCodeCodeScheme.CTV3 :
												  UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCodeCodeScheme.Snomed,
					Code = snomedCode == null ? optionCode : snomedCode,
				}
			};
			clinicalCodes.Add(clinicalCode);
		}

		private static UpdatePatientRecord.ClientIntegrationRequest CreateClientIntegrationRequest(
			string nhsNumber, Credential credential,
			List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventClinicalCode> clinicalCodes,
			List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventNarrative> eventNarratives)
		{
			return new UpdatePatientRecord.ClientIntegrationRequest
			{
				APIKey = credential.UserName,
				DeviceID = credential.Password,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID",
				FunctionParameters = new UpdatePatientRecord.ClientIntegrationRequestFunctionParameters()
				{
					Identity = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersIdentity()
					{
						NHSNumber = nhsNumber
					},
					Clinical = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinical()
					{
						Event = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEvent()
						{
							ClinicalCode = clinicalCodes.ToArray(),
							Narrative = eventNarratives.ToArray()
						}
					},
				
				}
			};
		}

		private static void SetNarrativesToClinicalEvent(List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventNarrative> eventNarratives, string displayTerm)
		{
			var eventNarrative = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersClinicalEventNarrative()
			{
				Line = displayTerm.Length > 200 ? displayTerm.Substring(0, 200) : displayTerm,
			};
			eventNarratives.Add(eventNarrative);
		}

		internal List<GetPatientRecordDetails.ClientIntegrationResponseResponse> GetPatientRecordDetails(TppClient client,List<string> nhsstring)
		{
			return client.GetPatientRecordDetails(nhsstring);
		}
	}
}
