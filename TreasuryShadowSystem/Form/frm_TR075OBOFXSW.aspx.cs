using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR075OBOFXSW : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR075OBOFXSW";

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer.aspx?report=TR055OBOFX&d=" + txtDealNo.Text.Trim());

            string deal = txtDealNo.Text;

            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&d=" + deal;
            Page.DataBind();
        }
    }
}
