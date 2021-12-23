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

namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_SanctionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object SWIFTList(string transno, string execdate, string status, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return SWIFTMsg.SWIFTList(Config.ConnectionString, transno, execdate, status, jtStartIndex, jtPageSize, jtSorting);
        }

    }
}
