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
    public class Model_RPT_TR025OITAL
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR025OITAL));
        public Model_RPT_TR025OITAL()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string TransFrom, string TransTo, string CashIStatus, string SecuritiesIStatus, string ThirdPaytyStatus)
        {
            Log.Info("-----Model_RPT_TR025OITAL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR025OITAL.Owner + ".KKB_SP_TSS_TR025OITAL");
            Log.Info("Data : Branch=" + Branch + ", TransDateFrom=" + TransFrom + ", TransDateTo=" + TransTo + ", CashInfStatus=" + CashIStatus + ", SecInfStatus=" + SecuritiesIStatus + ", ThirdParyStatus=" + ThirdPaytyStatus);

            RPT_TR025OITAL report = new RPT_TR025OITAL();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR025OITAL.Owner + ".KKB_SP_TSS_TR025OITAL", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_trans_date_from", TransFrom);
                o.setInputParameter("p_trans_date_to", TransTo);
                o.setInputParameter("p_cash_inf_status", CashIStatus);
                o.setInputParameter("p_sec_inf_status", SecuritiesIStatus);
                o.setInputParameter("p_third_party_status", ThirdPaytyStatus);
                s = o.executeReader("ref_rpt_cur", "TB_TR025OITAL");
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
                Log.Info("-----Model_RPT_TR025OITAL Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
