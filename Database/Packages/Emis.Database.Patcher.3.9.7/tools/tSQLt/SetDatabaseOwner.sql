
/* 
   
   If the owner recorded in sys.database_principals differs from the owner in
   the master database, CLR procs cannot be added.  This script checks for and 
   corrects that condition.

   This commonly occurs where a database was restored from a backup taken from
   another machine where the owner was not an in-built principal, but a 
   domain user.

*/

declare @MasterOwnerSID varbinary(85);
declare @DBOwnerSID varbinary(85);

select @MasterOwnerSID = owner_sid from master.sys.databases where name = db_name();
select @DBOwnerSID = sid from sys.database_principals where name = 'dbo';

if (@MasterOwnerSID <> @DBOwnerSID) or (@DBOwnerSID is null)
begin
	declare @NewOwnerName sysname = suser_sname(@MasterOwnerSID);
	exec sp_changedbowner @NewOwnerName;
end;
