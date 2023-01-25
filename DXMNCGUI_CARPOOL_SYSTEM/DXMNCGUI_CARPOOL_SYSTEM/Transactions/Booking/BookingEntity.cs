using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public class BookingEntity
    {
        private BookingDB myBookingCommand;
        internal DataSet myDataSet;

        private DataRow myRowUser;
        private DataRow myRowAdmin;
        private DataRow myRowDriver;

        private DataTable myUserTable;
        private DataTable myUserDetailTable;
        private DataTable myAdminTable;
        private DataTable myDriverTable;

        private DXCAction myAction;
        private DXCType myDocType;
        public string strErrorGenTicket;
        private string myAppNote;

        internal DataRow RowUser
        {
            get { return myRowUser; }
        }
        internal DataRow RowAdmin
        {
            get { return myRowAdmin; }
        }
        internal DataRow RowDriver
        {
            get { return myRowDriver; }
        }

        public BookingDB Bookingcommand
        {
            get
            {
                return this.myBookingCommand;
            }
        }
        public DataTable DataTableUser
        {
            get
            {
                return this.myUserTable;
            }
        }
        public DataTable DataTableUserDetail
        {
            get
            {
                return this.myUserDetailTable;
            }
        }
        public DataTable DataTableAdmin
        {
            get
            {
                return this.myAdminTable;
            }
        }
        public DataTable DataTableDriver
        {
            get
            {
                return this.myDriverTable;
            }
        }

        public DataSet BookingDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }
        public string ApprovalNote
        {
            get
            {
                return this.myAppNote;
            }
            set
            {
                this.myAppNote = value;
            }
        }
        public BookingEntity(BookingDB aBooking, DataSet ds, DXCAction action)
        {
            myBookingCommand = aBooking;
            myDataSet = ds;
            this.myAction = action;
            this.myUserTable = this.myDataSet.Tables["User"];
            this.myUserDetailTable = this.myDataSet.Tables["UserDetail"];
            this.myAdminTable = this.myDataSet.Tables["Admin"];
            this.myDriverTable = this.myDataSet.Tables["Driver"];

            myRowUser = myUserTable.Rows[0];
            myRowAdmin = myAdminTable.Rows[0];
            myRowDriver = myDriverTable.Rows[0];
        }
        public DataTable LoadDocNoFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = myBookingCommand.myLocalDBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='CD'", false);
            return mytable;
        }
        public DataTable LoadCategoryTable()
        {
            DataTable mytable = new DataTable();
            mytable = myBookingCommand.myLocalDBSetting.GetDataTable("select Category from [dbo].[Category] where DocType='CD'", false);
            return mytable;
        }
        public DataTable LoadCategorySubTable(string category)
        {
            DataTable mytable = new DataTable();
            string strQuery = "select Category, SubCategory from CategorySub where Category=? order by SubCategory";
            mytable = myBookingCommand.myLocalDBSetting.GetDataTable(strQuery, false, category);
            return mytable;
        }
        public DataTable LoadApproverTable(string sID)
        {
            object obj = null;
            string sFirstUpline = "", sSecondUpline = "", sThirdUpline = "";
            string strQuery = "select HEAD from Users where NIK=?";
            DataTable mytable = new DataTable();

            obj = myBookingCommand.DBSetting.ExecuteScalar(strQuery, sID);
            if (obj != null && obj != DBNull.Value)
            {
                sFirstUpline = obj.ToString();
            }
            obj = myBookingCommand.DBSetting.ExecuteScalar(strQuery, sFirstUpline);
            if (obj != null && obj != DBNull.Value)
            {
                sSecondUpline = obj.ToString();
            }
            obj = myBookingCommand.DBSetting.ExecuteScalar(strQuery, sSecondUpline);
            if (obj != null && obj != DBNull.Value)
            {
                sThirdUpline = obj.ToString();
            }

            strQuery = "select HEAD, HEADNAME from Users where NIK=? \n";
            strQuery += "UNION ALL \n";
            strQuery += "select HEAD, HEADNAME from Users where NIK=? \n";
            strQuery += "UNION ALL \n";
            strQuery += "select HEAD, HEADNAME from Users where NIK=? \n";
            strQuery += "UNION ALL \n";
            strQuery += "select HEAD, HEADNAME from Users where NIK=? \n";
            mytable = myBookingCommand.DBSetting.GetDataTable(strQuery, false, sID, sFirstUpline, sSecondUpline, sThirdUpline);
            return mytable;
        }

        public DXCAction Action
        {
            get
            {
                return this.myAction;
            }
        }
        public DXCType DocumentType
        {
            get
            {
                return this.myDocType;
            }
        }

        internal DataRow[] ValidDetailLinesRows
        {
            get
            {
                return this.myUserDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }
        public void Save(string userID, string userName, string strDocName, SaveAction saveaction, string sCurrentStat, string approver)
        {
            if (saveaction == SaveAction.Cancel)
            {
                this.myAction = DXCAction.Cancel;
            }
            //if(saveaction == SaveAction.PickupByDriver)
            //{
            //    this.myRowUser["Status"] = "PICKUP";
            //}
            //if (saveaction == SaveAction.FinishByDriver)
            //{
            //    this.myRowUser["Status"] = "FINISH";
            //}
            //if (saveaction == SaveAction.RejectByDriver)
            //{
            //    this.myRowUser["Status"] = "REJECTED BY DRIVER";
            //}
            //if (saveaction == SaveAction.Approve)
            //{
            //this.myRowUser["Status"] = "NEW";
            //this.myRowUser["Status"] = "APPROVED BY " + userID +"-"+ userName;
            //}
            if (saveaction == SaveAction.Reject)
            {
                this.myRowUser["Status"] = "REJECTED";
            }
            {
                //bool flag = this.myRowUser.RowState != DataRowState.Unchanged;
                //foreach (DataRow dataRow in this.ValidDetailLinesRows)
                //{
                //    if (!flag && dataRow.RowState != DataRowState.Unchanged)
                //        flag = true;
                //}
                //if (!flag && this.myUserDetailTable.Select("", "Seq", DataViewRowState.Deleted).Length > 0)
                //    flag = true;
                //if (flag)
                //{
                    //userID = this.myRowUser["EmployeeName"].ToString();
                    this.myRowUser["LastModifiedBy"] = this.myRowUser["EmployeeName"];
                    this.myRowUser["LastModifiedDateTime"] = (object)this.myBookingCommand.DBSetting.GetServerTime();
                    if (this.myRowUser["CreatedBy"].ToString().Length == 0)
                        this.myRowUser["CreatedBy"] = this.myRowUser["LastModifiedUser"];
                    this.myRowUser.EndEdit();
                    myBookingCommand.SaveEntity(this, strDocName, saveaction, sCurrentStat, userID, userName, approver);
                //}
                this.myAction = DXCAction.View;
            }
        }
        public void SaveComment(string userName, string sComment, SaveAction saveaction)
        {
            myBookingCommand.SaveCommentEntity(this, saveaction, userName, sComment);
            this.myAction = DXCAction.View;
        }
        public void Edit()
        {
            if (this.myAction == DXCAction.View)
            {
                this.myAction = DXCAction.Edit;
            }
        }
        public int LinesCount
        {
            get
            {
                return this.ValidDetailLinesRows.Length;
            }
        }
        public bool DeleteDetailLines(int index)
        {
            if (this.myAction == DXCAction.View)
            {
                throw new Exception("Cannot Delete read-only Booking");
            }
            else
            {
                DataRow[] validRows = this.ValidDetailLinesRows;
                if (index >= 0 && index < validRows.Length)
                {
                    validRows[index].Delete();
                    return true;
                }
                else
                    return false;
            }
        }
        public void ClearLines()
        {
            if (this.myAction == DXCAction.View)
            {
                throw new Exception("Cannot edit read-only Booking");
            }
            else
            {
                foreach (DataRow dataRow in this.ValidDetailLinesRows)
                    dataRow.Delete();
            }
        }
        public long DtlKeyUniqueKey()
        {
            return myBookingCommand.DBReg.IncOne((Controllers.Registry.IRegistryID)new BookingDetailLinesDtlKey());
        }
        public BookingLinesRecord AddLines()
        {
            if (this.myAction == DXCAction.View)
                throw new Exception("Cannot edit read-only Detail Lines");
            else
                return this.InternalAddLines(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private BookingLinesRecord InternalAddLines(int seq)
        {
            DataRow row = this.myUserDetailTable.NewRow();
            DateTime myDate = myBookingCommand.DBSetting.GetServerTime();
            string iUserID = myBookingCommand.DBSession.LoginUserID;
            row["DtlKey"] = (object)myBookingCommand.DtlKeyUniqueKey();
            row["DocKey"] = this.myRowUser["DocKey"];
            row["Seq"] = (object)seq;
            row["Condition"] = DBNull.Value;
            row["ItemDescription"] = DBNull.Value;
            row["Year"] = myDate.Year;
            row["UnitPrice"] = 0;
            row["Qty"] = 0;
            row["SubTotal"] = 0;
            row.EndEdit();
            this.myUserDetailTable.Rows.Add(row);
            return new BookingLinesRecord(row, this);
        }
        public bool IsModified()
        {
            if (this.myUserDetailTable.GetChanges() != null)
                return true;
            else
                return false;
        }

        #region USER ENTITY
        public object DocKey
        {
            get { return myRowUser["DocKey"]; }
            set { myRowUser["DocKey"] = value; }
        }
        public object DocNo
        {
            get { return myRowUser["DocNo"]; }
            set { myRowUser["DocNo"] = value; }
        }
        public object DocDate
        {
            get { return myRowUser["DocDate"]; }
            set { myRowUser["DocDate"] = value; }
        }
        public object DocType
        {
            get { return myRowUser["DocType"]; }
            set { myRowUser["DocType"] = value; }
        }
        public object Note
        {
            get { return myRowUser["Note"]; }
            set { myRowUser["Note"] = value; }
        }
        public object Remark1
        {
            get { return myRowUser["Remark1"]; }
            set { myRowUser["Remark1"] = value; }
        }
        public object Remark2
        {
            get { return myRowUser["Remark2"]; }
            set { myRowUser["Remark2"] = value; }
        }
        public object Remark3
        {
            get { return myRowUser["Remark3"]; }
            set { myRowUser["Remark3"] = value; }
        }
        public object Remark4
        {
            get { return myRowUser["Remark4"]; }
            set { myRowUser["Remark4"] = value; }
        }
        public object EmployeeName
        {
            get { return myRowUser["EmployeeName"]; }
            set { myRowUser["EmployeeName"] = value; }
        }
        public object EmployeeCompanyName
        {
            get { return myRowUser["EmployeeCompanyName"]; }
            set { myRowUser["EmployeeCompanyName"] = value; }
        }
        public object Status
        {
            get { return myRowUser["Status"]; }
            set { myRowUser["Status"] = value; }
        }
        public object NumberOfSeat
        {
            get { return myRowUser["NumberOfSeat"]; }
            set { myRowUser["NumberOfSeat"] = value; }
        }
        public object RequestStartTime
        {
            get { return myRowUser["RequestStartTime"]; }
            set { myRowUser["RequestStartTime"] = value; }
        }
        public object RequestFinishTime
        {
            get { return myRowUser["RequestFinishTime"]; }
            set { myRowUser["RequestFinishTime"] = value; }
        }
        public object RequestPickLoc
        {
            get { return myRowUser["RequestPickLoc"]; }
            set { myRowUser["RequestPickLoc"] = value; }
        }
        public object RequestDestLoc
        {
            get { return myRowUser["RequestDestLoc"]; }
            set { myRowUser["RequestDestLoc"] = value; }
        }
        public object RequestPickAddress
        {
            get { return myRowUser["RequestPickAddress"]; }
            set { myRowUser["RequestPickAddress"] = value; }
        }
        public object RequestDestAddress
        {
            get { return myRowUser["RequestDestAddress"]; }
            set { myRowUser["RequestDestAddress"] = value; }
        }
        public object CreatedBy
        {
            get { return myRowUser["CreatedBy"]; }
            set { myRowUser["CreatedBy"] = value; }
        }
        public object CreatedDateTime
        {
            get { return myRowUser["CreatedDateTime"]; }
            set { myRowUser["CreatedDateTime"] = value; }
        }
        public object LastModifiedBy
        {
            get { return myRowUser["LastModifiedBy"]; }
            set { myRowUser["LastModifiedBy"] = value; }
        }
        public object LastModifiedDateTime
        {
            get { return myRowUser["LastModifiedDateTime"]; }
            set { myRowUser["LastModifiedDateTime"] = value; }
        }
        public object Cancelled
        {
            get { return myRowUser["Cancelled"]; }
            set { myRowUser["Cancelled"] = value; }
        }
        public object CancelledBy
        {
            get { return myRowUser["CancelledBy"]; }
            set { myRowUser["CancelledBy"] = value; }
        }
        public object CancelledDateTime
        {
            get { return myRowUser["CancelledDateTime"]; }
            set { myRowUser["CancelledDateTime"] = value; }
        }
        public object CancelledReason
        {
            get { return myRowUser["CancelledReason"]; }
            set { myRowUser["CancelledReason"] = value; }
        }
        public object Department
        {
            get { return myRowUser["Department"]; }
            set { myRowUser["Department"] = value; }
        }
        public object TripDetails
        {
            get { return myRowUser["TripDetails"]; }
            set { myRowUser["TripDetails"] = value; }
        }
        public object IsSettlement
        {
            get { return myRowUser["IsSettlement"]; }
            set { myRowUser["IsSettlement"] = value; }
        }
        public object Hp
        {
            get { return myRowUser["Hp"]; }
            set { myRowUser["Hp"] = value; }
        }
        public object NeeedApproval
        {
            get { return myRowUser["NeeedApproval"]; }
            set { myRowUser["NeeedApproval"] = value; }
        }
        public object Approver
        {
            get { return myRowUser["Approver"]; }
            set { myRowUser["Approver"] = value; }
        }
        #endregion
        #region ADMIN ENTITY
        public object AdminDocKey
        {
            get { return myRowAdmin["DocKey"]; }
            set { myRowAdmin["DocKey"] = value; }
        }
        public object AdminSourceKey
        {
            get { return myRowAdmin["SourceKey"]; }
            set { myRowAdmin["SourceKey"] = value; }
        }
        public object AdminDriverCode
        {
            get { return myRowAdmin["DriverCode"]; }
            set { myRowAdmin["DriverCode"] = value; }
        }
        public object AdminDriverName
        {
            get { return myRowAdmin["DriverName"]; }
            set { myRowAdmin["DriverName"] = value; }
        }
        public object AdminCarCode
        {
            get { return myRowAdmin["CarCode"]; }
            set { myRowAdmin["CarCode"] = value; }
        }
        public object AdminCarType
        {
            get { return myRowAdmin["CarType"]; }
            set { myRowAdmin["CarType"] = value; }
        }
        public object AdminCarLicensePlate
        {
            get { return myRowAdmin["CarLicensePlate"]; }
            set { myRowAdmin["CarLicensePlate"] = value; }
        }
        public object AdminRemark
        {
            get { return myRowAdmin["Remark"]; }
            set { myRowAdmin["Remark"] = value; }
        }
        public object AdminEstPickDateTime
        {
            get { return myRowAdmin["EstPickDateTime"]; }
            set { myRowAdmin["EstPickDateTime"] = value; }
        }
        public object AdminEstArriveDateTime
        {
            get { return myRowAdmin["EstArriveDateTime"]; }
            set { myRowAdmin["EstArriveDateTime"] = value; }
        }
        public object AdminCode
        {
            get { return myRowAdmin["AdminCode"]; }
            set { myRowAdmin["AdminCode"] = value; }
        }
        public object AdminName
        {
            get { return myRowAdmin["AdminName"]; }
            set { myRowAdmin["AdminName"] = value; }
        }
        public object AdminCreatedBy
        {
            get { return myRowAdmin["CreatedBy"]; }
            set { myRowAdmin["CreatedBy"] = value; }
        }
        public object AdminCreatedDateTime
        {
            get { return myRowAdmin["CreatedDateTime"]; }
            set { myRowAdmin["CreatedDateTime"] = value; }
        }
        public object AdminLastModifiedBy
        {
            get { return myRowAdmin["LastModifiedBy"]; }
            set { myRowAdmin["LastModifiedBy"] = value; }
        }
        public object AdminLastModifiedDateTime
        {
            get { return myRowAdmin["LastModifiedDateTime"]; }
            set { myRowAdmin["LastModifiedDateTime"] = value; }
        }
        public object AdminLastKilometer
        {
            get { return myRowAdmin["LastKilometer"]; }
            set { myRowAdmin["LastKilometer"] = value; }
        }
        #endregion
        #region DRIVER ENTITY
        public object DriverDocKey
        {
            get { return myRowDriver["DocKey"]; }
            set { myRowDriver["DocKey"] = value; }
        }
        public object DriverSourceKey
        {
            get { return myRowDriver["SourceKey"]; }
            set { myRowDriver["SourceKey"] = value; }
        }
        public object DriverName
        {
            get { return myRowDriver["DriverName"]; }
            set { myRowDriver["DriverName"] = value; }
        }
        public object DriverActualPickDateTime
        {
            get { return myRowDriver["ActualPickDateTime"]; }
            set { myRowDriver["ActualPickDateTime"] = value; }
        }
        public object DriverActualArriveDateTime
        {
            get { return myRowDriver["ActualArriveDateTime"]; }
            set { myRowDriver["ActualArriveDateTime"] = value; }
        }
        public object DriverRemark
        {
            get { return myRowDriver["DriverRemark"]; }
            set { myRowDriver["DriverRemark"] = value; }
        }
        public object DriverLastModifiedBy
        {
            get { return myRowDriver["LastModifiedBy"]; }
            set { myRowDriver["LastModifiedBy"] = value; }
        }
        public object DriverLastModifiedDateTime
        {
            get { return myRowDriver["LastModifiedDateTime"]; }
            set { myRowDriver["LastModifiedDateTime"] = value; }
        }
        public object DriverLastKilometer
        {
            get { return myRowDriver["LastKilometer"]; }
            set { myRowDriver["LastKilometer"] = value; }
        }
        public object DriverCurrentKilometer
        {
            get { return myRowDriver["CurrentKilometer"]; }
            set { myRowDriver["CurrentKilometer"] = value; }
        }
        #endregion

        public DataTable Bookingtable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}