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

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR025OITAL : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR025OITAL";

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Statement Report for payments before eod";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer.aspx?report=TR025OITAL&b=" + txtBranch.Text + "&tf=" + calTransFrom.SelectedDate.ToString("dd/MM/yyyy") + "&tt=" + calTransTo.SelectedDate.ToString("dd/MM/yyyy") + "&cs=" + ddCashIStatus.SelectedValue + "&ss=" + ddSecuritiesIStatus.SelectedValue + "&tps=" + ddThirdParty.SelectedValue);

            string branch = txtBranch.Text;
            string transStart = txtTransStartDate.Value;
            string transEnd = txtTransEndDate.Value;
            string cash = ddCashIStatus.SelectedValue;
            string securities = ddSecuritiesIStatus.SelectedValue;
            string thirdparty = ddThirdParty.SelectedValue;


            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&b=" + branch + "&tf=" + transStart + "&tt=" + transEnd;
            TheUrl += "&cs=" + cash + "&ss=" + securities + "&tps=" + thirdparty;
            Page.DataBind();
        }
    }
}
