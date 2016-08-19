using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.DataBase
{
    public partial class PassengerNotFoundException : Exception
    {
        public PassengerNotFoundException(string passID, string Message)
            : base(Message)
        {
            PassID = passID;
        }

        public string PassID { get; private set; }
    }

	public partial class FlightNotFoundException : Exception
	{
		public FlightNotFoundException(string flightNo, string Message)
			: base(Message)
		{
			FlightNo = flightNo;
		}

		public string FlightNo { get; private set; }
	}

    public partial class Reservation
    {
        public void apply_reservation(SkySharkDbContainer context, string flightNo, string reservedBy, DateTime curTime)
        {
            if (flightNo != null)
                FlightNo = flightNo;
            if (reservedBy != null)
                ReservedBy = reservedBy;
			DateOfRes = curTime;
			Flight = context.FlightDetailsSet.Find(FlightNo);
			if (Flight == null)
				throw new FlightNotFoundException(FlightNo, "Не найден рейс с таким номером");
            Passenger = context.PassengerSet.Find(ReservedBy);
            if (Passenger == null)
                throw new PassengerNotFoundException(ReservedBy, "Не найден пассажир с таким номером паспорта");
			occupy_place();
            context.ReservationSet.Add(this);
			if (EMail != null)
				foreach (var user in context.UserSet.Where(u => u.EMail == EMail))
					user.Reservation.Add(this);
            calculate_fare();
            Status = ReservationStatus.Unprocessed;
        }

        public void confirm(SkySharkDbContainer context)
        {
            if (Status == ReservationStatus.Cancelled)
                throw new InvalidOperationException("Нельзя подтвердить отменённую бронь.");
            calculate_fare();
            Passenger.FareCollected += Fare;
			Passenger.TotalTimesFlown++;
            Flight.FareCollected += Fare;
            Status = ReservationStatus.Confirmed;
        }

        private void calculate_fare()
        {
            if (ClassOfRes == FlightClass.Eco)
                Fare = (decimal)(1.0 - Passenger.Discount) * Flight.FareEco;
            if (ClassOfRes == FlightClass.Business)
                Fare = (decimal)(1.0 - Passenger.Discount) * Flight.FareBn;
        }

        public void cancel(SkySharkDbContainer context)
        {
            if (Status == ReservationStatus.Confirmed)
                throw new InvalidOperationException("Нельзя отменить оплаченную бронь.");
            foreach (var user in context.UserSet.Where(u => u.EMail == EMail))
                user.Reservation.Remove(this);
            Status = ReservationStatus.Cancelled;
            free_place();
            context.ReservationSet.Remove(this);
        }

        public void refund(SkySharkDbContainer context, string operatorId, DateTime curTime)
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Вернуть можно лишь оплаченный билет.");
            DataBase.Cancellation canc = new Cancellation();
            // За сутки и более возврат в 100%
            // За два часа и более возврат 25%
            // Иначе нет компенсации
            if (curTime <= Flight.DepTime.AddDays(-1))
            {
				canc.initialize_from_ticket(this, Fare, "Возврат более чем за сутки", operatorId, curTime);
                Passenger.FareCollected -= Fare;
				Passenger.TotalTimesFlown--;
                Flight.FareCollected -= Fare;
                Fare = 0;
            }
            else
                if (curTime <= Flight.DepTime.AddHours(-2))
                {
					canc.initialize_from_ticket(this, Fare * 0.25m, "Возврат более чем за два часа", operatorId, curTime);
                    Passenger.FareCollected -= Fare;
					Passenger.TotalTimesFlown--;
                    Flight.FareCollected -= Fare * 0.25m;
                    Fare *= 0.75m;
                }
                else
                {
					canc.initialize_from_ticket(this, 0, "Несвоевременный возврат", operatorId, curTime);
                }
            free_place();
            context.CancellationSet.Add(canc);
            Status = ReservationStatus.Cancelled;
        }

        private void occupy_place()
        {
			if (ClassOfRes == FlightClass.Eco)
				if (Flight.EcoFree > 0)
					Flight.EcoFree--;
				else
					throw new InvalidOperationException("На этот рейс нет билетов эконом-класса.");
            if (ClassOfRes == FlightClass.Business)
				if (Flight.BnFree > 0)
					Flight.BnFree--;
				else
					throw new InvalidOperationException("На этот рейс нет билетов бизнес-класса.");
        }

        private void free_place()
        {
            if (ClassOfRes == FlightClass.Eco)
                Flight.EcoFree++;
            if (ClassOfRes == FlightClass.Business)
                Flight.BnFree++;
        }

        public bool IsCancelled() { return (Status == ReservationStatus.Cancelled); }
    }
}