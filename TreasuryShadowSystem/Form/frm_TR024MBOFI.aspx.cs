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

using KKB.Treasury.TreasuryModel.Master; 

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR024MBOFI : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR024MBOFI";

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Investment Summary";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");

            if (!IsPostBack)
                BindControl();
        }

        private void BindControl()
        {
            DataSet s = Model_MS_COST.getDataSet(Configuration.Config.ConnectionString);
            ddCost.DataSource = s;
            ddCost.DataTextField = "COSTDESC";
            ddCost.DataValueField = "COSTCENT";
            ddCost.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer.aspx?report=TR024MBOFI&b=" + txtBranch.Text + "&ex=" + calExecuteDate.SelectedDate.ToString("dd/MM/yyyy") + "&c=" + ddCost.SelectedValue);

            string branch = txtBranch.Text;
            string date = txtExecuteDate.Value;
            string cost = ddCost.SelectedValue;
            Session["ReportDate"] = date;

            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&b=" + branch + "&ex=" + date + "&c=" + cost;
            Page.DataBind();
        }
    }
}
