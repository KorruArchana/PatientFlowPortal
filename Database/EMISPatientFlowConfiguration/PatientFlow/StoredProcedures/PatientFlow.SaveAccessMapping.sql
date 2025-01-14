if object_id ('[PatientFlow].[SaveAccessMapping]') is not null
drop procedure [PatientFlow].[SaveAccessMapping];
go
create procedure [PatientFlow].[SaveAccessMapping] 
	@UserName varchar(128),
	@AccessList PatientFlow.List readonly,
	@ModifiedBy varchar(50)
as
begin
set nocount on;
set transaction isolation level read committed
		begin try
		begin transaction;		
		        declare @PerCount int;				
				select @PerCount  = count(*) from @AccessList;
				
				if @PerCount = 0
				  delete from PatientFlow.OrganisationAccessMapping where upper(UserName) = upper(@UserName);
				else
					begin
						if not exists (select 1 from PatientFlow.OrganisationAccessMapping where  upper(UserName) = upper(@UserName))
							 begin
							   insert into PatientFlow.OrganisationAccessMapping
							   (
									UserName,
									OrganisationId,
									[ModifiedBy],
									[Modified]
								)
							    select 
									@UserName,
									Id,
									@ModifiedBy,
									GETDATE() 
								from @AccessList
							 end
						else
							 begin
							    insert into [PatientFlow].[OrganisationAccessMapping]  
								(
									UserName,
									OrganisationId,
									[ModifiedBy],
									[Modified]
								)
								select
									@UserName,
									Id,
									@ModifiedBy,
									GETDATE()
								from @AccessList a
								left outer join (select OrganisationId from  PatientFlow.OrganisationAccessMapping  where upper(UserName) = upper(@UserName))  m on a.Id = m.OrganisationId
								where m.OrganisationId is null;
							 end
						-- DELETE RECORDS
				        delete  from  PatientFlow.OrganisationAccessMapping
						where upper(UserName) = upper(@UserName)  and  OrganisationId not in ( select  Id from @AccessList )	 
					end
				
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
				raiserror('SaveAccessMapping : %d: %s', 16, 1, @error, @message) ;
		end catch     
end

