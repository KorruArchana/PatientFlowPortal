drop trigger tSQLt.TR_tSQLt_TestResult_Insert_PopulateUnexpectedTestExecutionDuringPatching;
go

if object_id('tSQLtValidationTest.TestNoTestsExecutedNeedlesslyDuringPatchRun') is not null
drop procedure tSQLtValidationTest.TestNoTestsExecutedNeedlesslyDuringPatchRun
go
 
create procedure tSQLtValidationTest.TestNoTestsExecutedNeedlesslyDuringPatchRun
/**************************************************************************************************************   
Description:  Unit tests have been executed prior to when they were expected to be.
Checks for issues whereby developers have left a "run" or "run test class" in their unit tests.
**************************************************************************************************************/
as
set transaction isolation level read committed;
set nocount on;

exec tSQLt.AssertEmptyTable @TableName = 'tSQLt.UnexpectedTestExectutionDuringPatching', @Message = N'Unit tests have been executed prior to when they were expected to be.';

go
