using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;
using EMIS.PatientFlow.Common.Enums;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
        public List<Appointment> GetMatchingAppointments(List<Appointment> syncedAppointments, bool isSynced)
        {
            return DbAccess.GetMatchingAppointments(syncedAppointments, isSynced);
        }

        public void UpdateAppointmentStatus(int appointmentId)
        {
            DbAccess.UpdateAppointmentStatus(appointmentId);
        }

		public List<DoctorDetailsBooking> GetAppointmentSessions(DateTime date, string slotTypeId)
        {
            return DbAccess.GetAppointmentSessions(date, slotTypeId);
        }

        public void SaveAppointmentSessions(List<AppointmentSession> appointmentSessions, DateTime selectedDate)
        {
            DbAccess.SaveAppointmentSessions(appointmentSessions, selectedDate);
        }

        public bool IsPatientNewsletterSubscribed(int patientId)
        {
            return DbAccess.IsPatientNewsletterSubscribed(patientId);
        }

        public void UpdateNewsletterSubscription()
        {
            DbAccess.UpdateNewsletterSubscription();
        }

		public void SaveAppointments(List<Appointment> appointments, DateTime fromDate, 
			SystemType systemType, Boolean status, Boolean isUntimed)
        {
			DbAccess.SaveAppointments(appointments, fromDate, systemType, status, isUntimed);
        }

		public void SaveTPPAppointments(List<Appointment> appointments, DateTime fromDate,
			SystemType systemType, Boolean status, Boolean isUntimed)
		{
			DbAccess.SaveTPPAppointments(appointments, fromDate, systemType, status, isUntimed);
		}

		public List<Int64> GetAppointmentsStatus(int patientId, int organisationId)
		{
			return DbAccess.GetAppointmentsStatus(patientId, organisationId);
		}
	}
}
