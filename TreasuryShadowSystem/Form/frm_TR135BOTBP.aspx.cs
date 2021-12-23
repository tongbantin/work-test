using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryShadowSystem.Configuration;
using System.IO;
using log4net;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using CrystalDecisions.Shared;
using System.Data;
namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR135BOTBP : System.Web.UI.Page
    {
        public string TheUrl = "";
        string reportname = "TR135BOTBP";
        ReportClass rpt = new ReportClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindYear();
        }

        void BindYear()
        {
            try
            {
                for (int i = DateTime.Now.Year + 1; i >= 2012; i--)
                    YearBox.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
            catch (Exception ex)
            {

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string MonthYear = YearBox.SelectedValue.ToString() + MonthBox.SelectedValue.ToString();
                rpt = Model_RPT_TR135BOTBP.getReport(Config.ConnectionString, MonthYear);
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
            catch (Exception ex)
            { 
            
            }
        }
    }
}
