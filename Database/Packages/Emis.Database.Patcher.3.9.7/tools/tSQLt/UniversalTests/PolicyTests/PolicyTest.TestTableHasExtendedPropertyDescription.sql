if object_id('PolicyTest.TestTableHasExtendedPropertyDescription') is not null
drop procedure PolicyTest.TestTableHasExtendedPropertyDescription
go
 
create procedure PolicyTest.TestTableHasExtendedPropertyDescription
/**************************************************************************************************************   
Description:  A test to check that indexes are created in the correct file group following established standards.
**************************************************************************************************************/
as
set transaction isolation level read committed;
set nocount on;

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TableHasExtendedPropertyDescription';

if exists (
	select *
	from sys.extended_properties
	where name like @PolicyTestExceptionName
	and class = 0
	and class_desc = 'DATABASE'
	and major_id = 0
	and minor_id = 0
)
begin 
	return;
end;

select
	object_schema_name(po.object_id) as ObjectSchema
	, object_name(po.object_id) as TableName
	,	case	when (epMissing.major_id is null and epExceptionOverride.major_id is null)
						then 'Table Missing Extended Property Description'
					else 'Table in exceptions list yet has Extended Property Description'
		end as FailureReason
into #TestTableHasExtendedPropertyDescription
from PolicyTest.ProductObject po
	inner join sys.tables t
		on  po.object_id = t.object_id
	left outer join sys.extended_properties epMissing
		on  t.object_id = epMissing.major_id
		and epMissing.minor_id = 0 -- Actually at the table level.
		and epMissing.name not like 'UnitTestException_%' -- ignore our own overrides for other policy tests.
		and epMissing.name != 'AnonymisationApproach' -- ignore AnonymisationApproach extended properties.
	left outer join sys.extended_properties epExceptionOverride
		on  t.object_id = epExceptionOverride.major_id
		and epExceptionOverride.minor_id = 0 -- Actually at the table level.
		and epExceptionOverride.name = @PolicyTestExceptionName -- not explicitly overridden.		
where po.type = 'U' -- User Table
and (
	(epMissing.major_id is null and epExceptionOverride.major_id is null) -- Missing extended property and not overridden.
	or 
	(epMissing.major_id is not null and epExceptionOverride.major_id is not null) -- Has extended property and is overridden.
)
order by 
			case	when (epMissing.major_id is null and epExceptionOverride.major_id is null)
							then 1
						else 0
			end asc
	, object_schema_name(po.object_id) + '.' + object_name(po.object_id) asc;

if exists (
	select *
	from #TestTableHasExtendedPropertyDescription )
begin

	print '[PolicyTest].[TestTableHasExtendedPropertyDescription] failed. Table naming templates are provided below.';
	print '';
	
	declare TableNamingCursor cursor for
		select
			ObjectSchema,
			TableName
		from #TestTableHasExtendedPropertyDescription;

	open TableNamingCursor;

	declare		
		@ObjectSchema varchar(512), 
		@TableName varchar(512);

	fetch next from TableNamingCursor into @ObjectSchema, @TableName;
		
	while @@fetch_status = 0
	begin
	
		print 'exec sys.sp_addextendedproperty';
		print '	@name=N''MS_Description'',';
		print '	@value= #enter description text#,';
		print '	@level0type=N''SCHEMA'',';
		print '	@level0name=N''' + @ObjectSchema + ''',';
		print '	@level1type=N''TABLE'',';
		print '	@level1name=N''' + @TableName + ''';';
		print '';

		fetch next from TableNamingCursor into @ObjectSchema, @TableName;

	end

	close TableNamingCursor;
	deallocate TableNamingCursor;

end

exec tSQLt.AssertEmptyTable '#TestTableHasExtendedPropertyDescription';

drop table #TestTableHasExtendedPropertyDescription;