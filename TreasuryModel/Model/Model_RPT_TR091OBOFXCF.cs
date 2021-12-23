using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR091OBOFXCF
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR091OBOFXCF));

        protected Model_RPT_TR091OBOFXCF()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR091OBOFXCF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR091OBOFXCF.Owner + ".KKB_SP_TSS_TR091OBOFXCF");
            Log.Info("Data : DealNo=" + DealNo);

            RPT_TR091OBOFXCF report = new RPT_TR091OBOFXCF();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null, s2 = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR091OBOFXCF.Owner + ".KKB_SP_TSS_TR091OBOFXCF", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR091OBOFXCF");

                //s2 = new DataSet();
                //DataTable dt = new DataTable("TB_NETSETTLE");
                //dt.Columns.Add(new DataColumn("KKTEXT"));
                //dt.Columns.Add(new DataColumn("DEALNO"));
                //dt.Columns.Add(new DataColumn("RECEIVETXT"));
                //dt.Columns.Add(new DataColumn("PAYTXT"));
                //dt.Columns.Add(new DataColumn("VDATE"));
                //dt.Columns.Add(new DataColumn("FARNEARIND"));
                //DataRow dr = dt.NewRow();
                //dr["KKTEXT"] = "";
                //dr["DEALNO"] = "";
                //dr["RECEIVETXT"] = "";
                //dr["PAYTXT"] = "";
                //dr["FARNEARIND"] = "";
                //dt.Rows.Add(dr);
                //s2.Tables.Add(dt);


                if (s != null)
                {
                    //if (s2 != null)
                    //    s.Merge(s2.Tables[0]);
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
                Log.Info("-----Model_RPT_TR091OBOFXCF Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR091OBOFXCF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR091OBOFXCF.Owner + ".KKB_SP_TSS_TR091OBOFXCF");
            Log.Info("Data : DealNo=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR091OBOFXCF.Owner + ".KKB_SP_TSS_TR091OBOFXCF", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR091OBOFXCF");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR091OBOFXCF Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataSet checkCustNoReport(string ConnectionString, string dealno, string custnoshow)
        {
            Log.Info("-----Model_RPT_TR091OBOFXCF-----");
            Log.Info("checkCustNoReport : Start");

            String sql = "";
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                sql += "SELECT DEALNO FROM OPICS.FXDH INNER JOIN OPICS.CUST ON FXDH.CUST = CUST.CNO WHERE CUST.CMNE IN (" + custnoshow + ") AND TRIM(DEALNO) = " + dealno;
               
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----getEmailByProdtype Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_WEBPARAM Finish-----");
                o.closeConnection();
            }

            if (s.Tables[0].Rows.Count > 0)
                return s;
            else
                return null;
                
        }
    }
}
