using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms
{
    public partial class MasterForm : System.Web.UI.MasterPage
    {     
        protected void Page_Load(object sender, EventArgs e)
        {
            prepare_tabs();
            display_login();
        }

        protected void prepare_tabs()
        {
            DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
            switch (role)
            {
                case DataBase.UserRole.Unqualified: break;
                case DataBase.UserRole.Client:
                    // Client code
                    personalTicketsLink.Visible = true;
                    break;
                case DataBase.UserRole.Administrator:
                    // Admin code
                    personalTicketsLink.Visible = true;
                    manageUsers.Visible = true;
                    backUp.Visible = true;
                    break;
                case DataBase.UserRole.Executive:
                    // Executive code
                    personalTicketsLink.Visible = true;
                    searchReservations.Visible = true;
                    break;
                case DataBase.UserRole.Manager:
                    // Manager code
                    personalTicketsLink.Visible = true;
					createFlightLink.Visible = true;
                    manageUsers.Visible = true;
                    finReport.Visible = true;
					passengerList.Visible = true;
                    break;
            }
        }

        protected void display_login()
        {
            string login = (string)Session["Login"];
            if (login != null)
            {
                loginLink.Visible = false;
                logoffLink.Visible = true;
                registerLink.Visible = false;
                changePassword.Visible = true;
                loginLabel.Text = login;
            }
            else
            {
                loginLink.Visible = true;
                logoffLink.Visible = false;
                registerLink.Visible = true;
                changePassword.Visible = false;
                loginLabel.Text = "Вход не выполнен";
            }
        }

        protected void logoffLink_Click(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Session["Privilege"] = DataBase.UserRole.Unqualified;
            Response.Redirect("~/WebForms/Common/Default.aspx");
        }
    }
}