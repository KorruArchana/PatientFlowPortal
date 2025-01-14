if object_id('[PatientFlow].[ValidateOrganisationName]') is not null
drop procedure [PatientFlow].[ValidateOrganisationName]
go

create procedure [PatientFlow].[ValidateOrganisationName]
	@OrganisationName varchar(100),
	@OrganisationId int,
	@Result bit output
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    if exists( select 1 from [PatientFlow].[Organisation] organisation
				where OrganisationName=@OrganisationName and OrganisationId!=@OrganisationId
			 )
		set @Result= 0;   
	else 
		 set @Result= 1;
end
