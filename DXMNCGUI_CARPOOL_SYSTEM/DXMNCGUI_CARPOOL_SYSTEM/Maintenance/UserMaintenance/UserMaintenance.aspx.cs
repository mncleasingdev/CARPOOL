using DevExpress.Web;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Maintenance.UserMaintenance
{
    public partial class UserMaintenance : BasePage
    {
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected DataSet myMainDataSet
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myMainDataSet" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainDataSet" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainDataTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainDataTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainDataTable" + this.ViewState["_PageID"]] = value; }
        }
        protected bool bChangePass
        {
            get { isValidLogin(false); return (bool)HttpContext.Current.Session["bChangePass" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["bChangePass" + this.ViewState["_PageID"]] = value; }
        }

        //private int GetNewId()
        //{
        //    myMainDataSet = (DataSet)Session["myMainDataSet"];
        //    DataTable mytable = myMainDataSet.Tables[0];
        //    if (mytable.Rows.Count == 0) return 0;
        //    int max = Convert.ToInt32(mytable.Rows[0]["USER_ID"]);
        //    for (int i = 1; i < mytable.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(mytable.Rows[i]["USER_ID"]) > max)
        //            max = Convert.ToInt32(mytable.Rows[i]["USER_ID"]);
        //    }
        //    return max + 1;
        //}
        private string GeneratePassword(string myEncryption)
        {
            string myString = string.Empty;
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT CONVERT(VARCHAR(32), HashBytes('MD5', @PASS), 2)");
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@PASS", SqlDbType.NVarChar, 32);
                sqlParameter1.Value = myEncryption;
                sqlParameter1.Direction = ParameterDirection.Input;

                myString = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
            return myString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                bChangePass = false;
                myMainDataSet = new DataSet();
                myMainDataTable = new DataTable();
                myMainDataTable = myLocalDBSetting.GetDataTable("SELECT * FROM [dbo].[MasterUser] ORDER BY USER_NAME", false);
                myMainDataTable.PrimaryKey = new DataColumn[] { myMainDataTable.Columns["EMAIL"] };
                myMainDataSet.Tables.AddRange(new DataTable[] { myMainDataTable });
                Session["myMainDataSet"] = myMainDataSet;

                gvMain.DataSource = myMainDataSet.Tables[0];
                gvMain.DataBind();
            }
            else
            {
                myMainDataSet = (DataSet)Session["myMainDataSet"];
                gvMain.DataSource = myMainDataSet.Tables[0];
                gvMain.DataBind();
            }
        }

        protected void gvMain_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "USER_PASSWORD")
            {
                e.Editor.ReadOnly = false;
                e.Editor.BackColor = System.Drawing.Color.Transparent;
            }
        }
        protected void gvMain_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            myMainDataSet = (DataSet)Session["myMainDataSet"];
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable mytable = myMainDataSet.Tables[0];
            DataRow row = mytable.NewRow();
            //e.NewValues["USER_ID"] = GetNewId();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
                if (enumerator.Key.ToString() != "Count")
                    row[enumerator.Key.ToString()] = enumerator.Value;
            gridView.CancelEdit();
            e.Cancel = true;

            try
            {
                row["USER_PASSWORD"] = GeneratePassword(Convert.ToString(row["USER_PASSWORD"]));
                row["IS_ACTIVE_FLAG"] = row["IS_ACTIVE_FLAG"] == DBNull.Value || row["IS_ACTIVE_FLAG"] == null ? 0 : 1;
                row["IsAdmin"] = row["IsAdmin"] == DBNull.Value || row["IsAdmin"] == null ? "F" : "T";
                row["IsCoordinator"] = row["IsCoordinator"] == DBNull.Value || row["IsCoordinator"] == null ? "F" : "T";
                row["IsCustomer"] = row["IsCustomer"] == DBNull.Value || row["IsCustomer"] == null ? "F" : "T";
                row["IsDriver"] = row["IsDriver"] == DBNull.Value || row["IsDriver"] == null ? "F" : "T";
                row["NeedApproval"] = row["NeedApproval"] == DBNull.Value || row["NeedApproval"] == null ? "F" : "T";
                row["CRE_BY"] = this.UserName;
                row["CRE_DATE"] = myLocalDBSetting.GetServerTime();
                row["MOD_BY"] = this.UserName;
                row["MOD_DATE"] = myLocalDBSetting.GetServerTime();
                row["CRE_IP_ADDRESS"] = "127.0.0.1";
                row["MOD_IP_ADDRESS"] = "127.0.0.1";
                mytable.Rows.Add(row);

                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[MasterUser]");
                if (row["IsAdmin"].ToString() == "T")
                    {
                        myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_ADMIN");
                        myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_ADMIN");
                    }
                else
                    {   myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_ADMIN"); }
                if (row["IsCustomer"].ToString() == "T")
                    {
                        myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_CUSTOMER");
                        myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_CUSTOMER");
                    }
                else
                    {   myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_CUSTOMER"); }
                if (row["IsDriver"].ToString() == "T")

                    {
                        myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DRIVER");
                        myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_DRIVER");
                    }
                else
                    {   myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DRIVER"); }
                if (row["IsCoordinator"].ToString() == "T")

                {
                    myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DISPATCHER");
                    myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_DISPATCHER");
                }
                else
                { myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DISPATCHER"); }
                myLocalDBSetting.Commit();
            }
            catch { myLocalDBSetting.Rollback(); }
            finally { myLocalDBSetting.EndTransaction(); }
        }
        protected void gvMain_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            myMainDataSet = (DataSet)Session["myMainDataSet"];
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable mytable = myMainDataSet.Tables[0];
            DataRow row = mytable.Rows.Find(e.Keys[0]);
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
                row[enumerator.Key.ToString()] = enumerator.Value;
            gridView.CancelEdit();
            e.Cancel = true;

            try
            {
                row["MOD_BY"] = this.UserName;
                row["MOD_DATE"] = myLocalDBSetting.GetServerTime();

                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[MasterUser]");
                if (row["IsAdmin"].ToString() == "T")
                {
                    myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_ADMIN");
                    myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_ADMIN");
                }
                else
                {  myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_ADMIN"); }
                if (row["IsCustomer"].ToString() == "T")
                {
                    myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_CUSTOMER");
                    myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_CUSTOMER");
                }
                else
                {  myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_CUSTOMER"); }
                if (row["IsDriver"].ToString() == "T")

                {
                    myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DRIVER");
                    myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_DRIVER");
                }
                else
                {   myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DRIVER"); }
                if (row["IsCoordinator"].ToString() == "T")

                {
                    myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DISPATCHER");
                    myLocalDBSetting.ExecuteNonQuery("INSERT INTO AccessRight VALUES (?,?)", (object)row["Email"], "IS_DISPATCHER");
                }
                else
                { myLocalDBSetting.ExecuteNonQuery("DELETE AccessRight WHERE Email=? AND CMDid=?", (object)row["Email"], "IS_DISPATCHER"); }
                myLocalDBSetting.Commit();
            }
            catch { myLocalDBSetting.Rollback(); }
            finally { myLocalDBSetting.EndTransaction(); }
        }
        protected void gvMain_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int i = gvMain.FindVisibleIndexByKeyValue(e.Keys[gvMain.KeyFieldName]);
            e.Cancel = true;
            myMainDataSet = (DataSet)Session["myMainDataSet"];
            myMainDataSet.Tables[0].Rows.Remove(myMainDataSet.Tables[0].Rows.Find(e.Keys[gvMain.KeyFieldName]));
            DataTable mytable = myMainDataSet.Tables[0];
            try
            {
                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.ExecuteNonQuery("DELETE [dbo].[MasterUser] WHERE EMAIL=?", e.Keys[gvMain.KeyFieldName]);
                myLocalDBSetting.Commit();
            }
            catch { myLocalDBSetting.Rollback(); }
            finally { myLocalDBSetting.EndTransaction(); }
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            UpdatePasswordField(GeneratePassword(cnpsw.Text));
            ASPxPopupControl1.ShowOnPageLoad = false;
        }
        protected void UpdatePasswordField(string newPassword)
        {
            int index = gvMain.EditingRowVisibleIndex;
            string email = (string)gvMain.GetRowValues(index, "EMAIL");

            myLocalDBSetting.ExecuteNonQuery("UPDATE MasterUser SET USER_PASSWORD=? WHERE EMAIL=?", (object)newPassword, (object)email);
        }

        protected void gvMain_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["EMAIL"] != null)
            {
                object obj = myLocalDBSetting.ExecuteScalar("SELECT COUNT(EMAIL) FROM [dbo].[MasterUser] WHERE EMAIL=? AND USER_NAME <> ?", Convert.ToString(e.NewValues["EMAIL"]), Convert.ToString(e.OldValues["USER_NAME"]));
                if (obj != null && obj != DBNull.Value)
                {
                    if(Convert.ToInt32(obj) > 0)
                    {
                        AddError(e.Errors, gvMain.Columns["EMAIL"], "Duplicated email. This email has been registered.");
                    }
                }
            }
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
    }
}