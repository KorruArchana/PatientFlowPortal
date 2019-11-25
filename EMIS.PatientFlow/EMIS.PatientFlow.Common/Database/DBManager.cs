using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using EMIS.PatientFlow.Common.Interfaces;

namespace EMIS.PatientFlow.Common.Database
{
    public sealed class DbManager
        : IDbManager,
            IDisposable
    {
        private SqlConnection _dbConnection;
        private SqlTransaction _dbTransaction;
        private String _connectionString;

        public SqlConnection Connection
        {
            get
            {
                return _dbConnection;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        public String ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public DbManager(string connectionString)
        {
            _connectionString = connectionString;

            _dbConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Opens connection to the DB.
        /// </summary>
        public void Open()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
        }

        public void Open(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        /// <summary>
        /// Closes connection to the DB.
        /// </summary>
        public void Close()
        {
            if (_dbConnection.State != ConnectionState.Closed)
                _dbConnection.Close();
        }
       
        public void Close(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        public SqlConnection GetNewConnection()
        {
            return new SqlConnection(_connectionString);
        }
       
        public void BeginTransaction()
        {
            if (_dbTransaction == null)
                _dbTransaction = _dbConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbTransaction != null)
                _dbTransaction.Commit();

            _dbTransaction = null;
        }

        public void Dispose()
        {
            Close();

            _dbTransaction = null;
            _dbConnection = null;
        }
    
        public SqlCommand GetSqlCommand(string sqlQuery)
        {
            var command = new SqlCommand();
            command.Connection = (SqlConnection)_dbConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlQuery;
	        command.CommandTimeout = 120;
            return command;
        }

        public SqlCommand GetSqlCommand(string sqlQuery, SqlConnection connection)
        {
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlQuery;
			command.CommandTimeout = 120;
            return command;
        }

        /// <summary>
        /// Instantiates a SQL server Stored Procedure command type.
        /// </summary>
        /// <param name="sprocName">Name of the stored procedure.</param>
        /// <returns>Returns a SQL server command object.</returns>
        public SqlCommand GetSprocCommand(string sprocName)
        {
            var command = new SqlCommand(sprocName);
            command.Connection = (SqlConnection)_dbConnection;
            command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 90;
            return command;
        }

        public SqlCommand GetSprocCommand(string sprocName, SqlConnection connection)
        {
            var command = new SqlCommand(sprocName);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 90;
            return command;
        }
      
        public SqlParameter CreateNullParameter(string name, SqlDbType paramType)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Value = null;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        public SqlParameter CreateOutputParameter(string name, SqlDbType paramType)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public SqlParameter CreateParameter(string name, Guid value)
        {
            if (value.Equals(Guid.Empty))
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.UniqueIdentifier);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.UniqueIdentifier;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, int value)
        {
            if (value == int.MinValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Int);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Int;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(
            string name,
            Byte[] value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.VarBinary;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        public SqlParameter CreateParameter(string name, XmlDocument value)
        {
            if (value == null)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Xml);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Xml;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateXmlParameter(string name, string value)
        {
            if (value == null)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Xml);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Xml;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, decimal value)
        {
            if (value == Decimal.MinValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Decimal);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Decimal;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.DateTime);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.DateTime;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, float value)
        {
            if (value == float.MinValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Float);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Float;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, bool value)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Bit;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        public SqlParameter CreateParameter(string name, string value, int size)
        {
            if (String.IsNullOrEmpty(value))
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.NVarChar);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.NVarChar;
                if (size > 0)
                {
                    parameter.Size = size;
                }

                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, object value, int size)
        {
            if (value == null)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.VarBinary);
            }
            else
            {
                var parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.VarBinary;
                if (size > 0)
                {
                    parameter.Size = size;
                }

                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public SqlParameter CreateParameter(string name, DataTable value, string typeName)
        {
            var parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            parameter.TypeName = typeName;
            return parameter;
        }     
    }
}
