using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data.SqlClient;

namespace WebPortal.WebForms
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["Privilege"] = DataBase.UserRole.Unqualified;
            Session["Login"] = null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static void print_error(Exception error, Label label)
        {
            if (error == null)
                return;
            else
                if (error.InnerException != null)
                    print_error(error.InnerException, label);
                else
                {
                    label.Text = "ОШИБКА: " + error.Message;
                    label.Visible = true;
                }
            }
    }
}