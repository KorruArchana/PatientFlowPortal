/*********************************************
**********************************************
Script for controlled edits to tSQLt code base.

Doing them after the fact means that the latest 
code can be "dropped in" without having to be
concerned with our own edits to the code.

**********************************************
*********************************************/

/*********************************************

tSQLt.ApplyColumnXMLSchemaBindingToFakedTable
=============================================

Upgrade to allow xml schema binding to be retained
on columns for faked tables.

Can be applied to the whole table or just a single column.

Will also upgrade tSQLt.FakeTable to allow it to pass a 
parameter in to do it.

*********************************************/

create procedure tSQLt.ApplyColumnXMLSchemaBindingToFakedTable
	@TableName nvarchar(max),
	@ColumnName nvarchar(128) = null
as

declare 
	@CurrentObjectId int,
	@OriginalObjectId int,
	@ApplyXMLSchemaBindingToColumnInFakeTableSQL nvarchar(max) = N'',
	@FullyQualifiedObjectName nvarchar(256);

select 
	@CurrentObjectId = ron.objectId,
	@OriginalObjectId = oti.OrgTableObjectId,
	@FullyQualifiedObjectName = ron.quotedFullName 
from [tSQLt].[Private_ResolveObjectName] (@TableName) ron
	outer apply [tSQLt].[Private_GetOriginalTableInfo] (ron.objectId) oti;

if (@CurrentObjectId is null or @OriginalObjectId is null)
begin 
	raiserror(N'Faked Table cannot be found to apply xml schema bindings to columns.',16,1);
	return;
end;

select 
	@ApplyXMLSchemaBindingToColumnInFakeTableSQL += (
		'alter table '
		+ @FullyQualifiedObjectName
		+ char(13) + char(10) + char(9)
		+ ' alter column '
		+ quotename(c.name) 
		+ ' xml '
		+ '(content ' + quotename(schema_name(xsc.schema_id)) + '.' + quotename(xsc.name) + ') '
		-- require the "faked object's" nullablity state
		+ case when cFaked.is_nullable = 0 then 'not ' else '' end
		+ 'null;' 
		+ char(13) + char(10)
	)
from sys.all_columns c
	inner join sys.xml_schema_collections xsc
		on  c.xml_collection_id = xsc.xml_collection_id
	inner join sys.all_columns cFaked
		on  c.column_id = cFaked.column_id
where c.object_id = @OriginalObjectId
and c.name = isnull(@ColumnName, c.name)
and cFaked.object_id = @CurrentObjectId;

if @@rowcount > 0
begin 
	
	exec(@ApplyXMLSchemaBindingToColumnInFakeTableSQL);

end
go


/*********************************************

tSQLt.FakeTable
===============

This stored procedure allows itself to operate even
if it is not inside a transaction.

This corrupts systems if ran in the improper manner.

Create a wrapper proc to ensure the underlying procedure
cannot be called if a transaction is not involved.

*********************************************/
if exists ( 
	select *
	from sys.all_objects
	where object_id = object_id('tSQLt.FakeTable__AsShipped')
)
begin 
	
	raiserror(N'Object "tSQLt.FakeTable__AsShipped" already exists. This is an unexpected state. Cannot proceed.',16,1);
	return
	
end

declare @CurrentDefinition nvarchar(max)

select @CurrentDefinition = definition
from sys.all_sql_modules
where object_id = object_id('tSQLt.FakeTable')

declare @FakeTableParameters table
							(
								  ParameterName nvarchar(128) not null
								, ParameterId int not null
								, NextParameterName nvarchar(128) not null
								, RemainingCode nvarchar(max) null
							)

insert into @FakeTableParameters
							(
								  ParameterName
								, ParameterId
								, NextParameterName
							)
select  p.name as ParameterName
			, p.parameter_id as ParameterId
			, isnull(pNext.name,char(13)+char(10)+'AS') as NextParameterName -- Assumption as to how "AS" is coded.
from sys.parameters p
	left outer join sys.parameters pNext
		on  p.object_id = pNext.object_id
		and p.parameter_id = pNext.parameter_id - 1
where p.object_id = object_id('tSQLt.FakeTable')

declare @IterationsRequired int = @@rowcount
declare @CurrentIteration int = 1

declare @ParameterListIntoProcedure nvarchar(max) = N'';
declare @ParameterList nvarchar(max) = N'';

while @CurrentIteration <= @IterationsRequired
begin 

select  @ParameterListIntoProcedure = @ParameterListIntoProcedure + substring(@CurrentDefinition, charindex(ftp.ParameterName,@CurrentDefinition), charindex(ftp.NextParameterName,@CurrentDefinition) - charindex(ftp.ParameterName,@CurrentDefinition))
			, @ParameterList = @ParameterList + ftp.ParameterName + ' = ' + ftp.ParameterName + case when ParameterId = @IterationsRequired then '' else ',' + char(13) + char(10) end 
from @FakeTableParameters ftp
where ParameterId = @CurrentIteration

select @CurrentDefinition = substring(@CurrentDefinition, charindex(ftp.NextParameterName,@CurrentDefinition), len(@CurrentDefinition))
from @FakeTableParameters ftp
where ParameterId = @CurrentIteration

set @CurrentIteration = @CurrentIteration + 1

end

exec sp_rename 'tSQLt.FakeTable','FakeTable__AsShipped';

declare @ReplacingProcedureDefinition nvarchar(max);

set @ReplacingProcedureDefinition = 
'create procedure tSQLt.FakeTable ' + char(13) + char(10)
+ @ParameterListIntoProcedure + ',' + char(13) + char(10)
+ '@RetainXMLSchemaBinding bit = 0,' + char(13) + char(10)
+ '@AllowFakeOfPreviouslyFakedTable bit = 0
as 
-- If we are not in a transaction do not allow this
if @@trancount = 0
begin

	-- Using exceptionally high severity and "with log" option to sever the connection immediately.
	raiserror(N''Fake table attempted whilst not inside a transaction. Not permitted due to potential corruption'', 20, -1) with log;
	return;

end

if object_id(@TableName) is null
begin 
	
	declare @ErrorMessage nvarchar(4000) = N''Could not locate the table/object "'' + @TableName + N''" to fake. Please check the table/object name.'';
	
	raiserror(@ErrorMessage,16,1);
	
	return;

end

if @AllowFakeOfPreviouslyFakedTable = 0
begin 
	if exists (
		select *
		from [tSQLt].[Private_GetOriginalTableInfo] (object_id(@TableName)) oti
		where OrgTableName is not null
	)
	begin 
		raiserror(N''Table %s has already been faked and therefore cannot be faked again. If this is intentional use the @AllowFakeOfPreviouslyFakedTable parameter to override this error'', 16, 1, @TableName);
		return;
	end;

end;

-- Remove schema binding from view definitions first when required.
exec tSQLt.RemoveSchemaBindingFromObject @FullTableName = @TableName;

-- if all is ok pass into the actual shipped procedure.
exec tSQLt.FakeTable__AsShipped' + char(13) + char(10)
+ @ParameterList
+ ';' + char(13) + char(10) + char(13) + char(10)
+ 'if @RetainXMLSchemaBinding = 1
begin

	exec tSQLt.ApplyColumnXMLSchemaBindingToFakedTable @TableName = @TableName;

end;'
	
exec (@ReplacingProcedureDefinition);

go

/*********************************************

tSQLt.Private_RenameObjectToUniqueName
======================================

The stored procedure does not specify that the rename
is for an object. This means it does not function correctly 
for certain objects (found with trying to FakeTable on 
WorkflowManager.Task initially).

Change the procedure if it is in the exact expected format.

If it is not present or in the correct format throw.

*********************************************/

declare 
	  @FullyQualifiedProcedureName nvarchar(256) = '[tSQLt].[Private_RenameObjectToUniqueName]'
	, @ExpectedProcedureDefinition varbinary(max) = 0x4300520045004100540045002000500052004F0043004500440055005200450020007400530051004C0074002E0050007200690076006100740065005F00520065006E0061006D0065004F0062006A0065006300740054006F0055006E0069007100750065004E0061006D0065000D000A002000200020002000400053006300680065006D0061004E0061006D00650020004E00560041005200430048004100520028004D004100580029002C000D000A00200020002000200040004F0062006A006500630074004E0061006D00650020004E00560041005200430048004100520028004D004100580029002C000D000A00200020002000200040004E00650077004E0061006D00650020004E00560041005200430048004100520028004D0041005800290020003D0020004E0055004C004C0020004F00550054005000550054000D000A00410053000D000A0042004500470049004E000D000A00200020002000530045005400200040004E00650077004E0061006D0065003D007400530051004C0074002E0050007200690076006100740065003A003A0043007200650061007400650055006E0069007100750065004F0062006A006500630074004E0061006D006500280029003B000D000A000D000A002000200020004400450043004C0041005200450020004000520065006E0061006D00650043006D00640020004E00560041005200430048004100520028004D004100580029003B000D000A0020002000200053004500540020004000520065006E0061006D00650043006D00640020003D002000270045005800450043002000730070005F00720065006E0061006D006500200027002700270020002B0020000D000A0020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000400053006300680065006D0061004E0061006D00650020002B00200027002E00270020002B00200040004F0062006A006500630074004E0061006D00650020002B0020002700270027002C00200027002700270020002B0020000D000A002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200040004E00650077004E0061006D00650020002B0020002700270027003B0027003B000D000A002000200020000D000A00200020002000450058004500430020007400530051004C0074002E0050007200690076006100740065005F004D00610072006B004F0062006A006500630074004200650066006F0072006500520065006E0061006D0065002000400053006300680065006D0061004E0061006D0065002C00200040004F0062006A006500630074004E0061006D0065003B000D000A000D000A000D000A00200020002000450058004500430020007400530051004C0074002E00530075007000700072006500730073004F007500740070007500740020004000520065006E0061006D00650043006D0064003B000D000A000D000A0045004E0044003B00
	, @ErrorMessage nvarchar(max);

if not exists (
	select asm.object_id
	from sys.all_sql_modules asm
	where asm.object_id = object_id(@FullyQualifiedProcedureName)
	and convert(varbinary(max),asm.definition) = @ExpectedProcedureDefinition
)
begin

	set @ErrorMessage  = N'Procedure ' + @FullyQualifiedProcedureName + N' cannot be located or has an altered definition. Cannot proceed.';
	
	raiserror(@ErrorMessage,16,1);
	
	return;

end


declare @ProcedureReplacementCode nvarchar(max) = N'
alter procedure [tSQLt].[Private_RenameObjectToUniqueName]
    @SchemaName nvarchar(max),
    @ObjectName nvarchar(max),
    @NewName nvarchar(max) = null output
as
begin

	/*****************
	NOTE procedure edited by EMIS to enforce that the object is qualified on the call.
	*****************/

	set @NewName=tSQLt.Private::CreateUniqueObjectName();

	-- We shouldn''t have any objects with "[" or "]" in them. If so that is another issue...
	select 
		  @SchemaName = quotename(replace(replace(@SchemaName,''['',''''),'']'',''''))
		, @ObjectName = quotename(replace(replace(@ObjectName,''['',''''),'']'',''''))

	-- Belt and braces... Ensure @NewName is in correct format (not containing injection).
	-- could not leave this as potentially injectable while editing it.
	-- Example expected format is : tSQLt_tempobject_1ef8c420522a4a599e992a31e100c030
	if	(@NewName not like ''tSQLt_tempobject_'' + replicate(''[a-f0-9]'',32))
	begin 
		
		declare @ErrorMessage nvarchar(max) = N''New name not in expected format. An unexpected code change could have been made to tSQLt code base. Derived name = '' + @NewName;
		raiserror(@ErrorMessage,16,1);
		return;
		
	end

	declare @RenameCmd nvarchar(max) = ''exec sp_rename @objname = ''''<<<@objname>>>'''', @newname =''''<<<@newname>>>'''', @objType = ''''OBJECT'''';''
	
	set @RenameCmd = replace(@RenameCmd, ''<<<@objname>>>'', @SchemaName + ''.'' + @ObjectName);
	set @RenameCmd = replace(@RenameCmd, ''<<<@newname>>>'', @NewName);
   
	exec tSQLt.Private_MarkObjectBeforeRename @SchemaName, @ObjectName;

	exec tSQLt.SuppressOutput @RenameCmd;

end;
'

exec sp_executeSQL @ProcedureReplacementCode;

go

/*********************************************

tSQLt.RemoveSchemaBindingFromObject
======================================

The stored procedure allows for the schema binding to
be dropped off objects (usually indexed views) to allow 
the underlying tables to be called through FakeTable.

tSQLt have this on their enhancement list however no
idea as yet when it will be delivered. Roll our own for 
now.

NOTE:- This used to be named "tSQLt.RemoveSchemaBindingFromViews"

*********************************************/

if object_id('tSQLt.RemoveSchemaBindingFromObject') is not null
begin 
	drop procedure tSQLt.RemoveSchemaBindingFromObject;
end
go

create procedure tSQLt.RemoveSchemaBindingFromObject
	  @FullTableName nvarchar(max)
as   
/*************************  
Stored procedure to allow for removing schema bindings from objects  
to allow the underlying tables to be mockable.  
  
This means that FakeTable can then be used on the principle tables.  
  
All unit tests must be run in a transaction and therefore this code  
will check that it is in a transaction and if not will fail.  
 
NOTE: Optional parameter added as "nvarchar(max)" to match FakeTable parameter.
  
*************************/  
set nocount on;  
set transaction isolation level read committed;  
  
-- If we are not in a transaction do not allow this  
if @@trancount = 0  
begin  
  
 -- Using exceptionally high severity and "with log" option to sever the connection immediately.  
 raiserror(N'Remove Schema Binding From Objects table attempted whilst not inside a transaction. Not permitted due to potential corruption', 20, -1) with log;  
 return;  
  
end  
declare @SupportedObjectType table (
	  ObjectType char(3) not null primary key
	, ObjectTypeFunctionalName varchar(20) not null
);
 
insert into @SupportedObjectType (
	  ObjectType
	, ObjectTypeFunctionalName
)
select 
	  'IF' as ObjectType
	, 'function' as ObjectTypeFunctionalName
union all
select 
	  'TF' as ObjectType
	, 'function' as ObjectTypeFunctionalName
union all
select 
	  'V' as ObjectType
	, 'view' as ObjectTypeFunctionalName;

declare @ObjectName nvarchar(128)
declare @ObjectTypeFunctionalName varchar(20)
declare @ObjectId int
declare @ObjectType char(3)
declare @RecreateObjectSQL nvarchar(max)   
declare @DropObjectSQL nvarchar(max)   
declare @SchemaBoundObjectList table (
	ObjectName nvarchar(128) not null,
	ObjectId int not null,
	ObjectType char(3) not null,
	Deferred bit not null default (0)
);
declare @ObjectText table (  
   RowId int identity(1,1)  
 , ObjectText nvarchar(max) not null  
);  

if @FullTableName is null
begin

	-- if no actual table name supplied then get all.
	insert into @SchemaBoundObjectList (
		ObjectName, 
		ObjectId,
		ObjectType
	)
	select 
		quotename(schema_name(av.schema_id)) + '.' + quotename(object_name(av.object_id)) as ObjectFullName,
		av.object_id as ObjectId,
		av.type as ObjectType
	from sys.all_sql_modules asm  
		inner join sys.all_objects av  
			on  asm.object_id = av.object_id
		inner join @SupportedObjectType sot
			on  av.type collate SQL_Latin1_General_CP1_CI_AS = sot.ObjectType
	where asm.is_schema_bound = 1  
	and is_ms_shipped = 0;  

end
else 
begin

	-- when a table name is input only break that one down.
	with SchemaBoundDependency as (
	select 
		av.object_id
	from sys.sql_expression_dependencies d
		inner join sys.all_objects av
			on  d.referencing_id = av.object_id
		inner join sys.all_sql_modules asm
			on  d.referencing_id = asm.object_id
	where d.referenced_id = object_id(@FullTableName)
	and d.referenced_class = 1 /* OBJECT_OR_COLUMN */
	and d.is_schema_bound_reference = 1
	and asm.is_schema_bound = 1  
	and av.is_ms_shipped = 0
	union all
	select 
		av.object_id
	from SchemaBoundDependency sbd
		inner join sys.sql_expression_dependencies d
			on  sbd.object_id = d.referenced_id
		inner join sys.all_objects av
			on  d.referencing_id = av.object_id		
		inner join sys.all_sql_modules asm
			on  d.referencing_id = asm.object_id
	where d.referenced_class = 1 /* OBJECT_OR_COLUMN */
	and d.is_schema_bound_reference = 1
	and asm.is_schema_bound = 1  
	and av.is_ms_shipped = 0
	)
	insert into @SchemaBoundObjectList (
		ObjectName, 
		ObjectId,
		ObjectType
	)
	select distinct
		quotename(schema_name(av.schema_id)) + '.' + quotename(object_name(av.object_id)) as ObjectFullName,
		av.object_id as ObjectId,
		av.type as ObjectType
	from SchemaBoundDependency sbd
		inner join sys.all_objects av
			on  sbd.object_id = av.object_id
		inner join @SupportedObjectType sot
			on  av.type collate SQL_Latin1_General_CP1_CI_AS = sot.ObjectType;

end
  
while exists (select * from @SchemaBoundObjectList)
begin

	update @SchemaBoundObjectList set Deferred = 0; 

	while exists (select * from @SchemaBoundObjectList where Deferred = 0)
	begin

		select top 1 
			@ObjectName = ObjectName, 
			@ObjectId = ObjectId,
			@ObjectType = ObjectType
		from @SchemaBoundObjectList 
		where Deferred = 0;

		if exists
		(
			select * from sys.sql_expression_dependencies d
			join @SchemaBoundObjectList sbvl on d.referencing_id = sbvl.ObjectId
			where d.referenced_id = @ObjectId
			and d.referenced_class = 1 /* OBJECT_OR_COLUMN */
			and d.is_schema_bound_reference = 1
		)
		begin
		
			 update @SchemaBoundObjectList set Deferred = 1 where ObjectName = @ObjectName;
			 
		end
		else
		begin
		   
			begin try 
				
				-- try a slightly more optimal method to replace the schemabound definition first.
				-- this has various "assumptions" as to the "with options" included on the object.
				
				set @ObjectTypeFunctionalName = (select sot.ObjectTypeFunctionalName from @SupportedObjectType sot where sot.ObjectType = @ObjectType);
				
				select @RecreateObjectSQL = stuff(Definition, charindex('with schemabinding', Definition), LEN('with schemabinding'), '')
				from sys.all_sql_modules asm
				where asm.object_id = @ObjectId

				set @RecreateObjectSQL = replace(@RecreateObjectSQL, 'create ' + @ObjectTypeFunctionalName, 'alter ' + @ObjectTypeFunctionalName);

				exec (@RecreateObjectSQL);

				if exists (
					select *
					from sys.all_sql_modules asm
					where asm.object_id = @ObjectId
					and asm.is_schema_bound = 1
				)
				begin
					raiserror(N'Schemabinding not removed correctly.',16,1);
				end;

			end try
			begin catch

				-- Old "slower method" if the more optimised version has any issues altering the object.
				delete from @ObjectText;  
				set @RecreateObjectSQL = '';  
			    
				insert into @ObjectText (  
					ObjectText  
				)  
				exec sp_helptext @ObjectName;  
			  
				set @DropObjectSQL = 'drop ' + (select sot.ObjectTypeFunctionalName from @SupportedObjectType sot where sot.ObjectType = @ObjectType) + ' ' + @ObjectName + ';';  
			  
				select @RecreateObjectSQL = @RecreateObjectSQL + ObjectText + ''  
				from @ObjectText  
				where charindex('withschemabinding',replace(ObjectText,' ',''),0) = 0  
				order by RowId;  
			  
				exec(@DropObjectSQL);  
				exec(@RecreateObjectSQL);  

			end catch;

			delete @SchemaBoundObjectList where ObjectName = @ObjectName;
			 
		end;
	end;
end;

go  

create procedure tSQLt.RemoveNoExpandHintFromDependentObject
	  @ReferencingObjectName nvarchar(max)
as   
/*************************  
Stored procedure to allow for removing noexpand hints   
  
This means that FakeTable can then be used on the principle tables.  
  
All unit tests must be run in a transaction and therefore this code  
will check that it is in a transaction and if not will fail.  
 
NOTE: Optional parameter added as "nvarchar(max)" to match FakeTable parameter.
  
*************************/  
set nocount on;  
set transaction isolation level read committed;  

if @@trancount = 0
begin 
	
	raiserror(N'Changes to remove noexpand hints must be performed inside a transaction.',16,1);
	return;

end;

declare @ReferencingObjectId int = object_id(@ReferencingObjectName);

with ObjectDependency as (
	select distinct
		referencing_id as ReferencingObjectId,
		referenced_id as ReferencedObjectId
	from sys.sql_expression_dependencies
), ObjectDependencyExpanded as (
	select 
		ReferencingObjectId as InitialReferencingObjectId,
		ReferencingObjectId,
		ReferencedObjectId,
		1 as NestLevel
	from ObjectDependency od
	where od.ReferencingObjectId = @ReferencingObjectId
	union all
	select 
		ode.InitialReferencingObjectId as InitialReferencingObjectId,
		od2.ReferencingObjectId,
		od2.ReferencedObjectId,
		ode.NestLevel + 1 as NestLevel
	from ObjectDependencyExpanded ode
		inner join ObjectDependency od2
			on  ode.ReferencedObjectId = od2.ReferencingObjectId
	where ode.InitialReferencingObjectId <> od2.ReferencingObjectId
	and od2.ReferencingObjectId <> od2.ReferencedObjectId
), ObjectDependencyFull as (
	select 
		InitialReferencingObjectId,
		ReferencedObjectId
	from ObjectDependencyExpanded
	where object_name(ReferencedObjectId) is not null
	union 
	select 
		@ReferencingObjectId as InitialReferencingObjectId,
		@ReferencingObjectId as ReferencedObjectId
)
select 
	object_schema_name(ReferencedObjectId) + '.' + object_name(ReferencedObjectId) as FullObjectName, 
	asm.definition as ObjectDefinition,
	InitialReferencingObjectId,
	ReferencedObjectId
into #DependentObject
from ObjectDependencyFull odf
	inner join sys.all_sql_modules asm
		on odf.ReferencedObjectId = asm.object_id
where odf.InitialReferencingObjectId = @ReferencingObjectId;

declare 
	@DropObjectSQL nvarchar(max),
	@ModifyObjectSQL nvarchar(max);

declare RecreateObject cursor read_only forward_only for
select 
	case when charindex('create ',ObjectDefinition,1) < isnull(nullif(charindex('alter ',ObjectDefinition,1),0), len(ObjectDefinition))
			then 'drop ' + (case ao.type when 'P' then 'procedure' when 'TF' then 'function' when 'IF' then 'function' when 'FN' then 'function' when 'V' then 'view' end) + ' ' + do.FullObjectName 
	end as DropSQL,
	replace(ObjectDefinition,'with(noexpand)','') as ObjectDefinition
from #DependentObject do
	inner join sys.all_objects ao
		on  do.ReferencedObjectId = ao.object_id
where do.ObjectDefinition like '%with(noexpand)%';

open RecreateObject;

fetch next from RecreateObject into @DropObjectSQL, @ModifyObjectSQL;

while @@fetch_status = 0
begin

	exec(@DropObjectSQL);
	exec(@ModifyObjectSQL);

	fetch next from RecreateObject into @DropObjectSQL, @ModifyObjectSQL;
end;

close RecreateObject;

deallocate RecreateObject;

drop table #DependentObject;

go

create procedure tSQLt.FakeTableReplicateTriggerOnFakedTable
	  @TableName nvarchar(max)
	, @TriggerName nvarchar(128)
as 
/*************************

Stored procedure to allow for faking a trigger on a 
table. The table must already been faked using tSQLt.FakeTable.

*************************/
set nocount on;
set transaction isolation level read committed;

-- If we are not in a transaction do not allow this
if @@trancount = 0
begin

	-- Using exceptionally high severity and "with log" option to sever the connection immediately.
	raiserror(N'Fake Trigger On Faked Table attempted whilst not inside a transaction. Not permitted due to potential corruption', 20, -1) with log;
	return;

end

declare @FakedTableObjectId int

select @FakedTableObjectId = rol.ObjectId
from tSQLt.Private_RenamedObjectLog rol
	inner join sys.all_objects o
		on  rol.ObjectId = o.object_id
where o.schema_id = schema_id(left(@TableName,charindex('.',@TableName,0)-1))
and rol.OriginalName = quotename(replace(replace(right(@TableName,len(@TableName) - charindex('.',@TableName,0)),'[',''),']',''))

if @@rowcount = 0
begin

	raiserror(N'Cannot locate requested object in Faked state. Cannot proceed.', 16, 1);
	return;
	
end

-- Look for the trigger on the table.
declare 
	  @TriggerDefinition nvarchar(max)

select 
	  @TriggerDefinition = asm.definition
from sys.triggers t
	inner join sys.all_sql_modules asm
		on  t.object_id = asm.object_id
	inner join sys.all_objects o
		on  t.object_id = o.object_id
where t.parent_id = @FakedTableObjectId
and t.name = @TriggerName;

if @@rowcount = 0
begin

	raiserror(N'Cannot locate requested trigger on object. Cannot proceed.', 16, 1);
	return;
	
end

set @TriggerDefinition = replace(@TriggerDefinition, @TriggerName, quotename('tsqlt_temptrigger_' + replace(lower(convert(nvarchar(128),newid())),'-','')))

-- not this assumes the trigger was created and not altered.... May need to clean up later.
-- This will create the trigger with the correct name on the "faked table".
exec(@TriggerDefinition);

go

/*********************************************

tSQLt.SpyProcedure
======================================

Current version of tSQLt.SpyProcedure creates objects (table)
in the actual schema of the procedure being spyed on meaning 
that - if created by accident - they are not disposed of correctly.

Change the implementation to create these temp objects in a
specific tSQLt Test Class schema for this purpose tSQLt_SpyProcedureLog.

These logging tables are created as 

	tSQLt_SpyProcedureLog.{SchemaName}_{ProcedureName}

for example (for Admin.LogAccess)

	tSQLt_SpyProcedureLog.Admin_LogAccess

*********************************************/

declare 
	  @ExpectedCodeBaseSpyProcedureDefinition varbinary(max) = 0x4300520045004100540045002000500052004F0043004500440055005200450020007400530051004C0074002E00530070007900500072006F006300650064007500720065000D000A0020002000200020004000500072006F006300650064007500720065004E0061006D00650020004E00560041005200430048004100520028004D004100580029002C000D000A002000200020002000400043006F006D006D0061006E00640054006F00450078006500630075007400650020004E00560041005200430048004100520028004D0041005800290020003D0020004E0055004C004C000D000A00410053000D000A0042004500470049004E000D000A0020002000200020004400450043004C0041005200450020004000500072006F006300650064007500720065004F0062006A0065006300740049006400200049004E0054003B000D000A002000200020002000530045004C0045004300540020004000500072006F006300650064007500720065004F0062006A006500630074004900640020003D0020004F0042004A004500430054005F004900440028004000500072006F006300650064007500720065004E0061006D00650029003B000D000A000D000A002000200020002000450058004500430020007400530051004C0074002E0050007200690076006100740065005F00560061006C0069006400610074006500500072006F00630065006400750072006500430061006E004200650055007300650064005700690074006800530070007900500072006F0063006500640075007200650020004000500072006F006300650064007500720065004E0061006D0065003B000D000A000D000A0020002000200020004400450043004C00410052004500200040004C006F0067005400610062006C0065004E0061006D00650020004E00560041005200430048004100520028004D004100580029003B000D000A002000200020002000530045004C00450043005400200040004C006F0067005400610062006C0065004E0061006D00650020003D002000510055004F00540045004E0041004D00450028004F0042004A004500430054005F0053004300480045004D0041005F004E0041004D00450028004000500072006F006300650064007500720065004F0062006A00650063007400490064002900290020002B00200027002E00270020002B002000510055004F00540045004E0041004D00450028004F0042004A004500430054005F004E0041004D00450028004000500072006F006300650064007500720065004F0062006A006500630074004900640029002B0027005F00530070007900500072006F006300650064007500720065004C006F006700270029003B000D000A000D000A002000200020002000450058004500430020007400530051004C0074002E0050007200690076006100740065005F00520065006E0061006D0065004F0062006A0065006300740054006F0055006E0069007100750065004E0061006D0065005500730069006E0067004F0062006A006500630074004900640020004000500072006F006300650064007500720065004F0062006A00650063007400490064003B000D000A000D000A002000200020002000450058004500430020007400530051004C0074002E0050007200690076006100740065005F00430072006500610074006500500072006F0063006500640075007200650053007000790020004000500072006F006300650064007500720065004F0062006A00650063007400490064002C0020004000500072006F006300650064007500720065004E0061006D0065002C00200040004C006F0067005400610062006C0065004E0061006D0065002C002000400043006F006D006D0061006E00640054006F0045007800650063007500740065003B000D000A000D000A002000200020002000520045005400550052004E00200030003B000D000A0045004E0044003B00
	, @CurrentCodeBaseSpyProcedureDefinition varbinary(max)

select @CurrentCodeBaseSpyProcedureDefinition = convert(varbinary(max),asm.definition)
from sys.all_sql_modules asm
where asm.object_id = object_id('tSQLt.SpyProcedure')

if @ExpectedCodeBaseSpyProcedureDefinition = @CurrentCodeBaseSpyProcedureDefinition
begin 

-- Add a new explicit test class for SpyProcedure objects to be created in.
exec tSQLt.NewTestClass 'tSQLt_SpyProcedureLog';

exec('ALTER PROCEDURE tSQLt.SpyProcedure
    @ProcedureName NVARCHAR(MAX),
    @CommandToExecute NVARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @ProcedureObjectId INT;
    SELECT @ProcedureObjectId = OBJECT_ID(@ProcedureName);

    EXEC tSQLt.Private_ValidateProcedureCanBeUsedWithSpyProcedure @ProcedureName;

    DECLARE @LogTableName NVARCHAR(MAX) = ''[tSQLt_SpyProcedureLog].'' + quotename(OBJECT_SCHEMA_NAME(@ProcedureObjectId) + ''_'' + OBJECT_NAME(@ProcedureObjectId));

    EXEC tSQLt.Private_RenameObjectToUniqueNameUsingObjectId @ProcedureObjectId;

    EXEC tSQLt.Private_CreateProcedureSpy @ProcedureObjectId, @ProcedureName, @LogTableName, @CommandToExecute;

    RETURN 0;
END;
---Build-
')

end
else 
begin 

	raiserror(N'tSQLt Procedure tSQLt.SpyProcedure was in an unexpected state. Cannot perform alter of procedure safely.',16,1);
	
end
go


create procedure tSQLt.ResultSetFilterDirectToTable
(
	@ResultsetNo	int,
	@Command		nvarchar(max),
	@TargetTable	nvarchar(255)
)
as 
/*********************************************

tSQLt.ResultSetFilterDirectToTable
======================================

The stored procedure extends tSQLt.ResultSetFilter to 
allow the result set to be saved in a table rather than 
being returned. It is primarily intended as a workaround 
when @Command inludes code that captures the resultset 
from a stored proc with 'insert into ... exec'. This 
prevents the result set from tSQLt.ResultSetFilter to
be captured as nested  'insert into ... exec' are not 
allowed.

*********************************************/
external name [Emis.TsqltExtensions].[ClrProcedures].[ResultSetFilterDirectToTable];
go

/*********************************************
Basic summary procedure for test execution durations.
*********************************************/
create procedure tSQLt.ExecutionTimesOfTestResultBreakdown
as 

select 
	count(*) as TotalTestExecutions,
	sum(datediff(ms,TestStartTime,TestEndTime)) as TotalTestExecutionMilliseconds,
	avg(datediff(ms,TestStartTime,TestEndTime)) as AverageTestExecutionMilliseconds,
	min(datediff(ms,TestStartTime,TestEndTime)) as MinimumTestExecutionMilliseconds,
	max(datediff(ms,TestStartTime,TestEndTime)) as MaximumTestExecutionMilliseconds
from tSQLt.TestResult;

select 
	Class as TestClass,
	count(*) as TotalTestExecutions,
	sum(datediff(ms,TestStartTime,TestEndTime)) as TotalTestExecutionMilliseconds,
	avg(datediff(ms,TestStartTime,TestEndTime)) as AverageTestExecutionMilliseconds,
	min(datediff(ms,TestStartTime,TestEndTime)) as MinimumTestExecutionMilliseconds,
	max(datediff(ms,TestStartTime,TestEndTime)) as MaximumTestExecutionMilliseconds
from tSQLt.TestResult
group by Class
order by sum(datediff(ms,TestStartTime,TestEndTime)) desc;

select 
	Class as TestClass,
	TestCase,
	datediff(ms,TestStartTime,TestEndTime) as TestExecutionMilliseconds
from tSQLt.TestResult
order by datediff(ms,TestStartTime,TestEndTime) desc;
go


/*********************************************
tSQLt.RunAllProvidedTestClasses
*********************************************/
declare 
	@Private_RunAllProcName varchar(100) = 'tsqlt.Private_RunAll',
	@Private_RunAllSyntax varbinary(max) = 0x4300520045004100540045002000500052004F0043004500440055005200450020007400530051004C0074002E0050007200690076006100740065005F00520075006E0041006C006C000D000A00200020004000540065007300740052006500730075006C00740046006F0072006D006100740074006500720020004E00560041005200430048004100520028004D004100580029000D000A00410053000D000A0042004500470049004E000D000A0020002000450058004500430020007400530051004C0074002E0050007200690076006100740065005F00520075006E0043007500720073006F00720020004000540065007300740052006500730075006C00740046006F0072006D006100740074006500720020003D0020004000540065007300740052006500730075006C00740046006F0072006D00610074007400650072002C002000400047006500740043007500720073006F007200430061006C006C006200610063006B0020003D00200027007400530051004C0074002E0050007200690076006100740065005F0047006500740043007500720073006F00720046006F007200520075006E0041006C006C0027003B000D000A0045004E0044003B00;

if not exists (
	select *
	from sys.all_sql_modules
	where object_id = object_id(@Private_RunAllProcName)
	and convert(varbinary(max),definition) = @Private_RunAllSyntax
)
begin 
	raiserror(N'Stored Procedure definition for %s cannot be verified. Cannot proceed as this could mean a functional change that needs to be accounted for. Please raise with Database Development.',16,1,@Private_RunAllProcName);
end;
go

create procedure tSQLt.NewTestClassToRunBeforeFunctionalTests
	@TestClassName nvarchar(max)
as

exec tSQLt.NewTestClass @TestClassName;

exec sys.sp_addextendedproperty 
    @name='tSQLt_UnitTestTestClassToRunBeforeFunctionalTests', 
    @value=N'true', 
    @level0type=N'SCHEMA',
    @level0name=@TestClassName;

go

create function tSQLt.GetTestClassBasedOffExecutionPoint (
	@GetTestClassToRunBeforeFunctionalTests bit
)
returns table
as
return (
	select 
		tc.Name
	from tSQLt.TestClasses tc
		left outer join sys.extended_properties ep
			on  ep.name = 'tSQLt_UnitTestTestClassToRunBeforeFunctionalTests'
			and ep.class = 3
			and tc.SchemaId = ep.major_id
			and ep.value = N'true'
			and ep.minor_id = 0
	where (
		(@GetTestClassToRunBeforeFunctionalTests = 1 and ep.major_id is not null)
		or (@GetTestClassToRunBeforeFunctionalTests = 0 and ep.major_id is null)
		or (@GetTestClassToRunBeforeFunctionalTests is null)
	)
)
go

create procedure tSQLt.RunAllBasedOffExecutionPoint
	@GetTestClassToRunBeforeFunctionalTests bit
as
/*************************
Copy of logic from Private_RunAll however allows for 
execution of provided list of unit test test classes.
*************************/
set nocount on;

declare 
	@TestClassName nvarchar(max),
	@TestProcName nvarchar(max),
	@TestResultFormatter nvarchar(max) = tSQLt.GetTestResultFormatter();

if (@GetTestClassToRunBeforeFunctionalTests = 1 or @GetTestClassToRunBeforeFunctionalTests is null)
begin
	exec tSQLt.Private_CleanTestResult;
end;

declare TestToExecute cursor local fast_forward for
select 
	Name
from tSQLt.GetTestClassBasedOffExecutionPoint (@GetTestClassToRunBeforeFunctionalTests);

open TestToExecute;
  
fetch next from TestToExecute into @TestClassName;
	
while @@fetch_status = 0
begin
	exec tSQLt.Private_RunTestClass @TestClassName;
    
	fetch next from TestToExecute into @TestClassName;
end;
  
close TestToExecute;
deallocate TestToExecute;
  
exec tSQLt.Private_OutputTestResults @TestResultFormatter;

go

/*********************************************

tSQLt.ResultsFormatterNoSuccessfulTestsInSummary
================================================

The stored procedure is a modified version of tSQLt.DefaultResultFormatter.
It formats the ouput in the same manner as the default formatter, except
that it excludes successfull tests from the Test Execution Summary. The
title of the summary has been canged to reflect this.

The modified formatter can be enabled by defining the following extented
property before execution tsqlt.Run or tsqlt.RunAll :

exec sp_addextendedproperty 
		@name =		N'tSQLt.ResultsFormatter',
		@value =	N'tSQLt.ResultsFormatterNoSuccessfulTestsInSummary',
		@level0type = N'SCHEMA',
		@level0name = N'tSQLt', 
		@level1type = N'PROCEDURE',
		@level1name = N'Private_OutputTestResults';


*********************************************/

create procedure tSQLt.ResultsFormatterNoSuccessfulTestsInSummary
as
begin
    declare @Msg1 nvarchar(max);  
    declare @Msg2 nvarchar(max);  
    declare @Msg3 nvarchar(max);  
    declare @Msg4 nvarchar(max);  
    declare @IsSuccess int;  
    declare @SuccessCnt int;  
    declare @Severity int;  
      
    select row_number() over(order by Result desc, Name asc) No, Name [Test Case Name], Result  
    into #Tmp  
    from tSQLt.TestResult
		where Result <> 'Success';  
      
    exec tSQLt.TableToText @Msg1 output, '#Tmp', 'No';  
  
    select @Msg3 = Msg,   
           @IsSuccess = 1 - SIGN(FailCnt + ErrorCnt),  
           @SuccessCnt = SuccessCnt  
    from tSQLt.TestCaseSummary();  
        
    select @Severity = 16*(1-@IsSuccess);  
      
    select @Msg2 = replicate('-', len(@Msg3)),  
           @Msg4 = char(13)+char(10);  
      
      
    exec tSQLt.Private_Print @Msg4,0;  
    exec tSQLt.Private_Print '+--------------------------------------------+',0;  
    exec tSQLt.Private_Print '|Test Execution Summary - Unsuccessful Tests |',0;  
    exec tSQLt.Private_Print '+--------------------------------------------+',0;  
    exec tSQLt.Private_Print @Msg4,0;  
    exec tSQLt.Private_Print @Msg1,0;  
    exec tSQLt.Private_Print @Msg2,0;  
    exec tSQLt.Private_Print @Msg3, @Severity;  
    exec tSQLt.Private_Print @Msg2,0;  
end;
go

create table tSQLt.UnexpectedTestExectutionDuringPatching (
	Class varchar(128) not null,
	TestCase varchar(500) not null,
	ExecutedAt datetime not null constraint DF_tSQLt_UnexpectedTestExectutionDuringPatching_ExecutedAt default (getdate()),
	constraint PK_tSQLt_UnexpectedTestExectutionDuringPatching_Class_TestCase_ExecutedAt
		primary key (
			Class,
			TestCase,
			ExecutedAt
		)
);
go

create trigger TR_tSQLt_TestResult_Insert_PopulateUnexpectedTestExecutionDuringPatching
	on tSQLt.TestResult
for insert 
as 

insert into tSQLt.UnexpectedTestExectutionDuringPatching (
	Class,
	TestCase
)
select
	convert(varchar(128),Class) as Class,
	convert(varchar(500),TestCase) as TestCase
from inserted;

go

