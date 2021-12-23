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
    public class Model_RPT_TR008DBOBN
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR008DBOBN));
        public Model_RPT_TR008DBOBN()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR008DBOBN Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR008DBOBN.Owner + ".KKB_SP_TSS_TR008DBOBN");
            Log.Info("Data : Branch=" + Branch + ", Execute Date=" + ExecuteDate + ", To Date=" + ToDate);

            RPT_TR008DBOBN report = new RPT_TR008DBOBN();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR008DBOBN.Owner + ".KKB_SP_TSS_TR008DBOBN", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_to_date", ToDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR008DBOBN");
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
                Log.Info("-----Model_RPT_TR008DBOBN Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR008DBOBN Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR008DBOBN.Owner + ".KKB_SP_TSS_TR008DBOBN");
            Log.Info("Data : Branch=" + Branch + ", Execute Date=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR008DBOBN.Owner + ".KKB_SP_TSS_TR008DBOBN", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_to_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR008DBOBN");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR008DBOBN Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
