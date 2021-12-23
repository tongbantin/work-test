using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using KKB.Treasury.TreasuryModel.Configuration;
using log4net;


namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR077OBOFXGL
    {
        private static string Owner = Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR077OBOFXGL));

        public Model_RPT_TR077OBOFXGL()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR077OBOFXGL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR077OBOFXGL.Owner + ".KKB_SP_TSS_TR077OBOFXGL");
            Log.Info("Branch=" + Branch);
            Log.Info("Execute Date=" + ExecuteDate);

            Oracle o = Oracle.getInstance(ConnectionString);
            RPT_TR077OBOFXGL report = new RPT_TR077OBOFXGL();
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR077OBOFXGL.Owner + ".KKB_SP_TSS_TR077OBOFXGL", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR077OBOFXGL");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = s.Tables[0].Rows[0];
                        report.SetDataSource(s);
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                        Log.Info(s.GetXml());
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR077OBOFXGL Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR077OBOFXGL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR077OBOFXGL.Owner + ".KKB_SP_TSS_TR077OBOFXGL");
            Log.Info("Branch=" + Branch);
            Log.Info("Execute Date=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR077OBOFXGL.Owner + ".KKB_SP_TSS_TR077OBOFXGL", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR077OBOFXGL");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR077OBOFXGL Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
