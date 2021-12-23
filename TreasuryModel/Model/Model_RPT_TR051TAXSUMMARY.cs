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
    public class Model_RPT_TR051TAXSUMMARY
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR051TAXSUMMARY));
        public Model_RPT_TR051TAXSUMMARY()
        {

        }
        public static ReportClass getReport(string ConnectionString, string StartDate, string EndDate)
        {
            Log.Info("-----Model_RPT_TR051TAXSUMMARY Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR051TAXSUMMARY.Owner + ".KKB_SP_TSS_TR051TAXSUMMARY");
            Log.Info("Data : Start Date=" + StartDate);
            Log.Info("Data : End Date=" + EndDate);

            RPT_TR051TAXSUMMARY report = new RPT_TR051TAXSUMMARY();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR051TAXSUMMARY.Owner + ".KKB_SP_TSS_TR051TAXSUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR051TAXSUMMARY");
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
                Log.Info("-----Model_RPT_TR051TAXSUMMARY Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string StartDate, string EndDate)
        {
            Log.Info("-----Model_RPT_TR051TAXSUMMARY Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR051TAXSUMMARY.Owner + ".KKB_SP_TSS_TR051TAXSUMMARY");
            Log.Info("Data : StartDate=" + StartDate);
            Log.Info("Data : EndDate=" + EndDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR051TAXSUMMARY.Owner + ".KKB_SP_TSS_TR051TAXSUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                s = o.executeReader("ref_rpt_cur", "TB_TF051TAXSUMMARY");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR051TAXSUMMARY Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
