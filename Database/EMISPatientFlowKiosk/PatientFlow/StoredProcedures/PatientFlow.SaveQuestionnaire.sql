if object_id ('[PatientFlow].[SaveQuestionnaire]') is not null
	drop procedure [PatientFlow].[SaveQuestionnaire];
go

create procedure [PatientFlow].[SaveQuestionnaire]
	@QuestionnaireId int,
	@Title varchar(150),
	@Frequency int,
	@CreateConsultation bit,
	@IsAnonymous bit,
	@OrganisationId int,
	@QuestionIdList as [PatientFlow].[ListWithOrder] readonly
as
begin
	set nocount on;
	set transaction isolation level read committed;
	if not exists 
	( 
		select 1 
		from PatientFlow.Questionnaire 
		where (QuestionnaireId = @QuestionnaireId))
		begin
			 insert into [PatientFlow].Questionnaire
			   (
				   [QuestionnaireId],
				   [Title],
				   [Frequency],
				   [CreateConsultation],
				   [IsAnonymous],
				   [Modified],
				   OrganisationId
			   )
			 values
			   (
				   @QuestionnaireId,
				   @Title,
				   @Frequency,
				   @CreateConsultation,
				   @IsAnonymous,
				   getdate(),
				   @OrganisationId
			   );
	   end
	 else
	  begin
	     update [PatientFlow].Questionnaire
			 set 
				[Title] = @Title,
				[Frequency] = @Frequency,
				[CreateConsultation] = @CreateConsultation,
				IsAnonymous=@IsAnonymous,
				OrganisationId=@OrganisationId
			 where [QuestionnaireId] = @QuestionnaireId;
		
		
			declare @temp table 
			(
				QuestionId int primary key,
				QuestionOrder int
			)
			insert into @temp 
			(
				QuestionId,
				QuestionOrder
			)
			( 
				select 
					Id,
					ListOrder 
				from @QuestionIdList
			);  
			declare @RowNum int;
			select @RowNum = count(*) from @temp

			 while @RowNum > 0
				 begin 
					declare @quesid int,@order int;
					select 
						@quesid=Questionid,
						@order=QuestionOrder 
					from @temp 
					where QuestionOrder=@RowNum;
			 
					update [PatientFlow].[Question]  
						set QuestionOrder= @order	
					where QuestionId= @quesid;
								
					set @RowNum = @RowNum - 1; 
				end
			end
	   
end
