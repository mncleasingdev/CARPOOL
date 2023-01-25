using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Microsoft.AspNet.Identity;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System.Web.Security;

namespace DXMNCGUI_CARPOOL_SYSTEM {
    public partial class RootMaster : System.Web.UI.MasterPage
    {
        protected SqlDBSetting myDBSetting
        {
            get { return (SqlDBSetting)HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { return (SqlDBSession)HttpContext.Current.Session["HomeSqlDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected AccesRight accessright
        {
            get { return (AccesRight)HttpContext.Current.Session["accessright"]; }
            set { HttpContext.Current.Session["accessright"] = value; }
        }
        protected string Email
        {
            get { return (string)HttpContext.Current.Session["Email"]; }
            set { HttpContext.Current.Session["Email"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxLabel2.Text = DateTime.Now.Year + Server.HtmlDecode(" &copy; Corporate Digital and System Information Division - PT. MNC Guna Usaha Indonesia");

            //if (accessright.IsAccessibleByUserID(Email.ToString(), "IS_ADMIN"))
            if (accessright.IsAccessibleByUserID(Email.ToString(), "IS_GA"))
            {
                HeaderMenu.Items.FindByName("MenuMaintenance").Visible = true;
                HeaderMenu.Items.FindByName("MenuReporting").Visible = true;
            }

            try
            {
                var lblUserName = this.HeadLoginView.FindControl("lblUserName") as ASPxLabel;
                if (lblUserName != null)
                {
                    lblUserName.Text = "Welcome, " + myDBSetting.ExecuteScalar("SELECT USER_NAME FROM MASTER_USER WHERE USER_ID=? AND IS_ACTIVE_FLAG=1", Email.ToString());
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Account/Login.aspx");
            }
        }
        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            HttpContext.Current.Session.Abandon();
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            myDBSession.Logout();
        }

        protected void ASPxCallback_Callback(object source, CallbackEventArgs e)
        {
            if (e.Parameter == "LogOut")
            {
                myDBSession.Logout();
                HttpContext.Current.Session.Abandon();
                Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Account/Login.aspx");
            }
        }
    }
}