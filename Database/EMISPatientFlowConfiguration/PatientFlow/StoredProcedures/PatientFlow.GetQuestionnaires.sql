if object_id ('[PatientFlow].[GetQuestionnaires]') is not null
	drop procedure [PatientFlow].[GetQuestionnaires];
go

create procedure [PatientFlow].[GetQuestionnaires]
as
begin
	set nocount on; 
	set transaction isolation level read committed;    

	select 
		Q.QuestionnaireId,
		Q.Frequency,
		Q.Title,
		Q.CreateConsultation,
		Q.IsAnonymous,
		Q.OrganisationId,
		OrganisationName,
		Q.IsActive,
		count(K.QuestionnaireId) as LinkCount
	from PatientFlow.Questionnaire as Q
	join PatientFlow.Organisation org on Q.OrganisationId = org.OrganisationId
	left outer join PatientFlow.KioskQuestionnaire as K on Q.QuestionnaireId = K.QuestionnaireId
	group by 
		Q.QuestionnaireId,
		Frequency,Title,
		CreateConsultation,
		IsAnonymous,
		Q.OrganisationId,
		OrganisationName,
		Q.IsActive
	order by Title

end

