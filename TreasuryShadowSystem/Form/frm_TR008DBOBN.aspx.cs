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
    public partial class frm_TR008DBOBN : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR008DBOBN";

        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Bond Movement";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");

            if (!Page.IsPostBack)
            {
                if (Session["ReportDate"] != null)
                {
                    txtReportDateFrom.Value = Session["ReportDate"].ToString();
                    txtReportDateTo.Value = Session["ReportDate"].ToString();
                }
                else
                {
                    txtReportDateFrom.Value = DateTime.Today.ToString("dd/MM/yyyy");
                    txtReportDateTo.Value = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer.aspx?report=TR008DBOBN&ex=" + calExecuteDate.SelectedDate.ToString("dd/MM/yyyy") + "&t=" + calToDate.SelectedDate.ToString("dd/MM/yyyy"));

            string _date = txtReportDateFrom.Value;
            string _to = txtReportDateTo.Value;
            Session["ReportDate"] = _date;
            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&ex=" + _date + "&t=" + _to;
            Page.DataBind();
        }
    }
}
