using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiX.AM.DT.Services.Web.AssetApi.Factories;
using MiX.AM.DT.Services.Web.AssetApi.Models;
using MiX.AM.DT.Services.Web.AssetApi.Repositories;
using MiX.AM.DT.Services.Web.AssetApi.Services.Configurations;
using Nancy;
using Nancy.Cookies;

namespace MiX.AM.DT.Services.Web.AssetApi.Services
{
	internal class Authenticator
	{
		private static UserRepository _userRepository = new UserRepository();
		private static AuthenticatorConfiguration _authenticatorConfiguration;
		private static readonly Dictionary<Guid, UserIdentity> _usersLoggedIn = new Dictionary<Guid, UserIdentity>();

		static Authenticator()
		{
			_authenticatorConfiguration = new AuthenticatorConfiguration();
		}

		/// <summary>
		/// Authenticates a user and creates a UserIdentity upon successful authentication. 
		/// </summary>
		/// <param name="username">Username</param>
		/// <param name="password">Password</param>
		/// <param name="identity">The identity that contains all info about a logged in user - token, permissions, etc</param>
		/// <returns>True if user authenticated.</returns>
		internal static bool Authenticate(string username, string password, out UserIdentity identity)
		{
			identity = null;

			if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
				return false;

			User user = _userRepository.GetUser(username);

			if (user == null || String.IsNullOrEmpty(user.PasswordHash) || String.IsNullOrEmpty(user.PasswordSalt))
				return false;

			string passwordHash = Password.GetHash(password, user.PasswordSalt);
			if (string.Compare(passwordHash, user.PasswordHash, StringComparison.Ordinal) == 0)
			{
				identity = new UserIdentity(username);
				//_usersLoggedIn.Add(identity.Token, identity);

				return true;
			}

			return false;
		}

		internal static Response AuthenticatedResponse(Guid token, DateTime cookieExpiryDate)
		{
			Response response = HttpStatusCode.OK;
			response.Headers.Add("Authorization", "Success!");

			INancyCookie authenticationCookie = Cookie.CreateEncryptedAndSignedAuthenticationCookie(token, cookieExpiryDate, _authenticatorConfiguration);
			response.AddCookie(authenticationCookie);

			return response;
		}
	}
}