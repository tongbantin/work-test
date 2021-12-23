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
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;
using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR051BONTH : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR094OBOFXSW";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR094OBOFXSW));

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Confirmation Bond Buy/Sell";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string deal = txtDealNo.Text;
            string nosign = NoSignBox.Checked == true ? "1" : "0";
            rpt = Model_RPT_TR051BONTH.getReport(Config.ConnectionString, deal, nosign);
            if (rpt == null)
            {
                DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                rpt = new RPT_MESSAGE(); 
                rpt.SummaryInfo.ReportTitle = "Error-BondTrade" + DateTime.Now.ToString("yyyyMMdd");
                rpt.SetDataSource(ds);
            }
            else
                rpt.SummaryInfo.ReportTitle = "BondTrade" + DateTime.Now.ToString("yyyyMMdd");
            Session["ReportSource"] = rpt;

            TheUrl = "../View/Viewer.aspx";
            Page.DataBind();
        }
    }
}
