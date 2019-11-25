if object_id ('[PatientFlow].[GetReports]') is not null
	drop procedure [PatientFlow].[GetReports];
go

create procedure [PatientFlow].[GetReports]
	@PageNo int,
	@PageSize int,
	@TotalCount bigint output
as
begin
	set nocount on;
	set transaction isolation level read committed;   
	 
	select 
		@TotalCount = Count(*)   
	from [PatientFlow].[Report] Report 
	where ReportId != 3
		
	select 
		RowNo,
		ReportId,
		ReportName
	from
		(select	
			row_number() over(order by ReportName) as RowNo,
			[ReportId],
			[ReportName]					 
		 from [PatientFlow].[Report] 					
		) as TBL
	where TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize) and ReportId != 3
end





