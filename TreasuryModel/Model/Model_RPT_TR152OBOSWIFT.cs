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
using TreasuryModel.Data;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR152OBOSWIFT
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR152OBOSWIFT));

        public static ReportClass getReport(string ConnectionString, string CTPY, string IncomingDate)
        {
            Log.Info("-----Model_RPT_TR152OBOSWIFT Start-----");

            //swift300Matching(ConnectionString, CTPY, FromDate); //run matching

            DataSet s = new DataSet();
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                report = new RPT_TR152OBOSWIFT();

                DataTable dtSWIFTMatched = getSWIFTMatch(ConnectionString, CTPY, IncomingDate);

                DataTable dtSWIFTNotMatch = getSWIFTNotMatch(ConnectionString, CTPY, IncomingDate);

                DataTable dtOpicsNotMatch = getOpicsNotMatch(ConnectionString, CTPY, IncomingDate);
                
                //add datatable to dataset
                s.Tables.Add(dtSWIFTNotMatch);
                s.Tables.Add(dtSWIFTMatched);
                s.Tables.Add(dtOpicsNotMatch);

                if (s != null)
                {
                    if (s.Tables.Count > 0)
                    {
                        report.SetDataSource(s);
                        Log.Info("Total Table : " + s.Tables.Count + " Table(s)");
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
                Log.Info("-----Model_RPT_TR152OBOSWIFT Finish-----");
                o.closeConnection();
            }
            return report;
        }

        

        private static DataTable getSWIFTNotMatch(string ConnectionString, string CTPY, string IncomingDate)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sqlSWIFTNotMatch = "SELECT  D.*, F.MSG1, F.MSG2, F.MSG3, F.MSG4, F.MSG5, F.MSG6, F.MSG7, F.MSG8   FROM OPICINF.KKB_TB_SWIFTMSG_DETAIL D LEFT OUTER JOIN OPICINF.KKB_TB_SWIFTMSG_FILE F ON D.SWIFTFILEID=F.SWIFTFILEID WHERE D.MSGCODE='300' AND F.DEALNO IS NULL ";
                if (!IncomingDate.Trim().Equals(""))
                    sqlSWIFTNotMatch += "AND TO_CHAR(F.INCOMINGDATE,'DD/MM/YYYY')='" + IncomingDate + "' ";
                sqlSWIFTNotMatch += " ORDER BY F.INCOMINGDATE ";
                o.createCommand(sqlSWIFTNotMatch, CommandType.Text);
                DataTable dtSWIFTNotMatchMessage = o.executeReaderToDT("SWIFTNOTMATCHTABLE");

                DataTable dt = new DataTable { TableName = "TB_SWIFT300NOTMATCH", Columns = { "SENDERREF", "TYPEOFOPER", "RELATEDREF", "BIC", "DEALDATE", "VDATE", "CCYRATE_8", "CCY", new DataColumn("CCYAMT", typeof(System.Decimal)), "CTRCCY", new DataColumn("CTRAMT", typeof(System.Decimal)) } };

                if (dtSWIFTNotMatchMessage.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["SENDERREF"] = "";
                    dr["TYPEOFOPER"] = "";
                    dr["RELATEDREF"] = "";
                    dr["BIC"] = "";
                    dr["DEALDATE"] = "";
                    dr["VDATE"] = "";
                    dr["CCYRATE_8"] = "";
                    dr["CCY"] = "";
                    dr["CCYAMT"] = DBNull.Value;
                    dr["CTRCCY"] = "";
                    dr["CTRAMT"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
                else
                {
                    for (int i = 0; i < dtSWIFTNotMatchMessage.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        String str = dtSWIFTNotMatchMessage.Rows[i]["MSG1"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG2"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG3"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG4"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG5"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG6"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG7"].ToString().Trim()
                                + dtSWIFTNotMatchMessage.Rows[i]["MSG8"].ToString().Trim();
                        Dictionary<string, string[]> msg = SWIFTMsg.SpliteMessage(str.Trim());
                        dr["SENDERREF"] = msg["4"][Array.IndexOf(msg["4"], "20") + 1].Replace("\r\n", "");
                        dr["TYPEOFOPER"] = msg["4"][Array.IndexOf(msg["4"], "22A") + 1].Replace("\r\n", "");
                        dr["RELATEDREF"] = msg["4"][Array.IndexOf(msg["4"], "21") + 1].Replace("\r\n", "");
                        //dr["BIC"] = msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "").Substring(0, msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "").Length - 3);
                        dr["BIC"] = msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "");
                        dr["DEALDATE"] = msg["4"][Array.IndexOf(msg["4"], "30T") + 1].Replace("\r\n", "");
                        dr["VDATE"] = msg["4"][Array.IndexOf(msg["4"], "30V") + 1].Replace("\r\n", "");
                        dr["CCYRATE_8"] = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "36") + 1].Replace("\r\n", "").Replace(",", "."));
                        dr["CCY"] = msg["4"][Array.IndexOf(msg["4"], "32B") + 1].Replace("\r\n", "").Substring(0, 3);
                        dr["CCYAMT"] = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "32B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                        dr["CTRCCY"] = msg["4"][Array.IndexOf(msg["4"], "33B") + 1].Replace("\r\n", "").Substring(0, 3);
                        dr["CTRAMT"] = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "33B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }

        private static DataTable getSWIFTMatch(string ConnectionString, string CTPY, string IncomingDate)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            DataTable dt = new DataTable { TableName = "TB_SWIFT300MATCHED", Columns = { "O_MATCHSTATUS", "O_DEALNO", "O_REVDATE", "O_CMNE", "O_BIC", "O_DEALDATE", "O_VDATE", "O_CCYRATE_8", "O_CCY", new DataColumn("O_CCYAMT", typeof(System.Decimal)), "O_CTRCCY", new DataColumn("O_CTRAMT", typeof(System.Decimal)), "S_SENDERREF", "S_TYPEOFOPER", "S_RELATEDREF", "S_BIC", "S_DEALDATE", "S_VDATE", "S_CCYRATE_8", "S_CCY", new DataColumn("S_CCYAMT", typeof(System.Decimal)), "S_CTRCCY", new DataColumn("S_CTRAMT", typeof(System.Decimal)) } };

            try
            {
                o.openConnection();
                string sqlSWIFTMatched = "SELECT 'Match' as MatchStatus, TRIM(FXDH.DEALNO) as DEALNO, CUST.CMNE, TRIM(CUST.BIC) as BIC, TO_CHAR(FXDH.DEALDATE,'YYYYMMDD') as DEALDATE, TO_CHAR(FXDH.VDATE,'YYYYMMDD') as VDATE, TO_CHAR(FXDH.REVDATE,'YYYYMMDD') as REVDATE, ABS(FXDH.CCYRATE_8) as CCYRATE_8, TRIM(FXDH.CCY) as CCY, ABS(FXDH.CCYAMT) as CCYAMT, TRIM(FXDH.CTRCCY) as CTRCCY, ABS(FXDH.CTRAMT) as CTRAMT, MSG1, MSG2, MSG3, MSG4, MSG5, MSG6, MSG7, MSG8 FROM OPICINF.KKB_TB_SWIFTMSG_FILE INNER JOIN OPICS.FXDH ON TRIM(KKB_TB_SWIFTMSG_FILE.DEALNO)=TRIM(FXDH.DEALNO) LEFT OUTER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO WHERE KKB_TB_SWIFTMSG_FILE.MSGCODE='300' ";
                if (!IncomingDate.Trim().Equals(""))
                    sqlSWIFTMatched += "AND TO_CHAR(KKB_TB_SWIFTMSG_FILE.INCOMINGDATE,'DD/MM/YYYY')='" + IncomingDate + "' ";
                sqlSWIFTMatched += " Order By FXDH.DEALNO ";
                o.createCommand(sqlSWIFTMatched, CommandType.Text);

                DataTable dtSWIFTMatched = o.executeReaderToDT("SWIFTMATCHEDTABLE");

                
                if (dtSWIFTMatched.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();

                    dr["O_MATCHSTATUS"] = "";
                    dr["O_DEALNO"] = "";
                    dr["O_REVDATE"] = "";
                    dr["O_CMNE"] = "";
                    dr["O_BIC"] = "";
                    dr["O_DEALDATE"] = "";
                    dr["O_VDATE"] = "";
                    dr["O_CCYRATE_8"] = "";
                    dr["O_CCY"] = "";
                    dr["O_CCYAMT"] = DBNull.Value;
                    dr["O_CTRCCY"] = "";
                    dr["O_CTRAMT"] = DBNull.Value;

                    dr["S_SENDERREF"] = "";
                    dr["S_TYPEOFOPER"] = "";
                    dr["S_RELATEDREF"] = "";
                    dr["S_BIC"] = "";
                    dr["S_DEALDATE"] = "";
                    dr["S_VDATE"] = "";
                    dr["S_CCYRATE_8"] = "";
                    dr["S_CCY"] = "";
                    dr["S_CCYAMT"] = DBNull.Value;
                    dr["S_CTRCCY"] = "";
                    dr["S_CTRAMT"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
                else
                {
                    for (int i = 0; i < dtSWIFTMatched.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["O_MATCHSTATUS"] = "MATCH";
                        dr["O_DEALNO"] = dtSWIFTMatched.Rows[i]["DEALNO"];
                        dr["O_REVDATE"] = dtSWIFTMatched.Rows[i]["REVDATE"];
                        dr["O_CMNE"] = dtSWIFTMatched.Rows[i]["CMNE"];
                        dr["O_BIC"] = dtSWIFTMatched.Rows[i]["BIC"];
                        dr["O_DEALDATE"] = dtSWIFTMatched.Rows[i]["DEALDATE"];
                        dr["O_VDATE"] = dtSWIFTMatched.Rows[i]["VDATE"];
                        dr["O_CCYRATE_8"] = dtSWIFTMatched.Rows[i]["CCYRATE_8"];
                        dr["O_CCY"] = dtSWIFTMatched.Rows[i]["CCY"];
                        dr["O_CCYAMT"] = decimal.Parse(dtSWIFTMatched.Rows[i]["CCYAMT"].ToString());
                        dr["O_CTRCCY"] = dtSWIFTMatched.Rows[i]["CTRCCY"];
                        dr["O_CTRAMT"] = decimal.Parse(dtSWIFTMatched.Rows[i]["CTRAMT"].ToString());

                        String str = dtSWIFTMatched.Rows[i]["MSG1"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG2"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG3"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG4"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG5"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG6"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG7"].ToString().Trim()
                                + dtSWIFTMatched.Rows[i]["MSG8"].ToString().Trim();
                        Dictionary<string, string[]> msg = SWIFTMsg.SpliteMessage(str.Trim());
                        dr["S_SENDERREF"] = msg["4"][Array.IndexOf(msg["4"], "20") + 1].Replace("\r\n", "");
                        dr["S_TYPEOFOPER"] = msg["4"][Array.IndexOf(msg["4"], "22A") + 1].Replace("\r\n", "");
                        dr["S_RELATEDREF"] = msg["4"][Array.IndexOf(msg["4"], "21") + 1].Replace("\r\n", "");
                        //dr["S_BIC"] = msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "").Substring(0, msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "").Length - 3);
                        dr["S_BIC"] = msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "");
                        dr["S_DEALDATE"] = msg["4"][Array.IndexOf(msg["4"], "30T") + 1].Replace("\r\n", "");
                        dr["S_VDATE"] = msg["4"][Array.IndexOf(msg["4"], "30V") + 1].Replace("\r\n", "");
                        dr["S_CCYRATE_8"] = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "36") + 1].Replace("\r\n", "").Replace(",", "."));
                        dr["S_CCY"] = msg["4"][Array.IndexOf(msg["4"], "32B") + 1].Replace("\r\n", "").Substring(0, 3);
                        dr["S_CCYAMT"] = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "32B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                        dr["S_CTRCCY"] = msg["4"][Array.IndexOf(msg["4"], "33B") + 1].Replace("\r\n", "").Substring(0, 3);
                        dr["S_CTRAMT"] = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "33B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                        dt.Rows.Add(dr);
                    }
                }
                
                return dt;
            }
            catch (Exception ex)
            {
                DataRow dr = dt.NewRow();

                dr["O_MATCHSTATUS"] = "";
                dr["O_DEALNO"] = "";
                dr["O_REVDATE"] = "";
                dr["O_CMNE"] = "";
                dr["O_BIC"] = "";
                dr["O_DEALDATE"] = "";
                dr["O_VDATE"] = "";
                dr["O_CCYRATE_8"] = "";
                dr["O_CCY"] = "";
                dr["O_CCYAMT"] = DBNull.Value;
                dr["O_CTRCCY"] = "";
                dr["O_CTRAMT"] = DBNull.Value;

                dr["S_SENDERREF"] = "";
                dr["S_TYPEOFOPER"] = "";
                dr["S_RELATEDREF"] = "";
                dr["S_BIC"] = "";
                dr["S_DEALDATE"] = "";
                dr["S_VDATE"] = "";
                dr["S_CCYRATE_8"] = "";
                dr["S_CCY"] = "";
                dr["S_CCYAMT"] = DBNull.Value;
                dr["S_CTRCCY"] = "";
                dr["S_CTRAMT"] = DBNull.Value;
                dt.Rows.Add(dr);
                return dt;
            }
            finally
            {
                o.closeConnection();
            }
        }

        private static DataTable getOpicsNotMatch(string ConnectionString, string CTPY, string IncomingDate)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                string sqlOpicsNotMatch = "SELECT TRIM(FXDH.DEALNO) as DEALNO, TRIM(CUST.CMNE) as CMNE, TRIM(CUST.BIC) as BIC, TO_CHAR(FXDH.DEALDATE,'YYYYMMDD') as DEALDATE, TO_CHAR(FXDH.VDATE,'YYYYMMDD') as VDATE, TO_CHAR(FXDH.REVDATE,'YYYYMMDD') as REVDATE, ABS(FXDH.CCYRATE_8) as CCYRATE_8, TRIM(FXDH.CCY) as CCY, ABS(FXDH.CCYAMT) as CCYAMT, TRIM(FXDH.CTRCCY) as CTRCCY, ABS(FXDH.CTRAMT) as CTRAMT  FROM OPICS.FXDH LEFT OUTER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO WHERE REVDATE IS NULL AND CTYPE='B'  ";
                if (!CTPY.Trim().Equals(""))
                    sqlOpicsNotMatch += "AND CUST.CMNE='" + CTPY + "' ";                
                if (!IncomingDate.Trim().Equals(""))
                    sqlOpicsNotMatch += "AND DEALDATE=TO_DATE('" + IncomingDate + "','DD/MM/YYYY') "; 
                sqlOpicsNotMatch += "AND NOT EXISTS (SELECT DEALNO FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE KKB_TB_SWIFTMSG_FILE.DEALNO=FXDH.DEALNO AND KKB_TB_SWIFTMSG_FILE.MSGCODE='300') ";
                sqlOpicsNotMatch += " ORDER BY DEALNO ";
                o.createCommand(sqlOpicsNotMatch, CommandType.Text);
                DataTable dt = o.executeReaderToDT("TB_OPICSNOTMATCHs");
                if (dt.Rows.Count == 0)
                {
                    DataTable tempdt = new DataTable() { TableName = "TB_OPICSNOTMATCHs", Columns = { "DEALNO", "CMNE", "BIC", "DEALDATE", "VDATE", "REVDATE", "CCYRATE_8", "CCY", new DataColumn("CCYAMT", typeof(System.Decimal)), "CTRCCY", new DataColumn("CTRAMT", typeof(System.Decimal)) } };
                    DataRow dr = tempdt.NewRow();
                    dr["DEALNO"] = "";
                    dr["CMNE"] = "";
                    dr["BIC"] = "";
                    dr["DEALDATE"] = "";
                    dr["VDATE"] = "";
                    dr["REVDATE"] = "";
                    dr["CCYRATE_8"] = "";
                    dr["CCY"] = "";
                    dr["CCYAMT"] = DBNull.Value;
                    dr["CTRCCY"] = "";
                    dr["CTRAMT"] = DBNull.Value;
                    tempdt.Rows.Add(dr);
                    return tempdt;
                }
                return dt;
            }
            catch (Exception ex)
            {
                DataTable tempdt = new DataTable() { TableName = "TB_OPICSNOTMATCHs", Columns = { "DEALNO", "CMNE", "BIC", "DEALDATE", "VDATE", "REVDATE", "CCYRATE_8", "CCY", new DataColumn("CCYAMT", typeof(System.Decimal)), "CTRCCY", new DataColumn("CTRAMT", typeof(System.Decimal)) } };
                DataRow dr = tempdt.NewRow();
                dr["DEALNO"] = "";
                dr["CMNE"] = "";
                dr["BIC"] = "";
                dr["DEALDATE"] = "";
                dr["VDATE"] = "";
                dr["REVDATE"] = "";
                dr["CCYRATE_8"] = "";
                dr["CCY"] = "";
                dr["CCYAMT"] = DBNull.Value;
                dr["CTRCCY"] = "";
                dr["CTRAMT"] = DBNull.Value;
                tempdt.Rows.Add(dr);
                return tempdt;
            }
            finally {
                o.closeConnection();
            }
        }
    }

}
