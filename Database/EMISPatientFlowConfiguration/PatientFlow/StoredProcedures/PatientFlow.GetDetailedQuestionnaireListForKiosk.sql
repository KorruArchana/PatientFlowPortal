if object_id ('[PatientFlow].[GetDetailedQuestionnaireListForKiosk]') is not null
drop procedure [PatientFlow].[GetDetailedQuestionnaireListForKiosk];
go

create procedure [PatientFlow].[GetDetailedQuestionnaireListForKiosk]
	@KioskId	int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	Questionnaire.QuestionnaireId,
	Questionnaire.Title,
	Questionnaire.Frequency
from [PatientFlow].Questionnaire as Questionnaire
join [PatientFlow].KioskQuestionnaire as KioskQuestionnaire 
on Questionnaire.QuestionnaireId=KioskQuestionnaire.QuestionnaireId
where KioskQuestionnaire.KioskId= @KioskId
order by KioskQuestionnaire.QuestionnaireOrder
end
