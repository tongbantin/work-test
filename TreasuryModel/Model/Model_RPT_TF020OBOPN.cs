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
    public class Model_RPT_TF020OBOPN
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TF020OBOPN));

        public Model_RPT_TF020OBOPN()
        {

        }

        public static ReportClass getReport(string ConnectionString, string Branch, string DealNo, string RefNo)
        {
            Log.Info("-----Model_RPT_TF020OBOPN Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TF020OBOPN.Owner + ".KKB_SP_TSS_TF020OBOPN");
            Log.Info("Data : Branch=" + Branch + ", Deal No=" + DealNo);

            RPT_TF020OBOPN report = new RPT_TF020OBOPN();            
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TF020OBOPN.Owner + ".KKB_SP_TSS_TF020OBOPN", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_deal_no", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TF020OBOPN");

                //String sql = @"SELECT UNISTR(FULL_CUST_NAME_TH) as CounterParty from KKB_TRO_VW_Customer@TOFCR C where C.CUST_CODE = '" + s.Tables[0].Rows[0]["CNO"].ToString().Trim() + "'";
                //Log.Info("Execute SQL : " + sql);
                //Log.Info("Data : Branch=" + Branch + ", Deal No=" + DealNo);
                //o.createCommand(sql, CommandType.Text);
                //DataTable cs = o.executeReaderToDT("TB_TF020OBOPN");
                if (s != null)
                {
                    //if (cs != null)
                    //{
                        if (s.Tables[0].Rows.Count > 0)
                        {
                            //s.Tables[0].Rows[0]["CounterParty"] = cs.Rows[0]["CounterParty"];

                            report.SetDataSource(s);
                            Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                            Log.Info(s.GetXml());
                        }
                        else
                        {
                            report = null;
                        }
                    //}
                    //else {
                    //    report = null;
                    //    Log.Error("-----Model Error-----");
                    //    Log.Error("Process Connect to DBlink Error");
                    //}
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
                Log.Info("-----Model_RPT_TF020OBOPN Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
