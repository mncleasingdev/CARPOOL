using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement
{
    public class SettlementSql : SettlementDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, @"SELECT a.approver + ' - ' + ISNULL(b.DESCS,'') [NextApprover],* FROM Settlement a
                                                                LEFT JOIN IFINANCING_GOLIVE..SYS_TBLEMPLOYEE B ON A.APPROVER = b.CODE
                                                                WHERE CreatedBy=? AND BookNo is not null ORDER BY DocDate DESC", true, userID);
            }
            else
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, @"SELECT a.approver + ' - ' + ISNULL(b.DESCS,'') [NextApprover],* FROM Settlement a
                                                                LEFT JOIN IFINANCING_GOLIVE..SYS_TBLEMPLOYEE B ON A.APPROVER = b.CODE
                                                                WHERE BookNo is not null ORDER BY DocDate DESC", true);
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadBrowseTableApproval(string sEmailAddress)
        {
            string sQuery = "";
            myBrowseTableApproval.Clear();
            sQuery = @"SELECT A.* FROM dbo.Settlement A
                        WHERE A.Status IN ('NEED APPROVAL') AND A.Approver=? AND BookNo is not null ORDER BY A.DocDate";
            myLocalDBSetting.LoadDataTable(myBrowseTableApproval, sQuery, true, sEmailAddress);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTableApproval.Columns["DocKey"];
            myBrowseTableApproval.PrimaryKey = keyHeader;
            return myBrowseTableApproval;
        }
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myDetailTable = new DataTable();
            string sSQLHeader = "SELECT * FROM [dbo].[Settlement] WHERE DocKey=@DocKey";
            string sSQLLines = "SELECT * FROM [dbo].[SettlementDetail] WHERE DocKey=@DocKey ORDER BY Seq";
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            using (SqlCommand cmdlines = new SqlCommand(sSQLLines, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdlines);
                cmdlines.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdlines.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myDetailTable);
            }
            myHeaderTable.TableName = "Header";
            myDetailTable.TableName = "Lines";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;
            DataColumn[] keyLines = new DataColumn[1];
            keyLines[0] = myDetailTable.Columns["DtlKey"];
            myDetailTable.PrimaryKey = keyLines;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myDetailTable);
            dataSet.Relations.Add("rlApplicationDetail", myHeaderTable.Columns["DocKey"], myDetailTable.Columns["DocKey"]);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                localdbSetting.ExecuteNonQuery("DELETE FROM [dbo].[Settlement] WHERE DocKey=?", (object)headerid);
                localdbSetting.Commit();

            }
            catch (SqlException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                localdbSetting.EndTransaction();
            }
        }
        protected override void SaveData(SettlementEntity Settlement, DataSet ds, string strDocName, SaveAction saveaction, string strUserName, string approver)
        {
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(localdbSetting.ConnectionString);
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                localdbSetting.StartTransaction();
                if (saveaction == SaveAction.Save)
                {
                    //if (dataRow["DocNo"].ToString().ToUpper() == "NEED APPROVAL")
                    //{
                        dataRow["DocDate"] = Mydate;
                        DataRow[] myrowDocNo = localdbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='ST'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), DBSetting.GetServerTime());
                            localdbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType=?", strDocName);
                        }
                    //}
                    //if (dataRow["Status"].ToString().ToUpper() == "NEW" && IsApproval)
                    //{
                    //    dataRow["Status"] = "WAITING APPROVAL";
                    //}
                }
                if (saveaction == SaveAction.Save)
                {
                    dataRow["Approver"] = approver;
                }
                if (saveaction == SaveAction.Cancel)
                {
                    dataRow["Status"] = "CANCELLED";
                    dataRow["Cancelled"] = "T";
                    dataRow["CancelledBy"] = strUserName;
                    dataRow["CancelledDateTime"] = Mydate;
                    dataRow["LastModifiedBy"] = strUserName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.Approve)
                {
                    //dataRow["Status"] = "APPROVED BY SUPERIOR";
                    dataRow["Status"] = "COMPLETE";
                    dataRow["LastModifiedBy"] = strUserName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                    
                }
                if (saveaction == SaveAction.Reject)
                {
                    //dataRow["Status"] = "REJECTED BY SUPERIOR";
                    dataRow["Status"] = "REJECTED";
                    dataRow["LastModifiedBy"] = strUserName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (Settlement.DocKey != null && saveaction == SaveAction.Approve)
                {
                    ClearDetail(Settlement, saveaction);
                }
                localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Settlement]");
                SaveDetail(ds, saveaction);
                if (saveaction == SaveAction.Approve)
                {
                    UpdateBookingData(ds, saveaction);
                }

                Settlement.strErrorGenTicket = "null";
                if (Settlement.strErrorGenTicket == "null")
                {
                    localdbSetting.Commit();
                }
                else
                {
                    localdbSetting.Rollback();
                    throw new ArgumentException(Settlement.strErrorGenTicket);
                }
            }
            catch (SqlException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                localdbSetting.EndTransaction();
            }
        }
        protected override void SaveDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Lines"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[SettlementDetail] (DtlKey, DocKey, Seq, ItemCode, ItemDesc, Note, Remark1, Remark2, Remark3, Remark4, Image, Qty, UnitPrice, SubTotal) VALUES (@DtlKey, @DocKey, @Seq, @ItemCode, @ItemDesc ,@Note, @Remark1, @Remark2, @Remark3, @Remark4, @Image, @Qty, @UnitPrice, @SubTotal)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    var varNote = dataRow["Note"];
                    var varRemark1 = dataRow["Remark1"];
                    var varRemark2 = dataRow["Remark2"];
                    var varRemark3 = dataRow["Remark3"];
                    var varRemark4 = dataRow["Remark4"];

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DtlKey", SqlDbType.Int);
                    sqlParameter1.Value = dataRow.Field<int>("DtlKey");
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                    sqlParameter2.Value = dataRow.Field<int>("DocKey");
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
                    sqlParameter3.Value = dataRow.Field<int>("Seq");
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@ItemCode", SqlDbType.NVarChar, 50);
                    sqlParameter4.Value = dataRow.Field<string>("ItemCode");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@ItemDesc", SqlDbType.NVarChar, 100);
                    sqlParameter5.Value = dataRow.Field<string>("ItemDesc");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Note", SqlDbType.NVarChar, 50);
                    sqlParameter6.Value = varNote;
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Remark1", SqlDbType.NVarChar);
                    sqlParameter7.Value = varRemark1;
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Remark2", SqlDbType.NVarChar);
                    sqlParameter8.Value = varRemark2;
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@Remark3", SqlDbType.NVarChar);
                    sqlParameter9.Value = varRemark3;
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@Remark4", SqlDbType.NVarChar);
                    sqlParameter10.Value = varRemark4;
                    sqlParameter10.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@Image", SqlDbType.Image, 1);
                    sqlParameter11.Value = DBNull.Value; //dataRow.Field<Image>("Image");
                    sqlParameter11.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@Qty", SqlDbType.Float);
                    sqlParameter12.Value = dataRow.Field<Decimal>("Qty");
                    sqlParameter12.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@UnitPrice", SqlDbType.Float);
                    sqlParameter13.Value = dataRow.Field<Decimal>("UnitPrice");
                    sqlParameter13.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@SubTotal", SqlDbType.Float);
                    sqlParameter14.Value = dataRow.Field<Decimal>("SubTotal");
                    sqlParameter14.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
                }
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
        protected override void UpdateBookingData(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Header"].Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("UPDATE [dbo].[Booking] SET IsSettlement=@IsSettlement, Status=@Status WHERE DocNo=@BookNo");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@BookNo", SqlDbType.NVarChar, 20);
                    sqlParameter1.Value = dataRow.Field<string>("BookNo");
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@IsSettlement", SqlDbType.NVarChar, 1);
                    sqlParameter2.Value = saveaction == SaveAction.Approve ? "T" : "F";
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 60);
                    sqlParameter3.Value = "COMPLETE";
                    sqlParameter3.Direction = ParameterDirection.Input;
                    sqlCommand.ExecuteNonQuery();
                }
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
        protected override void ClearDetail(SettlementEntity Settlement, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[SettlementDetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = Settlement.DocKey;
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
                myconn.Dispose();
            }
        }
    }
}