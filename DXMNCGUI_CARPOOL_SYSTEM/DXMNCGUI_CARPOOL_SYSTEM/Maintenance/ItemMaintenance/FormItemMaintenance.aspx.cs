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

                myMainDataSet = new DataSet();
                myMainDataTable = new DataTable();
                myMainDataTable = myDBSetting.GetDataTable("SELECT * FROM [dbo].[Item] ORDER BY ItemCode", false);
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
                DataRow[] myrowDocNo = myDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='ITEM'", "", DataViewRowState.CurrentRows);
                e.NewValues["ItemCode"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
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
                row["CreatedDateTime"] = myDBSetting.GetServerTime();
                row["LastModifiedBy"] = this.UserName;
                row["LastModifiedDateTime"] = myDBSetting.GetServerTime();
                mytable.Rows.Add(row);

                myDBSetting.StartTransaction();
                myDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[Item]");
                myDBSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType='ITEM'");
                myDBSetting.Commit();

            }
            catch (Exception ex)
            {
                myDBSetting.Rollback();
            }
            finally
            {
                myDBSetting.EndTransaction();
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
                row["LastModifiedDateTime"] = myDBSetting.GetServerTime();

                myDBSetting.StartTransaction();
                myDBSetting.SimpleSaveDataTable(mytable, "SELECT * FROM [dbo].[Item]");
                myDBSetting.Commit();
            }
            catch { myDBSetting.Rollback(); }
            finally { myDBSetting.EndTransaction(); }
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
                myDBSetting.StartTransaction();
                myDBSetting.ExecuteNonQuery("DELETE [dbo].[Item] WHERE ItemCode=?", e.Keys[gvMain.KeyFieldName]);
                myDBSetting.Commit();
            }
            catch { myDBSetting.Rollback(); }
            finally { myDBSetting.EndTransaction(); }
        }
    }
}