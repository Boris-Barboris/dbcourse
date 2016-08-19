using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class CustomMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            customLabel.Text = (string)Session["customMessage"];
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }
    }
}