if object_id ('[PatientFlow].[GetSyncServices]') is not null
	drop procedure [PatientFlow].[GetSyncServices];
go

create procedure [PatientFlow].[GetSyncServices] 
	@OrganisationName varchar(100)= null,
	@PageNo int,
	@PageSize int,
	@TotalCount bigint output
as
begin	
	set nocount on;
	set transaction isolation level read committed;
	
	
        select @TotalCount = count(*)  
		from PatientFlow.SyncService as S 
		inner join PatientFlow.Organisation as O 
		on S.OrganisationId = O.OrganisationId 
		where O.OrganisationName like isnull(@OrganisationName,'') + '%';
		
		select 
			RowNo,
			ProductKey, 
			OrganisationId, 
			IsActivated, 
			IsConnected,
			OrganisationName, 
			SyncServiceId,
			KioskId,
			KioskName  
		from
		(
			select    
				Row_number() over( Order by o.OrganisationName asc) as RowNo, 
				S.ProductKey, 
				S.OrganisationId, 
				S.IsActivated, 
				case when S.SyncConnectionId is not null then 1  
					 when S.OrgConnectionId is not null then 1 else 0 end as IsConnected,
				O.OrganisationName, 
				S.SyncServiceId,
				s.KioskId,
				K.KioskName  
			from PatientFlow.SyncService as S 
			inner join PatientFlow.Organisation as O 
			on S.OrganisationId = O.OrganisationId
			left join PatientFlow.Kiosk k
			on s.KioskId = k.KioskId
			where O.OrganisationName like isnull(@OrganisationName,'') + '%'			  
		) as TBL
		where TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize);
      end 
