using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            string login = (string)Session["Login"];
            if (login == null)
                Response.Redirect("~/WebForms/Common/Default.aspx");
        }

        protected void changePwBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Login code
                string login = (string)Session["Login"];
                if (login == null)
                    return;
                if (newPwBox1.Text != newPwBox2.Text)
                {
                    newPwEqual.IsValid = false;
                    return;
                }
                using (var dbContext = new DataBase.SkySharkDbContainer())
                {
                    DataBase.User result = dbContext.UserSet.Find(new[] { login });           // find username in database
                    result.Password = newPwBox1.Text;
                    result.passwordChanged = true;
                    dbContext.SaveChanges();
                    passwordChanged.Visible = true;
                }
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }
    }
}