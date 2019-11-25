if object_id('[PatientFlow].[SetDivert]') is not null
drop procedure [PatientFlow].[SetDivert]
go

create procedure [PatientFlow].[SetDivert]
	@Status bit,
	@SessionHolderId int,
	@OrganisationId int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    
	update [PatientFlow].[Member]
	set
		IsDivertSet=@Status	
	where SessionHolderId=@SessionHolderId
	and OrganisationId = @OrganisationId
	
	select 
		MemberId,
		LoginId,
		Firstname, 
		Surname, 
		SessionHolderId, 
		Title,
		IsDivertSet,
		OrganisationName
	from [PatientFlow].[Member] mem
	join PatientFlow.Organisation org on mem.OrganisationId = org.OrganisationId
	where SessionHolderId=@SessionHolderId
	and mem.OrganisationId = @OrganisationId	 
	
end
