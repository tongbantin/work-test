using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using CrystalDecisions.Shared;
using System.Data;
using KKB.Treasury.TreasuryReport.Report;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR065DBOCM : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "";
        ReportClass rpt = new ReportClass();
        public const string REPORT_USERCODE = "TR065DBOCM";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ReportDate"] != null)
                    txtReportDate.Value = Session["ReportDate"].ToString();
                else
                {
                    //txtReportDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    txtReportDate.Value = Model.Model_Tb_Master.getSysDate().ToString("yyyy-MM-dd");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string _date = txtReportDate.Value;
            reportname = "Repo_Cash_Margin_" + _date.Replace("-", "");
            rpt = Model_RPT_TR065DBOCM.getReport(Config.ConnectionString, _date);
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
