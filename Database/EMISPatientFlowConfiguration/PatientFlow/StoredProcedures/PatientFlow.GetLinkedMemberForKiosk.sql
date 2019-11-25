if object_id ('[PatientFlow].[GetLinkedMemberForKiosk]') is not null
	drop procedure [PatientFlow].[GetLinkedMemberForKiosk];
go

create procedure [PatientFlow].[GetLinkedMemberForKiosk]
	@KioskAddress varchar(50)
as
begin
	set nocount on;
set transaction isolation level read committed;
    select	
		KioskLink.TypeId as MemberId,
		Member.SessionHolderId,
		department.OrganisationId as OrganisationId 
	from	[PatientFlow].[KioskLinkedToDepMemDetails] KioskLink
			join [PatientFlow].[Kiosk] Kiosk on KioskLink.KioskId = Kiosk.KioskId
			join [PatientFlow].[Member] member on KioskLink.TypeId = member.MemberId
			join [PatientFlow].[Department] department on member.DepartmentId = department.DepartmentId
	where	KioskGuid = @KioskAddress and 
			KioskLink.LinkTypeId = 3

	union 

	select	
		MemberId,
		Member.SessionHolderId,
		department.OrganisationId as OrganisationId 
	from	[PatientFlow].[Member] member
			join [PatientFlow].[Department] department on member.DepartmentId = department.DepartmentId
			join [PatientFlow].[KioskLinkedToDepMemDetails] KioskLink on KioskLink.TypeId = department.DepartmentId
			join [PatientFlow].[Kiosk] Kiosk on KioskLink.KioskId = Kiosk.KioskId
	where	KioskGuid=@KioskAddress and 
			KioskLink.LinkTypeId = 2;
end
