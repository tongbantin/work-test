using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR133BODEBTOUT
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR133BODEBTOUT));
        public Model_RPT_TR133BODEBTOUT()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Report, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR133BODEBTOUT Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR133BODEBTOUT.Owner + ".KKB_SP_TSS_TR133BODEBTOUT");
            Log.Info("Data : Report=" + Report);

            RPT_TR133BODEBTOUT report = new RPT_TR133BODEBTOUT();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR133BODEBTOUT.Owner + ".KKB_SP_TSS_TR133BODEBTOUT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR133BODEBTOUT");

                o.createCommand(Model_RPT_TR133BODEBTOUT.Owner + ".KKB_SP_TSS_TR133BODEBTSUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_execute_date", ExecuteDate);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR133BODEBTSUMMARY");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR133BODEBTSUMMARY");
                    dt.Columns.Add(new DataColumn("REPORTTYPE"));
                    dt.Columns.Add(new DataColumn("SETTAMT"));
                    DataRow dr = dt.NewRow();
                    dr["REPORTTYPE"] = "";
                    dr["SETTAMT"] = 0;
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }

                if (s != null)
                {
                    if (s2 != null)
                        s.Merge(s2.Tables[0]);
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report.SetDataSource(s);
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
                Log.Info("-----Model_RPT_TR133BODEBTOUT Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
