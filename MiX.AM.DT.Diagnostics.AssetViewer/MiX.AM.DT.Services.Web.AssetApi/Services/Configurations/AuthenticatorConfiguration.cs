using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Cryptography;

namespace MiX.AM.DT.Services.Web.AssetApi.Services.Configurations
{
	public class AuthenticatorConfiguration
	{
		#region Fields

		private CryptographyConfiguration _cryptographyConfiguration;		

		#endregion Fields

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticatorConfiguration"/> class.
		/// </summary>
		public AuthenticatorConfiguration()
			: this(CryptographyConfiguration.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticatorConfiguration"/> class.
		/// </summary>
		/// <param name="cryptographyConfiguration">Cryptography configuration</param>
		public AuthenticatorConfiguration(CryptographyConfiguration cryptographyConfiguration)
		{
			_cryptographyConfiguration = cryptographyConfiguration;
		}

		#endregion Constructors

		#region Properties

		public CryptographyConfiguration CryptographyConfiguration
		{
			get { return _cryptographyConfiguration; }
		}

		#endregion Properties
	}
}