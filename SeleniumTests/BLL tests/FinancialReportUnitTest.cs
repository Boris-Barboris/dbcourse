using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPortal.DataBase;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
	[TestClass]
	public class FinancialReportUnitTest
	{
		FakeDbContaner fakeContainer;

		[TestInitialize]
		public void InitializeFakeDb()
		{
			fakeContainer = new FakeDbContaner();
		}

		FlightDetails createTestFlight(int id)
		{
			FlightDetails flight;
			flight = new FlightDetails();
			flight.AircraftType = "testAircraftType";
			flight.ArrTime = DateTime.Now.AddHours(2.0);
			flight.DepTime = DateTime.Now.AddHours(1.0);
			flight.BnFree = 30;
			flight.Destination = "testDestination";
			flight.EcoFree = 100;
			flight.FareBn = 5000m;
			flight.FareEco = 1000m;
			flight.FlightNo = "testFlightNo" + id.ToString();
			flight.Origin = "testOrigin";
			flight.SeatsBn = 50;
			flight.SeatsEco = 150;
			flight.FareCollected = 20 * 5000 + 50 * 1000;
			return flight;
		}

		Reservation createTestReservation(int id, int flightid)
		{
			Reservation res = new Reservation();
			res.FlightNo = "testFlightNo" + flightid.ToString();
			res.Status = ReservationStatus.Cancelled;
			res.TicketNo = id;
			return res;
		}

		Cancellation createTestCancellation(int id)
		{
			Cancellation can = new Cancellation();
			can.TicketNo = id;
			can.Refund = 100m;
			return can;
		}

		[TestMethod]
		public void finRepBasicTest()
		{
			FlightDetails fd = createTestFlight(0);
			fakeContainer.FlightDetailsSet.Add(fd);
			for (int i = 0; i < 3; i++)
			{
				fakeContainer.ReservationSet.Add(createTestReservation(i, 0));
				fakeContainer.CancellationSet.Add(createTestCancellation(i));
			}

			// test itself
			TimeIntervalReport report =
				new TimeIntervalReport(fakeContainer, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
			fakeContainer.clear();
			Assert.AreEqual(fd.FareCollected, report.TotalFare);
			Assert.AreEqual(300m, report.TotalRefunds);
			Assert.AreEqual(1, report.TotalFlights);
			Assert.AreEqual(70, report.TotalPassengersTravelled);
		}

		[TestMethod]
		public void inversedDataIntervalTest()
		{
			FlightDetails fd = createTestFlight(0);
			fakeContainer.FlightDetailsSet.Add(fd);
			for (int i = 0; i < 3; i++)
			{
				fakeContainer.ReservationSet.Add(createTestReservation(i, 0));
				fakeContainer.CancellationSet.Add(createTestCancellation(i));
			}

			// test itself
			TimeIntervalReport report =
				new TimeIntervalReport(fakeContainer, DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1));
			fakeContainer.clear();
			Assert.AreEqual(0, report.TotalFare);
			Assert.AreEqual(0, report.TotalRefunds);
			Assert.AreEqual(0, report.TotalFlights);
			Assert.AreEqual(0, report.TotalPassengersTravelled);
		}

		[TestMethod]
		public void multipleFlightsTest()
		{
			FlightDetails fd = createTestFlight(0);
			fakeContainer.FlightDetailsSet.Add(fd);
			fakeContainer.FlightDetailsSet.Add(createTestFlight(1));
			for (int i = 0; i < 5; i++)
			{
				fakeContainer.ReservationSet.Add(createTestReservation(i, 0));
				fakeContainer.CancellationSet.Add(createTestCancellation(i));
			}
			for (int i = 5; i < 10; i++)
			{
				fakeContainer.ReservationSet.Add(createTestReservation(i, 1));
				fakeContainer.CancellationSet.Add(createTestCancellation(i));
			}

			// test itself
			TimeIntervalReport report =
				new TimeIntervalReport(fakeContainer, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
			fakeContainer.clear();
			Assert.AreEqual(2 * fd.FareCollected, report.TotalFare);
			Assert.AreEqual(1000m, report.TotalRefunds);
			Assert.AreEqual(2, report.TotalFlights);
			Assert.AreEqual(140, report.TotalPassengersTravelled);
		}

		[TestMethod]
		public void noRefundsTest()
		{
			FlightDetails fd = createTestFlight(0);
			fakeContainer.FlightDetailsSet.Add(fd);
			fakeContainer.FlightDetailsSet.Add(createTestFlight(1));

			// test itself
			TimeIntervalReport report =
				new TimeIntervalReport(fakeContainer, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
			fakeContainer.clear();
			Assert.AreEqual(2 * fd.FareCollected, report.TotalFare);
			Assert.AreEqual(0, report.TotalRefunds);
			Assert.AreEqual(2, report.TotalFlights);
			Assert.AreEqual(140, report.TotalPassengersTravelled);
		}
	}
}
