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
using KKB.Treasury.TreasuryModel.Model;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR135BOTBPMANAGE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession=true)]
        public static object TBPList()
        {
            int jtStartIndex = 0; int jtPageSize = 0; string jtSorting = "";
            return Model_RPT_TR135BOTBP.TBPList(Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateTBP(Model_RPT_TR135BOTBP record)
        {
            return Model_RPT_TR135BOTBP.CreateTBP(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateTBP(Model_RPT_TR135BOTBP record)
        {
            return Model_RPT_TR135BOTBP.UpdateTBP(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteTBP(int TBPID)
        {
            return Model_RPT_TR135BOTBP.DeleteTBP(Config.ConnectionString, TBPID);
        }
    }
}
