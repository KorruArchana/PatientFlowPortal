if object_id ('[PatientFlow].[GetQuestions]') is not null
	drop procedure [PatientFlow].[GetQuestions];
go

create procedure [PatientFlow].[GetQuestions] 
	@QuestionnaireId int,
	@PageNo int,
	@PageSize int,
	@QuestionText varchar(1000) = null,
	@TotalCount bigint output
as
begin
	set nocount on;
	set transaction isolation level read committed;   
	if(@QuestionText is null or @QuestionText='')   
	begin
		select @TotalCount = Count(*)  
		from [PatientFlow].Question 
		where QuestionnaireId = @QuestionnaireId
		select 
		tbl.QuestionId,
		tbl.QuestionnaireId,
		tbl.QuestionText,
		tbl.AgeCriteria,
		tbl.LinkCount,
		tbl.QuestionType,
		tbl.Age1,
		tbl.Age2,
		tbl.Operation,
		tbl.Gender
		 from 
		(select Row_number() over(order by [QuestionOrder]) as RowNo,
			a.QuestionId,
			a.QuestionnaireId,
			a.QuestionText,
			a.AgeCriteria,
			a.LinkCount,
			a.QuestionType,
			a.Age1,
			a.Age2,
			a.Operation,
			a.Gender from
			(select 
					distinct 
					QuestionId,
					Question.QuestionnaireId,
					QuestionText,
					AgeCriteria,
					case when K.QuestionnaireId is null then 0 else 1 end as LinkCount,
					QuestionType,
					Age1,
					Age2,
					Operation,
					Gender,
					QuestionOrder
			 from	[PatientFlow].Question 
			 left outer join
             [PatientFlow].KioskQuestionnaire as K
			 on Question.QuestionnaireId =k.QuestionnaireId
			 where	Question.QuestionnaireId = @QuestionnaireId) as a)
			 as TBL
		where  TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize)
	end
	else
	begin
		select @TotalCount = Count(*)  
		from [PatientFlow].Question 
		where QuestionnaireId = @QuestionnaireId and 
			(QuestionText like '%' + @QuestionText + '%')
select 
		tbl.QuestionId,
		tbl.QuestionnaireId,
		tbl.QuestionText,
		tbl.AgeCriteria,
		tbl.LinkCount,
		tbl.QuestionType,
		tbl.Age1,
		tbl.Age2,
		tbl.Operation,
		tbl.Gender
		 from 
		(select Row_number() over(order by [QuestionOrder]) as RowNo,
			a.QuestionId,
			a.QuestionnaireId,
			a.QuestionText,
			a.AgeCriteria,
			a.LinkCount,
			a.QuestionType,
			a.Age1,
			a.Age2,
			a.Operation,
			a.Gender from
			(select 
					distinct 
					QuestionId,
					Question.QuestionnaireId,
					QuestionText,
					AgeCriteria,
					case when K.QuestionnaireId is null then 0 else 1 end as LinkCount,
					QuestionType,
					Age1,
					Age2,
					Operation,
					Gender,
					QuestionOrder
			 from	[PatientFlow].Question 
			 left outer join
             [PatientFlow].KioskQuestionnaire as K
			 on Question.QuestionnaireId =k.QuestionnaireId
			 where	Question.QuestionnaireId = @QuestionnaireId and 
			(QuestionText like '%' + @QuestionText + '%')) as a)
			 as TBL
		where  TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize)
		
	end
end

