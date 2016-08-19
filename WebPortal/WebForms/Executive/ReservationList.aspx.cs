using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Executive
{
    public partial class ReservationList : System.Web.UI.Page
    {
        string flightNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
            flightNo = (string)Session["FlightNo"];
            if (role != DataBase.UserRole.Executive || flightNo == null)
                Response.Redirect("~/WebForms/Common/Default.aspx");
            reservLabel.Text = "Билеты на рейс № " + flightNo;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<WebPortal.DataBase.Reservation> reservationList_GetData()
        {
            try
            {
                var context = new DataBase.SkySharkDbContainer();
                return context.ReservationSet.Where(res => (res.FlightNo == flightNo) && (res.Status != DataBase.ReservationStatus.Cancelled));
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
                return null;
            }
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }

        protected void reservationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Confirm")
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        db.ReservationSet.Find(Convert.ToInt32(e.CommandArgument)).confirm(db);
                        db.SaveChanges();
                        e.Handled = true;
                    }
                }
                if (e.CommandName == "Cancel")
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        db.ReservationSet.Find(Convert.ToInt32(e.CommandArgument)).cancel(db);
                        db.SaveChanges();
                        e.Handled = true;
                    }
                }
                if (e.CommandName == "Refund")
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        db.ReservationSet.Find(Convert.ToInt32(e.CommandArgument)).refund(db, (string)Session["Login"], DateTime.Now);
                        db.SaveChanges();
                        e.Handled = true;
                    }
                }
                reservationGridView.DataBind();
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

        protected void clearResBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DataBase.SkySharkDbContainer())
                {
                    foreach (var reserv in db.ReservationSet.Where(
                        res => (res.FlightNo == flightNo) && (res.Status == DataBase.ReservationStatus.Unprocessed)))
                        reserv.cancel(db);
                    db.SaveChanges();
                }
                reservationGridView.DataBind();
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }
    }
}