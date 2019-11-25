if object_id ('[PatientFlow].[SyncMembers]') is not null
	drop procedure [PatientFlow].[SyncMembers];
go

create procedure [PatientFlow].[SyncMembers]	
	@OrganisationId int,
	@SystemType int,
	@MemberList PatientFlow.Member readonly
as

set nocount on;
set transaction isolation level read committed;
	
begin try;
begin transaction; 

--Update member identifier for members

update PatientFlow.MemberIdentifier
	set
		[OrganisationId] = @OrganisationId,		
		[SystemType] = @SystemType,
		[Value] = ml.MemberIdentifierValue
from @MemberList ml 
join PatientFlow.MemberIdentifier m on ml.MemberIdentifierValue = m.Value and  m.OrganisationId = @OrganisationId 
where m.Value is not null;

--insert into member identifier for new members

insert into PatientFlow.MemberIdentifier
(
	OrganisationId,
	SystemType,
	Value
)
select distinct
	@OrganisationId,	
	@SystemType,
	ml.MemberIdentifierValue
from @MemberList ml
	left outer join PatientFlow.MemberIdentifier m 	on ml.MemberIdentifierValue = m.Value and m.OrganisationId = @OrganisationId
where m.Value is null;
	 	 
-- Update known members.

update PatientFlow.Member
	set 
		[MemberId] = mid.PatientFlowMemberIdentifierId,
		[Title] = ml.Title,
		[FirstName] = ml.FirstName,
		[LastName] = ml.LastName,
		[ModifiedBy] = ml.ModifiedBy,
		[Modified] = getdate(),
		[WaitingTime] = 0
from @MemberList ml
	join PatientFlow.Memberidentifier mid on mid.Value = ml.MemberIdentifierValue and mid.SystemType = @SystemType
	join PatientFlow.Member m on mid.PatientFlowMemberIdentifierId = m.MemberId and m.OrganisationId = @OrganisationId 
where ml.MemberIdentifierValue is not null;

-- Insert new members.

insert into [PatientFlow].[Member]  
(
	[MemberId],
	[Title],
	[FirstName],
	[LastName],
	[ModifiedBy],	
	[OrganisationId],
	[WaitingTime]
)
select distinct 
	[MemberId] = mid.PatientFlowMemberIdentifierId,
	ml.Title,
	ml.FirstName,
	ml.LastName,
	ml.ModifiedBy,	
	@OrganisationId,
	ml.WaitingTime
from @MemberList ml
	join PatientFlow.Memberidentifier mid on mid.Value = ml.MemberIdentifierValue and mid.OrganisationId = @OrganisationId
	left join PatientFlow.Member m on mid.PatientFlowMemberIdentifierId = m.MemberId and m.OrganisationId = @OrganisationId
where m.PatientFlowMemberId is null;

commit transaction;

end try
begin catch;
		declare @Error int, @Message varchar(4000);		
		select 
			@Error = error_number(), 
			@Message = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SyncMembers : %d: %s', 16, 1, @Error, @Message);
end catch;