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
    public class Model_RPT_TC021OBOBE
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TC021OBOBE));
        public Model_RPT_TC021OBOBE()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string TradeFrom, string TradeTo, string DealFrom, string DealTo)
        {
            Log.Info("-----Model_RPT_TC021OBOBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TC021OBOBE.Owner + ".KKB_SP_TSS_TC021OBOBE");
            Log.Info("Data : Branch=" + Branch + ", TradeDateFrom=" + TradeFrom + ", TradeDateTo=" + TradeTo + ", DealNoFrom=" + DealFrom + ", DealNoTo=" + DealTo);

            RPT_TC021OBOBE report = new RPT_TC021OBOBE();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand( Model_RPT_TC021OBOBE.Owner + ".KKB_SP_TSS_TC021OBOBE", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_trade_date_from", TradeFrom);
                o.setInputParameter("p_trade_date_to", TradeTo);
                o.setInputParameter("p_deal_no_from", DealFrom);
                o.setInputParameter("p_deal_no_to", DealTo);
                s = o.executeReader("ref_rpt_cur", "TB_TC021OBOBE");
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
                Log.Info("-----Model_RPT_TC021OBOBE Finish-----");
                o.closeConnection();
            }
            
            return report;
        }
    }
}
