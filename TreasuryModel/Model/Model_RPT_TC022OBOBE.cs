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
    public class Model_RPT_TC022OBOBE
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TC022OBOBE));
        public Model_RPT_TC022OBOBE()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string CounterParty, string MaturityFrom)
        {
            Log.Info("-----Model_RPT_TC022OBOBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TC022OBOBE.Owner + ".KKB_SP_TSS_TC022OBOBE");
            Log.Info("Data : Branch=" + Branch + ", Counter Party=" + CounterParty + ", Maturity Date From=" + MaturityFrom);

            RPT_TC022OBOBE report = new RPT_TC022OBOBE();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                DataSet q = null;
                o.openConnection();
                o.createCommand(Model_RPT_TC022OBOBE.Owner + ".KKB_SP_TSS_TC022OBOBE", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_counter_party", CounterParty);
                o.setInputParameter("p_maturity_date", MaturityFrom);
                s = o.executeReader("ref_rpt_cur", "TB_TC022OBOBE");

                o.createCommand(Model_RPT_TC022OBOBE.Owner + ".KKB_SP_TSS_TC022OBOBEPAY", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_counter_party", CounterParty);
                o.setInputParameter("p_maturity_date", MaturityFrom);
                q = o.executeReader("ref_rpt_cur", "TB_TC022OBOBE_PAYMENT");
                if ((s != null) && (q != null))
                {
                    //s.Tables.Add("TB_TC022OBOBE_PAYMENT");
                    s.Merge(q.Tables[0]);
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
                Log.Info("-----Model_RPT_TC022OBOBE Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
