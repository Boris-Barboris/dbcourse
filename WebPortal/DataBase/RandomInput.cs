using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.DataBase
{
	public class RandomInput
	{
		const double eco_partition = 0.7;
		const double business_partiotion = 0.5;
		const int generated_passengers = 1000;
        const int generated_flights = 10;

        static readonly string[] cities = new string[] { "Москва", "Санкт-Петербург", "Сочи", "Киев", "Волгоград", "Рязань",
            "Симферополь", "Тбилиси" };
        static readonly string[] planes = new string[] { "Ту-154", "Ту-202", "Ил-96" };

        public static void generate_flights(SkySharkDbContainer db)
        {
            // clear users with reservations
            foreach (var u in db.UserSet)
            {
                if (u.Reservation.Count > 0 || u.Cancellation.Count > 0)
                    db.UserSet.Remove(u);
            }
            // clear cancellations
            foreach (var c in db.CancellationSet)
            {
                db.CancellationSet.Remove(c);
            }
            // clear reservations
            foreach (var r in db.ReservationSet)
            {
                db.ReservationSet.Remove(r);
            }
            // clear passengers
            foreach (var p in db.PassengerSet)
                db.PassengerSet.Remove(p);
            db.SaveChanges();            
            // clear flight set
            foreach (var flight in db.FlightDetailsSet)
                db.FlightDetailsSet.Remove(flight);
            // generate new flights
            Random rng = new Random();
            DateTime now = DateTime.Now;
            for (int i = 0; i < generated_flights; i++)
            {
                FlightDetails f = new FlightDetails();
                f.DepTime = now + new TimeSpan((int)(20.0 * rng.NextDouble()) + 1, 0, 0, 0);
                f.ArrTime = f.DepTime + new TimeSpan(1 + (int)(2.0 * rng.NextDouble()), 0, 0);
                f.AircraftType = planes[(int)Math.Round(rng.NextDouble() * (planes.Length - 1))];
                f.FlightNo = "FLT" + i.ToString("G2");
                int origin_index = (int)Math.Round(rng.NextDouble() * (cities.Length - 1));
                int dest_index = 0;
                do
                {
                    dest_index = (int)Math.Round(rng.NextDouble() * (cities.Length - 1));
                } while (dest_index == origin_index);
                f.Origin = cities[origin_index];
                f.Destination = cities[dest_index];
                f.SeatsEco = (short)(200 + 50.0 * rng.NextDouble());
                f.EcoFree = f.SeatsEco;
                f.FareEco = Math.Round(5000 + (decimal)(2000.0 * rng.NextDouble()), 2);
                f.SeatsBn = (short)(40 + 10.0 * rng.NextDouble());
                f.BnFree = f.SeatsBn;
                f.FareBn = Math.Round(18000 + (decimal)(6000.0 * rng.NextDouble()), 2);
                
                // add
                db.FlightDetailsSet.Add(f);
            }
            // apply
            db.SaveChanges();
        }

		public static void regen_passStats(SkySharkDbContainer db)
		{
			foreach (var p in db.PassengerSet)
			{
				var pRes =  db.ReservationSet.Where(r => (r.ReservedBy == p.ID) && (r.Status == ReservationStatus.Confirmed));
				p.TotalTimesFlown = pRes.Count();
				p.FareCollected = pRes.Sum(r => r.Fare);
			}
			db.SaveChanges();
		}

		public static void confirm_unconfirmed_flights(SkySharkDbContainer db)
		{
			DateTime now = DateTime.Now;
			foreach (var flight in db.FlightDetailsSet.ToList())
			{
				if (flight.DepTime < now)
				{
					var unconfirmed_res =
						from res in db.ReservationSet
						where (res.FlightNo == flight.FlightNo) && (res.Status == ReservationStatus.Unprocessed)
						select res;
					foreach (var res in unconfirmed_res)
						res.confirm(db);
				}
			}
		}

		public static void generate_passenger_traffic(SkySharkDbContainer db)
		{
            foreach (var passenger in db.PassengerSet)
                db.PassengerSet.Remove(passenger);

			var all_flights = db.FlightDetailsSet;
			for (int i = 0; i < generated_passengers; i++)
			{
				var pass = new Passenger();
				pass.initialize(db, "GNR_ID_" + i.ToString(), "Generated Passenger " + i.ToString());
			}

			int current_pass = 0;
			foreach (var flight in all_flights)
			{
				DateTime regTime = flight.DepTime.AddDays(-2) < DateTime.Now ? flight.DepTime.AddDays(-2) : DateTime.Now;
				bool mass_confirm = regTime < DateTime.Now;
				if (flight.EcoFree > 0)
				{
					int eco_generated = (int)Math.Floor(flight.EcoFree * eco_partition);
					while (eco_generated > 0)
					{
						var reservation = new Reservation();
						reservation.ClassOfRes = FlightClass.Eco;
						reservation.apply_reservation(db, flight.FlightNo, "GNR_ID_" + current_pass.ToString(), regTime);
						if (mass_confirm)
							reservation.confirm(db);
						inc_pass(ref current_pass);
						eco_generated--;
					}
				}
				if (flight.BnFree > 0)
				{
					int bn_generated = (int)Math.Floor(flight.BnFree * business_partiotion);
					while (bn_generated > 0)
					{
						var reservation = new Reservation();
						reservation.ClassOfRes = FlightClass.Business;
						reservation.apply_reservation(db, flight.FlightNo, "GNR_ID_" + current_pass.ToString(), regTime);
						if (mass_confirm)
							reservation.confirm(db);
						inc_pass(ref current_pass);
						bn_generated--;
					}
				}
			}

			db.SaveChanges();
		}

		private static void inc_pass(ref int pass)
		{
			if (pass < generated_passengers - 1)
				pass++;
			else
				pass = 0;
		}

	}
}