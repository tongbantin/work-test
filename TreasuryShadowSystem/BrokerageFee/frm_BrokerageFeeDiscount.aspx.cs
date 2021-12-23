using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using System.Web.Script.Serialization;
using System.Threading;
using System.Globalization;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_BrokerageFeeDiscount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (!IsPostBack)
                BindProduct();
        }


        private void BindProduct()
        {
            try
            {
                string Product = Request.QueryString["Product"];
                if (Product.Equals("FI"))
                {
                    prodtype.Items.Add(new ListItem("FI", "FI"));
                    prodtype.Items.Add(new ListItem("SWAP", "SWAP"));
                }
                else if (Product.Equals("FX"))
                {
                    prodtype.Items.Add(new ListItem("FX", "FX"));
                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod(EnableSession = true)]
        public static object DiscountList(string brok, string prodtype, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return BrokerageFeeDiscount.DiscountList(Config.ConnectionString, brok, prodtype, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateDiscount(BrokerageFeeDiscount record)
        {
            return BrokerageFeeDiscount.CreateDiscount(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateDiscount(BrokerageFeeDiscount record)
        {
            return BrokerageFeeDiscount.UpdateDiscount(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteDiscount(int DISCOUNTID)
        {
            return BrokerageFeeDiscount.DeleteDiscount(Config.ConnectionString, DISCOUNTID);
        }
    }
}
