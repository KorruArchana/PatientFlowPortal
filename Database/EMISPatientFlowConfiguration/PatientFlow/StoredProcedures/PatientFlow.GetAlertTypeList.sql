if object_id ('[PatientFlow].[GetAlertTypeList]') is not null
	drop procedure [PatientFlow].[GetAlertTypeList];
go
create procedure [PatientFlow].[GetAlertTypeList]
as	
begin
set nocount on;
set transaction isolation level read committed;
select 
	 AlertTypeId,
	 AlertTypeText
from [PatientFlow].[AlertType]
END
