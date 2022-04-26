using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry;
using System;
using System.Data;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement
{
    public class SettlementEntity
    {
        private SettlementDB mySettlementCommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DXCAction myAction;
        private DXCType myDocType;
        public string strErrorGenTicket;
        internal DataRow Row
        {
            get { return myRow; }
        }
        public SettlementDB Settlementcommand
        {
            get
            {
                return this.mySettlementCommand;
            }
        }
        public DataTable DataTableHeader
        {
            get
            {
                return this.myHeaderTable;
            }
        }
        public DataTable DataTableDetail
        {
            get
            {
                return this.myDetailTable;
            }
        }
        public DataSet ApplicationDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }
        public SettlementEntity(SettlementDB aSettlement, DataSet ds, DXCAction action)
        {
            mySettlementCommand = aSettlement;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Lines"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
            this.myDetailTable.RowChanged += new DataRowChangeEventHandler(this.myDetailTable_RowChanged);
            this.myDetailTable.ColumnChanged += new DataColumnChangeEventHandler(this.DetailDataColumnChangeEventHandler);
            this.myDetailTable.RowDeleting += new DataRowChangeEventHandler(this.myDetailTable_RowDeleting);
            this.myDetailTable.RowDeleted += new DataRowChangeEventHandler(this.DetailDataRowDeletedEventHandler);
            this.myDetailTable.ColumnChanging += new DataColumnChangeEventHandler(this.myDetailTable_ColumnChanging);
        }

        private void myHeaderTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myDetailTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {

        }
        private void myDetailTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myDetailTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {

        }
        private void DetailDataRowDeletedEventHandler(object sender, DataRowChangeEventArgs e)
        {
        }
        private void DetailDataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e)
        {
        }

        public DataTable LoadDocNoFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = mySettlementCommand.DBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='ST'", false);
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
                return this.myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }

        public void Save(string userID, string userName, string strDocName, SaveAction saveaction, bool IsApproval)
        {
            if (saveaction == SaveAction.Cancel)
            {
                this.myAction = DXCAction.Cancel;
            }
            if (saveaction == SaveAction.Approve)
            {
                this.myRow["Status"] = "APPROVED";
            }
            if (saveaction == SaveAction.Reject)
            {
                this.myRow["Status"] = "REJECTED";
            }
            {
                bool flag = this.myRow.RowState != DataRowState.Unchanged;
                foreach (DataRow dataRow in this.ValidDetailLinesRows)
                {
                    if (!flag && dataRow.RowState != DataRowState.Unchanged)
                        flag = true;
                }
                if (!flag && this.myDetailTable.Select("", "Seq", DataViewRowState.Deleted).Length > 0)
                    flag = true;
                if (flag)
                {
                    this.myRow["LastModifiedBy"] = userName;
                    this.myRow["LastModifiedDateTime"] = (object)this.mySettlementCommand.DBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedUser"];
                    this.myRow.EndEdit();
                    mySettlementCommand.SaveEntity(this, strDocName, saveaction, userName, IsApproval);
                }
                this.myAction = DXCAction.View;
            }
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
                throw new Exception("Cannot Delete read-only Settlement");
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
                throw new Exception("Cannot edit read-only Settlement");
            }
            else
            {
                foreach (DataRow dataRow in this.ValidDetailLinesRows)
                    dataRow.Delete();
            }
        }
        public long DtlKeyUniqueKey()
        {
            return mySettlementCommand.DBReg.IncOne((Controllers.Registry.IRegistryID)new SettlementDetailLinesDtlKey());
        }
        public SettlementLinesRecord AddLines()
        {
            if (this.myAction == DXCAction.View)
                throw new Exception("Cannot edit read-only Detail Lines");
            else
                return this.InternalAddLines(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private SettlementLinesRecord InternalAddLines(int seq)
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = mySettlementCommand.DBSetting.GetServerTime();
            string iUserID = mySettlementCommand.DBSession.LoginUserID;
            row["DtlKey"] = (object)mySettlementCommand.DtlKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["Seq"] = (object)seq;
            row["Condition"] = DBNull.Value;
            row["ItemDescription"] = DBNull.Value;
            row["Year"] = myDate.Year;
            row["UnitPrice"] = 0;
            row["Qty"] = 0;
            row["SubTotal"] = 0;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new SettlementLinesRecord(row, this);
        }
        public bool IsModified()
        {
            if (this.myDetailTable.GetChanges() != null)
                return true;
            else
                return false;
        }

        #region SETTLEMENT ENTITY
        public object DocKey
        {
            get { return myRow["DocKey"]; }
            set { myRow["DocKey"] = value; }
        }
        public object SourceKey
        {
            get { return myRow["SourceKey"]; }
            set { myRow["SourceKey"] = value; }
        }
        public object DocNo
        {
            get { return myRow["DocNo"]; }
            set { myRow["DocNo"] = value; }
        }
        public object DocDate
        {
            get { return myRow["DocDate"]; }
            set { myRow["DocDate"] = value; }
        }
        public object BookNo
        {
            get { return myRow["BookNo"]; }
            set { myRow["BookNo"] = value; }
        }
        public object BookDate
        {
            get { return myRow["BookDate"]; }
            set { myRow["BookDate"] = value; }
        }
        public object BookCompany
        {
            get { return myRow["BookCompany"]; }
            set { myRow["BookCompany"] = value; }
        }
        public object BookBy
        {
            get { return myRow["BookBy"]; }
            set { myRow["BookBy"] = value; }
        }
        public object BookDept
        {
            get { return myRow["BookDept"]; }
            set { myRow["BookDept"] = value; }
        }
        public object BookType
        {
            get { return myRow["BookType"]; }
            set { myRow["BookType"] = value; }
        }
        public object BookSeatNumber
        {
            get { return myRow["BookSeatNumber"]; }
            set { myRow["BookSeatNumber"] = value; }
        }
        public object BookPickupLoc
        {
            get { return myRow["BookPickupLoc"]; }
            set { myRow["BookPickupLoc"] = value; }
        }
        public object BookDestinationLoc
        {
            get { return myRow["BookDestinationLoc"]; }
            set { myRow["BookDestinationLoc"] = value; }
        }
        public object BookPickupAddress
        {
            get { return myRow["BookPickupAddress"]; }
            set { myRow["BookPickupAddress"] = value; }
        }
        public object BookDestinantionAddress
        {
            get { return myRow["BookDestinantionAddress"]; }
            set { myRow["BookDestinantionAddress"] = value; }
        }
        public object BookTripDetail
        {
            get { return myRow["BookTripDetail"]; }
            set { myRow["BookTripDetail"] = value; }
        }
        public object BookActPickupDateTime
        {
            get { return myRow["BookActPickupDateTime"]; }
            set { myRow["BookActPickupDateTime"] = value; }
        }
        public object BookActArrivalDateTime
        {
            get { return myRow["BookActArrivalDateTime"]; }
            set { myRow["BookActArrivalDateTime"] = value; }
        }
        public object BookDriver
        {
            get { return myRow["BookDriver"]; }
            set { myRow["BookDriver"] = value; }
        }
        public object BookCarType
        {
            get { return myRow["BookCarType"]; }
            set { myRow["BookCarType"] = value; }
        }
        public object BookCarLicense
        {
            get { return myRow["BookCarLicense"]; }
            set { myRow["BookCarLicense"] = value; }
        }
        public object Total
        {
            get { return myRow["Total"]; }
            set { myRow["Total"] = value; }
        }
        public object CreatedBy
        {
            get { return myRow["CreatedBy"]; }
            set { myRow["CreatedBy"] = value; }
        }
        public object CreatedDateTime
        {
            get { return myRow["CreatedDateTime"]; }
            set { myRow["CreatedDateTime"] = value; }
        }
        public object LastModifiedBy
        {
            get { return myRow["LastModifiedBy"]; }
            set { myRow["LastModifiedBy"] = value; }
        }
        public object LastModifiedDateTime
        {
            get { return myRow["LastModifiedDateTime"]; }
            set { myRow["LastModifiedDateTime"] = value; }
        }
        public object Cancelled
        {
            get { return myRow["Cancelled"]; }
            set { myRow["Cancelled"] = value; }
        }
        public object CancelledBy
        {
            get { return myRow["CancelledBy"]; }
            set { myRow["CancelledBy"] = value; }
        }
        public object CancelledDateTime
        {
            get { return myRow["CancelledDateTime"]; }
            set { myRow["CancelledDateTime"] = value; }
        }
        public object CancelledReason
        {
            get { return myRow["CancelledReason"]; }
            set { myRow["CancelledReason"] = value; }
        }
        public object NeedApproval
        {
            get { return myRow["NeedApproval"]; }
            set { myRow["NeedApproval"] = value; }
        }
        public object Approver
        {
            get { return myRow["Approver"]; }
            set { myRow["Approver"] = value; }
        }
        public object Status
        {
            get { return myRow["Status"]; }
            set { myRow["Status"] = value; }
        }
        #endregion

        public DataTable SettlementTable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}