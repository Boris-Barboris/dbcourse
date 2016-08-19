using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Executive
{
    public partial class SearchReservations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
            if (role != DataBase.UserRole.Executive)
                Response.Redirect("~/WebForms/Common/Default.aspx");
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
                return context.ReservationSet.Where(res => (res.Passenger.ID == passIDBox.Text));
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
                if (e.CommandName == "ShowCancellation")
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        var canc = db.ReservationSet.Find(Convert.ToInt32(e.CommandArgument)).Cancellation;
                        if (canc != null)
                        {
                            listView.Visible = true;
                            listView.DataSource = new List<DataBase.Cancellation>() { canc };
                            listView.DataBind();
                        }
                        else
                            listView.Visible = false;
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

        protected void searchButton_Click(object sender, EventArgs e)
        {
            reservationGridView.DataBind();
        }
    }
}