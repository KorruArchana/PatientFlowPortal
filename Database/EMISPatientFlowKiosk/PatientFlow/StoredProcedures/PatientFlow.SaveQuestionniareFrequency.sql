if object_id ('[PatientFlow].SaveQuestionniareFrequency') is not null
	drop procedure [PatientFlow].SaveQuestionniareFrequency;
go

create procedure [PatientFlow].SaveQuestionniareFrequency
	@QuestionnaireId int,
	@PatientId int,
	@AccessedOn date,
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	declare @RowCount as int;
	select @RowCount=count(*) 
	from [PatientFlow].[NonAnonymousSurveyFrequency] nsf
		join PatientFlow.Patient p 
		on nsf.PatientFlowPatientId = p.PatientFlowPatientId
	where PatientId = @PatientId 
		  and QuestionnaireId = @QuestionnaireId 
		  and OrganisationId = @OrganisationId;

	if(@RowCount>0)
		update nsf 
		set nsf.AccessedOn=@AccessedOn 
			from PatientFlow.NonAnonymousSurveyFrequency nsf 
			join PatientFlow.Patient p on nsf.PatientFlowPatientId = p.PatientFlowPatientId
		where PatientId = @PatientId 
			  and QuestionnaireId = @QuestionnaireId 
			  and OrganisationId = @OrganisationId;
	else
		insert into  [PatientFlow].[NonAnonymousSurveyFrequency]
		(
			PatientFlowPatientId,
			QuestionnaireId,
			AccessedOn			
		) 				
		select 
		PatientFlowPatientId,
		@QuestionnaireId,
		@AccessedOn 
		from 
		PatientFlow.Patient 
		where Patientid = @PatientId  
			  and OrganisationId = @OrganisationId;
		
		
end