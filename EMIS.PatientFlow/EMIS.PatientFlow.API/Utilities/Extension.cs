using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace EMIS.PatientFlow.API.Utilities
{
   public static class Extension
    {
       private const string _key = "7P9Z5x4q";
       public static T ConvertFromXmlString<T>(this string xml)
       {
           if (string.IsNullOrWhiteSpace(xml)) return default(T);

           var reader = new StringReader(xml);

           var serializer = new XmlSerializer(typeof(T));

           return (T)serializer.Deserialize(reader);
       }
      
       public static string Encrypt(this string value)
       {
           if (string.IsNullOrWhiteSpace(value)) return default(string);

           byte[] results;

           MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

           TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider()
           {
               Key = md5.ComputeHash(UTF8Encoding.Default.GetBytes(_key)),
               Mode = CipherMode.ECB,
               Padding = PaddingMode.PKCS7
           };

           byte[] data = UTF8Encoding.Default.GetBytes(value);
           try
           {
               ICryptoTransform encryptor = desalg.CreateEncryptor();
               results = encryptor.TransformFinalBlock(data, 0, data.Length);
           }
           finally
           {
               desalg.Clear();
               md5.Clear();
           }

           return Convert.ToBase64String(results);
       }
       public static string Decrypt(this string value)
       {
           if (string.IsNullOrWhiteSpace(value)) return default(string);

           byte[] results;

           MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
           TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider()
           {
               Key = md5.ComputeHash(UTF8Encoding.Default.GetBytes(_key)),
               Mode = CipherMode.ECB,
               Padding = PaddingMode.PKCS7
           };

           byte[] data = Convert.FromBase64String(value);

           try
           {
               ICryptoTransform decryptor = desalg.CreateDecryptor();
               results = decryptor.TransformFinalBlock(data, 0, data.Length);
           }
           finally
           {
               desalg.Clear();
               md5.Clear();

           }
           return UTF8Encoding.Default.GetString(results);
       }

		public static string Serialize(object dataToSerialize)
		{
			if (dataToSerialize == null) return null;

			using (StringWriter stringwriter = new StringWriter())
			{
				var serializer = new XmlSerializer(dataToSerialize.GetType());
				serializer.Serialize(stringwriter, dataToSerialize);
				return stringwriter.ToString();
			}
		}

		public static T Deserialize<T>(string xmlText)
		{
			if (String.IsNullOrWhiteSpace(xmlText))
				return default(T);

			using (StringReader stringReader = new System.IO.StringReader(xmlText))
			{
				var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("ClientIntegrationResponse"));
				return (T)serializer.Deserialize(stringReader);
			}
		}


		public static byte[] ReceiveAll(this Socket socket)
		{
			var buffer = new List<byte>();

			while (socket.Available > 0)
			{
				var currByte = new Byte[1];
				var byteCounter = socket.Receive(currByte, currByte.Length, SocketFlags.None);

				if (byteCounter.Equals(1))
				{
					buffer.Add(currByte[0]);
				}
			}

			return buffer.ToArray();
		}
	}
}
