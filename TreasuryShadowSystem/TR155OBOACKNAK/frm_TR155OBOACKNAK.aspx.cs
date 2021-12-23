using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;
using System.Data;
using log4net;
using KKB.Treasury.TreasuryModel.Model;

namespace TreasuryShadowSystem.TR155OBOACKNAK
{
    public partial class frm_TR155OBOACKNAK : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR155OBOACKNAK";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR155OBOACKNAK));

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            { 
                string IncomingDate = IncomingDateBox.Text.Trim();
                rpt = Model_RPT_TR155OBOACKNAK.getReport(Config.ConnectionString, IncomingDate);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "AckNakMatching_" + DateTime.Now.ToString("yyyyMMdd");
                Session["ReportSource"] = rpt;

                TheUrl = "../View/Viewer.aspx";
                Page.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
