using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPortal.DataBase;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
	[TestClass]
	public class PassengerUnitTests
	{
		FakeDbContaner fakeContainer;

		[TestInitialize]
		public void InitializeFakeDb()
		{
			fakeContainer = new FakeDbContaner();
		}

		[TestMethod]
		public void passengerInit()
		{
			Passenger pass = new Passenger();
			pass.initialize(fakeContainer, "testid", "TestName");
			Passenger[] added = (from p in fakeContainer.PassengerSet
							  where (p.ID == "testid") && (p.Name == "TestName")
							  select p).ToArray();
			Assert.IsNotNull(added);
			Assert.AreEqual(added.Length, 1);
		}

		[TestMethod]
		public void constructorNullTest()
		{
			Passenger pass = new Passenger();
			try
			{
				pass.initialize(null, "123", "321");
				Assert.Fail();
			}
			catch (ArgumentNullException) { }
			try
			{
				pass.initialize(fakeContainer, null, "321");
				Assert.Fail();
			}
			catch (ArgumentNullException) { }
			try
			{
				pass.initialize(fakeContainer, "123", null);
				Assert.Fail();
			}
			catch (ArgumentNullException) { }
		}
	}
}
