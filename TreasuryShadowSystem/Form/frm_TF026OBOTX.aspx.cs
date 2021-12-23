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
    public partial class frm_TF026OBOTX : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TF026OBOTX";

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "TAX Certificate";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer.aspx?report=TF026OBOTX&b=" + txtBranch.Text + "&d=" + txtRefNo.Text);
            string branch = txtBranch.Text;
            string refer = txtRef.Text;

            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&b=" + branch + "&d=" + refer;
            Page.DataBind();
        }
    }
}
