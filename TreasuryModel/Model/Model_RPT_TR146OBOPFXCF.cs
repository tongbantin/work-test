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
    public class Model_RPT_TR146OBOPFXCF
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR146OBOPFXCF));

        public static String getProduct(string ConnectionString, string DealNo)
        {
            String sql = "Select SWAPDEAL, SPOTFWDIND From OPICS.FXDH Where TRIM(DEALNO)='" + DealNo.Trim() + "' ";

            Log.Info("-----Model_RPT_TR146OBOPFXCF Start-----");
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
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        if (s.Tables[0].Rows[0]["SWAPDEAL"].ToString().Trim().Equals("0") && (s.Tables[0].Rows[0]["SPOTFWDIND"].ToString().Trim().Equals("S")))
                            return "SPOT";
                        else if (s.Tables[0].Rows[0]["SWAPDEAL"].ToString().Trim().Equals("0") && (s.Tables[0].Rows[0]["SPOTFWDIND"].ToString().Trim().Equals("F")))
                            return "FORWARD";
                        else if (!s.Tables[0].Rows[0]["SWAPDEAL"].ToString().Trim().Equals("0"))
                            return "SWAP";
                        else
                            return "";
                    }
                    else

                        return "";
                }
                else
                {
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
                Log.Info("-----Model_RPT_TR146OBOPFXCF Finish-----");
                o.closeConnection();
            }
        }

        public static ReportClass getReport(string ConnectionString, string Fund, string Product, string DealNo)
        {
            Log.Info("-----Model_RPT_TR146OBOPFXCF Start-----");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                string TableName = "";
                if (Product.Equals("SPOT"))
                {
                    TableName = "TB_TR146OBOPFXCF";
                    report = new RPT_TR146OBOPFXCF();
                }
                else if (Product.Equals("FORWARD"))
                {
                    TableName = "TB_TR146OBOPFXCF";
                    report = new RPT_TR146OBOPFXCF();
                }
                else if (Product.Equals("SWAP"))
                {
                    TableName = "TB_TR146OBOPFXSWCF";
                    report = new RPT_TR146OBOPFXSWCF();
                }
                o.openConnection();
                o.createCommand(Model_RPT_TR146OBOPFXCF.Owner + ".KKB_SP_TSS_TR146OBOPFXCF", CommandType.StoredProcedure);
                o.setInputParameter("p_fund", Fund);
                o.setInputParameter("p_product", Product);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", TableName);

                if (s != null)
                {
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
                Log.Info("-----Model_RPT_TR146OBOPFXCF Finish-----");
                o.closeConnection();
            }
            return report;
        }
              
        public static DataSet getPreConfirm(string ConnectionString, string WithoutParty, string Fund, string FromDealNo, string ToDealNo, string DealDate)
        {
            String sql = "SELECT FXDH.DEALNO ,FXDH.DEALNO || CASE WHEN FXDH.SWAPDEAL<>'0' THEN '/' || FXDH.SWAPDEAL ELSE '' END as SWAPDEAL, CUST.CMNE, CUST.SN, FXDEAL.SWAPPOINT, FXDEAL.TENOR, FXDEAL.PRINTDATE, FXDEAL.PRINTBY, FXDEAL.NEARPURPOSE || ':' || FXDEAL.NEARPURPOSEDESC as NEARPURPOSE, FXDEAL.FARPURPOSE || ':' || FXDEAL.FARPURPOSEDESC as FARPURPOSE, FXDEAL.TENORVAL, FXDEAL.REMARK ";
            sql += "FROM OPICS.FXDH LEFT JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO LEFT JOIN OPICINF.KKB_TB_FXDEAL FXDEAL ON FXDH.DEALNO=FXDEAL.DEALNO LEFT JOIN OPICS.BRPS ON BRPS.BR=FXDH.BR ";
            //sql += "WHERE FXDH.VERDATE IS NULL ";
            sql += "WHERE CUST.CTYPE<>'B' AND TRIM(CUST.CMNE) NOT IN (SELECT TO_CHAR(REGEXP_SUBSTR('" + WithoutParty + "','[^,]+', 1, LEVEL)) as Party FROM DUAL CONNECT BY REGEXP_SUBSTR('" + WithoutParty + "', '[^,]+', 1, LEVEL) IS NOT NULL) "; //AND FXDH.REVDATE IS NULL 
            sql += "AND ((FXDH.SWAPDEAL=0) OR (FXDH.SWAPDEAL IS NULL) OR ((FXDH.SWAPDEAL<>'0') AND (FXDH.FARNEARIND='N'))) ";
            if (!DealDate.Equals(""))
            {
                sql += "AND DEALDATE=TO_DATE('" + DealDate + "','DD/MM/YYYY') ";
                //sql += "AND (FXDH.DEALDATE=BRPS.BRANPRCDATE OR FXDH.INPUTDATE=BRPS.BRANPRCDATE) ";
            }
            if ((!FromDealNo.Trim().Equals("")) && (ToDealNo.Trim().Equals("")))
                sql += "AND FXDH.DEALNO=" + FromDealNo;
            else if ((!FromDealNo.Trim().Equals("")) && (!ToDealNo.Trim().Equals("")))
                sql += "AND FXDH.DEALNO BETWEEN " + FromDealNo + " AND " + ToDealNo;
            if (Fund.Trim().Equals("CORPORATE"))
                sql += "And CUST.CTYPE='C' ";
            else if (!Fund.Trim().Equals(""))
                sql += "And (CUST.CNO IN (SELECT GRPMEMBER FROM OPICS.CRGM WHERE TRIM(GRPID)='" + Fund + "')  OR CUST.CMNE='" + Fund + "') ";
            sql += "Order By FXDH.DEALNO DESC";

            Log.Info("-----Model_RPT_TR146OBOPFXCF Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR146OBOPFXCF Finish-----");
                o.closeConnection();
            }
        }

    }

}
