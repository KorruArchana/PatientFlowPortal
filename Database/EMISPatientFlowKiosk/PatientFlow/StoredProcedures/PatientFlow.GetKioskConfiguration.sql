if object_id ('[PatientFlow].[GetKioskConfiguration]') is not null
	drop procedure [PatientFlow].[GetKioskConfiguration];
go

create procedure [PatientFlow].[GetKioskConfiguration] 
	@ConfigType varchar(100),
	@KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select Value 
	from  Patientflow.KioskConfiguration 
	where  (ConfigType = @ConfigType) 
	and (KioskID like isnull(@KioskId,'')+'%');
end
