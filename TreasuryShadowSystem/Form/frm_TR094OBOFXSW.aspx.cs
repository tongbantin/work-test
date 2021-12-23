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

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR094OBOFXSW : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR094OBOFXSW";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR094OBOFXSW));

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Purchase Interest";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                ProductBox.Items.Clear();
                ProductBox.Items.Add(new ListItem("Pre-FX Netting", "FX"));
                ProductBox.Items.Add(new ListItem("FX Payment", "FXNet"));
                ProductBox.Items.Add(new ListItem("Long FX Swap", "OTHERSWAP"));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string FromDate = FromDateBox.Text;
                string ToDate = ToDateBox.Text;
                if (ToDateBox.Text.Trim().Equals(""))
                    ToDate = FromDateBox.Text;
                
                if (ProductBox.SelectedValue.Equals("FX"))
                {
                    rpt = Model_RPT_TR094OBOFXSW.getReport(Config.ConnectionString, CMNEBox.Text.ToUpper(), FromDate, ToDate, false);
                    if (rpt!=null)
                        rpt.SummaryInfo.ReportTitle = "FXPreNetting" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (ProductBox.SelectedValue.Equals("FXNet"))
                {
                    rpt = Model_RPT_TR094OBOFXSW.getReport(Config.ConnectionString, CMNEBox.Text.ToUpper(), FromDate, ToDate, true);
                    //rpt = Model_RPT_TR094OBOFXSW.getReport(Config.ConnectionString, FromDate, Config.GetParameterFile("094Party"), "","");
                    if (rpt != null)
                    {
                        rpt.SummaryInfo.ReportTitle = "PaymentReport" + DateTime.Now.ToString("yyyyMMdd");
                    }
                }
                else if (ProductBox.SelectedValue.Equals("OTHERSWAP"))
                {
                    rpt = Model_RPT_TR139PAYS.getReport(Config.ConnectionString, "OTHERSWAP", "", FromDateBox.Text.Trim(), ToDateBox.Text.Trim());
                    if (rpt != null)
                        rpt.SummaryInfo.ReportTitle = "PaymentSwap" + DateTime.Now.ToString("yyyyMMdd");
                }

                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE(); 
                    rpt.SummaryInfo.ReportTitle = "Error-PaymentReport" + DateTime.Now.ToString("yyyyMMdd");
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
