if object_id('[PatientFlow].[SetPublish]') is not null
drop procedure [PatientFlow].[SetPublish]
go

create procedure [PatientFlow].[SetPublish]
	@Status bit,
	@QuestionnaireId int,
	@ModifiedBy varchar(50)
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    
	update [PatientFlow].[Questionnaire]
	set
		IsActive=@Status,
		ModifiedBy = @ModifiedBy
	where QuestionnaireId=@QuestionnaireId
	
	select 
		QuestionnaireId,
		Title,
		Frequency,
		CreateConsultation,
		IsAnonymous,
		OrganisationId,
		IsActive		
	from [PatientFlow].[Questionnaire] 
	where QuestionnaireId=@QuestionnaireId	 
	
end
