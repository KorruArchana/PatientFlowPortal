if object_id ('[PatientFlow].[UpdateKioskStatus]') is not null
	drop procedure [PatientFlow].[UpdateKioskStatus];
go

create procedure [PatientFlow].[UpdateKioskStatus]
	@Value varchar(50),
	@KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;

    update PatientFlow.KioskConfiguration
	set
		Value=@Value
	where KioskId=@KioskId and ConfigType='Status';

	return 1;

end
