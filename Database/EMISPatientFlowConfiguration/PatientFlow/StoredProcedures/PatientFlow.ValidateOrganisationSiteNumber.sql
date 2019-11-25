if object_id('[PatientFlow].[ValidateOrganisationSiteNumber]') is not null
drop procedure [PatientFlow].[ValidateOrganisationSiteNumber]
go

create procedure [PatientFlow].[ValidateOrganisationSiteNumber]
	@OrganisationSiteNumber varchar(100),
	@OrganisationId int,
	@Result bit output
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    if exists( select 1 from [PatientFlow].[Organisation] organisation
				where SiteNumber=@OrganisationSiteNumber and OrganisationId!=@OrganisationId
			 )
		set @Result= 0;   
	else 
		 set @Result= 1;
end
