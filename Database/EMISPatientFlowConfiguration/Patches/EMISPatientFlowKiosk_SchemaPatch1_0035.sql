/*
	Description: Fixing the SQL COP issues and errors
	Author: Aravind
	Patch Number: 1.0035
	Dependant Patch Number = 1.0034
*/


if object_id('PatientFlow.SaveSlotType') is not null
	drop procedure PatientFlow.SaveSlotType;

if object_id('PatientFlow.SaveSyncServiceOrganisations') is not null
	drop procedure PatientFlow.SaveSyncServiceOrganisations;

if object_id('PatientFlow.UpdateAlert') is not null
	drop procedure PatientFlow.UpdateAlert;

if object_id('PatientFlow.UpdateDivert') is not null
	drop procedure PatientFlow.UpdateDivert;

if object_id('PatientFlow.UpdateKioskDetails') is not null
	drop procedure PatientFlow.UpdateKioskDetails;

if object_id('PatientFlow.ValidateKioskName') is not null
	drop procedure PatientFlow.ValidateKioskName;

if object_id('PatientFlow.AddAlert') is not null
	drop procedure PatientFlow.AddAlert;

if object_id('PatientFlow.AddAlertLinkedToDepMemDetails') is not null
	drop procedure PatientFlow.AddAlertLinkedToDepMemDetails;

if object_id('PatientFlow.AddKioskDetails') is not null
	drop procedure PatientFlow.AddKioskDetails;

if object_id('PatientFlow.AddKioskLinkedToDepMemDetails') is not null
	drop procedure PatientFlow.AddKioskLinkedToDepMemDetails;

if object_id('PatientFlow.SaveAccessMapping') is not null
	drop procedure PatientFlow.SaveAccessMapping;

if object_id('PatientFlow.GetAlertsListForOrganisationIds') is not null
	drop procedure PatientFlow.GetAlertsListForOrganisationIds;

if object_id('PatientFlow.GetDepartmentMemberTreeList') is not null
	drop procedure PatientFlow.GetDepartmentMemberTreeList;

if object_id('PatientFlow.GetDepartmentMemberTreeListByOrgs') is not null
	drop procedure PatientFlow.GetDepartmentMemberTreeListByOrgs;

if object_id('PatientFlow.GetSelectedDepartmentMemberTreeList') is not null
	drop procedure PatientFlow.GetSelectedDepartmentMemberTreeList;

if object_id('PatientFlow.GetOrganisationsByIds') is not null
	drop procedure PatientFlow.GetOrganisationsByIds;

if object_id('PatientFlow.UpdateQuestionnaireDetails') is not null
	drop procedure PatientFlow.UpdateQuestionnaireDetails;

if object_id('PatientFlow.SaveLog') is not null
	drop procedure PatientFlow.SaveLog;

if object_id('PatientFlow.SaveSyncedMember') is not null
	drop procedure PatientFlow.SaveSyncedMember;

if object_id('PatientFlow.SaveScreenControlTranslatedText') is not null
	drop procedure PatientFlow.SaveScreenControlTranslatedText;

if object_id('PatientFlow.SaveModuleTranslatedText') is not null
	drop procedure PatientFlow.SaveModuleTranslatedText;

if object_id('PatientFlow.UpdateOrganisationDetails') is not null
	drop procedure PatientFlow.UpdateOrganisationDetails;

if object_id('PatientFlow.AddOrganisation') is not null
	drop procedure PatientFlow.AddOrganisation;

if object_id('PatientFlow.UpdateAlert') is not null
	drop procedure PatientFlow.UpdateAlert;

if object_id('PatientFlow.AddAlert') is not null
	drop procedure PatientFlow.AddAlert;

if object_id('PatientFlow.GetAlerts') is not null
	drop procedure PatientFlow.GetAlerts;

if object_id('PatientFlow.GetAlertsByUser') is not null
	drop procedure PatientFlow.GetAlertsByUser;
go

drop type PatientFlow.AppointmentSlotType;
drop type PatientFlow.List;
drop type PatientFlow.ListWithOrder;
drop type PatientFlow.ModuleTranslations;
drop type PatientFlow.OrganisationSites;
drop type PatientFlow.Permission;
drop type PatientFlow.LogEntry;
drop type PatientFlow.Member;
drop type PatientFlow.KioskLinkOrganisationSites;
drop type PatientFlow.stringlist;
go

create type PatientFlow.AppointmentSlotType as table
(
	AppointmentSlotTypeId int not null primary key,
	OrganisationId int not null,
	SlotTypeId varchar(100) not null,
	[Description] varchar(100) null
);

create type PatientFlow.List as table
(
	Id int not null  primary key
);

create type PatientFlow.ListWithOrder as table
(
	Id int not null primary key,
	ListOrder int not null
);


create type PatientFlow.KioskLinkOrganisationSites as table
(
	KioskLinkOrganisationSitesId int primary key,
	OrganisationId int not null,
	SiteId int null,
	MainLocation bit 	
);

create type PatientFlow.LogEntry as table
(
	LogId int not null  primary key,
    LogDate datetime NOT null,
	Thread varchar(255) null,
	LogLevel varchar(50) null,
	Logger varchar(255) null,
	LogUser varchar(50) null,
	LogMessage varchar(4000) null,
	Exception varchar(2000) null
);

create type PatientFlow.Member as table
(
	SyncMemberId int not null  primary key,
	MemberId int not null,
	Firstname varchar(100) null,
	Surname varchar(100) null,
	Title varchar(100) null,
	SessionHolderId int null,
	DepartmentId int null
)

create type PatientFlow.ModuleTranslations as table
(
	Id int not null  primary key,
	LanguageCode varchar(50) not null,
	TranslatedText nvarchar(1000) not null,
	TranslationRefId int not null,
	ModifiedBy varchar(50) not null
);

create type PatientFlow.OrganisationSites as table
(	
	OrganisationId int not null,
	SiteDBID bigint not null,
	SiteName varchar(100) not null,
	primary key clustered (OrganisationId,SiteDBID)
);

create type PatientFlow.StringList as table
(
	StringListId int primary key,
	value nvarchar(4000) null
);

go

exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Patient Message should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'table',
    @level1name = 'PatientMessage',
    @level2type = N'COLUMN',
    @level2name = 'Message';

exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Alert Message should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'table',
    @level1name = 'Alert',
    @level2type = N'COLUMN',
    @level2name = 'AlertText';
                
                
exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Kiosk text should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'table',
    @level1name = 'Translation',
    @level2type = N'COLUMN',
    @level2name = 'TranslationText';
                
exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Kiosk text should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'TYPE',
    @level1name = 'ModuleTranslations',
    @level2type = N'COLUMN',
    @level2name = 'TranslatedText';
                
exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Kiosk text should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'TYPE',
    @level1name = 'stringlist',
    @level2type = N'COLUMN',
    @level2name = 'value';

go

exec sp_rename 'PatientFlow.AlertLinkToOrganisation.Id', 'AlertLinkId', 'COLUMN';
exec sp_rename 'PatientFlow.AlertLinkToOrganisation.PK_PatientFlow_AlertLinkToOrganisation_Id', 'PK_PatientFlow_AlertLinkToOrganisation_AlertLinkId', N'INDEX'

go

exec sp_rename 'PatientFlow.KioskLinkedToDetails.Id', 'KioskLinkDetailsId', 'COLUMN'; 
exec sp_rename 'PatientFlow.KioskLinkedToDetails.PK_PatientFlow_KioskLinkedToDetails_Id', 'PK_PatientFlow_KioskLinkedToDetails_KioskLinkDetailsId', N'INDEX'
go

exec sp_rename 'PatientFlow.AlertsLinkedToDepMem.Id', 'AlertDepMemLinkId', 'COLUMN';
exec sp_rename 'PatientFlow.AlertsLinkedToDepMem.PK_PatientFlow_AlertsLinkedToDepMem_Id', 'PK_PatientFlow_AlertsLinkedToDepMem_AlertDepMemLinkId', N'INDEX'
go

exec sp_rename 'PatientFlow.KioskLinkedToDepMemDetails.Id', 'KioskDepMemLinkId', 'COLUMN';
exec sp_rename 'PatientFlow.KioskLinkedToDepMemDetails.PK_PatientFlow_KioskLinkedToDepMemDetails_Id', 'PK_PatientFlow_KioskLinkedToDepMemDetails_KioskDepMemLinkId', N'INDEX'
go

