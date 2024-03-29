﻿using DevExpress.Web;
using DevExpress.XtraMap;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public partial class BookingEntry : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(false); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + this.ViewState["_PageID"]] = value; }
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
        protected DataTable mySectionTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySectionTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySectionTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myBookTypeTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myBookTypeTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBookTypeTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDepartmentTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDepartmentTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDepartmentTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalGATable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalGATable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalGATable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myCarTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myCarTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCarTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myRoleTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myRoleTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myRoleTable" + this.ViewState["_PageID"]] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                myLocalDBSetting = localdbsetting;
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                myDocNoFormatTable = new DataTable();
                mySectionTable = new DataTable();
                myBookTypeTable = new DataTable();
                myDepartmentTable = new DataTable();
                myApprovalTable = new DataTable();
                myApprovalGATable = new DataTable();
                myCarTable = new DataTable();
                myRoleTable = new DataTable();
                strDocName = "";
                iLineIndex = -1;
                igridindex = -1;
                iLineID = -1;
                myds = new DataSet();
                this.myBookingDB = BookingDB.Create(myDBSetting, myDBSession, myLocalDBSetting);
                strKey = Request.QueryString["Key"];

                if (this.Request.QueryString["Dockey"] != null && this.Request.QueryString["Action"] == "Approval")
                {
                    this.myBookingDB = BookingDB.Create(dbsetting, myDBSession, myLocalDBSetting);
                    myBookingEntity = this.myBookingDB.Approve(Convert.ToInt32(this.Request.QueryString["DocKey"]));
                    strKey = this.Request.QueryString["DocKey"].ToString();
                   
                }
                if (this.Request.QueryString["Dockey"] != null && this.Request.QueryString["Action"] == "OnSchedule")
                {
                    this.myBookingDB = BookingDB.Create(dbsetting, myDBSession, myLocalDBSetting);
                    myBookingEntity = this.myBookingDB.View(Convert.ToInt32(this.Request.QueryString["DocKey"]));
                    strKey = this.Request.QueryString["DocKey"].ToString();

                }
                if (this.Request.QueryString["Dockey"] != null && this.Request.QueryString["Action"] == "ChangeCar")
                {
                    this.myBookingDB = BookingDB.Create(dbsetting, myDBSession, myLocalDBSetting);
                    myBookingEntity = this.myBookingDB.View(Convert.ToInt32(this.Request.QueryString["DocKey"]));
                    strKey = this.Request.QueryString["DocKey"].ToString();

                }
                SetApplication((BookingEntity)HttpContext.Current.Session["myBookingEntity" + strKey]);
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
                myds = myBookingEntity.myDataSet;
                myStatus = this.myBookingEntity.Status.ToString();
                myHeaderTable = myds.Tables[0];              
                setuplookupedit();
                BindingMaster();
                Accessable();             
            }
        }
        private void setuplookupedit()
        {
            if (myBookingEntity != null)
            {
                DataView dv = new DataView(myBookingEntity.LoadDocNoFormatTable());
                myDocNoFormatTable = dv.ToTable();

                myDetailTable = myLocalDBSetting.GetDataTable("SELECT DtlKey,Dockey,Seq,NIK,Name,Jabatan,Email FROM [dbo].[BookingDetail] where DtlKey = 0 ORDER BY DtlKey", false);
                gvPersonDetail.DataSource = myDetailTable;
                gvPersonDetail.DataBind();

                myDepartmentTable = myDBSetting.GetDataTable("SELECT * FROM [dbo].[PC_DEPT] ORDER BY CODE", false);
                cbDepartment.DataSource = myDepartmentTable;
                cbDepartment.DataBind();

                myCarTable = myLocalDBSetting.GetDataTable("select * from MasterCar where IsActive='T'", false);
                luCarType.DataSource = myCarTable;
                luCarType.DataBind();

                myApprovalGATable = myLocalDBSetting.GetDataTable("select top 1 * from BookingApprovalList where DtlKey = 0 ORDER BY Seq ASC", false);

                myApprovalTable = myLocalDBSetting.GetDataTable("select top 1 * from BookingApprovalList where DtlKey = 0 ORDER BY Seq ASC", false);
                gvApproval.DataSource = myApprovalTable;
                gvApproval.DataBind();
            }
        }

      
        private void BindingMaster()
        {
            txtEmployee.Value = myBookingEntity.EmployeeName;
            txtComapany.Value = myBookingEntity.EmployeeCompanyName;
            txtStatus.Value = myBookingEntity.Status;
            txtDocNo.Value = myBookingEntity.DocNo.ToString();
            cbDepartment.Value = myBookingEntity.Department.ToString();
            txtHp.Value = myBookingEntity.Hp.ToString();
            deDocDate.Value = myBookingEntity.DocDate;
            deReqPickupTime.Value = myBookingEntity.RequestStartTime;
            deReqArrivalTime.Value = myBookingEntity.RequestFinishTime;
            txtPickupLoc.Value = myBookingEntity.RequestPickLoc;
            txtDestinationLoc.Value = myBookingEntity.RequestDestLoc;
            mmPickupAddress.Value = myBookingEntity.RequestPickAddress;
            mmDestinationAddress.Value = myBookingEntity.RequestDestAddress;
            mmTripDetail.Value = myBookingEntity.TripDetails;

            luCarType.Value = myBookingEntity.AdminCarType;
            txtLicensePlate.Value = myBookingEntity.AdminCarLicensePlate;
            if (this.Request.QueryString["Action"] == "OnSchedule")
            {
                txtLastKM.Value = myBookingEntity.AdminLastKilometer;
            }
            else
            {
                txtLastKM.Value = myBookingEntity.AdminCurrentKilometer;
            }
            mmAdminRemark.Value = myBookingEntity.AdminRemark;

            if (myAction == DXCAction.View || myAction == DXCAction.Approve)
            {
                myDetailTable = myLocalDBSetting.GetDataTable("SELECT DtlKey,Dockey,Seq,NIK,Name,Jabatan,Email FROM [dbo].[BookingDetail] where Dockey=? ORDER BY Seq ASC", false, myBookingEntity.DocKey);
                gvPersonDetail.DataSource = myDetailTable;
                gvPersonDetail.DataBind();

                myApprovalTable = myLocalDBSetting.GetDataTable("select * from BookingApprovalList where Dockey = ? ORDER BY Seq ASC", false, myBookingEntity.DocKey);
                gvApproval.DataSource = myApprovalTable;
                gvApproval.DataBind();
            }
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
                    gvPersonDetail.Focus();
                    focusF = true;
                    strmessageError = "Please add detail person, empty detail person is not allowed.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            #region ADMIN
            if (accessright.IsAccessibleByUserID(Email, ""))
            {
                if (myAction != DXCAction.New)
                {
                    if (luCarType.Value == null)
                    {
                        errorF = true;
                        luCarType.IsValid = false;
                        luCarType.ErrorText = "Car type can't be empty.";
                        if (!focusF)
                        {
                            luCarType.Focus();
                            focusF = true;
                            strmessageError = "Car Type, can't be empty.";
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                            cplMain.JSProperties["cplActiveTabIndex"] = 1;
                        }
                    }
                }
            }
            #endregion
            return errorF;
        }
        private void Accessable()
        { 
            DateTime mydate = myLocalDBSetting.GetServerTime();
            myStatus = myBookingEntity.Status.ToString();

            txtEmployee.ReadOnly = true;
            txtComapany.ReadOnly = true;
            txtStatus.ReadOnly = true;
            txtDocNo.ReadOnly = true;
            txtLastKM.ReadOnly = true;
            txtHp.ReadOnly = true;
            deDocDate.ReadOnly = false;

            if ((!accessright.IsAccessibleByUserID(Email, "IS_GA")) || (accessright.IsAccessibleByUserID(Email, "IS_ADMIN")))
            {
                ASPxFormLayout1.FindItemOrGroupByName("LayoutGroupAdminEntry").Visible = false;
                luCarType.ClientVisible = true;
                txtLicensePlate.ClientVisible = true;
                mmAdminRemark.ClientVisible = true;
            }

            if (myAction == DXCAction.View)
            {
                ASPxFormLayout1.FindItemOrGroupByName("LayoutGroupAdminEntry").Visible = true;
                luCarType.ClientVisible = true;
                mmAdminRemark.ClientVisible = true;
                txtLastKM.ClientVisible = true;
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                deDocDate.ClientEnabled = false;
                cbDepartment.ClientEnabled = false;
                deReqPickupTime.ClientEnabled = false;
                deReqArrivalTime.ClientEnabled = false;
                txtPickupLoc.ClientEnabled = false;
                txtDestinationLoc.ClientEnabled = false;
                mmPickupAddress.ClientEnabled = false;
                mmDestinationAddress.ClientEnabled = false;
                mmTripDetail.ClientEnabled = false;
                luCarType.ClientEnabled = false;
                mmAdminRemark.ClientEnabled = false;
                txtLastKM.ClientEnabled = false;
                gvPersonDetail.Columns["ClmnCommand"].Visible = false;
                gvApproval.Columns["ClmnCommand"].Visible = false;

                if (this.Request.QueryString["Action"] == "OnSchedule")
                {
                    btnFinish.ClientVisible = true;
                    btnAdminApprove.ClientVisible = false;
                    btnAdminOnHold.ClientVisible = false;
                    btnAdminReject.ClientVisible = false;
                    txtLastKM.ReadOnly = false;
                    mmAdminRemark.ReadOnly = false;
                    mmAdminRemark.ClientEnabled = true;
                    txtLastKM.ClientEnabled = true;
                }

                if (this.Request.QueryString["Action"] == "ChangeCar")
                {
                    btnFinish.ClientVisible = false;
                    btnAdminApprove.ClientVisible = true;
                    btnAdminOnHold.ClientVisible = false;
                    btnAdminReject.ClientVisible = false;
                    txtLastKM.ClientEnabled = true;
                    mmAdminRemark.ClientEnabled = true;                   
                    luCarType.ClientEnabled = true;                   
                }
            }

            

            if (myAction == DXCAction.Approve)
            {
                btnApprove.ClientVisible = true;
                btnReject.ClientVisible = true;
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                deDocDate.ClientEnabled = false;
                cbDepartment.ClientEnabled = false;
                deReqPickupTime.ClientEnabled = false;
                deReqArrivalTime.ClientEnabled = false;
                txtPickupLoc.ClientEnabled = false;
                txtDestinationLoc.ClientEnabled = false;
                mmPickupAddress.ClientEnabled = false;
                mmDestinationAddress.ClientEnabled = false;
                mmTripDetail.ClientEnabled = false;
                luCarType.ClientEnabled = false;
                gvPersonDetail.Columns["ClmnCommand"].Visible = false;
                gvApproval.Columns["ClmnCommand"].Visible = false;

                if ((accessright.IsAccessibleByUserID(Email, "IS_GA")))
                {
                    btnAdminApprove.ClientVisible = true;
                    btnAdminReject.ClientVisible = true;
                    btnApprove.ClientVisible = false;
                    btnReject.ClientVisible = false;
                    luCarType.ClientEnabled = true;
                    txtLastKM.ClientEnabled = true;
                    mmAdminRemark.ClientEnabled = true;
                }
            }
            #region Control Color
            btnSubmit.ForeColor = btnSubmit.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnCancel.ForeColor = btnCancel.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            //btnAdminApprove.ForeColor = btnAdminApprove.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            //btnAdminOnHold.ForeColor = btnAdminOnHold.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            //btnAdminReject.ForeColor = btnAdminReject.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            //btnDriverPickUp.ForeColor = btnDriverPRickUp.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            //btnDriverFinish.ForeColor = btnDriverFinish.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            //btnDriverReject.ForeColor = btnDriverReject.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            txtEmployee.BackColor = txtEmployee.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtComapany.BackColor = txtComapany.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtStatus.BackColor = txtStatus.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDocNo.BackColor = txtDocNo.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            cbDepartment.BackColor = cbDepartment.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtHp.BackColor = txtHp.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deDocDate.BackColor = deDocDate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //cbBookType.BackColor = cbBookType.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //cbNumberSeat.BackColor = cbNumberSeat.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deReqPickupTime.BackColor = deReqPickupTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deReqArrivalTime.BackColor = deReqArrivalTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtPickupLoc.BackColor = txtPickupLoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDestinationLoc.BackColor = txtDestinationLoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmPickupAddress.BackColor = mmPickupAddress.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmDestinationAddress.BackColor = mmDestinationAddress.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmTripDetail.BackColor = mmTripDetail.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;

            //cbDriver.BackColor = cbDriver.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            luCarType.BackColor = luCarType.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtLicensePlate.BackColor = txtLicensePlate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtLastKM.BackColor = txtLastKM.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //deEstPickupTime.BackColor = deEstPickupTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //deEstArrivalTime.BackColor = deEstArrivalTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmAdminRemark.BackColor = mmAdminRemark.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;

            //txtActDriverName.BackColor = txtActDriverName.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //deActPickupTime.BackColor = deActPickupTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //deActArrivalTime.BackColor = deActArrivalTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            //mmActRemark.BackColor = mmActRemark.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            #endregion
        }
        protected void gvApproval_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalTable;
        }
        protected void gvApproval_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            DataRow myrow = gvApproval.GetDataRow(gvApproval.FocusedRowIndex);

            if (e.NewValues["Nama"] == null) throw new Exception("Column 'Nama' is mandatory.");
            if (e.NewValues["NIK"] == null) throw new Exception("Column 'Nik' is mandatory.");
            if (e.NewValues["Jabatan"] == null) throw new Exception("Column 'Jabatan' is mandatory.");
            if (myrow != null)
            {
                if (myrow["NIK"].ToString() == Convert.ToString((e.NewValues["NIK"]))) throw new Exception("User approval is exists.");
            }

            if (StrErrorMsg == "")
            {
                gvApproval.JSProperties["cpCmd"] = "INSERT";

                int i = gvApproval.VisibleRowCount;

                string dtlNIK = e.NewValues["NIK"].ToString();
                string dtlNama = e.NewValues["Nama"].ToString();
                string dtlJabatan = e.NewValues["Jabatan"].ToString();
                string dtlEmail = e.NewValues["Email"].ToString();

                if (myAction == DXCAction.New)
                {
                    myApprovalTable.Rows.Add(i, myBookingEntity.DocKey, "Approval Baru", i, dtlNIK, dtlNama, dtlJabatan, "F", DBNull.Value, DBNull.Value, DBNull.Value, dtlEmail, DBNull.Value);
                }

                if (myAction == DXCAction.Edit)
                {
                    myApprovalTable.Rows.Add(i, myBookingEntity.DocKey, "Approval Edit", i, dtlNIK, dtlNama, dtlJabatan, "F", DBNull.Value, DBNull.Value, DBNull.Value, dtlEmail, DBNull.Value);
                }
                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }

        protected void gvApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            DataRow myrow = gvApproval.GetDataRow(gvApproval.FocusedRowIndex);

            if (e.NewValues["Nama"] == null) throw new Exception("Column 'Nama' is mandatory.");
            if (e.NewValues["NIK"] == null) throw new Exception("Column 'Nik' is mandatory.");
            if (e.NewValues["Jabatan"] == null) throw new Exception("Column 'Jabatan' is mandatory.");
            if (Convert.ToString(e.NewValues["NIK"]) == UserID) throw new Exception("User approval and user login can't be same.");
            if (myrow != null)
            {

                if ((myrow["Jabatan"].ToString() == Convert.ToString((e.NewValues["Jabatan"]))))
                {
                    if (myrow["NIK"].ToString() == Convert.ToString((e.NewValues["NIK"]))) throw new Exception("User approval is exists.");
                }
            }

            if (StrErrorMsg == "")
            {
                if (myAction == DXCAction.New || myAction == DXCAction.Edit)
                {
                    gvApproval.JSProperties["cpCmd"] = "UPDATE";
                    int editingRowVisibleIndex = gvApproval.EditingRowVisibleIndex;
                    int id = (int)gvApproval.GetRowValues(editingRowVisibleIndex, "DtlKey");

                    var searchExpression = "DtlKey = " + id.ToString();
                    DataRow[] foundRow = myApprovalTable.Select(searchExpression);
                    foreach (DataRow dr in foundRow)
                    {
                        dr["NIK"] = Convert.ToString(e.NewValues["NIK"]);
                        dr["Nama"] = Convert.ToString(e.NewValues["Nama"]);
                        dr["Jabatan"] = Convert.ToString(e.NewValues["Jabatan"]);
                        dr["Email"] = Convert.ToString(e.NewValues["Email"]);
                    }

                    ASPxGridView grid = sender as ASPxGridView;
                    grid.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        protected void gvApproval_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (myAction == DXCAction.New || myAction == DXCAction.Edit)
            {
                gvApproval.JSProperties["cpCmd"] = "DELETE";
                int id = (int)e.Keys["DtlKey"];

                var searchExpression = "DtlKey = " + id.ToString();
                DataRow[] foundRow = myApprovalTable.Select(searchExpression);
                foreach (DataRow dr in foundRow)
                {
                    myApprovalTable.Rows.Remove(dr);
                }

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }

        protected void gvApproval_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvApproval_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }
        protected void gvApproval_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }

        protected void gvApproval_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Nama")
            {
                isValidLogin(false);
                if (Page.IsCallback)
                {
                    DataTable myitem = new DataTable();
                    myitem = myDBSetting.GetDataTable(@"select top 1
                                                            a.HEAD AS NIK, 
                                                            a.HEAD_DESCS AS Nama,
                                                            c.USERGROUPDESC AS Jabatan,
                                                            d.EMAIL AS Email
                                                        from SYS_TBLEMPLOYEE a
                                                        left join MASTER_USER_COMPANY_GROUP b on a.HEAD = b.USER_ID
                                                        left join MASTER_GROUP c on b.GROUP_CODE = c.USERGROUP
                                                        left join SYS_TBLEMPLOYEE d on a.HEAD = d.CODE
                                                        where a.CODE = ? and d.ISACTIVE = 1", false,Email);
                    ASPxComboBox cmb = (ASPxComboBox)e.Editor;
                    cmb.DataSource = myitem;
                    cmb.DataBindItems();
                }
            }
        }

        private void insertApproval(DataRow dataRow, string TypeApproval, SaveAction saveaction)
        {
            string ssql = @"insert into BookingApprovalList ([DocKey],[TypeApproval],[Seq],[NIK],[Nama],[Jabatan],[IsDecision],[DecisionState],[DecisionDate],[DecisionNote],[Email])
                        values(@DocKey,@TypeApproval,@Seq,@NIK,@Nama,@Jabatan,@IsDecision,@DecisionState,@DecisionDate,@DecisionNote,@Email)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();
            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
            sqlParameter1.Value = myBookingEntity.DocKey;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TypeApproval", SqlDbType.VarChar);
            sqlParameter2.Value = TypeApproval;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
            sqlParameter3.Value = dataRow.Field<int>("Seq");
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@NIK", SqlDbType.VarChar);
            sqlParameter4.Value = dataRow.Field<string>("NIK");
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Nama", SqlDbType.VarChar);
            sqlParameter5.Value = dataRow.Field<string>("Nama");
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Jabatan", SqlDbType.VarChar);
            sqlParameter6.Value = dataRow.Field<string>("Jabatan");
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@IsDecision", SqlDbType.VarChar);
            if ((saveaction == SaveAction.ApproveByAdmin) || (saveaction == SaveAction.RejectByAdmin))
            {
                sqlParameter7.Value = dataRow.Field<string>("IsDecision");
            }
            else
            {
                sqlParameter7.Value = "F";
            }
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@DecisionState", SqlDbType.VarChar);
            sqlParameter8.Value = dataRow.Field<string>("DecisionState") == null ? "" : dataRow.Field<string>("DecisionState");
            sqlParameter8.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@DecisionDate", SqlDbType.DateTime);
            if ((saveaction == SaveAction.ApproveByAdmin) || (saveaction == SaveAction.RejectByAdmin))
            {
                sqlParameter9.Value = myLocalDBSetting.GetServerTime();
            }
            else
            {
                sqlParameter9.Value = DBNull.Value;
            }
            sqlParameter9.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@DecisionNote", SqlDbType.VarChar);
            sqlParameter10.Value = dataRow.Field<string>("DecisionNote") == null ? "" : dataRow.Field<string>("DecisionNote");
            sqlParameter10.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar);
            sqlParameter11.Value = dataRow.Field<string>("Email") == null ? "" : dataRow.Field<string>("Email");
            sqlParameter11.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@DecisionCode", SqlDbType.VarChar);
            if (saveaction == SaveAction.ApproveByAdmin)
            {
                sqlParameter12.Value = 1;
            }
            else if (saveaction == SaveAction.RejectByAdmin)
            {
                sqlParameter12.Value = 2;
            }
            else
            {
                sqlParameter12.Value = DBNull.Value;
            }
            sqlParameter12.Direction = ParameterDirection.Input;
            sqlCommand.ExecuteNonQuery();
            myconn.Close();
        }

        private void insertPersonDetail(DataRow dataRow)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[BookingDetail] (DtlKey, DocKey, Seq, Name, Gender, Status, 
                                                         Remark1, Remark2, Remark3, Remark4, NIK, Jabatan, Email) 
                                                         VALUES (@DtlKey, @DocKey, @Seq, @Name, @Gender ,@Status, @Remark1, @Remark2, 
                                                         @Remark3, @Remark4, @NIK, @Jabatan, @Email)");
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DtlKey", SqlDbType.Int);
                sqlParameter1.Value = dataRow.Field<int>("DtlKey");
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter2.Value = Convert.ToInt32(dataRow["Dockey"]);//dataRow.Field<int>("DocKey");
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
                sqlParameter3.Value = Convert.ToInt32(dataRow["Seq"]);//dataRow.Field<int>("Seq");
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                sqlParameter4.Value = dataRow.Field<string>("Name");
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 1);
                sqlParameter5.Value = "";// dataRow.Field<string>("Gender");
                sqlParameter5.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
                sqlParameter6.Value = "";//dataRow.Field<string>("Status");
                sqlParameter6.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Remark1", SqlDbType.NVarChar);
                sqlParameter7.Value = "";//varRemark1;
                sqlParameter7.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Remark2", SqlDbType.NVarChar);
                sqlParameter8.Value = "";// varRemark2;
                sqlParameter8.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@Remark3", SqlDbType.NVarChar);
                sqlParameter9.Value = "";// varRemark3;
                sqlParameter9.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@Remark4", SqlDbType.NVarChar);
                sqlParameter10.Value = "";// varRemark3;
                sqlParameter10.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@NIK", SqlDbType.NVarChar);
                sqlParameter11.Value = dataRow.Field<string>("NIK");
                sqlParameter11.Direction = ParameterDirection.Input;
                SqlParameter sqlParamete12 = sqlCommand.Parameters.Add("@Jabatan", SqlDbType.NVarChar);
                sqlParamete12.Value = dataRow.Field<string>("Jabatan");
                sqlParamete12.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar);
                sqlParameter13.Value = dataRow.Field<string>("Email");
                sqlParameter13.Direction = ParameterDirection.Input;

                sqlCommand.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }

        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            DataTable dtCopyApp = new DataTable();

            gvPersonDetail.UpdateEdit();
            myBookingEntity.DocNo = txtDocNo.Value;
            myBookingEntity.DocDate = deDocDate.Value;
            myBookingEntity.Department = cbDepartment.Value;
            myBookingEntity.Hp = txtHp.Value;
            myBookingEntity.EmployeeName = txtEmployee.Value;
            myBookingEntity.EmployeeCompanyName = txtComapany.Value;
            myBookingEntity.Status = txtStatus.Value;
            myBookingEntity.RequestStartTime = deReqPickupTime.Value;
            myBookingEntity.RequestFinishTime = deReqArrivalTime.Value;
            myBookingEntity.RequestPickLoc = txtPickupLoc.Value;
            myBookingEntity.RequestDestLoc = txtDestinationLoc.Value;
            myBookingEntity.RequestPickAddress = mmPickupAddress.Value;
            myBookingEntity.RequestDestAddress = mmDestinationAddress.Value;
            myBookingEntity.TripDetails = mmTripDetail.Value;
            
            myBookingEntity.AdminCarType = luCarType.Value;
            myBookingEntity.AdminCarLicensePlate = txtLicensePlate.Value;
            if (saveAction == SaveAction.ApproveByAdmin)
            {
                myBookingEntity.AdminLastKilometer = txtLastKM.Value;
            }
            else if (saveAction == SaveAction.FinishByDriver)
            {
                myBookingEntity.AdminCurrentKilometer = txtLastKM.Value;
            }

            myBookingEntity.AdminRemark = mmAdminRemark.Value;

            if (saveAction == SaveAction.ApproveByAdmin)
            {
                string sEmail = "";
                if (Email == "1610024")
                {
                    sEmail = "yeffi.hendrisco@mncgroup.com";
                }
                else if(Email == "97030053")
                {
                    sEmail = "amin.gusdarri@mncgroup.com";
                }
                myApprovalGATable.Rows.Add(2, myBookingEntity.DocKey, "Pengajuan Approval", 1, Email, UserName, "HO GENERAL AFFAIR MANAGER", "T", "APPROVE", DBNull.Value, mmAdminRemark.Value, sEmail, DBNull.Value);
                foreach (DataRow drApproveGA in myApprovalGATable.Rows)
                {
                    insertApproval(drApproveGA, "Pengajuan Approval", SaveAction.ApproveByAdmin);
                }
            }

            if (saveAction == SaveAction.RejectByAdmin)
            {
                string sEmail = "";
                if (Email == "1610024")
                {
                    sEmail = "yeffi.hendrisco@mncgroup.com";
                }
                else if (Email == "97030053")
                {
                    sEmail = "amin.gusdarri@mncgroup.com";
                }
                myApprovalGATable.Rows.Add(2, myBookingEntity.DocKey, "Pengajuan Approval", 1, Email, UserName, "HO GENERAL AFFAIR MANAGER", "T", "REJECT", DBNull.Value, mmAdminRemark.Value, sEmail, DBNull.Value);
                foreach (DataRow drApproveGA in myApprovalGATable.Rows)
                {
                    insertApproval(drApproveGA, "Pengajuan Approval", SaveAction.RejectByAdmin);
                }
            }

            if (saveAction == SaveAction.Save)
            {
                foreach (DataRow dr in myDetailTable.Rows)
                {
                    insertPersonDetail(dr);

                }

                foreach (DataRow drApprove in myApprovalTable.Rows)
                {
                    insertApproval(drApprove, "Pengajuan Approval", SaveAction.Save);
                    myBookingEntity.Approver = drApprove["NIK"].ToString();
                }
            }

            if (myAction == DXCAction.New)
            {
                myBookingEntity.CreatedBy = txtEmployee.Value;
                myBookingEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
                myBookingEntity.LastModifiedDateTime = myLocalDBSetting.GetServerTime();
            }
            myBookingEntity.Save(Email, UserName, "BK", saveAction, myBookingEntity.Status.ToString(), Convert.ToString(myBookingEntity.Approver));



            return bSave;
        }

        private string cekKilometerMobil()
        {
            string lastkilometer = "";
            SqlConnection connection = new SqlConnection(this.myLocalDBSetting.ConnectionString);
            try
            {
                connection.Open();
                object obj = null;

                obj = myLocalDBSetting.ExecuteScalar("select lastkilometer from bookingadmin where CarLicensePlate=?", myBookingEntity.AdminCarLicensePlate);
                if (obj != null && obj != DBNull.Value)
                {
                    lastkilometer = obj.ToString();
                }
            }
            catch (SqlException ex)
            {
                DataError.HandleSqlException(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            return lastkilometer;

        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            urlsave = "~/Transactions/Booking/BookingList.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("DocKey", myBookingEntity.DocKey.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting;
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                #region ACTION BY USER
                case "SUBMIT":
                    if (myApprovalTable.Rows.Count == 0)
                    {
                        cplMain.JSProperties["cpAlertMessage"] = "Please fill in approval...";
                    }
                    else
                    { 
                    Save(SaveAction.Save);
                        cplMain.JSProperties["cpAlertMessage"] = "Transaction has been submit...";
                        cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    }
                    break;
                case "SUBMITCONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to submit this book?";
                    cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "APPROVE":
                    SaveApprove(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "This book has been approved..";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");
                    break;
                case "APPROVECONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "Are you sure want to approve this book trip?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    if (ErrorInField(out strmessageError, SaveAction.Approve))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "REJECT":
                    SaveApprove(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "This book has been rejected..";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT_CONFIRM";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");
                    break;
                case "REJECTCONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "Are you sure want to reject this book trip?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    break;
                case "CANCEL":
                    Save(SaveAction.Cancel);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been cancelled...";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");
                    break;
                case "CANCEL_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to cancel this book?";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
                #region ACTION BY ADMIN
                case "ADMIN_PENDING":
                    Save(SaveAction.HoldByAdmin);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been holding...";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_PENDING";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");
                    break;
                case "ADMIN_PENDING_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to hold this book?";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_PENDING";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "ADMIN_REJECT":
                    Save(SaveAction.RejectByAdmin);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been rejected...";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_REJECT";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");
                    break;
                case "ADMIN_REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this book?";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "ADMIN_APPROVE":
                    if (this.Request.QueryString["Action"] == "ChangeCar")
                    {
                        Save(SaveAction.ApproveChangeCar);
                    }
                    else
                    {
                        Save(SaveAction.ApproveByAdmin);
                    }
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been approved...";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_APPROVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");                                        
                    break;
                case "ADMIN_APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this book?";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_APPROVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion             
                case "FINISH":
                    string lastkilometer = cekKilometerMobil();
                    string currentkilometer = txtLastKM.Text;
                    if (currentkilometer == lastkilometer)
                    {
                        cplMain.JSProperties["cpAlertMessage"] = "Kilometer Mobil Belum diUpdate!";
                    }
                    else
                    {
                        Save(SaveAction.FinishByDriver);
                        cplMain.JSProperties["cpAlertMessage"] = "Transaction has been finish...";
                        cplMain.JSProperties["cplblActionButton"] = "FINISH";
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback("BookingList.aspx");
                    }
                    break;
                case "FINISH_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to finish this book?";
                    cplMain.JSProperties["cplblActionButton"] = "FINISH";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
            }
        }
        private bool SaveApprove(SaveAction saveAction)
        {
            bool bSave = true;
            try
            {
                this.myBookingDB = BookingDB.Create(myDBSetting, myDBSession, myLocalDBSetting);
                myBookingEntity = this.myBookingDB.View(Convert.ToInt32(myBookingEntity.DocKey));
            }
            catch
            { }
            myBookingEntity.Save(Email, UserName, "BK", saveAction, Convert.ToString(myBookingEntity.Status),"");
            if (saveAction == SaveAction.Approve)
            {
                UpdateApproval("APPROVE", DecisionNote.Value.ToString(), 1);
            }
            else if (saveAction == SaveAction.Reject)
            {
                UpdateApproval("REJECT", DecisionNote.Value.ToString(), 2);
            }
            
            return bSave;
        }

        protected void UpdateApproval(string Status, string Note, int DecisionCode)
        {
            string ssql = "UPDATE BookingApprovalList SET DecisionState=?, DecisionCode=?, DecisionNote=?, IsDecision = 'T', DecisionDate=GETDATE() WHERE DocKey=? AND NIK =?";
            myLocalDBSetting.ExecuteNonQuery(ssql, Status, DecisionCode, Note, strKey, Email);
        }
        protected void cbDepartment_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myDepartmentTable;
        }        
        protected void luCarType_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myCarTable;
        }
        protected void gvPersonDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailTable;//myds.Tables[1];
        }
        protected void gvPersonDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
      
        }
        protected void gvPersonDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Name"] == null) throw new Exception("Column 'Name' is mandatory.");

            if (StrErrorMsg == "")
            {
                int seq = gvPersonDetail.VisibleRowCount;

                myDetailTable.Rows.Add(myBookingEntity.Bookingcommand.DtlKeyUniqueKey(), myBookingEntity.DocKey, seq, e.NewValues["NIK"], e.NewValues["Name"], e.NewValues["Jabatan"], e.NewValues["Email"]);


                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvPersonDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Name"] == null) throw new Exception("Column 'Name' is mandatory.");
            if (StrErrorMsg == "")
            {
                int editingRowVisibleIndex = gvPersonDetail.EditingRowVisibleIndex;
                int id = Convert.ToInt32(gvPersonDetail.GetRowValues(editingRowVisibleIndex, "DtlKey"));
                var searchExpression = "DtlKey = " + id.ToString();
                DataRow[] foundRow = myDetailTable.Select(searchExpression);
                foreach (DataRow dr in foundRow)
                {
                    dr["Name"] = e.NewValues["Name"];
                    dr["DtlKey"] = e.NewValues["DtlKey"];
                    dr["NIK"] = e.NewValues["NIK"];
                    dr["Jabatan"] = e.NewValues["Jabatan"];
                    dr["Email"] = e.NewValues["Email"];
                }
                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvPersonDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int id = Convert.ToInt32(e.Keys["DtlKey"]);

            var searchExpression = "DtlKey = " + id.ToString();
            DataRow[] foundRow = myDetailTable.Select(searchExpression);
            foreach (DataRow dr in foundRow)
            {
                myDetailTable.Rows.Remove(dr);
            }

            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;
        }
        protected void gvPersonDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void gvPersonDetail_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Name")
            {
                isValidLogin(false);
                if (Page.IsCallback)
                {
                    DataTable myitem = new DataTable();
                    myitem = myDBSetting.GetDataTable(@"SELECT A.ID AS DtlKey,'' DocKey,'' Seq,A.CODE AS NIK ,A.DESCS AS NAME,c.USERGROUPDESC AS JABATAN,A.EMAIL 
                                                        FROM SYS_TBLEMPLOYEE a
                                                        left join MASTER_USER_COMPANY_GROUP b on a.code = b.USER_ID and a.C_CODE=b.C_CODE
                                                        left join MASTER_GROUP c on b.GROUP_CODE = c.USERGROUP
                                                        where a.isactive=1", false);
                    ASPxComboBox cmb = (ASPxComboBox)e.Editor;
                    cmb.DataSource = myitem;
                    cmb.DataBindItems();
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Booking/BookingList.aspx");
        }
    }
}