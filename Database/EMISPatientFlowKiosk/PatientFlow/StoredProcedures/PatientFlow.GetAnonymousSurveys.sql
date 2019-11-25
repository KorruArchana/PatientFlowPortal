if object_id ('[PatientFlow].[GetAnonymousSurveys]') is not null
	drop procedure [PatientFlow].[GetAnonymousSurveys];
go

create procedure [PatientFlow].[GetAnonymousSurveys]
	@ProductKey varchar(50)	
as
begin
	set nocount on;
	set transaction isolation level read committed;
	declare @LastRowID bigint;
    
	select  @LastRowID = LastRowID 
	from PatientFlow.SynchronisationLog 
	where (SyncType = 2) and ProductKey=@ProductKey;
	 
	select	Row_number() over( Order by AnswerId ASC) as RowNo, 
					AnswerId, 
					KioskId, 
					RefQuestionnaireId as QuestionnaireId,
					RefQuestionId as QuestionId, 
					RefOptionId as OptionId, 
					AnswerText,
					QuestionnaireTitle,
					QuestionText, 
					ModifiedBy, 
					Modified
			from	AnonymousSurvey
			where	(AnswerId > isnull(@LastRowID, 0))
		
end

