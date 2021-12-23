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

namespace TreasuryShadowSystem.Maintain
{
    public partial class frm_Parameters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        [WebMethod(EnableSession = true)]
        public static object ParameterList(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return WebParameter.ParameterList(Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateParameter(WebParameter record)
        {
            return WebParameter.CreateParameter(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateParameter(WebParameter record)
        {
            return WebParameter.UpdateParameter(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteParameter(int ParameterID)
        {
            return WebParameter.DeleteParameter(Config.ConnectionString, ParameterID);
        }
    }
}
