using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using log4net;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR108DBOEMIR
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR108DBOEMIR));
        public Model_RPT_TR108DBOEMIR()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate, string CustomerGroup, string Customer)
        {
            Log.Info("-----Model_RPT_TR108DBOEMIR Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR108DBOEMIR.Owner + ".KKB_SP_TSS_TR108DBOEMIR");
            Log.Info("Data : Branch=" + Branch + ", Execute Date="+ ExecuteDate + " , Customer Group=" + CustomerGroup + ", Customer=" + Customer);

            RPT_TR108DBOEMIR report = new RPT_TR108DBOEMIR();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR108DBOEMIR.Owner + ".KKB_SP_TSS_TR108DBOEMIR", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_customergroup", CustomerGroup);
                o.setInputParameter("p_customer", Customer);
                s = o.executeReader("ref_rpt_cur", "TB_TR108DBOEMIR");

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
                Log.Info("-----Model_RPT_TR108DBOEMIR Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
