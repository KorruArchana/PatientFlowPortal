if object_id ('[PatientFlow].[UpdateClientConfiguration]') is not null
	drop procedure [PatientFlow].[UpdateClientConfiguration];
go

create procedure PatientFlow.UpdateClientConfiguration
	@OrgIdList as PatientFlow.List  Readonly
as
begin
	set nocount on;
	set transaction isolation level read committed;
	begin try;
   
    
    delete app from PatientFlow.Appointment app
	join PatientFlow.Patient  pa on app.PatientFlowPatientId = pa.PatientFlowPatientId
	where OrganisationId not in ( select id from @OrgIdList)	
	
    delete app from PatientFlow.NonAnonymousSurveyFrequency app
	join PatientFlow.Patient  pa on app.PatientFlowPatientId = pa.PatientFlowPatientId
	where OrganisationId not in ( select id from @OrgIdList)
	
	delete app from PatientFlow.DemographicDetailsFrequency app
	join PatientFlow.Patient  pa on app.PatientFlowPatientId = pa.PatientFlowPatientId
	where OrganisationId not in ( select id from @OrgIdList)	
	
    delete from PatientFlow.Patient where OrganisationId not in ( select id from @OrgIdList)
    delete from PatientFlow.Member where OrganisationId not in ( select id from @OrgIdList)
	
	
	delete q from PatientFlow.KioskQuestionnaire q join PatientFlow.Questionnaire qr 
	on qr.QuestionnaireId = q.QuestionnaireId
	where OrganisationId not in ( select id from @OrgIdList)
	
	delete qa from PatientFlow.QuestionAnswerOptions qa 
	join PatientFlow.Question q on q.QuestionId = qa.QuestionId
	join PatientFlow.Questionnaire qr 
	on qr.QuestionnaireId = q.QuestionnaireId
	where OrganisationId not in ( select id from @OrgIdList)
	
	
	delete q from PatientFlow.Question q join PatientFlow.Questionnaire qr 
	on qr.QuestionnaireId = q.QuestionnaireId
	where OrganisationId not in ( select id from @OrgIdList)
	
	delete from PatientFlow.Questionnaire where OrganisationId not in ( select id from @OrgIdList)
	
	delete from PatientFlow.PatientFlowUser where OrganisationId not in ( select id from @OrgIdList)
	delete from PatientFlow.Organisation where OrganisationId not in ( select id from @OrgIdList)




end try
begin catch;
		declare @Error int, @Message varchar(4000);		
		select 
			@Error = error_number(), 
			@Message = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SavePatientFlowUser : %d: %s', 16, 1, @Error, @Message);
end catch;		
end