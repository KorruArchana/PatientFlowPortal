if object_id('PolicyTest.TestConstraintsNamedCorrectly') is not null
drop procedure PolicyTest.TestConstraintsNamedCorrectly;
go

create procedure PolicyTest.TestConstraintsNamedCorrectly  
as  
/*******************************************************************************

PolicyTest.TestConstraintsNamedCorrectly

Ensures that all constraints are named according to standards

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;
   
-- Constraint naming
with AllConstraint as (  
	-- Foreign Keys.  
	select  
		fks.name as SchemaName,  
		fko.name as ObjectName,  
		replace(replace(fk.name, '[', ''), ']', '') as CurrentConstraintName,  
		'FK_'   
		+ case when fks.name = 'dbo' then '' else fks.name + '_' end  
		+ fko.name + '_'  
		+ ( -- Included Column list. 
			select stuff((
				select 
					'_' + fkcol.name  
				from sys.foreign_key_columns fkc   
					join sys.columns fkcol on fkcol.column_id = fkc.parent_column_id  
					join sys.columns rc on rc.object_id = fkc.referenced_object_id and rc.column_id = fkc.referenced_column_id  
				where fkcol.object_id = fk.parent_object_id  
				and fkc.constraint_object_id = fk.object_id  
				order by fkc.constraint_column_id  
				for xml path ('')  
			),1,1,'')  
		) as CorrectConstraintName         
	from sys.foreign_keys fk  
		join PolicyTest.ProductObject fko on fko.object_id = fk.parent_object_id  
		join sys.schemas fks on fks.schema_id = fko.schema_id  
	where fko.type = 'U'
	union all  
	-- Key Constraints.  
	select  
		ks.name,  
		ko.name,  
		replace(replace(k.name, '[', ''), ']', '') as CurrentConstraintName,  
		k.type + '_'  
		+ case when ks.name = 'dbo' then '' else ks.name + '_' end  
		+ ko.name + '_'  
		+ ( -- Included Column list.
			select stuff((     
				select 
					'_' + c.name  
				from sys.index_columns ic   
					join sys.columns c on c.column_id = ic.column_id  
				where c.object_id = ko.object_id 
				and ic.object_id = i.object_id 
				and ic.index_id = i.index_id  
				order by ic.key_ordinal
				for xml path ('') 
			),1,1,'')  
		) as CorrectConstraintName  
	from sys.key_constraints k  
		join PolicyTest.ProductObject ko on ko.object_id = k.parent_object_id  
		join sys.schemas ks on ks.schema_id = ko.schema_id  
		join sys.indexes i on i.index_id = k.unique_index_id and i.object_id = k.parent_object_id  
	where ko.type = 'U'  
	union all
	-- Default Constraints.  	   
	select   
		ds.name,   
		do.name,  
		replace(replace(d.name, '[', ''), ']', '') as CurrentConstraintName,
		case  
			when ds.name = 'dbo' then 'DF_' + do.name + '_' + c.name  
			else 'DF_' + ds.name + '_' + do.name + '_' + c.name 
		end as CorrectConstraintName
	from sys.default_constraints d  
		join PolicyTest.ProductObject do on do.object_id = d.parent_object_id  
		join sys.schemas ds on ds.schema_id = do.schema_id  
		join sys.columns c on c.object_id = do.object_id and c.column_id = d.parent_column_id  
	where do.type = 'U'  
), AllConstraintCaseSensitive as (
	select   
		SchemaName,  
		ObjectName,  
		CurrentConstraintName collate SQL_Latin1_General_CP1_CS_AS as CurrentConstraintName,
		CorrectConstraintName collate SQL_Latin1_General_CP1_CS_AS as CorrectConstraintName  
	from AllConstraint
), AllConstraintCaseSensitiveLengthCapped as (
	select   
		SchemaName,  
		ObjectName,  
		CurrentConstraintName,  
		case 
			when (len(CorrectConstraintName) > 128 and charindex('__',CurrentConstraintName,0) > 0)
				then left(CorrectConstraintName,3) + isnull(nullif(SchemaName + '_','dbo_'),'') + ObjectName + '_%' 
			when len(CorrectConstraintName) > 128
				then left(CorrectConstraintName,116) + '...truncated'
			else CorrectConstraintName
		end as CorrectConstraintName
	from AllConstraintCaseSensitive  
)
select   
	SchemaName,  
	ObjectName,  
	CurrentConstraintName,  
	CorrectConstraintName  
into #IncorrectlyNamedConstraint  
from AllConstraintCaseSensitiveLengthCapped  
where CurrentConstraintName not like replace(CorrectConstraintName,'_','[_]'); 

/* Check Constraint Policy Checker: 
-- Single Column constraint	  = CK_SchemaName_TableName_ColumnName (Example: CK_ETP_PrescriptionRejection_RejectionCode)
-- Multiple Column constraint = CK_SchemaName_TableName_%__OptionalDescription (Example: CK_Cryptography_KeyLock_Key__PreventInsert)
*/
with GetCheckConstraint as (
select
    ks.name as SchemaName,
    ko.name as ObjectName,    
    left(k.name,isnull(nullif(charindex('__',k.name,0),0)-1,len(k.name))) as CurrentConstraintName,
    k.name as ActualCurrentConstraintName,
    ko.name,
    'CK_' 
    + case when ks.name = 'dbo' then '' else ks.name + '_' end  
    + ko.name + '_'  
    + isnull((select cSingleton.name from sys.columns cSingleton where cSingleton.object_id = k.parent_object_id and cSingleton.column_id = k.parent_column_id and k.parent_column_id <> 0),'%')
	   as ConstraintNameAnalysis
    ,(  
    Select stuff((     
           (select '_' + cIncColumn.name   
		  from sys.columns cIncColumn 
            where cIncColumn.object_id = k.parent_object_id 
            and charindex(quotename(cIncColumn.name),k.definition,0) > 0
            order by cIncColumn.column_id  
            for xml path ('')
            )  
    ),1,1,'')  
   ) as FullColumnList
from sys.check_constraints k
inner join PolicyTest.ProductObject ko on k.parent_object_id = ko.object_id
join sys.schemas ks on ks.schema_id = k.schema_id  
where k.type = 'C' 
    and k.name <> 'CK_Audit_PatchType'
	and ko.type <> 'TT'
), GetCheckConstraintCaseSensitive as (
	select 
		SchemaName,
		ObjectName,
		CurrentConstraintName collate SQL_Latin1_General_CP1_CS_AS  as CurrentConstraintName,
		ActualCurrentConstraintName collate SQL_Latin1_General_CP1_CS_AS  as ActualCurrentConstraintName,
		ConstraintNameAnalysis collate SQL_Latin1_General_CP1_CS_AS  as ConstraintNameAnalysis,
		FullColumnList collate SQL_Latin1_General_CP1_CS_AS as FullColumnList
	from GetCheckConstraint
), BuildListOfInCorrectConstraintNames as
(
select 
    SchemaName,
    ObjectName,
    ActualCurrentConstraintName as CurrentConstraintName,
    case when right(ConstraintNameAnalysis,1) = '%' 
	   then left(ConstraintNameAnalysis,len(ConstraintNameAnalysis)-1) + FullColumnList 
    else ConstraintNameAnalysis
	   end as CorrectConstraintName
from GetCheckConstraintCaseSensitive
where CurrentConstraintName not like replace(ConstraintNameAnalysis,'_','[_]')
)
insert into #IncorrectlyNamedConstraint
(
    SchemaName,
    ObjectName,
    CurrentConstraintName,
    CorrectConstraintName
)  
select 
    SchemaName,
    ObjectName,
    CurrentConstraintName,
    CorrectConstraintName
from BuildListOfInCorrectConstraintNames

exec tSQLt.AssertEmptyTable @TableName = '#IncorrectlyNamedConstraint';