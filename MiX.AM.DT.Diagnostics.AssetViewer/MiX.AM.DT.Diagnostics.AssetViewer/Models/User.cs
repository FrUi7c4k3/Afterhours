using MiX.AM.DT.Diagnostics.AssetViewer.Services;

namespace MiX.AM.DT.Diagnostics.AssetViewer.Models
{
	public class User
	{
		public string Username { get; set; }
		public string Token { get; set; }
		public bool IsLoggedIn { get; set; }
		internal AuthenticationResultEventArgs LastLoginResult { get; set; }

		public static User New(string username)
		{
			return new User() { Username = username , IsLoggedIn = false, LastLoginResult = new AuthenticationResultEventArgs() };
		}
	}
}
