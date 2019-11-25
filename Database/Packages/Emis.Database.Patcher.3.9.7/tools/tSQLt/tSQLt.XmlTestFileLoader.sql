
if object_id('tSQLt.XmlTestFileLoader') is not null
drop procedure tSQLt.XmlTestFileLoader;
go

create procedure tSQLt.XmlTestFileLoader  
  @Identifier uniqueidentifier,
  @FileContent xml
as  
/*******************************************************************************

tSQLt.XmlTestFileLoader  

Loads xmlfiles into test table for use against unit testing.

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

if exists(select * from tSQLt.XmlTestFile where Id = @Identifier)
begin

  update tSQLt.XmlTestFile
  set FileContent = @FileContent
  where  Id = @Identifier

end
else
begin
  insert tSQLt.XmlTestFile
  (
    Id,
    FileContent
  )
  values
  (
    @Identifier,
    @FileContent
  )

end