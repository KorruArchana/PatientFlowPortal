if object_id ('PatientFlow.SaveDemographicDetailsFrequency') is not null
	drop procedure PatientFlow.SaveDemographicDetailsFrequency;
go

create procedure PatientFlow.SaveDemographicDetailsFrequency
	@PatientId int,
	@AccessedOn date,
	@OrganisationId int,
	@ValidDetails bit
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	declare @RowCount as int;
	select @RowCount=count(*) 
	from PatientFlow.DemographicDetailsFrequency ddf
		join PatientFlow.Patient p 
		on ddf.PatientFlowPatientId = p.PatientFlowPatientId
	where PatientId = @PatientId 		 
		  and OrganisationId = @OrganisationId;

	if(@RowCount>0)
		update ddf 
		set ddf.AccessedOn		=	@AccessedOn,
			ddf.ValidDetails	=	@ValidDetails 
			from PatientFlow.DemographicDetailsFrequency ddf 
			join PatientFlow.Patient p on ddf.PatientFlowPatientId = p.PatientFlowPatientId
		where PatientId = @PatientId 			  
			  and OrganisationId = @OrganisationId;
	else
		insert into  PatientFlow.DemographicDetailsFrequency
		(
			PatientFlowPatientId,			
			AccessedOn,
			ValidDetails			
		) 				
		select 
		PatientFlowPatientId,
		@AccessedOn,
		@ValidDetails
		from 
		PatientFlow.Patient 
		where Patientid = @PatientId  
			  and OrganisationId = @OrganisationId;
		
		
end