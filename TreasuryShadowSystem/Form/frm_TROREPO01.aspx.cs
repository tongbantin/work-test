using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using CrystalDecisions.Shared;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TROREPO01 : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR0REPO01";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ReportDate"] != null)
                    txtReportDate.Value = Session["ReportDate"].ToString();
                else
                {
                    txtReportDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    //txtReportDate.Value = Model.Model_Tb_Master.getSysDate().ToString("yyyy-mm-dd");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string _date = txtReportDate.Value;
            Session["ReportDate"] = _date;
            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&d=" + _date; ;
            Page.DataBind();
                           
        }
    }
}
