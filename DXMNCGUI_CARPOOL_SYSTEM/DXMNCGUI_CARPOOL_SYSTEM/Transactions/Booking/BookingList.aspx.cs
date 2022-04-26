using DevExpress.Web;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public partial class BookingList : BasePage
    {
        DateTime datenow = DateTime.Now;
        public List<int> MergedIndexList = new List<int>();
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected BookingDB myBookingDB
        {
            get { isValidLogin(false); return (BookingDB)HttpContext.Current.Session["myBookingDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBookingDB" + this.ViewState["_PageID"]] = value; }
        }
        protected BookingEntity myBookingEntity
        {
            get { isValidLogin(false); return (BookingEntity)HttpContext.Current.Session["myBookingEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBookingEntity" + this.ViewState["_PageID"]] = value; }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                this.myBookingDB = BookingDB.Create(myDBSetting, dbsession);
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
                refreshdatagrid();
                setEnabledButton();
            }
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
            if (accessright.IsAccessibleByUserID(Email, "IS_CUSTOMER"))
            {
                myMainTable = this.myBookingDB.LoadBrowseTable(false, false, UserName, "");
            }
            if (accessright.IsAccessibleByUserID(Email, "IS_DISPATCHER"))
            {
                object obj = myDBSetting.ExecuteScalar("SELECT Department FROM MasterUser WHERE Email=?", Email);
                if (obj != null || obj != DBNull.Value)
                {
                    myMainTable = this.myBookingDB.LoadBrowseTable(false, true, UserName, Convert.ToString(obj));
                }
            }
            if (accessright.IsAccessibleByUserID(Email, "IS_CUSTOMER") && accessright.IsAccessibleByUserID(Email, "IS_DISPATCHER"))
            {
                object obj = myDBSetting.ExecuteScalar("SELECT Department FROM MasterUser WHERE Email=?", Email);
                if (obj != null || obj != DBNull.Value)
                {
                    myMainTable = this.myBookingDB.LoadBrowseTable(false, true, UserName, Convert.ToString(obj));
                }
            }
            if (accessright.IsAccessibleByUserID(Email, "IS_ADMIN"))
            {
                myMainTable = this.myBookingDB.LoadBrowseTable(true, false, UserName, "");
            }
            if (accessright.IsAccessibleByUserID(Email, "IS_DRIVER"))
            {
                string updatedQueryString = "";
                myMainTable = this.myBookingDB.LoadBrowseTableForDriver(UserName);
                if (myMainTable.Rows.Count > 0)
                {
                    DataRow[] myrow = myMainTable.Select("Status='PICKUP'");
                    if (myrow.Length > 0)
                    {
                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();
                        myBookingEntity = myBookingDB.View(System.Convert.ToInt32(myrow[0]["DocKey"]));
                        if (accessright.IsAccessibleByUserID(Email, "IS_DRIVER"))
                        {
                            Response.Redirect("~/Transactions/Booking/DriverActionForm.aspx" + updatedQueryString);
                        }
                    }
                }
            }
            gvMain.DataSource = myMainTable;
            gvMain.DataBind();

            myApprovalTable = this.myBookingDB.LoadBrowseTableApproval(Email);
            gvApprovalList.DataSource = myApprovalTable;
            gvApprovalList.DataBind();
        }
        private void setEnabledButton()
        {

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";

            if (accessright.IsAccessibleByUserID(Email, "IS_DRIVER"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Sorry, you are an driver, cannot create booking ticket !');", true);
                return;
            }

            try
            {
                myBookingEntity = myBookingDB.Entity(DXCType.BK);
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/Booking/BookingEntry.aspx" + updatedQueryString);
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myBookingEntity = myBookingDB.View(System.Convert.ToInt32(myrow["DocKey"]));
                    if (accessright.IsAccessibleByUserID(Email, "IS_DRIVER"))
                    {
                        Response.Redirect("~/Transactions/Booking/DriverActionForm.aspx" + updatedQueryString);
                    }
                    else
                    {
                        Response.Redirect("~/Transactions/Booking/BookingEntry.aspx" + updatedQueryString);
                    }
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
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myBookingEntity = myBookingDB.Edit(System.Convert.ToInt32(myrow["DocKey"]), DXCAction.Edit);
                    Response.Redirect("~/Transactions/Booking/BookingEntry.aspx" + updatedQueryString);
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
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    refreshdatagrid();
                    break;
                case "APPROVE":
                    SaveApprove(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "This book has been approved..";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE_CONFIRM";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "Are you sure want to approve this book trip?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    break;
                case "REJECT":
                    SaveApprove(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "This book has been rejected..";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT_CONFIRM";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "Are you sure want to reject this book trip?";
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
                this.myBookingDB = BookingDB.Create(myDBSetting, dbsession);
                myApprovalTable = this.myBookingDB.LoadBrowseTableApproval(Email);
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
                this.myBookingDB = BookingDB.Create(myDBSetting, myDBSession);
                myBookingEntity = this.myBookingDB.View(Convert.ToInt32(myrow["DocKey"]));
            }
            catch
            { }
            myBookingEntity.Save(UserID, UserName, "BK", saveAction, Convert.ToString(myrow["Status"]));
            return bSave;
        }
    }
}