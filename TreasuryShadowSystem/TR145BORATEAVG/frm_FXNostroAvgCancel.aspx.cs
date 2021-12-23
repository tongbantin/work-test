using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using TreasuryModel.Data;
using TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;
using log4net;
using System.Web.Services;
using KKB.Treasury.TreasuryReport.Report;

namespace TreasuryShadowSystem.TR145BORATEAVG
{
    public partial class frm_FXNostroAvgCancel : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                BindCancelNo();
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

        protected void BindCancelNo()
        {
            try
            {
                DataTable dt = AdjustNostro.GetCancelNo(Config.ConnectionString);
                CancelNoBox.DataSource = dt;
                CancelNoBox.DataTextField = "CANCELDESC";
                CancelNoBox.DataValueField = "CANCELNO";
                CancelNoBox.DataBind();
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
                DataSet ds = TreasuryModel.Model.Model_RPT_TR145BORATEAVG.getDataSet(Config.ConnectionString, "CANCEL_REPORT", CancelNoBox.SelectedValue);
                GridNostroAvg.DataSource = ds.Tables[0];
                GridNostroAvg.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object PrintCancelDiffSpotRate(string NOSTAVGHISID)
        {
            try
            {
                ReportClass rpt = new ReportClass();
                rpt = Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "CANCELDIFF_REPORT", NOSTAVGHISID, "", "", "");
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
        public static object PrintCancelPLTHBAccount(string NOSTAVGHISID)
        {
            try
            {
                ReportClass rpt = new ReportClass();
                rpt = Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "CANCELPL_REPORT", NOSTAVGHISID, "", "", "");
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
    }
}
