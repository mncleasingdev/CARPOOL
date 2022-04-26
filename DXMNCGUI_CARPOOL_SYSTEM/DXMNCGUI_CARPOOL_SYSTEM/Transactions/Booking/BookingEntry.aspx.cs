using DevExpress.Web;
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

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public partial class BookingEntry : BasePage
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
        protected DataTable myNumberOfSeatTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myNumberOfSeatTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myNumberOfSeatTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDriverTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDriverTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDriverTable" + this.ViewState["_PageID"]] = value; }
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
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                myDocNoFormatTable = new DataTable();
                mySectionTable = new DataTable();
                myBookTypeTable = new DataTable();
                myNumberOfSeatTable = new DataTable();
                myDriverTable = new DataTable();
                myCarTable = new DataTable();
                myRoleTable = new DataTable();
                strDocName = "";
                iLineIndex = -1;
                igridindex = -1;
                iLineID = -1;
                myds = new DataSet();
                this.myBookingDB = BookingDB.Create(myDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
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

                //deReqPickupTime.MinDate = myDBSetting.GetServerTime().AddMinutes(+15);
                //deReqArrivalTime.MinDate = myDBSetting.GetServerTime().AddMinutes(+15);
                //deEstPickupTime.MinDate = myDBSetting.GetServerTime().AddMinutes(+15);
                //deEstArrivalTime.MinDate = myDBSetting.GetServerTime().AddMinutes(+15);

                myAction = this.myBookingEntity.Action;
                myDocType = this.myBookingEntity.DocumentType;
                myds = myBookingEntity.myDataSet;
                myStatus = this.myBookingEntity.Status.ToString();
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                myds.Tables[1].DefaultView.Sort = "DtlKey";
                gvPersonDetail.DataSource = myDetailTable;
                gvPersonDetail.DataBind();
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

                myBookTypeTable = myDBSetting.GetDataTable("SELECT * FROM [dbo].[BookingType] ORDER BY BookTypeCode", false);
                cbBookType.DataSource = myBookTypeTable;
                cbBookType.DataBind();

                myNumberOfSeatTable = myDBSetting.GetDataTable("SELECT * FROM [dbo].[NumberOfSeat] ORDER BY NumberOfSeat", false);
                cbNumberSeat.DataSource = myNumberOfSeatTable;
                cbNumberSeat.DataBind();

                myDriverTable = myDBSetting.GetDataTable("SELECT USER_NAME FROM [dbo].[MasterUser] WHERE IsDriver=? ORDER BY USER_NAME", false, "T");
                cbDriver.DataSource = myDriverTable;
                cbDriver.DataBind();

                myCarTable = myDBSetting.GetDataTable("SELECT CarCode, CarType, CarName, CarLicense, NumberOfSeat, Kilometer FROM [dbo].[MasterCar] ORDER BY CarType", false);
                luCarType.DataSource = myCarTable;
                luCarType.DataBind();
            }
        }
        private void BindingMaster()
        {
            txtEmployee.Value = myBookingEntity.EmployeeName;
            txtComapany.Value = myBookingEntity.EmployeeCompanyName;
            txtStatus.Value = myBookingEntity.Status;
            txtDocNo.Value = myBookingEntity.DocNo.ToString();
            txtDepartment.Value = myBookingEntity.Department.ToString();
            txtHp.Value = myBookingEntity.Hp.ToString();
            deDocDate.Value = myBookingEntity.DocDate;
            cbBookType.Value = myBookingEntity.DocType;
            cbNumberSeat.Value = myBookingEntity.NumberOfSeat;
            deReqPickupTime.Value = myBookingEntity.RequestStartTime;
            deReqArrivalTime.Value = myBookingEntity.RequestFinishTime;
            txtPickupLoc.Value = myBookingEntity.RequestPickLoc;
            txtDestinationLoc.Value = myBookingEntity.RequestDestLoc;
            mmPickupAddress.Value = myBookingEntity.RequestPickAddress;
            mmDestinationAddress.Value = myBookingEntity.RequestDestAddress;
            mmTripDetail.Value = myBookingEntity.TripDetails;

            cbDriver.Value = myBookingEntity.AdminDriverName;
            luCarType.Value = myBookingEntity.AdminCarType;
            txtLicensePlate.Value = myBookingEntity.AdminCarLicensePlate;
            txtLastKM.Value = myBookingEntity.AdminLastKilometer;
            deEstPickupTime.Value = myBookingEntity.AdminEstPickDateTime;
            deEstArrivalTime.Value = myBookingEntity.AdminEstArriveDateTime;
            mmAdminRemark.Value = myBookingEntity.AdminRemark;

            txtActDriverName.Value = myBookingEntity.DriverName;
            deActPickupTime.Value = myBookingEntity.DriverActualPickDateTime;
            deActArrivalTime.Value = myBookingEntity.DriverActualArriveDateTime;
            mmActRemark.Value = myBookingEntity.DriverRemark;
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
            if (accessright.IsAccessibleByUserID(Email, "IS_ADMIN"))
            {
                if (myAction != DXCAction.New)
                {
                    if (cbDriver.Value == null)
                    {
                        errorF = true;
                        cbDriver.IsValid = false;
                        cbDriver.ErrorText = "Driver can't be empty.";
                        if (!focusF)
                        {
                            cbDriver.Focus();
                            focusF = true;
                            strmessageError = "Driver, can't be empty.";
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                            cplMain.JSProperties["cplActiveTabIndex"] = 1;
                        }
                    }
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
                    if (deEstPickupTime.Value == null)
                    {
                        errorF = true;
                        deEstPickupTime.IsValid = false;
                        deEstPickupTime.ErrorText = "Estimate pick-up date time can't be empty.";
                        if (!focusF)
                        {
                            deEstPickupTime.Focus();
                            focusF = true;
                            strmessageError = "Estimate pick-up date time can't be empty.";
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                            cplMain.JSProperties["cplActiveTabIndex"] = 1;
                        }
                    }
                    if (deEstArrivalTime.Value == null)
                    {
                        errorF = true;
                        deEstArrivalTime.IsValid = false;
                        deEstArrivalTime.ErrorText = "Estimate arrival date time can't be empty.";
                        if (!focusF)
                        {
                            deEstArrivalTime.Focus();
                            focusF = true;
                            strmessageError = "Estimate arrival date time can't be empty.";
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
            DateTime mydate = myDBSetting.GetServerTime();
            myStatus = myBookingEntity.Status.ToString();

            txtEmployee.ReadOnly = true;
            txtComapany.ReadOnly = true;
            txtStatus.ReadOnly = true;
            txtDocNo.ReadOnly = true;
            txtDepartment.ReadOnly = true;
            txtHp.ReadOnly = true;
            deDocDate.ReadOnly = false;

            #region Super Admin Accessable
            if (accessright.IsAccessibleByUserID(Email, "IS_SUPER_ADMIN"))
            {

            }
            #endregion
            #region Customer Accessable
            if (accessright.IsAccessibleByUserID(Email, "IS_CUSTOMER"))
            {
                deDocDate.ReadOnly = false;
                cbBookType.ReadOnly = false;
                cbNumberSeat.ReadOnly = false;
                deReqPickupTime.ReadOnly = false;
                deReqArrivalTime.ReadOnly = false;
                txtPickupLoc.ReadOnly = false;
                txtDestinationLoc.ReadOnly = false;
                mmPickupAddress.ReadOnly = false;
                mmDestinationAddress.ReadOnly = false;
                mmTripDetail.ReadOnly = false;
                gvPersonDetail.Columns["ClmnCommand"].Visible = true;
                gvPersonDetail.Columns["colNo"].Visible = false;

                cbDriver.ReadOnly = true;
                luCarType.ReadOnly = true;
                deEstPickupTime.ReadOnly = true;
                deEstArrivalTime.ReadOnly = true;
                txtLicensePlate.ReadOnly = true;
                txtLastKM.ReadOnly = true;
                mmAdminRemark.ReadOnly = true;
                btnAdminOnHold.ClientEnabled = false;
                btnAdminReject.ClientEnabled = false;
                btnAdminApprove.ClientEnabled = false;
                btnAdminApprove.ClientEnabled = false;
                btnAdminOnHold.ClientEnabled = false;
                btnAdminReject.ClientEnabled = false;

                mmActRemark.ReadOnly = true;
                deActPickupTime.ReadOnly = true;
                deActArrivalTime.ReadOnly = true;
                btnDriverPickUp.ClientEnabled = false;
                btnDriverReject.ClientEnabled = false;
                btnDriverFinish.ClientEnabled = false;

                btnCancel.ClientEnabled = myBookingEntity.DocNo.ToString() == "NEW" ? false : true;

                if (myBookingEntity.Status.ToString() != "NEW")
                {
                    deDocDate.ReadOnly = true;
                    cbBookType.ReadOnly = true;
                    cbNumberSeat.ReadOnly = true;
                    deReqPickupTime.ReadOnly = true;
                    deReqArrivalTime.ReadOnly = true;
                    txtPickupLoc.ReadOnly = true;
                    txtDestinationLoc.ReadOnly = true;
                    mmPickupAddress.ReadOnly = true;
                    mmDestinationAddress.ReadOnly = true;
                    mmTripDetail.ReadOnly = true;
                    gvPersonDetail.Columns["ClmnCommand"].Visible = false;
                    gvPersonDetail.Columns["colNo"].Visible = true;

                    btnSubmit.ClientEnabled = false;
                    btnCancel.ClientEnabled = false;
                }
            }
            #endregion
            #region Dispatcher Accessable
            if (accessright.IsAccessibleByUserID(Email, "IS_DISPATCHER"))
            {
                btnSubmit.ClientEnabled = false;
                btnCancel.ClientEnabled = false;

                if (!accessright.IsAccessibleByUserID(Email, "IS_CUSTOMER"))
                {
                    deDocDate.ReadOnly = true;
                    cbBookType.ReadOnly = true;
                    cbNumberSeat.ReadOnly = true;
                    deReqPickupTime.ReadOnly = true;
                    deReqArrivalTime.ReadOnly = true;
                    txtPickupLoc.ReadOnly = true;
                    txtDestinationLoc.ReadOnly = true;
                    mmPickupAddress.ReadOnly = true;
                    mmDestinationAddress.ReadOnly = true;
                    mmTripDetail.ReadOnly = true;
                    gvPersonDetail.Columns["ClmnCommand"].Visible = false;
                    gvPersonDetail.Columns["colNo"].Visible = true;
                }
                if (accessright.IsAccessibleByUserID(Email, "IS_CUSTOMER"))
                {
                    btnSubmit.ClientEnabled = true;
                    btnCancel.ClientEnabled = true;
                }

                mmActRemark.ReadOnly = true;
                btnDriverPickUp.ClientEnabled = false;
                btnDriverReject.ClientEnabled = false;
                btnDriverFinish.ClientEnabled = false;

                if (myStatus.Contains("ON SCHEDULE")
                    || myStatus.Contains("REJECTED BY ADMIN")
                        || myStatus.ToString().Contains("PICKUP")
                            || myStatus.Contains("REJECTED BY DRIVER")
                                || myStatus.Contains("FINISH")
                                    || myStatus.Contains("WAITING APPROVAL") 
                                        || myStatus.Contains("REJECT BY SUPERIOR"))
                {
                    cbDriver.ReadOnly = true;
                    luCarType.ReadOnly = true;
                    deEstPickupTime.ReadOnly = true;
                    deEstArrivalTime.ReadOnly = true;
                    txtLicensePlate.ReadOnly = true;
                    txtLastKM.ReadOnly = true;
                    mmAdminRemark.ReadOnly = true;

                    btnAdminApprove.ClientEnabled = false;
                    btnAdminOnHold.ClientEnabled = false;
                    btnAdminReject.ClientEnabled = false;

                    btnSubmit.ClientVisible = false;
                }
                if (myStatus.Contains("NEW"))
                {
                    if (myAction != DXCAction.New)
                    {
                        cbDriver.ReadOnly = false;
                        luCarType.ReadOnly = false;
                        deEstPickupTime.ReadOnly = false;
                        deEstArrivalTime.ReadOnly = false;
                        mmAdminRemark.ReadOnly = false;

                        btnAdminApprove.ClientEnabled = true;
                        btnAdminOnHold.ClientEnabled = true;
                        btnAdminReject.ClientEnabled = true;

                        btnSubmit.ClientVisible = false;
                    }
                }
                if (myStatus == "HOLD BY ADMIN")
                {
                    btnAdminApprove.ClientEnabled = true;
                    btnAdminOnHold.ClientEnabled = false;
                    btnAdminReject.ClientEnabled = true;
                }
            }
            #endregion
            #region Driver Accessable
            if (accessright.IsAccessibleByUserID(Email, "IS_DRIVER"))
            {
                deDocDate.ReadOnly = true;
                cbBookType.ReadOnly = true;
                cbNumberSeat.ReadOnly = true;
                deReqPickupTime.ReadOnly = true;
                deReqArrivalTime.ReadOnly = true;
                txtPickupLoc.ReadOnly = true;
                txtDestinationLoc.ReadOnly = true;
                mmPickupAddress.ReadOnly = true;
                mmDestinationAddress.ReadOnly = true;
                mmTripDetail.ReadOnly = true;
                gvPersonDetail.Columns["ClmnCommand"].Visible = false;
                gvPersonDetail.Columns["colNo"].Visible = true;

                cbDriver.ReadOnly = true;
                luCarType.ReadOnly = true;
                deEstPickupTime.ReadOnly = true;
                deEstArrivalTime.ReadOnly = true;
                txtLicensePlate.ReadOnly = true;
                txtLastKM.ReadOnly = true;
                mmAdminRemark.ReadOnly = true;
                btnAdminOnHold.ClientEnabled = false;
                btnAdminReject.ClientEnabled = false;
                btnAdminApprove.ClientEnabled = false;

                btnSubmit.ClientEnabled = false;
                btnCancel.ClientEnabled = false;
            }
            #endregion
            #region Admin Accessable
            if (accessright.IsAccessibleByUserID(Email, "IS_ADMIN"))
            {
                btnSubmit.ClientEnabled = true;
                btnCancel.ClientEnabled = true;

                mmActRemark.ReadOnly = true;
                btnDriverPickUp.ClientEnabled = false;
                btnDriverReject.ClientEnabled = false;
                btnDriverFinish.ClientEnabled = false;

                if (myStatus.Contains("ON SCHEDULE")
                    || myStatus.Contains("REJECTED BY ADMIN")
                        || myStatus.ToString().Contains("PICKUP")
                            || myStatus.Contains("REJECTED BY DRIVER")
                                || myStatus.Contains("FINISH")
                                    || myStatus.Contains("WAITING APPROVAL")
                                        || myStatus.Contains("REJECT BY SUPERIOR"))
                {
                    cbDriver.ReadOnly = true;
                    luCarType.ReadOnly = true;
                    deEstPickupTime.ReadOnly = true;
                    deEstArrivalTime.ReadOnly = true;
                    txtLicensePlate.ReadOnly = true;
                    txtLastKM.ReadOnly = true;
                    mmAdminRemark.ReadOnly = true;

                    btnAdminApprove.ClientEnabled = false;
                    btnAdminOnHold.ClientEnabled = false;
                    btnAdminReject.ClientEnabled = false;

                    btnSubmit.ClientVisible = false;
                }
                if (myStatus.Contains("NEW"))
                {
                    if (myAction != DXCAction.New)
                    {
                        cbDriver.ReadOnly = false;
                        luCarType.ReadOnly = false;
                        deEstPickupTime.ReadOnly = false;
                        deEstArrivalTime.ReadOnly = false;
                        mmAdminRemark.ReadOnly = false;

                        btnAdminApprove.ClientEnabled = true;
                        btnAdminOnHold.ClientEnabled = true;
                        btnAdminReject.ClientEnabled = true;

                        btnSubmit.ClientVisible = false;
                    }
                }
                if (myStatus == "HOLD BY DISPATCHER")
                {
                    btnAdminApprove.ClientEnabled = true;
                    btnAdminOnHold.ClientEnabled = false;
                    btnAdminReject.ClientEnabled = true;
                }          
            }
            #endregion

            #region Control Color
            btnSubmit.ForeColor = btnSubmit.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnCancel.ForeColor = btnCancel.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            btnAdminApprove.ForeColor = btnAdminApprove.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnAdminOnHold.ForeColor = btnAdminOnHold.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnAdminReject.ForeColor = btnAdminReject.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            btnDriverPickUp.ForeColor = btnDriverPickUp.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnDriverFinish.ForeColor = btnDriverFinish.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.DarkSlateBlue;
            btnDriverReject.ForeColor = btnDriverReject.ClientEnabled == false ? System.Drawing.Color.Gray : System.Drawing.Color.Red;

            txtEmployee.BackColor = txtEmployee.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtComapany.BackColor = txtComapany.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtStatus.BackColor = txtStatus.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDocNo.BackColor = txtDocNo.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDepartment.BackColor = txtDepartment.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtHp.BackColor = txtHp.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deDocDate.BackColor = deDocDate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            cbBookType.BackColor = cbBookType.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            cbNumberSeat.BackColor = cbNumberSeat.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deReqPickupTime.BackColor = deReqPickupTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deReqArrivalTime.BackColor = deReqArrivalTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtPickupLoc.BackColor = txtPickupLoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtDestinationLoc.BackColor = txtDestinationLoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmPickupAddress.BackColor = mmPickupAddress.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmDestinationAddress.BackColor = mmDestinationAddress.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmTripDetail.BackColor = mmTripDetail.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;

            cbDriver.BackColor = cbDriver.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            luCarType.BackColor = luCarType.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtLicensePlate.BackColor = txtLicensePlate.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            txtLastKM.BackColor = txtLastKM.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deEstPickupTime.BackColor = deEstPickupTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deEstArrivalTime.BackColor = deEstArrivalTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmAdminRemark.BackColor = mmAdminRemark.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;

            txtActDriverName.BackColor = txtActDriverName.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deActPickupTime.BackColor = deActPickupTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            deActArrivalTime.BackColor = deActArrivalTime.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmActRemark.BackColor = mmActRemark.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            #endregion
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            DataTable dtCopyApp = new DataTable();

            gvPersonDetail.UpdateEdit();
            myBookingEntity.DocNo = txtDocNo.Value;
            myBookingEntity.DocDate = deDocDate.Value;
            myBookingEntity.Department = txtDepartment.Value;
            myBookingEntity.Hp = txtHp.Value;
            myBookingEntity.DocType = cbBookType.Value;
            myBookingEntity.EmployeeName = txtEmployee.Value;
            myBookingEntity.EmployeeCompanyName = txtComapany.Value;
            myBookingEntity.Status = txtStatus.Value;
            myBookingEntity.NumberOfSeat = cbNumberSeat.Value;
            myBookingEntity.RequestStartTime = deReqPickupTime.Value;
            myBookingEntity.RequestFinishTime = deReqArrivalTime.Value;
            myBookingEntity.RequestPickLoc = txtPickupLoc.Value;
            myBookingEntity.RequestDestLoc = txtDestinationLoc.Value;
            myBookingEntity.RequestPickAddress = mmPickupAddress.Value;
            myBookingEntity.RequestDestAddress = mmDestinationAddress.Value;
            myBookingEntity.TripDetails = mmTripDetail.Value;

            myBookingEntity.AdminDriverName = cbDriver.Value;
            myBookingEntity.AdminCarType = luCarType.Value;
            myBookingEntity.AdminCarLicensePlate = txtLicensePlate.Value;
            myBookingEntity.AdminLastKilometer = txtLastKM.Value;
            myBookingEntity.AdminEstPickDateTime = deEstPickupTime.Value == null ? DBNull.Value : deEstPickupTime.Value;
            myBookingEntity.AdminEstArriveDateTime = deEstArrivalTime.Value == null ? DBNull.Value : deEstArrivalTime.Value;
            myBookingEntity.AdminRemark = mmAdminRemark.Value;

            myBookingEntity.DriverName = txtActDriverName.Value;
            myBookingEntity.DriverActualPickDateTime = deActPickupTime.Value == null ? DBNull.Value : deActPickupTime.Value;
            myBookingEntity.DriverActualArriveDateTime = deActArrivalTime.Value == null ? DBNull.Value : deActArrivalTime.Value;
            myBookingEntity.DriverRemark = mmActRemark.Value;

            if (myAction == DXCAction.New)
            {
                myBookingEntity.CreatedBy = txtEmployee.Value;
                myBookingEntity.CreatedDateTime = myDBSetting.GetServerTime();
                myBookingEntity.LastModifiedDateTime = myDBSetting.GetServerTime();
            }
            myBookingEntity.Save(UserID, UserName, "BK", saveAction, myBookingEntity.Status.ToString());
            return bSave;
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
            SqlDBSetting dbSetting = this.myDBSetting;
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                #region ACTION BY USER
                case "SUBMIT":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been submit...";
                    cplMain.JSProperties["cplblActionButton"] = "SUBMIT";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
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
                case "CANCEL":
                    Save(SaveAction.Cancel);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been cancelled...";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
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
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
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
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
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
                    Save(SaveAction.ApproveByAdmin);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been approved...";
                    cplMain.JSProperties["cplblActionButton"] = "ADMIN_APPROVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
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
                #region ACYION BY DRIVER
                case "DRIVER_PICKUP":
                    Save(SaveAction.PickupByDriver);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been pickup...";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_PICKUP";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "DRIVER_PICKUP_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to pick-up this book?";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_PICKUP";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
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
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to rejected this book?";
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
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to finish this book?";
                    cplMain.JSProperties["cplblActionButton"] = "DRIVER_FINISH";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
            }
        }
        protected void cbBookType_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myBookTypeTable;
        }
        protected void cbNumberSeat_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myNumberOfSeatTable;
        }
        protected void cbDriver_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myDriverTable;
        }
        protected void luCarType_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myCarTable;
        }
        protected void gvPersonDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myds.Tables[1];
        }
        protected void gvPersonDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Remark1"] = DBNull.Value;
            e.NewValues["Remark2"] = DBNull.Value;
            e.NewValues["Remark3"] = DBNull.Value;
        }
        protected void gvPersonDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Name"] == null) throw new Exception("Column 'Name' is mandatory.");
            if (e.NewValues["Gender"] == null) throw new Exception("Column 'Gender' is mandatory.");
            if (e.NewValues["Status"] == null) throw new Exception("Column 'Status' is mandatory.");

            if (StrErrorMsg == "")
            {
                DataRow[] ValidLinesRows = myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                myDetailTable.Rows.Add(myBookingEntity.Bookingcommand.DtlKeyUniqueKey(), myBookingEntity.DocKey, seq, e.NewValues["Name"], e.NewValues["Gender"], e.NewValues["Status"], e.NewValues["Remark1"], e.NewValues["Remark2"], e.NewValues["Remark3"]);

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvPersonDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Name"] == null) throw new Exception("Column 'Name' is mandatory.");
            if (e.NewValues["Gender"] == null) throw new Exception("Column 'Gender' is mandatory.");
            if (e.NewValues["Status"] == null) throw new Exception("Column 'Status' is mandatory.");
            if (StrErrorMsg == "")
            {
                int editingRowVisibleIndex = gvPersonDetail.EditingRowVisibleIndex;
                int id = (int)gvPersonDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");
                DataRow dr = myDetailTable.Rows.Find(id);
                dr["Name"] = e.NewValues["Name"];
                dr["Gender"] = e.NewValues["Gender"];
                dr["Status"] = e.NewValues["Status"];
                dr["Remark1"] = e.NewValues["Remark1"];
                dr["Remark2"] = e.NewValues["Remark2"];
                dr["Remark3"] = e.NewValues["Remark3"];

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvPersonDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int id = (int)e.Keys["DtlKey"];
            DataRow dr = myDetailTable.Rows.Find(id);

            myDetailTable.Rows.Remove(dr);

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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Booking/BookingList.aspx");
        }
    }
}