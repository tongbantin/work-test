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
    public class Model_RPT_TR049DBOFX
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR049DBOFX));

        public Model_RPT_TR049DBOFX()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate, string Cost)
        {
            Log.Info("-----Model_RPT_TR049DBOFX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR049DBOFX.Owner + ".KKB_SP_TSS_TR049DBOFX");
            Log.Info("Data : Branch=" + Branch);

            RPT_TR049DBOFX report = new RPT_TR049DBOFX();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR049DBOFX.Owner + ".KKB_SP_TSS_TR049DBOFX", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_cost", Cost);
                s = o.executeReader("ref_rpt_cur", "TB_TR049DBOFX");
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
                Log.Info("-----Model_RPT_TR049DBOFX Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate, string Cost)
        {
            Log.Info("-----Model_RPT_TR049DBOFX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR049DBOFX.Owner + ".KKB_SP_TSS_TR049DBOFX");
            Log.Info("Data : DealNo=" + Branch);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR049DBOFX.Owner + ".KKB_SP_TSS_TR049DBOFX", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_cost", Cost);
                s = o.executeReader("ref_rpt_cur", "TB_TR049DBOFX");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR049DBOFX Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static ReportClass getReport(string p, string p_2, string p_3)
        {
            throw new NotImplementedException();
        }
    }
}
