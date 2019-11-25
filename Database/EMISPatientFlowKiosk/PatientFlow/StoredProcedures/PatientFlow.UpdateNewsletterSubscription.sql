if object_id ('[PatientFlow].[UpdateNewsletterSubscription]') is not null
	drop procedure [PatientFlow].[UpdateNewsletterSubscription];
go

create procedure [PatientFlow].[UpdateNewsletterSubscription] 
	@PatientId int,
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;

    update [PatientFlow].[Patient]
	set 
		IsNewsletterSubscribed  = 1
	where PatientId = @PatientId and
	OrganisationId = @OrganisationId;
END
