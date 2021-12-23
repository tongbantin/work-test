using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using log4net;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR070DFOTD
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR070DFOTD));

        public Model_RPT_TR070DFOTD()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate, string SettlementDate)
        {
            Log.Info("-----Model_RPT_TR070DFOTD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR070DFOTD.Owner + ".KKB_SP_TSS_TR070DFOTD");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : Execute Date=" + ExecuteDate);

            RPT_TR070DFOTD report = new RPT_TR070DFOTD();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR070DFOTD.Owner + ".KKB_SP_TSS_TR070DFOTD", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_settlement_date", SettlementDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR070DFOTD");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report.SetDataSource(s);
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                        Log.Info(s.GetXml());
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
                Log.Info("-----Model_RPT_TR070DFOTD Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate, string SettlementDate)
        {
            Log.Info("-----Model_RPT_TR070DFOTD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR070DFOTD.Owner + ".KKB_SP_TSS_TR070DFOTD");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : Settlement Date=" + SettlementDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR070DFOTD.Owner + ".KKB_SP_TSS_TR070DFOTD", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_settlement_date", SettlementDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR070DFOTD");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR070DFOTD Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
