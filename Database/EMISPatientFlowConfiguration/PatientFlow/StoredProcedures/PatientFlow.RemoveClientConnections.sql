if object_id('PatientFlow.RemoveClientConnections') is not null
drop Procedure PatientFlow.RemoveClientConnections
go

create procedure PatientFlow.RemoveClientConnections
	@Type varchar(1)
AS
BEGIN
	set nocount on;
	set transaction isolation level read committed;
	if @Type = 'K'
		update 
			[patientflow].[kiosk]
		set 
			[connectionguid] = null,
			ConnectionStatus=3
		where [connectionguid] is not null
	else if @Type = 'S'
		update 
			[patientflow].[SyncService]
		set 
			[syncconnectionid] = null
		where [syncconnectionid]  is not null
	else if @Type = 'O'
		update 
			[patientflow].[SyncService]
		set 
			[orgconnectionid] = null
		where [orgconnectionid]  is not null
	else
		begin
			update 
				[patientflow].[kiosk]
			set 
				[connectionguid] = null,
				ConnectionStatus=3
			where [connectionguid] is not null
			update 
				[patientflow].[SyncService]
			set 
				[syncconnectionid] = null
			where [syncconnectionid] is not null
			update 
				[patientflow].[SyncService]
			set 
				[orgconnectionid] = null	
			where [orgconnectionid] is not null	
			
		end	  
END
GO
