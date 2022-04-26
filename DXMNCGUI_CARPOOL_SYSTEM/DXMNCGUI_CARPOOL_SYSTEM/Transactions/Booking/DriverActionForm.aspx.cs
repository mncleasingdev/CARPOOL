using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.GeoCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public partial class DriverActionForm : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
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
        protected DXCAction myAction
        {
            get { isValidLogin(false); return (DXCAction)HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]] = value; }
        }
        protected DXCType myDocType
        {
            get { isValidLogin(false); return (DXCType)HttpContext.Current.Session["myDocType" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocType" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected string myStatus
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                if (this.Request.QueryString["SourceKey"] != null && this.Request.QueryString["Type"] != null)
                {
                    this.myBookingDB = BookingDB.Create(dbsetting, myDBSession);
                    myBookingEntity = this.myBookingDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myds = new DataSet();
                this.myBookingDB = BookingDB.Create(myDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((BookingEntity)HttpContext.Current.Session["myBookingEntity" + strKey]);
             
                //CLocation myLocation = new CLocation();
                //myLocation.GetLocationEvent();
            }
            if (!IsCallback)
            {

            }
        }
        private void SetApplication(BookingEntity BookingEntity)
        {
            if (this.myBookingEntity != BookingEntity)
            {
                if (BookingEntity != null)
                {
                    this.myBookingEntity = BookingEntity;
                }
                myAction = this.myBookingEntity.Action;
                myDocType = this.myBookingEntity.DocumentType;
                myds = myBookingEntity.myDataSet;
                myStatus = this.myBookingEntity.Status.ToString();
                BindingMaster();
                Accessable();
            }
        }
        private void BindingMaster()
        {
            txtDocNo.Value = myBookingEntity.DocNo;
            deDocDate.Value = myBookingEntity.DocDate;
            txtBookingBy.Value = myBookingEntity.CreatedBy;
            txtPhone.Value = myBookingEntity.Hp;
            deStart.Value = myBookingEntity.AdminEstPickDateTime;
            deEnd.Value = myBookingEntity.AdminEstArriveDateTime;
            mmFrom.Value = myBookingEntity.RequestPickAddress;
            mmTo.Value = myBookingEntity.RequestDestAddress;
            mmDetailTrip.Value = myBookingEntity.TripDetails;
            mmRemark.Value = myBookingEntity.AdminRemark;
            txtLastKilometer.Value = myBookingEntity.AdminLastKilometer;
        }
        private void Accessable()
        {
            DateTime mydate = myDBSetting.GetServerTime();
            myStatus = myBookingEntity.Status.ToString();

            txtDocNo.ReadOnly = true;
            deDocDate.ReadOnly = true;
            deStart.ReadOnly = true;
            deEnd.ReadOnly = true;
            mmFrom.ReadOnly = true;
            mmTo.ReadOnly = true;
            mmDetailTrip.ReadOnly = true;
            mmRemark.ReadOnly = true;
            txtLastKilometer.ReadOnly = true;

            if (myStatus == "PICKUP")
            {
                btnPickUp.Text = "Finish";
                btnReject.ClientVisible = false;
                FormLayout1.FindItemOrGroupByName("liCurrentKM").Visible = true;
            }
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            return errorF;
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            myBookingEntity.DriverName = this.UserName;
            myBookingEntity.DriverRemark = mmRemark.Value;
            myBookingEntity.DriverCurrentKilometer = txtCurrentKilometer.Value;
            myBookingEntity.DriverLastKilometer = txtLastKilometer.Value;
            myBookingEntity.Save(UserID, UserName, "BK", saveAction, myBookingEntity.Status.ToString());
            return bSave;
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            urlsave = "~/Transactions/Booking/BookingList.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("DocKey", myBookingEntity.DocKey.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                #region ACYION BY DRIVER
                case "DRIVER_PICKUP":
                    Save(SaveAction.PickupByDriver);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been pickup...";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_PICKUP";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "DRIVER_PICKUP_CONFIRM":
                    if (myStatus != "PICKUP")
                    {
                        cplMain.JSProperties["cplblmessageError"] = "";
                        cplMain.JSProperties["cplblmessage"] = "are you sure want to pick-up this booking?";
                        cplMain.JSProperties["cplblActionButton"] = "DRIVER_PICKUP";
                        if (ErrorInField(out strmessageError, SaveAction.Save))
                        {
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                        }
                    }
                    else
                    {
                        cplMain.JSProperties["cplblmessageError"] = "";
                        cplMain.JSProperties["cplblmessage"] = "are you sure want to finish this booking?";
                        cplMain.JSProperties["cplblActionButton"] = "DRIVER_FINISH";
                        if (ErrorInField(out strmessageError, SaveAction.Save))
                        {
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                        }
                    }
                    break;
                case "DRIVER_REJECT":
                    Save(SaveAction.RejectByDriver);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been rejected...";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_REJECT";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "DRIVER_REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this booking?";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "DRIVER_FINISH":
                    Save(SaveAction.FinishByDriver);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been finish...";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_FINISH";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "DRIVER_FINISH_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to finish this booking?";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_FINISH";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                    #endregion
            }
        }
    }
}