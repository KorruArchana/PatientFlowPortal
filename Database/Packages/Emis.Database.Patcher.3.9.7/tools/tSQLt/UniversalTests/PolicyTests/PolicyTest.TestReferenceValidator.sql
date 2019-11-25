
if object_id('PolicyTest.TestReferenceValidator') is not null
drop procedure PolicyTest.TestReferenceValidator;
go


create procedure PolicyTest.TestReferenceValidator  
as  
/*******************************************************************************

PolicyTest.TestReferenceValidator

Test PolicyTest.ReferenceValidator to ensure that it functions as designed.  Not
currently 100% comprehensive due to original design.

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

if object_id('PolicyTest.NonexistentReference') is not null drop procedure PolicyTest.NonexistentReference; 
if object_id('PolicyTest.NonexistentReference2') is not null drop procedure PolicyTest.NonexistentReference2; 
if object_id('PolicyTest.ValidReference') is not null drop procedure PolicyTest.ValidReference; 
if object_id('PolicyTest.ValidReference2') is not null drop procedure PolicyTest.ValidReference2; 
  
exec sp_executesql N'create procedure PolicyTest.NonexistentReference as

delete from CompletelyNonsenseTable';

exec sp_executesql N'create procedure PolicyTest.NonexistentReference2 as

delete from a
from CompletelyNonsenseTable a';

exec sp_executesql N'create procedure PolicyTest.ValidReference as

delete from a
from dbo.Patient a';

exec sp_executesql N'create procedure PolicyTest.ValidReference2 as

delete from a
from dbo.Patient p
join dbo.Organisation a on a.OrganisationId = p.RegistrationOrganisationId';

declare @ValidWarning bit;

exec PolicyTest.ReferenceValidator 'PolicyTest', 'NonexistentReference', 'CompletelyNonsenseTable', @ValidWarning output

exec tSQLt.AssertEquals 1, @ValidWarning, 'Failed to identify issue on nonexistent table';

set @ValidWarning = null
exec PolicyTest.ReferenceValidator 'PolicyTest', 'NonexistentReference2', 'CompletelyNonsenseTable', @ValidWarning output

exec tSQLt.AssertEquals 1, @ValidWarning, 'Failed to identify issue on nonexistent table';

set @ValidWarning = null
exec PolicyTest.ReferenceValidator 'PolicyTest', 'NonexistentReference2', 'a', @ValidWarning output

exec tSQLt.AssertEquals 0, @ValidWarning, 'Warned incorrectly on table alias (from clause)';

set @ValidWarning = null
exec PolicyTest.ReferenceValidator 'PolicyTest', 'ValidReference', 'a', @ValidWarning output

exec tSQLt.AssertEquals 0, @ValidWarning, 'Warned incorrectly on table alias (from clause)';

set @ValidWarning = null
exec PolicyTest.ReferenceValidator 'PolicyTest', 'ValidReference2', 'a', @ValidWarning output

exec tSQLt.AssertEquals 0, @ValidWarning, 'Warned incorrectly on table alias (join clause)';
