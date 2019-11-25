
if object_id('tSQLt.XmlTestFile') is not null
drop table tSQLt.XmlTestFile;
go

create table tSQLt.XmlTestFile
(  
  Id uniqueidentifier not null,
  FileContent xml not null
)