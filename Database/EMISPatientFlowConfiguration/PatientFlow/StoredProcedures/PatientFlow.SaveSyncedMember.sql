if object_id('[PatientFlow].[SaveSyncedMember]') is not null
drop procedure [PatientFlow].[SaveSyncedMember]
go

create procedure [PatientFlow].[SaveSyncedMember]
	@MemberList PatientFlow.Member readonly,
	@OrganisationId int
as
begin
	
	set nocount on;
	set transaction isolation level read committed
	begin try
		begin transaction;  
		
			insert into [PatientFlow].[Member]
			(
				Firstname,
				Surname,
				Title,
				SessionHolderId,
				DepartmentId,
				OrganisationId,
				LoginId
			)
			select 
				memberList.Firstname,
				memberList.Surname,
				memberList.Title,
				memberList.SessionHolderId,
				memberList.DepartmentId,
				@OrganisationId,
				memberList.LoginId
			from @MemberList memberList
			left outer join [PatientFlow].[Member] m  
			on m.DepartmentId = memberList.DepartmentId and m.SessionHolderId=memberList.SessionHolderId
			where m.DepartmentId is null and m.SessionHolderId is null


			declare @DepartmentId int;
			set @DepartmentId =(select top 1 DepartmentId
								 from @MemberList memberList 
								)
			
		commit transaction;
	end try
	begin catch
		declare @Error int, @Message varchar(4000)		
		select 
			@Error = error_number(), 
			@Message = error_message()
		if xact_state() <> 0 begin
			rollback transaction
		end
		raiserror('SaveMember : %d: %s', 16, 1, @error, @message) ;
	end catch		
end
