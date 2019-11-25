if object_id('PolicyTest.TestConstraintsAreTrusted') is not null
	drop procedure PolicyTest.TestConstraintsAreTrusted;
go

create procedure PolicyTest.TestConstraintsAreTrusted  
as  
/*******************************************************************************

PolicyTest.TestConstraintsAreTrusted

Ensures that all constraints are trusted.
The only time a check constraint should be created "with nocheck" is due to performance reasons, on 
very large tables that would take a long time to check and we can guarantee the data in there is correct.

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

with UntrustedConstraints as
(
	select
		schema_name(cc.[schema_id]) as SchemaName,
		object_name(cc.parent_object_id) as TableName,
		cc.name as ConstraintName,
		cc.[object_id]
	from sys.check_constraints cc
	where cc.is_not_trusted = 1
	union
	select
		schema_name(fk.[schema_id]) as SchemaName,
		object_name(fk.parent_object_id) as TableName,
		fk.name as ConstraintName,
		fk.[object_id]
	from sys.foreign_keys fk
	where fk.is_not_trusted = 1
)
select
	uc.SchemaName,
	uc.TableName,
	uc.ConstraintName
into #TestConstraintsAreTrustedActual
from UntrustedConstraints uc
inner join PolicyTest.ProductObject po
	on  uc.object_id = po.object_id
left outer join sys.extended_properties epException
	on  epException.major_id = uc.[object_id]
	and epException.minor_id = 0
	and epException.name = 'UnitTestException_TestConstraintsAreTrusted'
	and epException.class_desc = 'OBJECT_OR_COLUMN'	
where epException.major_id is null
order by 
	uc.SchemaName, 
	uc.TableName, 
	uc.ConstraintName;

exec tSQLt.AssertEmptyTable @TableName = '#TestConstraintsAreTrustedActual';