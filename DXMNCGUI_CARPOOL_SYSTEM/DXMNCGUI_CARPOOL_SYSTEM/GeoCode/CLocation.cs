using System;
using System.Device.Location;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DXMNCGUI_CARPOOL_SYSTEM.Controllers;
using DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking;
using System.Data.SqlClient;

namespace DXMNCGUI_CARPOOL_SYSTEM.GeoCode
{
    public class CLocation : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        GeoCoordinateWatcher watcher;

        public void GetLocationEvent()
        {
            myDBSetting = dbsetting;
            this.watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            bool started = this.watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
            if (!started)
            {
                Console.WriteLine("GeoCoordinateWatcher timed out on start.");
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            UpdatePosition(e.Position.Location.Latitude, e.Position.Location.Longitude);
        }

        void UpdatePosition(double Latitude, double Longitude)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE [dbo].[GeoLoc] SET GeoLat=@GeoLat, GeoLong=@GeoLong WHERE DriverName = @DriverName");
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DriverName", SqlDbType.NVarChar, 50);
                sqlParameter1.Value = this.UserName;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@GeoLat", SqlDbType.Float);
                sqlParameter2.Value = Latitude;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@GeoLong", SqlDbType.Float);
                sqlParameter3.Value = Longitude;
                sqlParameter3.Direction = ParameterDirection.Input;
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
    }
}