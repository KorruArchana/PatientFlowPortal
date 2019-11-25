using System;
using System.Collections.Generic;
using System.Linq;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	// check this class now. later we can move all items related to appointments from APIhelper to appointment help and move to file
	public static class AppointmentHelper
	{
		private static readonly IConfigurationRepository ConfigRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
		private static readonly string KioskId = Utilities.GetAppSettingValue("RegistrationKey");

		public static List<SlotWeekCalender> GetWeekAvailability(DateTime date)
		{
			DateTime startdate = GetMondayStartDate(date);
			var weekenddate = startdate.AddDays(6);

			return GetSessionAvailability(startdate, weekenddate);
		}

		public static List<SlotWeekCalender> GetMonthAvailability(int year, int month)
		{
			var monthFirstDate = new DateTime(year, month, 1);
			var monthendDate = monthFirstDate.AddMonths(1).AddDays(-1);

			return GetSessionAvailability(monthFirstDate, monthendDate);
		}

		private static List<SlotWeekCalender> GetSessionAvailability(DateTime date, DateTime endDate)
		{
			var apiHelper = new ApiHelper();
			var monthCalender = new List<SlotWeekCalender>();
			
			try
			{
				var slotType = (GlobalVariables.SelectedSlotType == null) ? string.Empty : GlobalVariables.SelectedSlotType.SlotTypeId;
				var credential = ConfigRepository.GetPatientFlowUser();

				List<AppointmentSession> allSessionHolderList = new List<AppointmentSession>();
				if (credential.SystemType == SystemType.TPPSystmOne)
				{
						using (TppClient client = new TppClient(credential))
						{
						GetAppointmentSlots.ClientIntegrationResponseResponseClinic[] response = client.GetAppointmentSlotsList(date, endDate);

						foreach (GetAppointmentSlots.ClientIntegrationResponseResponseClinic appSession in response)
						{
							AppointmentSession appSess = new AppointmentSession();

							var firstSlot = appSession.Slot.Where(s => DateTime.Now < DateTime.Parse(s.DateTimeStart)).FirstOrDefault();
							if(firstSlot != null)
								appSess.Date = firstSlot.DateTimeStart;

							appSess.SiteName = appSession.SiteName;
							appSess.Member = new Member { FirstName = appSession.UserName };

							appSess.AvaiableSlots = appSession.Slot.Where(i => DateTime.Parse(i.DateTimeStart) > DateTime.Now).Count();
							allSessionHolderList.Add(appSess);
						}
					}
				}
				else
				{
					allSessionHolderList = apiHelper.GetAllSessionHolderList(credential, date, endDate, slotType);
				}
				
				int days = (endDate - date).Days + 1;

				var sessionList = new List<AppointmentSession>();
				if (string.IsNullOrEmpty(GlobalVariables.BookAppointmentSettings.SelectedMemberList))
					sessionList = allSessionHolderList;
				else
				{
					var memberList = GlobalVariables.BookAppointmentSettings.SelectedMemberList.Split(',').Select(int.Parse).ToList();
					sessionList.AddRange(memberList.SelectMany(id => allSessionHolderList.Where(session => session.Member.Id == id)));
				}

				sessionList = apiHelper.MatchSiteData(sessionList);
				sessionList = apiHelper.MatchMemberData(sessionList);

				var week = new SlotWeekCalender();
				for (var i = 0; i < days; i++)
				{
					var date1 = date.AddDays(i).Date;
					var slotsCount = 0;

					if (date1 > DateTime.Today)
					{
						if (credential.SystemType == SystemType.TPPSystmOne)
						{
							slotsCount = sessionList.Where(session => session.Date != null  &&
							date1 == DateTime.Parse(session.Date).Date).Sum(session => session.AvaiableSlots);
						}
						else
						{
							slotsCount = sessionList.Where(session => date1 == DateTime.Parse(session.Date)).Sum(session => session.AvaiableSlots);
						}
					}
					else if (date1 == DateTime.Today)
					{
						if (credential.SystemType == SystemType.TPPSystmOne)
						{
							slotsCount = sessionList.Where(session => session.Date != null 
							&& DateTime.Now < DateTime.Parse(session.Date)).Sum(session => session.AvaiableSlots);
						}
						else
						{
							slotsCount = sessionList.Where(session => date1 == DateTime.Parse(session.Date) && DateTime.Now.TimeOfDay < TimeSpan.Parse(session.EndTime)).Sum(session => session.AvaiableSlots);
						}
					}

					week.SetDateRow(date1, slotsCount);

					if (date1.DayOfWeek == DayOfWeek.Sunday)
					{
						monthCalender.Add(week);
						week = new SlotWeekCalender();
					}
					if (i + 1 != days) continue;
					monthCalender.Add(week);
					week = new SlotWeekCalender();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			return monthCalender;
		}

		private static DateTime GetMondayStartDate(DateTime currentDate)
		{
			const int day = (int)DayOfWeek.Monday;
			var currentday = (int)currentDate.DayOfWeek;
			return currentday != 0 ? currentDate.AddDays(-currentday + day) : currentDate.AddDays(-6);
		}

	}

}
