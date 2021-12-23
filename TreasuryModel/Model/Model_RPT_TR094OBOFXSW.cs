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
    public class Model_RPT_TR094OBOFXSW
    {   
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR094OBOFXSW));

        protected Model_RPT_TR094OBOFXSW() { }

        //Version 2018-02-07
        public static ReportClass getReport(string ConnectionString, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR094FXPAY Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAY");
            Log.Info("Data : Execute Date=" + ExecuteDate);

            RPT_TR094OBOFXSW report = new RPT_TR094OBOFXSW();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAY", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_OBOFXSW");

                //o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094SUMMARY", CommandType.StoredProcedure);
                //o.setInputParameter("p_execute_date", ExecuteDate);
                //DataSet s2 = o.executeReader("ref_rpt_cur", "TB_NETSETTLE");
                //if (s2 == null)
                //{
                //    s2 = new DataSet();
                //    DataTable dt = new DataTable("TB_NETSETTLE");
                //    dt.Columns.Add(new DataColumn("KKTEXT"));
                //    dt.Columns.Add(new DataColumn("DEALNO"));
                //    dt.Columns.Add(new DataColumn("RECEIVETEXT"));
                //    dt.Columns.Add(new DataColumn("PAYTEXT"));
                //    dt.Columns.Add(new DataColumn("VDATE"));
                //    dt.Columns.Add(new DataColumn("FARNEARIND"));
                //    DataRow dr = dt.NewRow();
                //    dr["KKTEXT"] = "";
                //    dr["DEALNO"] = "";
                //    dr["RECEIVETEXT"] = "";
                //    dr["PAYTEXT"] = "";
                //    dr["FARNEARIND"] = "";
                //    dt.Rows.Add(dr);
                //    s2.Tables.Add(dt);
                //}

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
                Log.Info("-----Model_RPT_TR094FXPAY Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static ReportClass getReport(string ConnectionString, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR094FXPAY Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAY");
            Log.Info("Data : From Date=" + FromDate + "-" + ToDate);

            RPT_TR094FXPAY report = new RPT_TR094FXPAY();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAY", CommandType.StoredProcedure);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR094FXPAY");

                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAYSUM", CommandType.StoredProcedure);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR094FXPAYSUM");

                if (s != null)
                {
                    if (s.Tables != null)
                    {
                        if (s.Tables.Count > 0)
                        {
                            if (s2 == null)
                            {
                                s2 = new DataSet();
                                DataTable dt = new DataTable("TB_TR094FXPAYSUM");
                                dt.Columns.Add(new DataColumn("CMNE"));
                                dt.Columns.Add(new DataColumn("VDATE"));
                                dt.Columns.Add(new DataColumn("CCY"));
                                dt.Columns.Add(new DataColumn("AMOUNT"));
                                DataRow dr = dt.NewRow();
                                dr["CMNE"] = "";
                                dr["VDATE"] = "01/01/2011";
                                dr["CCY"] = "THB";
                                dr["AMOUNT"] = 0;
                                dt.Rows.Add(dr);
                                s2.Tables.Add(dt);
                            }

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
                        {
                            report = null;
                        }
                    }
                    else
                    {
                        report = null;
                    }
                }
                else
                    report = null;
                return report;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094FXPAY Finish-----");
                o.closeConnection();
            }
        }
        
        public static ReportClass getReport(string ConnectionString, string CMNE, string FromDate, string ToDate, Boolean Netting)
        {
            Log.Info("-----Model_RPT_TR094FXPAY Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAY");
            Log.Info("Data : From Date=" + FromDate + "-" + ToDate);

            Oracle o = Oracle.getInstance(ConnectionString);
            string ProcedureDetail = Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAY";
            string ProcedureSum = Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXPAYSUM";
            ReportClass report = new RPT_TR094FXPAY();
            string TBName = "TB_TR094FXPAY";
            string TBSumName = "TB_TR094FXPAYSUM";
            
            if (Netting)
            {
                ProcedureDetail = Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXNET";
                ProcedureSum = Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094FXNETSUM";
                report = new RPT_TR094FXNET();
                TBName = "TB_TR094FXNET";
                TBSumName = "TB_TR094FXNETSUM";
            }
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(ProcedureDetail, CommandType.StoredProcedure);
                o.setInputParameter("p_cmne", CMNE);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                s = o.executeReader("ref_rpt_cur", TBName);

                o.createCommand(ProcedureSum, CommandType.StoredProcedure);
                o.setInputParameter("p_cmne", CMNE);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                DataSet s2 = o.executeReader("ref_rpt_cur", TBSumName);
               
                if (s != null)
                {
                    if (s.Tables != null)
                    {
                        if (s.Tables.Count > 0)
                        {
                            if (s2 == null)
                            {
                                s2 = new DataSet();
                                DataTable dt = new DataTable(TBSumName);
                                dt.Columns.Add(new DataColumn("CMNE"));
                                dt.Columns.Add(new DataColumn("VDATE"));
                                dt.Columns.Add(new DataColumn("CCY"));
                                dt.Columns.Add(new DataColumn("AMOUNT"));
                                DataRow dr = dt.NewRow();
                                dr["CMNE"] = "";
                                dr["VDATE"] = "01/01/2011";
                                dr["CCY"] = "THB";
                                dr["AMOUNT"] = 0;
                                dt.Rows.Add(dr);
                                s2.Tables.Add(dt);
                            }

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
                        {
                            report = null;
                        }
                    }
                    else
                    {
                        report = null;
                    }
                }
                else
                    report = null;
                return report;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094FXPAY Finish-----");
                o.closeConnection();
            }
        }

        public static ReportClass getReport(string ConnectionString, string ExecuteDate, string Party, string FromAmt, string ToAmt)
        {
            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094OBOFXSW");
            Log.Info("Data : Execute Date=" + ExecuteDate);
            Log.Info("Data : Party=" + Party);
            Log.Info("Data : From Amount=" + FromAmt);
            Log.Info("Data : To Amount=" + ToAmt);

            RPT_TR094OBOFXSW report = new RPT_TR094OBOFXSW();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094OBOFXSW", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_party", Party);
                o.setInputParameter("p_fromamt", FromAmt);
                o.setInputParameter("p_toamt", ToAmt);
                s = o.executeReader("ref_rpt_cur", "TB_TR094OBOFXSW");
                
                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_party", Party);
                o.setInputParameter("p_fromamt", FromAmt);
                o.setInputParameter("p_toamt", ToAmt);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR094SUMMARY");

                if (s!=null)
                {
                    if (s.Tables != null)
                    {
                        if (s.Tables.Count > 0)
                        {
                            if (s2 == null)
                            {
                                s2 = new DataSet();
                                DataTable dt = new DataTable("TB_TR094SUMMARY");
                                dt.Columns.Add(new DataColumn("CMNE"));
                                dt.Columns.Add(new DataColumn("VDATE"));
                                dt.Columns.Add(new DataColumn("CCY"));
                                dt.Columns.Add(new DataColumn("AMOUNT"));
                                DataRow dr = dt.NewRow();
                                dr["CMNE"] = "";
                                dr["VDATE"] = "01/01/2011";
                                dr["CCY"] = "THB";
                                dr["AMOUNT"] = 0;
                                dt.Rows.Add(dr);
                                s2.Tables.Add(dt);
                            }

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
                        else {
                            report = null;
                        }
                    }
                    else {
                        report = null;
                    }
                }
                else
                    report = null;
            return report;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
        }

        public static DataSet getDataSet(string ConnectionString, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094OBOFXSW");
            Log.Info("Data : Execute Date=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094OBOFXSW", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR094OBOFXSW");

                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR094SUMMARY");

                if (s2 == null)
                {
                    DataTable dt = new DataTable("TB_TR094SUMMARY");
                    dt.Columns.Add(new DataColumn("CMNE"));
                    dt.Columns.Add(new DataColumn("VDATE"));
                    dt.Columns.Add(new DataColumn("CCY"));
                    dt.Columns.Add(new DataColumn("AMOUNT"));
                    DataRow dr = dt.NewRow();
                    dr["CMNE"] = "";
                    dr["VDATE"] = "01/01/2011";
                    dr["CCY"] = "THB";
                    dr["AMOUNT"] = "0";
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }
                if (s2 != null)
                    s.Merge(s2.Tables[0]);
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataSet getDataSet(string ConnectionString, string ExecuteDate, string Party)
        {
            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094OBOFXSW");
            Log.Info("Data : Execute Date=" + ExecuteDate);
            Log.Info("Data : Party=" + Party);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094OBOFXSW", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_party", Party);
                s = o.executeReader("ref_rpt_cur", "TB_TR094OBOFXSW");

                o.createCommand(Model_RPT_TR094OBOFXSW.Owner + ".KKB_SP_TSS_TR094SUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_party", Party);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR094SUMMARY");

                if (s2 == null)
                {
                    DataTable dt = new DataTable("TB_TR094SUMMARY");
                    dt.Columns.Add(new DataColumn("CMNE"));
                    dt.Columns.Add(new DataColumn("VDATE"));
                    dt.Columns.Add(new DataColumn("CCY"));
                    dt.Columns.Add(new DataColumn("AMOUNT"));
                    DataRow dr = dt.NewRow();
                    dr["CMNE"] = "";
                    dr["VDATE"] = "01/01/2011";
                    dr["CCY"] = "THB";
                    dr["AMOUNT"] = "0";
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }
                if (s2!=null)
                    s.Merge(s2.Tables[0]);
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
            return s;
        }
                
        public static DataSet getFXDeal(string ConnectionString, string CMNE, string FromDate, string ToDate, string CCSPROD)
        {
            String sql = "SELECT CUST.CMNE, BR, PRODTYPE, PRODCODE ,COST ,FXDH.DEALNO ,TO_CHAR(DEALDATE,'DD/MM/YYYY') DEALDATE ,CUST.CMNE as CUSTNAME ,'NET' as CTYPE ,PS ,CCY ,CCYAMT ,CTRCCY ,CTRAMT ,CCYRATE_8 ,TO_CHAR(VDATE,'DD/MM/YYYY') as VDATE ,SWAPDEAL ,CASE WHEN TRIM(KKB_TB_FXFLAG.GROSSNET)='G' THEN 'G' ELSE '' END as GROSSNET, NETDEALNO, NETTINGNO ";
            sql += " FROM OPICS.FXDH LEFT OUTER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO LEFT OUTER JOIN OPICINF.KKB_TB_FXFLAG ON TRIM(FXDH.DEALNO)=TRIM(KKB_TB_FXFLAG.DEALNO) AND KKB_TB_FXFLAG.GROSSNET='G' ";
            sql += " WHERE REVDATE IS NULL AND VDATE BETWEEN TO_DATE('" + FromDate + "', 'DD/MM/YYYY') AND TO_DATE('" + ToDate + "','DD/MM/YYYY') AND PRODCODE<>'FXO' AND TRIM(CMNE) NOT IN ('KKLEASING','KKEXPENSE','INTERDEAL') ";
            if (!CMNE.Trim().Equals(""))
                sql += "AND CMNE='" + CMNE + "' ";
            sql += "UNION ALL ";
            sql += "SELECT CUST.CMNE ,SWDH.BR ,SWDH.PRODTYPE ,SWDH.PRODUCT as PRODCODE ,SWDH.COST ,SWDH.DEALNO ,TO_CHAR(SWDH.DEALDATE,'DD/MM/YYYY') as DEALDATE ,CUST.CMNE as CUSTNAME ";
            sql += "    ,'NET' as CTYPE ,NULL as PS ,FIRSTLEG.NOTCCY as CCY ,FIRSTLEG.NOTCCYAMT as CCYAMT ,SECONDLEG.NOTCCY as CTRCCY ,SECONDLEG.NOTCCYAMT as CTRAMT ,NULL as CCYRATE_8 ";
            sql += "    ,TO_CHAR(SWDH.MATDATE,'DD/MM/YYYY') as VDATE ,NULL as SWAPDEAL,CASE WHEN TRIM(KKB_TB_FXFLAG.GROSSNET)='G' THEN 'G' ELSE '' END as GROSSNET , NETDEALNO, NETTINGNO  ";
            sql += "FROM OPICS.SWDH INNER JOIN OPICS.CUST ON SWDH.CNO=CUST.CNO  ";
            sql += "    INNER JOIN (SELECT SUBSTR(TO_CHAR(REGEXP_SUBSTR('" + CCSPROD + "','[^,]+', 1, LEVEL)),0,INSTR(TO_CHAR(REGEXP_SUBSTR('" + CCSPROD + "','[^,]+', 1, LEVEL)),'=',1,1)-1) as PRODUCT ";
            sql += "        ,SUBSTR(TO_CHAR(REGEXP_SUBSTR('" + CCSPROD + "','[^,]+', 1, LEVEL)),INSTR(TO_CHAR(REGEXP_SUBSTR('" + CCSPROD + "','[^,]+', 1, LEVEL)),'=',1,1)+1) as PRODTYPE ";
            sql += "        FROM DUAL CONNECT BY REGEXP_SUBSTR('" + CCSPROD + "', '[^,]+', 1, LEVEL) IS NOT NULL) ";
            sql += "    PROD ON TRIM(SWDH.PRODUCT)=TRIM(PROD.PRODUCT) AND TRIM(SWDH.PRODTYPE)=TRIM(PROD.PRODTYPE) ";
            sql += "    LEFT OUTER JOIN OPICS.SWDT FIRSTLEG ON SWDH.DEALNO=FIRSTLEG.DEALNO AND FIRSTLEG.SEQ='001' ";
            sql += "    LEFT OUTER JOIN OPICS.SWDT SECONDLEG ON SWDH.DEALNO=SECONDLEG.DEALNO AND SECONDLEG.SEQ='002' ";
            sql += "    LEFT OUTER JOIN OPICINF.KKB_TB_FXFLAG ON TRIM(SWDH.DEALNO)=TRIM(KKB_TB_FXFLAG.DEALNO) ";
            sql += "WHERE (NVL(SWDH.ETINPUTDATE,SWDH.MATDATE) BETWEEN TO_DATE('" + FromDate + "','DD/MM/YYYY') AND TO_DATE('" + ToDate + "','DD/MM/YYYY')) ";
            sql += "    AND SWDH.REVDATE IS NULL ";
            if (!CMNE.Trim().Equals(""))
                sql += "AND CMNE='" + CMNE + "' ";

            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
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
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
        }

        public static List<string> getDealNet(string ConnectionString)
        {
            string sql = "SELECT TO_CHAR(REGEXP_SUBSTR((SELECT WM_CONCAT(TRIM(NETDEALNO)) FROM OPICINF.KKB_TB_FXFLAG WHERE NETDEALNO IS NOT NULL),'[^,]+', 1, LEVEL)) as DEALNO "; 
            sql += "FROM DUAL CONNECT BY REGEXP_SUBSTR(((SELECT WM_CONCAT(TRIM(NETDEALNO)) FROM OPICINF.KKB_TB_FXFLAG WHERE NETDEALNO IS NOT NULL)), '[^,]+', 1, LEVEL) IS NOT NULL";
            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable tb = o.executeReaderToDT("FXNET");
                List<string> ls = (from rw in tb.AsEnumerable()
                          select Convert.ToString(rw["DEALNO"])
                          ).ToList<string>();
                return ls;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
        }

        public static object getFXNettingDeal(string ConnectionString, string NettingDeal, string CMNE, string FromDate, string ToDate)
        {
            String sql = "SELECT FXDH.DEALNO ,CASE WHEN FXFLAG.DEALNO IS NOT NULL AND TRIM(FXFLAG.DEALNO)<>'' THEN 'Y' ELSE 'N' END NETDEAL  , CASE WHEN FXNFLAG.DEALNO IS NOT NULL AND TRIM(FXNFLAG.DEALNO)<>'' THEN 'Y' ELSE 'N' END NETNOTDEAL ";
            sql += "FROM OPICS.FXDH LEFT OUTER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO   ";
            sql += "LEFT OUTER JOIN ( ";
            sql += "SELECT TO_CHAR(REGEXP_SUBSTR((SELECT NETDEALNO FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)='" + NettingDeal.Trim() + "'),'[^,]+', 1, LEVEL)) as DEALNO  ";
            sql += "FROM DUAL CONNECT BY REGEXP_SUBSTR(((SELECT NETDEALNO FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)='" + NettingDeal.Trim() + "')), '[^,]+', 1, LEVEL) IS NOT NULL  ";
            sql += ") FXFLAG ON TRIM(FXDH.DEALNO)=TRIM(FXFLAG.DEALNO)  ";
            sql += "LEFT OUTER JOIN (  ";
            sql += "SELECT TO_CHAR(REGEXP_SUBSTR((SELECT WM_CONCAT(NETDEALNO || ',' || DEALNO) FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)<>'" + NettingDeal.Trim() + "'),'[^,]+', 1, LEVEL)) as DEALNO  ";
            sql += "FROM DUAL CONNECT BY REGEXP_SUBSTR(((SELECT WM_CONCAT(NETDEALNO || ',' || DEALNO) FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)<>'" + NettingDeal.Trim() + "')), '[^,]+', 1, LEVEL) IS NOT NULL  ";
            sql += ") FXNFLAG ON TRIM(FXDH.DEALNO)=TRIM(FXNFLAG.DEALNO) AND TRIM(FXNFLAG.DEALNO)<>'" + NettingDeal.Trim() + "' ";
            sql += "LEFT OUTER JOIN OPICS.FXDH NETDEAL ON NETDEAL.DEALNO='" + NettingDeal.Trim() + "' ";
            sql += "WHERE FXDH.REVDATE IS NULL AND FXDH.VDATE BETWEEN TO_DATE('" + FromDate + "', 'DD/MM/YYYY') AND TO_DATE('" + ToDate + "','DD/MM/YYYY')  ";
            sql += "AND FXDH.PRODCODE<>'FXO' AND TRIM(CUST.CMNE) NOT IN ('KKLEASING','KKEXPENSE','INTERDEAL') AND TRIM(CUST.CMNE)='" + CMNE.Trim() + "' ";
            sql += "UNION ALL ";
            sql += "SELECT SWDH.DEALNO ,CASE WHEN FXFLAG.DEALNO IS NOT NULL AND TRIM(FXFLAG.DEALNO)<>'' THEN 'Y' ELSE 'N' END NETDEAL  , CASE WHEN FXNFLAG.DEALNO IS NOT NULL AND TRIM(FXNFLAG.DEALNO)<>'' THEN 'Y' ELSE 'N' END NETNOTDEAL ";
            sql += "FROM OPICS.SWDH LEFT OUTER JOIN OPICS.CUST ON SWDH.CNO=CUST.CNO ";
            sql += "LEFT OUTER JOIN ( ";
            sql += "SELECT TO_CHAR(REGEXP_SUBSTR((SELECT NETDEALNO FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)='" + NettingDeal.Trim() + "'),'[^,]+', 1, LEVEL)) as DEALNO  ";
            sql += "FROM DUAL CONNECT BY REGEXP_SUBSTR(((SELECT NETDEALNO FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)='" + NettingDeal.Trim() + "')), '[^,]+', 1, LEVEL) IS NOT NULL  ";
            sql += ") FXFLAG ON TRIM(SWDH.DEALNO)=TRIM(FXFLAG.DEALNO)  ";
            sql += "LEFT OUTER JOIN (  ";
            sql += "SELECT TO_CHAR(REGEXP_SUBSTR((SELECT WM_CONCAT(NETDEALNO || ',' || DEALNO) FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)<>'" + NettingDeal.Trim() + "'),'[^,]+', 1, LEVEL)) as DEALNO  ";
            sql += "FROM DUAL CONNECT BY REGEXP_SUBSTR(((SELECT WM_CONCAT(NETDEALNO || ',' || DEALNO) FROM OPICINF.KKB_TB_FXFLAG WHERE TRIM(DEALNO)<>'" + NettingDeal.Trim() + "')), '[^,]+', 1, LEVEL) IS NOT NULL  ";
            sql += ") FXNFLAG ON TRIM(SWDH.DEALNO)=TRIM(FXNFLAG.DEALNO) AND TRIM(FXNFLAG.DEALNO)<>'" + NettingDeal.Trim() + "' ";
            sql += "LEFT OUTER JOIN OPICS.SWDH NETDEAL ON NETDEAL.DEALNO='" + NettingDeal.Trim() + "' ";
            sql += "WHERE SWDH.REVDATE IS NULL AND SWDH.MATDATE BETWEEN TO_DATE('" + FromDate + "', 'DD/MM/YYYY') AND TO_DATE('" + ToDate + "','DD/MM/YYYY')  ";
            sql += "AND TRIM(CUST.CMNE) NOT IN ('KKLEASING','KKEXPENSE','INTERDEAL') AND TRIM(CUST.CMNE)='" + CMNE.Trim() + "' ";            

            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable tb = o.executeReaderToDT("NettingDealNo");
                var ls = (from rw in tb.AsEnumerable()
                          select new 
                          {
                              DEALNO = Convert.ToString(rw["DEALNO"]),
                              NETDEAL = Convert.ToString(rw["NETDEAL"]),
                              NETNOTDEAL = Convert.ToString(rw["NETNOTDEAL"])
                          }).ToList();
                return new { Result = "OK", Records = ls };
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
        }
        
        public static object UpdateGross(string ConnectionString, string DealNo, string Gross, bool IsExists)
        {

            Log.Info("-----KKB_TB_FXDEAL Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "";
                if (IsExists)
                    sql = "Update OPICINF.KKB_TB_FXFLAG SET GROSSNET='" + Gross.Trim() + "' WHERE Trim(DEALNO)='" + DealNo.Trim() + "'";
                else
                    sql = "Insert Into OPICINF.KKB_TB_FXFLAG(DEALNO, GROSSNET) VALUES('" + DealNo.Trim() + "','" + Gross.Trim() + "')";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could Not Update Data" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_FXDEAL Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdatePostNet(string ConnectionString, string NettingDeal, string CMNE, string DealNo, bool IsExists)
        {

            Log.Info("-----KKB_TB_FXDEAL Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "";
                DealNo = DealNo.Trim().Replace(' ', ',');
                if (DealNo.Trim().Equals(""))
                {
                    sql = "UPDATE OPICINF.KKB_TB_FXFLAG SET NETDEALNO=NULL, NETTINGNO=NULL, UPDATEBY='', UPDATEDATE=SYSDATE Where TRIM(DEALNO)='" + NettingDeal.Trim() + "'";
                } else
                {
                    if (IsExists)
                        sql = "Update OPICINF.KKB_TB_FXFLAG SET NETDEALNO='" + DealNo.Trim() + "', NETTINGNO='NTT" + NettingDeal.Trim() + "', UPDATEBY='', UPDATEDATE=SYSDATE WHERE Trim(DEALNO)='" + NettingDeal.Trim() + "'";
                    else
                        sql = "Insert Into OPICINF.KKB_TB_FXFLAG(DEALNO, NETDEALNO, NETTINGNO, CREATEBY, CREATEDATE) VALUES('" + NettingDeal.Trim() + "','" + DealNo.Trim() + "','NTT" + NettingDeal.Trim() + "','',SYSDATE)";
                }
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could Not Netting Deal" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_FXDEAL Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }

        public static bool IsExisting(string ConnectionString, string DealNo)
        {
            String sql = "Select count(*) From OPICINF.KKB_TB_FXFLAG Where TRIM(DEALNO)=TRIM('" + DealNo + "') ";

            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                int rows = int.Parse(o.executeScalar().ToString());
                if (rows <=0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }

        public static DataSet GetFXDealByCounterParty(string ConnectionString, string DealNo, string CMNE)
        {
            String sql = "SELECT DEALNO FROM OPICS.FXDH WHERE CUST IN (SELECT CNO FROM OPICS.CUST WHERE CMNE='" + CMNE + "') ";

            Log.Info("-----Model_RPT_TR094OBOFXSW Start-----");
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
                Log.Info("-----Model_RPT_TR094OBOFXSW Finish-----");
                o.closeConnection();
            }
        }
    }
}
