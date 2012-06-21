using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiX.AM.DT.Services.Web.AssetApi.Repositories;
using Nancy;

namespace MiX.AM.DT.Services.Web.AssetApi.Modules
{
	public class OrganisationModule : NancyModule
	{
		public OrganisationModule()
		{
			Get["/"] = parameters => "This is a test!";

			Get["/organisation/{id}"] = parameters => OrganisationRepository.GetById(parameters.id);
		}
	}
}