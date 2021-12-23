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
    public class Model_RPT_TR069FMALLPM
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR069FMALLPM));

        public Model_RPT_TR069FMALLPM()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR069FMALLPM Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR069FMALLPM.Owner + ".KKB_SP_TSS_TR069FMALLPM");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : Execute Date=" + ExecuteDate);

            RPT_TR069FMALLPM report = new RPT_TR069FMALLPM();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR069FMALLPM.Owner + ".KKB_SP_TSS_TR069FMALLPM", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR069FMALLPM");
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
                Log.Info("-----Model_RPT_TR069FMALLPM Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR069FMALLPM Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR069FMALLPM.Owner + ".KKB_SP_TSS_TR069FMALLPM");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : Execute Date=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR069FMALLPM.Owner + ".KKB_SP_TSS_TR069FMALLPM", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR069FMALLPM");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR069FMALLPM Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
