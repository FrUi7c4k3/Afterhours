using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MiX.AM.DT.Services.Web.AssetApi.Models;
using MiX.AM.DT.Services.Web.AssetApi.Factories;
using PocoDb;

namespace MiX.AM.DT.Services.Web.AssetApi.Repositories
{
	internal class UserRepository
	{
		internal User GetUser(string username)
		{
			SqlDb sqlDb = new SqlDb();
			User user = sqlDb.ExecuteReaderSproc<User>("Users_GetByUserName", ParameterFactory.Create(@"UserName", username)).SingleOrDefault(); ;

			return user;
		}
	}
}