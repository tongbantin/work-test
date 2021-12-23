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

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR138PTBE : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR138PTBE";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR138PTBE));

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] Fee = Config.GetParameterFile("138FEE").Split(',');
                string Report = ReportBox.Value;
                string FromDataDate = FromDateBox.Text.Trim();
                string ToDataDate = ToDateBox.Text.Trim();
                rpt = Model_RPT_TR138PTBE.getReport(Config.ConnectionString, Report, FromDataDate, ToDataDate, Fee);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                
                Session["ReportSource"] = rpt;

                TheUrl = "../View/Viewer.aspx";
                Page.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
