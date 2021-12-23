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
    public class Model_RPT_TR072OFOCF
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR072OFOCF));

        public Model_RPT_TR072OFOCF()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ToSettDate, string ToTradeDate, string options)
        {
            Log.Info("-----Model_RPT_TR072OFOCF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR072OFOCF.Owner + ".KKB_SP_TSS_TR072OFOCF");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : To Settlement Date=" + ToSettDate);
            Log.Info("Data : To TradeDate Date=" + ToTradeDate);
            Log.Info("Data : Last Day=" + options);

            RPT_TR072OFOCF report = new RPT_TR072OFOCF();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR072OFOCF.Owner + ".KKB_SP_TSS_TR072OFOCF", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_settdate", ToSettDate);
                o.setInputParameter("p_tradedate", ToTradeDate);
                o.setInputParameter("p_option", options);
                s = o.executeReader("ref_rpt_cur", "TB_TR072OFOCF");
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
                Log.Info("-----Model_RPT_TR072OFOCF Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ToSettDate, string ToTradeDate, string options)
        {
            Log.Info("-----Model_RPT_TR072OFOCF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR072OFOCF.Owner + ".KKB_SP_TSS_TR072OFOCF");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : To Settlement Date=" + ToSettDate);
            Log.Info("Data : To Trade Date=" + ToTradeDate);
            Log.Info("Data : Last Day=" + options);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR072OFOCF.Owner + ".KKB_SP_TSS_TR072OFOCF", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_settdate", ToSettDate);
                o.setInputParameter("p_tradedate", ToTradeDate);
                o.setInputParameter("p_option", options);
                s = o.executeReader("ref_rpt_cur", "TB_TR072OFOCF");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR072OFOCF Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
