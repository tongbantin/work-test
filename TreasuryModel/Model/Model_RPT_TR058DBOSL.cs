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
    public class Model_RPT_TR058DBOSL
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR058DBOSL));

        public Model_RPT_TR058DBOSL()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR058DBOSL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR058DBOSL.Owner + ".KKB_SP_TSS_TR058DBOSL");
            Log.Info("Data : DealNo=" + DealNo);

            RPT_TR058DBOSL report = new RPT_TR058DBOSL();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR058DBOSL.Owner + ".KKB_SP_TSS_TR058DBOSL", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR058DBOSL");
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
                Log.Info("-----Model_RPT_TR058DBOSL Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR058DBOSL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR058DBOSL.Owner + ".KKB_SP_TSS_TR058DBOSL");
            Log.Info("Data : DealNo=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR058DBOSL.Owner + ".KKB_SP_TSS_TR058DBOSL", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR058DBOSL");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR058DBOSL Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
