if object_id('[PatientFlow].[QuestionnaireReport]') is not null
drop procedure [PatientFlow].[QuestionnaireReport]
go

create procedure [PatientFlow].[QuestionnaireReport]
	@KioskId int,
	@StartDate datetime,
	@EndDate datetime
as
begin

	set nocount on;
    set transaction isolation level read committed;
    
select distinct 
		QuestionnaireTitle,
		QuestionText,
		AnswerText,
		S.Modified 
from PatientFlow.Survey as S
inner join [PatientFlow].[Kiosk] as K 
on cast(K.KioskGuid AS varchar(max)) = s.RefKioskId
where  k.KioskId=@KioskId
and cast(s.[Modified] as date) >= cast(@StartDate as date)
and cast(s.[Modified] as date) <= cast(@EndDate as date)  
order by Modified desc,QuestionnaireTitle,QuestionText,AnswerText
end


