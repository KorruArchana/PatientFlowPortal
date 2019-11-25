-- NOTE: This script must also be compatible with PDW patching therefore various syntax is not supported in the parser.

if exists(select * from sys.schemas where name = 'tSQLt')
begin
	-- Drop test class schemas
	declare @TestClass varchar(100);
	declare @sql nvarchar(4000);
	declare @NumberOfTestClassesToRemove int;

	select 
		@NumberOfTestClassesToRemove = count(*)
	from tSQLt.TestClasses tc; 

	-- NOTE: PDW compatibility
	while exists (
		select *
		from tSQLt.TestClasses tc
	)
	begin
		
		select top 1 
			@TestClass = Name
		from tSQLt.TestClasses tc;

		-- NOTE: PDW compatibility
		set @sql = N'exec tSQLt.DropClass ' + quotename(@TestClass);
		exec (@sql);

		set @NumberOfTestClassesToRemove = @NumberOfTestClassesToRemove - 1;
		
		if @NumberOfTestClassesToRemove < 0
		begin 
			raiserror(N'Removal of Test Classes is not functioning as expected. Breaking while loop to prevent infinite loop!', 16,1);
			break;
		end
		
	end

	-- Uninstall tSQLt
	-- NOTE: PDW compatibility
	exec (N'exec tSQLt.Uninstall')
	
	-- drop extension assembly
	-- NOTE: PDW compatibility
	if exists (select * from sys.assemblies where name = 'Emis.TsqltExtensions')
		exec('drop assembly [Emis.TsqltExtensions]')
		
end;