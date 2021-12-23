using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;
using System.Collections;

namespace KKB.Treasury.TreasuryModel.Model
{
    public enum PATRABE_REPORT
    {
        PTSUMMARY,
        PTCONFBE,
        PTOBE,
        PTMBE,
        PTFEEBE,
        PTINVOICE,
        PTSECINV,
        PTSECBN
    }

    public class Model_RPT_TR138PTBE
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR138PTBE));
        
        public static ReportClass getReport(string ConnectionString, string ReportName, string FromDate, string ToDate, string[] Fee)
        {
            Log.Info("-----Model_RPT_TR138PTBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR138PTBE.Owner + ".KKB_SP_TSS_TR138PTBE");

            string ParamFee = "";
            string feeticket = Fee[0];
            string feebn = Fee[1];

            ReportClass report = null;
            string TableName="";
            if (ReportName==PATRABE_REPORT.PTSUMMARY.ToString())
            {
                TableName = "TB_TR138PTSUMMARY";
                report = new RPT_TR138PTSUMMARY();
            }
            else if (ReportName == PATRABE_REPORT.PTINVOICE.ToString())
            {
                TableName = "TB_TR138INV";
                report = new RPT_TR138INV();
            }
            else if(ReportName == PATRABE_REPORT.PTSECINV.ToString()){
                ParamFee = feeticket;
                TableName = "TB_TR138PTSECINV";
                report = new RPT_TR138PTSECINV();
            }
            else if (ReportName == PATRABE_REPORT.PTSECBN.ToString())
            {
                ParamFee = feebn;
                TableName = "TB_TR138PTSECINV";
                report = new RPT_TR138PTSECINV();            
            }
            else
            {
                if (ReportName == PATRABE_REPORT.PTFEEBE.ToString()) { 
                    TableName = "TB_TR138PTBE";
                    report = new RPT_TR138PTBE1();
                }
                else if (ReportName == PATRABE_REPORT.PTOBE.ToString()){
                    TableName = "TB_TR138PTBE";
                    report = new RPT_TR138PTBE2();
                }
                else {
                    TableName = "TB_TR138PTBE";
                    report = new RPT_TR138PTBE3();
                }                
            }
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR138PTBE.Owner + ".KKB_SP_TSS_TR138PTBE", CommandType.StoredProcedure);
                o.setInputParameter("p_report", ReportName);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                o.setInputParameter("p_fee", ParamFee);
                s = o.executeReader("ref_rpt_cur", TableName);

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report = Model_RPT_TR138PTBE.ReportFileName(ReportName, ToDate, report);
                        report.SetDataSource(s);
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                    }
                    else
                    {
                        report = null;
                    }
                }
                else
                    report = null;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR136PTBE Finish-----");
                o.closeConnection();
            }
            return report;
        }

        private static ReportClass ReportFileName(string ReportName, string ToDate, ReportClass rpt) {
            try
            {
                Log.Info("-----Model_RPT_TR138PTBE ReportFileName-----");
                if (ToDate.Equals(""))
                    ToDate = DateTime.Now.ToString("dd/MM/yyyy");
                string[] d = ToDate.Split('/');
                switch (ReportName)
                {
                    case "PTSUMMARY":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PT_Summary_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTCONFBE":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PT_ConfirmBUY_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTOBE":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PT_Outstd_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTMBE":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PT_Maturity_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTFEEBE":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PT_EOMFee_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTINVOICE":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PT_INVOICE_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTSECINV":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PTSEC_INVOICE_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                    case "PTSECBN":
                        rpt.SummaryInfo.ReportTitle = "BEKK_PTSEC_BN_" + d[2].Substring(2) + d[1] + d[0];
                        break;
                }
                Log.Info("report name : " + rpt.SummaryInfo.ReportTitle);
                return rpt;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return rpt;
            }
        }
    }

}
