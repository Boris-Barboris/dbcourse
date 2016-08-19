using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPortal.DataBase;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests
{
	[TestClass]
	public class ReservationUnitTest
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
			flight.BnFree = 30;
			flight.EcoFree = 100;
			flight.FareBn = 5000m;
			flight.FareEco = 1000m;
			flight.FlightNo = "testFlightNo" + id.ToString();
			flight.SeatsBn = 50;
			flight.SeatsEco = 150;
			flight.FareCollected = 20 * 5000 + 50 * 1000;
			return flight;
		}

		Reservation createTestReservation(int id, int flightid)
		{
			Reservation res = new Reservation();
			res.FlightNo = "testFlightNo" + flightid.ToString();
			res.Status = ReservationStatus.Unprocessed;
			res.TicketNo = id;
			return res;
		}

		[TestMethod]
		public void applyReservationBasic()
		{
			FlightDetails flight = createTestFlight(0);
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			resE.EMail = "testEmail";
			Reservation resB = createTestReservation(1, 0);
			resB.ClassOfRes = FlightClass.Business;
			resB.EMail = "testEmail";
			Passenger pas = new Passenger();
			pas.ID = "testPassId";
			pas.Discount = 0.0f;
			User user = new User();
			user.EMail = "testEmail";

			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);
			fakeContainer.UserSet.Add(user);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resB.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			
			Assert.AreEqual(2, fakeContainer.ReservationSet.ToArray().Length);
			fakeContainer.clear();
			Assert.AreEqual(flight, resE.Flight);
			Assert.AreEqual(flight, resB.Flight);
			Assert.AreEqual(pas, resE.Passenger);
			Assert.AreEqual(pas, resB.Passenger);
			Assert.AreEqual(99, flight.EcoFree);
			Assert.AreEqual(29, flight.BnFree);
			Assert.AreEqual(1000, resE.Fare);
			Assert.AreEqual(5000, resB.Fare);
			Assert.AreEqual(ReservationStatus.Unprocessed, resE.Status);
			Assert.AreEqual(ReservationStatus.Unprocessed, resB.Status);
			Assert.AreEqual(2, user.Reservation.ToArray().Length);
			Assert.IsTrue(user.Reservation.Contains(resE));
			Assert.IsTrue(user.Reservation.Contains(resB));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void noSeatsReservation()
		{
			FlightDetails flight = createTestFlight(0);
			flight.EcoFree = 0;
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			resE.EMail = "testEmail";
			Passenger pas = new Passenger();
			pas.ID = "testPassId";

			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FlightNotFoundException))]
		public void noFlightTest()
		{
			Reservation res = createTestReservation(0, 0);
			res.ClassOfRes = FlightClass.Eco;
			res.EMail = null;			

			res.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			fakeContainer.clear();
			Assert.Fail();			
		}

		[TestMethod]
		[ExpectedException(typeof(PassengerNotFoundException))]
		public void noPassengerTest()
		{
			FlightDetails flight = createTestFlight(0);
			Reservation res = createTestReservation(0, 0);
			res.ClassOfRes = FlightClass.Eco;
			res.EMail = null;
			fakeContainer.FlightDetailsSet.Add(flight);

			res.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			fakeContainer.clear();
			Assert.Fail();
		}

		[TestMethod]
		public void confirmBasic()
		{
			FlightDetails flight = createTestFlight(0);
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			Reservation resB = createTestReservation(1, 0);
			resB.ClassOfRes = FlightClass.Business;
			Passenger pas = new Passenger();
			pas.ID = "testPassId";
			pas.Discount = 0.0f;
			
			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resE.confirm(fakeContainer);
			resB.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resB.confirm(fakeContainer);
			fakeContainer.clear();

			Assert.AreEqual(ReservationStatus.Confirmed, resE.Status);
			Assert.AreEqual(ReservationStatus.Confirmed, resB.Status);
			Assert.AreEqual(1000, resE.Fare);
			Assert.AreEqual(5000, resB.Fare);
			Assert.AreEqual(6000, pas.FareCollected);
			Assert.AreEqual(2, pas.TotalTimesFlown);
			Assert.AreEqual(21 * 5000 + 51 * 1000, flight.FareCollected);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void confirmCancelled()
		{
			Reservation res = createTestReservation(0, 0);
			res.Status = ReservationStatus.Cancelled;

			res.confirm(fakeContainer);
			Assert.Fail();
		}

		[TestMethod]
		public void cancelBasic()
		{
			FlightDetails flight = createTestFlight(0);
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			resE.EMail = "testEmail";
			Reservation resB = createTestReservation(1, 0);
			resB.ClassOfRes = FlightClass.Business;
			resB.EMail = "testEmail";
			Passenger pas = new Passenger();
			pas.ID = "testPassId";
			pas.Discount = 0.0f;
			User user = new User();
			user.EMail = "testEmail";

			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);
			fakeContainer.UserSet.Add(user);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resE.cancel(fakeContainer);
			resB.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resB.cancel(fakeContainer);
			fakeContainer.clear();

			Assert.AreEqual(ReservationStatus.Cancelled, resE.Status);
			Assert.AreEqual(ReservationStatus.Cancelled, resB.Status);
			Assert.AreEqual(0, fakeContainer.ReservationSet.ToArray().Length);
			Assert.AreEqual(0, user.Reservation.ToArray().Length);
			Assert.AreEqual(100, flight.EcoFree);
			Assert.AreEqual(30, flight.BnFree);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void cancelConfirmed()
		{
			Reservation res = createTestReservation(0, 0);
			res.Status = ReservationStatus.Confirmed;

			res.cancel(fakeContainer);
			Assert.Fail();
		}

		[TestMethod]
		public void refundFull()
		{
			FlightDetails flight = createTestFlight(0);
			flight.DepTime = DateTime.Now.AddDays(1.1);
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			Passenger pas = new Passenger();
			pas.ID = "testPassId";
			pas.Discount = 0.0f;

			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resE.confirm(fakeContainer);
			resE.refund(fakeContainer, "testId", DateTime.Now);
			Cancellation canc = fakeContainer.CancellationSet.First();

			Assert.IsNotNull(canc);
			Assert.AreEqual(ReservationStatus.Cancelled, resE.Status);
			Assert.AreEqual(20 * 5000 + 50 * 1000, flight.FareCollected);
			Assert.AreEqual(0, resE.Fare);
			Assert.AreEqual(0, pas.TotalTimesFlown);
			Assert.AreEqual(0, pas.FareCollected);
			Assert.AreEqual(100, flight.EcoFree);
		}

		[TestMethod]
		public void refundQuarter()
		{
			FlightDetails flight = createTestFlight(0);
			flight.DepTime = DateTime.Now.AddDays(0.5);
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			Passenger pas = new Passenger();
			pas.ID = "testPassId";
			pas.Discount = 0.0f;

			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resE.confirm(fakeContainer);
			resE.refund(fakeContainer, "testId", DateTime.Now);
			Cancellation canc = fakeContainer.CancellationSet.First();

			Assert.IsNotNull(canc);
			Assert.AreEqual(ReservationStatus.Cancelled, resE.Status);
			Assert.AreEqual(20 * 5000 + 50 * 1000 + 750, flight.FareCollected);
			Assert.AreEqual(750, resE.Fare);
			Assert.AreEqual(0, pas.TotalTimesFlown);
			Assert.AreEqual(0, pas.FareCollected);
			Assert.AreEqual(100, flight.EcoFree);
		}

		[TestMethod]
		public void refundLate()
		{
			FlightDetails flight = createTestFlight(0);
			flight.DepTime = DateTime.Now.AddDays(0.05);
			Reservation resE = createTestReservation(0, 0);
			resE.ClassOfRes = FlightClass.Eco;
			Passenger pas = new Passenger();
			pas.ID = "testPassId";
			pas.Discount = 0.0f;

			fakeContainer.FlightDetailsSet.Add(flight);
			fakeContainer.PassengerSet.Add(pas);

			resE.apply_reservation(fakeContainer, "testFlightNo0", "testPassId", DateTime.Now);
			resE.confirm(fakeContainer);
			resE.refund(fakeContainer, "testId", DateTime.Now);
			Cancellation canc = fakeContainer.CancellationSet.First();

			Assert.IsNotNull(canc);
			Assert.AreEqual(ReservationStatus.Cancelled, resE.Status);
			Assert.AreEqual(20 * 5000 + 51 * 1000, flight.FareCollected);
			Assert.AreEqual(1000, resE.Fare);
			Assert.AreEqual(1, pas.TotalTimesFlown);
			Assert.AreEqual(1000, pas.FareCollected);
			Assert.AreEqual(100, flight.EcoFree);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void refundUnconfirmed()
		{
			Reservation res = createTestReservation(0, 0);
			res.Status = ReservationStatus.Unprocessed;

			res.refund(fakeContainer, null, DateTime.Now);
			Assert.Fail();
		}

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void cancelUnconfirmed()
        {
            Reservation res = createTestReservation(0, 0);
            res.Status = ReservationStatus.Unprocessed;
            Cancellation cans = new Cancellation();
            cans.initialize_from_ticket(res, 0.0m, null, null, DateTime.Now);
            Assert.Fail();
        }
	}
}
