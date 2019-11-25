using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EMIS.PatientFlow.Common.Extensions
{
    public static class Convertor
    {
        private const string _key = "7P9Z5x4q";
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            var lst = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                Type tp = typeof(T);
                //create instance of the type
                T obj = Activator.CreateInstance<T>();
                //fetch all properties
                PropertyInfo[] pf = tp.GetProperties();
                foreach (PropertyInfo pinfo in pf)
                {
                    //read the implemented custome atribute for a property
                    string colname = pinfo.Name;
                    if (colname.Length == 0) continue;
                    if (!dt.Columns.Contains(colname)) continue;
                    if (dr[colname] == null) continue;
                    if (dr[colname] == DBNull.Value) continue;
                    //set property value
                    pinfo.SetValue(obj, dr[colname], null);
                }

                lst.Add(obj);
            }

            return lst;
        }

        public static DataTable ConvertToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            object[] values = new object[props.Count];
			DataTable table = new DataTable();
            long propCount = props.Count;
            for (int i = 0; i < propCount; ++i)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (T item in data)
            {
                long valCount = values.Length;
                for (int i = 0; i < valCount; ++i)
                {
                    values[i] = props[i].GetValue(item);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        public static T ConvertFromXmlString<T>(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return default(T);

            var reader = new StringReader(xml);

            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(reader);
        }

        public static string ConvertObjectToXmlString(object classObject)
        {
            string xmlString;
            var xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }

            return xmlString;
        }

        public static T ConvertFromJsonString<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ConvertToJsonString(this object value)
        {
            if (value == null) return default(string);

            return JsonConvert.SerializeObject(value);
        }

        public static string Encrypt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return default(string);

            byte[] results;

            var md5 = new MD5CryptoServiceProvider();

            var desalg = new TripleDESCryptoServiceProvider()
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

            var md5 = new MD5CryptoServiceProvider();
            var desalg = new TripleDESCryptoServiceProvider()
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

		public static string EncryptAES256(this string value)
		{
			return string.IsNullOrWhiteSpace(value) ? default(string) : SimpleRijndaelEncryptor.EncryptAES256(value);
		}

	    public static string DecryptAES256(this string value)
		{
			return string.IsNullOrWhiteSpace(value) ? default(string) : SimpleRijndaelEncryptor.DecryptAES256(value);
		}

		public static T ParseEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string GetDisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            Object[] attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            string outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }
}
