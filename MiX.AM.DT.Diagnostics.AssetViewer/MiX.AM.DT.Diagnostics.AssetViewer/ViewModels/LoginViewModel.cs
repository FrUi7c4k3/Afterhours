using System;
using System.Net;
using Caliburn.Micro;
using MiX.AM.DT.Diagnostics.AssetViewer.Models;
using MiX.AM.DT.Diagnostics.AssetViewer.Services;
using Action = System.Action;

namespace MiX.AM.DT.Diagnostics.AssetViewer.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName;
		private string _password;
		private string _error;
		private readonly Action _loginSuccess;

		#region Constructors

		public LoginViewModel(Action processLoginSuccess)
		{
			_loginSuccess = processLoginSuccess;
		}

		#endregion //Constructors

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
			AuthenticationService.Instance.AuthenticateCompleted += OnLoginSuccess;
			AuthenticationService.Instance.Authenticate(UserName, Password);
		}

		private void OnLoginSuccess(object sender, AuthenticationResultEventArgs args)
		{
			if (!args.LoginSuccessful)
			{
				Error = "Invalid username or password..";
				return;
			}

			Action handler = _loginSuccess;
			if (handler != null)
			{
				handler();
			}
		}

		#endregion //Methods
	}
}
