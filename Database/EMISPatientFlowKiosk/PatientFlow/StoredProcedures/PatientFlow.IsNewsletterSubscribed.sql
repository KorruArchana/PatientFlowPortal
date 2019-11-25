if object_id ('[PatientFlow].[IsNewsletterSubscribed]') is not null
	drop procedure [PatientFlow].[IsNewsletterSubscribed];
go

create procedure [PatientFlow].[IsNewsletterSubscribed]	
	@PatientId int,
	@OrganisationId int,
	@Result bit output
as
begin
	set nocount on;
	set transaction isolation level read committed;
 
	 if ((select [IsNewsletterSubscribed] 
	 from [PatientFlow].[Patient] 
	 where [PatientId] = @PatientId and 
	 OrganisationId=@OrganisationId) = 1)
		set @Result= 1;
	 else
		 set @Result= 0;   
	select @Result;
end
