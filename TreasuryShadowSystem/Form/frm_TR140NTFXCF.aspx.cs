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

using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR140NTFXCF : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR140NTFXCF";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR140NTFXCF));

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Product = Model_RPT_TR140NTFXCF.getProduct(Config.ConnectionString, DealNoBox.Text.Trim());

                rpt = Model_RPT_TR140NTFXCF.getReport(Config.ConnectionString, Product, DealNoBox.Text.Trim());
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
                Log.Error(ex.Message);
            }
        }
    }
}
