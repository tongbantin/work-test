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
using KKB.Treasury.TreasuryModel.Model;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR080ACSWGL : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR080ACSWGL";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR080ACSWGL));

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Branch = txtBranch.Text.Trim();
                string CostCenter = ddCost.SelectedValue;
                string FromDate = txtFromDate.Value.Trim();
                string ToDate = txtToDate.Value.Trim();
                if (txtToDate.Value.Trim().Equals(""))
                    ToDate = FromDate;
                rpt = Model_RPT_TR080ACSWGL.getReport(Config.ConnectionString, Branch, CostCenter, FromDate, ToDate);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "RealizeCCS_" + DateTime.Now.ToString("yyyyMMdd");
                Session["ReportSource"] = rpt;

                TheUrl = "../View/Viewer.aspx";
                Page.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("~/View/ReportViewer2.aspx?report=TR063DBOFI&b=" + txtBranch.Text + "&e=" + txtExecuteDate.Value);
        //    TheUrl = "../View/ReportViewer2.aspx?report=TR080ACSWGL";
        //    TheUrl += "&b=" + txtBranch.Text + "&e=" + txtExecuteDate.Value;
        //    Page.DataBind();
        //}
    }
}
