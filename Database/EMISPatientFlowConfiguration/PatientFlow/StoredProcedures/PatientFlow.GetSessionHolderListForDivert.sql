if object_id ('[PatientFlow].[GetSessionHolderListForDivert]') is not null
	drop procedure [PatientFlow].[GetSessionHolderListForDivert];
go

create procedure [PatientFlow].[GetSessionHolderListForDivert]
	@DivertId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select SessionHolderId from [PatientFlow].Member Member
	join [PatientFlow].[DivertLinkedToDetail] LinkedMember 
	on Member.MemberId=LinkedMember.TypeId
	where LinkedMember.DivertId=@DivertId;
end
