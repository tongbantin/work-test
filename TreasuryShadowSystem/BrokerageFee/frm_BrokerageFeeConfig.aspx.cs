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

namespace TreasuryShadowSystem.Form
{
    public partial class frm_BrokerageFeeConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProduct();
            }
        }


        private void BindProduct()
        {
            try
            {
                string Product = Request.QueryString["Product"];
                if (Product.Equals("FI"))
                {
                    prodtype.Items.Add(new ListItem("FI", "FI"));
                    prodtype.Items.Add(new ListItem("IR", "IR"));
                    prodtype.Items.Add(new ListItem("IF", "IF"));
                    prodtype.Items.Add(new ListItem("ES", "ES"));
                    prodtype.Items.Add(new ListItem("CC", "CC"));
                }
                else if (Product.Equals("FX"))
                {
                    prodtype.Items.Add(new ListItem("SPOT", "SPOT"));
                    prodtype.Items.Add(new ListItem("FORWARD", "FORWARD"));
                    prodtype.Items.Add(new ListItem("FX SWAP", "SWAP"));
                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod(EnableSession = true)]
        public static object ConfigList(string brok, string prodtype, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return BrokerageFeeConfig.ConfigList(Config.ConnectionString, brok, prodtype, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateConfig(BrokerageFeeConfig record)
        {
            return BrokerageFeeConfig.CreateConfig(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateConfig(BrokerageFeeConfig record)
        {
            return BrokerageFeeConfig.UpdateConfig(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteConfig(int DEALFEECONFID)
        {
            return BrokerageFeeConfig.DeleteConfig(Config.ConnectionString, DEALFEECONFID);
        }
    }
}
