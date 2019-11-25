if object_id('[PatientFlow].[ValidateDepartmentName]') is not null
drop procedure [PatientFlow].[ValidateDepartmentName]
go

create procedure [PatientFlow].[ValidateDepartmentName]
	@DepartmentName varchar(100),
	@DepartmentId int,
	@OrganisationId int,
	@Result bit output
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    if exists( select 1 from [PatientFlow].[Department] department
				where DepartmentName=@DepartmentName 
				and OrganisationId=@OrganisationId
				and DepartmentId!=@Departmentid
			 )
		set @Result= 0;   
	else 
		 set @Result= 1;
end
