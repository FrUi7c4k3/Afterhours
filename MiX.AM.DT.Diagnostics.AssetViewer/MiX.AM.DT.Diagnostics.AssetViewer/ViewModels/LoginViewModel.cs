using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows.Threading;
using Caliburn.Micro;

namespace MiX.AM.DT.Diagnostics.AssetViewer.ViewModels
{
	[Export(typeof(ILoginViewModel))]
	public class LoginViewModel : Screen, ILoginViewModel
	{
		private readonly IWindowManager _windowManager;
		private readonly IDashboardViewModel _dashboardViewModel;
		DispatcherSynchronizationContext syncCtx;
		private string _userName;
		private string _password;
		private string _error;

		[ImportingConstructor]
		public LoginViewModel(IWindowManager windowManager, IDashboardViewModel dashboardViewModel)
		{
			this._windowManager = windowManager;
			this._dashboardViewModel = dashboardViewModel;
			syncCtx = new DispatcherSynchronizationContext();
		}

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

		public void Login()
		{
			//HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost/MiX.AM.DT.Services.Web.AssetApi/login");
			//request.Method = "POST";
			//request.Headers[HttpRequestHeader.Authorization] = String.Format("username={0}&password={1}", UserName, Password);
			//request.BeginGetResponse(ctx => ProcessLoginResponse(ctx), request);
			this._windowManager.ShowDialog(_dashboardViewModel);
			this.TryClose();
		}

		private void ProcessLoginResponse(IAsyncResult ctx)
		{
			syncCtx.Send(obj =>
			{
				var rqst = (HttpWebRequest)ctx.AsyncState;
				var response = (HttpWebResponse)rqst.EndGetResponse(ctx);
				if (response.StatusCode == HttpStatusCode.OK && response.Headers[HttpRequestHeader.Authorization] == "Success!")
				{
					Error = String.Empty;
					this._windowManager.ShowDialog(_dashboardViewModel);
					this.TryClose();
					return;
				}
				Error = "Invalid username or password";
			}, null);
		}
	}
}
