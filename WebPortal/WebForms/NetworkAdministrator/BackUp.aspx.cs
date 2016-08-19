using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

namespace WebPortal.WebForms.NetworkAdministrator
{
    public partial class BackUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
            if (role != DataBase.UserRole.Administrator)
                Response.Redirect("~/WebForms/Common/Default.aspx");
        }



        private void Page_Error(object sender, EventArgs e)
        {
            Session["Error"] = Server.GetLastError();
            Response.Redirect("~/WebForms/Common/ErrorPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DataBase.SkySharkDbContainer())
                {
                    string dataTime = DateTime.Now.ToString("yyyy-MM-dd") + "-" + DateTime.Now.ToString("HH-mm");
                    string directory = HttpContext.Current.Server.MapPath("~/") + "backups\\" + dataTime + "\\";
                    string fileName = directory + dataTime + ".bak";
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction,
                        "BACKUP DATABASE SkySharkDb TO DISK = '" + fileName + "' WITH FORMAT, MEDIANAME = 'SkySharkDb_backups', NAME = 'Full Backup of SkySharkDb'");
                    Session["customMessage"] = "Создана резервная копия базы данных: " + fileName;
                    Response.Redirect("~/WebForms/Common/CustomMessage.aspx");
                }
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

		protected void generatePassengers_Click(object sender, EventArgs e)
		{
			try
			{
				using (var db = new DataBase.SkySharkDbContainer())
				{
                    DataBase.RandomInput.generate_flights(db);
					DataBase.RandomInput.confirm_unconfirmed_flights(db);
					DataBase.RandomInput.regen_passStats(db);
					DataBase.RandomInput.generate_passenger_traffic(db);
				}
			}
			catch (Exception error)
			{
				Global.print_error(error, errorLabel);
			}
		}
    }
}