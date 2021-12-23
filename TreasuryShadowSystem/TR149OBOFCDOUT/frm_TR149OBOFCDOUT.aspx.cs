using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryShadowSystem.Configuration;
using System.IO;
using log4net;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using CrystalDecisions.Shared;
using System.Data;
using log4net;

namespace TreasuryShadowSystem.TR149OBOFCDOUT
{
    public partial class frm_TR149OBOFCDOUT : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR149OBOFCDOUT";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR149OBOFCDOUT));
 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string FromDate = FromDateBox.Text;
                string ToDate = ToDateBox.Text;
                if (ToDateBox.Text.Trim().Equals(""))
                    ToDate = FromDateBox.Text;


                rpt = Model_RPT_TR149OBOFCDOUT.getReport(Config.ConnectionString, FromDateBox.Text.Trim(), ToDateBox.Text.Trim());
                if (rpt != null)
                    rpt.SummaryInfo.ReportTitle = "FCD Outstanding " + DateTime.Now.ToString("yyyyMMdd");

                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SummaryInfo.ReportTitle = "Error-FCD Outstanding Report " + DateTime.Now.ToString("yyyyMMdd");
                    rpt.SetDataSource(ds);
                }
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
