using System;
using System.Collections.Generic;
using System.Linq;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.API.Enums;
using EMIS.PatientFlow.API.Utilities;

namespace EMIS.PatientFlow.API
{
	public sealed class WebClient : IDisposable
    {
        readonly EM_PACC.IPatientAccess_20100519 _client = new EM_PACC.PatientAccess_20100519Class();

        private readonly string _sessionId;
        private readonly string _loginId;
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
        }

        public WebClient(Credential credential)
        {
            List<System.ComponentModel.DataAnnotations.ValidationResult> results;

            Validation.ArgumentIsNull(credential, "credential");

            //Parameter validation
            if (!Validation.TryValidate(credential, out results))
                throw new ArgumentException(string.Join(",", results.Select(x => x.ErrorMessage).ToArray()));

            //login webclient
            if (!GetLoginId(credential.IpAddress, credential.OrganisationKey, credential.SupplierId, out _loginId, out _message))
                throw new ApplicationException(_message);
            //login api
            if (!GetSessionId(credential.UserName, credential.Password, out _sessionId, out _message))
                throw new ApplicationException(_message);
        }

        private bool GetSessionId(
            string userName,
            string password,
            out string sessionId,
            out string message)
        {
            object error, outcome, id;
         
            _client.Logon(_loginId, userName, password, out id, out error, out outcome);

            sessionId = Convert.ToString(id);

            message = GetOutcomeMessage(ApiMethod.Initialise, Convert.ToInt32(outcome));

            message += ";" + Convert.ToString(error);

            return Convert.ToInt32(outcome) == 1;
        }

        private bool GetLoginId(string ip, string organisationId, string supplierId, out string loginId, out string message)
        {
            object cdb, productName, version, userLoginId, error, outcome, id;

            _client.InitializeWithID(
                2,
                ip,
                "LV",
                "LV",
                organisationId,
                supplierId,
                out cdb,
                out productName,
                out version,
                out userLoginId,
                out error,
                out outcome,
                out id);

            loginId = Convert.ToString(userLoginId);

            message = GetOutcomeMessage(ApiMethod.Logon, Convert.ToInt32(outcome));

            message += ";" + Convert.ToString(error);

            return Convert.ToInt32(outcome) == 1;
        }

        private string GetOutcomeMessage(ApiMethod method, int outcome)
        {
            return outcome == -1 ? ResultOutcome.MessageList[(int)method][0] : ResultOutcome.MessageList[(int)method][outcome];
        }

        private ApiResult<T> GetApiResult<T>(
            ApiMethod method,
            string data,
            int outcome,
            string error)
        {
            var result = new ApiResult<T>();

            result.IsSuccess = Convert.ToInt32(outcome) == 1;
            result.Outcome = outcome;
            result.Error = error;

            result.Result = Convert.ToString(data).ConvertFromXmlString<T>();

            _message = GetOutcomeMessage(method, Convert.ToInt32(outcome));

            _message += ";" + Convert.ToString(error);

            return result;
        }

        public ApiResult<BookedPatients> GetBookedPatients(
            int within,
            int before)
        {
            object error, outcome, patients;
            _client.GetBookedPatients(_sessionId, within, before, out patients, out error, out outcome);

            var result = GetApiResult<BookedPatients>(
                ApiMethod.GetBookedPatients,
                Convert.ToString(patients),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<PatientMatches> GetMatchedPatients(string filter)
        {
            object error, outcome, patients;

			string[] arrayOfFilter = filter.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
			filter = String.Join(" ", arrayOfFilter);

			_client.GetMatchedPatient(_sessionId, filter, out patients, out error, out outcome);

            var result = GetApiResult<PatientMatches>(
                ApiMethod.GetMatchedPatient,
                Convert.ToString(patients),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<AppointmentSessionList> GetAppointmentSessions(string startDate, string endDate, string slotType)
        {
            object error, outcome, sessions;

            _client.GetAppointmentSessions(_sessionId, 0, slotType, startDate, endDate, out sessions, out error, out outcome);

            var result = GetApiResult<AppointmentSessionList>(
                ApiMethod.GetAppointmentSessions,
                Convert.ToString(sessions),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<SlotList> GetSlotForSessions(int appointmentSessionId)
        {
            object error, outcome, slots;

            _client.GetSlotsForSession(_sessionId, appointmentSessionId, out slots, out error, out outcome);

            var result = GetApiResult<SlotList>(
                ApiMethod.GetSlotForSessions,
                Convert.ToString(slots),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<BookedAppointment> BookAppointment(Appointment appointment)
        {
            object error, outcome, bookedAppointment;

            _client.BookAppointment(
                _sessionId,
                appointment.SlotId,
                appointment.PatientId,
                appointment.Reason,
                appointment.BookedBy,
                appointment.BookingNote,
                out bookedAppointment,
                out error,
                out outcome);

            var result = GetApiResult<BookedAppointment>(
                ApiMethod.BookAppointment,
                Convert.ToString(bookedAppointment),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<string> SetAppointmentStatus(int appointmentId, AppointmentStatus status)
        {
            object error, outcome;

            _client.SetAppointmentStatus(_sessionId, appointmentId, (int)status, out error, out outcome);

            var result = GetApiResult<string>(
                ApiMethod.SetAppointmentStatus,
                string.Empty,
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<OrganisationInformation> GetOrganisation()
        {
            object error, outcome, organisation;

            _client.GetOrganisation(_sessionId, out organisation, out error, out outcome);

            var result = GetApiResult<OrganisationInformation>(
                ApiMethod.GetOrganisation,
                Convert.ToString(organisation),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }
    
        public ApiResult<UserDetails> GetCurrentUser()
        {
            object error, outcome, userDetails;

            _client.GetCurrentUser(_sessionId, out userDetails, out error, out outcome);

            var result = GetApiResult<UserDetails>(
                ApiMethod.GetCurrentUser,
                Convert.ToString(userDetails),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

        public ApiResult<AppointmentConfiguration> GetAppointmentConfiguration(string startDate, string endDate)
        {
            object error, outcome, appointmentConfiguration;

            _client.GetAppointmentConfiguration(
                _sessionId,
                startDate,
                endDate,
                out appointmentConfiguration,
                out error,
                out outcome);

            var result = GetApiResult<AppointmentConfiguration>(
                ApiMethod.GetAppointmentConfiguration,
                Convert.ToString(appointmentConfiguration),
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

		public ApiResult<BookedPatients> GetSentInPatients(int within, int before)
		{
			object error, outcome, patients;
			_client.GetSentIn(_sessionId, within, before,0, out patients, out error, out outcome);

			var result = GetApiResult<BookedPatients>(
				ApiMethod.GetBookedPatients,
				Convert.ToString(patients),
				Convert.ToInt32(outcome),
				Convert.ToString(error));

			return result;
		}

		public ApiResult<BookedPatients> GetArrivedPatients(int within, int before)
		{
			object error, outcome, patients;
			_client.GetArrived(_sessionId, within, before, 0, out patients, out error, out outcome);

			var result = GetApiResult<BookedPatients>(
				ApiMethod.GetBookedPatients,
				Convert.ToString(patients),
				Convert.ToInt32(outcome),
				Convert.ToString(error));

			return result;
		}

		public ApiResult<string> SavePatientSurvey(string accessPassword, string patientId, string medicalRecord, string attachmentPath)
        {
            object error, outcome;

            _client.FileRecord(
                _sessionId,
                accessPassword,
                patientId,
                medicalRecord,
                attachmentPath,
                out error,
                out outcome);

            var result = GetApiResult<string>(
                ApiMethod.FileRecord,
                string.Empty,
                Convert.ToInt32(outcome),
                Convert.ToString(error));

            return result;
        }

		public ApiResult<EMISWebPA.PatientAppointmentList> GetPatientAppointments(string patientId)
		{
			object error, outcome, patientAppointments;
			string startDate = DateTime.Today.ToString();
			string endDate = DateTime.Today.AddDays(1).AddMinutes(-1).ToString();

			_client.GetPatientAppointments(_sessionId, patientId, startDate, endDate, out patientAppointments, out error, out outcome);

			var result = GetApiResult<EMISWebPA.PatientAppointmentList>(
			Enums.ApiMethod.GetPatientAppointments,
			Convert.ToString(patientAppointments),
			Convert.ToInt32(outcome),
			Convert.ToString(error));
		
			return result;
		}

        public void Dispose()
        {
            _client.Logout();
        }
    }
}
