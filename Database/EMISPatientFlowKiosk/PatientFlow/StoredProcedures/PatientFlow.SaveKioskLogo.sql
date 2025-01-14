if object_id ('[PatientFlow].[SaveKioskLogo]') is not null
	drop procedure [PatientFlow].[SaveKioskLogo];
go

create procedure [PatientFlow].[SaveKioskLogo]
   @KioskId varchar(50),
   @Logo varbinary(max)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	if exists(select 1 from [PatientFlow].[KioskLogo] where KioskId=@KioskId)
		update [PatientFlow].[KioskLogo]
		set
			Logo=@Logo
		where KioskId=@KioskId
	else
		insert into [PatientFlow].[KioskLogo]
		(
			KioskId,
			Logo
		)
		values
		(
			@KioskId,
			@Logo
		)
end