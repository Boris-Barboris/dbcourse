using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class FlightList : System.Web.UI.Page
    {
        DataBase.UserRole userrole;

        protected void Page_Load(object sender, EventArgs e)
        {
            userrole = (DataBase.UserRole)Session["Privilege"];
            if (userrole == DataBase.UserRole.Manager)
                showManagerControls();
            if (userrole == DataBase.UserRole.Executive)
                showExecutiveControls();
            errorLabel.Visible = false;
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }

        protected void showManagerControls()
        {
            flightGridView.Columns[5].Visible = true;
            flightGridView.Columns[6].Visible = true;
            flightGridView.Columns[8].Visible = true;
            flightGridView.Columns[12].Visible = true;
            addFlightLink.Visible = true;
            showOldFlights.Visible = true;
        }

        protected void showExecutiveControls()
        {
            flightGridView.Columns[5].Visible = true;
            flightGridView.Columns[6].Visible = true;
            flightGridView.Columns[8].Visible = true;
            flightGridView.Columns[14].Visible = true;
            showOldFlights.Visible = true;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<WebPortal.DataBase.FlightDetails> flightGridView_GetData()
        {
            try
            {
                var context = new DataBase.SkySharkDbContainer();
                IQueryable<WebPortal.DataBase.FlightDetails> result;
                if (showOldFlights.Checked)
                    result = DataBase.FlightDetails.getAllFlights(context).OrderByDescending(d => d.DepTime);
                else
                    result = DataBase.FlightDetails.getNotDepartedFlights(context).OrderByDescending(d => d.DepTime);
                if (filterCheckBox.Checked)
                {
                    if (depBox.Text != null)
                        result = result.Where(f => f.Origin.Contains(depBox.Text));
                    if (destBox.Text != null)
                        result = result.Where(f => f.Destination.Contains(destBox.Text));
                }
                return result;
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
                return null;
            }
        }

        protected void flightGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reserve")
            {
                Session["FlightNo"] = e.CommandArgument;
                e.Handled = true;
                Response.Redirect("~/WebForms/Common/ReserveSeat.aspx");
            }
            if (e.CommandName == "manageReservations")
            {
                Session["FlightNo"] = e.CommandArgument;
                e.Handled = true;
                Response.Redirect("~/WebForms/Executive/ReservationList.aspx");
            }
        }

        protected void filterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            flightGridView.DataBind();
        }

        protected void depBox_TextChanged(object sender, EventArgs e)
        {
            if (filterCheckBox.Checked)
                flightGridView.DataBind();
        }


    }
}