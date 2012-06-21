using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocoDb
{
	public class SqlDb : ISqlDb
	{
		#region Fields

		private string _connectionString;

		#endregion //Fields

		#region Constructors

		public SqlDb()
			: this("AssetManagement")
		{ }

		public SqlDb(string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

			if (string.IsNullOrEmpty(connectionString))
				throw new Exception(String.Format("ConnectionString {0} not found. Please check the relevent config setting.", connectionStringName));

			_connectionString = connectionString;
		}

		#endregion //Constructors

		#region Public Methods

		public IEnumerable<T> ExecuteReaderSproc<T>(string sproc, params IDbDataParameter[] parameters)
			where T : new()
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				
				var command = connection.CreateCommand();
				command.CommandText = sproc;
				command.CommandType = CommandType.StoredProcedure;
				
				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i]);
				}

				var reader = command.ExecuteReader();
				
				DataTable table = new DataTable();
				table.Load(reader);
				reader.Close();

				return ObjectMapper.Map<T>(table);
			}
		}

		#endregion //Public Methods
	}
}
