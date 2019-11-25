if object_id('[PatientFlow].[UpdatePatient]') is not null
drop procedure [PatientFlow].[UpdatePatient]
go

create procedure [PatientFlow].[UpdatePatient]
	@Id int,
	@Message varchar(400),
	@Firstname varchar(100),
	@Surname varchar(100)=null,
	@Modifiedby Varchar(100)
as
begin
	set nocount on;
	set  transaction isolation level read committed;

	update [PatientFlow].[Patient]
	set
		[Message]=@Message,
		ModifiedBy=@ModifiedBy ,
		Firstname=@Firstname,
		Surname=@Surname,
		Modified =getdate()
	where Id=@Id

	select @Id

End


