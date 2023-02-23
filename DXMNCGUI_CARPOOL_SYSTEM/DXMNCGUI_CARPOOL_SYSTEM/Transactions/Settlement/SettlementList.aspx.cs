using DevExpress.Web;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement
{
    public partial class SettlementList : BasePage
    {
        DateTime datenow = DateTime.Now;
        public List<int> MergedIndexList = new List<int>();
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
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
        protected SettlementDB mySettlementDB
        {
            get { isValidLogin(false); return (SettlementDB)HttpContext.Current.Session["mySettlementDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySettlementDB" + this.ViewState["_PageID"]] = value; }
        }
        protected SettlementEntity mySettlementEntity
        {
            get { isValidLogin(false); return (SettlementEntity)HttpContext.Current.Session["mySettlementEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySettlementEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                this.mySettlementDB = SettlementDB.Create(myDBSetting, dbsession, localdbsetting);

                if ((accessright.IsAccessibleByUserID(Email, "IS_GA")) || (accessright.IsAccessibleByUserID(Email, "IS_ADMIN")))
                {
                    myMainTable = this.mySettlementDB.LoadBrowseTable(true, UserName);
                }
                else
                {
                    myMainTable = this.mySettlementDB.LoadBrowseTable(false, UserName);
                }
          
                gvMain.DataBind();
                GetApprovalTable();
                gvApprovalList.DataBind();

                btnApprovalList.Text += " (" + Convert.ToString(myApprovalTable.Rows.Count) + ")";

                accessable();

                if (this.Request.QueryString["DocKey"] != null)
                {
                    int indexVal = gvMain.FindVisibleIndexByKeyValue(this.Request.QueryString["DocKey"]);
                    gvMain.FocusedRowIndex = indexVal;
                }
                else
                {
                    gvMain.FocusedRowIndex = -1;
                }
                setEnabledButton();
            }
        }

        protected void GetApprovalTable()
        {
            string ssql = "exec GetApprovalSettlement ?";
            myApprovalTable = myLocalDBSetting.GetDataTable(ssql, false, Email);
        }


        protected void accessable()
        {
        }
        protected bool ErrorInField(out string strmessageError)
        {
            bool errorF = false;
            strmessageError = "";
            return errorF;
        }
        private void refreshdatagrid()
        {
            if ((accessright.IsAccessibleByUserID(Email, "IS_GA")) || (accessright.IsAccessibleByUserID(Email, "IS_ADMIN")))
            {
                myMainTable = this.mySettlementDB.LoadBrowseTable(true, UserName);
            }
            else
            {
                myMainTable = this.mySettlementDB.LoadBrowseTable(false, UserName);
            }
            gvMain.DataSource = myMainTable;
            gvMain.DataBind();
        }
        private void setEnabledButton()
        { }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                mySettlementEntity = mySettlementDB.Entity(DXCType.BK);
                var docKeyValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                docKeyValues.Set("DocKey", this.ViewState["_PageID"].ToString());

                var actionType = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                actionType.Set("Action", "New");

                updatedQueryString = "?" + docKeyValues.ToString() + "&" + actionType.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/Settlement/SettlementEntry.aspx" + updatedQueryString);

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {

                    var dockeyValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    dockeyValues.Set("DocKey", myrow["DocKey"].ToString());

                    var actionValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    actionValues.Set("Action", "View");

                    updatedQueryString = "?" + dockeyValues.ToString() + "&" + actionValues.ToString();
                    
                    mySettlementEntity = mySettlementDB.View(System.Convert.ToInt32(myrow["DocKey"]));
                    Response.Redirect("~/Transactions/Settlement/SettlementEntry.aspx" + updatedQueryString,false);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvMain_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();

                myDBSetting = dbsetting;
                myDBSession = dbsession;
            }
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    refreshdatagrid();
                    break;
                case "SHOW":
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());

                    DataRow myapprovalrow = gvApprovalList.GetDataRow(gvApprovalList.FocusedRowIndex);
                    DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                    string updatedQueryString = "";

                    nameValues.Set("DocKey", myapprovalrow["DocKey"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();

                    nameValues.Set("Action", "Approval");
                    updatedQueryString = "?" + nameValues.ToString();
                    ASPxWebControl.RedirectOnCallback("~/Transactions/Settlement/SettlementEntry.aspx" + updatedQueryString);
                    break;
                case "APPROVE":
                    SaveApprove(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "This settlement has been approved..";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE_CONFIRM";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "Are you sure want to approve this settlement?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    break;
                case "REJECT":
                    SaveApprove(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "This settlement has been rejected..";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT_CONFIRM";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "Are you sure want to reject this settlement?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    break;
            }
        }

        protected void gvApprovalList_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!IsPostBack)
            {
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                myApprovalTable = new DataTable();
                this.mySettlementDB = SettlementDB.Create(myDBSetting, dbsession, localdbsetting);
                myApprovalTable = this.mySettlementDB.LoadBrowseTableApproval(Email);
            }
            gvApprovalList.DataSource = myApprovalTable;
            gvApprovalList.DataBind();
        }
        protected void gvApprovalList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalTable;
        }
        protected void gvApprovalList_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvApprovalList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        private bool SaveApprove(SaveAction saveAction)
        {
            bool bSave = true;
            DataRow myrow = gvApprovalList.GetDataRow(gvApprovalList.FocusedRowIndex);
            try
            {
                this.mySettlementDB = SettlementDB.Create(myDBSetting, myDBSession, localdbsetting);
                mySettlementEntity = this.mySettlementDB.View(Convert.ToInt32(myrow["DocKey"]));
            }
            catch
            { }
            mySettlementEntity.Save(Email, UserName, "BK", saveAction, "");
            return bSave;
        }
    }
}