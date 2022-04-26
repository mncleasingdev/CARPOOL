using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement
{
    public class SettlementLinesRecord
    {
        private DataRow myRow;
        private SettlementEntity mySettlement;

        public DataRow Row
        {
            get
            {
                return this.myRow;
            }
        }
        public int DocKey
        {
            get
            {
                return System.Convert.ToInt32(this.myRow["DocKey"]);
            }
        }
        public int Seq
        {
            get
            {
                return System.Convert.ToInt32(this.myRow["Seq"]);
            }
        }
        public string ItemCode
        {
            get
            {
                return System.Convert.ToString(this.myRow["ItemCode"]);
            }
            set
            {
                this.myRow["ItemCode"] = (object)value;
            }
        }
        public string ItemDesc
        {
            get
            {
                return System.Convert.ToString(this.myRow["ItemDesc"]);
            }
            set
            {
                this.myRow["ItemDesc"] = (object)value;
            }
        }
        public string Note
        {
            get
            {
                return System.Convert.ToString(this.myRow["Note"]);
            }
            set
            {
                this.myRow["Note"] = (object)value;
            }
        }
        public string Remark1
        {
            get
            {
                return System.Convert.ToString(this.myRow["Remark1"]);
            }
            set
            {
                this.myRow["Remark1"] = (object)value;
            }
        }
        public string Remark2
        {
            get
            {
                return System.Convert.ToString(this.myRow["Remark2"]);
            }
            set
            {
                this.myRow["Remark2"] = (object)value;
            }
        }
        public string Remark3
        {
            get
            {
                return System.Convert.ToString(this.myRow["Remark3"]);
            }
            set
            {
                this.myRow["Remark3"] = (object)value;
            }
        }
        public string Remark4
        {
            get
            {
                return System.Convert.ToString(this.myRow["Remark4"]);
            }
            set
            {
                this.myRow["Remark4"] = (object)value;
            }
        }
        public Bitmap Image
        {
            get
            {
                byte[] imageBytes = Encoding.Unicode.GetBytes(this.myRow["Image"].ToString());
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return new Bitmap(ms);
                }
            }
            set
            {
                this.myRow["Image"] = (object)value;
            }
        }
        public decimal Qty
        {
            get
            {
                return System.Convert.ToDecimal(this.myRow["Qty"]);
            }
            set
            {
                this.myRow["Qty"] = System.Convert.ToDecimal(value);
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return System.Convert.ToDecimal(this.myRow["UnitPrice"]);
            }
            set
            {
                this.myRow["UnitPrice"] = System.Convert.ToDecimal(value);
            }
        }
        public decimal SubTotal
        {
            get
            {
                return System.Convert.ToDecimal(this.myRow["SubTotal"]);
            }
            set
            {
                this.myRow["SubTotal"] = System.Convert.ToDecimal(value);
            }
        }
        internal SettlementLinesRecord(DataRow row, SettlementEntity SettlementEntity)
        {
            this.myRow = row;
            this.mySettlement = SettlementEntity;
        }
    }
}