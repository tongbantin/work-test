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
    public class Model_RPT_TR073DBOFIDF
    {
        private static string Owner = Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR073DBOFIDF));

        public Model_RPT_TR073DBOFIDF()
        {

        }

        public static ReportClass getReport(string ConnectionString, string Branch, string FromExecuteDate, string ToExecuteDate, string options)
        {
            Log.Info("-----Model_RPT_TR073DBOFIDF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR073DBOFIDF.Owner + ".KKB_SP_TSS_TR073DBOFIDF");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : To Execute Date=" + ToExecuteDate);
            Log.Info("Data : Last Day=" + options);

            RPT_TR073DBOFIDF report = new RPT_TR073DBOFIDF();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR073DBOFIDF.Owner + ".KKB_SP_TSS_TR073DBOFIDF", CommandType.StoredProcedure);
                o.setInputParameter("p_from_date", FromExecuteDate);
                o.setInputParameter("p_to_date", ToExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR073DBOFIDF");
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
                Log.Info("-----Model_RPT_TR073DBOFIDF Finish-----");
                o.closeConnection();
            }
            return report;
        }


        public static ReportClass getReport(string ConnectionString, string ProductType, string FromExecuteDate, string ToExecuteDate)
        {
            Log.Info("-----Model_RPT_TR073DBOFIDF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR073DBOFIDF.Owner + ".KKB_SP_TSS_TR073DBOFIDF");
            Log.Info("Data : To Execute Date=" + ToExecuteDate);
            Log.Info("Data : Product Type=" + ProductType);

            RPT_TR073DBOFIDF report = new RPT_TR073DBOFIDF();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR073DBOFIDF.Owner + ".KKB_SP_TSS_TR073DBOFIDF", CommandType.StoredProcedure);
                o.setInputParameter("p_product", ProductType);
                o.setInputParameter("p_from_date", FromExecuteDate);
                o.setInputParameter("p_to_date", ToExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR073DBOFIDF");
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
                Log.Info("-----Model_RPT_TR073DBOFIDF Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static DataSet getDataSet(string ConnectionString, string Branch, string ToExecuteDate, string options)
        {
            Log.Info("-----Model_RPT_TR073DBOFIDF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR073DBOFIDF.Owner + ".KKB_SP_TSS_TR073DBOFIDF");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : To Execute Date=" + ToExecuteDate);
            Log.Info("Data : Last Day=" + options);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR073DBOFIDF.Owner + ".KKB_SP_TSS_TR073DBOFIDF", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_to_execute_date", ToExecuteDate);
                o.setInputParameter("p_option", options);
                s = o.executeReader("ref_rpt_cur", "TB_TR073DBOFIDF");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR073DBOFIDF Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
