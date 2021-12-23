using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using log4net;

namespace TreasuryShadowSystem.Debenture
{
    public partial class frm_DebentureBE : System.Web.UI.Page
    {

        ReportClass rpt = new ReportClass();
        string reportname = "TR153DEBE";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_DebentureBE));

        public string TheUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            //TheUrl = "../View/ReportViewer2.aspx?report=TR153DEBE";
            //TheUrl += "&d=" + txtsecid.Text;
            //Page.DataBind();
            try
            {
                string Secid = txtsecid.Text;

                rpt = Model_RPT_TR153DEBE.getReport(Config.ConnectionString, Secid);
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
