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
    public class Model_RPT_TR130BOTANNUALD
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR130BOTANNUALD));

        
        public Model_RPT_TR130BOTANNUALD()
        {

        }
        public static ReportClass getReport(string ConnectionString, string StartDate, string EndDate)
        {
            Log.Info("-----Model_RPT_TR130BOTANNUALD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130BOTANNUALD");
            //Log.Info("Data : DealNo=" + DealNo + ", Customer Type=" + CustomerType);

            ReportClass report = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130BOTANNUALD", CommandType.StoredProcedure);
                o.setInputParameter("p_startdate", StartDate);
                o.setInputParameter("p_enddate", EndDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR130BOTANNUALD");

                o.createCommand(Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130BOND", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", EndDate);
                DataSet ds1 = o.executeReader("ref_rpt_cur", "TB_BOND");
                s.Merge(ds1);

                o.createCommand(Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130BONDSECSCRIP", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", EndDate);
                DataSet ds2 = o.executeReader("ref_rpt_cur", "TB_BONDSECSCRIP");
                s.Merge(ds2);

                o.createCommand(Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130THAIBMASCRIP", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", EndDate);
                DataSet ds3 = o.executeReader("ref_rpt_cur", "TB_BONDTHAIBMASCRIP");
                s.Merge(ds3);

                o.createCommand(Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130FX", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", EndDate);
                DataSet ds4 = o.executeReader("ref_rpt_cur", "TB_FX");
                s.Merge(ds4);

                o.createCommand(Model_RPT_TR130BOTANNUALD.Owner + ".KKB_SP_TSS_TR130SET50", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", EndDate);
                DataSet ds5 = o.executeReader("ref_rpt_cur", "TB_SET50");
                s.Merge(ds5);

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        s.Tables[0].TableName = "TB_TR130BOTANNUALD";                        
                        report = new RPT_TR130BOTANNUALD();
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
                Log.Info("-----Model_RPT_TR130BOTANNUALD Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
