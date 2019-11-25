if object_id('[PatientFlow].[AddAlert]') is not null
drop Procedure [PatientFlow].[AddAlert]
go

create procedure [PatientFlow].[AddAlert]
	@AlertText nvarchar(4000),
	@Gender varchar(50),
	@Age1 int,
	@Age2 int,
	@Operation varchar(50),
	@AlertDisplayFormatTypeId smallint,
	@OrganisationId int,
	@KioskList as [PatientFlow].[StringList] readonly,
	@ModifiedBy varchar(50),
    @Departments as [PatientFlow].[List] readonly,
    @Members as [PatientFlow].[List] readonly
as
begin
set nocount on;
set transaction isolation level read committed
begin try
	begin transaction
		insert into [PatientFlow].[Alert]
		(
			AlertText,
			Gender,
			AlertType,
			Age1,
			Age2,
			Operation,
			ModifiedBy,
			Modified,
			AlertDisplayFormatTypeId
		)
		values
		(
			@AlertText,
			@Gender,
			1,
			@Age1,
			@Age2,
			@Operation,
			@ModifiedBy,
			getdate(),
			@AlertDisplayFormatTypeId
		);

		declare @AlertId int
		set @AlertId= (select cast(scope_identity() as int))

	    exec [PatientFlow].[AddAlertLinkedToDepMemDetails] @AlertId,@Departments,@Members,@ModifiedBy
	    
	   insert into Patientflow.AlertLinkToOrganisation
	   (
			AlertId,
			OrganisationId
		)
        values
        (
        @AlertId,
        @OrganisationId
        )

		insert into PatientFlow.AlertLinkToKiosk 
		(
			AlertId,
			KioskId
		) 
	    select 
			@AlertId,
			kiosk.KioskId 
	from @KioskList kl
	join PatientFlow.Kiosk kiosk on kiosk.KioskGuid = kl.value 
	where Value is not null

			commit transaction;
			select @AlertId as AlertId
end try

begin catch
declare @Error int, @Message varchar(4000)		
select 
	@Error = error_number(), 
	@Message = error_message()
if xact_state() <> 0 begin
	rollback transaction
end
raiserror('AddAlert : %d: %s', 16, 1, @error, @message) ;        
rollback transaction
end catch

end

go

exec sys.sp_addextendedproperty 
	@name = N'UnitTestException_TestForInvalidDataTypeUse',
	@value = N'Alert Message should support multiple language text.',
	@level0type = N'SCHEMA',
	@level0name = 'PatientFlow',
	@level1type = N'PROCEDURE',
	@level1name = 'AddAlert',
	@level2type = N'PARAMETER',
	@level2name = '@AlertText';







