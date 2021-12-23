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


namespace TreasuryShadowSystem.MarkToMarket
{
    public partial class frm_MarkToMarketPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod(EnableSession = true)]
        public static object PointList(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return MarkPoint.PointList(Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreatePoint(MarkPoint record)
        {
            return MarkPoint.CreatePoint(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdatePoint(MarkPoint record)
        {
            return MarkPoint.UpdatePoint(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeletePoint(int CONFIGPOINTID)
        {
            return MarkPoint.DeletePoint(Config.ConnectionString, CONFIGPOINTID);
        }
    }
}
