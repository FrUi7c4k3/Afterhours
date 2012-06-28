using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows.Threading;
using Caliburn.Micro;

namespace MiX.AM.DT.Diagnostics.AssetViewer.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName;
		private string _password;
		private string _error;

		private System.Action _onLoginSuccess;

		public LoginViewModel(System.Action onLoginSuccess)
		{
			if (onLoginSuccess == null)
				throw new ArgumentNullException("Cannon initialize Login screen with onLoginSuccess set to null.");

			_onLoginSuccess = onLoginSuccess;
		}
		
		#region Properties

		public string UserName
		{
			get	{	return _userName;	}
			set
			{
				if (_userName == value)
					return;

				_userName = value;
				NotifyOfPropertyChange("UserName");
			}
		}

		public string Password
		{
			get	{	return _password;	}
			set
			{
				if (_password == value)
					return;

				_password = value;
				NotifyOfPropertyChange("Password");
			}
		}

		public string Error
		{
			get { return _error; }
			set
			{
				if (_error == value)
					return;

				_error = value;
				NotifyOfPropertyChange("Error");
			}
		}

		#endregion Properties

		#region Methods

		public void Login()
		{
			//HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost/MiX.AM.DT.Services.Web.AssetApi/login");
			//request.Method = "POST";
			//request.Headers[HttpRequestHeader.Authorization] = String.Format("username={0}&password={1}", UserName, Password);
			//request.BeginGetResponse(ctx => ProcessLoginResponse(ctx), request);
			_onLoginSuccess();
		}

		private void ProcessLoginResponse(IAsyncResult ctx)
		{
			Execute.OnUIThread(() =>
			{
				var rqst = (HttpWebRequest) ctx.AsyncState;
				var response = (HttpWebResponse) rqst.EndGetResponse(ctx);
				if (response.StatusCode == HttpStatusCode.OK &&
				    response.Headers[HttpRequestHeader.Authorization] == "Success!")
				{
					Error = String.Empty;
					this._onLoginSuccess();
				}
				Error = "Invalid username or password";
			});
		}

		#endregion //Methods
	}
}
