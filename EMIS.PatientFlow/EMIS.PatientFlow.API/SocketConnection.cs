using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace EMIS.PatientFlow.API
{
	public static class SocketConnection
	{
		public static string  ConnectToMiddleWareSocket(string sbsend, string webServiceUrl)
		{
			string endpoint = webServiceUrl + "?requestString=" + sbsend;
			string responseString = string.Empty;
			using (HttpClient client = new HttpClient())
			{
				var response = client.GetAsync(endpoint).Result;
				if (response.IsSuccessStatusCode)
				{ 
					var responseContent = response.Content;
					responseString = responseContent.ReadAsStringAsync().Result;

					responseString = Regex.Unescape(responseString);
					if(responseString.Length > 3)
						responseString = responseString.Substring(1, responseString.Length - 2);
				}
				else
				{
					string message = string.Format("Failed to call the TPPMiddleWare API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
					//Logger.Instance.WriteLog(Common.Enums.LogType.Info, message, null, Utilities.GetAppSettingValue("RegistrationKey"));
				}
				return responseString;
			}
		}

		public static string ConnectToSocketlatest(string sbSend)
		{
			string responseString = string.Empty;
			try
			{
				responseString = ConnectToSocket(sbSend);
			}
			catch (Exception)
			{
				throw;
			}
			return responseString;
		}

		private static string ConnectToSocket(string sbSend)
		{
			try
			{
				byte[] receivedData = new byte[1000000]; // 4 Meg Buffer

				Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				mySocket.Connect("127.0.0.1", 40700);
				mySocket.ReceiveBufferSize = 8192;
				mySocket.ReceiveTimeout = 3000;
				SocketError error = new SocketError();
				byte[] msg = Encoding.ASCII.GetBytes(sbSend + "<EOF>");
				int bytesSent = mySocket.Send(msg);

				int numberOfBytesRead = 0;
				int totalNumberOfBytes = 0;
				do
				{
					numberOfBytesRead = mySocket.Receive(receivedData, totalNumberOfBytes, mySocket.ReceiveBufferSize, SocketFlags.None, out error);
					totalNumberOfBytes += numberOfBytesRead;
				}
				while (numberOfBytesRead == 0 || numberOfBytesRead == 8192);

				mySocket.Close();

				byte[] formatedBytes = new byte[totalNumberOfBytes];
				for (int i = 0; i < totalNumberOfBytes; i++)
				{
					formatedBytes[i] = receivedData[i];
				}


				string response = Encoding.ASCII.GetString(formatedBytes, 0, totalNumberOfBytes);

				return response;
			}
			catch (SocketException)
			{
				throw;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}

}
