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
    public class Model_RPT_TR051TAX
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR051TAX));
        public Model_RPT_TR051TAX()
        {

        }
        public static ReportClass getReport(string ConnectionString, string SenderRef, string RefNo)
        {
            Log.Info("-----Model_RPT_TR051TAX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR051TAX.Owner + ".KKB_SP_TSS_TR051TAX");
            Log.Info("Data : SenderRef=" + SenderRef);
            Log.Info("Data : RefNo=" + RefNo);

            RPT_TF026OBOTX report = new RPT_TF026OBOTX();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR051TAX.Owner + ".KKB_SP_TSS_TR051TAX", CommandType.StoredProcedure);
                o.setInputParameter("p_senderref", SenderRef);
                o.setInputParameter("p_refno", RefNo);
                s = o.executeReader("ref_rpt_cur", "TB_TF026OBOTX");
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
                Log.Info("-----Model_RPT_TR051TAX Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string SenderRef, string RefNo)
        {
            Log.Info("-----Model_RPT_TR051TAX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR051TAX.Owner + ".KKB_SP_TSS_TR051TAX");
            Log.Info("Data : SenderRef=" + SenderRef);
            Log.Info("Data : RefNo=" + RefNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR051TAX.Owner + ".KKB_SP_TSS_TR051TAX", CommandType.StoredProcedure);
                o.setInputParameter("p_senderref", SenderRef);
                o.setInputParameter("p_RefNo", RefNo);
                s = o.executeReader("ref_rpt_cur", "TB_TF026OBOTX");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR051TAX Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
