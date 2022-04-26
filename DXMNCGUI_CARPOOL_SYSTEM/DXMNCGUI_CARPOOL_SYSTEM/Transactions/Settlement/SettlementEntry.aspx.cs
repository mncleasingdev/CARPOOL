using DevExpress.Web;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement
{
    public partial class SettlementEntry : BasePage
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
        protected DataTable myHeaderTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHeaderTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHeaderTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDocNoFormatTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDocNoFormatTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocNoFormatTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myBookingTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myBookingTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBookingTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mySectionTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySectionTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySectionTable" + this.ViewState["_PageID"]] = value; }
        }
        protected Int32 iLineID
        {
            get { isValidLogin(false); return (Int32)HttpContext.Current.Session["BookingiLINE_ID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["BookingiLINE_ID" + this.ViewState["_PageID"]] = value; }
        }
        protected string strDocName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["BookingstrDocName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["BookingstrDocName" + this.ViewState["_PageID"]] = value; }
        }
        protected int igridindex
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["Bookingigridindex" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["Bookingigridindex" + this.ViewState["_PageID"]] = value; }
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
        protected string myStatus
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]] = value; }
        }
        protected string sFieldNameLines
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["ApplicationFieldNameLines" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["ApplicationFieldNameLines" + this.ViewState["_PageID"]] = value; }
        }
        protected DataRow myItemRow
        {
            get { isValidLogin(false); return (DataRow)HttpContext.Current.Session["myItemRow" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myItemRow" + this.ViewState["_PageID"]] = value; }
        }
        protected IContainer components
        {
            get { isValidLogin(false); return (IContainer)HttpContext.Current.Session["components" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["components" + this.ViewState["_PageID"]] = value; }
        }
        protected int iLineIndex
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["BookingiLineIndex" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["BookingiLineIndex" + this.ViewState["_PageID"]] = value; }
        }

        bool isReadOnly = false;

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
                    this.mySettlementDB = SettlementDB.Create(dbsetting, myDBSession);
                    mySettlementEntity = this.mySettlementDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                myDocNoFormatTable = new DataTable();
                myBookingTable = new DataTable();
                mySectionTable = new DataTable();
                strDocName = "";
                iLineIndex = -1;
                igridindex = -1;
                iLineID = -1;
                myds = new DataSet();
                this.mySettlementDB = SettlementDB.Create(myDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((SettlementEntity)HttpContext.Current.Session["mySettlementEntity" + strKey]);
            }
            if (!IsCallback)
            {

            }
        }
        private void SetApplication(SettlementEntity SettlementEntity)
        {
            if (this.mySettlementEntity != SettlementEntity)
            {
                if (SettlementEntity != null)
                {
                    this.mySettlementEntity = SettlementEntity;
                }

                myAction = this.mySettlementEntity.Action;
                myDocType = this.mySettlementEntity.DocumentType;
                myds = mySettlementEntity.myDataSet;
                myStatus = this.mySettlementEntity.Cancelled.ToString();
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                myds.Tables[1].DefaultView.Sort = "DtlKey";
                gvSettlementDetail.DataSource = myDetailTable;
                gvSettlementDetail.DataBind();
                setuplookupedit();
                BindingMaster();
                Accessable();
            }
        }
        private void setuplookupedit()
        {
            if (mySettlementEntity != null)
            {
                DataView dv = new DataView(mySettlementEntity.LoadDocNoFormatTable());
                myDocNoFormatTable = dv.ToTable();

                string sQuery = @"SELECT BO.*, BOA.CarType, BOA.CarLicensePlate, BOD.DriverName, BOD.ActualPickDateTime, BOD.ActualArriveDateTime 
                                    FROM [dbo].[Booking] BO
                                    INNER JOIN [dbo].[BookingAdmin] BOA ON BO.DocKey = BOA.SourceKey
                                    INNER JOIN [dbo].[BookingDriver] BOD ON BO.DocKey = BOD.SourceKey
                                    WHERE Status='FINISH' AND IsSettlement='F' ORDER BY DocDate";
                myBookingTable = myDBSetting.GetDataTable(sQuery, false);
                luBookNo.DataSource = myBookingTable;
                luBookNo.DataBind();
            }
        }
        private void BindingMaster()
        {
            txtDocNo.Value = mySettlementEntity.DocNo;
            txtStatus.Value = mySettlementEntity.Status;
            deDocDate.Value = mySettlementEntity.DocDate;
            txtBookNo.Value = mySettlementEntity.BookNo;
            luBookNo.Value = mySettlementEntity.BookNo;
            deBookingDate.Value = mySettlementEntity.BookDate;
            txtBookingType.Value = mySettlementEntity.BookType;
            txtNumberOfSeat.Value = mySettlementEntity.BookSeatNumber;
            txtBookingBy.Value = mySettlementEntity.BookBy;
            txtCompany.Value = mySettlementEntity.BookCompany;
            txtDepartement.Value = mySettlementEntity.BookDept;
            txtDriver.Value = mySettlementEntity.BookDriver;
            txtCarType.Value = mySettlementEntity.BookCarType;
            txtLicense.Value = mySettlementEntity.BookCarLicense;
            deBookingPickupDate.Value = mySettlementEntity.BookActPickupDateTime;
            deBookingArrivalDate.Value = mySettlementEntity.BookActArrivalDateTime;
            txtBookingPickupLoc.Value = mySettlementEntity.BookPickupLoc;
            txtBookingDestinationLoc.Value = mySettlementEntity.BookDestinationLoc;
            mmBookingPickupAddress.Value = mySettlementEntity.BookPickupAddress;
            mmBookingDestinationAddress.Value = mySettlementEntity.BookDestinantionAddress;
            mmBookingTripDetail.Value = mySettlementEntity.BookTripDetail;
            seTotal.Value = mySettlementEntity.Total;
            txtApprover.Value = mySettlementEntity.Approver;
            chkNeedApproval.CheckState = Convert.ToString(mySettlementEntity.NeedApproval) == "T" ? CheckState.Checked : CheckState.Unchecked;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            bool focusF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            if (myDetailTable.Rows.Count == 0)
            {
                errorF = true;
                if (!focusF)
                {
                    gvSettlementDetail.Focus();
                    focusF = true;
                    strmessageError = "Please add detail settlement, empty detail settlement is not allowed.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            return errorF;
        }
        private void Accessable()
        {
            DateTime mydate = myDBSetting.GetServerTime();
            myStatus = mySettlementEntity.Cancelled.ToString();

            ASPxFormLayout1.FindItemOrGroupByName("lyttxtBookNo").Visible = myAction == DXCAction.New ? false : true;
            ASPxFormLayout1.FindItemOrGroupByName("lytluBookNo").Visible = myAction == DXCAction.New ? true : false;

            txtDocNo.ReadOnly = true;
            txtStatus.ReadOnly = true;
            txtBookNo.ReadOnly = true;
            deBookingDate.ReadOnly = true;
            txtBookingType.ReadOnly = true;
            txtNumberOfSeat.ReadOnly = true;
            txtBookingBy.ReadOnly = true;
            txtCompany.ReadOnly = true;
            txtDepartement.ReadOnly = true;
            txtDriver.ReadOnly = true;
            txtCarType.ReadOnly = true;
            txtLicense.ReadOnly = true;
            deBookingPickupDate.ReadOnly = true;
            deBookingArrivalDate.ReadOnly = true;
            txtBookingPickupLoc.ReadOnly = true;
            txtBookingDestinationLoc.ReadOnly = true;
            mmBookingPickupAddress.ReadOnly = true;
            mmBookingDestinationAddress.ReadOnly = true;
            mmBookingTripDetail.ReadOnly = true;
            seTotal.ReadOnly = true;
            chkNeedApproval.ReadOnly = true;
            txtApprover.ReadOnly = true;

            if (myAction == DXCAction.View)
            {
                deDocDate.ReadOnly = true;

                gvSettlementDetail.Columns["ClmnCommand"].Visible = false;
                gvSettlementDetail.Columns["ClmnCommand2"].Visible = true;
                gvSettlementDetail.Columns["colNo"].Visible = true;
            }

            #region Control Color
            btnSubmit.ForeColor = btnSubmit.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnCancel.ForeColor = btnCancel.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            deDocDate.BackColor = deDocDate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDocNo.BackColor = txtDocNo.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtStatus.BackColor = txtStatus.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtBookNo.BackColor = txtDocNo.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deBookingDate.BackColor = deBookingDate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtBookingType.BackColor = txtBookingType.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtNumberOfSeat.BackColor = txtNumberOfSeat.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtBookingBy.BackColor = txtBookingBy.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtCompany.BackColor = txtCompany.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDepartement.BackColor = txtDepartement.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDriver.BackColor = txtDriver.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtCarType.BackColor = txtCarType.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtLicense.BackColor = txtLicense.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deBookingPickupDate.BackColor = deBookingPickupDate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deBookingArrivalDate.BackColor = deBookingArrivalDate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtBookingPickupLoc.BackColor = txtBookingPickupLoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtBookingDestinationLoc.BackColor = txtBookingDestinationLoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmBookingPickupAddress.BackColor = mmBookingPickupAddress.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmBookingDestinationAddress.BackColor = mmBookingDestinationAddress.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmBookingTripDetail.BackColor = mmBookingTripDetail.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            seTotal.BackColor = seTotal.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            chkNeedApproval.BackColor = chkNeedApproval.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtApprover.BackColor = txtApprover.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            #endregion
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            bool IsApproval = false;
            DataTable dtCopyApp = new DataTable();

            gvSettlementDetail.UpdateEdit();
            mySettlementEntity.DocNo = txtDocNo.Value;
            mySettlementEntity.Status = txtStatus.Value;
            mySettlementEntity.DocDate = deDocDate.Value;
            mySettlementEntity.BookNo = luBookNo.Value;
            mySettlementEntity.BookDate = deBookingDate.Value;
            mySettlementEntity.BookType = txtBookingType.Value;
            mySettlementEntity.BookSeatNumber = txtNumberOfSeat.Value;
            mySettlementEntity.BookCompany = txtCompany.Value;
            mySettlementEntity.BookDept = txtDepartement.Value;
            mySettlementEntity.BookBy = txtBookingBy.Value;
            mySettlementEntity.BookDriver = txtDriver.Value;
            mySettlementEntity.BookCarType = txtCarType.Value;
            mySettlementEntity.BookCarLicense = txtLicense.Value;
            mySettlementEntity.BookActPickupDateTime = deBookingPickupDate.Value == null ? DBNull.Value : deBookingPickupDate.Value;
            mySettlementEntity.BookActArrivalDateTime = deBookingArrivalDate.Value == null ? DBNull.Value : deBookingArrivalDate.Value;
            mySettlementEntity.BookPickupLoc = txtBookingPickupLoc.Value;
            mySettlementEntity.BookDestinationLoc = txtBookingDestinationLoc.Value;
            mySettlementEntity.BookPickupAddress = mmBookingPickupAddress.Value;
            mySettlementEntity.BookDestinantionAddress = mmBookingDestinationAddress.Value;
            mySettlementEntity.BookTripDetail = mmBookingTripDetail.Value;
            mySettlementEntity.Total = seTotal.Value == null ? 0 : seTotal.Value;
            mySettlementEntity.Approver = txtApprover.Value;
            mySettlementEntity.NeedApproval = chkNeedApproval.CheckState == CheckState.Checked ? "T" : "F";

            if (myAction == DXCAction.New)
            {
                mySettlementEntity.CreatedBy = this.UserName;
                mySettlementEntity.CreatedDateTime = myDBSetting.GetServerTime();
                mySettlementEntity.LastModifiedDateTime = myDBSetting.GetServerTime();
            }
            if (Convert.ToString(mySettlementEntity.NeedApproval) == "T")
            {
                IsApproval = true;
            }
            mySettlementEntity.Save(UserID, UserName, "ST", saveAction, IsApproval);
            return bSave;
        }

        protected bool bookNoIsExist(string BookNo)
        {
            bool rtBool = false;

            string ssql = "";


            return rtBool;
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            urlsave = "~/Transactions/Settlement/SettlementList.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("DocKey", mySettlementEntity.DocKey.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                #region ACTION BY ADMIN
                case "SUBMIT":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Settlement has been submit...";
                    cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "SUBMITCONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to submit this Settlement?";
                    cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "CANCEL":
                    Save(SaveAction.Cancel);
                    cplMain.JSProperties["cpAlertMessage"] = "Settlement has been cancelled...";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "CANCEL_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to cancel this Settlement?";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
            }
        }
        protected void luBookNo_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myBookingTable;
        }
        protected void gvSettlementDetail_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Qty" || e.Column.FieldName == "UnitPrice")
            {
                (e.Editor as ASPxTextBox).AutoPostBack = false;
            }
        }
        protected void gvSettlementDetail_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void gvSettlementDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvSettlementDetail_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID != "ctmbtnView") return;
            isReadOnly = true;
            gvSettlementDetail.StartEdit(e.VisibleIndex);
        }
        protected void gvSettlementDetail_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            e.Editor.ReadOnly = isReadOnly;
        }
        protected void gvSettlementDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myds.Tables[1];
        }
        protected void gvSettlementDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Remark1"] = DBNull.Value;
            e.NewValues["Remark2"] = DBNull.Value;
            e.NewValues["Remark3"] = DBNull.Value;
            e.NewValues["Remark4"] = DBNull.Value;
        }
        protected void gvSettlementDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["ItemCode"] == null) throw new Exception("Item code is null are not allowed.");
            if (e.NewValues["Qty"] == null) throw new Exception("Column 'Qty' is mandatory.");
            if (e.NewValues["UnitPrice"] == null) throw new Exception("Column 'UnitPrice' is mandatory.");

            if (StrErrorMsg == "")
            {
                DataRow[] ValidLinesRows = myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                myDetailTable.Rows.Add(
                    mySettlementEntity.Settlementcommand.DtlKeyUniqueKey(), 
                    mySettlementEntity.DocKey, 
                    seq, 
                    e.NewValues["ItemCode"], 
                    e.NewValues["ItemDesc"],
                    e.NewValues["Note"],
                    e.NewValues["Remark1"], 
                    e.NewValues["Remark2"],
                    null,
                    null,
                    e.NewValues["Image"],
                    e.NewValues["Qty"], 
                    e.NewValues["UnitPrice"], 
                    e.NewValues["SubTotal"]);

                decimal vNetAmountTotal = decimal.Parse(seTotal.Value.ToString());
                vNetAmountTotal += decimal.Parse(e.NewValues["SubTotal"].ToString());
                gvSettlementDetail.JSProperties["cpCmd"] = "INSERT";
                gvSettlementDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvSettlementDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["ItemCode"] == null) throw new Exception("Item code is null are not allowed.");
            if (e.NewValues["Qty"] == null) throw new Exception("Column 'Qty' is mandatory.");
            if (e.NewValues["UnitPrice"] == null) throw new Exception("Column 'UnitPrice' is mandatory.");

            if (StrErrorMsg == "")
            {
                gvSettlementDetail.JSProperties["cpCmd"] = "UPDATE";
                int editingRowVisibleIndex = gvSettlementDetail.EditingRowVisibleIndex;
                int id = (int)gvSettlementDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");
                DataRow dr = myDetailTable.Rows.Find(id);

                dr["ItemCode"] = e.NewValues["ItemCode"];
                dr["ItemDesc"] = e.NewValues["ItemDesc"];
                dr["Note"] = e.NewValues["Note"];
                dr["Remark1"] = e.NewValues["Remark1"];
                dr["Remark2"] = e.NewValues["Remark2"];
                dr["Image"] = e.NewValues["Image"];
                dr["Qty"] = e.NewValues["Qty"];
                dr["UnitPrice"] = e.NewValues["UnitPrice"];
                dr["SubTotal"] = e.NewValues["SubTotal"];

                decimal vNetAmountTotal = decimal.Parse(seTotal.Value.ToString());
                vNetAmountTotal -= decimal.Parse(e.OldValues["SubTotal"].ToString());
                vNetAmountTotal += decimal.Parse(e.NewValues["SubTotal"].ToString());
                gvSettlementDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvSettlementDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            gvSettlementDetail.JSProperties["cpCmd"] = "DELETE";
            int id = (int)e.Keys["DtlKey"];
            DataRow dr = myDetailTable.Rows.Find(id);

            decimal vNetAmountTotal = decimal.Parse(seTotal.Value.ToString());
            vNetAmountTotal -= decimal.Parse(dr["SubTotal"].ToString());
            gvSettlementDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

            myDetailTable.Rows.Remove(dr);

            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Settlement/SettlementList.aspx");
        }
    }
}