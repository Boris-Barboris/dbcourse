using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.BusinessManager
{
	public partial class NewFlight : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
			if (role != DataBase.UserRole.Manager)
				Response.Redirect("~/WebForms/Common/Default.aspx");
			errorLabel.Visible = false;
		}

		public void newFlightForm_InsertItem()
		{
			try
			{
				var item = new WebPortal.DataBase.FlightDetails();
				TryUpdateModel(item);
				if (ModelState.IsValid)
				{
					// Flight additional validation code
					item.Initialize();
					if (item.DepTime >= item.ArrTime)
					{
						ModelBindingExecutionContext.ModelState.AddModelError(null, "Вылет позже прилёта");
						throw new Exception("Вылет позже прилёта");
					}
					if (item.DepTime <= DateTime.Now)
					{
						ModelBindingExecutionContext.ModelState.AddModelError(null, "Вылет до текущей даты");
						throw new Exception("Вылет в прошлом");
					}
					using (var context = new DataBase.SkySharkDbContainer())
					{
						context.FlightDetailsSet.Add(item);
						context.SaveChanges();
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