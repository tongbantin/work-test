using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Globalization;


namespace TreasuryModel.Data
{
    public class BESeries
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BESeries));        

        public int BESERIESID { get; set; }
        public string BE_SERIES_CODE { get; set; }
        public decimal BE_TOTAL_AMT { get; set; }
        public DateTime BE_ISSUE_DATE { get; set; }
        public DateTime BE_MAT_DATE { get; set; }
        public string BE_ISSUER_ID { get; set; }
        public decimal BE_DIS_RATE { get; set; }
        public string BE_SERIES_DEAL_NO { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }

        public string BE_ISSUE_DATE_STR { get; set; }
        public string BE_MAT_DATE_STR { get; set; }
        public string BE_ISSUER_NAME { get; set; }
        public string BE_ISSUER_BANK { get; set; }
        public string BE_ISSUER_ACCT { get; set; }
        public decimal BE_SERIES_REMAIN { get; set; }
        public decimal BE_SERIES_PROCEED { get; set; }
        public string BE_AVAL_TYPE { get; set; }

        public Customer CUSTOMER { get; set; }        

        public static List<BESeries> BESeriesList(string ConnectionString, string Series, string MatFromDate, string MatToDate ,string IssueFromDate , string IssueToDate)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select BESERIESID, BE_SERIES_CODE, BE_TOTAL_AMT, BE_SERIES_DEAL_NO, TO_CHAR(BE_ISSUE_DATE,'MM/DD/YYYY HH:MI:SS') as BE_ISSUE_DATE"
                           + ", BE_DIS_RATE, BE_ISSUER_ID, CUST1.NAMEEN as BE_ISSUER_NAME, TO_CHAR(BE_MAT_DATE,'MM/DD/YYYY HH:MI:SS') as BE_MAT_DATE"
                           + ", NVL(BE_AVAL_TYPE, '0') as BE_AVAL_TYPE"
                           + ", NAMEEN as BE_ISSUER_NAME, TARGETACCT as BE_ISSUER_ACCT, TARGETBANK as BE_ISSUER_BANK, BE_TOTAL_AMT - NVL((SELECT SUM(NVL(BE_ALLOT_AMT,0))"
                           + " FROM OPICINF.KKB_TB_BE_ALLOTMENT WHERE BESERIESID = KKB_TB_BE_SERIES.BESERIESID AND FLAG IS NULL),0) as BE_SERIES_REMAIN, NVL(BE_SERIES_PROCEED,BE_TOTAL_AMT) as BE_SERIES_PROCEED"
                           + " From OPICINF.KKB_TB_BE_SERIES LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER CUST1 ON KKB_TB_BE_SERIES.BE_ISSUER_ID = CUST1.CUSTOMERID"
                           + " Where 1=1 ";
 // "Select BESERIESID, BE_SERIES_CODE, BE_TOTAL_AMT, BE_SERIES_DEAL_NO, TO_CHAR(BE_ISSUE_DATE,'MM/DD/YYYY HH:MI:SS') as BE_ISSUE_DATE, BE_DIS_RATE, BE_ISSUER_ID, TO_CHAR(BE_MAT_DATE,'MM/DD/YYYY HH:MI:SS') as BE_MAT_DATE, NAMEEN as BE_ISSUER_NAME, TARGETACCT as BE_ISSUER_ACCT, TARGETBANK as BE_ISSUER_BANK, BE_TOTAL_AMT - NVL((SELECT SUM(NVL(BE_ALLOT_AMT,0)) FROM OPICINF.KKB_TB_BE_ALLOTMENT WHERE BESERIESID=KKB_TB_BE_SERIES.BESERIESID AND FLAG IS NULL),0) as BE_SERIES_REMAIN, NVL(BE_SERIES_PROCEED,BE_TOTAL_AMT) as BE_SERIES_PROCEED  From OPICINF.KKB_TB_BE_SERIES LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER ON KKB_TB_BE_SERIES.BE_ISSUER_ID=KKB_TB_CUSTOMER.CUSTOMERID  Where 1=1 ";
                if (!Series.Equals(""))
                    sql += "AND UPPER(TRIM(BE_SERIES_CODE)) like UPPER('" + Series + "%')";
                if (!MatFromDate.Equals(""))
                {
                    if (MatToDate.Equals(""))
                        sql += "AND BE_MAT_DATE=TO_DATE('" + MatFromDate + "','DD/MM/YYYY') ";
                    else
                        sql += "AND BE_MAT_DATE BETWEEN TO_DATE('" + MatFromDate + "','DD/MM/YYYY') AND TO_DATE('" + MatToDate + "','DD/MM/YYYY') ";
                }
                if (!IssueFromDate.Equals(""))
                {
                    if (IssueToDate.Equals(""))
                        sql += "AND BE_ISSUE_DATE=TO_DATE('" + IssueFromDate + "','DD/MM/YYYY') ";
                    else
                        sql += "AND BE_ISSUE_DATE BETWEEN TO_DATE('" + IssueFromDate + "','DD/MM/YYYY') AND TO_DATE('" + IssueToDate + "','DD/MM/YYYY') ";
                }

                sql += "ORDER BY BESERIESID DESC";

                Log.Info("-----BESeriesList-----");  
                Log.Info("SQL Statement : " + sql);

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BESeries");

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
             

                List<BESeries> param = new List<BESeries>();
                int rownum = dt.Rows.Count > 100 ? 100 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    BESeries p = new BESeries();
                    p.BESERIESID = int.Parse(dt.Rows[i]["BESERIESID"].ToString());
                    p.BE_SERIES_CODE = dt.Rows[i]["BE_SERIES_CODE"].ToString();
                    p.BE_TOTAL_AMT = decimal.Parse(dt.Rows[i]["BE_TOTAL_AMT"].ToString());
                    p.BE_SERIES_DEAL_NO = dt.Rows[i]["BE_SERIES_DEAL_NO"].ToString();
                    p.BE_ISSUE_DATE = DateTime.Parse(dt.Rows[i]["BE_ISSUE_DATE"].ToString());
                    p.BE_DIS_RATE = decimal.Parse(dt.Rows[i]["BE_DIS_RATE"].ToString());
                    p.BE_ISSUER_ID = dt.Rows[i]["BE_ISSUER_ID"].ToString();
                    p.BE_MAT_DATE = DateTime.Parse(dt.Rows[i]["BE_MAT_DATE"].ToString());
                    p.BE_ISSUER_NAME = dt.Rows[i]["BE_ISSUER_NAME"].ToString();
                    p.BE_ISSUER_BANK = dt.Rows[i]["BE_ISSUER_BANK"].ToString();
                    p.BE_ISSUER_ACCT = dt.Rows[i]["BE_ISSUER_ACCT"].ToString();
                    p.BE_SERIES_REMAIN = Decimal.Parse(dt.Rows[i]["BE_SERIES_REMAIN"].ToString());
                    p.BE_SERIES_PROCEED = Decimal.Parse(dt.Rows[i]["BE_SERIES_PROCEED"].ToString());

                    p.BE_ISSUE_DATE_STR = p.BE_ISSUE_DATE.ToString("dd/MM/yyyy");
                    p.BE_MAT_DATE_STR = p.BE_MAT_DATE.ToString("dd/MM/yyyy");
                    p.BE_ISSUER_NAME = dt.Rows[i]["BE_ISSUER_NAME"].ToString();
                    p.BE_AVAL_TYPE = dt.Rows[i]["BE_AVAL_TYPE"].ToString();
                    param.Add(p);
                }

                //Return result to jTable
                return param;                

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static object getBESeries(string ConnectionString, int BESeriesID)
        {
            String sql = "Select BESERIESID, BE_SERIES_CODE, BE_TOTAL_AMT, BE_SERIES_DEAL_NO, TO_CHAR(BE_ISSUE_DATE,'MM/DD/YYYY') as BE_ISSUE_DATE, BE_DIS_RATE, NVL(BE_ISSUER_ID,0) as BE_ISSUER_ID "
                       + ", TO_CHAR(BE_MAT_DATE,'MM/DD/YYYY') as BE_MAT_DATE, NVL(CUST1.CUSTOMERID,0) as CUSTOMERID, NVL(CUST1.NAMEEN,'') as NAMEEN, NVL(CUST1.TARGETACCT,'') as TARGETACCT "
                       + ", NVL(CUST1.TARGETBANK,'') as TARGETBANK From OPICINF.KKB_TB_BE_SERIES SERIES "
                       + "LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER CUST1 ON SERIES.BE_ISSUER_ID = CUST1.CUSTOMERID "
                       + "WHERE BESERIESID='" + BESeriesID + "' ";
            //"Select BESERIESID, BE_SERIES_CODE, BE_TOTAL_AMT, BE_SERIES_DEAL_NO, TO_CHAR(BE_ISSUE_DATE,'MM/DD/YYYY') as BE_ISSUE_DATE, BE_DIS_RATE, NVL(BE_ISSUER_ID,0) as BE_ISSUER_ID, TO_CHAR(BE_MAT_DATE,'MM/DD/YYYY') as BE_MAT_DATE, NVL(CUSTOMERID,0) as CUSTOMERID, NVL(NAMEEN,'') as NAMEEN, NVL(TARGETACCT,'') as TARGETACCT, NVL(TARGETBANK,'') as TARGETBANK From OPICINF.KKB_TB_BE_SERIES LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER ON KKB_TB_BE_SERIES.BE_ISSUER_ID=KKB_TB_CUSTOMER.CUSTOMERID Where BESERIESID='" + BESeriesID + "' ";
            Log.Info("-----KKB_TB_BE_SERIES Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BESeries");

                List<BESeries> param = new List<BESeries>();
                BESeries p = new BESeries();
                p.BESERIESID = int.Parse(dt.Rows[0]["BESERIESID"].ToString());
                p.BE_SERIES_CODE = dt.Rows[0]["BE_SERIES_CODE"].ToString();
                p.BE_TOTAL_AMT = decimal.Parse(dt.Rows[0]["BE_TOTAL_AMT"].ToString());
                p.BE_SERIES_DEAL_NO = dt.Rows[0]["BE_SERIES_DEAL_NO"].ToString();
                p.BE_ISSUE_DATE = DateTime.ParseExact(dt.Rows[0]["BE_ISSUE_DATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                p.BE_DIS_RATE = decimal.Parse(dt.Rows[0]["BE_DIS_RATE"].ToString());
                p.BE_ISSUER_ID = dt.Rows[0]["BE_ISSUER_ID"].ToString();
                p.BE_MAT_DATE = DateTime.ParseExact(dt.Rows[0]["BE_MAT_DATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                p.BE_ISSUE_DATE_STR = p.BE_ISSUE_DATE.ToString("dd/MM/yyyy");
                p.BE_MAT_DATE_STR = p.BE_MAT_DATE.ToString("dd/MM/yyyy");

                Customer c = new Customer();
                c.CustomerID = int.Parse(dt.Rows[0]["CUSTOMERID"].ToString());
                c.NameEn = dt.Rows[0]["NAMEEN"].ToString();
                c.TargetAcct = dt.Rows[0]["TARGETACCT"].ToString();
                c.TargetBank = dt.Rows[0]["TARGETBANK"].ToString();
                p.CUSTOMER = c;

                param.Add(p);

                //Return result to jTable
                return param;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_SERIES Finish-----");
                o.closeConnection();
            }
        }

        public static object Create(string ConnectionString, BESeries record)
        {

            Log.Info("-----KKB_TB_BE_SERIES Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                int day = (record.BE_MAT_DATE - record.BE_ISSUE_DATE).Days;
                decimal proceed = Math.Round(100 / (1 + (record.BE_DIS_RATE/100 * day / 365)), 6) * (record.BE_TOTAL_AMT / 100);
                o.openConnection();
                string sql = "Insert Into OPICINF.KKB_TB_BE_SERIES(BE_SERIES_CODE, BE_TOTAL_AMT, BE_ISSUE_DATE, BE_MAT_DATE, BE_ISSUER_ID, BE_DIS_RATE, BE_SERIES_DEAL_NO, BE_SERIES_PROCEED, CREATE_DATE, CREATE_USER, BE_AVAL_TYPE) VALUES(:BE_SERIES_CODE, :BE_TOTAL_AMT, TO_DATE(:BE_ISSUE_DATE,'DD/MM/YYYY'), TO_DATE(:BE_MAT_DATE,'DD/MM/YYYY'), :BE_ISSUER_ID, :BE_DIS_RATE, :BE_SERIES_DEAL_NO, :BE_SERIES_PROCEED, SYSDATE, :CREATE_USER, :BE_AVAL_TYPE)";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("BE_SERIES_CODE", record.BE_SERIES_CODE);
                o.setInputParameter("BE_TOTAL_AMT", record.BE_TOTAL_AMT);
                o.setInputParameter("BE_ISSUE_DATE", record.BE_ISSUE_DATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("BE_MAT_DATE", record.BE_MAT_DATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("BE_ISSUER_ID", record.BE_ISSUER_ID);
                o.setInputParameter("BE_DIS_RATE", record.BE_DIS_RATE);
                o.setInputParameter("BE_SERIES_DEAL_NO", record.BE_SERIES_DEAL_NO);
                o.setInputParameter("BE_SERIES_PROCEED", proceed);
                o.setInputParameter("CREATE_USER", record.CREATE_USER);
                o.setInputParameter("BE_AVAL_TYPE", record.BE_AVAL_TYPE);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_DEALFEE Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not insert data." };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

        public static object Update(string ConnectionString, BESeries record)
        {
            
            Log.Info("-----KKB_TB_DEALFEE Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                int day = (record.BE_MAT_DATE - record.BE_ISSUE_DATE).Days;
                decimal proceed = Math.Round(100/(1+(record.BE_DIS_RATE/100 * day/365)),6) * (record.BE_TOTAL_AMT/100);
                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_BE_SERIES Set BE_SERIES_CODE=:BE_SERIES_CODE, BE_TOTAL_AMT=:BE_TOTAL_AMT, BE_ISSUE_DATE=TO_DATE(:BE_ISSUE_DATE, 'DD/MM/YYYY'), BE_MAT_DATE=TO_DATE(:BE_MAT_DATE, 'DD/MM/YYYY'), BE_ISSUER_ID=:BE_ISSUER_ID, BE_DIS_RATE=:BE_DIS_RATE, BE_SERIES_DEAL_NO=:BE_SERIES_DEAL_NO, BE_SERIES_PROCEED=:BE_SERIES_PROCEED, UPDATE_DATE=SYSDATE, UPDATE_USER=:UPDATE_USER Where BESERIESID=:BESERIESID ";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("BE_SERIES_CODE", record.BE_SERIES_CODE);
                o.setInputParameter("BE_TOTAL_AMT", record.BE_TOTAL_AMT);
                o.setInputParameter("BE_ISSUE_DATE", record.BE_ISSUE_DATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("BE_MAT_DATE", record.BE_MAT_DATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("BE_ISSUER_ID", record.BE_ISSUER_ID);
                o.setInputParameter("BE_DIS_RATE", record.BE_DIS_RATE);
                o.setInputParameter("BE_SERIES_DEAL_NO", record.BE_SERIES_DEAL_NO);
                o.setInputParameter("BE_SERIES_PROCEED", proceed);
                o.setInputParameter("UPDATE_USER", record.UPDATE_USER);
                o.setInputParameter("BESERIESID", record.BESERIESID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_DEALFEE Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

        public static object Delete(string ConnectionString, int BESERIESID)
        {

            Log.Info("-----KKB_TB_BE_SERIES Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_BE_SERIES WHERE BESERIESID='" + BESERIESID.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_BE_SERIES Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_SERIES Finish-----");
                o.closeConnection();
            }
        }

        // validation
        public static object duplicateBESeries(string ConnectionString, string beseriesno, int beseriesid)
        {
            string sql = string.Format("SELECT 1 FROM OPICINF.KKB_TB_BE_SERIES WHERE BE_SERIES_CODE = '{0}' AND BESERIESID <> {1}", beseriesno, beseriesid);

            Log.Info("-----Check Duplicate BE Series Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BESeries");

                if (dt.Rows.Count > 0)
                {
                    // duplicate
                    return new { Success = false, Message = string.Format("BE Series Code : {0} is duplicate.", beseriesno) };
                }
                else
                {
                    return new { Success = true, Message = "Successfully" };
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Check Duplicate BE Series Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Check Duplicate BE Series Finish-----");
                o.closeConnection();
            }
        }

        public static object duplicateBENo(string ConnectionString, string beno, int allotid)
        {
            string sql = string.Format("SELECT 1 FROM OPICINF.KKB_TB_BE_ALLOTMENT WHERE BE_ALLOT_NO = '{0}' AND BEALLOTMENTID <> {1} AND FLAG IS NULL", beno, allotid);

            Log.Info("-----Check Duplicate B/E No Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BESeries");

                if (dt.Rows.Count > 0)
                {
                    // duplicate
                    return new { Success = false, Message = string.Format("B/E No : {0} is duplicate.", beno) };
                }
                else
                {
                    return new { Success = true, Message = "Successfully." };
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Check Duplicate B/E No Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Check Duplicate B/E No Finish-----");
                o.closeConnection();
            }
        }

        public static object validateHoliday(string ConnectionString, DateTime date)
        {
            string sql = string.Format("SELECT 1 FROM OPICS.HLDY WHERE CALENDARID = '{0}' AND HOLIDATE = to_date('{1:dd/MM/yyyy}', 'dd/mm/yyyy')", "THB", date);

            Log.Info("-----Validate Holiday Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BESeries");

                if (dt.Rows.Count > 0)
                {
                    return new { Success = false, Message = string.Format("{0:dd/MM/yyyy} is holiday date.", date) };
                }
                else
                {
                    return new { Success = true, Message = "Successfully." };
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Validate Holiday Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Validate Holiday Finish-----");
                o.closeConnection();
            }
        }
    }
}
