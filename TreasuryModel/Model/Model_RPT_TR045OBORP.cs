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
    public class Model_RPT_TR045OBORP
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR045OBORP));
        public Model_RPT_TR045OBORP()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR045OBORP Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR045OBORP.Owner + ".KKB_SP_TSS_TR045OBORP");
            Log.Info("Data : Deal No=" + DealNo);

            RPT_TR045OBORP report = new RPT_TR045OBORP();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand( Model_RPT_TR045OBORP.Owner + ".KKB_SP_TSS_TR045OBORP", CommandType.StoredProcedure);
                o.setInputParameter("p_deal_no", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR045OBORP");
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
                Log.Info("-----Model_RPT_TR045OBORP Finish-----");
                o.closeConnection();
            }
            
            return report;
        }
    }
}
