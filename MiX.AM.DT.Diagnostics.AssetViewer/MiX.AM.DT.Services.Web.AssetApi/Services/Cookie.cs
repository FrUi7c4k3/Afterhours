using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiX.AM.DT.Services.Web.AssetApi.Services.Configurations;
using Nancy.Cookies;
using Nancy.Cryptography;

namespace MiX.AM.DT.Services.Web.AssetApi.Factories
{
	public class Cookie
	{
		/// <summary>
		/// Build the forms authentication cookie
		/// </summary>
		/// <param name="userIdentifier">Authenticated user identifier</param>
		/// <param name="cookieExpiryDate">Optional expiry date for the cookie (for 'Remember me')</param>
		/// <param name="authenticatorConfiguration">Current configuration</param>
		/// <returns>Nancy cookie instance</returns>
		internal static INancyCookie CreateEncryptedAndSignedAuthenticationCookie(Guid userIdentifier, DateTime? cookieExpiryDate, AuthenticatorConfiguration authenticatorConfiguration)
		{
			string cookieContents = EncryptAndSignCookie(userIdentifier.ToString(), authenticatorConfiguration);

			return new NancyCookie("mix.auth", cookieContents, true) { Expires = cookieExpiryDate };
		}

		/// <summary>
		/// Encrypt and sign the cookie contents
		/// </summary>
		/// <param name="cookieValue">Plain text cookie value</param>
		/// <param name="configuration">Current configuration</param>
		/// <returns>Encrypted and signed string</returns>
		private static string EncryptAndSignCookie(string cookieValue, AuthenticatorConfiguration authenticatorConfiguration)
		{
			string encryptedCookie = authenticatorConfiguration.CryptographyConfiguration.EncryptionProvider.Encrypt(cookieValue);
			
			byte[] hmacBytes = authenticatorConfiguration.CryptographyConfiguration.HmacProvider.GenerateHmac(encryptedCookie);
			string hmacstring = Convert.ToBase64String(hmacBytes);

			return String.Format("{1}{0}", encryptedCookie, hmacstring);
		}

		/// <summary>
		/// Decrypt and validate an encrypted and signed cookie value
		/// </summary>
		/// <param name="cookieValue">Encrypted and signed cookie value</param>
		/// <param name="configuration">Current configuration</param>
		/// <returns>Decrypted value, or empty on error or if failed validation</returns>
		internal static string DecryptAndValidateAuthenticationCookie(string cookieValue, AuthenticatorConfiguration configuration)
		{
			// TODO - shouldn't this be automatically decoded by nancy cookie when that change is made?
			var decodedCookie = HttpUtility.UrlDecode(cookieValue);

			var hmacStringLength = Base64Helpers.GetBase64Length(configuration.CryptographyConfiguration.HmacProvider.HmacLength);

			var encryptedCookie = decodedCookie.Substring(hmacStringLength);
			var hmacString = decodedCookie.Substring(0, hmacStringLength);

			var encryptionProvider = configuration.CryptographyConfiguration.EncryptionProvider;

			// Check the hmacs, but don't early exit if they don't match
			var hmacBytes = Convert.FromBase64String(hmacString);
			var newHmac = configuration.CryptographyConfiguration.HmacProvider.GenerateHmac(encryptedCookie);
			var hmacValid = HmacComparer.Compare(newHmac, hmacBytes, configuration.CryptographyConfiguration.HmacProvider.HmacLength);

			var decrypted = encryptionProvider.Decrypt(encryptedCookie);

			// Only return the decrypted result if the hmac was ok
			return hmacValid ? decrypted : String.Empty;
		}
	}
}