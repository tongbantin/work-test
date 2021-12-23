using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;
using System.Data;

namespace TreasuryShadowSystem.TR152OBOSWIFT
{
    public partial class frm_TR152OBOSWIFT : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR152OBOSWIFT";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR152OBOSWIFT));

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Couterparty = CTPYBox.Text.Trim(); 
                string IncomingDate = IncomingDateBox.Text.Trim();
                rpt = Model_RPT_TR152OBOSWIFT.getReport(Config.ConnectionString, Couterparty, IncomingDate);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "MT300Matching_" + DateTime.Now.ToString("yyyyMMdd");
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
