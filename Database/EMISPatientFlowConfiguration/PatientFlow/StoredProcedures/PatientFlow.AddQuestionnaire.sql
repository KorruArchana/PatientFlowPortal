if object_id ('[PatientFlow].[AddQuestionnaire]') is not null
	drop procedure [PatientFlow].[AddQuestionnaire];
go

create procedure [PatientFlow].[AddQuestionnaire]
	@Title varchar(50),
	@Frequency int,
	@CreateConsultation bit,
	@IsAnonymous bit,
	@ModifiedBy varchar(50), 
	@OrganisationId int,
	@IsActive int,
	@KioskList as [PatientFlow].[List] readonly
as
begin
	set nocount on;
	set transaction isolation level read committed

	begin try
	begin transaction

	insert into [PatientFlow].[Questionnaire]
		(
			Title,
			Frequency,
			CreateConsultation,
			IsAnonymous,
			ModifiedBy,
			Modified,
			OrganisationId,
			IsActive
		)
		values
		(
			@Title,
			@Frequency,
			@CreateConsultation ,
			@IsAnonymous ,
			@ModifiedBy ,
			getdate(),
			@OrganisationId,
			@IsActive
		);
		
		declare @QuestionnaireId int
		set @QuestionnaireId= (select cast(scope_identity() as int))

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
			raiserror('AddQuestionnaire : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch

end




