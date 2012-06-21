using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using MiX.AM.DT.Services.Web.AssetApi.Repositories;
using MiX.AM.DT.Services.Web.AssetApi.Services;
using Nancy;

namespace MiX.AM.DT.Services.Web.AssetApi.Modules
{
	public class AuthenticationModule : NancyModule
	{
		public AuthenticationModule()
		{
			Post["/login"] = ctx =>
			{
				Dictionary<string, string> authHeaders = ExtractAuthHeaders(this.Request.Headers.Authorization);

				if (authHeaders.Count > 0 &&
						!String.IsNullOrEmpty(authHeaders["username"]) &&
						!String.IsNullOrEmpty(authHeaders["password"]))
				{
					UserIdentity userIdentity = null;
					if (Authenticator.Authenticate(authHeaders["username"], authHeaders["password"], out userIdentity))
					{
						return Authenticator.AuthenticatedResponse(userIdentity.Token, DateTime.UtcNow.AddDays(7));
					}
				}

				return new Response()	{	StatusCode = HttpStatusCode.OK };
			};
		}

		#region Helper Methods

		private static Dictionary<string, string> ExtractAuthHeaders(string auth)
		{
			if (String.IsNullOrEmpty(auth))
				return new Dictionary<string,string>();

			Dictionary<string, string> authHeaders = new Dictionary<string, string>();

			IEnumerable<string> authFields = auth.Split('&');
			if (authFields.Count() > 0)
			{
				foreach (string field in authFields)
				{
					int index = field.IndexOf('=');
					if (index != -1)
					{
						string key = field.Substring(0, index);
						string value = field.Substring(index + 1, field.Length - (index + 1));
						authHeaders.Add(key, value);
					}
				}
			}

			return authHeaders;
		}

		#endregion Helper Methods
	}
}