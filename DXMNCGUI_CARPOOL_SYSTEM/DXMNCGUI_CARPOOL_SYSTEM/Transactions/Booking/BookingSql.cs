using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public class BookingSql : BookingDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, bool bViewAllperDept, string userID, string department)
        {
            if (!bViewAll)
            {
                myBrowseTable.Clear();
                myDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM dbo.Booking WHERE EmployeeName=? ORDER BY DocDate DESC", true, userID);
            }
            else
            {
                myBrowseTable.Clear();
                myDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM dbo.Booking ORDER BY DocDate DESC", true);
            }

            if (bViewAllperDept)
            {
                myBrowseTable.Clear();
                myDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM dbo.Booking WHERE EmployeeName=? OR Department=? ORDER BY DocDate DESC", true, userID, department);
            }

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadBrowseTableForDriver(string DriverName)
        {
            string sQuery = "";
            myBrowseTable.Clear();
            sQuery = @"SELECT A.* FROM dbo.Booking A
                        INNER JOIN [dbo].[BookingAdmin] B
                        ON A.DocKey = B.SourceKey
                        WHERE A.Status IN ('ON SCHEDULE', 'PICKUP') AND B.DriverName=? ORDER BY A.DocDate";
            myDBSetting.LoadDataTable(myBrowseTable, sQuery, true, DriverName);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadBrowseTableApproval(string sEmailAddress)
        {
            string sQuery = "";
            myBrowseTableApproval.Clear();
            sQuery = @"SELECT A.* FROM dbo.Booking A
                        WHERE A.Status IN ('WAITING APPROVAL') AND A.Approver=? ORDER BY A.DocDate";
            myDBSetting.LoadDataTable(myBrowseTableApproval, sQuery, true, sEmailAddress);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTableApproval.Columns["DocKey"];
            myBrowseTableApproval.PrimaryKey = keyHeader;
            return myBrowseTableApproval;
        }
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();

            DataTable myUserTable = new DataTable();
            DataTable myUserDetailTable = new DataTable();
            DataTable myAdminTable = new DataTable();
            DataTable myDriverTable = new DataTable();

            string sQueryUser = "SELECT * FROM [dbo].[Booking] WHERE DocKey=@DocKey";
            string sQueryUserDetail = "SELECT * FROM [dbo].[BookingDetail] WHERE DocKey=@DocKey ORDER BY Seq";
            string sQueryAdmin = "SELECT * FROM [dbo].[BookingAdmin] WHERE SourceKey=@SourceKey";
            string sQueryDriver = "SELECT * FROM [dbo].[BookingDriver] WHERE SourceKey=@SourceKey";

            using (SqlCommand cmduser = new SqlCommand(sQueryUser, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmduser);
                cmduser.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmduser.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myUserTable);
            }
            using (SqlCommand cmduserdetail = new SqlCommand(sQueryUserDetail, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmduserdetail);
                cmduserdetail.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmduserdetail.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myUserDetailTable);
            }
            using (SqlCommand cmdadmin = new SqlCommand(sQueryAdmin, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdadmin);
                cmdadmin.Parameters.Add("@SourceKey", SqlDbType.BigInt);
                cmdadmin.Parameters["@SourceKey"].Value = headerid;
                adapter.Fill(myAdminTable);
            }
            using (SqlCommand cmddriver = new SqlCommand(sQueryDriver, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmddriver);
                cmddriver.Parameters.Add("@SourceKey", SqlDbType.BigInt);
                cmddriver.Parameters["@SourceKey"].Value = headerid;
                adapter.Fill(myDriverTable);
            }
            myUserTable.TableName = "User";
            myUserDetailTable.TableName = "UserDetail";
            myAdminTable.TableName = "Admin";
            myDriverTable.TableName = "Driver";

            DataColumn[] keyUser = new DataColumn[1];
            keyUser[0] = myUserTable.Columns["DocKey"];
            myUserTable.PrimaryKey = keyUser;

            DataColumn[] keyUserDetail = new DataColumn[1];
            keyUserDetail[0] = myUserDetailTable.Columns["DtlKey"];
            myUserDetailTable.PrimaryKey = keyUserDetail;

            DataColumn[] keyAdmin = new DataColumn[1];
            keyAdmin[0] = myAdminTable.Columns["SourceKey"];
            myAdminTable.PrimaryKey = keyAdmin;

            DataColumn[] keyDriver = new DataColumn[1];
            keyDriver[0] = myDriverTable.Columns["SourceKey"];
            myDriverTable.PrimaryKey = keyDriver;

            dataSet.Tables.Add(myUserTable);
            dataSet.Tables.Add(myUserDetailTable);
            dataSet.Tables.Add(myAdminTable);
            dataSet.Tables.Add(myDriverTable);
            dataSet.Relations.Add("rlBookingDetail", myUserTable.Columns["DocKey"], myUserDetailTable.Columns["DocKey"]);
            dataSet.Relations.Add("rlBookingAdmin", myUserTable.Columns["DocKey"], myAdminTable.Columns["SourceKey"]);
            dataSet.Relations.Add("rlBookingDriver", myUserTable.Columns["DocKey"], myDriverTable.Columns["SourceKey"]);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlDBSetting dbSetting = this.myDBSetting.StartTransaction();
            try
            {
                dbSetting.ExecuteNonQuery("DELETE FROM [dbo].[Application] WHERE DocKey=?", (object)headerid);
                dbSetting.Commit();

            }
            catch (SqlException ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                dbSetting.EndTransaction();
            }
        }
        protected override void SaveData(BookingEntity Booking, DataSet ds, string strDocName, SaveAction saveaction, string strUpline, string userID, string userName)
        {
            SqlDBSetting dbSetting = this.myDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(dbSetting.ConnectionString);
            DateTime Mydate = myDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["User"].Rows[0];
            DataRow dataAdminRow = ds.Tables["Admin"].Rows[0];
            DataRow dataDriverRow = ds.Tables["Driver"].Rows[0];
            try
            {
                dbSetting.StartTransaction();
                if (saveaction == SaveAction.Save)
                {
                    if (dataRow["DocNo"].ToString().ToUpper() == "NEW")
                    {
                        DataRow[] myrowDocNo = dbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='BK'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), DBSetting.GetServerTime());
                            dbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType=?", strDocName);
                        }
                    }
                    if(dataRow["NeedApproval"].ToString() == "T")
                    {
                        dataRow["Status"] = "WAITING APPROVAL";
                    }
                }
                if (saveaction == SaveAction.Cancel)
                {
                    dataRow["Status"] = "CANCELLED";
                    dataRow["Cancelled"] = "T";
                    dataRow["CancelledBy"] = userName;
                    dataRow["CancelledDateTime"] = Mydate;
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }

                if (saveaction == SaveAction.HoldByAdmin)
                {
                    dataRow["Status"] = "HOLD BY DISPATCHER";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.RejectByAdmin)
                {
                    dataRow["Status"] = "REJECTED BY DISPATCHER";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.ApproveByAdmin)
                {
                    dataRow["Status"] = "ON SCHEDULE";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                    SendSMS(Booking, saveaction);
                }

                if (saveaction == SaveAction.PickupByDriver)
                {
                    dataRow["Status"] = "PICKUP";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;

                    dataDriverRow["DriverName"] = userName;
                    dataDriverRow["ActualPickDateTime"] = Mydate;
                    dataDriverRow["LastModifiedBy"] = userName;
                    dataDriverRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.RejectByDriver)
                {
                    dataRow["Status"] = "REJECTED BY DRIVER";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;

                    dataDriverRow["LastModifiedBy"] = userName;
                    dataDriverRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.FinishByDriver)
                {
                    dataRow["Status"] = "FINISH";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;

                    dataDriverRow["DriverName"] = userName;
                    dataDriverRow["ActualArriveDateTime"] = Mydate;
                    dataDriverRow["LastModifiedBy"] = userName;
                    dataDriverRow["LastModifiedDateTime"] = Mydate;

                    dbSetting.ExecuteNonQuery("UPDATE [dbo].[MasterCar] SET Kilometer=? WHERE CarLicense=?", (object)dataDriverRow["CurrentKilometer"], (object)dataAdminRow["CarLicensePlate"]);
                }

                if (Booking.DocKey != null)
                {
                    ClearDetail(Booking, saveaction);
                }
                dbSetting.SimpleSaveDataTable(ds.Tables["User"], "SELECT * FROM [dbo].[Booking]");
                dbSetting.SimpleSaveDataTable(ds.Tables["Admin"], "SELECT * FROM [dbo].[BookingAdmin]");
                dbSetting.SimpleSaveDataTable(ds.Tables["Driver"], "SELECT * FROM [dbo].[BookingDriver]");
                SaveDetail(ds, saveaction);

                Booking.strErrorGenTicket = "null";
                if (Booking.strErrorGenTicket == "null")
                {
                    dbSetting.Commit();
                }
                else
                {
                    dbSetting.Rollback();
                    throw new ArgumentException(Booking.strErrorGenTicket);
                }
            }
            catch (SqlException ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                dbSetting.EndTransaction();
            }
        }
        protected override void SaveDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(DBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["UserDetail"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[BookingDetail] (DtlKey, DocKey, Seq, Name, Gender, Status, Remark1, Remark2, Remark3) VALUES (@DtlKey, @DocKey, @Seq, @Name, @Gender ,@Status, @Remark1, @Remark2, @Remark3)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    var varRemark1 = dataRow["Remark1"];
                    var varRemark2 = dataRow["Remark2"];
                    var varRemark3 = dataRow["Remark3"];

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DtlKey", SqlDbType.Int);
                    sqlParameter1.Value = dataRow.Field<int>("DtlKey");
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                    sqlParameter2.Value = dataRow.Field<int>("DocKey");
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
                    sqlParameter3.Value = dataRow.Field<int>("Seq");
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                    sqlParameter4.Value = dataRow.Field<string>("Name");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 1);
                    sqlParameter5.Value = dataRow.Field<string>("Gender");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
                    sqlParameter6.Value = dataRow.Field<string>("Status");
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
        protected override void SaveHistory(BookingEntity Booking, DataSet ds, SaveAction saveaction, string userID, string userName, DateTime myLastApprove, string myLastState)
        {
            int imyDiffTime;
            DateTime Mydate = myDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationHistory] (DocKey, Status, TransByID, TransBy, TransDate, DiffTime, FromStatus) VALUES (@DocKey, @Status, @TransByID, @TransBy, @TransDate ,@DiffTime, @FromStatus)");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = Booking.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 10);
                sqlParameter2.Value = Booking.Status;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TransByID", SqlDbType.NVarChar, 20);
                sqlParameter3.Value = userID;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@TransBy", SqlDbType.NVarChar, 20);
                sqlParameter4.Value = userName;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@TransDate", SqlDbType.DateTime);
                sqlParameter5.Value = Mydate;
                sqlParameter5.Direction = ParameterDirection.Input;

                imyDiffTime = Convert.ToInt32((Mydate - myLastApprove).TotalMinutes);
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@DiffTime", SqlDbType.Int);
                sqlParameter6.Value = imyDiffTime;
                sqlParameter6.Direction = ParameterDirection.Input;

                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@FromStatus", SqlDbType.NVarChar, 10);
                sqlParameter7.Value = myLastState;
                sqlParameter7.Direction = ParameterDirection.Input;

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
        protected override void SaveComment(BookingEntity Booking, SaveAction saveaction, string userFullName, string userComment)
        {
            DateTime Mydate = myDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationCommentHistory] (SourceDocKey, DocNo, CommentBy, CommentNote, CommentDate) VALUES (@SourceDocKey, @DocNo, @CommentBy, @CommentNote, @CommentDate)");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@SourceDocKey", SqlDbType.Int);
                sqlParameter1.Value = Booking.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.NVarChar, 20);
                sqlParameter2.Value = Booking.DocNo;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@CommentBy", SqlDbType.NVarChar, 20);
                sqlParameter3.Value = userFullName;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CommentNote", SqlDbType.NVarChar);
                sqlParameter4.Value = userComment;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CommentDate", SqlDbType.DateTime);
                sqlParameter5.Value = Mydate;
                sqlParameter5.Direction = ParameterDirection.Input;
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
        protected override void DeleteWorkingList(BookingEntity Booking, string myID)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE WorkList WHERE Source=@Source AND NeedApproveByID=@NeedApproveByID");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@Source", SqlDbType.NVarChar, 100);
                sqlParameter1.Value = Booking.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@NeedApproveByID", SqlDbType.NVarChar, 20);
                sqlParameter2.Value = myID;
                sqlParameter2.Direction = ParameterDirection.Input;
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
        protected override void UpdateWorkingList()
        {
            try
            {
                DBSetting.ExecuteNonQuery("UPDATE dbo.WorkList SET WorkList.Source = (SELECT DocKey FROM dbo.ChangeDataList WHERE WorkList.TicketNo=ChangeDataList.TicketNo)");
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
            }
        }
        protected override string GetNextStatus(string myLastStatus)
        {
            string myNextStatus = "";
            try
            {
                object obj = null;
                obj = myDBSetting.ExecuteScalar("SELECT A.StateDescription FROM [dbo].[ApplicationWorkflowScheme] A WHERE A.Seq = (SELECT Seq + 1 FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=?)", myLastStatus);
                if (obj != null && obj != DBNull.Value)
                {
                    myNextStatus = obj.ToString();
                }
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

            }
            return myNextStatus;
        }
        protected override void ClearDetail(BookingEntity Booking, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[BookingDetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = Booking.DocKey;
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
        protected override void SendSMS(BookingEntity Booking, SaveAction saveaction)
        {
            string sRedaksi1 = "Hallo Customer, Booking anda sudah diproses oleh dispatcher : " + Booking.DocNo.ToString() + ", terima kasih.";
            string sAddress1 = @"http://" + "www.etracker.cc/bulksms/mesapi.aspx?user=MNCLEASING&pass=P@$$w0rD@MNC&type=0&to=" + Booking.Hp.ToString() + "&from=MNC%20LEASING&text=" + sRedaksi1 + "&servid=MNC001";
            new WebClient().DownloadString(sAddress1);
            myDBSetting.ExecuteNonQuery("INSERT INTO SMSHist VALUES (?,?,?,?)", (object)Booking.DocNo, (object)myDBSetting.GetServerTime(), (object)Booking.Hp, (object)sRedaksi1);

            object obj = myDBSetting.ExecuteScalar("select top 1 Hp from MasterUser WHERE user_name=?", (object)Booking.AdminDriverName);
            if (obj != null && obj != DBNull.Value)
            {
                string sRedaksi2 = "Hallo Driver, anda mendapatkan order " + Booking.DocNo.ToString() + "Silahkan buka aplikasi carpool untuk mengetahui detail trip.";
                string sAddress2 = @"http://" + "www.etracker.cc/bulksms/mesapi.aspx?user=MNCLEASING&pass=P@$$w0rD@MNC&type=0&to=" + obj.ToString() + "&from=MNC%20LEASING&text=" + sRedaksi2 + "&servid=MNC001";
                new WebClient().DownloadString(sAddress2);
                myDBSetting.ExecuteNonQuery("INSERT INTO SMSHist VALUES (?,?,?,?)", (object)Booking.DocNo, (object)myDBSetting.GetServerTime(), (object)obj, (object)sRedaksi2);
            }
        }
    }
}