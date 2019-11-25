/*
	Description: Fixing SQL COP issues and Adding Snomed Code Option
	Author: Aravind
	Patch Number: 1.0038
	Dependant Patch Number = 1.0034
*/

create table PatientFlow.AlertLinkToKiosk
(
	AlertId int not null,
	KioskId int not null,	
	constraint FK_PatientFlow_AlertLinkToKiosk_AlertId foreign key (AlertId) references PatientFlow.Alert (AlertId),
	constraint FK_PatientFlow_AlertLinkToKiosk_KioskId foreign key (KioskId) references PatientFlow.Kiosk (KioskId),
	constraint PK_PatientFlow_AlertLinkToKiosk_AlertId_KioskId primary key clustered (AlertId,KioskId)	
)

exec sys.sp_addextendedproperty
	@name=N'MS_Description',
	@value= 'Mapping between Alerts and Kiosk',
	@level0type=N'SCHEMA',
	@level0name=N'PatientFlow',
	@level1type=N'TABLE',
	@level1name=N'AlertLinkToKiosk';

go

insert into PatientFlow.AlertLinkToKiosk 
(
	AlertId,
	KioskId
) 
select 
	AlertId,
	kiosk.KioskId 
from PatientFlow.AlertLinkToOrganisation alertorg
join PatientFlow.Kiosk kiosk on alertorg.kioskguid = kiosk.KioskGuid
where alertorg.kioskguid is not null;

go

alter table PatientFlow.AlertLinkToOrganisation
drop column kioskguid

go

with cte
     as (select *,row_number() over (partition by alertid, organisationid order by alertid, organisationid) rowno
         from  PatientFlow.AlertLinkToOrganisation)
delete from cte
where  rowno > 1;

go

alter table PatientFlow.AlertLinkToOrganisation
drop constraint PK_PatientFlow_AlertLinkToOrganisation_AlertLinkId

alter table PatientFlow.AlertLinkToOrganisation
drop column AlertLinkId

alter table PatientFlow.AlertLinkToOrganisation
add constraint PK_PatientFlow_AlertLinkToOrganisation_AlertId_OrganisationId primary key clustered (AlertId,OrganisationId)

go

alter table PatientFlow.AlertsLinkedToDepMem
drop constraint PK_PatientFlow_AlertsLinkedToDepMem_AlertDepMemLinkId

go

alter table PatientFlow.AlertsLinkedToDepMem
add constraint PK_PatientFlow_AlertsLinkedToDepMem_AlertDepMemLinkId primary key nonclustered (AlertDepMemLinkId)

create clustered index IDX_PatientFlow_AlertsLinkedToDepMem_AlertId  
on PatientFlow.AlertsLinkedToDepMem (AlertId);  

go


alter table PatientFlow.OrganisationAccessMapping
drop constraint PK_PatientFlow_OrganisationAccessMapping_AccessMappingId

go

alter table PatientFlow.OrganisationAccessMapping
add constraint PK_PatientFlow_OrganisationAccessMapping_AccessMappingId primary key nonclustered (AccessMappingId)


alter table PatientFlow.OrganisationAccessMapping
add constraint UQ_PatientFlow_OrganisationAccessMapping_UserName_OrganisationId unique clustered (UserName,OrganisationId)
  
go

alter table PatientFlow.Kiosk
add constraint UQ_PatientFlow_Kiosk_KioskGuid unique (KioskGuid)

go

alter table PatientFlow.QuestionAnswerOptions
drop constraint PK_PatientFlow_QuestionAnswerOptions_OptionId

go

alter table PatientFlow.QuestionAnswerOptions
add constraint PK_PatientFlow_QuestionAnswerOptions_OptionId primary key nonclustered (OptionId)


create clustered index IDX_PatientFlow_QuestionAnswerOptions_QuestionId  
on PatientFlow.QuestionAnswerOptions (QuestionId);  

go

alter table PatientFlow.Questionnaire
add IsActive bit constraint DF_PatientFlow_Questionnaire_IsActive default (1) not null

go

alter table PatientFlow.QuestionAnswerOptions
add SnomedCode bigint null 

go

