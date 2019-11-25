/*
Description: Increase Field Length for BookAppoinmentType
Author: Aravind
Reviewer: Sathish
Patch Number: 1.0014
Dependant Patch Number = 1.0013
*/


if exists(select 1 from PatientFlow.Appointment) 
 begin 
	delete from PatientFlow.Appointment;
 end
 
 if exists(select 1 from PatientFlow.AppointmentSession) 
 begin 
	delete from PatientFlow.AppointmentSession;
 end
 
 if exists(select 1 from PatientFlow.Member) 
 begin 
	 delete from PatientFlow.Member;
 end
 
 if exists(select 1 from PatientFlow.NonAnonymousSurveyFrequency) 
 begin 
	delete from PatientFlow.NonAnonymousSurveyFrequency;
 end
 
 if exists(select 1 from PatientFlow.Patient) 
 begin 
	delete from PatientFlow.Patient;
 end
 
go

alter table PatientFlow.NonAnonymousSurveyFrequency
drop constraint FK_PatientFlow_NonAnonymousSurveyFrequency_PatientId_OrganisationId;
go


alter table PatientFlow.NonAnonymousSurveyFrequency
drop constraint FK_PatientFlow_NonAnonymousSurveyFrequency_OrganisationId;
go

alter table PatientFlow.Appointment
drop constraint  FK_PatientFlow_Appointment_PatientId_OrganisationId;
go

alter table PatientFlow.Appointment
drop constraint FK_PatientFlow_Appointment_SessionHolderId; 
go


alter table PatientFlow.Appointment
drop constraint PK_PatientFlow_Appointment_AppointmentId_OrganisationId;
go


alter table PatientFlow.Appointment
drop constraint FK_PatientFlow_Appointment_OrganisationId;
go



alter table PatientFlow.AppointmentSession
drop constraint PK_PatientFlow_AppointmentSession_SessionId_OrganisationId;
go

alter table PatientFlow.AppointmentSession
drop constraint FK_PatientFlow_AppointmentSession_SessionHolderId; 
go



alter table PatientFlow.AppointmentSession
drop constraint FK_PatientFlow_AppointmentSession_OrganisationId;
go 

alter table PatientFlow.Member
drop constraint PK_PatientFlow_Member_MemberId; 
go


alter table PatientFlow.Patient
drop constraint PK_PatientFlow_Patient_PatientId_OrganisationId;
go



------------------ PatientFlow.Patient ------------------ 


alter table PatientFlow.Patient
add PatientFlowPatientId int identity(1,1) not null;


alter table PatientFlow.Patient
add constraint PK_PatientFlow_Patient_PatientFlowPatientId
primary key(PatientFlowPatientId);
 
 
alter table PatientFlow.Patient
add constraint UQ_PatientFlow_Patient_PatientId_OrganisationId unique(PatientId,OrganisationId); 
go



------------------ PatientFlow.Member ------------------ 


alter table PatientFlow.Member
add 
	PatientFlowMemberId int identity(1,1) not null,
	OrganisationId int not null;
go


alter table PatientFlow.Member
add constraint UQ_PatientFlow_Member_MemberId_OrganisationId unique(MemberId,OrganisationId);
go


alter table PatientFlow.Member
add constraint PK_PatientFlow_Member_PatientFlowMemberId primary key(PatientFlowMemberId);
go

alter table PatientFlow.Member 
add constraint FK_PatientFlow_Member_OrganisationId foreign key (OrganisationId) 
    references PatientFlow.Organisation (OrganisationId);
go



------------------ PatientFlow.Appointment ------------------ 

alter table PatientFlow.Appointment 
drop column SessionHolderId;
go

alter table PatientFlow.Appointment 
drop column PatientId;
go

alter table PatientFlow.Appointment 
drop column OrganisationId;
go


alter table PatientFlow.Appointment
add PatientFlowAppointmentId int identity(1,1) not null
go

alter table PatientFlow.Appointment
add constraint PK_PatientFlow_Appointment_PatientFlowAppointmentId
primary key(PatientFlowAppointmentId)
go
 
alter table PatientFlow.Appointment
add PatientFlowPatientId int not null;
go

alter table PatientFlow.Appointment 
add PatientFlowMemberId int not null;
go


alter table PatientFlow.Appointment
add constraint FK_PatientFlow_Appointment_PatientFlowPatientId
 foreign key(PatientFlowPatientId) references  PatientFlow.Patient(PatientFlowPatientId);
go

alter table PatientFlow.Appointment 
add constraint FK_PatientFlow_Appointment_PatientFlowMemberId foreign key (PatientFlowMemberId) 
    references PatientFlow.Member (PatientFlowMemberId);
go



------------------ PatientFlow.NonAnonymousSurveyFrequency ------------------ 


alter table PatientFlow.NonAnonymousSurveyFrequency 
drop column PatientId;
go

alter table PatientFlow.NonAnonymousSurveyFrequency 
drop column OrganisationId;
go

alter table PatientFlow.NonAnonymousSurveyFrequency
add PatientFlowPatientId int not null;
go

alter table PatientFlow.NonAnonymousSurveyFrequency
add constraint FK_PatientFlow_NonAnonymousSurveyFrequency_PatientFlowPatientId
 foreign KEY(PatientFlowPatientId)  REFERENCES PatientFlow.Patient(PatientFlowPatientId);
go


------------------ PatientFlow.AppointmentSession ------------------ 

 

alter table PatientFlow.AppointmentSession 
drop column SessionHolderId;
go

alter table PatientFlow.AppointmentSession 
drop column OrganisationId;
go

alter table PatientFlow.AppointmentSession
add PatientFlowAppointmentSessionId int identity(1,1) not null;
go

alter table PatientFlow.AppointmentSession
add constraint PK_PatientFlow_AppointmentSession_PatientFlowAppointmentSessionId
primary key(PatientFlowAppointmentSessionId);
go

alter table PatientFlow.AppointmentSession 
add PatientFlowMemberId int not null;
go

alter table PatientFlow.AppointmentSession 
add constraint FK_PatientFlow_AppointmentSession_PatientFlowMemberId foreign key (PatientFlowMemberId) 
    references PatientFlow.Member (PatientFlowMemberId);
	go







