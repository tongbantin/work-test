using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Web.Services;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using System.Web.Script.Serialization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryModel.Model;
using System.Data;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryModel.Model;

namespace TreasuryShadowSystem.TR145BORATEAVG
{
    public partial class frm_AdjNostro : System.Web.UI.Page
    {
        private static string UserExecute;
        public string TheUrl = "";
        string reportname = "TR145BORATEAVG";        
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_AdjNostro));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
                UserExecute = Session["UserName"].ToString();
            if (!IsPostBack)
                BindAcctCode();
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

        [WebMethod(EnableSession = true)]
        public static object AdjNostroList(string Account, string FromDate, string ToDate, string Status, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return TreasuryModel.Data.AdjustNostro.AdjustNostroList(Config.ConnectionString, Account, FromDate , ToDate, Status, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateAdjNostro(TreasuryModel.Data.AdjustNostro record)
        {
            //record.CREATEBY = frm_AdjNostro.UserExecute;
            record.CREATEBY = HttpContext.Current.Session["Username"].ToString();
            return TreasuryModel.Data.AdjustNostro.CreateAdjustNostro(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateAdjNostro(TreasuryModel.Data.AdjustNostro record)
        {
            //string createby = record.CREATEBY == null ? "" : record.CREATEBY;
            //string reviewby = record.REVIEWBY == null ? "" : record.REVIEWBY;
            //string authorizeby = record.AUTHORIZEBY == null ? "" : record.AUTHORIZEBY;

            //if (record.CREATEBY.ToString().Equals(frm_AdjustNostro.UserExecute))
            //    return new { Result = "ERROR", Message = "You are Creater. Don't review/authorize" };

            //if (record.REVIEWBY != null)
            //{
            //    if (record.REVIEWBY.Equals(frm_AdjustNostro.UserExecute))
            //        return new { Result = "ERROR", Message = "You are Reviewer. Don't authorize" };
            //}
            string UserVerifyAuthorize = HttpContext.Current.Session["Username"].ToString();
            return TreasuryModel.Data.AdjustNostro.UpdateAdjustNostro(Config.ConnectionString, record, UserVerifyAuthorize);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteAdjNostro(int ADJNOSTROID)
        {
            return TreasuryModel.Data.AdjustNostro.DeleteAdjustNostro(Config.ConnectionString, ADJNOSTROID);
        }
        
        [WebMethod(EnableSession = true)]
        public static object GetAccountCode()
        {
            return TreasuryModel.Data.AdjustNostro.GetAccountCode(Config.ConnectionString, "2");
        }

        [WebMethod(EnableSession = true)]
        public static object GetAdjAccountCode()
        {
            return TreasuryModel.Data.AdjustNostro.GetAccountCode(Config.ConnectionString, "1");
        }

        [WebMethod(EnableSession = true)]
        public static object PrintAdjNostro(string ADJNOSTROID)
        {
            ReportClass rpt = new ReportClass();
            rpt = Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "DAILY_REPORT", ADJNOSTROID, "", "", "");
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

        //protected void PrintAdjNostro_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        rpt = Model_RPT_TR145BORATEAVG.getReport(Config.ConnectionString, "DAILY_REPORT", "22", "", "", "");
        //        if (rpt == null)
        //        {
        //            DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
        //            rpt = new RPT_MESSAGE();
        //            rpt.SetDataSource(ds);
        //        }
        //        rpt.SummaryInfo.ReportTitle = "AdjustNostro" + "_" + DateTime.Now.ToString("yyyyMMdd");
        //        Session["ReportSource"] = rpt;

        //        //TheUrl = "../View/Viewer.aspx";
        //        //Page.DataBind();
                
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#iReport').attr('src','../View/Viewer.aspx');$('.dialogreport').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, width: 1000});", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //    }
        //}

    }
}
