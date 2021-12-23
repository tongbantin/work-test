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
    public partial class frm_TR072OFOCF : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR072OFOCF";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            string days = "0";
            if (rdDays3.Checked)
                days = "3";
            else if (rdDays7.Checked)
                days = "7";
            else if (rdDays30.Checked)
                days = "30";
            TheUrl += "&b=" + txtBranch.Text + "&s=" + txtToExecuteDate.Value + "&t=" + txtToTradeDate.Value + "&o=" + days;
            Page.DataBind();
        }
    }
}
