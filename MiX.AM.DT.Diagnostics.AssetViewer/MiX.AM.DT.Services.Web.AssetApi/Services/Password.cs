using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MiX.AM.DT.Services.Web.AssetApi.Services
{
	public class Password
	{
		internal static string GetHash(string password, string salt)
		{
			byte[] passwordBytes = Encoding.Unicode.GetBytes(password.Trim());
			byte[] saltBytes = Convert.FromBase64String(salt);
			
			byte[] result = new byte[saltBytes.Length + passwordBytes.Length];

			Buffer.BlockCopy(saltBytes, 0, result, 0, saltBytes.Length);
			Buffer.BlockCopy(passwordBytes, 0, result, saltBytes.Length, passwordBytes.Length);
		
			HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
			byte[] encodedPassword = algorithm.ComputeHash(result);
			
			return Convert.ToBase64String(encodedPassword);
		}
	}
}