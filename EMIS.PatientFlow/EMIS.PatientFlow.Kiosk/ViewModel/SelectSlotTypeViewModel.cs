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

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class SelectSlotTypeViewModel : ViewModelBase
	{
		private int _distinctPatientCount;
		private bool _exceptionFlag;
		private string _hiText;
		private string _patientNameText;
		private string _notYouText;
		private string _selectSlotText;
		private string _cannotFindSpeakToReceptionText;
		private bool _isPatientNotFound;

		private IAppointmentRepository _appointmentRepository;
		private IConfigurationRepository _configRepository;
		private List<AppointmentSlotType> _slotTypeList;
		private RelayCommand<string> _setSlotTypeCommand;
		private RelayCommand<string> _loadedCommand;
		private RelayCommand<object> _notYouCommand;

		public string HiText
		{
			get
			{
				return _hiText;
			}
			set
			{
				_hiText = value;
				RaisePropertyChanged("HiText");
			}
		}

		public string PatientNameText
		{
			get
			{
				return _patientNameText;
			}
			set
			{
				_patientNameText = value;
				RaisePropertyChanged("PatientNameText");
			}
		}

		public string NotYouText
		{
			get
			{
				return _notYouText;
			}
			set
			{
				_notYouText = value;
				RaisePropertyChanged("NotYouText");
			}
		}

		public string SelectSlotText
		{
			get
			{
				return _selectSlotText;
			}
			set
			{
				_selectSlotText = value;
				RaisePropertyChanged("SelectSlotText");
			}
		}

		public string CannotFindSpeakToReceptionText
		{
			get
			{
				return _cannotFindSpeakToReceptionText;
			}

			set
			{
				_cannotFindSpeakToReceptionText = value;
				RaisePropertyChanged("CannotFindSpeakToReceptionText");
			}
		}

		public List<AppointmentSlotType> SlotTypeList
		{
			get
			{
				return _slotTypeList;
			}

			set
			{
				_slotTypeList = value;
				RaisePropertyChanged("SlotTypeList");
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
											  else if (_distinctPatientCount > 1)
											  {
												  Messenger.Default.Send(GlobalVariables.IsMuliplePatientCheckDone
													  ? ExceptionDivert()
													  : AppPages.SelectPostCode);
											  }
											  else if (_isPatientNotFound)
											  {
												  GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
												  Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
											  }
											  else if ((GlobalVariables.SlotTypeList.Count <= 1 || GlobalVariables.SlotTypeList == null) && _distinctPatientCount != 0)
											  {
												  Messenger.Default.Send(AppPages.FirstAvailableAppointment);
											  }
										  }));
			}
		}

		private AppPages ExceptionDivert()
		{
			Logger.Instance.WriteLog(LogType.Info, ActionType.MultipleMatchesInValid.GetDisplayName(), null, KioskId);
			GlobalVariables.ErrorCode = ErrorCodes.MultiplePatientsFound;
			return AppPages.MultiplePatientsExceptionPage;
		}

		public RelayCommand<string> SetSlotTypeCommand
		{
			get
			{
				return _setSlotTypeCommand
					?? (_setSlotTypeCommand = new RelayCommand<string>(
										  p =>
										  {
											  GlobalVariables.SelectedSlotType = SlotTypeList.FirstOrDefault(x => x.SlotTypeId == p);
											  Messenger.Default.Send(AppPages.FirstAvailableAppointment);

										  }));
			}
		}

		public RelayCommand<object> NotYouCommand
		{
			get
			{
				return _notYouCommand
					?? (_notYouCommand = new RelayCommand<object>(
						p =>
						{   //PNF
							GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
							Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
						}));

			}
		}

		public SelectSlotTypeViewModel()
		{
			FilterPatients();
			GetSlots();
			InitializeControls();
		}

		private void InitializeControls()
		{
			SetControlText();
		}

		internal void SetControlText()
		{
			if (GlobalVariables.PatientMatchesPatient != null)
			{
				SelectSlotText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectSlotText];
				CannotFindSpeakToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.CannotFindSpeakToReception] + Environment.NewLine + GlobalVariables.SelectedLanguageIdText[LanguageText.SpeakToReceptionText];
				SlotTypeList = GlobalVariables.SlotTypeList;
				HiText = GlobalVariables.SelectedLanguageIdText[LanguageText.HiText];
				PatientNameText = GlobalVariables.PatientMatchesPatient.PatientDisplayName;
				NotYouText = GlobalVariables.SelectedLanguageIdText[LanguageText.NotYouText];
			}
			else
			{
				_isPatientNotFound = true;
			}
		}

		private void FilterPatients()
		{
			try
			{
				List<Appointment> appointments = null;
				EMIS.PatientFlow.API.Data.PatientMatchesPatient[] matchingPatients =
					GlobalVariables.PatientMatches != null ? GlobalVariables.PatientMatches.PatientList : null;
				_appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();
				GlobalVariables.PatientMatchesPatient = null;

				if (matchingPatients != null)
				{
					appointments = _appointmentRepository.GetMatchingAppointments(null, false);
					if (appointments.Count > 0)
					{
						_distinctPatientCount = appointments.GroupBy(x => x.BookedPatient.Id).ToList().Count;
						if (_distinctPatientCount == 1)
						{
							GlobalVariables.PatientMatchesPatient = GlobalVariables.PatientMatches.PatientList.First(
								patientMatchesPatient => patientMatchesPatient.DBID == Convert.ToString(
								appointments.First().BookedPatient.Id));

							GlobalVariables.Appointment.PatientId = GlobalVariables.PatientMatchesPatient.DBID;
							GlobalVariables.Appointment.PatientIdentifier = GlobalVariables.PatientMatchesPatient.NhsNumber;
						}
						else if (_distinctPatientCount > 1)
						{
							Logger.Instance.WriteLog(
										LogType.Info,
										ActionType.MultipleMatches.GetDisplayName(),
										null,
										KioskId);
						}
					}
					else
					{
						_isPatientNotFound = true;
					}
				}
				else
				{
					Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, "Patient Not Found", null, KioskId);
					_isPatientNotFound = true;
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void GetSlots()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			List<AppointmentSlotType> slotTypes = _configRepository.GetKioskConfiguration<List<AppointmentSlotType>>(KioskConfigType.SlotTypes.ToString());
			slotTypes = (slotTypes == null)
				? null
				: slotTypes.Where(
					appointmentSlotType => appointmentSlotType.OrganisationId == Convert.ToInt32(
				GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();
			GlobalVariables.SlotTypeList = slotTypes;
			GlobalVariables.SelectedSlotType = (slotTypes != null && slotTypes.Count >= 1) ? slotTypes[0] : null;
		}
	}
}
