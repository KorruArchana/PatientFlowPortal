if object_id ('[PatientFlow].[GetMenuByPermission]') is not null
	drop procedure [PatientFlow].[GetMenuByPermission];
go

create procedure [PatientFlow].[GetMenuByPermission]
	@ParentMenuId	int
as
begin	
	set nocount on;
	set transaction isolation level read committed;
	select Count(1) from PatientFlow.KioskLinkedToDetails as KioskLinked 
	inner join PatientFlow.KioskModule as Module
    on KioskLinked.KioskId=Module.KioskId
    where KioskLinked.TypeId=@ParentMenuId and Module.ModuleId=2
end




