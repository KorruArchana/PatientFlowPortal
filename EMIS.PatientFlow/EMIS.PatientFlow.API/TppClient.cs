using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.API.Enums;
using EMIS.PatientFlow.API.Utilities;

namespace EMIS.PatientFlow.API
{
	public sealed class TppClient : IDisposable
	{
		string ApiKey;
		string _deviceId;

		public TppClient(Credential credential)
		{
			ApiKey = credential.UserName;
			_deviceId = credential.Password;
		}

		public TppClient(string apiKey, string deviceID)
		{
			ApiKey = apiKey;
			_deviceId = deviceID;
		}

		public void Dispose()
		{
			ApiKey = string.Empty;
			_deviceId = string.Empty;
		}

		public GetAppointmentSlots.ClientIntegrationResponseResponseClinic[] GetAppointmentSlotsList(DateTime startDate, DateTime endDate)
		{
			GetAppointmentSlots.ClientIntegrationRequest request = new GetAppointmentSlots.ClientIntegrationRequest
			{
				APIKey = ApiKey,
				DeviceID = _deviceId,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID",
				FunctionParameters = new GetAppointmentSlots.ClientIntegrationRequestFunctionParameters()
				{
					MaxDateTime = endDate,
					MinDateTime = startDate
				}
			};
			string requeststring = Extension.Serialize(request);
			SocketConnection.ConnectToSocketlatest(requeststring);
			var result = Extension.Deserialize<GetAppointmentSlots.ClientIntegrationResponse>(SocketConnection.ConnectToSocketlatest(requeststring));
			GetAppointmentSlots.ClientIntegrationResponseResponseClinic[] results = result.Response;
			return results;
		}

		public string UpdatePatientAppointmentStatus(string NHSNumber, string[] AppointmentUID)
		{
			var appointments = new List<UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinicalAppointment>();

			foreach (var appointmentID in AppointmentUID)
			{
				var app = new UpdatePatientRecord.ClientIntegrationRequestFunctionParametersNonClinicalAppointment()
				{
					AppointmentUID = appointmentID,
					Status = "Arrived"
				};
				appointments.Add(app);
			}

			UpdatePatientRecord.ClientIntegrationRequest request = new UpdatePatientRecord.ClientIntegrationRequest
			{
				APIKey = ApiKey,
				DeviceID = _deviceId,
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
						Appointment = appointments.ToArray()
					}
				}
			};
			string requeststring = Extension.Serialize(request);
			return SocketConnection.ConnectToSocketlatest(requeststring);
		}

		public ApiResult<string> BookAppointment(string request)
		{
			string resultData = SocketConnection.ConnectToSocketlatest(request);
			var result = GetApiResult<string>(
			   ApiMethod.BookAppointment,
			   resultData);

			return result;
		}

		public ApiResult<GetOrganisation.ClientIntegrationResponse> GetOrganisation(string request, string webServiceUrl)
		{
			string resultData = SocketConnection.ConnectToSocketlatest(request);

			var result = GetApiResult<GetOrganisation.ClientIntegrationResponse>(
			   ApiMethod.GetOrganisation,
			   resultData);

			return result;
		}

		public GetOrganisation.ClientIntegrationResponseResponse[] GetOrganisation()
		{
			GetOrganisation.ClientIntegrationRequest request = new GetOrganisation.ClientIntegrationRequest
			{
				APIKey = ApiKey,
				DeviceID = _deviceId,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID",
				FunctionParameters = new GetOrganisation.ClientIntegrationRequestFunctionParameters()
			};
			string requeststring = Extension.Serialize(request);
			string res = SocketConnection.ConnectToSocketlatest(requeststring);

			var result = Extension.Deserialize<GetOrganisation.ClientIntegrationResponse>(res);
			GetOrganisation.ClientIntegrationResponseResponse[] results = result.Response;
			return results;
		}

		public PatientMatches GetPatientRecord(string filter, string gender)
		{
			PatientSearch.ClientIntegrationRequest request = new PatientSearch.ClientIntegrationRequest
			{
				APIKey = ApiKey,
				DeviceID = _deviceId,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID"

			};

			string[] arrayOfFilter = filter.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
			var dateofBirth = arrayOfFilter[0];
			var surname = arrayOfFilter[1];

			request.FunctionParameters = new PatientSearch.ClientIntegrationRequestFunctionParameters();
			request.FunctionParameters.Surname = surname;
			request.FunctionParameters.DateOfBirth = Convert.ToDateTime(dateofBirth);

			if (gender == "M")
				request.FunctionParameters.Gender = PatientSearch.ClientIntegrationRequestFunctionParametersGender.M;
			else if (gender == "F")
				request.FunctionParameters.Gender = PatientSearch.ClientIntegrationRequestFunctionParametersGender.F;
			else
				request.FunctionParameters.Gender = PatientSearch.ClientIntegrationRequestFunctionParametersGender.None;

			string requeststring = Extension.Serialize(request);
			string res = SocketConnection.ConnectToSocketlatest(requeststring);

			var result = Extension.Deserialize<PatientSearch.ClientIntegrationResponse>(res);

			//PatientSearch.ClientIntegrationResponseResponseResult[] results = result.Response;

			PatientMatches patientMatch = new PatientMatches();
			List<PatientMatchesPatient> patientMatchList = new List<PatientMatchesPatient>();
			int patientIdentifier = 50000;
			foreach (var patient in result.Response)
			{
				PatientMatchesPatient patientMatchPatient = new PatientMatchesPatient();
				patientMatchPatient.FirstNames = patient.FirstName;
				patientMatchPatient.Sex = patient.Sex;
				patientMatchPatient.CallingName = patient.FirstName;
				patientMatchPatient.NhsNumber = patient.Identity[0].NHSNumber;
				patientMatchPatient.DBID = patientIdentifier.ToString();
				patientMatchPatient.DateOfBirth = patient.DateOfBirth;
				patientMatchPatient.FamilyName = patient.Surname;
				patientMatchPatient.Address = new AddressType { PostCode = patient.Address.First().Postcode };

				patientMatchList.Add(patientMatchPatient);
                patientIdentifier++;
			}

			patientMatch.PatientList = patientMatchList.ToArray();

			return patientMatch;
		}

		public GetDairy.ClientIntegrationResponseResponseResult[] GetTodaysAppointments()
		{
			GetDiary.ClientIntegrationRequest request = new GetDiary.ClientIntegrationRequest
			{
				APIKey = ApiKey,
				DeviceID = _deviceId,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID",
				FunctionParameters = new GetDiary.ClientIntegrationRequestFunctionParameters()
				{
					MaxDateTime = DateTime.Today.AddDays(1).AddMinutes(-1),
					MinDateTime = DateTime.Today
				}
			};
			string requeststring = Extension.Serialize(request);
			string res = SocketConnection.ConnectToSocketlatest(requeststring);

			var result = Extension.Deserialize<GetDairy.ClientIntegrationResponse>(res);
			GetDairy.ClientIntegrationResponseResponseResult[] results = result.Response;

			return results;
		}

		public ApiResult<string> SavePatientSurvey(string request)
		{
			string resultData = SocketConnection.ConnectToSocketlatest(request);
			var result = GetApiResult<string>(
			   ApiMethod.FileRecord,
			   resultData);

			return result;
		}

		private ApiResult<T> GetApiResult<T>(
		   ApiMethod method,
		   string resultdata)
		{
			var result = new ApiResult<T>();

			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(resultdata);

			if (method == ApiMethod.BookAppointment)
			{
				string appUID = ValidateElementName(xdoc, "AppointmentUID");
				string error = ValidateElementName(xdoc, "ErrorMessage");

				bool hasError = !string.IsNullOrWhiteSpace(error);
				bool hasAppId = !string.IsNullOrWhiteSpace(appUID);

				result.Error = hasError ? error : (hasAppId ? string.Empty : "Failed");
			}
			else
			{
				result.Error = ValidateElementName(xdoc, "ErrorMessage");
			}

			result.IsSuccess = string.IsNullOrWhiteSpace(result.Error) ? true : false;
			result.Outcome = string.IsNullOrWhiteSpace(result.Error) ? 1 : 0;
			result.Result = Extension.Deserialize<T>(resultdata);

			return result;
		}

		private static string ValidateElementName(XmlDocument xdoc, string elementName)
		{
			if (xdoc.GetElementsByTagName(elementName).Count > 0)
			{
				return xdoc.GetElementsByTagName(elementName)[0].InnerText;
			}

			return "";
		}

		public List<GetPatientRecordDetails.ClientIntegrationResponseResponse> GetPatientRecordDetails(List<string> nhsstring)
		{
			List<GetPatientRecordDetails.ClientIntegrationResponseResponse> results = new List<GetPatientRecordDetails.ClientIntegrationResponseResponse>();

			foreach (var nhs in nhsstring)
			{
				GetPatientRecordDetails.ClientIntegrationRequest request = new GetPatientRecordDetails.ClientIntegrationRequest
				{
					APIKey = ApiKey,
					DeviceID = _deviceId,
					DeviceVersion = "1.0",
					RequestUID = "@RequestUID",
					FunctionParameters = new GetPatientRecordDetails.ClientIntegrationRequestFunctionParameters() {
						Identity = new GetPatientRecordDetails.ClientIntegrationRequestFunctionParametersIdentity() { NHSNumber = nhs }, Filter = new GetPatientRecordDetails.ClientIntegrationRequestFunctionParametersFilter() { Demographics = new GetPatientRecordDetails.ClientIntegrationRequestFunctionParametersFilterDemographics() }  },
					
				};
				string requeststring = Extension.Serialize(request);
				string res = SocketConnection.ConnectToSocketlatest(requeststring);

				var result = Extension.Deserialize<GetPatientRecordDetails.ClientIntegrationResponse>(res);

				results.AddRange(result.Response);
			}		
			
			return results;
		}
	}
}
