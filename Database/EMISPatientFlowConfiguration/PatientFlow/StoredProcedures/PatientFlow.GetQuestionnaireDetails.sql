if object_id ('[PatientFlow].[GetQuestionnaireDetails]') is not null
	drop procedure [PatientFlow].[GetQuestionnaireDetails];
go

create procedure [PatientFlow].[GetQuestionnaireDetails]
	@QuestionnaireId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select 
		QuestionnaireId,
		Title,
		Frequency,
		CreateConsultation,
		IsAnonymous,
		IsActive,
		OrganisationId
	from [PatientFlow].Questionnaire as Questionnaire
	where Questionnaire.QuestionnaireId = @QuestionnaireId

	select 
		QuestionId,
		QuestionnaireId,
		QuestionText,
		QuestionOrder,
		AgeCriteria,
		QuestionType,
		OptionCharLimit,
		Gender,
		Age1,
		Age2,
		Operation
	from [PatientFlow].Question
	where QuestionnaireId = @QuestionnaireId;

	select 
		OptionId, 
		Qa.QuestionId,
		OptionValue,
		NestedQuestionId,
		OptionCode,
		SnomedCode 		
	from [PatientFlow].[QuestionAnswerOptions] QA
	left join [PatientFlow].[Question] Question on QA.QuestionId=Question.QuestionId
	join [PatientFlow].Questionnaire as Q on Question.QuestionnaireId=Q.QuestionnaireId
	where Q.QuestionnaireId=@QuestionnaireId

	select 
		kq.KioskId,
		KioskName
	from PatientFlow.KioskQuestionnaire kq
	join PatientFlow.Kiosk k on kq.KioskId = k.KioskId
	where QuestionnaireId = @QuestionnaireId
	
end



