if object_id ('[PatientFlow].[GetQuestionnaireList]') is not null
	drop procedure [PatientFlow].[GetQuestionnaireList];
go

create procedure [PatientFlow].[GetQuestionnaireList]
	@ProductKey varchar(50),
	@SyncedRowDate datetime,
	@LastRowDate datetime output
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select @LastRowDate = max(questionnaire.Modified)
	from [PatientFlow].Questionnaire
	join [PatientFlow].SyncService syncService on questionnaire.OrganisationId=syncService.OrganisationId
	where (questionnaire.Modified > isnull(@SyncedRowDate,'01/01/1900') and syncService.ProductKey = @ProductKey)

    select	
		QuestionnaireId,
		Title,
		Frequency,
		CreateConsultation,
		IsAnonymous,
		questionnaire.OrganisationId,
		IsActive
	from [PatientFlow].Questionnaire questionnaire
	join [PatientFlow].SyncService syncService on questionnaire.OrganisationId=syncService.OrganisationId
	where (questionnaire.Modified > isnull(@SyncedRowDate,'01/01/1900') and syncService.ProductKey = @ProductKey)
	
end