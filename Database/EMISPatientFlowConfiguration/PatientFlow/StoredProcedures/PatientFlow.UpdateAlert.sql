if object_id('[PatientFlow].[UpdateAlert]') is not null
	drop procedure [PatientFlow].[UpdateAlert]
go

create procedure [PatientFlow].[UpdateAlert]
    @AlertText nvarchar(4000),
	@Gender varchar(50),
	@Age1 int,
	@Age2 int,
	@Operation varchar(50),
	@AlertDisplayFormatTypeId smallint,
	@OrganisationId int,
	@KioskList as [PatientFlow].[StringList] readonly,
	@AlertId int,	
	@ModifiedBy varchar(50),
	@Departments as [PatientFlow].[List] readonly,
    @Members as [PatientFlow].[List] readonly	
as
begin

set nocount on;
set transaction isolation level read committed;

update [PatientFlow].[Alert]
set 
	AlertText=@AlertText,
	Gender=@Gender,
	Age1=@Age1,
	Age2=@Age2,
	Operation=@Operation,
	ModifiedBy = @ModifiedBy,
	AlertDisplayFormatTypeId = @AlertDisplayFormatTypeId,
	Modified = getdate()
where AlertId=@AlertId;

exec [PatientFlow].[AddAlertLinkedToDepMemDetails] @AlertId,@Departments,@Members,@ModifiedBy

delete from Patientflow.AlertLinkToOrganisation
where AlertId=@AlertId

delete from Patientflow.AlertLinkToKiosk
where AlertId=@AlertId

insert into Patientflow.AlertLinkToOrganisation 
(
	AlertId,
	OrganisationId
) 
values
(
@AlertId,
@OrganisationId
)

insert into PatientFlow.AlertLinkToKiosk 
(
	AlertId,
	KioskId
) 
select 
	@AlertId,
	kiosk.KioskId 
from @KioskList kl
join PatientFlow.Kiosk kiosk on kiosk.KioskGuid = kl.value 
where Value is not null


select @AlertId as AlertId

end

go

exec sys.sp_addextendedproperty 
@name = N'UnitTestException_TestForInvalidDataTypeUse',
@value = N'Alert Message should support multiple language text.',
@level0type = N'SCHEMA',
@level0name = 'PatientFlow',
@level1type = N'PROCEDURE',
@level1name = 'UpdateAlert',
@level2type = N'PARAMETER',
@level2name = '@AlertText';



