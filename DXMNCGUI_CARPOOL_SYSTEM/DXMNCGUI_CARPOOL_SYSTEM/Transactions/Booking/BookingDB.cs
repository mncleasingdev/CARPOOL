using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public class BookingDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableApproval;
        protected DataTable myBrowseTableSchedule;
        protected DataTable myDataTableAllMaster;
        protected DBRegistry myDBReg;
        internal BookingDB()
        {
            myBrowseTable = new DataTable();
            myBrowseTableApproval = new DataTable();
            myBrowseTableSchedule = new DataTable();
        }
        public SqlDBSetting DBSetting
        {
            get { return myDBSetting; }
        }
        public SqlLocalDBSetting LocalDBSetting
        {
            get { return myLocalDBSetting; }
        }
        public SqlDBSession DBSession
        {
            get { return myDBSession; }
        }
        public Controllers.Registry.DBRegistry DBReg
        {
            get
            {
                return this.myDBReg;
            }
        }     
        public static BookingDB Create(SqlDBSetting dbSetting, SqlDBSession dbSession, SqlLocalDBSetting localDBSetting)
        {
            BookingDB aBooking = (BookingDB)null; ;
            aBooking = new BookingSql();
            aBooking.myDBSetting = dbSetting;
            aBooking.myDBSession = dbSession;
            aBooking.myLocalDBSetting = localDBSetting;
            return aBooking;
        }
        public DataTable DataTableAllMaster
        {
            get { return this.myDataTableAllMaster; }
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public DataTable BrowseTableApproval
        {
            get { return myBrowseTableApproval; }
        }
        public DataTable BrowseTableSchedule
        {
            get { return myBrowseTableSchedule; }
        }
        public virtual void Sendmail(string strapprovalID, string strapprovalName, BookingEntity Booking, string strsubject, string strbody, SqlDBSetting dbsetting, bool bsender, bool breject, string strrejectnote, string traveltype, Int64 itravelKey)
        {
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, bool bViewAllperDept, string userID, string Department)
        {
            return null;
        }
        public virtual DataTable LoadBrowseTableForDriver(string DriverName)
        {
            return null;
        }
        public virtual DataTable LoadBrowseTableApproval(string UserName)
        {
            return null;
        }
        public virtual DataTable LoadBrowseTableSchedule(string UserName)
        {
            return null;
        }
        public BookingEntity Entity(DXCType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);

            DataRow rowuser = dataSet.Tables["User"].NewRow();
            this.InitUserRow(rowuser, type);
            dataSet.Tables["User"].Rows.Add(rowuser);

            DataRow rowadmin = dataSet.Tables["Admin"].NewRow();
            this.InitAdminRow(rowadmin, Convert.ToInt32(rowuser["DocKey"]), type);
            dataSet.Tables["Admin"].Rows.Add(rowadmin);

            DataRow rowdriver = dataSet.Tables["Driver"].NewRow();
            this.InitDriverRow(rowdriver, Convert.ToInt32(rowuser["DocKey"]), type);
            dataSet.Tables["Driver"].Rows.Add(rowdriver);

            return new BookingEntity(this, dataSet, DXCAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new BookingHeaderDocKey());
        }
        public long DtlKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new BookingDetailLinesDtlKey());
        }
        public long DocKeyAdminUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new BookingHeaderAdminDocKey());
        }
        public long DocKeyDriverUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new BookingHeaderDriverDocKey());
        }
        public void InitUserRow(DataRow row, DXCType type)
        {
            row.BeginEdit();
            DateTime mydate = myDBSetting.GetServerTime();
            DataRow myrow = myDBSetting.GetDataTable("SELECT * FROM [dbo].[Master_User] WHERE USER_ID=? AND IS_ACTIVE_FLAG=1", false, myDBSession.LoginEmail).Rows[0];
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = "NEED APPROVAL";
            row["DocDate"] = mydate;           
            row["Note"] = DBNull.Value;
            row["Remark1"] = DBNull.Value;
            row["Remark2"] = DBNull.Value;
            row["Remark3"] = DBNull.Value;
            row["Remark4"] = DBNull.Value;
            row["EmployeeName"] = myrow["USER_NAME"].ToString();
            row["EmployeeCompanyName"] = "MNC Leasing";
            row["Status"] = "NEED APPROVAL";
            row["RequestStartTime"] = DBNull.Value;
            row["RequestFinishTime"] = DBNull.Value;
            row["RequestPickLoc"] = DBNull.Value;
            row["RequestDestLoc"] = DBNull.Value;
            row["RequestPickAddress"] = DBNull.Value;
            row["RequestDestAddress"] = DBNull.Value;
            row["Department"] = "";
            row["TripDetails"] = DBNull.Value;
            row["IsSettlement"] = "F";
            row["Hp"] = "0213910993";//myrow["Hp"].ToString();
            row["NeedApproval"] = "";
            row["Approver"] = "";
            row.EndEdit();
        }
        private void InitAdminRow(DataRow row, long sourcekey, DXCType type)
        {
            row.BeginEdit();
            DateTime mydate = myDBSetting.GetServerTime();
            row["DocKey"] = DocKeyAdminUniqueKey();
            row["SourceKey"] = sourcekey;
            row["DriverCode"] = DBNull.Value;
            row["DriverName"] = DBNull.Value;
            row["CarCode"] = DBNull.Value;
            row["CarType"] = DBNull.Value;
            row["CarLicensePlate"] = DBNull.Value;
            row["Remark"] = DBNull.Value;
            row["EstPickDateTime"] = DBNull.Value;
            row["EstArriveDateTime"] = DBNull.Value;
            row["AdminCode"] = DBNull.Value;
            row["AdminName"] = DBNull.Value;
            row["CreatedBy"] = DBNull.Value;
            row["CreatedDateTime"] = DBNull.Value;
            row["LastModifiedBy"] = DBNull.Value;
            row["LastModifiedDateTime"] = DBNull.Value;
            row.EndEdit();
        }
        private void InitDriverRow(DataRow row, long sourcekey, DXCType type)
        {
            row.BeginEdit();
            DateTime mydate = myDBSetting.GetServerTime();
            row["DocKey"] = DocKeyDriverUniqueKey();
            row["SourceKey"] = sourcekey;
            row["DriverName"] = DBNull.Value;
            row["ActualPickDateTime"] = DBNull.Value;
            row["ActualArriveDateTime"] = DBNull.Value;
            row["DriverRemark"] = DBNull.Value;
            row["LastModifiedBy"] = DBNull.Value;
            row["LastModifiedDateTime"] = DBNull.Value;
            row.EndEdit();
        }
        public BookingEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new BookingEntity(this, ds, DXCAction.Edit);
        }
        public BookingEntity Edit(long headerid, DXCAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public BookingEntity Grab(long headerid, DXCAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public BookingEntity Approve(long headerid, DXCAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public BookingEntity View(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalView(this.LoadData(headerid));
        }
        public BookingEntity Approve(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalApprove(this.LoadData(headerid));
        }
        public void UpdateAllMaster(DataTable sourceTable)
        {
            if (this.myDataTableAllMaster.PrimaryKey.Length != 0)
            {
                DataRow row = this.myDataTableAllMaster.Rows.Find(sourceTable.Rows[0]["DocKey"]) ?? this.myDataTableAllMaster.NewRow();
                foreach (DataColumn index1 in (InternalDataCollectionBase)row.Table.Columns)
                {
                    int index2 = sourceTable.Columns.IndexOf(index1.ColumnName);
                    if (index2 >= 0)
                        row[index1] = sourceTable.Rows[0][index2];
                }
                row.EndEdit();
                if (row.RowState == DataRowState.Detached)
                    this.myDataTableAllMaster.Rows.Add(row);
            }
        }
        public void DeleteAllMaster(long docKey)
        {
            if (myDataTableAllMaster.PrimaryKey.Length != 0)
            {
                DataRow dataRow = this.myDataTableAllMaster.Rows.Find((object)docKey);
                if (dataRow != null)
                    dataRow.Delete();
            }
        }
        private BookingEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["User"].Rows.Count == 0)
            {
                return (BookingEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["User"].Rows[0]["DocKey"]);
                return new BookingEntity(this, newDataSet, DXCAction.View);
            }
        }
        private BookingEntity InternalApprove(DataSet newDataSet)
        {
            if (newDataSet.Tables["User"].Rows.Count == 0)
            {
                return (BookingEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["User"].Rows[0]["DocKey"]);
                return new BookingEntity(this, newDataSet, DXCAction.Approve);
            }
        }
        private BookingEntity InternalEdit(DataSet newDataSet, DXCAction action)
        {
            if (newDataSet.Tables["User"].Rows.Count == 0)
            {
                return (BookingEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["User"].Rows[0]["DocKey"]);
                return new BookingEntity(this, newDataSet, action);
            }
        }
        public void SaveEntity(BookingEntity entity, string strDocName, SaveAction saveaction, string strUpline, string strID, string strName, string approver)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new EmptyDocNoException();


            SaveData(entity, entity.myDataSet, strDocName, saveaction, strUpline, strID, strName, approver);
            LoadBrowseTable(false, false, myDBSession.LoginUserID, "");
            try
            {
                if (myBrowseTable.Rows.Count > 0)
                {
                    DataRow r = myBrowseTable.Rows.Find(entity.DocKey);
                    if (r == null)
                    {
                        r = myBrowseTable.NewRow();
                        foreach (DataColumn col in entity.Bookingtable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.RowUser[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.Bookingtable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.RowUser[col];
                        }
                    }
                    myBrowseTable.AcceptChanges();
                }
            }
            catch { }
        }
        public void SaveCommentEntity(BookingEntity entity, SaveAction saveaction, string strID, string strComment)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new EmptyDocNoException();
            SaveComment(entity, saveaction, strID, strComment);
        }
        protected virtual DataSet LoadData(long headerid)
        {
            return null;
        }
        public virtual void Delete(long headerid)
        {
        }
        protected virtual void ClearBookingAdmin(DataSet ds)
        { }
        protected virtual void SaveData(BookingEntity Booking, DataSet ds, string strDocName, SaveAction saveaction, string strUpline, string userID, string userName, string approver)
        {
        }
        protected virtual void SaveDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void SaveBookingAdmin(DataSet ds, string username)
        { }
        protected virtual void SaveHistory(BookingEntity Booking, DataSet ds, SaveAction saveaction, string userID, string userName, DateTime myLastApprove, string myLastState)
        {
        }
        protected virtual void SaveComment(BookingEntity Booking, SaveAction saveaction, string myUserName, string myComment)
        { }
        protected virtual void DeleteWorkingList(BookingEntity Booking, string myID)
        { }
        protected virtual void UpdateWorkingList()
        { }
        protected virtual string GetNextStatus(string myStatus)
        {
            return null;
        }      
        protected virtual void ClearDetail(BookingEntity Booking, SaveAction saveaction)
        { }
        protected virtual void SendSMS(BookingEntity Booking, SaveAction saveaction)
        { }
        protected virtual void SendNotifEmail(BookingEntity Booking)
        { }
    }
}