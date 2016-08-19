using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.BusinessManager
{
	public partial class PassengerList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
			if (role != DataBase.UserRole.Manager)
				Response.Redirect("~/WebForms/Common/Default.aspx");
			errorLabel.Visible = false;
		}

		private void Page_Error(object sender, EventArgs e)
		{
			Session["Error"] = Server.GetLastError();
			Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
		}

		protected void filterCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			passengerGridView.DataBind();
		}

		// The return type can be changed to IEnumerable, however to support
		// paging and sorting, the following parameters must be added:
		//     int maximumRows
		//     int startRowIndex
		//     out int totalRowCount
		//     string sortByExpression
		public IQueryable<WebPortal.DataBase.Passenger> passengerGridView_GetData()
		{
			try
			{
				var context = new DataBase.SkySharkDbContainer();
				if (filterCheckBox.Checked)
				{
					try
					{
						Decimal min = Convert.ToDecimal(minFareBox.Text);
						Decimal max = Convert.ToDecimal(maxFareBox.Text);
						return (from p in context.PassengerSet
								where (p.FareCollected >= min) && (p.FareCollected <= max)
								select p);
					}
					catch (FormatException error)
					{
						Global.print_error(error, errorLabel);
						return context.PassengerSet;
					}
				}
				else
					return context.PassengerSet;
			}
			catch (Exception error)
			{
				Global.print_error(error, errorLabel);
				return null;
			}
		}

		protected void minFareBox_TextChanged(object sender, EventArgs e)
		{
			if (filterCheckBox.Checked)
				passengerGridView.DataBind();
		}

		protected void discountBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (filterCheckBox.Checked)
				{
					try
					{
						using (var context = new DataBase.SkySharkDbContainer())
						{
							Decimal min = Convert.ToDecimal(minFareBox.Text);
							Decimal max = Convert.ToDecimal(maxFareBox.Text);
							float discount = Convert.ToSingle(discountBox.Text);
							if (discount < 0.0 || discount > 1.0)
								throw new Exception("Размер скидки должен принадлежать диапазону от 0.0 до 1.0");
							var diap =
								(from p in context.PassengerSet
								 where (p.FareCollected >= min) && (p.FareCollected <= max)
								 select p);
							foreach (var p in diap)
								p.Discount = discount;
							context.SaveChanges();
							passengerGridView.DataBind();
						}
					}
					catch (FormatException error)
					{
						Global.print_error(error, errorLabel);
					}
				}
				else
					throw new Exception("Определите диапазон - критерий выбора пассажиров и включите фильтр!");
			}
			catch (Exception error)
			{
				Global.print_error(error, errorLabel);
			}
		}
	}
}