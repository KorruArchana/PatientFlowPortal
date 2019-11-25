if object_id ('[PatientFlow].[SetKioskConfiguration]') is not null
	drop procedure [PatientFlow].[SetKioskConfiguration];
go

create procedure [PatientFlow].[SetKioskConfiguration]
	@ConfigType varchar(100),
	@KioskId varchar(50),
	@Value nvarchar(4000),
	@UserName varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;

    if exists (
		select 1 
		from Patientflow.KioskConfiguration 
		where (ConfigType = @ConfigType) and (KioskID = @KioskId))

        update Patientflow.KioskConfiguration  
	    set
			[Value] = @Value,
			[ModifiedBy] = @UserName,
			[Modified] = getdate()    
         where (ConfigType = @ConfigType) and (KioskID = @KioskId);
     else    
        insert into Patientflow.KioskConfiguration
        (
			[ConfigType],
			[KioskID],
			[Value],
			[ModifiedBy],
			[Modified]
		)
        values
        (
			@ConfigType,
			@KioskId,
			@Value,
			@UserName, 
			getdate()
		)    
  
end

go

exec sys.sp_addextendedproperty 
	@name = N'UnitTestException_TestForInvalidDataTypeUse',
	@value = N'Value should support multiple language text.',
	@level0type = N'SCHEMA',
	@level0name = 'PatientFlow',
	@level1type = N'PROCEDURE',
	@level1name = 'SetKioskConfiguration',
	@level2type = N'PARAMETER',
	@level2name = '@Value';
	
go