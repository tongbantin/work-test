using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using log4net;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;
using System.Data;
using TreasuryModel.Data;
using TreasuryModel.Model;
using System.Web.Services;

namespace TreasuryShadowSystem.TR145BORATEAVG
{
    public partial class frm_FXNostroAvg : System.Web.UI.Page
    {
        public static string UserExecute;
        public string TheUrl = "";
        string reportname = "TR145NOSTROAVG";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_FXNostroAvg));

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridNostroAvg.PageIndexChanging += new GridViewPageEventHandler(GridNostroAvg_PageIndexChanging);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
                UserExecute = Session["UserName"].ToString();
            //if (Session["UserGroup"] != null)
            //{
            //    if (Session["UserGroup"].Equals("Treasury_Oper"))
            //        btnCalculate.Visible = true;
            //    else
            //        btnCalculate.Visible = false;
            //}
            //else
            //    btnCalculate.Visible = false;

            if (!IsPostBack)
            {
                BindAcctCode();
            }
        }

        void GridNostroAvg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridNostroAvg.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void BindAcctCode()
        {
            try
            {
                DataTable dt = AdjustNostro.GetAccount(Config.ConnectionString);
                AccountBox.DataSource = dt;
                AccountBox.DataTextField = "ACCTDESC";
                AccountBox.DataValueField = "ACCTCODEID";
                AccountBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Account = AccountBox.SelectedValue;
                string FromDate = FromDateBox.Text.Trim();
                string ToDate = ToDateBox.Text.Trim();

                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void BindGrid()
        {
            try
            {
                DataSet ds = TreasuryModel.Model.Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "NOSTRO_REPORT", AccountBox.SelectedValue, FromDateBox.Text, ToDateBox.Text);
                GridNostroAvg.DataSource = ds.Tables[0];
                GridNostroAvg.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                string Account = AccountBox.SelectedValue;
                string FromDate = FromDateBox.Text.Trim();
                string ToDate = ToDateBox.Text.Trim();
                bool result = TreasuryModel.Model.Model_RPT_TR145BORATEAVG.calcNostroAvg(Config.ConnectionString, Account, FromDate, ToDate, frm_FXNostroAvg.UserExecute);
                
                if (result)
                {
                    MessageBox.Text = "ระบบคำนวณเรียบร้อย";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "เกิดข้อผิดพลาก กรุณาตรวจสอบเงื่อนไข";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object PrintNostroAccount(string NOSTAVGID)
        {
            try
            {
                ReportClass rpt = new ReportClass();
                rpt = Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "DAILY_REPORT", NOSTAVGID, "", "", "");
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound("TR145BORATEAVG");
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "AdjustNostro" + "_" + DateTime.Now.ToString("yyyyMMdd");
                HttpContext.Current.Session["ReportSource"] = rpt;
                return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
        
        [WebMethod(EnableSession = true)]
        public static object PrintPLTHBAccount(string NOSTAVGID)
        {
            try
            {
                ReportClass rpt = new ReportClass();
                rpt = Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "PLTHB_REPORT", NOSTAVGID, "", "", "");
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound("TR145BORATEAVG");
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "AdjustNostro" + "_" + DateTime.Now.ToString("yyyyMMdd");
                HttpContext.Current.Session["ReportSource"] = rpt;
                return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object PrintNostroAverageSummary(string Account, string FromDate, string ToDate)
        {
            try
            {
                ReportClass rpt = new ReportClass();
                rpt = TreasuryModel.Model.Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "NOSTRO_REPORT", "", Account, FromDate, ToDate);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound("TR145NOSTROAVG");
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                rpt.SummaryInfo.ReportTitle = "NostroAvg_" + DateTime.Now.ToString("yyyyMMdd");
                HttpContext.Current.Session["ReportSource"] = rpt;
                return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}
