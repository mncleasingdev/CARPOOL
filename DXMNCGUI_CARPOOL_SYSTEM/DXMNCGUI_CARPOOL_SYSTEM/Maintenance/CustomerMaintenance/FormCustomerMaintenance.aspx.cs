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

namespace DXMNCGUI_CARPOOL_SYSTEM.Maintenance.CustomerMaintenance
{
    public partial class FormCustomerMaintenance : BasePage
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
        protected void Page_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                myMainDataSet = new DataSet();
                myMainDataTable = new DataTable();
                myMainDataTable = myLocalDBSetting.GetDataTable("SELECT * FROM [dbo].[MasterClient] ORDER BY NAME", false);
                myMainDataTable.PrimaryKey = new DataColumn[] { myMainDataTable.Columns["ClientID"] };
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void gvMain_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "SmileID") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke;}
            if (e.Column.FieldName == "ClientID") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "Address1") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "RT") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "RW") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "Kelurahan") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "Kecamatan") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "Kota") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "KodePos") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "ContactPerson") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
            if (e.Column.FieldName == "MobilePhone") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
        }
        protected void gvMain_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            myMainDataSet = (DataSet)Session["myMainDataSet"];
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable mytable =  myMainDataSet.Tables[0];
            DataRow row = mytable.NewRow();
            e.NewValues["ClientID"] = GetNewId();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
                if (enumerator.Key.ToString() != "Count")
                    row[enumerator.Key.ToString()] = enumerator.Value;
            gridView.CancelEdit();
            e.Cancel = true;
           
            try
            {
                row["IsActive"] = row["IsActive"] == DBNull.Value || row["IsActive"] == null ? "F" : "T";
                row["CreatedBy"] = this.UserName;
                row["CreatedDateTime"] = myLocalDBSetting.GetServerTime();
                row["LastModifiedBy"] = this.UserName;
                row["LastModifiedTime"] = myLocalDBSetting.GetServerTime();
                mytable.Rows.Add(row);

                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[MasterClient]");
                myLocalDBSetting.Commit();
            }
            catch {myLocalDBSetting.Rollback();}
            finally {myLocalDBSetting.EndTransaction();}     
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
                row["LastModifiedBy"] = this.UserName;
                row["LastModifiedTime"] = myLocalDBSetting.GetServerTime();

                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[MasterClient]");
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
                myLocalDBSetting.ExecuteNonQuery("DELETE [dbo].[MasterClient] WHERE ClientID=?", e.Keys[gvMain.KeyFieldName]);
                myLocalDBSetting.Commit();
            }
            catch { myLocalDBSetting.Rollback(); }
            finally { myLocalDBSetting.EndTransaction(); }
        }
        private int GetNewId()
        {
            myMainDataSet = (DataSet)Session["myMainDataSet"];
            DataTable mytable = myMainDataSet.Tables[0];
            if (mytable.Rows.Count == 0) return 0;
            int max = Convert.ToInt32(mytable.Rows[0]["ClientID"]);
            for (int i = 1; i < mytable.Rows.Count; i++)
            {
                if (Convert.ToInt32(mytable.Rows[i]["ClientID"]) > max)
                    max = Convert.ToInt32(mytable.Rows[i]["ClientID"]);
            }
            return max + 1;
        }
    }
}