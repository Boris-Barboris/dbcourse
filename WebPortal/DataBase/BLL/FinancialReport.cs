using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.DataBase
{
    public class TimeIntervalReport
    {
        public TimeIntervalReport(SkySharkDbContainer context, DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;

            var flights = from f in context.FlightDetailsSet
						  where ((f.DepTime >= start) && (f.DepTime < end))
                          select f;

            TotalFare = flights.Select(f => f.FareCollected).DefaultIfEmpty(0m).Sum();

            TotalRefunds =
                (from f in flights
                 join r in context.ReservationSet
                 on f.FlightNo equals r.FlightNo
                 where r.Status == ReservationStatus.Cancelled
                 join c in context.CancellationSet
                 on r.TicketNo equals c.TicketNo
                 select c.Refund).DefaultIfEmpty(0m).Sum();
            
            TotalFlights = flights.Count();

            TotalPassengersTravelled =
                (from f in flights
                 select f.SeatsEco + f.SeatsBn - f.EcoFree - f.BnFree).DefaultIfEmpty(0).Sum();
        }

        [Display(Name = "Общая прибыль")]
		[DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalFare { get; set; }

        [Display(Name = "Всего возвратов")]
		[DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalRefunds { get; set; }

        [Display(Name = "Кол-во рейсов")]
        public int TotalFlights { get; set; }

        [Display(Name = "Кол-во пассажиров")]
        public int TotalPassengersTravelled { get; set; }

        [Display(Name = "начало")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public DateTime start { get; set; }

        [Display(Name = "конец")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public DateTime end { get; set; }
    }
}