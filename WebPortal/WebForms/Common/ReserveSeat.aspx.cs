using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class ReserveSeat : System.Web.UI.Page
    {
        protected string FlightNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            FlightNo = (string)Session["FlightNo"];
            reservLabel.Text = "Бронирование билета на рейс № " + FlightNo;
        }

        public void newReservationForm_InsertItem()
        {
            try
            {
                var item = new WebPortal.DataBase.Reservation();
                item.FlightNo = FlightNo;
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        try
                        {
                            if (nameTextBox.Visible)
                            {
                                var pass = new DataBase.Passenger();
                                pass.initialize(db, (string)Session["PassID"], nameTextBox.Text);
                            }
                            item.apply_reservation(db, null, null, DateTime.Now);
                            db.SaveChanges();
                            Session["customMessage"] = "Бронирование прошло успешно!";
                            Response.Redirect("~/WebForms/Common/CustomMessage.aspx");
                        }
                        catch (DataBase.PassengerNotFoundException nfe)
                        {
                            Session["PassID"] = nfe.PassID;
                            nameTextBox.Visible = true;
                            nameLabel.Visible = true;
                        }                        
                    }
                }
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }
    }
}