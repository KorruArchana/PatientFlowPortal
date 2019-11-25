using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Model;
using EMIS.PatientFlow.Kiosk.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public static class Utilities
	{
		private static readonly string KioskId = ConfigurationManager.AppSettings["RegistrationKey"];

		public static string GetAppSettingValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		public static void PatientMatchScreenNavigation(string screenCode)
		{
			try
			{
				
				if (AppPages.SelectSurname.GetDisplayName() == screenCode)
				{
					GlobalVariables.PatientMatchSurname = null;
					Messenger.Default.Send(AppPages.SelectSurname);
				}
				else if (AppPages.SelectGender.GetDisplayName() == screenCode)
				{
					GlobalVariables.PatientMatchGender = null;
					Messenger.Default.Send(AppPages.SelectGender);
				}

				else if (AppPages.SelectYear.GetDisplayName() == screenCode)
				{
					GlobalVariables.Year = null;
					Messenger.Default.Send(AppPages.SelectYear);
				}
				else if (AppPages.SelectDay.GetDisplayName() == screenCode)
				{
					GlobalVariables.Day = null;
					Messenger.Default.Send(AppPages.SelectDay);
				}
				else if (AppPages.SelectMonth.GetDisplayName() == screenCode)
				{
                    GlobalVariables.PatientMatchSelectedMonth = null;
					Messenger.Default.Send(AppPages.SelectMonth);
				}
				else if (screenCode == "DateofBirth")
				{
					Messenger.Default.Send(AppPages.SelectDayMonthYear);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		public static void PatientMatchBackwardNavigation()
		{
			try
			{
				if (GlobalVariables.SelectedPatientMatchOrder == 0)
				{
					Messenger.Default.Send(AppPages.SelectModule);
				}
				else
				{
					string screenCode = _pageOrder[GlobalVariables.SelectedPatientMatchOrder - 1];
					GlobalVariables.SelectedPatientMatchOrder--;
					PatientMatchScreenNavigation(screenCode);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		public static void PatientMatchForwardNavigation()
		{
			try
			{
				if (GlobalVariables.SelectedPatientMatchOrder + 1 < _pageOrder.Count)
				{
					string screenCode = _pageOrder[GlobalVariables.SelectedPatientMatchOrder + 1];
					GlobalVariables.SelectedPatientMatchOrder++;
					PatientMatchScreenNavigation(screenCode);
				}
				else if (GlobalVariables.SelectedPatientMatchOrder + 1 >= _pageOrder.Count)
				{
					GlobalVariables.SelectedPatientMatchOrder++;
					Messenger.Default.Send(GlobalVariables.IsArrive ? AppPages.ArrivalRouting : AppPages.SlotType);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		public static void MatchPatient()
		{
			try
			{
				if (GlobalVariables.IsArrive || GlobalVariables.SelectedPatientMatchOrder + 1 < _pageOrder.Count) return;
				string filter = GlobalVariables.PatientMatchDob != null ? GlobalVariables.PatientMatchDobFilter : null;
				if (!string.IsNullOrEmpty(GlobalVariables.PatientMatchSurname) 
                    && (GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.EmisWeb.GetDisplayName()))
                    || (GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.TPPSystmOne.GetDisplayName())))
				{
					filter = !string.IsNullOrEmpty(filter)
						? String.Format("{0}, {1}", filter, GlobalVariables.PatientMatchSurname) : GlobalVariables.PatientMatchSurname;
				}

				ApiHelper apiHelper = new ApiHelper();
				apiHelper.GetMatchedPatients(filter);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		public static void ClearPatientMatchingAppValues()
		{
			GlobalVariables.Day = null;
			GlobalVariables.Year = null;
			GlobalVariables.YearofBirth = 0;
			GlobalVariables.PatientMatches = new PatientMatches();
			GlobalVariables.PatientMatchesPatient = null;
			GlobalVariables.PatientMatchGender = null;
			GlobalVariables.PatientMatchSurname = null;
			GlobalVariables.PatientMatchDobFilter = null;
			GlobalVariables.PatientMatchSelectedMonth = "0";
			GlobalVariables.PatientMatchOrderList = null;
			GlobalVariables.PatientMatchDob = null;
			GlobalVariables.SelectedPatientMatchOrder = 0;
			GlobalVariables.ArrivedPatientAge = 0;
			GlobalVariables.CommandParameter = -1;
			ViewModelLocator.Cleanup(AppPages.SelectDay);
			ViewModelLocator.Cleanup(AppPages.SelectDobYear);
			ViewModelLocator.Cleanup(AppPages.SelectYear);
			ViewModelLocator.Cleanup(AppPages.FullDateOfBirthViewModel);
		}

		private static List<string> _pageOrder;
		public static void SetPatientMatchFirstPage(string configType)
		{
			try
			{
				ClearPatientMatchingAppValues();
				var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
				var patientMatchOrderList = configRepository.GetKioskConfiguration<List<PatientMatch>>(configType);
				GlobalVariables.PatientMatchOrderList = patientMatchOrderList;
				GlobalVariables.SelectedPatientMatchOrder = 0;
				GlobalVariables.IsMuliplePatientCheckDone = false;
				GlobalVariables.PatientMatchPinCode = null;
				GlobalVariables.ViewArrivalInfo = false;

				if (GlobalVariables.IsArrive)
				{
					var kioskArrival = configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
					GlobalVariables.KioskArrival = kioskArrival;
				}

				_pageOrder = new List<string>();

				foreach (var patientMatch in patientMatchOrderList)
				{
					if (patientMatch.ScreenCode == AppPages.SelectGender.GetDisplayName())
					{
						_pageOrder.Add(patientMatch.ScreenCode);
					}
					else if (patientMatch.ScreenCode == AppPages.SelectSurname.GetDisplayName())
					{
						_pageOrder.Add(patientMatch.ScreenCode);
					}
					else if (patientMatch.ScreenCode == AppPages.SelectYear.GetDisplayName())
					{
						_pageOrder.Add(patientMatch.ScreenCode);
					}
					else if (patientMatch.ScreenCode == AppPages.SelectDay.GetDisplayName())
					{
						_pageOrder.Add(patientMatch.ScreenCode);
					}
					else if (patientMatch.ScreenCode == AppPages.SelectMonth.GetDisplayName())
					{
						_pageOrder.Add(patientMatch.ScreenCode);
					}
					else if (patientMatch.ScreenCode == AppPages.SelectDobYear.GetDisplayName())
					{
						if (_pageOrder.Contains(AppPages.SelectDay.GetDisplayName()))
							_pageOrder.Remove(AppPages.SelectDay.GetDisplayName());
						if (_pageOrder.Contains(AppPages.SelectMonth.GetDisplayName()))
							_pageOrder.Remove(AppPages.SelectMonth.GetDisplayName());
						_pageOrder.Add("DateofBirth");
					}
				}

				PatientMatchScreenNavigation(_pageOrder.FirstOrDefault());
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		public static bool CheckQuestionByPatient(Questions question, int patientAge)
		{
			if (question.Gender == GlobalVariables.ArrivedPatientDetails.BookedPatient.Gender || question.Gender == Constants.GenderNone)
			{
				switch (question.Operation)
				{
					case AgeOperations.LessThan:
						if (patientAge <= question.Age1)
							return true;
						break;
					case AgeOperations.GreaterThan:
						if (patientAge >= question.Age1)
							return true;
						break;
					case AgeOperations.Between:
						if ((patientAge >= question.Age1 && patientAge <= question.Age2) || (patientAge >= question.Age2 && patientAge <= question.Age1))
							return true;
						break;
					case AgeOperations.None:
						return true;
				}
			}

			return false;
		}
	}
}
