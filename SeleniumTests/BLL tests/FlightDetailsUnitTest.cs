using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPortal.DataBase;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
	[TestClass]
	public class FlightDetailsUnitTest
	{
		FakeDbContaner fakeContainer;

		[TestInitialize]
		public void InitializeFakeDb()
		{
			fakeContainer = new FakeDbContaner();
		}

		[TestMethod]
		public void nonDepartedFlightsTest()
		{
			FlightDetails det = new FlightDetails();
			det.DepTime = DateTime.Now.AddHours(1);
			fakeContainer.FlightDetailsSet.Add(det);
			var nonDeparted = FlightDetails.getNotDepartedFlights(fakeContainer).ToArray();
			Assert.IsNotNull(nonDeparted);
			Assert.AreEqual(1, nonDeparted.Length);
		}
	}
}
