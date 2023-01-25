using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System.Drawing;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers.Data;

namespace DXMNCGUI_CARPOOL_SYSTEM
{
    public partial class Login : BasePage
    {
        private Boolean fIsEntryValid()
        {
            if ("" == txtID.Text)
            {
                lblMessage.Text = "User Id must be filled!";
                txtID.Focus();
                return false;
            }
            else if ("" == txtPassword.Text)
            {
                lblMessage.Text = "Password must be filled!";
                txtPassword.Focus();
                return false;
            }
            return true;
        }
        private void mInitialize()
        {
            String strClientScript = "if(event.keyCode==13){ " + Page.ClientScript.GetPostBackEventReference(this, "Login", false) + "; }";
            txtID.Attributes.Add("onkeydown", strClientScript);
            txtPassword.Attributes.Add("onkeydown", strClientScript);
            lblMessage.Text = "";
            txtID.Focus();
            this.mExtractQueryString();
            Connection();
        }
        private void Connection()
        {
            this.dbsetting = new SqlDBSetting(DBServerType.SQL2000, GetSqlConnectionString(), 30); //"User Id=apps;Password=apps;Data Source= (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.213)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=DEV2)))", 30);
            this.localdbsetting = new SqlLocalDBSetting(DBServerType.SQL2000, GetLocalConnectionString(), 30); //"User Id=apps;Password=apps;Data Source= (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.213)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=DEV2)))", 30);
        }   
        private void mExtractQueryString()
        {
            String strMessageID = Server.UrlDecode(Request.QueryString["MsgID"]);

            switch (strMessageID)
            {
                case "1":
                    lblMessage.Text = "Sorry, your session expired.";
                    break;

                default:
                    break;
            }
        }        
        private void userexists(string strUserName, string strPassword)
        {
            try
            {
                MembershipUser userexists = Membership.GetUser(strUserName.ToUpper(), false);
                if (userexists == null)
                {
                    MembershipUser newuser = Membership.CreateUser(strUserName, strPassword, "www." + strUserName + "@gmail.com");
                }
                else
                {
                    userexists.ChangePassword(userexists.ResetPassword(), strPassword);
                }
            }
            catch { }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["SessionExpired"] != null)
                {
                    string strPage = "";
                    strPage = Request.QueryString["SessionExpired"].ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Sorry , " + strPage + "...');", true);
                }
                Session.Abandon();
                this.mInitialize();
                object value = Cache["SCH_Company"];
                
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Connection();
            if (!fIsEntryValid()) { return; }

            this.dbsession = SqlDBSession.Create(dbsetting, localdbsetting);
            this.dbsession.UseEncryptedPassword = true;


            if (this.dbsession.Login(txtID.Text, txtPassword.Text))
            {
                this.UserID = this.dbsession.LoginUserID.ToUpper();
                this.Email = this.dbsession.LoginEmail.ToUpper();
                this.SessionID = Guid.NewGuid();
                accessright = AccesRight.Create(localdbsetting, this.UserID);
                object obj = dbsetting.ExecuteScalar("SELECT USER_NAME FROM [dbo].[MASTER_USER] WHERE USER_ID=? AND IS_ACTIVE_FLAG=1", (object)this.Email);
                if (obj != null && obj != DBNull.Value)
                {
                    this.UserName = obj.ToString();
                }

                if (IsValid)
                {
                    if (this.Request.QueryString["SourceType"] != null && this.Request.QueryString["SourceKey"] != null)
                    {
                        string sourceType = this.Request.QueryString["SourceType"].ToString();
                        string sourceKey = this.Request.QueryString["SourceKey"].ToString();
                        return;
                    }
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
            }
            else
            {
                lblMessage.Text = "User name or password invalid!";
                txtPassword.Focus();
                return;
            }
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}