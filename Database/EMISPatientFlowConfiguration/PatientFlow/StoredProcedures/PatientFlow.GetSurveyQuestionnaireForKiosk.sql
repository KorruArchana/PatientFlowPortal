if object_id ('[PatientFlow].[GetSurveyQuestionnaireForKiosk]') is not null
	drop procedure [PatientFlow].[GetSurveyQuestionnaireForKiosk];
go

create procedure [PatientFlow].[GetSurveyQuestionnaireForKiosk]
	@KioskId int,
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
    select 
		q.QuestionnaireId,
		q.Title,
		q.Frequency,
		q.IsAnonymous
	from [PatientFlow].Questionnaire as q
	join [PatientFlow].KioskQuestionnaire as kq on q.QuestionnaireId = kq.QuestionnaireId
	where kq.KioskId=@KioskId
		and q.IsAnonymous='true'
		and q.OrganisationId=@OrganisationId
	order by kq.QuestionnaireOrder;
	
end



