using System;
using System.Net;
using Caliburn.Micro;
using MiX.AM.DT.Diagnostics.AssetViewer.Models;

namespace MiX.AM.DT.Diagnostics.AssetViewer.Services
{
	internal class AuthenticationResultEventArgs : EventArgs
	{
		public bool LoginSuccessful { get; set; }
		public string Result { get; set; }
		public Exception Exception { get; set; }
	}

	internal class AuthenticationService
	{
		private static readonly AuthenticationService _instance = new AuthenticationService();
		
		internal event EventHandler<AuthenticationResultEventArgs> AuthenticateCompleted;

		#region Constructors

		private AuthenticationService()
		{ }

		#endregion //Constructors

		#region Properties

		internal static AuthenticationService Instance
		{
			get { return _instance; }
		}

		internal User CurrentUser { get; private set; }

		#endregion Properties

		internal void Authenticate(string username, string password)
		{
			CurrentUser = User.New(username);

			//HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost/MiX.AM.DT.Services.Web.AssetApi/login");
			//request.Method = "POST";
			//request.Headers[HttpRequestHeader.Authorization] = String.Format("username={0}&password={1}", UserName, Password);
			//request.BeginGetResponse(ProcessLoginResponse, request);
			OnAuthenticateCompleted(new AuthenticationResultEventArgs() { LoginSuccessful = true, Result = "Success!" });
		}

		#region Private Methods

		private void ProcessLoginResponse(IAsyncResult ctx)
		{
			Execute.OnUIThread(() =>
			{
				AuthenticationResultEventArgs args = new AuthenticationResultEventArgs();
				try
				{
					var rqst = (HttpWebRequest)ctx.AsyncState;
					var response = (HttpWebResponse)rqst.EndGetResponse(ctx);
					if (response.StatusCode == HttpStatusCode.OK && response.Headers[HttpRequestHeader.Authorization] == "Success!")
					{
						args.LoginSuccessful = true;
						args.Result = "Success!";
					}
					else
					{
						args.LoginSuccessful = false;
						args.Result = response.Headers[HttpRequestHeader.Authorization];
					}
				}
				catch (Exception ex)
				{
					args.LoginSuccessful = true;
					args.Result = "Error!";
					args.Exception = ex;
				}
				finally
				{
					CurrentUser.LastLoginResult = args;
					CurrentUser.IsLoggedIn = args.LoginSuccessful;
					//CurrentUser.Token
					OnAuthenticateCompleted(args);
				}
			});
		}

		private void OnAuthenticateCompleted(AuthenticationResultEventArgs args)
		{
			var handler = AuthenticateCompleted;
			if (handler != null)
			{
				handler(this, args);
			}
		}

		#endregion Private Methods
	}
}
