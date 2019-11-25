if object_id ('[PatientFlow].[GetQuestionnairesByType]') is not null
	drop procedure [PatientFlow].[GetQuestionnairesByType];
go

create procedure [PatientFlow].[GetQuestionnairesByType]
	@KioskId varchar(50),
	@IsAnonymous bit,
	@OrganisationId int
as

begin
	set nocount on;
	set transaction isolation level read committed;
	select distinct
		Questionnaire.QuestionnaireId , 
		Questionnaire.Title, 
		Questionnaire.IsAnonymous, 
		Questionnaire.CreateConsultation,
		Questionnaire.Frequency,
		[PatientFlow].[KioskQuestionnaire].QuestionnaireOrder
	into #temp
	from [PatientFlow].Questionnaire
		join [PatientFlow].[KioskQuestionnaire] on [PatientFlow].[KioskQuestionnaire].QuestionnaireId = [PatientFlow].[Questionnaire].QuestionnaireId
		join [PatientFlow].[Question] on [PatientFlow].[Question].QuestionnaireId = [PatientFlow].[Questionnaire].QuestionnaireId
	where [Questionnaire].IsAnonymous = @IsAnonymous 
		and [KioskQuestionnaire].KioskGuid = @KioskId
		and [Questionnaire].OrganisationId = @OrganisationId

	select * from #temp order by QuestionnaireOrder

	drop table #temp
end