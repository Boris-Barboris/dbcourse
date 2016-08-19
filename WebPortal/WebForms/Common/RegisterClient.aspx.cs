using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class RegisterClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            // validate input
            if (!checkFields())
                return;
            try
            {
                // Proceed with registration
                DataBase.User newClient = new DataBase.User();
                newClient.Username = loginBox.Text;
                newClient.Password = pwBox1.Text;
                newClient.Role = DataBase.UserRole.Client;
                newClient.EMail = mailBox.Text;
                using (var context = new DataBase.SkySharkDbContainer())
                {
                    var sameMail = 
                        (from user in context.UserSet
                        where user.EMail == newClient.EMail
                        select user).FirstOrDefault();
                    if (sameMail != null)
                    {
                        mailUnique.IsValid = false;
                        return;
                    }
                    var sameUser =
                        (from user in context.UserSet
                         where user.Username == newClient.Username
                         select user).FirstOrDefault();
                    if (sameUser != null)
                    {
                        loginUnique.IsValid = false;
                        return;
                    }
                    context.UserSet.Add(newClient);
                    context.SaveChanges();
                    Response.Redirect("~/WebForms/Common/Login.aspx");
                }
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

        const string loginRegex = @"^[a-z,A-Z,0-9]+$";
        const string passwordRegex = @"^[a-z,A-Z,0-9]+$";

        protected bool checkFields()
        {
            if (loginBox.Text.Length == 0)
            {
                loginValidator.IsValid = false;
                return false;
            }
            if (!Regex.IsMatch(loginBox.Text, loginRegex))
            {
                loginRegExp.IsValid = false;
                return false;
            }
            if (pwBox1.Text.Length == 0)
            {
                pwValidator1.IsValid = false;
                return false;
            }
            if (pwBox2.Text.Length == 0)
            {
                pwValidator2.IsValid = false;
                return false;
            }
            if (pwBox1.Text != pwBox2.Text)
            {
                pwSameValidator.IsValid = false;
                return false;
            }
            if (!Regex.IsMatch(pwBox1.Text, passwordRegex))
            {
                pwRegExp.IsValid = false;
                return false;
            }
            if (mailBox.Text.Length <= 0)
            {
                mailValid.IsValid = false;
                return false;
            }
            return true;
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }
    }
}