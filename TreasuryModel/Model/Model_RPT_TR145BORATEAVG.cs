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

namespace TreasuryModel.Model
{
    public class Model_RPT_TR145BORATEAVG
    {
        private static string Owner = "OPICINF";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR145BORATEAVG));

        public static ReportClass getReport(string ConnectionString, string Report, string AdjNostroID, string AcctCode, string StartDate, string EndDate)
        {
            Log.Info("-----Model_RPT_TR145BORATEAVG Start-----");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                string TableName = "";
                if ((Report.Equals("DAILY_REPORT")) || (Report.Equals("PLTHB_REPORT")) || (Report.Equals("CANCELDIFF_REPORT")) || (Report.Equals("CANCELPL_REPORT")))
                {
                    TableName = "TB_TR145BORATEAVG";
                    report = new RPT_TR145BORATEAVG();
                }
                else if (Report.Equals("NOSTRO_REPORT"))
                {
                    TableName = "TB_TR145NOSTROAVG";
                    report = new RPT_TR145NOSTROAVG();
                }

                o.openConnection();
                o.createCommand(Model_RPT_TR145BORATEAVG.Owner + ".KKB_SP_TSS_TR145BOREPORT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_adjnostro", AdjNostroID);
                o.setInputParameter("p_acctcode", AcctCode);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                s = o.executeReader("ref_rpt_cur", TableName);

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
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
                Log.Info("-----Model_RPT_TR145BORATEAVG Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static DataSet getReport(string ConnectionString, string Report, string AcctCode, string StartDate, string EndDate)
        {
            Log.Info("-----Model_RPT_TR145BORATEAVG Start-----");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR145BORATEAVG.Owner + ".KKB_SP_TSS_TR145BOREPORT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_adjnostro", "");
                o.setInputParameter("p_acctcode", AcctCode);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                s = o.executeReader("ref_rpt_cur", "TR145BOREPORT");
                return s;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR145BORATEAVG Finish-----");
                o.closeConnection();
            }
        }

        public static DataSet getDataSet(string ConnectionString, string Report, string CancelNo)
        {
            Log.Info("-----Model_RPT_TR145BORATEAVG Start-----");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR145BORATEAVG.Owner + ".KKB_SP_TSS_TR145BOREPORT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_adjnostro", "");
                o.setInputParameter("p_acctcode", CancelNo);
                o.setInputParameter("p_startdate", "");
                o.setInputParameter("p_enddate", "");
                s = o.executeReader("ref_rpt_cur", "TR145BOREPORT");
                return s;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR145BORATEAVG Finish-----");
                o.closeConnection();
            }
        }


        public static bool calcNostroAvg(string ConnectionString, string AcctCode, string StartDate, string EndDate, string CreateBy)
        {
            Log.Info("-----Model_RPT_TR145BORATEAVG Start-----");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR145BORATEAVG.Owner + ".KKB_SP_TSS_TR145NOSTROAVG", CommandType.StoredProcedure);
                o.setInputParameter("p_acctcode", AcctCode);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                o.setInputParameter("p_createby", CreateBy);
                o.executeNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR145BORATEAVG Finish-----");
                o.closeConnection();
            }
        }
    }
}
