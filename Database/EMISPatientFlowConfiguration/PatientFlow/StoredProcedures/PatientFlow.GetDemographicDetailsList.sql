if object_id ('[PatientFlow].[GetDemographicDetailsList]') is not null
drop procedure PatientFlow.GetDemographicDetailsList;
go

create procedure PatientFlow.GetDemographicDetailsList
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select 
		DemographicDetailsTypeId,
		DemographicDetailsTypeName 
	from PatientFlow.DemographicDetailsType
end
