using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Login code
                string login = loginBox.Text;
                string password = passwordBox.Text;
                DataBase.User result;
                using (var dbContext = new DataBase.SkySharkDbContainer())
                    result = dbContext.UserSet.Find(login);           // find username in database
                if ((result != null) && (result.Password == password))
                {
                    // Login successfull
                    Session["Login"] = login;
                    Session["Privilege"] = result.Role;
                    Response.Redirect("~/WebForms/Common/Default.aspx");
                }
                else
                {
                    // Login failed
                    loginFailed.Visible = true;
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