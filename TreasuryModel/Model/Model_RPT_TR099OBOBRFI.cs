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
    public class Model_RPT_TR099OBOBRFI
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR099OBOBRFI));
        protected Model_RPT_TR099OBOBRFI()
        {

        }

        public static DataTable GetBroker(string ConnectionString, string Party)
        {
            String sql = "SELECT TO_CHAR(REGEXP_SUBSTR('" + Party + "','[^,]+', 1, LEVEL)) as Broker FROM DUAL CONNECT BY REGEXP_SUBSTR('" + Party + "', '[^,]+', 1, LEVEL) IS NOT NULL";
            Log.Info("-----Broker Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("Broker");

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Broker Finish-----");
                o.closeConnection();
            }
        }

        public static ReportClass getReport(string ConnectionString, string Broker, string Month)
        {
            Log.Info("-----Model_RPT_TR099OBOBRFI Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099OBOBRFI");
            Log.Info("Data : From Broker=" + Broker + ", Month=" + Month);

            RPT_TR099OBOBRFI report = new RPT_TR099OBOBRFI();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099OBOBRFI", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_month", Month);
                s = o.executeReader("ref_rpt_cur", "TB_TR099OBOBRFI");

                o.createCommand(Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_month", Month);
                o.setInputParameter("p_product", "FI");
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR099SUMMARY");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR099SUMMARY");
                    dt.Columns.Add(new DataColumn("SUBTOTAL"));
                    dt.Columns.Add(new DataColumn("DISCOUNT"));
                    dt.Columns.Add(new DataColumn("DISCOUNTAMT"));
                    DataRow dr = dt.NewRow();
                    dr["SUBTOTAL"] = 0;
                    dr["DISCOUNT"] = 0;
                    dr["DISCOUNTAMT"] = 0;
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }

                if (s2.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = s2.Tables[0].NewRow();
                    dr["SUBTOTAL"] = 0;
                    dr["DISCOUNT"] = 0;
                    dr["DISCOUNTAMT"] = 0;
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
                Log.Info("-----Model_RPT_TR099OBOBRFI Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static ReportClass getReport(string ConnectionString, string Broker, string Product, string Month)
        {
            Log.Info("-----Model_RPT_TR099OBOBRFI Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099OBOBRFI");
            Log.Info("Data : From Broker=" + Broker + ", Month=" + Month);

            RPT_TR099OBOBRFI report = new RPT_TR099OBOBRFI();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099OBOBRFI", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_product", Product);
                o.setInputParameter("p_month", Month);
                s = o.executeReader("ref_rpt_cur", "TB_TR099OBOBRFI");

                o.createCommand(Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_month", Month);
                o.setInputParameter("p_product", Product);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR099SUMMARY");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR099SUMMARY");
                    dt.Columns.Add(new DataColumn("SUBTOTAL"));
                    dt.Columns.Add(new DataColumn("DISCOUNT"));
                    dt.Columns.Add(new DataColumn("DISCOUNTAMT"));
                    DataRow dr = dt.NewRow();
                    dr["SUBTOTAL"] = 0;
                    dr["DISCOUNT"] = 0;
                    dr["DISCOUNTAMT"] = 0;
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }

                if (s2.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = s2.Tables[0].NewRow();
                    dr["SUBTOTAL"] = 0;
                    dr["DISCOUNT"] = 0;
                    dr["DISCOUNTAMT"] = 0;
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
                Log.Info("-----Model_RPT_TR099OBOBRFI Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Broker, string Month)
        {
            Log.Info("-----Model_RPT_TR099OBOBRFI Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099OBOBRFI");
            Log.Info("Data : Broker=" + Broker + ", Month=" + Month);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR099OBOBRFI.Owner + ".KKB_SP_TSS_TR099OBOBRFI", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_month", Month);
                s = o.executeReader("ref_rpt_cur", "TB_TR099OBOBRFI");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR099OBOBRFI Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
