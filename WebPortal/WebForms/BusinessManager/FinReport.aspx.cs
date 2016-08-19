using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.BusinessManager
{
    public partial class FinReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
            if (role != DataBase.UserRole.Manager)
                Response.Redirect("~/WebForms/Common/Default.aspx");
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }

        public const int month_count = 12;
        public IQueryable reportGridView_GetData()
        {
            try
            {
                var context = new DataBase.SkySharkDbContainer();
                var months = get_months(month_count);
                var result = new List<DataBase.TimeIntervalReport>();
                for (int i = 0; i < month_count; i++)
                    result.Add(new DataBase.TimeIntervalReport(context, months[i+1], months[i]));
                return result.AsQueryable();
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
                return null;
            }
        }

        private List<DateTime> get_months(int month_count)
        {
            List<DateTime> result = new List<DateTime>();
            DateTime currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            currentMonth = currentMonth.AddMonths(1);
            for (int i = 0; i <= month_count; i++)
            {
                result.Add(currentMonth);
                currentMonth = currentMonth.AddMonths(-1);
            }
            return result;
        }
    }
}