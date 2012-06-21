using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiX.AM.DT.Services.Web.AssetApi.Models;
using PocoDb;

namespace MiX.AM.DT.Services.Web.AssetApi.Repositories
{
	public class OrganisationRepository
	{
		private ISqlDb sqlDb;

		public OrganisationRepository(ISqlDb sqlDb)
		{
			this.sqlDb = sqlDb;
		}

		/// <summary>
		/// Get all organisations
		/// </summary>
		/// <returns>All organisations</returns>
		public IEnumerable<Organisation> GetAll()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get organisation by Id
		/// </summary>
		/// <returns>An organisation</returns>
		public Organisation GetById(Guid parentOrgId)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get all child organisations that belong to a parent.
		/// </summary>
		/// <param name="parentOrgId">The Id of the parent organisation</param>
		/// <returns>All child organisations belonging to the parent</returns>
		public Organisation GetAllChildren(Guid parentOrgId)
		{
			throw new NotImplementedException();
		}
	}
}