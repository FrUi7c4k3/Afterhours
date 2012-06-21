using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiX.AM.DT.Services.Web.AssetApi.Models
{
	public class Organisation
	{
		public Guid OrganisationId { get; set; }
		public string Name { get; set; }
		public int OrganisationType { get; set; }
	}
}