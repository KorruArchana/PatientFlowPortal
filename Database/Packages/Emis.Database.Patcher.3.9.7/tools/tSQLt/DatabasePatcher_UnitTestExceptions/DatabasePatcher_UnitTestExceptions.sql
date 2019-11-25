/**************************

Taken from Patch 1.0142

Patch needs to alter dbo.DBVersion which cannot be altered in a patch whilst it then wants to record 
into that table due to the fact that this would place a lock on the table.
	
*/

/*******************************************************************************
[PolicyTest].[TestIndexesCreatedInCorrectFileGroup]
*******************************************************************************/

if object_id('dbo.DBVersion') is not null
begin 

	if not exists (
			select *
			from::fn_listextendedproperty(N'UnitTestException_TestIndexesCreatedInCorrectFileGroup', N'SCHEMA', N'dbo', N'TABLE', N'DBVersion', 'index', 'PK_DBVersion_DBVersionId')
			)
	exec sys.sp_addextendedproperty @name=N'UnitTestException_TestIndexesCreatedInCorrectFileGroup', 
									@value=N'Adding into Exceptions. This CLUSTERED Index currently resides on filegroup PRIMARY but should reside on EMISExternalMessaging_Data', 
									@level0type=N'SCHEMA',
									@level0name=N'dbo', 
									@level1type=N'TABLE',
									@level1name=N'DBVersion', 
									@level2type=N'INDEX',
									@level2name=N'PK_DBVersion_DBVersionId';

	if not exists (
			select *
			from::fn_listextendedproperty(N'UnitTestException_TestIndexesCreatedInCorrectFileGroup', N'SCHEMA', N'dbo', N'TABLE', N'DBVersion', 'index', 'UQ_DBVersion_VersionNumber')
			)
	exec sys.sp_addextendedproperty @name=N'UnitTestException_TestIndexesCreatedInCorrectFileGroup', 
									@value=N'Adding into Exceptions. This NONCLUSTERED Index currently resides on filegroup PRIMARY but should reside on EMISExternalMessaging_Indexes', 
									@level0type=N'SCHEMA',
									@level0name=N'dbo', 
									@level1type=N'TABLE',
									@level1name=N'DBVersion', 
									@level2type=N'INDEX',
									@level2name=N'UQ_DBVersion_VersionNumber';

end;

if object_id('Patching.Audit') is not null
begin 

	if not exists (
		select *
		from::fn_listextendedproperty(N'UnitTestException_TestIndexesCreatedInCorrectFileGroup', N'SCHEMA', N'Patching', N'TABLE', N'Audit', 'index', 'PK_Patching_Audit_Id')
	)
	exec sys.sp_addextendedproperty @name=N'UnitTestException_TestIndexesCreatedInCorrectFileGroup', 
									@value=N'Adding into Exceptions. This CLUSTERED Index currently resides on filegroup PRIMARY', 
									@level0type=N'SCHEMA',
									@level0name=N'Patching', 
									@level1type=N'TABLE',
									@level1name=N'Audit', 
									@level2type=N'INDEX',
									@level2name=N'PK_Patching_Audit_Id';

end;