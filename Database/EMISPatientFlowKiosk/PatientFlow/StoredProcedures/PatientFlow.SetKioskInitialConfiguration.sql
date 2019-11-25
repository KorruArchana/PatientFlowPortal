if object_id ('[PatientFlow].[SetKioskInitialConfiguration]') is not null
	drop procedure [PatientFlow].[SetKioskInitialConfiguration];
go

create procedure [PatientFlow].[SetKioskInitialConfiguration]
	@KioskConfiguration PatientFlow.KioskConfiguration readonly
as
begin
	set nocount on;
	
	set transaction isolation level read committed;
	begin try
	begin transaction; 
	 
    update [PatientFlow].[KioskConfiguration]
	set
		Value=kioskconfig.Value
	from @KioskConfiguration kioskconfig
	inner join [PatientFlow].[KioskConfiguration] k 
	on k.KioskId=kioskconfig.KioskId and k.ConfigType=kioskconfig.ConfigType;
		 
			insert into [PatientFlow].[KioskConfiguration]
			(
				ConfigType,
				KioskId,
				Value,
				Modified
			)
			select 
				kioskconfig.ConfigType,
				kioskconfig.KioskId,
				kioskconfig.Value,
				getdate()
			from @KioskConfiguration kioskconfig
			left outer join [PatientFlow].[KioskConfiguration] k 
			on k.KioskId=kioskconfig.KioskId and k.ConfigType=kioskconfig.ConfigType
			where k.KioskId is null and k.ConfigType is null;

commit transaction;
end try
begin catch
		declare @Error int, @Message varchar(4000);		
		select 
			@Error = error_number(), 
			@Message = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SetKioskInitialConfiguration : %d: %s', 16, 1, @error, @message);
end catch		
end
