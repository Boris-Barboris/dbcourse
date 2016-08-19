using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure;

namespace WebPortal.WebForms.NetworkAdministrator
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.UserRole role = (DataBase.UserRole)Session["Privilege"];
            if ((role != DataBase.UserRole.Administrator) && (role != DataBase.UserRole.Manager))
                Response.Redirect("~/WebForms/Common/Default.aspx");
            if (role == DataBase.UserRole.Manager)
            {
                usersGridView.Enabled = false;
                usersGridView.Visible = false;
            }
        }

        public IQueryable<DataBase.User> usersGridView_GetData()
        {
            try
            {   
                var context = new DataBase.SkySharkDbContainer();
                return context.UserSet;
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
                return null;
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void usersGridView_UpdateItem(string Username)
        {
            try
            {
                using (var db = new DataBase.SkySharkDbContainer())
                {
                    DataBase.User item = null;
                    item = db.UserSet.Find(Username);
                    if (item == null)
                    {
                        ModelState.AddModelError("",
                          String.Format("Пользователь с логином {0} не был найден", Username));
                        return;
                    }

                    TryUpdateModel(item);
                    if (ModelState.IsValid)
                    {
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void usersGridView_DeleteItem(string Username)
        {
            try
            {
                using (var db = new DataBase.SkySharkDbContainer())
                {
                    DataBase.User item = new DataBase.User() { Username = Username };
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        ModelState.AddModelError("",
                            String.Format("Пользователь с логином {0} не существует в базе данных.", Username));
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

        public void newUserForm_InsertItem()
        {
            try
            {
                var item = new DataBase.User();

                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    using (var db = new DataBase.SkySharkDbContainer())
                    {
                        db.UserSet.Add(item);
                        db.SaveChanges();
                        usersGridView.DataBind();
                    }
                }
            }
            catch (Exception error)
            {
                Global.print_error(error, errorLabel);
            }
        }

    }
}