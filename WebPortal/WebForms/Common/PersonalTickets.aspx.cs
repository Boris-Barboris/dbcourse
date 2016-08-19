using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class PersonalTickets : System.Web.UI.Page
    {
        string login;

        protected void Page_Load(object sender, EventArgs e)
        {
            login = (string)Session["Login"];
            if (login == null)
                Response.Redirect("~/WebForms/Common/Default.aspx");
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }

        public IQueryable<WebPortal.DataBase.Reservation> reservationList_GetData()
        {
            try
            {
                var context = new DataBase.SkySharkDbContainer();
                return context.UserSet.Find(login).Reservation.Where(res => res.Status != DataBase.ReservationStatus.Cancelled).AsQueryable();
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
                return null;
            }
        }

        protected void reservationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Cancel")
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        db.ReservationSet.Find(Convert.ToInt32(e.CommandArgument)).cancel(db);
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
    }
}