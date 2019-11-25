using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class ArrivalConfirmationAndRoutingViewModel : ViewModelBase
	{
		private bool _enableScreenTap;
		private bool _exceptionFlag;
		private IAppointmentRepository _appointmentRepository;
		private AppointmentCollection _appointmentCollection;
		private bool? _isProgressBarVisible;
		private List<Appointment> _appointments;
		private int _commandParameter;
		private RelayCommand<string> _loadedCommand;

		public bool EnableScreenTap
		{
			get
			{
				return _enableScreenTap;
			}

			set
			{
				_enableScreenTap = value;
				RaisePropertyChanged("EnableScreenTap");
			}
		}

		public AppointmentCollection AppointmentCollection
		{
			get
			{
				return _appointmentCollection;
			}

			set
			{
				_appointmentCollection = value;
				RaisePropertyChanged("AppointmentCollection");
			}
		}

		public bool? IsProgressBarVisible
		{
			get
			{
				return _isProgressBarVisible;
			}

			set
			{
				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		public RelayCommand<string> LoadedCommand
		{
			get
			{
				return _loadedCommand
					?? (_loadedCommand = new RelayCommand<string>(
										  p =>
										  {
											  if (_exceptionFlag || !GlobalVariables.IsDbConnected)
											  {
												  Messenger.Default.Send(AppPages.ExceptionDivert);
											  }
										  }));
			}
		}

		public ArrivalConfirmationAndRoutingViewModel()
		{
			InitializeControls();
			ConfirmArrival(_commandParameter);
		}

		private void InitializeControls()
		{
			_appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();
			AppointmentCollection = GlobalVariables.AppointmentCollection;
			_appointments = GlobalVariables.Appointments;
			_commandParameter = GlobalVariables.CommandParameter;
			IsProgressBarVisible = null;
			GlobalVariables.ListOfArrivedAppointmentDetails = new List<Appointment>();
		}

		private void ConfirmArrival(int commandParameter)
		{
			try
			{
				if (commandParameter == 1 || commandParameter == 2 || commandParameter == 3)
				{
					if (commandParameter == 2)
					{
						foreach (AppointmentDetail app in AppointmentCollection.Where(app => app.IsEnabled))
						{
							app.IsChecked = true;
						}
					}
					if (commandParameter == 3)
					{
						if (AppointmentCollection.Any(x => x.IsEnabled))
							AppointmentCollection.First(a => a.IsEnabled).IsChecked = true;
					}

					EnableScreenTap = false;
					ConfirmArrival();
				}
				else
				{
					Messenger.Default.Send(AppPages.HomePage);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void ConfirmArrival()
		{
			try
			{
				if (AppointmentCollection.Count(appointmentdetail => appointmentdetail.IsChecked) > 0)
				{
					var appdetailList = AppointmentCollection.Where(a => a.IsChecked).ToList();
					foreach (long appid in appdetailList.Select(appointment => appointment.AppointmentId))
					{
						GlobalVariables.ListOfArrivedAppointmentDetails.Add(_appointments.First(a => a.Id == appid));
					}
				}
				IsProgressBarVisible = true;
				ExecuteTask();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void ExecuteTask()
		{
			bool isSuccess = false;
			GlobalVariables.IfForcedSurveyDone = false;

			Task.Factory.StartNew(() =>
			{
				if (AppointmentCollection != null)
				{
					int selectedAppointmentsCount = AppointmentCollection.Count(appointmentDetail => appointmentDetail.IsChecked);

					if (selectedAppointmentsCount == GetAppointmentsCountByArrivalType(AppointmentArrivalStatus.OnTime))
						isSuccess = ConfirmAppointment();
				}
			}).ContinueWith(
					t =>
					{
						Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => IsProgressBarVisible = null));
						EnableScreenTap = true;
						if (AppointmentCollection.Any(appointmentDetail => appointmentDetail.IsChecked))
						{
							var appCount = AppointmentCollection.Count(x => x.IsChecked);
							var arrivedAppCount = AppointmentCollection.Count(a => a.ErrorMessage == "Status already set");
							AppPages appPages;
							if (isSuccess)
								appPages = ShowDemoDetails() ? AppPages.DemographicMessages : AppPages.FinishRouting;
							else
							{
								//Has to write it more efficiently.
								if (arrivedAppCount > 0)
								{
									if (appCount > 1)
									{
										if (appCount == arrivedAppCount)
											appPages = AppPages.ArrivedAppointmentError;
										else
										{
											var arrivedAppointments = AppointmentCollection.Where(a => a.ErrorMessage == "Status already set").Select(a => a.AppointmentId).ToList();
											GlobalVariables.ListOfArrivedAppointmentDetails.RemoveAll(app => arrivedAppointments.Contains(app.Id));
											appPages = ShowDemoDetails() ? AppPages.DemographicMessages : AppPages.FinishRouting;
										}
									}
									else
										appPages = AppPages.ArrivedAppointmentError;
								}
								else
								{
									appPages = AppPages.ExceptionDivert;
								}
							}


							Messenger.Default.Send(appPages);
						}
					},
					TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool ConfirmAppointment()
		{
			var isSuccess = false;
			var doctorList = new List<int>();

			try
			{
				if (AppointmentCollection.Count > 0)
				{
					var apiHelper = new ApiHelper();
					foreach (AppointmentDetail appointment in AppointmentCollection.Where(appointment => appointment.IsChecked && appointment.ArrivalType == AppointmentArrivalStatus.OnTime))
					{
						string result;
						if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName().ToString())
						{
							result = apiHelper.SetTPPAppointmentStatus(appointment.TPPAppointmentId, appointment.PatientIdentifier);
						}
						else
						{
							result = apiHelper.SetAppointmentStatus((Int32)appointment.AppointmentId);
						}
						isSuccess = result.Contains("Success");
						if (result.Contains("Status already set"))
							appointment.ErrorMessage = "Status already set";
						if(result.Contains("Failure"))
							appointment.ErrorMessage = result;
						doctorList.Add(appointment.DoctorId);
						appointment.ConfirmationFailed = !isSuccess;
						if (isSuccess)
						{
							if (GlobalVariables.IsMuliplePatientCheckDone)
								Logger.Instance.WriteLog(LogType.Info, ActionType.MultipleMatchesValidated.GetDisplayName(), null, KioskId);
							string message = GlobalVariables.IsBarCodeArrivalDone ? ActionType.BarcodeArrival.ToString() : ActionType.Arrived.ToString();
							if (appointment.Time == "Appointment")
								message = "Arrived for untimed appointment - " + GlobalVariables.ArrivedPatientDetails.BookedPatient.Id + " - " + appointment.Name;
							else
								message = message + " - " + GlobalVariables.ArrivedPatientDetails.BookedPatient.Id + " - " + appointment.Name + " - " + appointment.Time;
							Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
							_appointmentRepository.UpdateAppointmentStatus((Int32)appointment.AppointmentId);
						}
						else
						{
							string message = GlobalVariables.IsMuliplePatientCheckDone ? ActionType.MultipleMatchesInValid.GetDisplayName() : ActionType.UnsuccessfullArrival.GetDisplayName();
							Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
						}
					}

					isSuccess = AppointmentCollection.Count(appointmentDetail => appointmentDetail.ConfirmationFailed) <= 0;
				}

				GlobalVariables.ArrivedPatientDoctorList = doctorList;
				return isSuccess;
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
				return false;
			}
		}

		private int GetAppointmentsCountByArrivalType(AppointmentArrivalStatus arrivalType)
		{
			return AppointmentCollection.Count(appointmentDetail => appointmentDetail.ArrivalType == arrivalType &&
				appointmentDetail.IsChecked);
		}

		private static bool ShowDemoDetails()
		{
			var demographicRepository = DiResolver.CurrentInstance.Reslove<IDemographicRepository>();
			UserDemographicSetting userDemographicSetting = GlobalVariables.UserDemographicDetailsSettings;
			bool isdatapresent = userDemographicSetting.UserDemographicLists != null && userDemographicSetting.UserDemographicLists.Count > 0;
			bool showDemographicDetailsForPatient = demographicRepository.ShowDemographicDetailsForPatient(userDemographicSetting.Duration);

			return !string.IsNullOrEmpty(GlobalVariables.ArrivedPatientDetails.BookedPatient.PostCode) && showDemographicDetailsForPatient && userDemographicSetting.ShowDetails && isdatapresent;
		}
	}
}