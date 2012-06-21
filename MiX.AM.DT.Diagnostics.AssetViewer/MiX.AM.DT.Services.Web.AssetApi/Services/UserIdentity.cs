using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Security;

namespace MiX.AM.DT.Services.Web.AssetApi.Services
{
	internal class UserIdentity : IUserIdentity
	{
		private readonly Guid _token;
		private IEnumerable<string> _claims = new List<string>();
		
		internal UserIdentity(string userName)
		{
			if (String.IsNullOrEmpty(userName))
				throw new ArgumentNullException("UserIdentity", "UserIdentity cannot be initialized with an empty or null UserName.");

			UserName = userName;
			_token = Guid.NewGuid();
		}

		public string UserName { get; set; }

		public Guid Token	
		{	
			get { return _token; }	
		} 
		
		public IEnumerable<string> Claims
		{
			get
			{
				foreach (var claim in _claims)
				{
					yield return claim;
				}
			}
			set
			{
				if (value == null)
					return;

				_claims = value;
			}
		}
	}
}
