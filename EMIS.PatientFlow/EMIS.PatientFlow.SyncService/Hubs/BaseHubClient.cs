using System;
using System.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.SyncService.Helper;
using Microsoft.AspNet.SignalR.Client;

namespace EMIS.PatientFlow.SyncService.Hubs
{
	public class BaseHubClient : IDisposable
	{
		protected const int PageSize = 100;
		private IHubProxy _hubProxy=null;
		private HubConnection _hubConnection ;
		protected IHubProxy HubProxy {
			get
			{
				return _hubProxy;
			}
			set
			{
				this._hubProxy = value;
			}
		}
		protected HubConnection HubConnection
		{
			get
			{
				return _hubConnection;
			}
			set
			{
				this._hubConnection = value;
			}
		}
		internal string RegistrationKey = Utility.GetAppSettingValue("ProductKey");

		public ConnectionState State
		{
			get { return HubConnection.State; }
		}

		protected void Init()
		{
			if (HubConnection == null)
				HubConnection = new HubConnection(
					Utility.GetAppSettingValue("ServerURI"),
					"id=" + Utility.GetAppSettingValue("ProductKey"));
		}

		public void CloseHub()
		{
			HubConnection.Stop();
			HubConnection.Dispose();
		}


		public bool StartHub()
		{
			if (IsSyncConnected)
				return true;

			bool isSuccess = true;
			try
			{
				var connectionTrial = 2;
				var connectionState = true;
				while (connectionTrial > 0 && connectionState)
				{
					try
					{
						if (HubConnecionState)
						{
							HubConnection.Start().Wait();
							connectionState = false;
						}
						else
							connectionState = false;
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteLog(LogType.Debug, ex.Message, HubConnection.LastError, RegistrationKey);
						Thread.Sleep(500);
						connectionTrial--;
					}

					if (connectionTrial == 0 && connectionState)
						isSuccess = false;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Debug, ex.Message, ex, RegistrationKey);
			}

			return isSuccess;
		}

		public bool HubConnecionState
		{
			get
			{
				return !(IsSyncConnected
						 || HubConnection.State == ConnectionState.Reconnecting
						 || HubConnection.State == ConnectionState.Connecting);
			}
		}

		public bool IsSyncConnected
		{
			get { return HubConnection.State == ConnectionState.Connected; }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				CloseHub();
			}
		}
	}
}
