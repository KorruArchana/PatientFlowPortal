if object_id ('[PatientFlow].[GetAnonymousSurveyReport]') is not null
	drop procedure [PatientFlow].[GetAnonymousSurveyReport];
go

create procedure [PatientFlow].[GetAnonymousSurveyReport]
	@StartDate datetime,
	@EndDate datetime
as
begin
set nocount on;
select * 
from [PatientFlow].[Survey]
where Modified between @StartDate and @EndDate 
order by Modified desc
end
