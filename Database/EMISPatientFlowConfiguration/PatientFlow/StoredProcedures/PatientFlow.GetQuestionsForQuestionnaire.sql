if object_id ('[PatientFlow].[GetQuestionsForQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetQuestionsForQuestionnaire];
go

create procedure [PatientFlow].[GetQuestionsForQuestionnaire]
	@QuestionnaireId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select   
		QuestionId,
		QuestionText
	from     [PatientFlow].Question
	where    QuestionnaireId = @QuestionnaireId
	order by [QuestionOrder]
end
