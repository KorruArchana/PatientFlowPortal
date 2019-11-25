if object_id ('[PatientFlow].[GetKioskListForQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetKioskListForQuestionnaire];
go

create procedure [PatientFlow].[GetKioskListForQuestionnaire]
@QuestionnaireId int,
@PageNo int,
@PageSize int,
@KioskTitle varchar(50)=null,
@TotalCount bigint output
as


set nocount on;
set transaction isolation level read committed;	

		select  @TotalCount = Count(Kiosk.Title)	
		from  [PatientFlow].KioskQuestionnaire as KioskQuestionnaire
		join  [PatientFlow].Kiosk as Kiosk 
			on Kiosk.KioskId = KioskQuestionnaire.KioskId
		join [PatientFlow].KioskLinkedToDetails as KioskLinkedToDetails
			on KioskLinkedToDetails.KioskId =Kiosk.KioskId
		join [PatientFlow].Organisation as organisation
			on organisation.OrganisationId = KioskLinkedToDetails.TypeId
		where KioskQuestionnaire.QuestionnaireId = @QuestionnaireId		
			and Title like ISNULL(@KioskTitle,'') + '%'

		select 
			RowNo,
			Title,
			OrganisationName
		from
			(  
			select 
				Row_number() over(order by organisation.OrganisationName) as RowNo,
				Kiosk.Title,
				organisation.OrganisationName
			from [PatientFlow].KioskQuestionnaire as KioskQuestionnaire
			join [PatientFlow].Kiosk as Kiosk 
			on Kiosk.KioskId = KioskQuestionnaire.KioskId
			join [PatientFlow].KioskLinkedToDetails as KioskLinkedToDetails
			on KioskLinkedToDetails.KioskId =Kiosk.KioskId
			join [PatientFlow].Organisation as organisation
			on organisation.OrganisationId = KioskLinkedToDetails.TypeId
			where KioskQuestionnaire.QuestionnaireId = @QuestionnaireId
			and Title like ISNULL(@KioskTitle,'') + '%'
			) as TBL
		where  TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize)


