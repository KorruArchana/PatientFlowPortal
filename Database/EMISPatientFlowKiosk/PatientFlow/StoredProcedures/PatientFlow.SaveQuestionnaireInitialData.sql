if object_id ('[PatientFlow].[SaveQuestionnaireinitialData]') is not null
	drop procedure [PatientFlow].[SaveQuestionnaireinitialData];
go

create procedure [PatientFlow].[SaveQuestionnaireinitialData]
	@LastRowModifiedDate datetime=null,
	@QuestionnaireList PatientFlow.Questionnaire readonly
as
begin	
	set nocount on;
	set transaction isolation level read committed
	begin try
    
	begin transaction;  
   
		update PatientFlow.Questionnaire
		set
			Title=questionnaire.Title,
			Frequency=questionnaire.Frequency,
			CreateConsultation=questionnaire.CreateConsultation,
			IsAnonymous=questionnaire.IsAnonymous,
			OrganisationId=questionnaire.OrganisationId
		from @QuestionnaireList questionnaire
		inner join PatientFlow.Questionnaire q 
		on questionnaire.QuestionnaireId=q.QuestionnaireId;
		 
		insert into PatientFlow.Questionnaire
		(
			QuestionnaireId,
			Title,
			Frequency,
			CreateConsultation,
			IsAnonymous,
			OrganisationId
		)
		select 
			questionnaire.QuestionnaireId,
			questionnaire.Title,
			questionnaire.Frequency,
			questionnaire.CreateConsultation,
			questionnaire.IsAnonymous,
			questionnaire.OrganisationId
		from @QuestionnaireList questionnaire
		left outer join PatientFlow.Questionnaire q 
		on questionnaire.QuestionnaireId=q.QuestionnaireId
		where q.QuestionnaireId is null

		
		update [PatientFlow].[SynchronisationLog]
		set Modified=@LastRowModifiedDate
		where SyncType=5;

	commit transaction;

end try
begin catch;
	declare @Error int, @Message varchar(4000);	
	select 
		@Error = error_number(), 
		@Message = error_message();
	if xact_state() <> 0 begin
		rollback transaction;
	end
	raiserror('SaveQuestionnaireinitialData : %d: %s', 16, 1, @Error, @Message);
end catch;		
end
