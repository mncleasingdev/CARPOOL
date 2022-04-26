using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_CARPOOL_SYSTEM
{
    public partial class DirectLoginDriver : BasePage
    {
        private void Connection()
        {
            this.dbsetting = new SqlDBSetting(DBServerType.SQL2000, GetSqlConnectionString(), 30); //"User Id=apps;Password=apps;Data Source= (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.213)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=DEV2)))", 30);
            this.localdbsetting = new SqlLocalDBSetting(DBServerType.SQL2000, GetLocalConnectionString(), 30); //"User Id=apps;Password=apps;Data Source= (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.213)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=DEV2)))", 30);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection();

            this.dbsession = SqlDBSession.Create(dbsetting, localdbsetting);
            this.dbsession.UseEncryptedPassword = true;


            if (this.dbsession.Login(this.Request.QueryString["Email"], this.Request.QueryString["Password"]))
            {
                this.UserID = this.dbsession.LoginUserID.ToUpper();
                this.Email = this.dbsession.LoginEmail.ToUpper();
                this.SessionID = Guid.NewGuid();
                accessright = AccesRight.Create(localdbsetting, this.UserID);
                object obj = dbsetting.ExecuteScalar("SELECT USER_NAME FROM [dbo].[MasterUser] WHERE EMAIL=?", (object)this.Email);
                if (obj != null && obj != DBNull.Value)
                {
                    this.UserName = obj.ToString();
                }
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                return;
            }
        }
    }
}