using DevExpress.Web;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Maintenance.ItemMaintenance
{
    public partial class FormItemMaintenance : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
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
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                myLocalDBSetting = localdbsetting;

                myMainDataSet = new DataSet();
                myMainDataTable = new DataTable();
                myMainDataTable = myLocalDBSetting.GetDataTable("SELECT * FROM [dbo].[Item] ORDER BY ItemCode", false);
                myMainDataTable.PrimaryKey = new DataColumn[] { myMainDataTable.Columns["ItemCode"] };
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
            if (e.Column.FieldName == "ItemCode") { e.Editor.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.WhiteSmoke; }
        }
        protected void gvMain_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            myMainDataSet = (DataSet)Session["myMainDataSet"];
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable mytable = myMainDataSet.Tables[0];
            DataRow row = mytable.NewRow();
            try
            {
                DataRow[] myrowDocNo = myLocalDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='ITEM'", "", DataViewRowState.CurrentRows);
                e.NewValues["ItemCode"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myLocalDBSetting.GetServerTime());
            }
            catch { }
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
                row["LastModifiedDateTime"] = myLocalDBSetting.GetServerTime();
                mytable.Rows.Add(row);

                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[Item]");
                myLocalDBSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType='ITEM'");
                myLocalDBSetting.Commit();

            }
            catch (Exception ex)
            {
                myLocalDBSetting.Rollback();
            }
            finally
            {
                myLocalDBSetting.EndTransaction();
            }
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
                row["LastModifiedDateTime"] = myLocalDBSetting.GetServerTime();

                myLocalDBSetting.StartTransaction();
                myLocalDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[Item]");
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
                myLocalDBSetting.ExecuteNonQuery("DELETE [dbo].[Item] WHERE ItemCode=?", e.Keys[gvMain.KeyFieldName]);
                myLocalDBSetting.Commit();
            }
            catch { myLocalDBSetting.Rollback(); }
            finally { myLocalDBSetting.EndTransaction(); }
        }
    }
}