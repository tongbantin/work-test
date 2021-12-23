using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryModel.Master;
using log4net;
using System.Data;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryReport.Report;
namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR099OBOBRFI : System.Web.UI.Page
    {
        public string TheUrl = "";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR099OBOBRFI));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                BindBroker();
                BindMonth();
                BindYear();
            }
        }

        private void BindBroker()
        {
            try
            {
                //DataTable t = Model_RPT_TR099OBOBRFI.GetBroker(Config.ConnectionString, Config.GetParameterFile("099Broker"));
                string message = Config.GetParameterFile("099Broker");
                var param = message.Split(',').Select(p => new { DisplayText = p.Trim(), Value = p.Trim() });
                BrokerBox.Items.Clear();
                BrokerBox.DataSource = param;
                BrokerBox.DataTextField = "DisplayText";
                BrokerBox.DataValueField = "Value";
                BrokerBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
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
                Log.Error(ex.Message);
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
                Log.Error(ex.Message);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //TheUrl = "../View/ReportViewer2.aspx?report=TR099OBOBRFI";
            //TheUrl += "&b=" + BrokerBox.SelectedValue + "&d=" + MontlyBox.SelectedValue + "/" + YearBox.SelectedValue;
            //Page.DataBind();

            try
            {
                ReportClass rpt = new ReportClass();
                rpt = Model_RPT_TR099OBOBRFI.getReport(Config.ConnectionString, BrokerBox.SelectedValue, ProductTypeBox.SelectedValue, MontlyBox.SelectedValue + "/" + YearBox.SelectedValue);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound("099OBOBRFI");
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "BrokerageFee" + "_" + DateTime.Now.ToString("yyyyMMdd");
                HttpContext.Current.Session["ReportSource"] = rpt;
                //return new { Result = "OK" };
                TheUrl = "../View/Viewer.aspx";
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}
