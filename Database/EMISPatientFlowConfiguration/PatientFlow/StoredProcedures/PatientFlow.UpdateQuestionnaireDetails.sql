if object_id('[PatientFlow].[UpdateQuestionnaireDetails]') is not null
drop procedure [PatientFlow].[UpdateQuestionnaireDetails]
go

create procedure [PatientFlow].[UpdateQuestionnaireDetails]
	@QuestionnaireId int,
	@Title varchar(200),
	@Frequency int,
	@CreateConsultation bit,
	@IsAnonymous bit,
	@IsActive bit,
	@OrganisationId int,
	@KioskList as [PatientFlow].[List] readonly,
	@ModifiedBy varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;

	begin try
	begin transaction

		update [PatientFlow].[Questionnaire]
		set
			Title=@Title,
			Frequency=@Frequency,
			CreateConsultation=@CreateConsultation,
			IsAnonymous=@IsAnonymous,
			IsActive = @IsActive,
			OrganisationId = @OrganisationId,
			ModifiedBy = @ModifiedBy,
			Modified = GETDATE()	
		where QuestionnaireId=@QuestionnaireId

		delete from Patientflow.KioskQuestionnaire
		where QuestionnaireId=@QuestionnaireId
			
		if not exists ( select  1 from @KioskList)
		begin 
			
			insert into Patientflow.KioskQuestionnaire 
			(
				KioskId,
				QuestionnaireId
			)
			select 
				k.KioskId,
				@QuestionnaireId 
			from PatientFlow.Kiosk k
			join PatientFlow.KioskLinkedToDetails d on k.KioskId = d.KioskId
			join PatientFlow.Organisation org on org.OrganisationId = d.TypeId
			where org.OrganisationId = @OrganisationId
		end	
			
		else
		begin
			insert into Patientflow.KioskQuestionnaire 
			(
				KioskId,
				QuestionnaireId
			)
			select 
				Id,
				@QuestionnaireId
			from @KioskList
		
		end

		select @QuestionnaireId as QuestionnaireId

	commit transaction
	end try
	
	begin catch
		declare @Error int, @Message varchar(4000)		
		select 
		@Error = error_number(), 
		@Message = error_message()
		if xact_state() <> 0 
			begin
				rollback transaction
			end
		raiserror('UpdateQuestionnaireDetails : %d: %s', 16, 1, @error, @message) ;  
	end catch
end




