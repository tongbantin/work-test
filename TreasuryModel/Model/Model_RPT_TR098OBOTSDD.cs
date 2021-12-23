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
    public class Model_RPT_TR098OBOTSDD
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR098OBOTSDD));
        public Model_RPT_TR098OBOTSDD()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Month, string Expense)
        {
            Log.Info("-----Model_RPT_TR098OBOTSDD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR098OBOTSDD.Owner + ".KKB_SP_TSS_TR098OBOTSDD");
            Log.Info("Data : From Month=" + Month + ", Expense=" + Expense);

            RPT_TR098OBOTSDD report = new RPT_TR098OBOTSDD();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR098OBOTSDD.Owner + ".KKB_SP_TSS_TR098OBOTSDD", CommandType.StoredProcedure);
                o.setInputParameter("p_month", Month);
                o.setInputParameter("p_expense", Expense);
                s = o.executeReader("ref_rpt_cur", "TB_TR098OBOTSDD");

                o.createCommand(Model_RPT_TR098OBOTSDD.Owner + ".KKB_SP_TSS_TR098EXPENSE", CommandType.StoredProcedure);
                o.setInputParameter("p_month", Month);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR098EXPENSE");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR098EXPENSE");
                    dt.Columns.Add(new DataColumn("COST"));
                    dt.Columns.Add(new DataColumn("EXPENSE"));
                    dt.Columns.Add(new DataColumn("TOTAL"));
                    DataRow dr = dt.NewRow();
                    dr["COST"] = "340";
                    dr["EXPENSE"] = 0;
                    dr["TOTAL"] = 0;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["COST"] = "800";
                    dr["EXPENSE"] = 0;
                    dr["TOTAL"] = 0;
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }

                if (s2.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = s2.Tables[0].NewRow();
                    dr["COST"] = "340";
                    dr["EXPENSE"] = 0;
                    dr["TOTAL"] = 0;
                    s2.Tables[0].Rows.Add(dr);
                    dr = s2.Tables[0].NewRow();
                    dr["COST"] = "800";
                    dr["EXPENSE"] = 0;
                    dr["TOTAL"] = 0;
                    s2.Tables[0].Rows.Add(dr);
                }

                if (s != null)
                {
                    if (s2 != null)
                        s.Merge(s2.Tables[0]);
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
                Log.Info("-----Model_RPT_TR098OBOTSDD Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Month, string Expense)
        {
            Log.Info("-----Model_RPT_TR098OBOTSDD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR098OBOTSDD.Owner + ".KKB_SP_TSS_TR098OBOTSDD");
            Log.Info("Data : From Month=" + Month + ",Expense=" + Expense);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR098OBOTSDD.Owner + ".KKB_SP_TSS_TR098OBOTSDD", CommandType.StoredProcedure);
                o.setInputParameter("p_month", Month);
                o.setInputParameter("p_expense", Expense);
                s = o.executeReader("ref_rpt_cur", "TB_TR098OBOTSDD");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR098OBOTSDD Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
