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
    public partial class frm_TC022OBOBE1 : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TC022OBOBE";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/SessionExpired.aspx");

            if (!Page.IsPostBack)
                bindCounterParty();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string branch = txtBranch.Text;
            string cp = ddParty.SelectedValue;
            string maturity = txtMaturityDate.Value;


            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&b=" + branch + "&cp=" + cp +"&mf=" + maturity;
            Page.DataBind();
        }

        private void bindCounterParty()
        {
            DataSet s = Model_MS_COUNTERPARTY.getDataSet(Configuration.Config.ConnectionString);
            ddParty.DataSource = s;
            ddParty.DataTextField = "COUNTERPARTY";
            ddParty.DataValueField = "CNO";
            ddParty.DataBind();
        }
    }
}
