if object_id ('[PatientFlow].[GetQuestionnairesByPatient]') is not null
	drop procedure [PatientFlow].[GetQuestionnairesByPatient];
go

create procedure [PatientFlow].[GetQuestionnairesByPatient]
	@PatientId int,
	@OrganisationId int
as

begin
	set nocount on;
	set transaction isolation level read committed;
	
	select	
		QuestionnaireId,
		p.PatientId,
		AccessedOn 
	from [PatientFlow].[NonAnonymousSurveyFrequency] na
	inner join PatientFlow.Patient p 
	on na.PatientFlowPatientId = p.PatientFlowPatientId
	where p.PatientId = @PatientId 
		  and  p.OrganisationId = @OrganisationId;
end