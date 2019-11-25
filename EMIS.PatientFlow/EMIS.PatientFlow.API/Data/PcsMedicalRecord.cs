﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMIS.PatientFlow.API.Data
{
	public class PcsMedicalRecord
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		[System.Xml.Serialization.XmlRootAttribute("MedicalRecord", Namespace = "http://www.e-mis.com/emisopen/MedicalRecord", IsNullable = false)]
		public partial class MedicalRecordType
		{

			private RegistrationType registrationField;

			private MedicalRecordTypeRegistrationEntry[] registrationChangeHistoryField;

			private MedicalRecordTypeMiscellaneousData miscellaneousDataField;

			private EventType[] eventListField;

			private ConsultationType[] consultationListField;

			private InvestigationType[] investigationListField;

			private ReferralType[] referralListField;

			private AllergyType[] allergyListField;

			private MedicationType[] medicationListField;

			private IssueType[] issueListField;

			private AttachmentType[] attachmentListField;

			private DiaryType[] diaryListField;

			private AlertType[] alertListField;

			private RegistrationStatusType registrationStatusField;

			private PathologyReportType[] eDIPathologyReportListField;

			private TestRequestHeaderType[] testRequestHeaderListField;

			private NoteType[] noteListField;

			private AppointmentType[] appointmentListField;

			private PersonType[] peopleListField;

			private LocationType[] locationListField;

			private TypeOfLocationType[] locationTypeListField;

			private OriginatorType originatorField;

			private IdentType recipientIDField;

			private MedicalRecordTypeMessageInformation messageInformationField;

			private PolicyListTypePolicyType[] policyListField;

			private string patientIDField;

			/// <remarks/>
			public RegistrationType Registration
			{
				get
				{
					return this.registrationField;
				}
				set
				{
					this.registrationField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("RegistrationEntry", IsNullable = false)]
			public MedicalRecordTypeRegistrationEntry[] RegistrationChangeHistory
			{
				get
				{
					return this.registrationChangeHistoryField;
				}
				set
				{
					this.registrationChangeHistoryField = value;
				}
			}

			/// <remarks/>
			public MedicalRecordTypeMiscellaneousData MiscellaneousData
			{
				get
				{
					return this.miscellaneousDataField;
				}
				set
				{
					this.miscellaneousDataField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Event", IsNullable = false)]
			public EventType[] EventList
			{
				get
				{
					return this.eventListField;
				}
				set
				{
					this.eventListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Consultation", IsNullable = false)]
			public ConsultationType[] ConsultationList
			{
				get
				{
					return this.consultationListField;
				}
				set
				{
					this.consultationListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Investigation", IsNullable = false)]
			public InvestigationType[] InvestigationList
			{
				get
				{
					return this.investigationListField;
				}
				set
				{
					this.investigationListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Referral", IsNullable = false)]
			public ReferralType[] ReferralList
			{
				get
				{
					return this.referralListField;
				}
				set
				{
					this.referralListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Allergy", IsNullable = false)]
			public AllergyType[] AllergyList
			{
				get
				{
					return this.allergyListField;
				}
				set
				{
					this.allergyListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Medication", IsNullable = false)]
			public MedicationType[] MedicationList
			{
				get
				{
					return this.medicationListField;
				}
				set
				{
					this.medicationListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Issue", IsNullable = false)]
			public IssueType[] IssueList
			{
				get
				{
					return this.issueListField;
				}
				set
				{
					this.issueListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Attachment", IsNullable = false)]
			public AttachmentType[] AttachmentList
			{
				get
				{
					return this.attachmentListField;
				}
				set
				{
					this.attachmentListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Diary", IsNullable = false)]
			public DiaryType[] DiaryList
			{
				get
				{
					return this.diaryListField;
				}
				set
				{
					this.diaryListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Alert", IsNullable = false)]
			public AlertType[] AlertList
			{
				get
				{
					return this.alertListField;
				}
				set
				{
					this.alertListField = value;
				}
			}

			/// <remarks/>
			public RegistrationStatusType RegistrationStatus
			{
				get
				{
					return this.registrationStatusField;
				}
				set
				{
					this.registrationStatusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("PathologyReport", IsNullable = false)]
			public PathologyReportType[] EDIPathologyReportList
			{
				get
				{
					return this.eDIPathologyReportListField;
				}
				set
				{
					this.eDIPathologyReportListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("TestRequestHeader", IsNullable = false)]
			public TestRequestHeaderType[] TestRequestHeaderList
			{
				get
				{
					return this.testRequestHeaderListField;
				}
				set
				{
					this.testRequestHeaderListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Note", IsNullable = false)]
			public NoteType[] NoteList
			{
				get
				{
					return this.noteListField;
				}
				set
				{
					this.noteListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Appointment", IsNullable = false)]
			public AppointmentType[] AppointmentList
			{
				get
				{
					return this.appointmentListField;
				}
				set
				{
					this.appointmentListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Person", IsNullable = false)]
			public PersonType[] PeopleList
			{
				get
				{
					return this.peopleListField;
				}
				set
				{
					this.peopleListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Location", IsNullable = false)]
			public LocationType[] LocationList
			{
				get
				{
					return this.locationListField;
				}
				set
				{
					this.locationListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("LocationType", IsNullable = false)]
			public TypeOfLocationType[] LocationTypeList
			{
				get
				{
					return this.locationTypeListField;
				}
				set
				{
					this.locationTypeListField = value;
				}
			}

			/// <remarks/>
			public OriginatorType Originator
			{
				get
				{
					return this.originatorField;
				}
				set
				{
					this.originatorField = value;
				}
			}

			/// <remarks/>
			public IdentType RecipientID
			{
				get
				{
					return this.recipientIDField;
				}
				set
				{
					this.recipientIDField = value;
				}
			}

			/// <remarks/>
			public MedicalRecordTypeMessageInformation MessageInformation
			{
				get
				{
					return this.messageInformationField;
				}
				set
				{
					this.messageInformationField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("PolicyType", IsNullable = false)]
			public PolicyListTypePolicyType[] PolicyList
			{
				get
				{
					return this.policyListField;
				}
				set
				{
					this.policyListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
			public string PatientID
			{
				get
				{
					return this.patientIDField;
				}
				set
				{
					this.patientIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class RegistrationType : IdentType
		{

			private string dateOfBirthField;

			private string sexField;

			private string previousNamesField;

			private string familyNameField;

			private string callingNameField;

			private string firstNamesField;

			private string titleField;

			private string nhsNumberField;

			private AddressType addressField;

			private string homeTelephoneField;

			private IdentType registeredGpIDField;

			private IdentType usualGpIDField;

			private IdentType tradingHaIDField;

			private string walkingQuartersField;

			private string dispensingField;

			private string ruralMileageField;

			private string difficultQuartersField;

			private string residentialInstituteField;

			private string blockedSpecialField;

			private string recordsAtField;

			private string hospitalNumbersField;

			private string deductedDateField;

			private string dateOfDeathField;

			private RegistrationTypeDead deadField;

			private bool deadFieldSpecified;

			private string dateAddedField;

			private string oldNhsNumberField;

			private string workTelephoneField;

			private string mobileField;

			private string emailField;

			private AddressType homeAddressField;

			private string homeGPField;

			private string homeGPCodeField;

			private string cHINumberField;

			private string footpathMilesField;

			private string waterMilesField;

			private string serviceTypeField;

			private string serviceNumberField;

			private RegistrationTypeCustomRegistrationEntry[] customRegistrationFieldsField;

			private string gRONumberField;

			private string uPCINumberField;

			private string maritalStatusField;

			private string medicationReviewDateField;

			private string exemptionExpiryField;

			private string screenMessageField;

			private string reminderSentField;

			private string ethnicCodeField;

			private string prisonNumberField;

			private string previousPrisonNumbersField;

			private IdentType prisonDoctorGPField;

			private string socialSecurityNumberField;

			/// <remarks/>
			public string DateOfBirth
			{
				get
				{
					return this.dateOfBirthField;
				}
				set
				{
					this.dateOfBirthField = value;
				}
			}

			/// <remarks/>
			public string Sex
			{
				get
				{
					return this.sexField;
				}
				set
				{
					this.sexField = value;
				}
			}

			/// <remarks/>
			public string PreviousNames
			{
				get
				{
					return this.previousNamesField;
				}
				set
				{
					this.previousNamesField = value;
				}
			}

			/// <remarks/>
			public string FamilyName
			{
				get
				{
					return this.familyNameField;
				}
				set
				{
					this.familyNameField = value;
				}
			}

			/// <remarks/>
			public string CallingName
			{
				get
				{
					return this.callingNameField;
				}
				set
				{
					this.callingNameField = value;
				}
			}

			/// <remarks/>
			public string FirstNames
			{
				get
				{
					return this.firstNamesField;
				}
				set
				{
					this.firstNamesField = value;
				}
			}

			/// <remarks/>
			public string Title
			{
				get
				{
					return this.titleField;
				}
				set
				{
					this.titleField = value;
				}
			}

			/// <remarks/>
			public string NhsNumber
			{
				get
				{
					return this.nhsNumberField;
				}
				set
				{
					this.nhsNumberField = value;
				}
			}

			/// <remarks/>
			public AddressType Address
			{
				get
				{
					return this.addressField;
				}
				set
				{
					this.addressField = value;
				}
			}

			/// <remarks/>
			public string HomeTelephone
			{
				get
				{
					return this.homeTelephoneField;
				}
				set
				{
					this.homeTelephoneField = value;
				}
			}

			/// <remarks/>
			public IdentType RegisteredGpID
			{
				get
				{
					return this.registeredGpIDField;
				}
				set
				{
					this.registeredGpIDField = value;
				}
			}

			/// <remarks/>
			public IdentType UsualGpID
			{
				get
				{
					return this.usualGpIDField;
				}
				set
				{
					this.usualGpIDField = value;
				}
			}

			/// <remarks/>
			public IdentType TradingHaID
			{
				get
				{
					return this.tradingHaIDField;
				}
				set
				{
					this.tradingHaIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string WalkingQuarters
			{
				get
				{
					return this.walkingQuartersField;
				}
				set
				{
					this.walkingQuartersField = value;
				}
			}

			/// <remarks/>
			public string Dispensing
			{
				get
				{
					return this.dispensingField;
				}
				set
				{
					this.dispensingField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string RuralMileage
			{
				get
				{
					return this.ruralMileageField;
				}
				set
				{
					this.ruralMileageField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string DifficultQuarters
			{
				get
				{
					return this.difficultQuartersField;
				}
				set
				{
					this.difficultQuartersField = value;
				}
			}

			/// <remarks/>
			public string ResidentialInstitute
			{
				get
				{
					return this.residentialInstituteField;
				}
				set
				{
					this.residentialInstituteField = value;
				}
			}

			/// <remarks/>
			public string BlockedSpecial
			{
				get
				{
					return this.blockedSpecialField;
				}
				set
				{
					this.blockedSpecialField = value;
				}
			}

			/// <remarks/>
			public string RecordsAt
			{
				get
				{
					return this.recordsAtField;
				}
				set
				{
					this.recordsAtField = value;
				}
			}

			/// <remarks/>
			public string HospitalNumbers
			{
				get
				{
					return this.hospitalNumbersField;
				}
				set
				{
					this.hospitalNumbersField = value;
				}
			}

			/// <remarks/>
			public string DeductedDate
			{
				get
				{
					return this.deductedDateField;
				}
				set
				{
					this.deductedDateField = value;
				}
			}

			/// <remarks/>
			public string DateOfDeath
			{
				get
				{
					return this.dateOfDeathField;
				}
				set
				{
					this.dateOfDeathField = value;
				}
			}

			/// <remarks/>
			public RegistrationTypeDead Dead
			{
				get
				{
					return this.deadField;
				}
				set
				{
					this.deadField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DeadSpecified
			{
				get
				{
					return this.deadFieldSpecified;
				}
				set
				{
					this.deadFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string DateAdded
			{
				get
				{
					return this.dateAddedField;
				}
				set
				{
					this.dateAddedField = value;
				}
			}

			/// <remarks/>
			public string OldNhsNumber
			{
				get
				{
					return this.oldNhsNumberField;
				}
				set
				{
					this.oldNhsNumberField = value;
				}
			}

			/// <remarks/>
			public string WorkTelephone
			{
				get
				{
					return this.workTelephoneField;
				}
				set
				{
					this.workTelephoneField = value;
				}
			}

			/// <remarks/>
			public string Mobile
			{
				get
				{
					return this.mobileField;
				}
				set
				{
					this.mobileField = value;
				}
			}

			/// <remarks/>
			public string Email
			{
				get
				{
					return this.emailField;
				}
				set
				{
					this.emailField = value;
				}
			}

			/// <remarks/>
			public AddressType HomeAddress
			{
				get
				{
					return this.homeAddressField;
				}
				set
				{
					this.homeAddressField = value;
				}
			}

			/// <remarks/>
			public string HomeGP
			{
				get
				{
					return this.homeGPField;
				}
				set
				{
					this.homeGPField = value;
				}
			}

			/// <remarks/>
			public string HomeGPCode
			{
				get
				{
					return this.homeGPCodeField;
				}
				set
				{
					this.homeGPCodeField = value;
				}
			}

			/// <remarks/>
			public string CHINumber
			{
				get
				{
					return this.cHINumberField;
				}
				set
				{
					this.cHINumberField = value;
				}
			}

			/// <remarks/>
			public string FootpathMiles
			{
				get
				{
					return this.footpathMilesField;
				}
				set
				{
					this.footpathMilesField = value;
				}
			}

			/// <remarks/>
			public string WaterMiles
			{
				get
				{
					return this.waterMilesField;
				}
				set
				{
					this.waterMilesField = value;
				}
			}

			/// <remarks/>
			public string ServiceType
			{
				get
				{
					return this.serviceTypeField;
				}
				set
				{
					this.serviceTypeField = value;
				}
			}

			/// <remarks/>
			public string ServiceNumber
			{
				get
				{
					return this.serviceNumberField;
				}
				set
				{
					this.serviceNumberField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("CustomRegistrationEntry", IsNullable = false)]
			public RegistrationTypeCustomRegistrationEntry[] CustomRegistrationFields
			{
				get
				{
					return this.customRegistrationFieldsField;
				}
				set
				{
					this.customRegistrationFieldsField = value;
				}
			}

			/// <remarks/>
			public string GRONumber
			{
				get
				{
					return this.gRONumberField;
				}
				set
				{
					this.gRONumberField = value;
				}
			}

			/// <remarks/>
			public string UPCINumber
			{
				get
				{
					return this.uPCINumberField;
				}
				set
				{
					this.uPCINumberField = value;
				}
			}

			/// <remarks/>
			public string MaritalStatus
			{
				get
				{
					return this.maritalStatusField;
				}
				set
				{
					this.maritalStatusField = value;
				}
			}

			/// <remarks/>
			public string MedicationReviewDate
			{
				get
				{
					return this.medicationReviewDateField;
				}
				set
				{
					this.medicationReviewDateField = value;
				}
			}

			/// <remarks/>
			public string ExemptionExpiry
			{
				get
				{
					return this.exemptionExpiryField;
				}
				set
				{
					this.exemptionExpiryField = value;
				}
			}

			/// <remarks/>
			public string ScreenMessage
			{
				get
				{
					return this.screenMessageField;
				}
				set
				{
					this.screenMessageField = value;
				}
			}

			/// <remarks/>
			public string ReminderSent
			{
				get
				{
					return this.reminderSentField;
				}
				set
				{
					this.reminderSentField = value;
				}
			}

			/// <remarks/>
			public string EthnicCode
			{
				get
				{
					return this.ethnicCodeField;
				}
				set
				{
					this.ethnicCodeField = value;
				}
			}

			/// <remarks/>
			public string PrisonNumber
			{
				get
				{
					return this.prisonNumberField;
				}
				set
				{
					this.prisonNumberField = value;
				}
			}

			/// <remarks/>
			public string PreviousPrisonNumbers
			{
				get
				{
					return this.previousPrisonNumbersField;
				}
				set
				{
					this.previousPrisonNumbersField = value;
				}
			}

			/// <remarks/>
			public IdentType PrisonDoctorGP
			{
				get
				{
					return this.prisonDoctorGPField;
				}
				set
				{
					this.prisonDoctorGPField = value;
				}
			}

			/// <remarks/>
			public string SocialSecurityNumber
			{
				get
				{
					return this.socialSecurityNumberField;
				}
				set
				{
					this.socialSecurityNumberField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AddressType : IdentType
		{

			private string houseNameFlatField;

			private string streetField;

			private string villageField;

			private string townField;

			private string countyField;

			private string postCodeField;

			/// <remarks/>
			public string HouseNameFlat
			{
				get
				{
					return this.houseNameFlatField;
				}
				set
				{
					this.houseNameFlatField = value;
				}
			}

			/// <remarks/>
			public string Street
			{
				get
				{
					return this.streetField;
				}
				set
				{
					this.streetField = value;
				}
			}

			/// <remarks/>
			public string Village
			{
				get
				{
					return this.villageField;
				}
				set
				{
					this.villageField = value;
				}
			}

			/// <remarks/>
			public string Town
			{
				get
				{
					return this.townField;
				}
				set
				{
					this.townField = value;
				}
			}

			/// <remarks/>
			public string County
			{
				get
				{
					return this.countyField;
				}
				set
				{
					this.countyField = value;
				}
			}

			/// <remarks/>
			public string PostCode
			{
				get
				{
					return this.postCodeField;
				}
				set
				{
					this.postCodeField = value;
				}
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(TeamType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(RegistrationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PathologySpecimenType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredIdentType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PathologyReportType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(TypeOfLocationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(LocationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(RoleType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AddressType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PersonCategoryType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PersonType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(NoteType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AppointmentType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(StatusType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PathologyTestType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(InvestigationTypeBase))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PathologyInvestigationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(InvestigationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(EDIOrderType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(TestRequestHeaderType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(ConsultationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemBaseType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(CodedItemBaseType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AlertType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(TestRequestType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttachmentType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AllergyType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReferralType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(DiaryType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(MedicationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(IssueType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(MixtureItemType))]
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class IdentType
		{

			private string dBIDField;

			private string refIDField;

			private string gUIDField;

			private string fileStatusField;

			private string oldGUIDField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string DBID
			{
				get
				{
					return this.dBIDField;
				}
				set
				{
					this.dBIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string RefID
			{
				get
				{
					return this.refIDField;
				}
				set
				{
					this.refIDField = value;
				}
			}

			/// <remarks/>
			public string GUID
			{
				get
				{
					return this.gUIDField;
				}
				set
				{
					this.gUIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string FileStatus
			{
				get
				{
					return this.fileStatusField;
				}
				set
				{
					this.fileStatusField = value;
				}
			}

			/// <remarks/>
			public string OldGUID
			{
				get
				{
					return this.oldGUIDField;
				}
				set
				{
					this.oldGUIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class OriginatorType
		{

			private OriginatorTypeSystem systemField;

			private IdentType organisationTypeField;

			private IdentType organisationField;

			/// <remarks/>
			public OriginatorTypeSystem System
			{
				get
				{
					return this.systemField;
				}
				set
				{
					this.systemField = value;
				}
			}

			/// <remarks/>
			public IdentType OrganisationType
			{
				get
				{
					return this.organisationTypeField;
				}
				set
				{
					this.organisationTypeField = value;
				}
			}

			/// <remarks/>
			public IdentType Organisation
			{
				get
				{
					return this.organisationField;
				}
				set
				{
					this.organisationField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class OriginatorTypeSystem
		{

			private string nameField;

			private string versionField;

			/// <remarks/>
			public string Name
			{
				get
				{
					return this.nameField;
				}
				set
				{
					this.nameField = value;
				}
			}

			/// <remarks/>
			public string Version
			{
				get
				{
					return this.versionField;
				}
				set
				{
					this.versionField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class RegistrationHistoryType
		{

			private string dateRegisteredField;

			private string dateDeregisteredField;

			private IntegerCodeType patientTypeField;

			/// <remarks/>
			public string DateRegistered
			{
				get
				{
					return this.dateRegisteredField;
				}
				set
				{
					this.dateRegisteredField = value;
				}
			}

			/// <remarks/>
			public string DateDeregistered
			{
				get
				{
					return this.dateDeregisteredField;
				}
				set
				{
					this.dateDeregisteredField = value;
				}
			}

			/// <remarks/>
			public IntegerCodeType PatientType
			{
				get
				{
					return this.patientTypeField;
				}
				set
				{
					this.patientTypeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class IntegerCodeType
		{

			private string valueField;

			private string schemeField;

			private string termField;

			private string oldCodeField;

			private string mapCodeField;

			private string mapSchemeField;

			private StringCodeType interventionTargetField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Value
			{
				get
				{
					return this.valueField;
				}
				set
				{
					this.valueField = value;
				}
			}

			/// <remarks/>
			public string Scheme
			{
				get
				{
					return this.schemeField;
				}
				set
				{
					this.schemeField = value;
				}
			}

			/// <remarks/>
			public string Term
			{
				get
				{
					return this.termField;
				}
				set
				{
					this.termField = value;
				}
			}

			/// <remarks/>
			public string OldCode
			{
				get
				{
					return this.oldCodeField;
				}
				set
				{
					this.oldCodeField = value;
				}
			}

			/// <remarks/>
			public string MapCode
			{
				get
				{
					return this.mapCodeField;
				}
				set
				{
					this.mapCodeField = value;
				}
			}

			/// <remarks/>
			public string MapScheme
			{
				get
				{
					return this.mapSchemeField;
				}
				set
				{
					this.mapSchemeField = value;
				}
			}

			/// <remarks/>
			public StringCodeType InterventionTarget
			{
				get
				{
					return this.interventionTargetField;
				}
				set
				{
					this.interventionTargetField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class StringCodeType
		{

			private string valueField;

			private StringCodeTypeScheme schemeField;

			private bool schemeFieldSpecified;

			private string termField;

			private string oldCodeField;

			private string mapCodeField;

			private string mapSchemeField;

			/// <remarks/>
			public string Value
			{
				get
				{
					return this.valueField;
				}
				set
				{
					this.valueField = value;
				}
			}

			/// <remarks/>
			public StringCodeTypeScheme Scheme
			{
				get
				{
					return this.schemeField;
				}
				set
				{
					this.schemeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool SchemeSpecified
			{
				get
				{
					return this.schemeFieldSpecified;
				}
				set
				{
					this.schemeFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string Term
			{
				get
				{
					return this.termField;
				}
				set
				{
					this.termField = value;
				}
			}

			/// <remarks/>
			public string OldCode
			{
				get
				{
					return this.oldCodeField;
				}
				set
				{
					this.oldCodeField = value;
				}
			}

			/// <remarks/>
			public string MapCode
			{
				get
				{
					return this.mapCodeField;
				}
				set
				{
					this.mapCodeField = value;
				}
			}

			/// <remarks/>
			public string MapScheme
			{
				get
				{
					return this.mapSchemeField;
				}
				set
				{
					this.mapSchemeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum StringCodeTypeScheme
		{
			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("READ2")]
			READ2,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("READ4")]
			READ4,

			/// <remarks/>
			
			EMISCLINICAL,

			/// <remarks/>
			EMISDRUGGROUP,

			/// <remarks/>
			EMISCONSTITUENT,

			/// <remarks/>
			EMISDRUGNAME,

			/// <remarks/>
			EMISBOTH,

			/// <remarks/>
			EMISNONDRUGALLERGY,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class RegistrationStatusType
		{

			private RegistrationStatusTypeCurrentStatus currentStatusField;

			private StatusType[] statusHistoryListField;

			private RegistrationHistoryType[] registrationHistoryListField;

			/// <remarks/>
			public RegistrationStatusTypeCurrentStatus CurrentStatus
			{
				get
				{
					return this.currentStatusField;
				}
				set
				{
					this.currentStatusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("StatusHistory", IsNullable = false)]
			public StatusType[] StatusHistoryList
			{
				get
				{
					return this.statusHistoryListField;
				}
				set
				{
					this.statusHistoryListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("RegistrationHistory", IsNullable = false)]
			public RegistrationHistoryType[] RegistrationHistoryList
			{
				get
				{
					return this.registrationHistoryListField;
				}
				set
				{
					this.registrationHistoryListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class RegistrationStatusTypeCurrentStatus : StatusType
		{

			private string practiceRegisteredField;

			private string hARegisteredField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string PracticeRegistered
			{
				get
				{
					return this.practiceRegisteredField;
				}
				set
				{
					this.practiceRegisteredField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string HARegistered
			{
				get
				{
					return this.hARegisteredField;
				}
				set
				{
					this.hARegisteredField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class StatusType : IdentType
		{

			private IntegerCodeType codeField;

			private string statusDateField;

			private IntegerCodeType patientTypeField;

			/// <remarks/>
			public IntegerCodeType Code
			{
				get
				{
					return this.codeField;
				}
				set
				{
					this.codeField = value;
				}
			}

			/// <remarks/>
			public string StatusDate
			{
				get
				{
					return this.statusDateField;
				}
				set
				{
					this.statusDateField = value;
				}
			}

			/// <remarks/>
			public IntegerCodeType PatientType
			{
				get
				{
					return this.patientTypeField;
				}
				set
				{
					this.patientTypeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class EDIComment
		{

			private string fTXField;

			/// <remarks/>
			public string FTX
			{
				get
				{
					return this.fTXField;
				}
				set
				{
					this.fTXField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class NumericValueType
		{

			private string operatorField;

			private double valueField;

			private string unitsField;

			private string minRangeOperatorField;

			private string maximumRangeOperatorField;

			private double numericMinimumField;

			private bool numericMinimumFieldSpecified;

			private double numericMaximumField;

			private bool numericMaximumFieldSpecified;

			/// <remarks/>
			public string Operator
			{
				get
				{
					return this.operatorField;
				}
				set
				{
					this.operatorField = value;
				}
			}

			/// <remarks/>
			public double Value
			{
				get
				{
					return this.valueField;
				}
				set
				{
					this.valueField = value;
				}
			}

			/// <remarks/>
			public string Units
			{
				get
				{
					return this.unitsField;
				}
				set
				{
					this.unitsField = value;
				}
			}

			/// <remarks/>
			public string MinRangeOperator
			{
				get
				{
					return this.minRangeOperatorField;
				}
				set
				{
					this.minRangeOperatorField = value;
				}
			}

			/// <remarks/>
			public string MaximumRangeOperator
			{
				get
				{
					return this.maximumRangeOperatorField;
				}
				set
				{
					this.maximumRangeOperatorField = value;
				}
			}

			/// <remarks/>
			public double NumericMinimum
			{
				get
				{
					return this.numericMinimumField;
				}
				set
				{
					this.numericMinimumField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool NumericMinimumSpecified
			{
				get
				{
					return this.numericMinimumFieldSpecified;
				}
				set
				{
					this.numericMinimumFieldSpecified = value;
				}
			}

			/// <remarks/>
			public double NumericMaximum
			{
				get
				{
					return this.numericMaximumField;
				}
				set
				{
					this.numericMaximumField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool NumericMaximumSpecified
			{
				get
				{
					return this.numericMaximumFieldSpecified;
				}
				set
				{
					this.numericMaximumFieldSpecified = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class CarePlanType
		{

			private IdentType pKIdField;

			private string displayTermField;

			private StringCodeType termIdField;

			private string notesField;

			private IdentType templateIdField;

			private IdentType interventionCategoryIdField;

			private StringCodeType interventionTargetTermIdField;

			/// <remarks/>
			public IdentType PKId
			{
				get
				{
					return this.pKIdField;
				}
				set
				{
					this.pKIdField = value;
				}
			}

			/// <remarks/>
			public string DisplayTerm
			{
				get
				{
					return this.displayTermField;
				}
				set
				{
					this.displayTermField = value;
				}
			}

			/// <remarks/>
			public StringCodeType TermId
			{
				get
				{
					return this.termIdField;
				}
				set
				{
					this.termIdField = value;
				}
			}

			/// <remarks/>
			public string Notes
			{
				get
				{
					return this.notesField;
				}
				set
				{
					this.notesField = value;
				}
			}

			/// <remarks/>
			public IdentType TemplateId
			{
				get
				{
					return this.templateIdField;
				}
				set
				{
					this.templateIdField = value;
				}
			}

			/// <remarks/>
			public IdentType InterventionCategoryId
			{
				get
				{
					return this.interventionCategoryIdField;
				}
				set
				{
					this.interventionCategoryIdField = value;
				}
			}

			/// <remarks/>
			public StringCodeType InterventionTargetTermId
			{
				get
				{
					return this.interventionTargetTermIdField;
				}
				set
				{
					this.interventionTargetTermIdField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class CareAimType
		{

			private IdentType pKIdField;

			private string goalField;

			private System.DateTime targetDateField;

			private bool targetDateFieldSpecified;

			private string expectedValueField;

			private string actualValueField;

			/// <remarks/>
			public IdentType PKId
			{
				get
				{
					return this.pKIdField;
				}
				set
				{
					this.pKIdField = value;
				}
			}

			/// <remarks/>
			public string Goal
			{
				get
				{
					return this.goalField;
				}
				set
				{
					this.goalField = value;
				}
			}

			/// <remarks/>
			public System.DateTime TargetDate
			{
				get
				{
					return this.targetDateField;
				}
				set
				{
					this.targetDateField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool TargetDateSpecified
			{
				get
				{
					return this.targetDateFieldSpecified;
				}
				set
				{
					this.targetDateFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string ExpectedValue
			{
				get
				{
					return this.expectedValueField;
				}
				set
				{
					this.expectedValueField = value;
				}
			}

			/// <remarks/>
			public string ActualValue
			{
				get
				{
					return this.actualValueField;
				}
				set
				{
					this.actualValueField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ProblemType
		{

			private string problemStatusField;

			private int groupingStatusField;

			private bool groupingStatusFieldSpecified;

			private string endDateField;

			private int endDatePartField;

			private bool endDatePartFieldSpecified;

			private int problemType1Field;

			private bool problemType1FieldSpecified;

			private int significanceField;

			private bool significanceFieldSpecified;

			private string expectedDurationField;

			private IdentType parentProblemField;

			private int ownerField;

			private bool ownerFieldSpecified;

			private CareAimType[] careAimListField;

			private CarePlanType[] carePlanListField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string ProblemStatus
			{
				get
				{
					return this.problemStatusField;
				}
				set
				{
					this.problemStatusField = value;
				}
			}

			/// <remarks/>
			public int GroupingStatus
			{
				get
				{
					return this.groupingStatusField;
				}
				set
				{
					this.groupingStatusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool GroupingStatusSpecified
			{
				get
				{
					return this.groupingStatusFieldSpecified;
				}
				set
				{
					this.groupingStatusFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string EndDate
			{
				get
				{
					return this.endDateField;
				}
				set
				{
					this.endDateField = value;
				}
			}

			/// <remarks/>
			public int EndDatePart
			{
				get
				{
					return this.endDatePartField;
				}
				set
				{
					this.endDatePartField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool EndDatePartSpecified
			{
				get
				{
					return this.endDatePartFieldSpecified;
				}
				set
				{
					this.endDatePartFieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("ProblemType")]
			public int ProblemType1
			{
				get
				{
					return this.problemType1Field;
				}
				set
				{
					this.problemType1Field = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ProblemType1Specified
			{
				get
				{
					return this.problemType1FieldSpecified;
				}
				set
				{
					this.problemType1FieldSpecified = value;
				}
			}

			/// <remarks/>
			public int Significance
			{
				get
				{
					return this.significanceField;
				}
				set
				{
					this.significanceField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool SignificanceSpecified
			{
				get
				{
					return this.significanceFieldSpecified;
				}
				set
				{
					this.significanceFieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string ExpectedDuration
			{
				get
				{
					return this.expectedDurationField;
				}
				set
				{
					this.expectedDurationField = value;
				}
			}

			/// <remarks/>
			public IdentType ParentProblem
			{
				get
				{
					return this.parentProblemField;
				}
				set
				{
					this.parentProblemField = value;
				}
			}

			/// <remarks/>
			public int Owner
			{
				get
				{
					return this.ownerField;
				}
				set
				{
					this.ownerField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool OwnerSpecified
			{
				get
				{
					return this.ownerFieldSpecified;
				}
				set
				{
					this.ownerFieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("CareAim", IsNullable = false)]
			public CareAimType[] CareAimList
			{
				get
				{
					return this.careAimListField;
				}
				set
				{
					this.careAimListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("CarePlan", IsNullable = false)]
			public CarePlanType[] CarePlanList
			{
				get
				{
					return this.carePlanListField;
				}
				set
				{
					this.carePlanListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class QualifierType
		{

			private IntegerCodeType qualifierItemIDField;

			private IntegerCodeType groupField;

			/// <remarks/>
			public IntegerCodeType QualifierItemID
			{
				get
				{
					return this.qualifierItemIDField;
				}
				set
				{
					this.qualifierItemIDField = value;
				}
			}

			/// <remarks/>
			public IntegerCodeType Group
			{
				get
				{
					return this.groupField;
				}
				set
				{
					this.groupField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class LinkType
		{

			private IdentType targetField;

			private LinkTypeLinkType linkType1Field;

			/// <remarks/>
			public IdentType Target
			{
				get
				{
					return this.targetField;
				}
				set
				{
					this.targetField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("LinkType")]
			public LinkTypeLinkType LinkType1
			{
				get
				{
					return this.linkType1Field;
				}
				set
				{
					this.linkType1Field = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum LinkTypeLinkType
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ControlledDrugInfoType
		{

			private string dduBatchField;

			private string dduPrintedField;

			private string dduCollectField;

			private string dduNextDateField;

			private string dduReduceByField;

			private string dduAfterField;

			private string dduUntilField;

			private string dduNextDoseField;

			private string dduOffsetField;

			private string dduClWorkerField;

			private string dduPickUpField;

			private string dduDoseAbbrevField;

			/// <remarks/>
			public string DduBatch
			{
				get
				{
					return this.dduBatchField;
				}
				set
				{
					this.dduBatchField = value;
				}
			}

			/// <remarks/>
			public string DduPrinted
			{
				get
				{
					return this.dduPrintedField;
				}
				set
				{
					this.dduPrintedField = value;
				}
			}

			/// <remarks/>
			public string DduCollect
			{
				get
				{
					return this.dduCollectField;
				}
				set
				{
					this.dduCollectField = value;
				}
			}

			/// <remarks/>
			public string DduNextDate
			{
				get
				{
					return this.dduNextDateField;
				}
				set
				{
					this.dduNextDateField = value;
				}
			}

			/// <remarks/>
			public string DduReduceBy
			{
				get
				{
					return this.dduReduceByField;
				}
				set
				{
					this.dduReduceByField = value;
				}
			}

			/// <remarks/>
			public string DduAfter
			{
				get
				{
					return this.dduAfterField;
				}
				set
				{
					this.dduAfterField = value;
				}
			}

			/// <remarks/>
			public string DduUntil
			{
				get
				{
					return this.dduUntilField;
				}
				set
				{
					this.dduUntilField = value;
				}
			}

			/// <remarks/>
			public string DduNextDose
			{
				get
				{
					return this.dduNextDoseField;
				}
				set
				{
					this.dduNextDoseField = value;
				}
			}

			/// <remarks/>
			public string DduOffset
			{
				get
				{
					return this.dduOffsetField;
				}
				set
				{
					this.dduOffsetField = value;
				}
			}

			/// <remarks/>
			public string DduClWorker
			{
				get
				{
					return this.dduClWorkerField;
				}
				set
				{
					this.dduClWorkerField = value;
				}
			}

			/// <remarks/>
			public string DduPickUp
			{
				get
				{
					return this.dduPickUpField;
				}
				set
				{
					this.dduPickUpField = value;
				}
			}

			/// <remarks/>
			public string DduDoseAbbrev
			{
				get
				{
					return this.dduDoseAbbrevField;
				}
				set
				{
					this.dduDoseAbbrevField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class DrugDeliveryType
		{

			private float doseQuantityField;

			private bool doseQuantityFieldSpecified;

			private string doseUnitsField;

			private string frequencyField;

			private string frequencyUnitsField;

			private float dailyDoseField;

			private bool dailyDoseFieldSpecified;

			private string dailyDoseUnitsField;

			/// <remarks/>
			public float DoseQuantity
			{
				get
				{
					return this.doseQuantityField;
				}
				set
				{
					this.doseQuantityField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DoseQuantitySpecified
			{
				get
				{
					return this.doseQuantityFieldSpecified;
				}
				set
				{
					this.doseQuantityFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string DoseUnits
			{
				get
				{
					return this.doseUnitsField;
				}
				set
				{
					this.doseUnitsField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Frequency
			{
				get
				{
					return this.frequencyField;
				}
				set
				{
					this.frequencyField = value;
				}
			}

			/// <remarks/>
			public string FrequencyUnits
			{
				get
				{
					return this.frequencyUnitsField;
				}
				set
				{
					this.frequencyUnitsField = value;
				}
			}

			/// <remarks/>
			public float DailyDose
			{
				get
				{
					return this.dailyDoseField;
				}
				set
				{
					this.dailyDoseField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DailyDoseSpecified
			{
				get
				{
					return this.dailyDoseFieldSpecified;
				}
				set
				{
					this.dailyDoseFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string DailyDoseUnits
			{
				get
				{
					return this.dailyDoseUnitsField;
				}
				set
				{
					this.dailyDoseUnitsField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MixtureType
		{

			private IdentType localMixtureField;

			private string mixtureNameField;

			private MixtureTypeConstituents[] constituentsField;

			/// <remarks/>
			public IdentType LocalMixture
			{
				get
				{
					return this.localMixtureField;
				}
				set
				{
					this.localMixtureField = value;
				}
			}

			/// <remarks/>
			public string MixtureName
			{
				get
				{
					return this.mixtureNameField;
				}
				set
				{
					this.mixtureNameField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("Constituents")]
			public MixtureTypeConstituents[] Constituents
			{
				get
				{
					return this.constituentsField;
				}
				set
				{
					this.constituentsField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MixtureTypeConstituents
		{

			private IntegerCodeType preparationIDField;

			private string strengthQuantityField;

			/// <remarks/>
			public IntegerCodeType PreparationID
			{
				get
				{
					return this.preparationIDField;
				}
				set
				{
					this.preparationIDField = value;
				}
			}

			/// <remarks/>
			public string StrengthQuantity
			{
				get
				{
					return this.strengthQuantityField;
				}
				set
				{
					this.strengthQuantityField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AuthorType
		{

			private IdentType userField;

			private string systemDateField;

			/// <remarks/>
			public IdentType User
			{
				get
				{
					return this.userField;
				}
				set
				{
					this.userField = value;
				}
			}

			/// <remarks/>
			public string SystemDate
			{
				get
				{
					return this.systemDateField;
				}
				set
				{
					this.systemDateField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class TeamType : IdentType
		{

			private string nameField;

			private StringCodeType specialityField;

			/// <remarks/>
			public string Name
			{
				get
				{
					return this.nameField;
				}
				set
				{
					this.nameField = value;
				}
			}

			/// <remarks/>
			public StringCodeType Speciality
			{
				get
				{
					return this.specialityField;
				}
				set
				{
					this.specialityField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologySpecimenType : IdentType
		{

			private string reportIDField;

			private string specimenIDField;

			private string specimenTypeField;

			private string fastingStatusField;

			private float specimenVolumeField;

			private bool specimenVolumeFieldSpecified;

			private string specimenUnitsField;

			private string collectionProcedureField;

			private string anotomicalOriginField;

			private EDIComment[] specimenTextListField;

			private string sampleDateTimeField;

			private string collectionStartDateField;

			private string collectionEndDateField;

			private string receivedByLabDateTimeField;

			private PathologyInvestigationType[] eDIInvestigationListField;

			/// <remarks/>
			public string ReportID
			{
				get
				{
					return this.reportIDField;
				}
				set
				{
					this.reportIDField = value;
				}
			}

			/// <remarks/>
			public string SpecimenID
			{
				get
				{
					return this.specimenIDField;
				}
				set
				{
					this.specimenIDField = value;
				}
			}

			/// <remarks/>
			public string SpecimenType
			{
				get
				{
					return this.specimenTypeField;
				}
				set
				{
					this.specimenTypeField = value;
				}
			}

			/// <remarks/>
			public string FastingStatus
			{
				get
				{
					return this.fastingStatusField;
				}
				set
				{
					this.fastingStatusField = value;
				}
			}

			/// <remarks/>
			public float SpecimenVolume
			{
				get
				{
					return this.specimenVolumeField;
				}
				set
				{
					this.specimenVolumeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool SpecimenVolumeSpecified
			{
				get
				{
					return this.specimenVolumeFieldSpecified;
				}
				set
				{
					this.specimenVolumeFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string SpecimenUnits
			{
				get
				{
					return this.specimenUnitsField;
				}
				set
				{
					this.specimenUnitsField = value;
				}
			}

			/// <remarks/>
			public string CollectionProcedure
			{
				get
				{
					return this.collectionProcedureField;
				}
				set
				{
					this.collectionProcedureField = value;
				}
			}

			/// <remarks/>
			public string AnotomicalOrigin
			{
				get
				{
					return this.anotomicalOriginField;
				}
				set
				{
					this.anotomicalOriginField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
			public EDIComment[] SpecimenTextList
			{
				get
				{
					return this.specimenTextListField;
				}
				set
				{
					this.specimenTextListField = value;
				}
			}

			/// <remarks/>
			public string SampleDateTime
			{
				get
				{
					return this.sampleDateTimeField;
				}
				set
				{
					this.sampleDateTimeField = value;
				}
			}

			/// <remarks/>
			public string CollectionStartDate
			{
				get
				{
					return this.collectionStartDateField;
				}
				set
				{
					this.collectionStartDateField = value;
				}
			}

			/// <remarks/>
			public string CollectionEndDate
			{
				get
				{
					return this.collectionEndDateField;
				}
				set
				{
					this.collectionEndDateField = value;
				}
			}

			/// <remarks/>
			public string ReceivedByLabDateTime
			{
				get
				{
					return this.receivedByLabDateTimeField;
				}
				set
				{
					this.receivedByLabDateTimeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("EDIInvestigation", IsNullable = false)]
			public PathologyInvestigationType[] EDIInvestigationList
			{
				get
				{
					return this.eDIInvestigationListField;
				}
				set
				{
					this.eDIInvestigationListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyInvestigationType : InvestigationTypeBase
		{

			private string userCommentAppliedByField;

			private EDIComment[] labSpecifiedCommentListField;

			private string eDIResultStatusField;

			private string investigationPerformedDateTimeField;

			private StringCodeType filedCodeField;

			private string invContainsStandAloneTestField;

			private PathologyTestType[] pathologyEventListField;

			/// <remarks/>
			public string UserCommentAppliedBy
			{
				get
				{
					return this.userCommentAppliedByField;
				}
				set
				{
					this.userCommentAppliedByField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
			public EDIComment[] LabSpecifiedCommentList
			{
				get
				{
					return this.labSpecifiedCommentListField;
				}
				set
				{
					this.labSpecifiedCommentListField = value;
				}
			}

			/// <remarks/>
			public string EDIResultStatus
			{
				get
				{
					return this.eDIResultStatusField;
				}
				set
				{
					this.eDIResultStatusField = value;
				}
			}

			/// <remarks/>
			public string InvestigationPerformedDateTime
			{
				get
				{
					return this.investigationPerformedDateTimeField;
				}
				set
				{
					this.investigationPerformedDateTimeField = value;
				}
			}

			/// <remarks/>
			public StringCodeType FiledCode
			{
				get
				{
					return this.filedCodeField;
				}
				set
				{
					this.filedCodeField = value;
				}
			}

			/// <remarks/>
			public string InvContainsStandAloneTest
			{
				get
				{
					return this.invContainsStandAloneTestField;
				}
				set
				{
					this.invContainsStandAloneTestField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("PathologyEvent", IsNullable = false)]
			public PathologyTestType[] PathologyEventList
			{
				get
				{
					return this.pathologyEventListField;
				}
				set
				{
					this.pathologyEventListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyTestType : IdentType
		{

			private string reportIDField;

			private string specimenIDField;

			private string displayTermField;

			private StringCodeType codeField;

			private StringCodeType termIDField;

			private NumericValueType numericValueField;

			private string textValueField;

			private string abnormalIndicatorField;

			private string eDIResultStatusField;

			private EDIComment[] labSpecifiedCommentListField;

			private PathologyTestTypeRangeInformation[] rangeInformationListField;

			private StringCodeType filedCodeField;

			private string testNumberField;

			/// <remarks/>
			public string ReportID
			{
				get
				{
					return this.reportIDField;
				}
				set
				{
					this.reportIDField = value;
				}
			}

			/// <remarks/>
			public string SpecimenID
			{
				get
				{
					return this.specimenIDField;
				}
				set
				{
					this.specimenIDField = value;
				}
			}

			/// <remarks/>
			public string DisplayTerm
			{
				get
				{
					return this.displayTermField;
				}
				set
				{
					this.displayTermField = value;
				}
			}

			/// <remarks/>
			public StringCodeType Code
			{
				get
				{
					return this.codeField;
				}
				set
				{
					this.codeField = value;
				}
			}

			/// <remarks/>
			public StringCodeType TermID
			{
				get
				{
					return this.termIDField;
				}
				set
				{
					this.termIDField = value;
				}
			}

			/// <remarks/>
			public NumericValueType NumericValue
			{
				get
				{
					return this.numericValueField;
				}
				set
				{
					this.numericValueField = value;
				}
			}

			/// <remarks/>
			public string TextValue
			{
				get
				{
					return this.textValueField;
				}
				set
				{
					this.textValueField = value;
				}
			}

			/// <remarks/>
			public string AbnormalIndicator
			{
				get
				{
					return this.abnormalIndicatorField;
				}
				set
				{
					this.abnormalIndicatorField = value;
				}
			}

			/// <remarks/>
			public string EDIResultStatus
			{
				get
				{
					return this.eDIResultStatusField;
				}
				set
				{
					this.eDIResultStatusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
			public EDIComment[] LabSpecifiedCommentList
			{
				get
				{
					return this.labSpecifiedCommentListField;
				}
				set
				{
					this.labSpecifiedCommentListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("RangeInformation", IsNullable = false)]
			public PathologyTestTypeRangeInformation[] RangeInformationList
			{
				get
				{
					return this.rangeInformationListField;
				}
				set
				{
					this.rangeInformationListField = value;
				}
			}

			/// <remarks/>
			public StringCodeType FiledCode
			{
				get
				{
					return this.filedCodeField;
				}
				set
				{
					this.filedCodeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TestNumber
			{
				get
				{
					return this.testNumberField;
				}
				set
				{
					this.testNumberField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyTestTypeRangeInformation
		{

			private string minRangeField;

			private string maxRangeField;

			private EDIComment[] rangeFTXListField;

			private string rangeQualifierField;

			/// <remarks/>
			public string MinRange
			{
				get
				{
					return this.minRangeField;
				}
				set
				{
					this.minRangeField = value;
				}
			}

			/// <remarks/>
			public string MaxRange
			{
				get
				{
					return this.maxRangeField;
				}
				set
				{
					this.maxRangeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
			public EDIComment[] RangeFTXList
			{
				get
				{
					return this.rangeFTXListField;
				}
				set
				{
					this.rangeFTXListField = value;
				}
			}

			/// <remarks/>
			public string RangeQualifier
			{
				get
				{
					return this.rangeQualifierField;
				}
				set
				{
					this.rangeQualifierField = value;
				}
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(PathologyInvestigationType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(InvestigationType))]
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class InvestigationTypeBase : IdentType
		{

			private string displayTermField;

			private string dataSourceField;

			private StringCodeType codeField;

			private StringCodeType termIDField;

			private string abnormalField;

			private object dataTypeField;

			private string reportIDField;

			private string specimenIDField;

			private string specimenTypeField;

			private string numberOfTestsField;

			private string repInvNumberField;

			private string userCommentField;

			/// <remarks/>
			public string DisplayTerm
			{
				get
				{
					return this.displayTermField;
				}
				set
				{
					this.displayTermField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string DataSource
			{
				get
				{
					return this.dataSourceField;
				}
				set
				{
					this.dataSourceField = value;
				}
			}

			/// <remarks/>
			public StringCodeType Code
			{
				get
				{
					return this.codeField;
				}
				set
				{
					this.codeField = value;
				}
			}

			/// <remarks/>
			public StringCodeType TermID
			{
				get
				{
					return this.termIDField;
				}
				set
				{
					this.termIDField = value;
				}
			}

			/// <remarks/>
			public string Abnormal
			{
				get
				{
					return this.abnormalField;
				}
				set
				{
					this.abnormalField = value;
				}
			}

			/// <remarks/>
			public object DataType
			{
				get
				{
					return this.dataTypeField;
				}
				set
				{
					this.dataTypeField = value;
				}
			}

			/// <remarks/>
			public string ReportID
			{
				get
				{
					return this.reportIDField;
				}
				set
				{
					this.reportIDField = value;
				}
			}

			/// <remarks/>
			public string SpecimenID
			{
				get
				{
					return this.specimenIDField;
				}
				set
				{
					this.specimenIDField = value;
				}
			}

			/// <remarks/>
			public string SpecimenType
			{
				get
				{
					return this.specimenTypeField;
				}
				set
				{
					this.specimenTypeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string NumberOfTests
			{
				get
				{
					return this.numberOfTestsField;
				}
				set
				{
					this.numberOfTestsField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string RepInvNumber
			{
				get
				{
					return this.repInvNumberField;
				}
				set
				{
					this.repInvNumberField = value;
				}
			}

			/// <remarks/>
			public string UserComment
			{
				get
				{
					return this.userCommentField;
				}
				set
				{
					this.userCommentField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class InvestigationType : InvestigationTypeBase
		{

			private string assignedDateField;

			private short datePartField;

			private bool datePartFieldSpecified;

			private string assignedTimeField;

			private string descriptiveTextField;

			private AuthorType originalAuthorField;

			private IdentType authorIDField;

			private string policyIDField;

			private EventType[] eventListField;

			/// <remarks/>
			public string AssignedDate
			{
				get
				{
					return this.assignedDateField;
				}
				set
				{
					this.assignedDateField = value;
				}
			}

			/// <remarks/>
			public short DatePart
			{
				get
				{
					return this.datePartField;
				}
				set
				{
					this.datePartField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DatePartSpecified
			{
				get
				{
					return this.datePartFieldSpecified;
				}
				set
				{
					this.datePartFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string AssignedTime
			{
				get
				{
					return this.assignedTimeField;
				}
				set
				{
					this.assignedTimeField = value;
				}
			}

			/// <remarks/>
			public string DescriptiveText
			{
				get
				{
					return this.descriptiveTextField;
				}
				set
				{
					this.descriptiveTextField = value;
				}
			}

			/// <remarks/>
			public AuthorType OriginalAuthor
			{
				get
				{
					return this.originalAuthorField;
				}
				set
				{
					this.originalAuthorField = value;
				}
			}

			/// <remarks/>
			public IdentType AuthorID
			{
				get
				{
					return this.authorIDField;
				}
				set
				{
					this.authorIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string PolicyID
			{
				get
				{
					return this.policyIDField;
				}
				set
				{
					this.policyIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Event", IsNullable = false)]
			public EventType[] EventList
			{
				get
				{
					return this.eventListField;
				}
				set
				{
					this.eventListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class EventType : CodedItemBaseType
		{

			private string groupIDField;

			private sbyte episodicityField;

			private bool episodicityFieldSpecified;

			private NumericValueType numericValueField;

			private object textValueField;

			private string abnormalField;

			private string gMSField;

			private EventTypeEventType eventType1Field;

			private bool eventType1FieldSpecified;

			private string templateIDField;

			private string templateInstanceIDField;

			private string templateComponentNameField;

			private string qualifiedTermField;

			private string qualifiedCodeField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string GroupID
			{
				get
				{
					return this.groupIDField;
				}
				set
				{
					this.groupIDField = value;
				}
			}

			/// <remarks/>
			public sbyte Episodicity
			{
				get
				{
					return this.episodicityField;
				}
				set
				{
					this.episodicityField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool EpisodicitySpecified
			{
				get
				{
					return this.episodicityFieldSpecified;
				}
				set
				{
					this.episodicityFieldSpecified = value;
				}
			}

			/// <remarks/>
			public NumericValueType NumericValue
			{
				get
				{
					return this.numericValueField;
				}
				set
				{
					this.numericValueField = value;
				}
			}

			/// <remarks/>
			public object TextValue
			{
				get
				{
					return this.textValueField;
				}
				set
				{
					this.textValueField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Abnormal
			{
				get
				{
					return this.abnormalField;
				}
				set
				{
					this.abnormalField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string GMS
			{
				get
				{
					return this.gMSField;
				}
				set
				{
					this.gMSField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("EventType")]
			public EventTypeEventType EventType1
			{
				get
				{
					return this.eventType1Field;
				}
				set
				{
					this.eventType1Field = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool EventType1Specified
			{
				get
				{
					return this.eventType1FieldSpecified;
				}
				set
				{
					this.eventType1FieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TemplateID
			{
				get
				{
					return this.templateIDField;
				}
				set
				{
					this.templateIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TemplateInstanceID
			{
				get
				{
					return this.templateInstanceIDField;
				}
				set
				{
					this.templateInstanceIDField = value;
				}
			}

			/// <remarks/>
			public string TemplateComponentName
			{
				get
				{
					return this.templateComponentNameField;
				}
				set
				{
					this.templateComponentNameField = value;
				}
			}

			/// <remarks/>
			public string QualifiedTerm
			{
				get
				{
					return this.qualifiedTermField;
				}
				set
				{
					this.qualifiedTermField = value;
				}
			}

			/// <remarks/>
			public string QualifiedCode
			{
				get
				{
					return this.qualifiedCodeField;
				}
				set
				{
					this.qualifiedCodeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum EventTypeEventType
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("3")]
			Item3,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("4")]
			Item4,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("5")]
			Item5,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("6")]
			Item6,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("7")]
			Item7,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("8")]
			Item8,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("9")]
			Item9,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("10")]
			Item10,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("11")]
			Item11,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("12")]
			Item12,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("13")]
			Item13,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AlertType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(TestRequestType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttachmentType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AllergyType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReferralType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(DiaryType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventType))]
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class CodedItemBaseType : ItemBaseType
		{

			private StringCodeType codeField;

			private StringCodeType termIDField;

			private QualifierType[] qualifierListField;

			private ProblemType problemField;

			private LinkType[] problemLinkListField;

			private LinkType[] referralLinkListField;

			/// <remarks/>
			public StringCodeType Code
			{
				get
				{
					return this.codeField;
				}
				set
				{
					this.codeField = value;
				}
			}

			/// <remarks/>
			public StringCodeType TermID
			{
				get
				{
					return this.termIDField;
				}
				set
				{
					this.termIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Qualifier", IsNullable = false)]
			public QualifierType[] QualifierList
			{
				get
				{
					return this.qualifierListField;
				}
				set
				{
					this.qualifierListField = value;
				}
			}

			/// <remarks/>
			public ProblemType Problem
			{
				get
				{
					return this.problemField;
				}
				set
				{
					this.problemField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Link", IsNullable = false)]
			public LinkType[] ProblemLinkList
			{
				get
				{
					return this.problemLinkListField;
				}
				set
				{
					this.problemLinkListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Link", IsNullable = false)]
			public LinkType[] ReferralLinkList
			{
				get
				{
					return this.referralLinkListField;
				}
				set
				{
					this.referralLinkListField = value;
				}
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(CodedItemBaseType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AlertType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(TestRequestType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttachmentType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(AllergyType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReferralType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(DiaryType))]
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(EventType))]
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ItemBaseType : IdentType
		{

			private string assignedDateField;

			private short datePartField;

			private bool datePartFieldSpecified;

			private string assignedTimeField;

			private string sequenceField;

			private IdentType authorIDField;

			private AuthorType originalAuthorField;

			private string externalConsultantField;

			private string displayTermField;

			private string descriptiveTextField;

			private string dataSourceField;

			private string policyIDField;

			/// <remarks/>
			public string AssignedDate
			{
				get
				{
					return this.assignedDateField;
				}
				set
				{
					this.assignedDateField = value;
				}
			}

			/// <remarks/>
			public short DatePart
			{
				get
				{
					return this.datePartField;
				}
				set
				{
					this.datePartField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DatePartSpecified
			{
				get
				{
					return this.datePartFieldSpecified;
				}
				set
				{
					this.datePartFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string AssignedTime
			{
				get
				{
					return this.assignedTimeField;
				}
				set
				{
					this.assignedTimeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Sequence
			{
				get
				{
					return this.sequenceField;
				}
				set
				{
					this.sequenceField = value;
				}
			}

			/// <remarks/>
			public IdentType AuthorID
			{
				get
				{
					return this.authorIDField;
				}
				set
				{
					this.authorIDField = value;
				}
			}

			/// <remarks/>
			public AuthorType OriginalAuthor
			{
				get
				{
					return this.originalAuthorField;
				}
				set
				{
					this.originalAuthorField = value;
				}
			}

			/// <remarks/>
			public string ExternalConsultant
			{
				get
				{
					return this.externalConsultantField;
				}
				set
				{
					this.externalConsultantField = value;
				}
			}

			/// <remarks/>
			public string DisplayTerm
			{
				get
				{
					return this.displayTermField;
				}
				set
				{
					this.displayTermField = value;
				}
			}

			/// <remarks/>
			public string DescriptiveText
			{
				get
				{
					return this.descriptiveTextField;
				}
				set
				{
					this.descriptiveTextField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string DataSource
			{
				get
				{
					return this.dataSourceField;
				}
				set
				{
					this.dataSourceField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string PolicyID
			{
				get
				{
					return this.policyIDField;
				}
				set
				{
					this.policyIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AlertType : CodedItemBaseType
		{

			private string bookField;

			private string ariveSwapField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Book
			{
				get
				{
					return this.bookField;
				}
				set
				{
					this.bookField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string AriveSwap
			{
				get
				{
					return this.ariveSwapField;
				}
				set
				{
					this.ariveSwapField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class TestRequestType : CodedItemBaseType
		{

			private string referenceField;

			private IdentType requestHeaderIDField;

			private string specimenTypeField;

			private StringCodeType specimenTypeCodeField;

			private string collectionDateField;

			private TestRequestTypeStatus statusField;

			private bool statusFieldSpecified;

			private string lastStatusDateField;

			private IdentType resultsRefField;

			private IdentType[] investigationRefField;

			private string resultsDateField;

			/// <remarks/>
			public string Reference
			{
				get
				{
					return this.referenceField;
				}
				set
				{
					this.referenceField = value;
				}
			}

			/// <remarks/>
			public IdentType RequestHeaderID
			{
				get
				{
					return this.requestHeaderIDField;
				}
				set
				{
					this.requestHeaderIDField = value;
				}
			}

			/// <remarks/>
			public string SpecimenType
			{
				get
				{
					return this.specimenTypeField;
				}
				set
				{
					this.specimenTypeField = value;
				}
			}

			/// <remarks/>
			public StringCodeType SpecimenTypeCode
			{
				get
				{
					return this.specimenTypeCodeField;
				}
				set
				{
					this.specimenTypeCodeField = value;
				}
			}

			/// <remarks/>
			public string CollectionDate
			{
				get
				{
					return this.collectionDateField;
				}
				set
				{
					this.collectionDateField = value;
				}
			}

			/// <remarks/>
			public TestRequestTypeStatus Status
			{
				get
				{
					return this.statusField;
				}
				set
				{
					this.statusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool StatusSpecified
			{
				get
				{
					return this.statusFieldSpecified;
				}
				set
				{
					this.statusFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string LastStatusDate
			{
				get
				{
					return this.lastStatusDateField;
				}
				set
				{
					this.lastStatusDateField = value;
				}
			}

			/// <remarks/>
			public IdentType ResultsRef
			{
				get
				{
					return this.resultsRefField;
				}
				set
				{
					this.resultsRefField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("InvestigationRef")]
			public IdentType[] InvestigationRef
			{
				get
				{
					return this.investigationRefField;
				}
				set
				{
					this.investigationRefField = value;
				}
			}

			/// <remarks/>
			public string ResultsDate
			{
				get
				{
					return this.resultsDateField;
				}
				set
				{
					this.resultsDateField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum TestRequestTypeStatus
		{

			/// <remarks/>
			S,

			/// <remarks/>
			R,

			/// <remarks/>
			D,

			/// <remarks/>
			C,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AttachmentType : CodedItemBaseType
		{

			private string titleField;

			private string addressField;

			private string versionField;

			private string dDSIdentifierField;

			/// <remarks/>
			public string Title
			{
				get
				{
					return this.titleField;
				}
				set
				{
					this.titleField = value;
				}
			}

			/// <remarks/>
			public string Address
			{
				get
				{
					return this.addressField;
				}
				set
				{
					this.addressField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Version
			{
				get
				{
					return this.versionField;
				}
				set
				{
					this.versionField = value;
				}
			}

			/// <remarks/>
			public string DDSIdentifier
			{
				get
				{
					return this.dDSIdentifierField;
				}
				set
				{
					this.dDSIdentifierField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AllergyType : CodedItemBaseType
		{

			private sbyte episodicityField;

			private bool episodicityFieldSpecified;

			private sbyte allergyType1Field;

			private bool allergyType1FieldSpecified;

			private StringCodeType codesField;

			private string excludeField;

			/// <remarks/>
			public sbyte Episodicity
			{
				get
				{
					return this.episodicityField;
				}
				set
				{
					this.episodicityField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool EpisodicitySpecified
			{
				get
				{
					return this.episodicityFieldSpecified;
				}
				set
				{
					this.episodicityFieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("AllergyType")]
			public sbyte AllergyType1
			{
				get
				{
					return this.allergyType1Field;
				}
				set
				{
					this.allergyType1Field = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool AllergyType1Specified
			{
				get
				{
					return this.allergyType1FieldSpecified;
				}
				set
				{
					this.allergyType1FieldSpecified = value;
				}
			}

			/// <remarks/>
			public StringCodeType Codes
			{
				get
				{
					return this.codesField;
				}
				set
				{
					this.codesField = value;
				}
			}

			/// <remarks/>
			public string Exclude
			{
				get
				{
					return this.excludeField;
				}
				set
				{
					this.excludeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ReferralType : CodedItemBaseType
		{

			private IdentType consultantField;

			private IdentType providerField;

			private StringCodeType specialityField;

			private ReferralTypeRequestType requestTypeField;

			private bool requestTypeFieldSpecified;

			private IdentType teamField;

			private string referralReasonField;

			private ReferralTypeCommunity communityField;

			private bool communityFieldSpecified;

			private ReferralTypeUrgency urgencyField;

			private bool urgencyFieldSpecified;

			private ReferralTypeNHS nHSField;

			private bool nHSFieldSpecified;

			private ReferralTypeTransport transportField;

			private bool transportFieldSpecified;

			private string referralRefField;

			private IdentType sourceTypeField;

			private string directionField;

			private IdentType sourceLocationField;

			private System.DateTime datedReferralField;

			private bool datedReferralFieldSpecified;

			private sbyte rejectedField;

			private bool rejectedFieldSpecified;

			private string rejectionReasonField;

			private sbyte acceptedField;

			private bool acceptedFieldSpecified;

			private sbyte assessedField;

			private bool assessedFieldSpecified;

			private StringCodeType reasonTermField;

			private string sourceDescriptionField;

			private IdentType sourceSpecialityField;

			private string referralModeField;

			/// <remarks/>
			public IdentType Consultant
			{
				get
				{
					return this.consultantField;
				}
				set
				{
					this.consultantField = value;
				}
			}

			/// <remarks/>
			public IdentType Provider
			{
				get
				{
					return this.providerField;
				}
				set
				{
					this.providerField = value;
				}
			}

			/// <remarks/>
			public StringCodeType Speciality
			{
				get
				{
					return this.specialityField;
				}
				set
				{
					this.specialityField = value;
				}
			}

			/// <remarks/>
			public ReferralTypeRequestType RequestType
			{
				get
				{
					return this.requestTypeField;
				}
				set
				{
					this.requestTypeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool RequestTypeSpecified
			{
				get
				{
					return this.requestTypeFieldSpecified;
				}
				set
				{
					this.requestTypeFieldSpecified = value;
				}
			}

			/// <remarks/>
			public IdentType Team
			{
				get
				{
					return this.teamField;
				}
				set
				{
					this.teamField = value;
				}
			}

			/// <remarks/>
			public string ReferralReason
			{
				get
				{
					return this.referralReasonField;
				}
				set
				{
					this.referralReasonField = value;
				}
			}

			/// <remarks/>
			public ReferralTypeCommunity Community
			{
				get
				{
					return this.communityField;
				}
				set
				{
					this.communityField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool CommunitySpecified
			{
				get
				{
					return this.communityFieldSpecified;
				}
				set
				{
					this.communityFieldSpecified = value;
				}
			}

			/// <remarks/>
			public ReferralTypeUrgency Urgency
			{
				get
				{
					return this.urgencyField;
				}
				set
				{
					this.urgencyField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool UrgencySpecified
			{
				get
				{
					return this.urgencyFieldSpecified;
				}
				set
				{
					this.urgencyFieldSpecified = value;
				}
			}

			/// <remarks/>
			public ReferralTypeNHS NHS
			{
				get
				{
					return this.nHSField;
				}
				set
				{
					this.nHSField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool NHSSpecified
			{
				get
				{
					return this.nHSFieldSpecified;
				}
				set
				{
					this.nHSFieldSpecified = value;
				}
			}

			/// <remarks/>
			public ReferralTypeTransport Transport
			{
				get
				{
					return this.transportField;
				}
				set
				{
					this.transportField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool TransportSpecified
			{
				get
				{
					return this.transportFieldSpecified;
				}
				set
				{
					this.transportFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string ReferralRef
			{
				get
				{
					return this.referralRefField;
				}
				set
				{
					this.referralRefField = value;
				}
			}

			/// <remarks/>
			public IdentType SourceType
			{
				get
				{
					return this.sourceTypeField;
				}
				set
				{
					this.sourceTypeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Direction
			{
				get
				{
					return this.directionField;
				}
				set
				{
					this.directionField = value;
				}
			}

			/// <remarks/>
			public IdentType SourceLocation
			{
				get
				{
					return this.sourceLocationField;
				}
				set
				{
					this.sourceLocationField = value;
				}
			}

			/// <remarks/>
			public System.DateTime DatedReferral
			{
				get
				{
					return this.datedReferralField;
				}
				set
				{
					this.datedReferralField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DatedReferralSpecified
			{
				get
				{
					return this.datedReferralFieldSpecified;
				}
				set
				{
					this.datedReferralFieldSpecified = value;
				}
			}

			/// <remarks/>
			public sbyte Rejected
			{
				get
				{
					return this.rejectedField;
				}
				set
				{
					this.rejectedField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool RejectedSpecified
			{
				get
				{
					return this.rejectedFieldSpecified;
				}
				set
				{
					this.rejectedFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string RejectionReason
			{
				get
				{
					return this.rejectionReasonField;
				}
				set
				{
					this.rejectionReasonField = value;
				}
			}

			/// <remarks/>
			public sbyte Accepted
			{
				get
				{
					return this.acceptedField;
				}
				set
				{
					this.acceptedField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool AcceptedSpecified
			{
				get
				{
					return this.acceptedFieldSpecified;
				}
				set
				{
					this.acceptedFieldSpecified = value;
				}
			}

			/// <remarks/>
			public sbyte Assessed
			{
				get
				{
					return this.assessedField;
				}
				set
				{
					this.assessedField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool AssessedSpecified
			{
				get
				{
					return this.assessedFieldSpecified;
				}
				set
				{
					this.assessedFieldSpecified = value;
				}
			}

			/// <remarks/>
			public StringCodeType ReasonTerm
			{
				get
				{
					return this.reasonTermField;
				}
				set
				{
					this.reasonTermField = value;
				}
			}

			/// <remarks/>
			public string SourceDescription
			{
				get
				{
					return this.sourceDescriptionField;
				}
				set
				{
					this.sourceDescriptionField = value;
				}
			}

			/// <remarks/>
			public IdentType SourceSpeciality
			{
				get
				{
					return this.sourceSpecialityField;
				}
				set
				{
					this.sourceSpecialityField = value;
				}
			}

			/// <remarks/>
			public string ReferralMode
			{
				get
				{
					return this.referralModeField;
				}
				set
				{
					this.referralModeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum ReferralTypeRequestType
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("3")]
			Item3,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("5")]
			Item5,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("6")]
			Item6,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("7")]
			Item7,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum ReferralTypeCommunity
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum ReferralTypeUrgency
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum ReferralTypeNHS
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum ReferralTypeTransport
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class DiaryType : CodedItemBaseType
		{

			private string templateIDField;

			private string templateInstanceIDField;

			private string templateComponentNameField;

			private string durationTermField;

			private string reminderField;

			private string reminderTypeField;

			private IdentType locationTypeIDField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TemplateID
			{
				get
				{
					return this.templateIDField;
				}
				set
				{
					this.templateIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TemplateInstanceID
			{
				get
				{
					return this.templateInstanceIDField;
				}
				set
				{
					this.templateInstanceIDField = value;
				}
			}

			/// <remarks/>
			public string TemplateComponentName
			{
				get
				{
					return this.templateComponentNameField;
				}
				set
				{
					this.templateComponentNameField = value;
				}
			}

			/// <remarks/>
			public string DurationTerm
			{
				get
				{
					return this.durationTermField;
				}
				set
				{
					this.durationTermField = value;
				}
			}

			/// <remarks/>
			public string Reminder
			{
				get
				{
					return this.reminderField;
				}
				set
				{
					this.reminderField = value;
				}
			}

			/// <remarks/>
			public string ReminderType
			{
				get
				{
					return this.reminderTypeField;
				}
				set
				{
					this.reminderTypeField = value;
				}
			}

			/// <remarks/>
			public IdentType LocationTypeID
			{
				get
				{
					return this.locationTypeIDField;
				}
				set
				{
					this.locationTypeIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class StructuredIdentType : IdentType
		{

			private object itemField;

			private ItemChoiceType itemElementNameField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("AgreedID", typeof(string))]
			[System.Xml.Serialization.XmlElementAttribute("Name", typeof(string))]
			[System.Xml.Serialization.XmlElementAttribute("NationalCode", typeof(StructuredIdentTypeNationalCode))]
			[System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
			public object Item
			{
				get
				{
					return this.itemField;
				}
				set
				{
					this.itemField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public ItemChoiceType ItemElementName
			{
				get
				{
					return this.itemElementNameField;
				}
				set
				{
					this.itemElementNameField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class StructuredIdentTypeNationalCode
		{

			private string codeSchemeField;

			private string codeField;

			/// <remarks/>
			public string CodeScheme
			{
				get
				{
					return this.codeSchemeField;
				}
				set
				{
					this.codeSchemeField = value;
				}
			}

			/// <remarks/>
			public string Code
			{
				get
				{
					return this.codeField;
				}
				set
				{
					this.codeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord", IncludeInSchema = false)]
		public enum ItemChoiceType
		{

			/// <remarks/>
			AgreedID,

			/// <remarks/>
			Name,

			/// <remarks/>
			NationalCode,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportType : IdentType
		{

			private string reportDataTypeField;

			private string reportIDField;

			private string reportReceivedDateTimeField;

			private string reportIssueDateTimeField;

			private EDIComment[] reportTextListField;

			private string abnormalField;

			private string recordFileStatusField;

			private string consultationRefField;

			private string boxField;

			private string requestIDField;

			private string requestDateTimeField;

			private StructuredIdentType currentOwnerField;

			private PathologyReportTypeIdentifiers identifiersField;

			private PathologyReportTypeOriginalRequestor originalRequestorField;

			private StructuredIdentType messageRecepientField;

			private PathologyReportTypeServiceProvider serviceProviderField;

			private PathologyReportTypeOriginalMessageDetails originalMessageDetailsField;

			private EDIComment[] clinicalInformationListField;

			private object viewedByField;

			private object trueReportIDField;

			private PathologySpecimenType[] specimenListField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string ReportDataType
			{
				get
				{
					return this.reportDataTypeField;
				}
				set
				{
					this.reportDataTypeField = value;
				}
			}

			/// <remarks/>
			public string ReportID
			{
				get
				{
					return this.reportIDField;
				}
				set
				{
					this.reportIDField = value;
				}
			}

			/// <remarks/>
			public string ReportReceivedDateTime
			{
				get
				{
					return this.reportReceivedDateTimeField;
				}
				set
				{
					this.reportReceivedDateTimeField = value;
				}
			}

			/// <remarks/>
			public string ReportIssueDateTime
			{
				get
				{
					return this.reportIssueDateTimeField;
				}
				set
				{
					this.reportIssueDateTimeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
			public EDIComment[] ReportTextList
			{
				get
				{
					return this.reportTextListField;
				}
				set
				{
					this.reportTextListField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Abnormal
			{
				get
				{
					return this.abnormalField;
				}
				set
				{
					this.abnormalField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string RecordFileStatus
			{
				get
				{
					return this.recordFileStatusField;
				}
				set
				{
					this.recordFileStatusField = value;
				}
			}

			/// <remarks/>
			public string ConsultationRef
			{
				get
				{
					return this.consultationRefField;
				}
				set
				{
					this.consultationRefField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Box
			{
				get
				{
					return this.boxField;
				}
				set
				{
					this.boxField = value;
				}
			}

			/// <remarks/>
			public string RequestID
			{
				get
				{
					return this.requestIDField;
				}
				set
				{
					this.requestIDField = value;
				}
			}

			/// <remarks/>
			public string RequestDateTime
			{
				get
				{
					return this.requestDateTimeField;
				}
				set
				{
					this.requestDateTimeField = value;
				}
			}

			/// <remarks/>
			public StructuredIdentType CurrentOwner
			{
				get
				{
					return this.currentOwnerField;
				}
				set
				{
					this.currentOwnerField = value;
				}
			}

			/// <remarks/>
			public PathologyReportTypeIdentifiers Identifiers
			{
				get
				{
					return this.identifiersField;
				}
				set
				{
					this.identifiersField = value;
				}
			}

			/// <remarks/>
			public PathologyReportTypeOriginalRequestor OriginalRequestor
			{
				get
				{
					return this.originalRequestorField;
				}
				set
				{
					this.originalRequestorField = value;
				}
			}

			/// <remarks/>
			public StructuredIdentType MessageRecepient
			{
				get
				{
					return this.messageRecepientField;
				}
				set
				{
					this.messageRecepientField = value;
				}
			}

			/// <remarks/>
			public PathologyReportTypeServiceProvider ServiceProvider
			{
				get
				{
					return this.serviceProviderField;
				}
				set
				{
					this.serviceProviderField = value;
				}
			}

			/// <remarks/>
			public PathologyReportTypeOriginalMessageDetails OriginalMessageDetails
			{
				get
				{
					return this.originalMessageDetailsField;
				}
				set
				{
					this.originalMessageDetailsField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
			public EDIComment[] ClinicalInformationList
			{
				get
				{
					return this.clinicalInformationListField;
				}
				set
				{
					this.clinicalInformationListField = value;
				}
			}

			/// <remarks/>
			public object ViewedBy
			{
				get
				{
					return this.viewedByField;
				}
				set
				{
					this.viewedByField = value;
				}
			}

			/// <remarks/>
			public object TrueReportID
			{
				get
				{
					return this.trueReportIDField;
				}
				set
				{
					this.trueReportIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Specimen", IsNullable = false)]
			public PathologySpecimenType[] SpecimenList
			{
				get
				{
					return this.specimenListField;
				}
				set
				{
					this.specimenListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportTypeIdentifiers
		{

			private PathologyReportTypeIdentifiersOriginalDetails originalDetailsField;

			private PathologyReportTypeIdentifiersMatchedDetails matchedDetailsField;

			/// <remarks/>
			public PathologyReportTypeIdentifiersOriginalDetails OriginalDetails
			{
				get
				{
					return this.originalDetailsField;
				}
				set
				{
					this.originalDetailsField = value;
				}
			}

			/// <remarks/>
			public PathologyReportTypeIdentifiersMatchedDetails MatchedDetails
			{
				get
				{
					return this.matchedDetailsField;
				}
				set
				{
					this.matchedDetailsField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportTypeIdentifiersOriginalDetails
		{

			private string forenameField;

			private string lastNameField;

			private string dateOfBirthField;

			private string nHSField;

			private string addressField;

			private string sexField;

			private string labSubjectIDField;

			/// <remarks/>
			public string Forename
			{
				get
				{
					return this.forenameField;
				}
				set
				{
					this.forenameField = value;
				}
			}

			/// <remarks/>
			public string LastName
			{
				get
				{
					return this.lastNameField;
				}
				set
				{
					this.lastNameField = value;
				}
			}

			/// <remarks/>
			public string DateOfBirth
			{
				get
				{
					return this.dateOfBirthField;
				}
				set
				{
					this.dateOfBirthField = value;
				}
			}

			/// <remarks/>
			public string NHS
			{
				get
				{
					return this.nHSField;
				}
				set
				{
					this.nHSField = value;
				}
			}

			/// <remarks/>
			public string Address
			{
				get
				{
					return this.addressField;
				}
				set
				{
					this.addressField = value;
				}
			}

			/// <remarks/>
			public string Sex
			{
				get
				{
					return this.sexField;
				}
				set
				{
					this.sexField = value;
				}
			}

			/// <remarks/>
			public string LabSubjectID
			{
				get
				{
					return this.labSubjectIDField;
				}
				set
				{
					this.labSubjectIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportTypeIdentifiersMatchedDetails
		{

			private string forenameField;

			private string lastNameField;

			private string dateOfBirthField;

			private string nHSField;

			private string patientIDField;

			private string sexField;

			/// <remarks/>
			public string Forename
			{
				get
				{
					return this.forenameField;
				}
				set
				{
					this.forenameField = value;
				}
			}

			/// <remarks/>
			public string LastName
			{
				get
				{
					return this.lastNameField;
				}
				set
				{
					this.lastNameField = value;
				}
			}

			/// <remarks/>
			public string DateOfBirth
			{
				get
				{
					return this.dateOfBirthField;
				}
				set
				{
					this.dateOfBirthField = value;
				}
			}

			/// <remarks/>
			public string NHS
			{
				get
				{
					return this.nHSField;
				}
				set
				{
					this.nHSField = value;
				}
			}

			/// <remarks/>
			public string PatientID
			{
				get
				{
					return this.patientIDField;
				}
				set
				{
					this.patientIDField = value;
				}
			}

			/// <remarks/>
			public string Sex
			{
				get
				{
					return this.sexField;
				}
				set
				{
					this.sexField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportTypeOriginalRequestor
		{

			private StructuredIdentType itemField;

			private ItemChoiceType1 itemElementNameField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("Organisation", typeof(StructuredIdentType))]
			[System.Xml.Serialization.XmlElementAttribute("Person", typeof(StructuredIdentType))]
			[System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
			public StructuredIdentType Item
			{
				get
				{
					return this.itemField;
				}
				set
				{
					this.itemField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public ItemChoiceType1 ItemElementName
			{
				get
				{
					return this.itemElementNameField;
				}
				set
				{
					this.itemElementNameField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord", IncludeInSchema = false)]
		public enum ItemChoiceType1
		{

			/// <remarks/>
			Organisation,

			/// <remarks/>
			Person,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportTypeServiceProvider
		{

			private StructuredIdentType departmentField;

			private StructuredIdentType organisationField;

			private StructuredIdentType personField;

			/// <remarks/>
			public StructuredIdentType Department
			{
				get
				{
					return this.departmentField;
				}
				set
				{
					this.departmentField = value;
				}
			}

			/// <remarks/>
			public StructuredIdentType Organisation
			{
				get
				{
					return this.organisationField;
				}
				set
				{
					this.organisationField = value;
				}
			}

			/// <remarks/>
			public StructuredIdentType Person
			{
				get
				{
					return this.personField;
				}
				set
				{
					this.personField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PathologyReportTypeOriginalMessageDetails
		{

			private string senderEDICodeField;

			private string receiverEDICodeField;

			private string interChangeField;

			private string messageIDField;

			private PathologyReportTypeOriginalMessageDetailsMessageType messageTypeField;

			private bool messageTypeFieldSpecified;

			/// <remarks/>
			public string SenderEDICode
			{
				get
				{
					return this.senderEDICodeField;
				}
				set
				{
					this.senderEDICodeField = value;
				}
			}

			/// <remarks/>
			public string ReceiverEDICode
			{
				get
				{
					return this.receiverEDICodeField;
				}
				set
				{
					this.receiverEDICodeField = value;
				}
			}

			/// <remarks/>
			public string InterChange
			{
				get
				{
					return this.interChangeField;
				}
				set
				{
					this.interChangeField = value;
				}
			}

			/// <remarks/>
			public string MessageID
			{
				get
				{
					return this.messageIDField;
				}
				set
				{
					this.messageIDField = value;
				}
			}

			/// <remarks/>
			public PathologyReportTypeOriginalMessageDetailsMessageType MessageType
			{
				get
				{
					return this.messageTypeField;
				}
				set
				{
					this.messageTypeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool MessageTypeSpecified
			{
				get
				{
					return this.messageTypeFieldSpecified;
				}
				set
				{
					this.messageTypeFieldSpecified = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PathologyReportTypeOriginalMessageDetailsMessageType
		{

			/// <remarks/>
			ASTM,

			/// <remarks/>
			MEDRPT,

			/// <remarks/>
			NHS002,

			/// <remarks/>
			NHS003,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class TypeOfLocationType : IdentType
		{

			private string descriptionField;

			/// <remarks/>
			public string Description
			{
				get
				{
					return this.descriptionField;
				}
				set
				{
					this.descriptionField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class LocationType : IdentType
		{

			private string locationNameField;

			private string nationalCodeField;

			private IdentType locationTypeIDField;

			private AddressType addressField;

			private string telephone1Field;

			private string telephone2Field;

			private string faxField;

			private string emailField;

			private string contactField;

			private string cypherField;

			private string interchangeField;

			private string linkCodeField;

			private string seniorPartnerField;

			private string pCGCodeField;

			private string pCTCodeField;

			private LocationTypeGPLinkCode[] gPLinkCodeListField;

			/// <remarks/>
			public string LocationName
			{
				get
				{
					return this.locationNameField;
				}
				set
				{
					this.locationNameField = value;
				}
			}

			/// <remarks/>
			public string NationalCode
			{
				get
				{
					return this.nationalCodeField;
				}
				set
				{
					this.nationalCodeField = value;
				}
			}

			/// <remarks/>
			public IdentType LocationTypeID
			{
				get
				{
					return this.locationTypeIDField;
				}
				set
				{
					this.locationTypeIDField = value;
				}
			}

			/// <remarks/>
			public AddressType Address
			{
				get
				{
					return this.addressField;
				}
				set
				{
					this.addressField = value;
				}
			}

			/// <remarks/>
			public string Telephone1
			{
				get
				{
					return this.telephone1Field;
				}
				set
				{
					this.telephone1Field = value;
				}
			}

			/// <remarks/>
			public string Telephone2
			{
				get
				{
					return this.telephone2Field;
				}
				set
				{
					this.telephone2Field = value;
				}
			}

			/// <remarks/>
			public string Fax
			{
				get
				{
					return this.faxField;
				}
				set
				{
					this.faxField = value;
				}
			}

			/// <remarks/>
			public string Email
			{
				get
				{
					return this.emailField;
				}
				set
				{
					this.emailField = value;
				}
			}

			/// <remarks/>
			public string Contact
			{
				get
				{
					return this.contactField;
				}
				set
				{
					this.contactField = value;
				}
			}

			/// <remarks/>
			public string Cypher
			{
				get
				{
					return this.cypherField;
				}
				set
				{
					this.cypherField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Interchange
			{
				get
				{
					return this.interchangeField;
				}
				set
				{
					this.interchangeField = value;
				}
			}

			/// <remarks/>
			public string LinkCode
			{
				get
				{
					return this.linkCodeField;
				}
				set
				{
					this.linkCodeField = value;
				}
			}

			/// <remarks/>
			public string SeniorPartner
			{
				get
				{
					return this.seniorPartnerField;
				}
				set
				{
					this.seniorPartnerField = value;
				}
			}

			/// <remarks/>
			public string PCGCode
			{
				get
				{
					return this.pCGCodeField;
				}
				set
				{
					this.pCGCodeField = value;
				}
			}

			/// <remarks/>
			public string PCTCode
			{
				get
				{
					return this.pCTCodeField;
				}
				set
				{
					this.pCTCodeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("GPLinkCode", IsNullable = false)]
			public LocationTypeGPLinkCode[] GPLinkCodeList
			{
				get
				{
					return this.gPLinkCodeListField;
				}
				set
				{
					this.gPLinkCodeListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class LocationTypeGPLinkCode : IdentType
		{

			private string linkCodeField;

			/// <remarks/>
			public string LinkCode
			{
				get
				{
					return this.linkCodeField;
				}
				set
				{
					this.linkCodeField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ApplicationType : IdentType
		{

			private string applicationMnemonicField;

			private string applicationNameField;

			/// <remarks/>
			public string ApplicationMnemonic
			{
				get
				{
					return this.applicationMnemonicField;
				}
				set
				{
					this.applicationMnemonicField = value;
				}
			}

			/// <remarks/>
			public string ApplicationName
			{
				get
				{
					return this.applicationNameField;
				}
				set
				{
					this.applicationNameField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class RoleType : IdentType
		{

			private ApplicationType applicationField;

			private IdentType teamField;

			private IdentType locationField;

			/// <remarks/>
			public ApplicationType Application
			{
				get
				{
					return this.applicationField;
				}
				set
				{
					this.applicationField = value;
				}
			}

			/// <remarks/>
			public IdentType Team
			{
				get
				{
					return this.teamField;
				}
				set
				{
					this.teamField = value;
				}
			}

			/// <remarks/>
			public IdentType Location
			{
				get
				{
					return this.locationField;
				}
				set
				{
					this.locationField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PersonCategoryType : IdentType
		{

			private string mnemonicField;

			private string descriptionField;

			private PersonCategoryTypePurpose purposeField;

			private bool purposeFieldSpecified;

			/// <remarks/>
			public string Mnemonic
			{
				get
				{
					return this.mnemonicField;
				}
				set
				{
					this.mnemonicField = value;
				}
			}

			/// <remarks/>
			public string Description
			{
				get
				{
					return this.descriptionField;
				}
				set
				{
					this.descriptionField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute()]
			public PersonCategoryTypePurpose Purpose
			{
				get
				{
					return this.purposeField;
				}
				set
				{
					this.purposeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool PurposeSpecified
			{
				get
				{
					return this.purposeFieldSpecified;
				}
				set
				{
					this.purposeFieldSpecified = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonCategoryTypePurpose
		{

			/// <remarks/>
			UPDATE,

			/// <remarks/>
			REFERENCE,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PersonType : IdentType
		{

			private string mnemonicField;

			private string pCSUserField;

			private string activeEMISUserField;

			private string titleField;

			private string firstNamesField;

			private string lastNameField;

			private PersonCategoryType categoryField;

			private string nationalCodeField;

			private string prescriptionCodeField;

			private string gmcCodeField;

			private string uKCCCodeField;

			private PersonTypeScripts scriptsField;

			private bool scriptsFieldSpecified;

			private PersonTypeConsulter consulterField;

			private bool consulterFieldSpecified;

			private string contractStartField;

			private string contractEndField;

			private PersonTypeDoctor doctorField;

			private bool doctorFieldSpecified;

			private PersonTypeLocum locumField;

			private bool locumFieldSpecified;

			private PersonTypeRegistrar registrarField;

			private bool registrarFieldSpecified;

			private string maternityField;

			private PersonTypeChildSurveillance childSurveillanceField;

			private bool childSurveillanceFieldSpecified;

			private PersonTypeContraceptive contraceptiveField;

			private bool contraceptiveFieldSpecified;

			private PersonTypeTwentyFourHour twentyFourHourField;

			private bool twentyFourHourFieldSpecified;

			private PersonTypeMinorSurgery minorSurgeryField;

			private bool minorSurgeryFieldSpecified;

			private IdentType resonsibleHAField;

			private IdentType trainerField;

			private PersonTypeContractualRelationship contractualRelationshipField;

			private bool contractualRelationshipFieldSpecified;

			private AddressType addressField;

			private string telephone1Field;

			private string telephone2Field;

			private string faxField;

			private string emailField;

			private RoleType roleField;

			private string security1Field;

			private string security2Field;

			/// <remarks/>
			public string Mnemonic
			{
				get
				{
					return this.mnemonicField;
				}
				set
				{
					this.mnemonicField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string PCSUser
			{
				get
				{
					return this.pCSUserField;
				}
				set
				{
					this.pCSUserField = value;
				}
			}

			/// <remarks/>
			public string ActiveEMISUser
			{
				get
				{
					return this.activeEMISUserField;
				}
				set
				{
					this.activeEMISUserField = value;
				}
			}

			/// <remarks/>
			public string Title
			{
				get
				{
					return this.titleField;
				}
				set
				{
					this.titleField = value;
				}
			}

			/// <remarks/>
			public string FirstNames
			{
				get
				{
					return this.firstNamesField;
				}
				set
				{
					this.firstNamesField = value;
				}
			}

			/// <remarks/>
			public string LastName
			{
				get
				{
					return this.lastNameField;
				}
				set
				{
					this.lastNameField = value;
				}
			}

			/// <remarks/>
			public PersonCategoryType Category
			{
				get
				{
					return this.categoryField;
				}
				set
				{
					this.categoryField = value;
				}
			}

			/// <remarks/>
			public string NationalCode
			{
				get
				{
					return this.nationalCodeField;
				}
				set
				{
					this.nationalCodeField = value;
				}
			}

			/// <remarks/>
			public string PrescriptionCode
			{
				get
				{
					return this.prescriptionCodeField;
				}
				set
				{
					this.prescriptionCodeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string GmcCode
			{
				get
				{
					return this.gmcCodeField;
				}
				set
				{
					this.gmcCodeField = value;
				}
			}

			/// <remarks/>
			public string UKCCCode
			{
				get
				{
					return this.uKCCCodeField;
				}
				set
				{
					this.uKCCCodeField = value;
				}
			}

			/// <remarks/>
			public PersonTypeScripts Scripts
			{
				get
				{
					return this.scriptsField;
				}
				set
				{
					this.scriptsField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ScriptsSpecified
			{
				get
				{
					return this.scriptsFieldSpecified;
				}
				set
				{
					this.scriptsFieldSpecified = value;
				}
			}

			/// <remarks/>
			public PersonTypeConsulter Consulter
			{
				get
				{
					return this.consulterField;
				}
				set
				{
					this.consulterField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ConsulterSpecified
			{
				get
				{
					return this.consulterFieldSpecified;
				}
				set
				{
					this.consulterFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string ContractStart
			{
				get
				{
					return this.contractStartField;
				}
				set
				{
					this.contractStartField = value;
				}
			}

			/// <remarks/>
			public string ContractEnd
			{
				get
				{
					return this.contractEndField;
				}
				set
				{
					this.contractEndField = value;
				}
			}

			/// <remarks/>
			public PersonTypeDoctor Doctor
			{
				get
				{
					return this.doctorField;
				}
				set
				{
					this.doctorField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DoctorSpecified
			{
				get
				{
					return this.doctorFieldSpecified;
				}
				set
				{
					this.doctorFieldSpecified = value;
				}
			}

			/// <remarks/>
			public PersonTypeLocum Locum
			{
				get
				{
					return this.locumField;
				}
				set
				{
					this.locumField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool LocumSpecified
			{
				get
				{
					return this.locumFieldSpecified;
				}
				set
				{
					this.locumFieldSpecified = value;
				}
			}

			/// <remarks/>
			public PersonTypeRegistrar Registrar
			{
				get
				{
					return this.registrarField;
				}
				set
				{
					this.registrarField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool RegistrarSpecified
			{
				get
				{
					return this.registrarFieldSpecified;
				}
				set
				{
					this.registrarFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string Maternity
			{
				get
				{
					return this.maternityField;
				}
				set
				{
					this.maternityField = value;
				}
			}

			/// <remarks/>
			public PersonTypeChildSurveillance ChildSurveillance
			{
				get
				{
					return this.childSurveillanceField;
				}
				set
				{
					this.childSurveillanceField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ChildSurveillanceSpecified
			{
				get
				{
					return this.childSurveillanceFieldSpecified;
				}
				set
				{
					this.childSurveillanceFieldSpecified = value;
				}
			}

			/// <remarks/>
			public PersonTypeContraceptive Contraceptive
			{
				get
				{
					return this.contraceptiveField;
				}
				set
				{
					this.contraceptiveField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ContraceptiveSpecified
			{
				get
				{
					return this.contraceptiveFieldSpecified;
				}
				set
				{
					this.contraceptiveFieldSpecified = value;
				}
			}

			/// <remarks/>
			public PersonTypeTwentyFourHour TwentyFourHour
			{
				get
				{
					return this.twentyFourHourField;
				}
				set
				{
					this.twentyFourHourField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool TwentyFourHourSpecified
			{
				get
				{
					return this.twentyFourHourFieldSpecified;
				}
				set
				{
					this.twentyFourHourFieldSpecified = value;
				}
			}

			/// <remarks/>
			public PersonTypeMinorSurgery MinorSurgery
			{
				get
				{
					return this.minorSurgeryField;
				}
				set
				{
					this.minorSurgeryField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool MinorSurgerySpecified
			{
				get
				{
					return this.minorSurgeryFieldSpecified;
				}
				set
				{
					this.minorSurgeryFieldSpecified = value;
				}
			}

			/// <remarks/>
			public IdentType ResonsibleHA
			{
				get
				{
					return this.resonsibleHAField;
				}
				set
				{
					this.resonsibleHAField = value;
				}
			}

			/// <remarks/>
			public IdentType Trainer
			{
				get
				{
					return this.trainerField;
				}
				set
				{
					this.trainerField = value;
				}
			}

			/// <remarks/>
			public PersonTypeContractualRelationship ContractualRelationship
			{
				get
				{
					return this.contractualRelationshipField;
				}
				set
				{
					this.contractualRelationshipField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ContractualRelationshipSpecified
			{
				get
				{
					return this.contractualRelationshipFieldSpecified;
				}
				set
				{
					this.contractualRelationshipFieldSpecified = value;
				}
			}

			/// <remarks/>
			public AddressType Address
			{
				get
				{
					return this.addressField;
				}
				set
				{
					this.addressField = value;
				}
			}

			/// <remarks/>
			public string Telephone1
			{
				get
				{
					return this.telephone1Field;
				}
				set
				{
					this.telephone1Field = value;
				}
			}

			/// <remarks/>
			public string Telephone2
			{
				get
				{
					return this.telephone2Field;
				}
				set
				{
					this.telephone2Field = value;
				}
			}

			/// <remarks/>
			public string Fax
			{
				get
				{
					return this.faxField;
				}
				set
				{
					this.faxField = value;
				}
			}

			/// <remarks/>
			public string Email
			{
				get
				{
					return this.emailField;
				}
				set
				{
					this.emailField = value;
				}
			}

			/// <remarks/>
			public RoleType Role
			{
				get
				{
					return this.roleField;
				}
				set
				{
					this.roleField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute()]
			public string Security1
			{
				get
				{
					return this.security1Field;
				}
				set
				{
					this.security1Field = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute()]
			public string Security2
			{
				get
				{
					return this.security2Field;
				}
				set
				{
					this.security2Field = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeScripts
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeConsulter
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,

			/// <remarks/>
			O,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeDoctor
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeLocum
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeRegistrar
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeChildSurveillance
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeContraceptive
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeTwentyFourHour
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeMinorSurgery
		{

			/// <remarks/>
			Y,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum PersonTypeContractualRelationship
		{

			/// <remarks/>
			A,

			/// <remarks/>
			E,

			/// <remarks/>
			O,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class NoteType : IdentType
		{

			private string assignedDateField;

			private string timeField;

			private string urgencyField;

			private IdentType ownerIDField;

			private IdentType authorIDField;

			private string actionBeforeField;

			private string noteCodeField;

			private string statusField;

			private string noteTextField;

			private string dataTypeField;

			private string reportIDField;

			/// <remarks/>
			public string AssignedDate
			{
				get
				{
					return this.assignedDateField;
				}
				set
				{
					this.assignedDateField = value;
				}
			}

			/// <remarks/>
			public string Time
			{
				get
				{
					return this.timeField;
				}
				set
				{
					this.timeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Urgency
			{
				get
				{
					return this.urgencyField;
				}
				set
				{
					this.urgencyField = value;
				}
			}

			/// <remarks/>
			public IdentType OwnerID
			{
				get
				{
					return this.ownerIDField;
				}
				set
				{
					this.ownerIDField = value;
				}
			}

			/// <remarks/>
			public IdentType AuthorID
			{
				get
				{
					return this.authorIDField;
				}
				set
				{
					this.authorIDField = value;
				}
			}

			/// <remarks/>
			public string ActionBefore
			{
				get
				{
					return this.actionBeforeField;
				}
				set
				{
					this.actionBeforeField = value;
				}
			}

			/// <remarks/>
			public string NoteCode
			{
				get
				{
					return this.noteCodeField;
				}
				set
				{
					this.noteCodeField = value;
				}
			}

			/// <remarks/>
			public string Status
			{
				get
				{
					return this.statusField;
				}
				set
				{
					this.statusField = value;
				}
			}

			/// <remarks/>
			public string NoteText
			{
				get
				{
					return this.noteTextField;
				}
				set
				{
					this.noteTextField = value;
				}
			}

			/// <remarks/>
			public string DataType
			{
				get
				{
					return this.dataTypeField;
				}
				set
				{
					this.dataTypeField = value;
				}
			}

			/// <remarks/>
			public string ReportID
			{
				get
				{
					return this.reportIDField;
				}
				set
				{
					this.reportIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AppointmentType : IdentType
		{

			private IdentType sessionHolderField;

			private IdentType locationIDField;

			private string assignedDateField;

			private string timeField;

			private AppointmentTypeSite siteField;

			private string sessionIDField;

			private string reasonField;

			private float durationField;

			private bool durationFieldSpecified;

			private string rUBField;

			/// <remarks/>
			public IdentType SessionHolder
			{
				get
				{
					return this.sessionHolderField;
				}
				set
				{
					this.sessionHolderField = value;
				}
			}

			/// <remarks/>
			public IdentType LocationID
			{
				get
				{
					return this.locationIDField;
				}
				set
				{
					this.locationIDField = value;
				}
			}

			/// <remarks/>
			public string AssignedDate
			{
				get
				{
					return this.assignedDateField;
				}
				set
				{
					this.assignedDateField = value;
				}
			}

			/// <remarks/>
			public string Time
			{
				get
				{
					return this.timeField;
				}
				set
				{
					this.timeField = value;
				}
			}

			/// <remarks/>
			public AppointmentTypeSite Site
			{
				get
				{
					return this.siteField;
				}
				set
				{
					this.siteField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string SessionID
			{
				get
				{
					return this.sessionIDField;
				}
				set
				{
					this.sessionIDField = value;
				}
			}

			/// <remarks/>
			public string Reason
			{
				get
				{
					return this.reasonField;
				}
				set
				{
					this.reasonField = value;
				}
			}

			/// <remarks/>
			public float Duration
			{
				get
				{
					return this.durationField;
				}
				set
				{
					this.durationField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DurationSpecified
			{
				get
				{
					return this.durationFieldSpecified;
				}
				set
				{
					this.durationFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string RUB
			{
				get
				{
					return this.rUBField;
				}
				set
				{
					this.rUBField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class AppointmentTypeSite
		{

			private string idField;

			private string nameField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string ID
			{
				get
				{
					return this.idField;
				}
				set
				{
					this.idField = value;
				}
			}

			/// <remarks/>
			public string Name
			{
				get
				{
					return this.nameField;
				}
				set
				{
					this.nameField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class EDIOrderType : IdentType
		{

			private IdentType headerIDField;

			private IdentType providerField;

			private EDIOrderTypeFormDestination formDestinationField;

			private EDIOrderTypeOrderCategory orderCategoryField;

			private EDIOrderTypeStatus statusField;

			private bool statusFieldSpecified;

			private string lastStatusDateField;

			private TestRequestType[] testRequestListField;

			/// <remarks/>
			public IdentType HeaderID
			{
				get
				{
					return this.headerIDField;
				}
				set
				{
					this.headerIDField = value;
				}
			}

			/// <remarks/>
			public IdentType Provider
			{
				get
				{
					return this.providerField;
				}
				set
				{
					this.providerField = value;
				}
			}

			/// <remarks/>
			public EDIOrderTypeFormDestination FormDestination
			{
				get
				{
					return this.formDestinationField;
				}
				set
				{
					this.formDestinationField = value;
				}
			}

			/// <remarks/>
			public EDIOrderTypeOrderCategory OrderCategory
			{
				get
				{
					return this.orderCategoryField;
				}
				set
				{
					this.orderCategoryField = value;
				}
			}

			/// <remarks/>
			public EDIOrderTypeStatus Status
			{
				get
				{
					return this.statusField;
				}
				set
				{
					this.statusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool StatusSpecified
			{
				get
				{
					return this.statusFieldSpecified;
				}
				set
				{
					this.statusFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string LastStatusDate
			{
				get
				{
					return this.lastStatusDateField;
				}
				set
				{
					this.lastStatusDateField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("TestRequest", IsNullable = false)]
			public TestRequestType[] TestRequestList
			{
				get
				{
					return this.testRequestListField;
				}
				set
				{
					this.testRequestListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class EDIOrderTypeFormDestination : IdentType
		{

			private string descriptionField;

			/// <remarks/>
			public string Description
			{
				get
				{
					return this.descriptionField;
				}
				set
				{
					this.descriptionField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum EDIOrderTypeOrderCategory
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum EDIOrderTypeStatus
		{

			/// <remarks/>
			S,

			/// <remarks/>
			R,

			/// <remarks/>
			D,

			/// <remarks/>
			P,

			/// <remarks/>
			F,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class TestRequestHeaderType : IdentType
		{

			private IdentType requestorField;

			private IdentType consultationIDField;

			private string dateCreatedField;

			private string dateForTestField;

			private string dateLastXRayField;

			private object statusField;

			private string lastStatusDateField;

			private string copyToField;

			private TestRequestHeaderTypeNHS nHSField;

			private bool nHSFieldSpecified;

			private string priorityField;

			private TestRequestHeaderTypeInnoculationRisk innoculationRiskField;

			private bool innoculationRiskFieldSpecified;

			private TestRequestHeaderTypeFasted fastedField;

			private bool fastedFieldSpecified;

			private string lastMenstualPeriodField;

			private string clinicalInformationField;

			private TestRequestHeaderTypePregnant pregnantField;

			private bool pregnantFieldSpecified;

			private EDIOrderType[] eDIOrderListField;

			/// <remarks/>
			public IdentType Requestor
			{
				get
				{
					return this.requestorField;
				}
				set
				{
					this.requestorField = value;
				}
			}

			/// <remarks/>
			public IdentType ConsultationID
			{
				get
				{
					return this.consultationIDField;
				}
				set
				{
					this.consultationIDField = value;
				}
			}

			/// <remarks/>
			public string DateCreated
			{
				get
				{
					return this.dateCreatedField;
				}
				set
				{
					this.dateCreatedField = value;
				}
			}

			/// <remarks/>
			public string DateForTest
			{
				get
				{
					return this.dateForTestField;
				}
				set
				{
					this.dateForTestField = value;
				}
			}

			/// <remarks/>
			public string DateLastXRay
			{
				get
				{
					return this.dateLastXRayField;
				}
				set
				{
					this.dateLastXRayField = value;
				}
			}

			/// <remarks/>
			public object Status
			{
				get
				{
					return this.statusField;
				}
				set
				{
					this.statusField = value;
				}
			}

			/// <remarks/>
			public string LastStatusDate
			{
				get
				{
					return this.lastStatusDateField;
				}
				set
				{
					this.lastStatusDateField = value;
				}
			}

			/// <remarks/>
			public string CopyTo
			{
				get
				{
					return this.copyToField;
				}
				set
				{
					this.copyToField = value;
				}
			}

			/// <remarks/>
			public TestRequestHeaderTypeNHS NHS
			{
				get
				{
					return this.nHSField;
				}
				set
				{
					this.nHSField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool NHSSpecified
			{
				get
				{
					return this.nHSFieldSpecified;
				}
				set
				{
					this.nHSFieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Priority
			{
				get
				{
					return this.priorityField;
				}
				set
				{
					this.priorityField = value;
				}
			}

			/// <remarks/>
			public TestRequestHeaderTypeInnoculationRisk InnoculationRisk
			{
				get
				{
					return this.innoculationRiskField;
				}
				set
				{
					this.innoculationRiskField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool InnoculationRiskSpecified
			{
				get
				{
					return this.innoculationRiskFieldSpecified;
				}
				set
				{
					this.innoculationRiskFieldSpecified = value;
				}
			}

			/// <remarks/>
			public TestRequestHeaderTypeFasted Fasted
			{
				get
				{
					return this.fastedField;
				}
				set
				{
					this.fastedField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool FastedSpecified
			{
				get
				{
					return this.fastedFieldSpecified;
				}
				set
				{
					this.fastedFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string LastMenstualPeriod
			{
				get
				{
					return this.lastMenstualPeriodField;
				}
				set
				{
					this.lastMenstualPeriodField = value;
				}
			}

			/// <remarks/>
			public string ClinicalInformation
			{
				get
				{
					return this.clinicalInformationField;
				}
				set
				{
					this.clinicalInformationField = value;
				}
			}

			/// <remarks/>
			public TestRequestHeaderTypePregnant Pregnant
			{
				get
				{
					return this.pregnantField;
				}
				set
				{
					this.pregnantField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool PregnantSpecified
			{
				get
				{
					return this.pregnantFieldSpecified;
				}
				set
				{
					this.pregnantFieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("EDIOrder", IsNullable = false)]
			public EDIOrderType[] EDIOrderList
			{
				get
				{
					return this.eDIOrderListField;
				}
				set
				{
					this.eDIOrderListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum TestRequestHeaderTypeNHS
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum TestRequestHeaderTypeInnoculationRisk
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum TestRequestHeaderTypeFasted
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum TestRequestHeaderTypePregnant
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ConsultationType : IdentType
		{

			private string assignedDateField;

			private short datePartField;

			private bool datePartFieldSpecified;

			private IdentType userIDField;

			private string externalConsultantField;

			private IdentType locationIDField;

			private IdentType locationTypeIDField;

			private IdentType accompanyingHCPIDField;

			private sbyte consultationType1Field;

			private bool consultationType1FieldSpecified;

			private string durationField;

			private string travelTimeField;

			private string appointmentSlotIDField;

			private string dataSourceField;

			private string policyIDField;

			private AuthorType originalAuthorField;

			private ElementListTypeConsultationElement[] elementListField;

			/// <remarks/>
			public string AssignedDate
			{
				get
				{
					return this.assignedDateField;
				}
				set
				{
					this.assignedDateField = value;
				}
			}

			/// <remarks/>
			public short DatePart
			{
				get
				{
					return this.datePartField;
				}
				set
				{
					this.datePartField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DatePartSpecified
			{
				get
				{
					return this.datePartFieldSpecified;
				}
				set
				{
					this.datePartFieldSpecified = value;
				}
			}

			/// <remarks/>
			public IdentType UserID
			{
				get
				{
					return this.userIDField;
				}
				set
				{
					this.userIDField = value;
				}
			}

			/// <remarks/>
			public string ExternalConsultant
			{
				get
				{
					return this.externalConsultantField;
				}
				set
				{
					this.externalConsultantField = value;
				}
			}

			/// <remarks/>
			public IdentType LocationID
			{
				get
				{
					return this.locationIDField;
				}
				set
				{
					this.locationIDField = value;
				}
			}

			/// <remarks/>
			public IdentType LocationTypeID
			{
				get
				{
					return this.locationTypeIDField;
				}
				set
				{
					this.locationTypeIDField = value;
				}
			}

			/// <remarks/>
			public IdentType AccompanyingHCPID
			{
				get
				{
					return this.accompanyingHCPIDField;
				}
				set
				{
					this.accompanyingHCPIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("ConsultationType")]
			public sbyte ConsultationType1
			{
				get
				{
					return this.consultationType1Field;
				}
				set
				{
					this.consultationType1Field = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ConsultationType1Specified
			{
				get
				{
					return this.consultationType1FieldSpecified;
				}
				set
				{
					this.consultationType1FieldSpecified = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Duration
			{
				get
				{
					return this.durationField;
				}
				set
				{
					this.durationField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TravelTime
			{
				get
				{
					return this.travelTimeField;
				}
				set
				{
					this.travelTimeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string AppointmentSlotID
			{
				get
				{
					return this.appointmentSlotIDField;
				}
				set
				{
					this.appointmentSlotIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string DataSource
			{
				get
				{
					return this.dataSourceField;
				}
				set
				{
					this.dataSourceField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string PolicyID
			{
				get
				{
					return this.policyIDField;
				}
				set
				{
					this.policyIDField = value;
				}
			}

			/// <remarks/>
			public AuthorType OriginalAuthor
			{
				get
				{
					return this.originalAuthorField;
				}
				set
				{
					this.originalAuthorField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("ConsultationElement", IsNullable = false)]
			public ElementListTypeConsultationElement[] ElementList
			{
				get
				{
					return this.elementListField;
				}
				set
				{
					this.elementListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class ElementListTypeConsultationElement : IdentType
		{

			private sbyte displayOrderField;

			private bool displayOrderFieldSpecified;

			private sbyte problemSectionField;

			private bool problemSectionFieldSpecified;

			private IntegerCodeType headerField;

			private IdentType itemField;

			/// <remarks/>
			public sbyte DisplayOrder
			{
				get
				{
					return this.displayOrderField;
				}
				set
				{
					this.displayOrderField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool DisplayOrderSpecified
			{
				get
				{
					return this.displayOrderFieldSpecified;
				}
				set
				{
					this.displayOrderFieldSpecified = value;
				}
			}

			/// <remarks/>
			public sbyte ProblemSection
			{
				get
				{
					return this.problemSectionField;
				}
				set
				{
					this.problemSectionField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool ProblemSectionSpecified
			{
				get
				{
					return this.problemSectionFieldSpecified;
				}
				set
				{
					this.problemSectionFieldSpecified = value;
				}
			}

			/// <remarks/>
			public IntegerCodeType Header
			{
				get
				{
					return this.headerField;
				}
				set
				{
					this.headerField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("Allergy", typeof(AllergyType))]
			[System.Xml.Serialization.XmlElementAttribute("Attachment", typeof(AttachmentType))]
			[System.Xml.Serialization.XmlElementAttribute("Diary", typeof(DiaryType))]
			[System.Xml.Serialization.XmlElementAttribute("Event", typeof(EventType))]
			[System.Xml.Serialization.XmlElementAttribute("Investigation", typeof(InvestigationType))]
			[System.Xml.Serialization.XmlElementAttribute("Medication", typeof(MedicationType))]
			[System.Xml.Serialization.XmlElementAttribute("Referral", typeof(ReferralType))]
			[System.Xml.Serialization.XmlElementAttribute("TestRequest", typeof(TestRequestHeaderType))]
			public IdentType Item
			{
				get
				{
					return this.itemField;
				}
				set
				{
					this.itemField = value;
				}
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlIncludeAttribute(typeof(IssueType))]
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicationType : IdentType
		{

			private IdentType authorisedUserIDField;

			private AuthorType originalAuthorField;

			private string assignedDateField;

			private string dateLastIssueField;

			private MedicationTypeIssueMethod issueMethodField;

			private bool issueMethodFieldSpecified;

			private string dateRxExpireField;

			private string doseAbbreviationField;

			private string dosageField;

			private float quantityField;

			private bool quantityFieldSpecified;

			private string quantityUnitsField;

			private string quantityRepresentationField;

			private string durationField;

			private string mixtureIndicatorField;

			private MedicationTypeDrug drugField;

			private MedicationTypePrescriptionType prescriptionTypeField;

			private bool prescriptionTypeFieldSpecified;

			private string drugSourceField;

			private string rxReviewDateField;

			private string pharmacyTextField;

			private string rxTextField;

			private string authorisedIssueField;

			private string issueCountField;

			private MedicationTypeStatus statusField;

			private bool statusFieldSpecified;

			private string adviceField;

			private string templateField;

			private string templateComponentField;

			private string templateInstanceField;

			private DrugDeliveryType drugDeliveryField;

			private ControlledDrugInfoType controlledDrugField;

			private LinkType[] problemLinkListField;

			/// <remarks/>
			public IdentType AuthorisedUserID
			{
				get
				{
					return this.authorisedUserIDField;
				}
				set
				{
					this.authorisedUserIDField = value;
				}
			}

			/// <remarks/>
			public AuthorType OriginalAuthor
			{
				get
				{
					return this.originalAuthorField;
				}
				set
				{
					this.originalAuthorField = value;
				}
			}

			/// <remarks/>
			public string AssignedDate
			{
				get
				{
					return this.assignedDateField;
				}
				set
				{
					this.assignedDateField = value;
				}
			}

			/// <remarks/>
			public string DateLastIssue
			{
				get
				{
					return this.dateLastIssueField;
				}
				set
				{
					this.dateLastIssueField = value;
				}
			}

			/// <remarks/>
			public MedicationTypeIssueMethod IssueMethod
			{
				get
				{
					return this.issueMethodField;
				}
				set
				{
					this.issueMethodField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool IssueMethodSpecified
			{
				get
				{
					return this.issueMethodFieldSpecified;
				}
				set
				{
					this.issueMethodFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string DateRxExpire
			{
				get
				{
					return this.dateRxExpireField;
				}
				set
				{
					this.dateRxExpireField = value;
				}
			}

			/// <remarks/>
			public string DoseAbbreviation
			{
				get
				{
					return this.doseAbbreviationField;
				}
				set
				{
					this.doseAbbreviationField = value;
				}
			}

			/// <remarks/>
			public string Dosage
			{
				get
				{
					return this.dosageField;
				}
				set
				{
					this.dosageField = value;
				}
			}

			/// <remarks/>
			public float Quantity
			{
				get
				{
					return this.quantityField;
				}
				set
				{
					this.quantityField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool QuantitySpecified
			{
				get
				{
					return this.quantityFieldSpecified;
				}
				set
				{
					this.quantityFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string QuantityUnits
			{
				get
				{
					return this.quantityUnitsField;
				}
				set
				{
					this.quantityUnitsField = value;
				}
			}

			/// <remarks/>
			public string QuantityRepresentation
			{
				get
				{
					return this.quantityRepresentationField;
				}
				set
				{
					this.quantityRepresentationField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Duration
			{
				get
				{
					return this.durationField;
				}
				set
				{
					this.durationField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string MixtureIndicator
			{
				get
				{
					return this.mixtureIndicatorField;
				}
				set
				{
					this.mixtureIndicatorField = value;
				}
			}

			/// <remarks/>
			public MedicationTypeDrug Drug
			{
				get
				{
					return this.drugField;
				}
				set
				{
					this.drugField = value;
				}
			}

			/// <remarks/>
			public MedicationTypePrescriptionType PrescriptionType
			{
				get
				{
					return this.prescriptionTypeField;
				}
				set
				{
					this.prescriptionTypeField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool PrescriptionTypeSpecified
			{
				get
				{
					return this.prescriptionTypeFieldSpecified;
				}
				set
				{
					this.prescriptionTypeFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string DrugSource
			{
				get
				{
					return this.drugSourceField;
				}
				set
				{
					this.drugSourceField = value;
				}
			}

			/// <remarks/>
			public string RxReviewDate
			{
				get
				{
					return this.rxReviewDateField;
				}
				set
				{
					this.rxReviewDateField = value;
				}
			}

			/// <remarks/>
			public string PharmacyText
			{
				get
				{
					return this.pharmacyTextField;
				}
				set
				{
					this.pharmacyTextField = value;
				}
			}

			/// <remarks/>
			public string RxText
			{
				get
				{
					return this.rxTextField;
				}
				set
				{
					this.rxTextField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string AuthorisedIssue
			{
				get
				{
					return this.authorisedIssueField;
				}
				set
				{
					this.authorisedIssueField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string IssueCount
			{
				get
				{
					return this.issueCountField;
				}
				set
				{
					this.issueCountField = value;
				}
			}

			/// <remarks/>
			public MedicationTypeStatus Status
			{
				get
				{
					return this.statusField;
				}
				set
				{
					this.statusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool StatusSpecified
			{
				get
				{
					return this.statusFieldSpecified;
				}
				set
				{
					this.statusFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string Advice
			{
				get
				{
					return this.adviceField;
				}
				set
				{
					this.adviceField = value;
				}
			}

			/// <remarks/>
			public string Template
			{
				get
				{
					return this.templateField;
				}
				set
				{
					this.templateField = value;
				}
			}

			/// <remarks/>
			public string TemplateComponent
			{
				get
				{
					return this.templateComponentField;
				}
				set
				{
					this.templateComponentField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string TemplateInstance
			{
				get
				{
					return this.templateInstanceField;
				}
				set
				{
					this.templateInstanceField = value;
				}
			}

			/// <remarks/>
			public DrugDeliveryType DrugDelivery
			{
				get
				{
					return this.drugDeliveryField;
				}
				set
				{
					this.drugDeliveryField = value;
				}
			}

			/// <remarks/>
			public ControlledDrugInfoType ControlledDrug
			{
				get
				{
					return this.controlledDrugField;
				}
				set
				{
					this.controlledDrugField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("Link", IsNullable = false)]
			public LinkType[] ProblemLinkList
			{
				get
				{
					return this.problemLinkListField;
				}
				set
				{
					this.problemLinkListField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum MedicationTypeIssueMethod
		{

			/// <remarks/>
			S,

			/// <remarks/>
			Q,

			/// <remarks/>
			H,

			/// <remarks/>
			X,

			/// <remarks/>
			O,

			/// <remarks/>
			P,

			/// <remarks/>
			N,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicationTypeDrug
		{

			private object itemField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("Mixture", typeof(MixtureType))]
			[System.Xml.Serialization.XmlElementAttribute("PreparationID", typeof(IntegerCodeType))]
			public object Item
			{
				get
				{
					return this.itemField;
				}
				set
				{
					this.itemField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum MedicationTypePrescriptionType
		{

			/// <remarks/>
			ACUTE,

			/// <remarks/>
			REPEAT,

			/// <remarks/>
			AUTOMATIC,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum MedicationTypeStatus
		{

			/// <remarks/>
			C,

			/// <remarks/>
			N,

			/// <remarks/>
			P,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class IssueType : MedicationType
		{

			private double estimatedCostField;

			private bool estimatedCostFieldSpecified;

			private string commentField;

			private IdentType issuerIDField;

			private string dateTimePrintField;

			private string dateTimeDispenseField;

			private IdentType dispenserIDField;

			private string batchNumberField;

			/// <remarks/>
			public double EstimatedCost
			{
				get
				{
					return this.estimatedCostField;
				}
				set
				{
					this.estimatedCostField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlIgnoreAttribute()]
			public bool EstimatedCostSpecified
			{
				get
				{
					return this.estimatedCostFieldSpecified;
				}
				set
				{
					this.estimatedCostFieldSpecified = value;
				}
			}

			/// <remarks/>
			public string Comment
			{
				get
				{
					return this.commentField;
				}
				set
				{
					this.commentField = value;
				}
			}

			/// <remarks/>
			public IdentType IssuerID
			{
				get
				{
					return this.issuerIDField;
				}
				set
				{
					this.issuerIDField = value;
				}
			}

			/// <remarks/>
			public string DateTimePrint
			{
				get
				{
					return this.dateTimePrintField;
				}
				set
				{
					this.dateTimePrintField = value;
				}
			}

			/// <remarks/>
			public string DateTimeDispense
			{
				get
				{
					return this.dateTimeDispenseField;
				}
				set
				{
					this.dateTimeDispenseField = value;
				}
			}

			/// <remarks/>
			public IdentType DispenserID
			{
				get
				{
					return this.dispenserIDField;
				}
				set
				{
					this.dispenserIDField = value;
				}
			}

			/// <remarks/>
			public string BatchNumber
			{
				get
				{
					return this.batchNumberField;
				}
				set
				{
					this.batchNumberField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MixtureItemType : IdentType
		{

			private IntegerCodeType preparationIDField;

			private string strengthQuantityField;

			/// <remarks/>
			public IntegerCodeType PreparationID
			{
				get
				{
					return this.preparationIDField;
				}
				set
				{
					this.preparationIDField = value;
				}
			}

			/// <remarks/>
			public string StrengthQuantity
			{
				get
				{
					return this.strengthQuantityField;
				}
				set
				{
					this.strengthQuantityField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum RegistrationTypeDead
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("0")]
			Item0,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class RegistrationTypeCustomRegistrationEntry
		{

			private string fieldNameField;

			private string fieldValueField;

			/// <remarks/>
			public string FieldName
			{
				get
				{
					return this.fieldNameField;
				}
				set
				{
					this.fieldNameField = value;
				}
			}

			/// <remarks/>
			public string FieldValue
			{
				get
				{
					return this.fieldValueField;
				}
				set
				{
					this.fieldValueField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeRegistrationEntry
		{

			private string fieldNameField;

			private string fieldValueField;

			private string dateChangedField;

			private IdentType userIDField;

			/// <remarks/>
			public string FieldName
			{
				get
				{
					return this.fieldNameField;
				}
				set
				{
					this.fieldNameField = value;
				}
			}

			/// <remarks/>
			public string FieldValue
			{
				get
				{
					return this.fieldValueField;
				}
				set
				{
					this.fieldValueField = value;
				}
			}

			/// <remarks/>
			public string DateChanged
			{
				get
				{
					return this.dateChangedField;
				}
				set
				{
					this.dateChangedField = value;
				}
			}

			/// <remarks/>
			public IdentType UserID
			{
				get
				{
					return this.userIDField;
				}
				set
				{
					this.userIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeMiscellaneousData
		{

			private string medicationMessageField;

			private string automaticWeekNumberField;

			/// <remarks/>
			public string MedicationMessage
			{
				get
				{
					return this.medicationMessageField;
				}
				set
				{
					this.medicationMessageField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string AutomaticWeekNumber
			{
				get
				{
					return this.automaticWeekNumberField;
				}
				set
				{
					this.automaticWeekNumberField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeMessageInformation
		{

			private string dateCreatedField;

			private string timeCreatedField;

			private MedicalRecordTypeMessageInformationMessagePurpose messagePurposeField;

			private MedicalRecordTypeMessageInformationMessagingError messagingErrorField;

			/// <remarks/>
			public string DateCreated
			{
				get
				{
					return this.dateCreatedField;
				}
				set
				{
					this.dateCreatedField = value;
				}
			}

			/// <remarks/>
			public string TimeCreated
			{
				get
				{
					return this.timeCreatedField;
				}
				set
				{
					this.timeCreatedField = value;
				}
			}

			/// <remarks/>
			public MedicalRecordTypeMessageInformationMessagePurpose MessagePurpose
			{
				get
				{
					return this.messagePurposeField;
				}
				set
				{
					this.messagePurposeField = value;
				}
			}

			/// <remarks/>
			public MedicalRecordTypeMessageInformationMessagingError MessagingError
			{
				get
				{
					return this.messagingErrorField;
				}
				set
				{
					this.messagingErrorField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeMessageInformationMessagePurpose
		{

			private object itemField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute("Get", typeof(MedicalRecordTypeMessageInformationMessagePurposeGet))]
			[System.Xml.Serialization.XmlElementAttribute("Post", typeof(MedicalRecordTypeMessageInformationMessagePurposePost))]
			public object Item
			{
				get
				{
					return this.itemField;
				}
				set
				{
					this.itemField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeMessageInformationMessagePurposeGet
		{

			private MedicalRecordTypeMessageInformationMessagePurposeGetMRGetType mRGetTypeField;

			private string dataSetField;

			private string sessionIDField;

			/// <remarks/>
			public MedicalRecordTypeMessageInformationMessagePurposeGetMRGetType MRGetType
			{
				get
				{
					return this.mRGetTypeField;
				}
				set
				{
					this.mRGetTypeField = value;
				}
			}

			/// <remarks/>
			public string DataSet
			{
				get
				{
					return this.dataSetField;
				}
				set
				{
					this.dataSetField = value;
				}
			}

			/// <remarks/>
			public string SessionID
			{
				get
				{
					return this.sessionIDField;
				}
				set
				{
					this.sessionIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum MedicalRecordTypeMessageInformationMessagePurposeGetMRGetType
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("3")]
			Item3,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("4")]
			Item4,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeMessageInformationMessagePurposePost
		{

			private MedicalRecordTypeMessageInformationMessagePurposePostPostType postTypeField;

			private string sessionIDField;

			/// <remarks/>
			public MedicalRecordTypeMessageInformationMessagePurposePostPostType PostType
			{
				get
				{
					return this.postTypeField;
				}
				set
				{
					this.postTypeField = value;
				}
			}

			/// <remarks/>
			public string SessionID
			{
				get
				{
					return this.sessionIDField;
				}
				set
				{
					this.sessionIDField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public enum MedicalRecordTypeMessageInformationMessagePurposePostPostType
		{

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("1")]
			Item1,

			/// <remarks/>
			[System.Xml.Serialization.XmlEnumAttribute("2")]
			Item2,
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class MedicalRecordTypeMessageInformationMessagingError
		{

			private string errorStringField;

			private string errornumberField;

			/// <remarks/>
			public string ErrorString
			{
				get
				{
					return this.errorStringField;
				}
				set
				{
					this.errorStringField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string Errornumber
			{
				get
				{
					return this.errornumberField;
				}
				set
				{
					this.errornumberField = value;
				}
			}
		}

		/// <remarks/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
		[System.SerializableAttribute()]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class PolicyListTypePolicyType
		{

			private string dBIDField;

			private string refIdField;

			private string gUIDField;

			private string termField;

			private string fileStatusField;

			private PersonType[] userListField;

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string DBID
			{
				get
				{
					return this.dBIDField;
				}
				set
				{
					this.dBIDField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string RefId
			{
				get
				{
					return this.refIdField;
				}
				set
				{
					this.refIdField = value;
				}
			}

			/// <remarks/>
			public string GUID
			{
				get
				{
					return this.gUIDField;
				}
				set
				{
					this.gUIDField = value;
				}
			}

			/// <remarks/>
			public string Term
			{
				get
				{
					return this.termField;
				}
				set
				{
					this.termField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
			public string FileStatus
			{
				get
				{
					return this.fileStatusField;
				}
				set
				{
					this.fileStatusField = value;
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("PolicyUser", IsNullable = false)]
			public PersonType[] UserList
			{
				get
				{
					return this.userListField;
				}
				set
				{
					this.userListField = value;
				}
			}
		}

	}
}
