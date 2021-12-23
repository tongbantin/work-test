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
using System.Collections;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR140NTFXCF
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR140NTFXCF));

        public static String getProduct(string ConnectionString, string DealNo)
        {
                String sql = "Select SWAPDEAL, TENOR From OPICS.FXDH Where TRIM(DEALNO)='" + DealNo + "' ";

                Log.Info("-----Model_RPT_TR140NTFXCF Start-----");
                Log.Info("SQL Statement : " + sql);

                DataSet s = null;
                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    s = o.executeReader();
                    if (s.Tables.Count > 0)
                    {
                        if (s.Tables[0].Rows[0]["SWAPDEAL"].ToString().Trim().Equals("0") && (s.Tables[0].Rows[0]["TENOR"].ToString().Trim().Equals("S")))
                            return "SPOT";
                        else if (s.Tables[0].Rows[0]["SWAPDEAL"].ToString().Trim().Equals("0") && (s.Tables[0].Rows[0]["TENOR"].ToString().Trim().Equals("F")))
                            return "FORWARD";
                        else if (!s.Tables[0].Rows[0]["SWAPDEAL"].ToString().Trim().Equals("0"))
                            return "SWAP";
                        else
                            return "";
                    }
                    else {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("-----Model Error-----");
                    Log.Error(ex.Message);
                    return "";
                }
                finally
                {
                    Log.Info("-----Model_RPT_TR140NTFXCF Finish-----");
                    o.closeConnection();
                }
        }

        public static ReportClass getReport(string ConnectionString, string Product, string DealNo)
        {
            Log.Info("-----Model_RPT_TR140NTFXCF Start-----");

            DataSet s = null, s2 = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                string TableName = "";
                if (Product.Equals("SPOT"))
                {
                    TableName = "TB_TR092OBOFXCF";
                    report = new RPT_TR092OBOFXCF();
                }
                else if (Product.Equals("FORWARD"))
                {
                    TableName = "TB_TR093OBOFXCF";
                    report = new RPT_TR093OBOFXCF();
                }
                else if (Product.Equals("SWAP"))
                {
                    TableName = "TB_TR091OBOFXCF";
                    report = new RPT_TR091OBOFXCF();
                }
                o.openConnection();
                o.createCommand(Model_RPT_TR140NTFXCF.Owner + ".KKB_SP_TSS_TR140NTFXCF", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                o.setInputParameter("p_product", Product);
                s = o.executeReader("ref_rpt_cur", TableName);
                
                o.createCommand(Model_RPT_TR140NTFXCF.Owner + ".KKB_SP_TSS_TR140NTFXCF", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                o.setInputParameter("p_product", "NETSETTLE");
                s2 = o.executeReader("ref_rpt_cur", "TB_NETSETTLE");
                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_NETSETTLE");
                    dt.Columns.Add(new DataColumn("KKTEXT"));
                    dt.Columns.Add(new DataColumn("DEALNO"));
                    dt.Columns.Add(new DataColumn("RECEIVETXT"));
                    dt.Columns.Add(new DataColumn("PAYTXT"));
                    dt.Columns.Add(new DataColumn("VDATE"));
                    dt.Columns.Add(new DataColumn("FARNEARIND"));
                    DataRow dr = dt.NewRow();
                    dr["KKTEXT"] = "";
                    dr["DEALNO"] = "";
                    dr["RECEIVETXT"] = "";
                    dr["PAYTXT"] = "";
                    dr["FARNEARIND"] = "";
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
                Log.Info("-----Model_RPT_TR140NTFXCF Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }

}
