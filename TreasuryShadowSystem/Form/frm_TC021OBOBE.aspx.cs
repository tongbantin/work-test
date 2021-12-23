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
    public partial class frm_TC021OBOBE : System.Web.UI.Page
    {
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    Label lb = (Label)Master.FindControl("lblFormTitle");
        //    lb.Text = "Confirmation_DL (Deposit)";
        //}
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
        //        Response.Redirect("~/Login.aspx");
        //}

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/View/ReportViewer.aspx?report=TC021OBOBE&b=" + txtBranch.Text + "&tf=" + calTransFrom.SelectedDate.ToString("dd/MM/yyyy") + "&tt=" + calTransTo.SelectedDate.ToString("dd/MM/yyyy") + "&df=" + txtDealFrom.Text + "&dt=" + txtDealTo.Text);
        //}

        public string TheUrl = "";
        public const string REPORT_USERCODE = "TC021OBOBE";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string branch = txtBranch.Text;
            string transStart = txtTransStartDate.Value;
            string transEnd = txtTransEndDate.Value;
            string dealfrom = txtDealNoFrom.Value;
            string dealto = txtDealNoTo.Value;


            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&b=" + branch + "&tf=" + transStart + "&tt=" + transEnd + "&df=" + dealfrom + "&dt=" + dealto;
            Page.DataBind();

        }
    }
}
