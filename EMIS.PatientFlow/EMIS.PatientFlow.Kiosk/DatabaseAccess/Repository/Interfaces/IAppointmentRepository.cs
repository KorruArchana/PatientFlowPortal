using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.Model;
using EMIS.PatientFlow.Common.Enums;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
    interface IAppointmentRepository
    {
        List<Appointment> GetMatchingAppointments(List<Appointment> syncedAppointments, bool isSynced);
        void UpdateAppointmentStatus(int appointmentId);
		List<DoctorDetailsBooking> GetAppointmentSessions(DateTime date, string slotTypeId);
        void SaveAppointmentSessions(List<AppointmentSession> appointmentSessions, DateTime selectedDate);
        bool IsPatientNewsletterSubscribed(int patientId);
        void UpdateNewsletterSubscription();
        void SaveAppointments(List<Appointment> appointments, DateTime fromDate,SystemType systemType, Boolean status, Boolean isUntimed);
        void SaveTPPAppointments(List<Appointment> appointments, DateTime fromDate,SystemType systemType, Boolean status, Boolean isUntimed);
		List<Int64> GetAppointmentsStatus(int patientId, int organisationId);
	}
}
