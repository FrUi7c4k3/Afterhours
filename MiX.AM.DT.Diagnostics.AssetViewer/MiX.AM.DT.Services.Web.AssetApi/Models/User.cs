using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiX.AM.DT.Services.Web.AssetApi.Models
{
	public class User
	{
		public Guid UserId { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public string PasswordSalt { get; set; }
	}
}