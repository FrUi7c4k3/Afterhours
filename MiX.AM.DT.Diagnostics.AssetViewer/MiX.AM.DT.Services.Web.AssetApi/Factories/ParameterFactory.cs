using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiX.AM.DT.Services.Web.AssetApi.Factories
{
	public class ParameterFactory
	{
		internal static SqlParameter Create(string name, object value)
		{
			return new SqlParameter(name, value);
		}

		internal static SqlParameter Create(string sproc, object value, SqlDbType dbType)
		{
			var par = new SqlParameter(sproc, dbType);
			par.Value = value;
			return par;
		}
	}
}