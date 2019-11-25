if object_id ('[PatientFlow].[UpdateKioskQuestionnaire]') is not null
	drop procedure [PatientFlow].[UpdateKioskQuestionnaire];
go

create procedure [PatientFlow].[UpdateKioskQuestionnaire]
	@QuestionnaireIdList as [PatientFlow].[ListWithOrder] readonly,
	@KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;

    delete from Patientflow.KioskQuestionnaire where KioskGuid=@KioskId;
 
	insert into Patientflow.KioskQuestionnaire
    (
		KioskGuid,
		QuestionnaireId,
		QuestionnaireOrder
	) 
	( 
	  select
		@KioskId, 
		Id,
		ListOrder 
	  from @QuestionnaireIdList
	 );   
end