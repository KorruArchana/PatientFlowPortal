if object_id('[PatientFlow].[ValidateKioskName]') is not null
drop procedure [PatientFlow].[ValidateKioskName]
go

create procedure [PatientFlow].[ValidateKioskName]
	@KioskName varchar(100),
	@KioskId int,
	@OrganisationList as [PatientFlow].[List] readonly,
	@Result bit output
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    if exists( select 1 from [PatientFlow].[Kiosk] kiosk
				join [PatientFlow].[KioskLinkedToDetails] kioskdetails 
				on kiosk.KioskId=kioskdetails.KioskId
				where Title=@KioskName
				and kioskdetails.TypeId in (select Id from @OrganisationList)
				and kiosk.KioskId!=@KioskId
			 )
		set @Result= 0;   
	else 
		 set @Result= 1;
end
