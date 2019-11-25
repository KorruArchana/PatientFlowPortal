using System;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Database;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Common.Interfaces;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public IDbManager DbManager { get; set; }

		public DbAccess(DbManager dbManager)
		{
			DbManager = dbManager;
		}

		public IDbManager DbManager2 { get; set; }

		public DbAccess(string connectionstring)
		{
			DbManager2 = new DbManager(connectionstring);
		}

		private Int32 TryParseInt(SqlDataReader dr, string ColumnName)
		{
			return dr[ColumnName] == DBNull.Value ? 0 : Convert.ToInt32(dr[ColumnName]);
		}

		private string TryParseString(SqlDataReader dr, string ColumnName)
		{
			return dr[ColumnName] == DBNull.Value ? string.Empty : Convert.ToString(dr[ColumnName]);
		}

		private string TryParseStringAndDecrypt(SqlDataReader dr, string ColumnName)
		{
			return dr[ColumnName] == DBNull.Value ? string.Empty : Convert.ToString(dr[ColumnName]).DecryptAES256();
		}

		private string TryParseGuidString(DataRow dr, string ColumnName)
		{
			return dr[ColumnName] == DBNull.Value ? string.Empty : new Guid(dr[ColumnName].ToString()).ToString();
		}

		private string TryParseGuidString(SqlDataReader dr, string ColumnName)
		{
			return dr[ColumnName] == DBNull.Value ? string.Empty : new Guid(dr[ColumnName].ToString()).ToString();
		}

		private Boolean TryParseBoolean(SqlDataReader dr, string ColumnName)
		{
			return dr[ColumnName] != DBNull.Value && Convert.ToBoolean(dr[ColumnName]);
		}

		private int GetIntValue(DataRow dr, string columnName)
		{
			return (dr[columnName] == DBNull.Value) ? 0 : Convert.ToInt32(dr[columnName]);
		}

		private string GetStringValue(DataRow dr, string columnName)
		{
			return (dr[columnName] == DBNull.Value) ? string.Empty : Convert.ToString(dr[columnName]);
		}

		private long GetLongValue(DataRow dr, string columnName)
		{
			return (dr[columnName] == DBNull.Value) ? 0 : Convert.ToInt64(dr[columnName]);
		}

		private Boolean GetBooleanValue(DataRow dr, string ColumnName)
		{
			return dr[ColumnName] != DBNull.Value && Convert.ToBoolean(dr[ColumnName]);
		}
	}
}
