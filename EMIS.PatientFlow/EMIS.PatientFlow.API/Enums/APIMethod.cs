namespace EMIS.PatientFlow.API.Enums
{
    internal enum ApiMethod
    {
        Initialise,
        Logon,
        GetBookedPatients,
        GetMatchedPatient,
        GetAppointmentSessions,
        GetSlotForSessions,
        BookAppointment,
        SetAppointmentStatus,
        GetOrganisation,
        GetCurrentUser,
        GetAppointmentConfiguration,
        FileRecord,
		GetPatientAppointments
    }
}
