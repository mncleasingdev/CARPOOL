using DevExpress.Web;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM.Reporting.RptPenggunaanKendaraan
{
    public partial class ReportPenggunaanKendaraan : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHeaderTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHeaderTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHeaderTable" + this.ViewState["_PageID"]] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;

                myHeaderTable = new DataTable();
                GetMainData();

            }
            if (!IsCallback)
            {

            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myHeaderTable;
        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }

        protected void GetMainData()
        {
            string ssql = "exec dbo.spGetListReport";
            myHeaderTable = myDBSetting.GetDataTable(ssql, false);
            gvMain.DataSource = myHeaderTable;
            gvMain.DataBind();
        }
    }
}