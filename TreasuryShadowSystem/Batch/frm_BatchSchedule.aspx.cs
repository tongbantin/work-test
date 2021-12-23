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
using log4net;
using System.Configuration;
using TreasuryModel.Api;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace TreasuryShadowSystem.Batch
{
    public partial class frm_BatchSchedule : System.Web.UI.Page
    {
        public string TheUrl = "";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_BatchSchedule));
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRunBOTRate_Click(object sender, EventArgs e)
        {
            try
            {
                TheUrl = "../API/apiBOTExchangeRate.aspx?start_period=" + FromDate.Text + "&end_period=" + FromDate.Text;
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnRunBOTLoanInterest_Click(object sender, EventArgs e)
        {
            try
            {
                TheUrl = "../API/apiBOTInterestRate.aspx?start_period=" + FromDate.Text + "&end_period=" + FromDate.Text;
                Page.DataBind();                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
