if object_id('[PatientFlow].[EnableMessageForMember]') is not null
drop procedure [PatientFlow].[EnableMessageForMember]
go

create procedure [PatientFlow].[EnableMessageForMember]
	@AlertId int,
	@MemberId int,
	@result bit output
as
begin
	
	set nocount on;
	set  transaction isolation level read committed;

	declare @LinkTyped int = 3
	insert into PatientFlow.AlertsLinkedToDepMem
	(
		AlertId,
		LinkTypeId,
		TypeId,
		ModifiedBy,
		Modified
	) 
	values
	(
		@AlertId, 
		@LinkTyped, 
		@MemberId, 
		'Administrator', 
		Getdate()
	)
	set @Result= 1; 

end

