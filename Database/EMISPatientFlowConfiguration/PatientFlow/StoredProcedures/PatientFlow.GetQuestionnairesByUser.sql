if object_id ('[PatientFlow].[GetQuestionnairesByUser]') is not null
	drop procedure [PatientFlow].[GetQuestionnairesByUser];
go

create procedure [PatientFlow].[GetQuestionnairesByUser] 
	@User varchar(200)
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
	from PatientFlow.Questionnaire as q
	join PatientFlow.Organisation org on q.OrganisationId = org.OrganisationId
	left outer join PatientFlow.KioskQuestionnaire as K on Q.QuestionnaireId = k.QuestionnaireId
	join PatientFlow.OrganisationAccessMapping mapping on q.OrganisationId = mapping.OrganisationId	
	where mapping.UserName = @User
	group by 
		Q.QuestionnaireId,
		Frequency,
		Title,
		CreateConsultation,
		IsAnonymous,
		Q.OrganisationId,
		OrganisationName,
		Q.IsActive
	order by 
		Title
    
end
 
 
