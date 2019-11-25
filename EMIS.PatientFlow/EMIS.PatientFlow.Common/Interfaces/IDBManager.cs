using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace EMIS.PatientFlow.Common.Interfaces
{
    public interface IDbManager
    {
        string ConnectionString
        {
            get; 
        }

        SqlConnection Connection
        {
            get;
        }

        SqlTransaction Transaction
        {
            get;
        }

        void Open();
        void Open(SqlConnection connection);
        void Close();
        void Close(SqlConnection connection);
        void BeginTransaction();
        void CommitTransaction();
        SqlConnection GetNewConnection();
        SqlCommand GetSqlCommand(string sqlQuery);
        SqlCommand GetSqlCommand(string sqlQuery, SqlConnection connection);
        SqlCommand GetSprocCommand(string sprocName);
        SqlCommand GetSprocCommand(string sprocName, SqlConnection connection);
        SqlParameter CreateNullParameter(string name, SqlDbType paramType);
        SqlParameter CreateOutputParameter(string name, SqlDbType paramType);
        SqlParameter CreateParameter(string name, Guid value);
        SqlParameter CreateParameter(string name, int value);

        SqlParameter CreateParameter(string name, XmlDocument value);
        SqlParameter CreateXmlParameter(string name, string value);
        SqlParameter CreateParameter(string name, decimal value);
        SqlParameter CreateParameter(string name, DateTime value);
        SqlParameter CreateParameter(string name, float value);
        SqlParameter CreateParameter(string name, bool value);
        SqlParameter CreateParameter(string name, string value, int size);
        SqlParameter CreateParameter(string name, object value, int size);

        SqlParameter CreateParameter(
            string name,
            DataTable value,
            string typeName);

        SqlParameter CreateParameter(
            string name,
            Byte[] value);
    }
}
