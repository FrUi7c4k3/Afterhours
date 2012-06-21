using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiX.AM.DT.Services.Web.AssetApi.Repositories;
using MiX.AM.DT.Services.Web.AssetApi.Models;
using Telerik.JustMock;
using PocoDb;

namespace MiX.AM.DT.Services.Web.AssetApi.Tests
{
	/// <summary>
	/// Summary description for OrganisationRepositoryTests
	/// </summary>
	[TestClass]
	public class OrganisationRepositoryTests
	{
		public OrganisationRepositoryTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void Get_All_Should_Return_All_Organisations()
		{
			ISqlDb sqlDb = Mock.Create<ISqlDb>();
			
			OrganisationRepository repo = new OrganisationRepository(sqlDb);
			IEnumerable<Organisation> organisations = repo.GetAll();
		}
	}
}
