using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using TreasuryModel.Data;
namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_MessageBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(); 
            }
        }
        private void BindGrid()
        {
            try
            {
                GridSanction.DataSource = SWIFTMsgChk.getCheckSanctionByMessageID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                GridSanction.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
