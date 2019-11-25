if object_id('[PatientFlow].[ValidateSessionHolderId]') is not null
drop procedure [PatientFlow].[ValidateSessionHolderId]
go

create procedure [PatientFlow].[ValidateSessionHolderId]
	@SessionHolderId int,
	@Result bit output
as
begin
	
	set nocount on;
	set  transaction isolation level read committed;

    if exists(  select 1 from [PatientFlow].[Member] member
				join [PatientFlow].[Department] department on member.departmentId=department.DepartmentId
				join [PatientFlow].[Organisation] organisation on department.OrganisationId=organisation.OrganisationId
				where SessionHolderId=@SessionHolderId and organisation.SystemTypeId in(1,2)
			)
		set @Result= 0;   
	else 
		 set @Result= 1;
end

