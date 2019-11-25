if object_id ('[PatientFlow].[SaveAnonymousSurvey]') is not null
	drop procedure [PatientFlow].[SaveAnonymousSurvey];
go

create procedure [PatientFlow].[SaveAnonymousSurvey]
	@AnonymousSurvey PatientFlow.AnonymousSurvey readonly
as
begin
set nocount on;
set transaction isolation level read committed;
insert into [PatientFlow].[AnonymousSurvey]
(
	KioskId,
	RefQuestionnaireId,
	QuestionnaireTitle, 
	RefQuestionId,
	QuestionText,
	RefOptionId,
	AnswerText
)
select distinct 
	KioskId,
	QuestionnaireId,
	QuestionnaireTitle,
	QuestionId,
	QuestionText,
	OptionId,
	AnswerText
from @AnonymousSurvey;
end
