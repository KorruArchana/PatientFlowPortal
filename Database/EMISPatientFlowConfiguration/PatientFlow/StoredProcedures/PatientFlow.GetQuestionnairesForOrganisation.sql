if object_id ('[PatientFlow].[GetQuestionnairesForOrganisation]') is not null
	drop procedure [PatientFlow].[GetQuestionnairesForOrganisation];
go

create procedure [PatientFlow].[GetQuestionnairesForOrganisation]
@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select 
		QuestionnaireId,
		Title,
		Frequency,
		CreateConsultation,
		IsAnonymous
	from [PatientFlow].Questionnaire
	where IsAnonymous = 'false'	and OrganisationId=@OrganisationId
end





