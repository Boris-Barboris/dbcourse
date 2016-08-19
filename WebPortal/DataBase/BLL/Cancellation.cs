using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.DataBase
{
    public partial class Cancellation
    {
        public void initialize_from_ticket(Reservation res, decimal refund, string comment, string operatorId, DateTime curtime)
        {
            if (res.Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Нельзя создать Cancellation для не подтверждённого билета.");
            TicketNo = res.TicketNo;
            Comment = comment;
			CancelDate = curtime;
            UserServiced = operatorId;
            Refund = refund;
        }
    }
}