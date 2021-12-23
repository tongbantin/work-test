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
    public class Model_RPT_TR134BOLCR
    {

        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR134BOLCR));
                
        public Model_RPT_TR134BOLCR()
        {

        }

        public static ReportClass getReport(string ConnectionString, string Report, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR134BOLCR Start-----");
            //Log.Info("Data : DealNo=" + DealNo + ", Customer Type=" + CustomerType);

            Oracle o = Oracle.getInstance(ConnectionString);
                ReportClass report = null;
            try
            {
                DataSet s = null;
                o.openConnection();
                if (Report.Equals("Detail"))
                {

                    o.createCommand(Model_RPT_TR134BOLCR.Owner + ".KKB_SP_TSS_TR134BOLCRDETAIL", CommandType.StoredProcedure);
                    o.setInputParameter("p_execute_date", ExecuteDate);
                    s = o.executeReader("ref_rpt_cur", "TB_TR134BOLCRDETAIL");
                }
                else
                {
                    o.createCommand(Model_RPT_TR134BOLCR.Owner + ".KKB_SP_TSS_TR134BOLCRSUMMARY", CommandType.StoredProcedure);
                    o.setInputParameter("p_report", Report);
                    o.setInputParameter("p_execute_date", ExecuteDate);
                    s = o.executeReader("ref_rpt_cur", "TB_TR134BOLCRSUMMARY");
                }

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        if (Report.Equals("Detail"))
                        {
                            report = new RPT_TR134BOLCRDETAIL();
                            report.SetDataSource(s);
                        }
                        else
                        {
                            report = new RPT_TR134BOLCRSUMMARY();
                            report.SetDataSource(s);
                        }
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
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
                Log.Info("-----Model_RPT_TR134BOLCR Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
