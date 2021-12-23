using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;


namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR064DBORP
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR064DBORP));

        public Model_RPT_TR064DBORP()
        {

        }
        public static ReportClass getReport(string ConnectionString, string date)
        {
            //Log.Info("-----Model_RPT_TC021OBOBE Start-----");
            //Log.Info("Store Procedure : " + Model_RPT_TROREPO01.Owner + ".KKB_SP_TRO_REPO01");
            //Log.Info("Data : Branch=" + Branch + ", TradeDateFrom=" + TradeFrom + ", TradeDateTo=" + TradeTo + ", DealNoFrom=" + DealFrom + ", DealNoTo=" + DealTo);

            RPT_TR064DBORP report = new RPT_TR064DBORP();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR064DBORP.Owner + ".KKB_SP_TSS_TR064DBORP", CommandType.StoredProcedure);
                o.setInputParameter("p_date", date);
                s = o.executeReader("ref_rpt_cur", "TB_TR064DBORP");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report.SetDataSource(s);

                        //report.SetParameterValue("reportdate", date);

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
                Log.Info("-----Model_RPT_TR064DBORP Finish-----");
                o.closeConnection();
            }
            
            return report;
        }
    }
}
