using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.Common
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception error = (Exception)Session["error"];
            if (error == null)
                Response.Redirect("~/WebForms/Common/Default.aspx");
            Global.print_error(error, errorLabel);
        }
    }
}