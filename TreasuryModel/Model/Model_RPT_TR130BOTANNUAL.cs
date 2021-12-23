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
    public class Model_RPT_TR130BOTANNUAL
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR130BOTANNUAL));

        
        public Model_RPT_TR130BOTANNUAL()
        {

        }
        public static ReportClass getReport(string ConnectionString, string StartDate, string EndDate)
        {
            Log.Info("-----Model_RPT_TR130BOTANNUAL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR130BOTANNUAL.Owner + ".KKB_SP_TSS_TR130BOTANNUAL");
            //Log.Info("Data : DealNo=" + DealNo + ", Customer Type=" + CustomerType);

            ReportClass report = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR130BOTANNUAL.Owner + ".KKB_SP_TSS_TR130BOTANNUAL", CommandType.StoredProcedure);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR130BOTANNUAL");

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        s.Tables[0].TableName = "TB_TR130BOTANNUAL";
                        report = new RPT_TR130BOTANNUAL();
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
                Log.Info("-----Model_RPT_TR130BOTANNUAL Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
