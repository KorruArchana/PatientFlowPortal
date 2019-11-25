using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using EMIS.PatientFlow.API.Utilities;
using EMIS.PatientFlow.API.TopasServiceReference;

namespace EMIS.PatientFlow.API
{
	public sealed class TopasClient : IDisposable
	{
		private readonly KioskSoapClient _client;

		public TopasClient(Credential topasCredential)
		{
			List<System.ComponentModel.DataAnnotations.ValidationResult> results;

			Validation.ArgumentIsNull(topasCredential, "credential");

			//Parameter validation
			if (!Validation.TryValidate(topasCredential, out results))
				throw new ArgumentException(string.Join(",", results.Select(x => x.ErrorMessage).ToArray()));

			_client = new KioskSoapClient("KioskSoap", topasCredential.WebServiceUrl);
		}

		public ClinicDetail[] GetClinicSessions(string username, string password, string clinicset, DateTime startDateTime, DateTime endDateTime)
		{
			ClinicDetail[] clinicDetails = _client.GetClinicSessions(username, password, clinicset, startDateTime, endDateTime);

			return clinicDetails;
		}

		public AppointmentDetail[] GetClinicAppointments(string username, string password, string clinicset, DateTime startDateTime, DateTime endDateTime)
		{
			AppointmentDetail[] appointmentDetails = _client.GetClinicAppointments(username, password, clinicset, startDateTime, endDateTime);

			return appointmentDetails;
		}

		public void CheckIn(string username, string password, int slotID)
		{
			_client.CheckIn(username, password, slotID);
		}

	    public void Dispose()
	    {
	       _client.Close();
	    }
	}
}
