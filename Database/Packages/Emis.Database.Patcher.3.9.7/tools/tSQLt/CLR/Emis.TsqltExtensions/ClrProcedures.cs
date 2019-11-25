using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class ClrProcedures
{
	[Microsoft.SqlServer.Server.SqlProcedure(Name = "tSQLt.ResultSetFilterDirectToTable ")]
	public static void ResultSetFilterDirectToTable(
		int		resultsetNo,
		string	command,
		string	targetTable)
	{
		using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
			connection.Open();

			DataTable resultSet = new DataTable();

			using (SqlCommand cmdExecuteResultSetFilter = connection.CreateCommand())
			{
				cmdExecuteResultSetFilter.CommandType = CommandType.StoredProcedure;
				cmdExecuteResultSetFilter.CommandText = "tSQLt.ResultSetFilter";
				cmdExecuteResultSetFilter.Parameters.Add("@ResultsetNo", SqlDbType.Int).Value = resultsetNo;
				cmdExecuteResultSetFilter.Parameters.Add("@Command", SqlDbType.NVarChar, -1).Value = command;

				// Note: the reader must be closed before other statements can be executed on the connection
				using (SqlDataReader reader = cmdExecuteResultSetFilter.ExecuteReader())
				{
					resultSet.Load(reader);
				}
			}

			foreach (DataRow row in resultSet.Rows)
			{
				using (SqlCommand cmdInsert = connection.CreateCommand())
				{
					cmdInsert.CommandType = CommandType.Text;
					cmdInsert.CommandText = BuildResultSetRowInsertCommandText(targetTable, row);

					try
					{
						cmdInsert.ExecuteNonQuery();
					}
					catch (SqlException e)
					{
						throw new InvalidOperationException(String.Format("Failed to insert result set row in target table; error = {0}; insert command = {1}", 
																			e.Message,
																			cmdInsert.CommandText), 
															e);
					}
				}
			}
        }
	}

	private static string BuildResultSetRowInsertCommandText(
		string targetTable,
		DataRow row)
	{
		StringBuilder insertCommand = new StringBuilder(string.Format("insert into {0} select ",targetTable));
		foreach (object item in row.ItemArray)
		{
			if (DBNull.Value == item)
				insertCommand.Append("null ,");
			else
				switch (item.GetType().Name)
				{
					case "Int32":
					case "Int16":
					case "Byte":
					case "Double":
						insertCommand.AppendFormat(
							"{0} ,",
							item.ToString());
						break;

					case "DateTime":
						DateTime itemValue = (DateTime)item;
						insertCommand.AppendFormat(
							"'{0}' ,",
							itemValue.ToString(itemValue.TimeOfDay == TimeSpan.Zero
												? "yyyyMMdd"
												: (itemValue.Ticks % 10000 == 0
													? "yyyyMMdd HH:mm:ss.fff"
													: "yyyyMMdd HH:mm:ss.fffffff")));
						break;

					default:
						insertCommand.AppendFormat(
							"'{0}' ,",
							item.ToString().Replace("'", "''"));
						break;
				}
		}

		insertCommand.Remove(insertCommand.Length - 1, 1); // remove trailing comma

		return insertCommand.ToString();
	}
}
