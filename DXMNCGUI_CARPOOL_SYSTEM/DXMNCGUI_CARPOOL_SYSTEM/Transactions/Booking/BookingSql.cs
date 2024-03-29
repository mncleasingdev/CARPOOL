﻿using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking
{
    public class BookingSql : BookingDB
    {
        protected AccesRight accessright
        {
            get { return (AccesRight)HttpContext.Current.Session["accessright"]; }
            set { HttpContext.Current.Session["accessright"] = value; }
        }

        public override DataTable LoadBrowseTable(bool bViewAll, bool bViewAllperDept, string userID, string department)
        {
            if (!bViewAll)
            {
                myBrowseTable.Clear();
                myLocalDBSetting.LoadDataTable(myBrowseTable, @"SELECT CASE WHEN a.approver + '-' + ISNULL(b.DESCS,'') ='IS_GA-' THEN 'GENERAL AFFAIR'
                                                                ELSE a.approver + ' - ' + ISNULL(b.DESCS,'') END [NextApprover],* 
                                                                FROM dbo.Booking a
                                                                left join [MSSQLSRVGUI].IFINANCING_GOLIVE.DBO.SYS_TBLEMPLOYEE B ON A.APPROVER = b.CODE                                                                
                                                                WHERE EmployeeName='" + userID + "' OR b.DESCS='" + userID + "' ORDER BY DocDate DESC", true);
            }
            else
            {
                myBrowseTable.Clear();
                myLocalDBSetting.LoadDataTable(myBrowseTable, @"SELECT CASE WHEN a.approver + '-' + ISNULL(b.DESCS,'') ='IS_GA-' THEN 'GENERAL AFFAIR'
                                                                ELSE a.approver + ' - ' + ISNULL(b.DESCS,'') END [NextApprover],* 
                                                                FROM dbo.Booking a
                                                                left join [MSSQLSRVGUI].IFINANCING_GOLIVE.DBO.SYS_TBLEMPLOYEE B ON A.APPROVER = b.CODE                                
                                                                ORDER BY DocDate DESC", true);
            }

            if (bViewAllperDept)
            {
                myBrowseTable.Clear();
                myLocalDBSetting.LoadDataTable(myBrowseTable, @"SELECT CASE WHEN a.approver + '-' + ISNULL(b.DESCS,'') ='IS_GA-' THEN 'GENERAL AFFAIR'
                                                                ELSE a.approver + ' - ' + ISNULL(b.DESCS, '') END[NextApprover], *
                                                                FROM dbo.Booking a
                                                                left join [MSSQLSRVGUI].IFINANCING_GOLIVE.DBO.SYS_TBLEMPLOYEE B ON A.APPROVER = b.CODE
                                                                WHERE EmployeeName =? OR Department =?
                                                                ORDER BY DocDate DESC", true, userID, department);
            }

            //DataColumn[] keyHeader = new DataColumn[1];
            //keyHeader[0] = myBrowseTable.Columns["DocKey"];
            //myBrowseTable.PrimaryKey = keyHeader;
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
            myLocalDBSetting.LoadDataTable(myBrowseTable, sQuery, true, DriverName);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadBrowseTableApproval(string UserCode)
        {
            string sQuery = "";
            string cmdID = "";
            myBrowseTableApproval.Clear();
            if (accessright.IsAccessibleByUserID(UserCode, "IS_GA"))
            {
                cmdID = "IS_GA";
            }

            sQuery = "EXEC GetApprovalBooking ?, ?";
            myLocalDBSetting.LoadDataTable(myBrowseTableApproval, sQuery, true, UserCode, cmdID);

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTableApproval.Columns["DocKey"];
            myBrowseTableApproval.PrimaryKey = keyHeader;
            return myBrowseTableApproval;
        }

        public override DataTable LoadBrowseTableSchedule(string UserCode)
        {
            string sQuery = "";
            sQuery = "EXEC getOnScheduleBook ?";
            myLocalDBSetting.LoadDataTable(myBrowseTableSchedule, sQuery, true, UserCode);

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTableSchedule.Columns["DocKey"];
            myBrowseTableSchedule.PrimaryKey = keyHeader;
            return myBrowseTableSchedule;
        }

        public override DataTable LoadBrowseTableChangeCar(string UserCode)
        {
            string sQuery = "";
            sQuery = "EXEC GetDataChangeCar";
            myLocalDBSetting.LoadDataTable(myBrowseTableChangeCar, sQuery, true, UserCode);

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTableSchedule.Columns["DocKey"];
            myBrowseTableChangeCar.PrimaryKey = keyHeader;
            return myBrowseTableChangeCar;
        }

        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();

            DataTable myUserTable = new DataTable();
            DataTable myUserDetailTable = new DataTable();
            DataTable myAdminTable = new DataTable();
            DataTable myDriverTable = new DataTable();

            string sQueryUser = "SELECT * FROM [dbo].[Booking] WHERE DocKey=@DocKey";
            string sQueryUserDetail = "SELECT DtlKey,Dockey,Seq,NIK,Name,Jabatan,Email FROM [dbo].[BookingDetail] WHERE Dockey=@DocKey";
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
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                localdbSetting.ExecuteNonQuery("DELETE FROM [dbo].[Application] WHERE DocKey=?", (object)headerid);
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
        protected override void SaveData(BookingEntity Booking, DataSet ds, string strDocName, SaveAction saveaction, string strUpline, string userID, string userName, string approver)
        {
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            SqlDBSetting dbsetting = this.myDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(localdbSetting.ConnectionString);
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["User"].Rows[0];
            DataRow dataAdminRow = ds.Tables["Admin"].Rows[0];
            DataRow dataDriverRow = ds.Tables["Driver"].Rows[0];
            try
            {
                localdbSetting.StartTransaction();
                if (saveaction == SaveAction.Save)
                {
                        DataRow[] myrowDocNo = localdbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='BK'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), DBSetting.GetServerTime());
                            localdbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType=?", strDocName);
                        }
                }
                if (saveaction == SaveAction.Save)
                {
                    dataRow["Approver"] = approver;
                }

                if (saveaction == SaveAction.Approve)
                {
                    string approverName = getApprover(Convert.ToString(Booking.Approver));
                    SendEmailApproveHead(Convert.ToString(Booking.EmployeeName), Convert.ToString(Booking.DocNo), approverName);
                    dataRow["Status"] = "NEED APPROVAL";
                    dataRow["Approver"] = "IS_GA";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.Reject)
                {
                    dataRow["Status"] = "REJECTED";
                    dataRow["Approver"] = userID;
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.Cancel)
                {
                    dataRow["Status"] = "CANCELLED";
                    dataRow["Cancelled"] = "T";
                    dataRow["Approver"] = userID;
                    dataRow["CancelledBy"] = userName;
                    dataRow["CancelledDateTime"] = Mydate;
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }

                if (saveaction == SaveAction.HoldByAdmin)
                {
                    dataRow["Status"] = "HOLD BY GA";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.RejectByAdmin)
                {
                    dataRow["Status"] = "REJECTED BY GA";
                    dataRow["Approver"] = userID;
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                }
                if (saveaction == SaveAction.ApproveByAdmin)
                {
                    dataRow["Status"] = "ON SCHEDULE";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                    ClearBookingAdmin(ds);
                    SaveBookingAdmin(ds, userName);

                    SendEmailApproveGA(Convert.ToString(Booking.EmployeeName), Convert.ToString(Booking.AdminCarType), Convert.ToString(Booking.AdminCarLicensePlate), Convert.ToString(Booking.AdminRemark));
                    //SendNotifEmail(Booking);
                    // SendSMS(Booking, saveaction);
                }
                if (saveaction == SaveAction.ApproveChangeCar)
                {
                    dataRow["Status"] = "ON SCHEDULE";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                    ClearBookingAdmin(ds);
                    SaveBookingAdmin(ds, userName);

                    SendEmailChangeCar(Convert.ToString(Booking.EmployeeName), Convert.ToString(Booking.AdminCarType), Convert.ToString(Booking.AdminCarLicensePlate), Convert.ToString(Booking.AdminRemark));                    
                }
                if (saveaction == SaveAction.FinishByDriver)
                {
                    dataRow["Status"] = "FINISH";
                    dataRow["LastModifiedBy"] = userName;
                    dataRow["LastModifiedDateTime"] = Mydate;
                    dataRow["Approver"] = "";

                    dataDriverRow["DriverName"] = userName;
                    dataDriverRow["ActualPickDateTime"] = Mydate;
                    dataDriverRow["ActualArriveDateTime"] = Mydate;
                    dataDriverRow["LastModifiedBy"] = userName;
                    dataDriverRow["LastModifiedDateTime"] = Mydate;

                    localdbSetting.ExecuteNonQuery("UPDATE [dbo].[MasterCar] SET Kilometer=?, Remark=? WHERE CarLicense=?", Convert.ToString(Booking.AdminCurrentKilometer), Convert.ToString(Booking.AdminRemark), Convert.ToString(Booking.AdminCarLicensePlate));
                }

                localdbSetting.SimpleSaveDataTable(ds.Tables["User"], "SELECT * FROM [dbo].[Booking]");
                if (saveaction != SaveAction.ApproveByAdmin && saveaction != SaveAction.ApproveChangeCar)
                {
                        localdbSetting.SimpleSaveDataTable(ds.Tables["Admin"], "SELECT * FROM [dbo].[BookingAdmin]");
                }
                localdbSetting.SimpleSaveDataTable(ds.Tables["Driver"], "SELECT * FROM [dbo].[BookingDriver]");
                //SaveDetail(ds, saveaction);

                Booking.strErrorGenTicket = "null";
                if (Booking.strErrorGenTicket == "null")
                {
                    localdbSetting.Commit();
                    dbsetting.Commit();
                }
                else
                {
                    localdbSetting.Rollback();
                    dbsetting.Rollback();
                    throw new ArgumentException(Booking.strErrorGenTicket);
                }
            }
            catch (SqlException ex)
            {
                localdbSetting.Rollback();
                dbsetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                localdbSetting.Rollback();
                dbsetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                localdbSetting.Rollback();
                dbsetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                localdbSetting.EndTransaction();
                dbsetting.EndTransaction();
            }
        }

        private string getRecipient(string EmployeeName)
        {
            string lastkilometer = "";
            SqlConnection connection = new SqlConnection(this.myLocalDBSetting.ConnectionString);
            try
            {
                connection.Open();
                object obj = null;

                obj = myLocalDBSetting.ExecuteScalar("Exec sp_GetRecepient ?", EmployeeName);
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

        private string getApprover(string NIK)
        {
            string lastkilometer = "";
            SqlConnection connection = new SqlConnection(this.myLocalDBSetting.ConnectionString);
            try
            {
                connection.Open();
                object obj = null;

                obj = myLocalDBSetting.ExecuteScalar("Exec sp_getApproverName ?", NIK);
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


        private string getCarName(string platno)
        {
            string lastkilometer = "";
            SqlConnection connection = new SqlConnection(this.myLocalDBSetting.ConnectionString);
            try
            {
                connection.Open();
                object obj = null;

                obj = myLocalDBSetting.ExecuteScalar("SELECT CarName FROM MASTERCAR WHERE CarLicense= ?", platno);
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

        public void SendEmailApproveGA(string EmployeeName, string CarType, string PlatNo, string Remark)
        {
            try
            {
                string recipient = getRecipient(EmployeeName);
                string carName = getCarName(PlatNo);
                SmtpClient smtp = new SmtpClient();
                MailMessage mm = new MailMessage();

                mm.From = new MailAddress("no-reply@mncleasing.com", "Admin CARPOOL");
                mm.To.Add(new MailAddress(recipient));
                mm.To.Add(new MailAddress("arief.syamsudin@mncgroup.com"));
                mm.To.Add(new MailAddress("garry.florence@mncgroup.com"));
                mm.IsBodyHtml = true;
                mm.Body = @"<head><style>body {font-family: arial; font-size: 12px;}</style></head>";
                mm.Body = mm.Body + "<body>";
                mm.Body = mm.Body + "Dear " + EmployeeName + ",<br><br>";
                mm.Body = mm.Body + "Peminjaman Kendaraan Mobil sudah di approve oleh tim GA.<br>";
                mm.Body = mm.Body + "Kendaraan Mobil dapat segera diambil dengan detail berikut:<br><br>";
                mm.Body = mm.Body + "<table border=1 width=800><tr style=font - weight: bold; text - align:center;>";
                mm.Body = mm.Body + "<td width=100>Jenis Mobil</td>";
                mm.Body = mm.Body + "<td width=10>No. Plat</td>";
                mm.Body = mm.Body + "<td width=100>Remarks</td></tr>";
                mm.Body = mm.Body + "<tr><td style=text - align:center;>"+ carName + "</td>";
                mm.Body = mm.Body + "<td style=text - align:center;>"+ PlatNo +"</td>";
                mm.Body = mm.Body + "<td style=text - align:left;>"+ Remark +"</td></tr>";
                mm.Body = mm.Body + "</table><br><br>";
                mm.Body = mm.Body + "Regards,<br>MNC Leasing CARPOOL Auto Notification</body>";
                mm.Subject = "CARPOOL - Approve";
                
                smtp.Port = 25;
                smtp.Host = "172.31.215.100";//"zsmtp.mnc-cloud.xyz";//"27.0.198.70"; //for gmail host  
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("no-reply@mncleasing.com", "Welcome.21");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mm);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Unable to send email. Error : " + e);
            }
        }

        public void SendEmailChangeCar(string EmployeeName, string CarType, string PlatNo, string Remark)
        {
            try
            {
                string recipient = getRecipient(EmployeeName);
                string carName = getCarName(PlatNo);
                SmtpClient smtp = new SmtpClient();
                MailMessage mm = new MailMessage();

                mm.From = new MailAddress("no-reply@mncleasing.com", "Admin CARPOOL");
                mm.To.Add(new MailAddress(recipient));
                mm.To.Add(new MailAddress("arief.syamsudin@mncgroup.com"));
                mm.To.Add(new MailAddress("garry.florence@mncgroup.com"));
                mm.IsBodyHtml = true;
                mm.Body = @"<head><style>body {font-family: arial; font-size: 12px;}</style></head>";
                mm.Body = mm.Body + "<body>";
                mm.Body = mm.Body + "Dear " + EmployeeName + ",<br><br>";
                mm.Body = mm.Body + "Terdapat Perubahan Kendaraan Mobil oleh tim GA.<br>";
                mm.Body = mm.Body + "Kendaraan Mobil dapat segera diambil dengan detail berikut:<br><br>";
                mm.Body = mm.Body + "<table border=1 width=800><tr style=font - weight: bold; text - align:center;>";
                mm.Body = mm.Body + "<td width=100>Jenis Mobil</td>";
                mm.Body = mm.Body + "<td width=10>No. Plat</td>";
                mm.Body = mm.Body + "<td width=100>Remarks</td></tr>";
                mm.Body = mm.Body + "<tr><td style=text - align:center;>" + carName + "</td>";
                mm.Body = mm.Body + "<td style=text - align:center;>" + PlatNo + "</td>";
                mm.Body = mm.Body + "<td style=text - align:left;>" + Remark + "</td></tr>";
                mm.Body = mm.Body + "</table><br><br>";
                mm.Body = mm.Body + "Apabila ada pertanyaan silahkan hubungi tim GA.<br><br>";
                mm.Body = mm.Body + "Regards,<br>MNC Leasing CARPOOL Auto Notification</body>";
                mm.Subject = "CARPOOL - Perubahan Kendaraan";

                smtp.Port = 25;
                smtp.Host = "172.31.215.100";//"zsmtp.mnc-cloud.xyz";//"27.0.198.70"; //for gmail host  
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("no-reply@mncleasing.com", "Welcome.21");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mm);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Unable to send email. Error : " + e);
            }
        }

        public void SendEmailApproveHead(string EmployeeName, string BookNo, string Approval)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                MailMessage mm = new MailMessage();

                mm.From = new MailAddress("no-reply@mncleasing.com", "Admin CARPOOL");
                mm.To.Add(new MailAddress("gateam.mncleasing@mncgroup.com"));
                mm.To.Add(new MailAddress("arief.syamsudin@mncgroup.com"));
                mm.To.Add(new MailAddress("garry.florence@mncgroup.com"));
                mm.IsBodyHtml = true;
                mm.Body = @"<head><style>body {font-family: arial; font-size: 12px;}</style></head>";
                mm.Body = mm.Body + "<body>";
                mm.Body = mm.Body + "Dear Tim GA,<br><br>";
                mm.Body = mm.Body + "Pengajuan peminjaman Kendaraan Mobil atas karyawan berikut:<br><br>";
                mm.Body = mm.Body + "<table border=1 width=800><tr style=font - weight: bold; text - align:center;>";
                mm.Body = mm.Body + "<td width=20>Booking No</td>";
                mm.Body = mm.Body + "<td width=30>Employee Name</td>";
                mm.Body = mm.Body + "<td width=30>Approval</td></tr>";
                mm.Body = mm.Body + "<tr><td style=text - align:center;>" + BookNo + "</td>";
                mm.Body = mm.Body + "<td style=text - align:center;>" + EmployeeName + "</td>";
                mm.Body = mm.Body + "<td style=text - align:left;>" + Approval + "</td></tr>";
                mm.Body = mm.Body + "</table><br><br>";
                mm.Body = mm.Body + "Regards,<br>MNC Leasing CARPOOL Auto Notification</body>";
                mm.Subject = "CARPOOL - Pengajuan Peminjaman Kendaraan";

                smtp.Port = 25;
                smtp.Host = "172.31.215.100";//"zsmtp.mnc-cloud.xyz";//"27.0.198.70"; //for gmail host  
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("no-reply@mncleasing.com", "Welcome.21");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mm);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Unable to send email. Error : " + e);
            }
        }


        protected override void SaveBookingAdmin(DataSet ds, string userName)
        {
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(LocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                DataRow dataRow = ds.Tables["Admin"].Rows[0];

                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[BookingAdmin] (DocKey,SourceKey,DriverCode,DriverName,CarCode,
                    CarType,CarLicensePlate,Remark,EstPickDateTime,EstArriveDateTime,AdminCode,AdminName,CreatedBy,CreatedDateTime,LastModifiedBy,
                    LastModifiedDateTime,LastKilometer,CurrentKilometer) VALUES (@DocKey,@SourceKey,@DriverCode,@DriverName,@CarCode,@CarType,@CarLicensePlate,@Remark,
                    @EstPickDateTime,@EstArriveDateTime,@AdminCode,@AdminName,@CreatedBy,@CreatedDateTime,@LastModifiedBy,@LastModifiedDateTime,@LastKilometer,@CurrentKilometer)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                    sqlParameter1.Value = dataRow.Field<int>("DocKey");
                    sqlParameter1.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@SourceKey", SqlDbType.Int);
                    sqlParameter2.Value = dataRow.Field<int>("SourceKey");
                    sqlParameter2.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@DriverCode", SqlDbType.NVarChar, 40);
                    sqlParameter3.Value = DBNull.Value;
                    sqlParameter3.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@DriverName", SqlDbType.NVarChar, 100);
                    sqlParameter4.Value = DBNull.Value;
                    sqlParameter4.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CarCode", SqlDbType.NVarChar, 40);
                    sqlParameter5.Value = DBNull.Value;
                    sqlParameter5.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@CarType", SqlDbType.NVarChar, 100);
                    sqlParameter6.Value = dataRow.Field<string>("CarType");
                    sqlParameter6.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@CarLicensePlate", SqlDbType.NVarChar, 15);
                    sqlParameter7.Value = dataRow.Field<string>("CarLicensePlate");
                    sqlParameter7.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Remark", SqlDbType.NVarChar);
                    sqlParameter8.Value = dataRow.Field<string>("Remark");
                    sqlParameter8.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@EstPickDateTime", SqlDbType.DateTime);
                    sqlParameter9.Value = DBNull.Value;
                    sqlParameter9.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@EstArriveDateTime", SqlDbType.DateTime);
                    sqlParameter10.Value = DBNull.Value;
                    sqlParameter10.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@AdminCode", SqlDbType.NVarChar, 40);
                    sqlParameter11.Value = DBNull.Value;
                    sqlParameter11.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@AdminName", SqlDbType.NVarChar, 100);
                    sqlParameter12.Value = DBNull.Value;
                    sqlParameter12.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar);
                    sqlParameter13.Value = userName;
                    sqlParameter13.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime);
                    sqlParameter14.Value = Mydate;
                    sqlParameter14.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@LastModifiedBy", SqlDbType.NVarChar);
                    sqlParameter15.Value = DBNull.Value;
                    sqlParameter15.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@LastModifiedDateTime", SqlDbType.DateTime);
                    sqlParameter16.Value = DBNull.Value;
                    sqlParameter16.Direction = ParameterDirection.Input;

                    var lastkilometer = dataRow.Field<string>("LastKilometer");

                    SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@LastKilometer", SqlDbType.NVarChar);
                    sqlParameter17.Value = lastkilometer == null ? "" : lastkilometer;
                    sqlParameter17.Direction = ParameterDirection.Input;

                    var currentkilometer = dataRow.Field<string>("CurrentKilometer");

                    SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@CurrentKilometer", SqlDbType.NVarChar);
                    sqlParameter18.Value = currentkilometer == null ? "" : currentkilometer;
                    sqlParameter18.Direction = ParameterDirection.Input;

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
        protected override void SaveDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(LocalDBSetting.ConnectionString);
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
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
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
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
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
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
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
                LocalDBSetting.ExecuteNonQuery("UPDATE dbo.WorkList SET WorkList.Source = (SELECT DocKey FROM dbo.ChangeDataList WHERE WorkList.TicketNo=ChangeDataList.TicketNo)");
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
                obj = myLocalDBSetting.ExecuteScalar("SELECT A.StateDescription FROM [dbo].[ApplicationWorkflowScheme] A WHERE A.Seq = (SELECT Seq + 1 FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=?)", myLastStatus);
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
        protected override void ClearBookingAdmin(DataSet ds)
        {
            DataRow dataRow = ds.Tables["Admin"].Rows[0];
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[BookingAdmin] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = dataRow.Field<int>("DocKey"); ;
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
        protected override void ClearDetail(BookingEntity Booking, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
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

        protected override void SendNotifEmail(BookingEntity Booking)
        {
            string ssql = "exec SP_Email_Notification_Approval_CarPool '"+Booking.EmployeeName+"'";
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }

        protected override void SendSMS(BookingEntity Booking, SaveAction saveaction)
        {
            string sRedaksi1 = "Hallo Customer, Booking anda sudah diproses oleh dispatcher : " + Booking.DocNo.ToString() + ", terima kasih.";
            string sAddress1 = @"http://" + "www.etracker.cc/bulksms/mesapi.aspx?user=MNCLEASING&pass=P@$$w0rD@MNC&type=0&to=" + Booking.Hp.ToString() + "&from=MNC%20LEASING&text=" + sRedaksi1 + "&servid=MNC001";
            new WebClient().DownloadString(sAddress1);
            myLocalDBSetting.ExecuteNonQuery("INSERT INTO SMSHist VALUES (?,?,?,?)", (object)Booking.DocNo, (object)myLocalDBSetting.GetServerTime(), (object)Booking.Hp, (object)sRedaksi1);

            object obj = myLocalDBSetting.ExecuteScalar("select top 1 Hp from MasterUser WHERE user_name=?", (object)Booking.AdminDriverName);
            if (obj != null && obj != DBNull.Value)
            {
                string sRedaksi2 = "Hallo Driver, anda mendapatkan order " + Booking.DocNo.ToString() + "Silahkan buka aplikasi carpool untuk mengetahui detail trip.";
                string sAddress2 = @"http://" + "www.etracker.cc/bulksms/mesapi.aspx?user=MNCLEASING&pass=P@$$w0rD@MNC&type=0&to=" + obj.ToString() + "&from=MNC%20LEASING&text=" + sRedaksi2 + "&servid=MNC001";
                new WebClient().DownloadString(sAddress2);
                myLocalDBSetting.ExecuteNonQuery("INSERT INTO SMSHist VALUES (?,?,?,?)", (object)Booking.DocNo, (object)myLocalDBSetting.GetServerTime(), (object)obj, (object)sRedaksi2);
            }
        }
    }
}