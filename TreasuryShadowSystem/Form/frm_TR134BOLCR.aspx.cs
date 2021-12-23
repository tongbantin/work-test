using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryModel.Model;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR134BOLCR : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR134BOLCR";
        ReportClass rpt = new ReportClass();

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Purchase Interest";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            rpt = Model_RPT_TR134BOLCR.getReport(Config.ConnectionString, ChoiceBox.SelectedValue, txtExecuteDate.Value);
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
