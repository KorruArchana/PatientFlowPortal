using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper.ResolutionSizeHelper;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary>
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<SelectOptionViewModel>();
			SimpleIoc.Default.Register<SelectDayViewModel>();
			SimpleIoc.Default.Register<SelectYearViewModel>();
			SimpleIoc.Default.Register<FinishViewModel>();
			SimpleIoc.Default.Register<OutOfServiceViewModel>();
			SimpleIoc.Default.Register<SurveysChooseOptionViewModel>();
			SimpleIoc.Default.Register<SurveyQuestionsViewModel>();
			SimpleIoc.Default.Register<DoctorSelectionViewModel>();
			SimpleIoc.Default.Register<ConfirmBookingViewModel>();
			SimpleIoc.Default.Register<FinishBookingViewModel>();
			SimpleIoc.Default.Register<BookingTimeSelectionViewModel>();
			SimpleIoc.Default.Register<FinishQuestionnaireViewModel>();
			SimpleIoc.Default.Register<SelectGenderViewModel>();
			SimpleIoc.Default.Register<SelectSurnameViewModel>();
			SimpleIoc.Default.Register<SelectMonthViewModel>();
			SimpleIoc.Default.Register<ExceptionDivertViewModel>();
			SimpleIoc.Default.Register<SelectOrganisationViewModel>();
			SimpleIoc.Default.Register<SelectSlotTypeViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<PatientDemographicDetailsViewModel>();
			SimpleIoc.Default.Register<DemographicGpMessageViewModel>();
			SimpleIoc.Default.Register<BarcodeArrivalViewModel>();
			SimpleIoc.Default.Register<SelectPostCodeViewModel>();
			SimpleIoc.Default.Register<FirstAvailableAppointmentViewModel>();
			SimpleIoc.Default.Register<MultiplePatientsExceptionPageViewModel>();
			SimpleIoc.Default.Register<SiteMapViewModel>();
			SimpleIoc.Default.Register<SizeHelperViewModel>();
			SimpleIoc.Default.Register<HomePageViewModel>();
			SimpleIoc.Default.Register<SelectDayMonthYearViewModel>();
			SimpleIoc.Default.Register<ArrivalRoutingViewModel>();
			SimpleIoc.Default.Register<ArrivalConfirmationAndRoutingViewModel>();
			SimpleIoc.Default.Register<SingleAppointmentViewModel>();
			SimpleIoc.Default.Register<MultipleAppointmentsViewModel>();
			SimpleIoc.Default.Register<FinishRoutingViewModel>();
			SimpleIoc.Default.Register<ArrivedAppointmentErrorViewModel>();
		}

		public DemographicGpMessageViewModel DemographicGpMessage
		{
			get { return ServiceLocator.Current.GetInstance<DemographicGpMessageViewModel>(); }
		}

		public SelectOptionViewModel SelectOption
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectOptionViewModel>();
			}
		}

		public SelectDayViewModel SelectDay
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectDayViewModel>();
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed. Suppression is OK here.")]
		public SelectYearViewModel SelectYear
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectYearViewModel>();
			}
		}

		public SelectPostCodeViewModel SelectPostCode
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectPostCodeViewModel>();
			}
		}

		public FinishViewModel Finish
		{
			get
			{
				return ServiceLocator.Current.GetInstance<FinishViewModel>();
			}
		}

		public OutOfServiceViewModel OutOfService
		{
			get
			{
				return ServiceLocator.Current.GetInstance<OutOfServiceViewModel>();
			}
		}

		public SurveysChooseOptionViewModel SurveysChooseOption
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SurveysChooseOptionViewModel>();
			}
		}

		public SurveyQuestionsViewModel SurveyQuestions
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SurveyQuestionsViewModel>();
			}
		}

		public DoctorSelectionViewModel DoctorSelection
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DoctorSelectionViewModel>();
			}
		}

		public ConfirmBookingViewModel ConfirmBooking
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ConfirmBookingViewModel>();
			}
		}

		public FinishBookingViewModel FinishBooking
		{
			get
			{
				return ServiceLocator.Current.GetInstance<FinishBookingViewModel>();
			}
		}

		public BookingTimeSelectionViewModel BookingTimeSelection
		{
			get
			{
				return ServiceLocator.Current.GetInstance<BookingTimeSelectionViewModel>();
			}
		}

		public FinishQuestionnaireViewModel FinishQuestionnaires
		{
			get
			{
				return ServiceLocator.Current.GetInstance<FinishQuestionnaireViewModel>();
			}
		}

		public SelectGenderViewModel SelectGender
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectGenderViewModel>();
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed. Suppression is OK here.")]
		public SelectSurnameViewModel SelectSurname
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectSurnameViewModel>();
			}
		}

		public SelectMonthViewModel SelectMonth
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectMonthViewModel>();
			}
		}

		public ExceptionDivertViewModel Divert
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ExceptionDivertViewModel>();
			}
		}

		public SelectOrganisationViewModel SelectOrganisation
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectOrganisationViewModel>();
			}
		}

		public SelectSlotTypeViewModel SelectSlotType
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectSlotTypeViewModel>();
			}
		}

		public SettingsViewModel Settings
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SettingsViewModel>();
			}
		}

		public PatientDemographicDetailsViewModel PatientDemographicDetails
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PatientDemographicDetailsViewModel>();
			}
		}

		public BarcodeArrivalViewModel BarCodeArrival
		{
			get
			{
				return ServiceLocator.Current.GetInstance<BarcodeArrivalViewModel>();
			}
		}

		public FirstAvailableAppointmentViewModel FirstAvailableAppointment
		{
			get
			{
				return ServiceLocator.Current.GetInstance<FirstAvailableAppointmentViewModel>();
			}
		}

		public MultiplePatientsExceptionPageViewModel MultiplePatientsExceptionPage
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MultiplePatientsExceptionPageViewModel>();
			}
		}

		public SelectDayMonthYearViewModel SelectDayMonthYear
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SelectDayMonthYearViewModel>();
			}
		}

		public SiteMapViewModel SiteMap
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SiteMapViewModel>();
			}
		}

		public SizeHelperViewModel SizeHelperModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SizeHelperViewModel>();
			}
		}

		public HomePageViewModel HomePage
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HomePageViewModel>();
			}
		}

		public ArrivalRoutingViewModel ArrivalRouting
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ArrivalRoutingViewModel>();
			}
		}

		public ArrivalConfirmationAndRoutingViewModel ArrivalConfirmationAndRouting
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ArrivalConfirmationAndRoutingViewModel>();
			}
		}

		public SingleAppointmentViewModel SingleAppointment
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SingleAppointmentViewModel>();
			}
		}

		public MultipleAppointmentsViewModel MultipleAppointments
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MultipleAppointmentsViewModel>();
			}
		}

		public FinishRoutingViewModel FinishRouting
		{
			get { return ServiceLocator.Current.GetInstance<FinishRoutingViewModel>(); }
		}

		public ArrivedAppointmentErrorViewModel ArrivedAppointmentError
		{
			get { return ServiceLocator.Current.GetInstance<ArrivedAppointmentErrorViewModel>(); }
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed. Suppression is OK here.")]
		public static void Cleanup(AppPages selectedPage)
		{
			switch (selectedPage)
			{
				case AppPages.SelectDayMonthYear:
					if (SimpleIoc.Default.IsRegistered<SelectDayMonthYearViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectDayMonthYearViewModel>();
						SimpleIoc.Default.Register<SelectDayMonthYearViewModel>();
					}
					break;

				case AppPages.DemographicMessages:
					if (SimpleIoc.Default.IsRegistered<PatientDemographicDetailsViewModel>())
					{
						SimpleIoc.Default.Unregister<PatientDemographicDetailsViewModel>();
						SimpleIoc.Default.Register<PatientDemographicDetailsViewModel>();
					}
					break;

				case AppPages.ShowGpDemographicMessages:
					if (SimpleIoc.Default.IsRegistered<DemographicGpMessageViewModel>())
					{
						SimpleIoc.Default.Unregister<DemographicGpMessageViewModel>();
						SimpleIoc.Default.Register<DemographicGpMessageViewModel>();
					}
					break;

				case AppPages.OutOfService:
					if (SimpleIoc.Default.IsRegistered<OutOfServiceViewModel>())
					{
						SimpleIoc.Default.Unregister<OutOfServiceViewModel>();
						SimpleIoc.Default.Register<OutOfServiceViewModel>();
					}
					break;
				case AppPages.SelectModule:
					if (SimpleIoc.Default.IsRegistered<SelectOptionViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectOptionViewModel>();
						SimpleIoc.Default.Register<SelectOptionViewModel>();
					}
					break;
				case AppPages.SelectDay:
					if (SimpleIoc.Default.IsRegistered<SelectDayViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectDayViewModel>();
						SimpleIoc.Default.Register<SelectDayViewModel>();
					}
					break;
				case AppPages.SelectYear:
					if (SimpleIoc.Default.IsRegistered<SelectYearViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectYearViewModel>();
						SimpleIoc.Default.Register<SelectYearViewModel>();
					}
					break;
				case AppPages.Finish:
					if (SimpleIoc.Default.IsRegistered<FinishViewModel>())
					{
						SimpleIoc.Default.Unregister<FinishViewModel>();
						SimpleIoc.Default.Register<FinishViewModel>();
					}
					break;
				case AppPages.Surveys:
					if (SimpleIoc.Default.IsRegistered<SurveysChooseOptionViewModel>())
					{
						SimpleIoc.Default.Unregister<SurveysChooseOptionViewModel>();
						SimpleIoc.Default.Register<SurveysChooseOptionViewModel>();
					}
					break;
				case AppPages.SurveyQuestions:
					if (SimpleIoc.Default.IsRegistered<SurveyQuestionsViewModel>())
					{
						SimpleIoc.Default.Unregister<SurveyQuestionsViewModel>();
						SimpleIoc.Default.Register<SurveyQuestionsViewModel>();
					}
					break;
				case AppPages.BookMe:
					if (SimpleIoc.Default.IsRegistered<DoctorSelectionViewModel>())
					{
						SimpleIoc.Default.Unregister<DoctorSelectionViewModel>();
						SimpleIoc.Default.Register<DoctorSelectionViewModel>();
					}
					break;
				case AppPages.ConfirmBooking:
					if (SimpleIoc.Default.IsRegistered<ConfirmBookingViewModel>())
					{
						SimpleIoc.Default.Unregister<ConfirmBookingViewModel>();
						SimpleIoc.Default.Register<ConfirmBookingViewModel>();
					}
					break;
				case AppPages.FinishBooking:
					if (SimpleIoc.Default.IsRegistered<FinishBookingViewModel>())
					{
						SimpleIoc.Default.Unregister<FinishBookingViewModel>();
						SimpleIoc.Default.Register<FinishBookingViewModel>();
					}
					break;
				case AppPages.BookingTimeSelection:
					if (SimpleIoc.Default.IsRegistered<BookingTimeSelectionViewModel>())
					{
						SimpleIoc.Default.Unregister<BookingTimeSelectionViewModel>();
						SimpleIoc.Default.Register<BookingTimeSelectionViewModel>();
					}
					break;
				case AppPages.FinishQuestionnaires:
					if (SimpleIoc.Default.IsRegistered<FinishQuestionnaireViewModel>())
					{
						SimpleIoc.Default.Unregister<FinishQuestionnaireViewModel>();
						SimpleIoc.Default.Register<FinishQuestionnaireViewModel>();
					}
					break;

				case AppPages.SelectGender:
					if (SimpleIoc.Default.IsRegistered<SelectGenderViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectGenderViewModel>();
						SimpleIoc.Default.Register<SelectGenderViewModel>();
					}
					break;
				case AppPages.SelectSurname:
					if (SimpleIoc.Default.IsRegistered<SelectSurnameViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectSurnameViewModel>();
						SimpleIoc.Default.Register<SelectSurnameViewModel>();
					}
					break;
				case AppPages.SelectMonth:
					if (SimpleIoc.Default.IsRegistered<SelectMonthViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectMonthViewModel>();
						SimpleIoc.Default.Register<SelectMonthViewModel>();
					}
					break;
				case AppPages.ExceptionDivert:
					if (SimpleIoc.Default.IsRegistered<ExceptionDivertViewModel>())
					{
						SimpleIoc.Default.Unregister<ExceptionDivertViewModel>();
						SimpleIoc.Default.Register<ExceptionDivertViewModel>();
					}
					break;
				case AppPages.Organisation:
					if (SimpleIoc.Default.IsRegistered<SelectOrganisationViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectOrganisationViewModel>();
						SimpleIoc.Default.Register<SelectOrganisationViewModel>();
					}
					break;
				case AppPages.SlotType:
					if (SimpleIoc.Default.IsRegistered<SelectSlotTypeViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectSlotTypeViewModel>();
						SimpleIoc.Default.Register<SelectSlotTypeViewModel>();
					}
					break;
				case AppPages.Settings:
					if (SimpleIoc.Default.IsRegistered<SettingsViewModel>())
					{
						SimpleIoc.Default.Unregister<SettingsViewModel>();
						SimpleIoc.Default.Register<SettingsViewModel>();
					}
					break;
				case AppPages.SelectPostCode:
					if (SimpleIoc.Default.IsRegistered<SelectPostCodeViewModel>())
					{
						SimpleIoc.Default.Unregister<SelectPostCodeViewModel>();
						SimpleIoc.Default.Register<SelectPostCodeViewModel>();
					}
					break;
				case AppPages.ArrivalByBarcode:
					{
						if (SimpleIoc.Default.IsRegistered<BarcodeArrivalViewModel>())
						{
							SimpleIoc.Default.Unregister<BarcodeArrivalViewModel>();
							SimpleIoc.Default.Register<BarcodeArrivalViewModel>();
						}
						break;
					}
				case AppPages.FirstAvailableAppointment:
					{
						if (SimpleIoc.Default.IsRegistered<FirstAvailableAppointmentViewModel>())
						{
							SimpleIoc.Default.Unregister<FirstAvailableAppointmentViewModel>();
							SimpleIoc.Default.Register<FirstAvailableAppointmentViewModel>();
						}
						break;
					}
				case AppPages.MultiplePatientsExceptionPage:
					{
						if (SimpleIoc.Default.IsRegistered<MultiplePatientsExceptionPageViewModel>())
						{
							SimpleIoc.Default.Unregister<MultiplePatientsExceptionPageViewModel>();
							SimpleIoc.Default.Register<MultiplePatientsExceptionPageViewModel>();
						}
						break;
					}
				case AppPages.SiteMap:
					{
						if (SimpleIoc.Default.IsRegistered<SiteMapViewModel>())
						{
							SimpleIoc.Default.Unregister<SiteMapViewModel>();
							SimpleIoc.Default.Register<SiteMapViewModel>();
						}
						break;
					}
				case AppPages.HomePage:
					{
						if (SimpleIoc.Default.IsRegistered<HomePageViewModel>())
						{
							SimpleIoc.Default.Unregister<HomePageViewModel>();
							SimpleIoc.Default.Register<HomePageViewModel>();
						}
						break;
					}
				case AppPages.ArrivalRouting:
					{
						if (SimpleIoc.Default.IsRegistered<ArrivalRoutingViewModel>())
						{
							SimpleIoc.Default.Unregister<ArrivalRoutingViewModel>();
							SimpleIoc.Default.Register<ArrivalRoutingViewModel>();
						}
						break;
					}
				case AppPages.ArrivalConfirmationAndRouting:
					{
						if (SimpleIoc.Default.IsRegistered<ArrivalConfirmationAndRoutingViewModel>())
						{
							SimpleIoc.Default.Unregister<ArrivalConfirmationAndRoutingViewModel>();
							SimpleIoc.Default.Register<ArrivalConfirmationAndRoutingViewModel>();
						}
						break;
					}
				case AppPages.SingleAppointment:
					{
						if (SimpleIoc.Default.IsRegistered<SingleAppointmentViewModel>())
						{
							SimpleIoc.Default.Unregister<SingleAppointmentViewModel>();
							SimpleIoc.Default.Register<SingleAppointmentViewModel>();
						}
						break;
					}
				case AppPages.MultipleAppointments:
					{
						if (SimpleIoc.Default.IsRegistered<MultipleAppointmentsViewModel>())
						{
							SimpleIoc.Default.Unregister<MultipleAppointmentsViewModel>();
							SimpleIoc.Default.Register<MultipleAppointmentsViewModel>();
						}
						break;
					}

				case AppPages.FinishRouting:
					{
						if (SimpleIoc.Default.IsRegistered<FinishRoutingViewModel>())
						{
							SimpleIoc.Default.Unregister<FinishRoutingViewModel>();
							SimpleIoc.Default.Register<FinishRoutingViewModel>();
						}
						break;
					}

				case AppPages.ArrivedAppointmentError:
					{
						if (SimpleIoc.Default.IsRegistered<ArrivedAppointmentErrorViewModel>())
						{
							SimpleIoc.Default.Unregister<ArrivedAppointmentErrorViewModel>();
							SimpleIoc.Default.Register<ArrivedAppointmentErrorViewModel>();
						}
						break;
					}
			}
		}
	}
}