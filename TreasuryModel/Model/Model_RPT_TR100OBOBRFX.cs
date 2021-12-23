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
    public class Model_RPT_TR100OBOBRFX
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR100OBOBRFX));
        public Model_RPT_TR100OBOBRFX()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Broker, string Month)
        {
            Log.Info("-----Model_RPT_TR100OBOBRFX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR100OBOBRFX.Owner + ".KKB_SP_TSS_TR100OBOBRFX");
            Log.Info("Data : Broker=" + Broker + ", Month=" + Month);

            RPT_TR100OBOBRFX report = new RPT_TR100OBOBRFX();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR100OBOBRFX.Owner + ".KKB_SP_TSS_TR100OBOBRFX", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_month", Month);
                s = o.executeReader("ref_rpt_cur", "TB_TR100OBOBRFX");

                o.createCommand(Model_RPT_TR100OBOBRFX.Owner + ".KKB_SP_TSS_TR100SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_month", Month);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR100SUMMARY");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR100SUMMARY");
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
                Log.Info("-----Model_RPT_TR100OBOBRFX Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static ReportClass getReport(string ConnectionString, string Broker, string ProdType, string Month)
        {
            Log.Info("-----Model_RPT_TR100OBOBRFX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR100OBOBRFX.Owner + ".KKB_SP_TSS_TR100OBOBRFX");
            Log.Info("Data : Broker=" + Broker + ", ProdType=" + ProdType + ", Month=" + Month);

            RPT_TR100OBOBRFX report = new RPT_TR100OBOBRFX();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR100OBOBRFX.Owner + ".KKB_SP_TSS_TR100OBOBRFX", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_prodtype", ProdType);
                o.setInputParameter("p_month", Month);
                s = o.executeReader("ref_rpt_cur", "TB_TR100OBOBRFX");

                o.createCommand(Model_RPT_TR100OBOBRFX.Owner + ".KKB_SP_TSS_TR100SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_broker", Broker);
                o.setInputParameter("p_prodtype", ProdType);
                o.setInputParameter("p_month", Month);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR100SUMMARY");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR100SUMMARY");
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
                Log.Info("-----Model_RPT_TR100OBOBRFX Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
