using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement
{
    public class SettlementDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableApproval;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;
        internal SettlementDB()
        {
            myBrowseTable = new DataTable();
            myBrowseTableApproval = new DataTable();
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
        public static SettlementDB Create(SqlDBSetting dbSetting, SqlDBSession dbSession, SqlLocalDBSetting localdbsetting)
        {
            SettlementDB aSettlement = (SettlementDB)null; ;
            aSettlement = new SettlementSql();
            aSettlement.myDBSetting = dbSetting;
            aSettlement.myLocalDBSetting = localdbsetting;
            aSettlement.myDBSession = dbSession;
            return aSettlement;
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
        public virtual void Sendmail(string strapprovalID, string strapprovalName, SettlementEntity Settlement, string strsubject, string strbody, SqlDBSetting dbsetting, bool bsender, bool breject, string strrejectnote, string traveltype, Int64 itravelKey)
        {
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public virtual DataTable LoadBrowseTableApproval(string UserName)
        {
            return null;
        }
        public SettlementEntity Entity(DXCType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);

            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitUserRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);

            return new SettlementEntity(this, dataSet, DXCAction.New);
        }
        public SettlementEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new SettlementEntity(this, ds, DXCAction.Edit);
        }
        public SettlementEntity Edit(long headerid, DXCAction action)
        {
            myDBReg = DBRegistry.Create(myDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public SettlementEntity Grab(long headerid, DXCAction action)
        {
            myDBReg = DBRegistry.Create(myDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public SettlementEntity Approve(long headerid, DXCAction action)
        {
            myDBReg = DBRegistry.Create(myDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public SettlementEntity View(long headerid)
        {
            myDBReg = DBRegistry.Create(myDBSetting);
            return this.InternalView(this.LoadData(headerid));
        }
        private SettlementEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (SettlementEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new SettlementEntity(this, newDataSet, DXCAction.View);
            }
        }
        private SettlementEntity InternalEdit(DataSet newDataSet, DXCAction action)
        {
            if (newDataSet.Tables["User"].Rows.Count == 0)
            {
                return (SettlementEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["User"].Rows[0]["DocKey"]);
                return new SettlementEntity(this, newDataSet, action);
            }
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new SettlementHeaderDocKey());
        }
        public long DtlKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new SettlementDetailLinesDtlKey());
        }
        private void InitUserRow(DataRow row, DXCType type)
        {
            row.BeginEdit();
            DateTime mydate = myDBSetting.GetServerTime();
            DataRow myrow = myDBSetting.GetDataTable("SELECT * FROM [dbo].[Master_User] WHERE USER_ID=? AND IS_ACTIVE_FLAG=1", false, myDBSession.LoginEmail).Rows[0];
            row["DocKey"] = DocKeyUniqueKey();
            row["SourceKey"] = DBNull.Value;
            row["DocNo"] = "NEED APPROVAL";
            row["DocDate"] = mydate;
            row["BookNo"] = DBNull.Value;
            row["BookDate"] = DBNull.Value;
            row["BookCompany"] = DBNull.Value;
            row["BookBy"] = DBNull.Value;
            row["BookDept"] = DBNull.Value;
            row["BookSeatNumber"] = DBNull.Value;
            row["BookPickupLoc"] = DBNull.Value;
            row["BookDestinationLoc"] = DBNull.Value;
            row["BookPickupAddress"] = DBNull.Value;
            row["BookDestinantionAddress"] = DBNull.Value;
            row["BookTripDetail"] = DBNull.Value;
            row["BookActPickupDateTime"] = DBNull.Value;
            row["BookActArrivalDateTime"] = DBNull.Value;
            row["BookDriver"] = DBNull.Value;
            row["BookCarType"] = DBNull.Value;
            row["BookCarLicense"] = DBNull.Value;
            row["Total"] = 0;
            row["CreatedBy"] = myrow["USER_NAME"].ToString();
            row["CreatedDateTime"] = mydate;
            row["LastModifiedBy"] = myrow["USER_NAME"].ToString();
            row["LastModifiedDateTime"] = mydate;
            row["Cancelled"] = "F";
            row["CancelledBy"] = DBNull.Value;
            row["CancelledDateTime"] = DBNull.Value;
            row["CancelledReason"] = DBNull.Value;
            row["NeedApproval"] = "F";
            row["Approver"] = DBNull.Value;
            row["Status"] = "NEED APPROVAL";
            row.EndEdit();
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
        public void SaveEntity(SettlementEntity entity, string strDocName, SaveAction saveaction, string strUserName, string approver)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new EmptyDocNoException();


            SaveData(entity, entity.myDataSet, strDocName, saveaction, strUserName, approver);
            LoadBrowseTable(false, myDBSession.LoginUserID);
            try
            {
                if (myBrowseTable.Rows.Count > 0)
                {
                    DataRow r = myBrowseTable.Rows.Find(entity.DocKey);
                    if (r == null)
                    {
                        r = myBrowseTable.NewRow();
                        foreach (DataColumn col in entity.SettlementTable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.SettlementTable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                    }
                    myBrowseTable.AcceptChanges();
                }
            }
            catch { }
        }
        protected virtual DataSet LoadData(long headerid)
        {
            return null;
        }
        public virtual void Delete(long headerid)
        {
        }
        protected virtual void SaveData(SettlementEntity Settlement, DataSet ds, string strDocName, SaveAction saveaction, string strUserName, string approver)
        { }
        protected virtual void SaveDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void UpdateBookingData(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void ClearDetail(SettlementEntity Settlement, SaveAction saveaction)
        { }
    }
}