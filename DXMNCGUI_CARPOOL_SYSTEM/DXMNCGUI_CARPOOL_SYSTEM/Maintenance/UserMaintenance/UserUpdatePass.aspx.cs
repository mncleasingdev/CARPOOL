using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Maintenance.UserMaintenance
{
    public partial class UserUpdatePass : BasePage
    {
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected DataSet myMainDataSet
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myMainDataSet" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainDataSet" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainDataTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainDataTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainDataTable" + this.ViewState["_PageID"]] = value; }
        }
        protected bool bChangePass
        {
            get { isValidLogin(false); return (bool)HttpContext.Current.Session["bChangePass" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["bChangePass" + this.ViewState["_PageID"]] = value; }
        }

        private string GeneratePassword(string myEncryption)
        {
            string myString = string.Empty;
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT CONVERT(VARCHAR(32), HashBytes('MD5', @PASS), 2)");
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@PASS", SqlDbType.NVarChar, 32);
                sqlParameter1.Value = myEncryption;
                sqlParameter1.Direction = ParameterDirection.Input;

                myString = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
            return myString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                bChangePass = false;
            }
            else
            {

            }
        }



        protected void UpdatePasswordField(string newPassword)
        {
            string email = this.Email;

            myLocalDBSetting.ExecuteNonQuery("UPDATE MasterUser SET USER_PASSWORD=? WHERE EMAIL=?", (object)newPassword, (object)email);
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;

            switch (callbackParam[0].ToUpper())
            {
                case "SAVECONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to submit this?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    break;
                case "SAVE":
                    Save();
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";

                    myDBSession.Logout();
                    HttpContext.Current.Session.Abandon();
                    Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Account/Login.aspx");
                    break;
            }
        }
        
        protected void Save()
        {
            UpdatePasswordField(GeneratePassword(cnpsw.Text));
        }
    }
}