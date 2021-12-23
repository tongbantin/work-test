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
    public partial class frm_TR073DBOFIDF : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR073DBOFIDF";
        ReportClass rpt = new ReportClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /*
            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&f=" + txtFromExecuteDate.Value + "&e=" + txtToExecuteDate.Value + "&p=" + ProductBox.SelectedValue;
            Page.DataBind();
            */

            rpt = Model_RPT_TR073DBOFIDF.getReport(Config.ConnectionString, ProductBox.SelectedValue, txtFromExecuteDate.Value, txtToExecuteDate.Value);
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
    }
}
