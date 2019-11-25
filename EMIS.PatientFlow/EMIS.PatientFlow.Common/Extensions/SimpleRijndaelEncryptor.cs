using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EMIS.PatientFlow.Common.Extensions
{
	public sealed class SimpleRijndaelEncryptor
	{
		private SimpleRijndaelEncryptor() { }

		private const string hashAlgorithm = "SHA1";

		private const int passwordIterations = 2;

		private const string initVector = "913609209BD84d2b";

		private const int keySize = 256;

		private const string passphrase = "EgtonPatientFlow";
		private const string saltValue = "kiosk";

		public static string EncryptAES256(string plaintext)
		{

			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

			byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

			PasswordDeriveBytes password = new PasswordDeriveBytes(
			   passphrase,
			   saltValueBytes,
			   hashAlgorithm,
			   passwordIterations);

			byte[] keyBytes = password.GetBytes(keySize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey.Mode = CipherMode.CBC;


			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
			   keyBytes,
			   initVectorBytes);

			MemoryStream memoryStream = new MemoryStream();
			byte[] cipherTextBytes;

			using (CryptoStream cryptoStream = new CryptoStream(memoryStream,
			   encryptor,
			   CryptoStreamMode.Write))
			{
				cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
				cryptoStream.FlushFinalBlock();
				cipherTextBytes = memoryStream.ToArray();
			}
			string cipherText = Convert.ToBase64String(cipherTextBytes);
			return cipherText;
		}

		public static string DecryptAES256(string cipherText)
		{

			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(
			   passphrase,
			   saltValueBytes,
			   hashAlgorithm,
			   passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey.Mode = CipherMode.CBC;
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
			   keyBytes,
			   initVectorBytes);
			byte[] plaintextBytes;
			int decryptedByteCount;

			using (CryptoStream cryptoStream = new CryptoStream(new MemoryStream(cipherTextBytes),
			   decryptor,
			   CryptoStreamMode.Read))
			{
				plaintextBytes = new byte[cipherTextBytes.Length];
				decryptedByteCount = cryptoStream.Read(plaintextBytes, 0, plaintextBytes.Length);

			}
			string plaintext = Encoding.UTF8.GetString(plaintextBytes, 0, decryptedByteCount);
			return plaintext;
		}
	}
}
