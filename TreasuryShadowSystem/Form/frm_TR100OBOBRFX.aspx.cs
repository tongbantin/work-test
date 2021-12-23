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
using TreasuryModel.Master;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR100OBOBRFX : System.Web.UI.Page
    {
        public string TheUrl = "";
        private DataTable brokTable = new DataTable();
        private List<string> broker = new List<string>();

        string reportname = "TR100OBOBRFX";
        ReportClass rpt = new ReportClass();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                string message = Config.GetParameterFile("100Brok");
                broker = message.Split(',').Select(p => p.Trim()).ToList<string>();
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                BindMonth();
                BindYear();
                BindBroker();
            }
        }

        private void BindBroker()
        {
            try
            {
                BrokerBox.DataSource = broker;
                BrokerBox.DataBind();
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void BindMonth()
        {
            try
            {
                MontlyBox.DataSource = Model_MS_CALENDAR.GetMonths();
                MontlyBox.DataTextField = "MonthName";
                MontlyBox.DataValueField = "MonthID";
                MontlyBox.DataBind();

                MontlyBox.SelectedValue = DateTime.Now.Month.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindYear()
        {
            try
            {
                YearBox.DataSource = Model_MS_CALENDAR.GetYears();
                YearBox.DataTextField = "YearName";
                YearBox.DataValueField = "YearName";
                YearBox.DataBind();

                YearBox.SelectedValue = DateTime.Now.Year.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /*
            TheUrl = "../View/ReportViewer2.aspx?report=TR100OBOBRFX";
            TheUrl += "&b=" + BrokerBox.SelectedValue + "&d=" + MontlyBox.SelectedValue + "/" + YearBox.SelectedValue;
            Page.DataBind();
            */

            rpt = rpt = Model_RPT_TR100OBOBRFX.getReport(Config.ConnectionString, BrokerBox.SelectedValue, ProductTypeBox.SelectedValue, MontlyBox.SelectedValue + "/" + YearBox.SelectedValue);
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
    }
}
