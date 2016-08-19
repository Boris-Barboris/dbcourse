using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.DataBase
{
    public partial class FlightDetails
    {
        // Вызвать после конструктора и инициализировать вспомогательные поля
        public void Initialize()
        {
            EcoFree = SeatsEco;
            BnFree = SeatsBn;
            FareCollected = 0.0m;
        }

        public static IQueryable<FlightDetails> getNotDepartedFlights(SkySharkDbContainer context)
        {
            DateTime now = DateTime.Now;
            return (from flight in context.FlightDetailsSet
                    where flight.DepTime > now
                    select flight);
        }

        public static IQueryable<FlightDetails> getAllFlights(SkySharkDbContainer context)
        {
            return context.FlightDetailsSet;
        }
    }
}