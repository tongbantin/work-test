using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR085DBOIRS
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR085DBOIRS));

        public Model_RPT_TR085DBOIRS()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate, string Cost)
        {
            Log.Info("-----Model_RPT_TR085DBOIRS Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR085DBOIRS.Owner + ".KKB_SP_TSS_TR085DBOIRS");
            Log.Info("Data : Branch=" + Branch);

            RPT_TR085DBOIRS report = new RPT_TR085DBOIRS();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR085DBOIRS.Owner + ".KKB_SP_TSS_TR085DBOIRS", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_cost", Cost);
                o.setInputParameter("p_showloc", "0");
                s = o.executeReader("ref_rpt_cur", "TB_TR085DBOIRS");
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
                Log.Info("-----Model_RPT_TR085DBOIRS Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate, string Cost)
        {
            Log.Info("-----Model_RPT_TR085DBOIRS Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR085DBOIRS.Owner + ".KKB_SP_TSS_TR085DBOIRS");
            Log.Info("Data : DealNo=" + Branch);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR085DBOIRS.Owner + ".KKB_SP_TSS_TR085DBOIRS", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_cost", Cost);
                s = o.executeReader("ref_rpt_cur", "TB_TR085DBOIRS");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR085DBOIRS Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
