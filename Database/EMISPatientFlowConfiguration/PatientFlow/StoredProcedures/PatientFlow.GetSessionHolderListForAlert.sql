if object_id ('[PatientFlow].[GetSessionHolderListForAlert]') is not null
	drop procedure [PatientFlow].[GetSessionHolderListForAlert];
go

create procedure [PatientFlow].[GetSessionHolderListForAlert]
	@AlertId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select SessionHolderId from [PatientFlow].Member Member
	join [PatientFlow].AlertsLinkedToDepMem LinkedMember 
	on (case when LinkedMember.LinkTypeId = 3 Then Member.MemberId
			 when LinkedMember.LinkTypeId = 2 Then Member.DepartmentId end = LinkedMember.TypeId)
	where LinkedMember.AlertId=@AlertId;
	
end

