set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

if exists (	select * 
			from sys.objects 
            where object_id = object_id(N'PolicyTest.ReferenceValidator')
			and type=N'P')
  drop procedure PolicyTest.ReferenceValidator
go

create procedure PolicyTest.ReferenceValidator
  @schema_name sysname,
  @entity_name sysname,
  @referenced_entity_name nvarchar(128),
  @validWarning bit output
as
/*******************************************************************************

PolicyTest.ReferenceValidator

Stored procedure to work out if a referenced entity points to a table alias or
a real table name, by seeing if there is a reference to a 'from' or 'join'
after every 'update', 'delete', etc.


Return:
-------
0 - Invalid warning (alias)
1 - Valid warning

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

declare @ProcName sysname = @schema_name + '.' + @entity_name;

create table #StoredProc
(
	Id int not null identity primary key,
	Line varchar(max)
)

insert into #StoredProc
(
	Line
)
exec sp_helptext @ProcName

set @validWarning = 1
	
select 
	sp1.Line,
	sp2.Line as Line2
into #Results
from #StoredProc sp1
left join #StoredProc sp2 on sp2.Id > sp1.Id and (sp2.Line like '%from _[a-z]% ' + @referenced_entity_name + '%' or sp2.Line like '%join _[a-z]% ' + @referenced_entity_name + '%')
where ('.' + sp1.Line + '.' like '%[^a-z]update ' + @referenced_entity_name + '[^a-z]%'
	or '.' + sp1.Line + '.' like '%[^a-z]update[^a-z]top[^a-z](%)[^a-z]' + @referenced_entity_name + '[^a-z]%'
	or '.' + sp1.Line + '.' like '%[^a-z]delete[^a-z]top[^a-z](%)[^a-z]' + @referenced_entity_name + '[^a-z]%'
	or '.' + sp1.Line + '.' like '%[^a-z]delete ' + @referenced_entity_name + '[^a-z]%'
	or '.' + sp1.Line + '.' like '%[^a-z]delete from ' + @referenced_entity_name + '[^a-z]%') 

if @@rowcount > 0 and not exists (select * from #Results where Line2 is null)
set @validWarning = 0;
