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
    public class Model_RPT_TR051BONTH
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR051BONTH));
        public Model_RPT_TR051BONTH()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo, string NoSign)
        {
            Log.Info("-----Model_RPT_TR051BONTH Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR051BONTH.Owner + ".KKB_SP_TSS_TR051BONTH");
            Log.Info("Data : DealNo=" + DealNo);
            Log.Info("Data : NoSign=" + NoSign);

            RPT_TR051BONTH report = new RPT_TR051BONTH();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR051BONTH.Owner + ".KKB_SP_TSS_TR051BONTH", CommandType.StoredProcedure);
                o.setInputParameter("p_deal_no", DealNo);
                o.setInputParameter("p_nosign", NoSign);
                s = o.executeReader("ref_rpt_cur", "TB_TR051BONTH");
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
                Log.Info("-----Model_RPT_TR051BONTH Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string DealNo, string NoSign)
        {
            Log.Info("-----Model_RPT_TR051BONTH Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR051BONTH.Owner + ".KKB_SP_TSS_TR051BONTH");
            Log.Info("Data : DealNo=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR051BONTH.Owner + ".KKB_SP_TSS_TR051BONTH", CommandType.StoredProcedure);
                o.setInputParameter("p_deal_no", DealNo);
                o.setInputParameter("p_nosign", NoSign);
                s = o.executeReader("ref_rpt_cur", "TB_TR051BONTH");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR051BONTH Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
