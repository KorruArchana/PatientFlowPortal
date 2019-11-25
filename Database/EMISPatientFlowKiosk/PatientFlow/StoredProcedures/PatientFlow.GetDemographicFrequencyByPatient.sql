if object_id ('PatientFlow.GetDemographicFrequencyByPatient') is not null
	drop procedure PatientFlow.GetDemographicFrequencyByPatient;
go

create procedure PatientFlow.GetDemographicFrequencyByPatient
	@PatientId int,
	@OrganisationId int
as

begin
	set nocount on;
	set transaction isolation level read committed;
	
	select	
		AccessedOn 
	from PatientFlow.DemographicDetailsFrequency ddf
	inner join PatientFlow.Patient p 
	on ddf.PatientFlowPatientId = p.PatientFlowPatientId
	where p.PatientId = @PatientId 
	 and  p.OrganisationId = @OrganisationId;
end