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
    public class BEAllotment
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BESeries));

        public int BESERIESID { get; set; }
        public int BEALLOTMENTID { get; set; }
        public string BE_ALLOT_NO { get; set; }
        public string BE_ALLOT_REF_NO { get; set; }
        public decimal BE_ALLOT_AMT { get; set; }
        public decimal BE_ALLOT_UNIT { get; set; }
        public decimal BE_ALLOT_YIELD { get; set; }
        public string BE_ALLOT_COMMENT { get; set; }
        public DateTime BE_COMF_DATE { get; set; }
        public DateTime BE_SETTLE_DATE { get; set; }
        public string BE_COMF_DATE_STR { get; set; }
        public string BE_SETTLE_DATE_STR { get; set; }
        public string BE_BUYER_ID { get; set; }
        public string BE_BUYER_INCVAT { get; set; }
        public string BE_ALLOT_DEAL_NO { get; set; }
        public string BE_ALLOT_SKSNO { get; set; }
        public string BE_ALLOT_PATRAREGNO { get; set; }
        public string BE_ALLOT_CLIENTNAME { get; set; }
        public DateTime BE_ALLOT_DEPOSITDATE { get; set; }
        public DateTime BE_ALLOT_WITHDRAWDATE { get; set; }
        public string BE_ALLOT_IDCARD { get; set; }
        public string BE_ALLOT_BOOKBANK { get; set; }
        public string BE_ALLOT_OTHER { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        // add for identify copany KK or Other
        public string BE_OWNER { get; set; }

        public string BUYER_NAME { get; set; }
        public string BUYER_TAX { get; set; }
        public string BE_FLAG { get; set; }
        public DateTime BE_MAT_DATE { get; set; }
        public string BE_MAT_DATE_STR { get; set; }
        public string BE_ALLOT_DEPOSITDATE_STR { get; set; }
        public string BE_ALLOT_WITHDRAWDATE_STR { get; set; }

        public string BE_SELLER_ID { get; set; }
        public string SELLER_NAME { get; set; }

        public DateTime BE_SELLER_COMF_DATE { get; set; }
        public DateTime BE_SELLER_SETTLE_DATE { get; set; }
        public decimal BE_SELLER_ALLOT_YIELD { get; set; }

        public string DISPLAY_BUYER_COMF_DATE { get; set; }
        public string DISPLAY_BUYER_SETTLE_DATE { get; set; }
        public string DISPLAY_SELLER_COMF_DATE { get; set; }
        public string DISPLAY_SELLER_SETTLE_DATE { get; set; }
        public string BE_AVAL_TYPE { get; set; }
        
        public Customer CUSTOMER { get; set; }
        public Customer CUSTSELLER { get; set; }

        public static List<BEAllotment> BEAllotmentList(string ConnectionString, int BESERIESID)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                //string sql = "Select A.*, TO_CHAR(B.BE_MAT_DATE,'DD/MM/YYYY') as BE_MAT_DATE, C.NAMEEN as BUYER_NAME, ";
                //      sql += "CASE WHEN BE_OWNER_FLAG IS NULL THEN 'Kiatnakin' ELSE ( CASE WHEN  BE_OWNER_FLAG = 'KK' THEN 'Kiatnakin' ELSE 'Other' END  )END AS BE_OWNER ";
                //      sql += "From OPICINF.KKB_TB_BE_ALLOTMENT A LEFT JOIN OPICINF.KKB_TB_BE_SERIES B ON A.BESERIESID=B.BESERIESID LEFT JOIN OPICINF.KKB_TB_CUSTOMER C ON A.BE_BUYER_ID=C.CUSTOMERID Where (A.FLAG IS NULL OR A.FLAG='') AND A.BESERIESID='" + BESERIESID + "' ORDER BY A.BE_ALLOT_NO";
                string sql = "Select A.*, TO_CHAR(B.BE_MAT_DATE,'DD/MM/YYYY') as BE_MAT_DATE, C.NAMEEN as BUYER_NAME, nvl(S.NAMEEN, ' ') as SELLER_NAME, nvl(B.BE_AVAL_TYPE, '0') as BE_AVAL_TYPE, CASE WHEN BE_OWNER_FLAG IS NULL THEN 'Kiatnakin' ELSE ( CASE WHEN  BE_OWNER_FLAG = 'KK' THEN 'Kiatnakin' ELSE 'Other' END  )END AS BE_OWNER "
                           + "From OPICINF.KKB_TB_BE_ALLOTMENT A LEFT JOIN OPICINF.KKB_TB_BE_SERIES B ON A.BESERIESID=B.BESERIESID LEFT JOIN OPICINF.KKB_TB_CUSTOMER C ON A.BE_BUYER_ID=C.CUSTOMERID LEFT JOIN OPICINF.KKB_TB_CUSTOMER S ON A.BE_SELLER_ID=S.CUSTOMERID "
                           + "Where (A.FLAG IS NULL OR A.FLAG='') AND A.BESERIESID='" + BESERIESID + "' "
                           + "ORDER BY A.BE_ALLOT_NO";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("DealFee");

                List<BEAllotment> param = new List<BEAllotment>();
                int rownum = dt.Rows.Count > 200 ? 200 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    BEAllotment p = new BEAllotment();
                    p.BESERIESID = int.Parse(dt.Rows[i]["BESERIESID"].ToString());
                    p.BEALLOTMENTID = int.Parse(dt.Rows[i]["BEALLOTMENTID"].ToString());
                    p.BE_ALLOT_NO = dt.Rows[i]["BE_ALLOT_NO"].ToString();
                    p.BE_ALLOT_REF_NO = dt.Rows[i]["BE_ALLOT_REF_NO"].ToString();
                    p.BE_ALLOT_AMT = decimal.Parse(dt.Rows[i]["BE_ALLOT_AMT"].ToString());
                    p.BE_ALLOT_UNIT = dt.Rows[i]["BE_ALLOT_UNIT"] == DBNull.Value ? 0 : decimal.Parse(dt.Rows[i]["BE_ALLOT_UNIT"].ToString());
                    p.BE_ALLOT_YIELD = dt.Rows[i]["BE_ALLOT_YIELD"] == DBNull.Value ? 0 : decimal.Parse(dt.Rows[i]["BE_ALLOT_YIELD"].ToString());
                    p.BE_ALLOT_COMMENT = dt.Rows[i]["BE_ALLOT_COMMENT"].ToString();
                    //p.BE_COMF_DATE = dt.Rows[i]["BE_COMF_DATE"] == DBNull.Value ? string.Empty : DateTime.Parse(dt.Rows[i]["BE_COMF_DATE"].ToString());
                    //p.BE_SETTLE_DATE = dt.Rows[i]["BE_SETTLE_DATE"] == DBNull.Value ? string.Empty : DateTime.Parse(dt.Rows[i]["BE_SETTLE_DATE"].ToString());
                    p.DISPLAY_BUYER_COMF_DATE = dt.Rows[i]["BE_COMF_DATE"] == DBNull.Value ? string.Empty : DateTime.Parse(dt.Rows[i]["BE_COMF_DATE"].ToString()).ToString("dd/MM/yyyy");
                    p.DISPLAY_BUYER_SETTLE_DATE = dt.Rows[i]["BE_SETTLE_DATE"] == DBNull.Value ? string.Empty : DateTime.Parse(dt.Rows[i]["BE_SETTLE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    p.DISPLAY_SELLER_COMF_DATE = dt.Rows[i]["BE_SELLER_COMF_DATE"] == DBNull.Value ? string.Empty : DateTime.Parse(dt.Rows[i]["BE_SELLER_COMF_DATE"].ToString()).ToString("dd/MM/yyyy");
                    p.DISPLAY_SELLER_SETTLE_DATE = dt.Rows[i]["BE_SELLER_SETTLE_DATE"] == DBNull.Value ? string.Empty : DateTime.Parse(dt.Rows[i]["BE_SELLER_SETTLE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    p.BE_SELLER_ALLOT_YIELD = dt.Rows[i]["BE_SELLER_ALLOT_YIELD"] == DBNull.Value ? 0 : decimal.Parse(dt.Rows[i]["BE_SELLER_ALLOT_YIELD"].ToString());
                    p.BE_BUYER_ID = dt.Rows[i]["BE_BUYER_ID"].ToString();
                    p.BE_ALLOT_DEAL_NO = dt.Rows[i]["BE_ALLOT_DEAL_NO"].ToString();
                    p.BE_FLAG = dt.Rows[i]["FLAG"].ToString();
                    p.BE_SELLER_ID = dt.Rows[i]["BE_SELLER_ID"].ToString();
                    p.BUYER_NAME = dt.Rows[i]["BUYER_NAME"].ToString();
                    p.BE_OWNER = dt.Rows[i]["BE_OWNER"].ToString();
                    p.BE_AVAL_TYPE = dt.Rows[i]["BE_AVAL_TYPE"].ToString();

                    p.BE_MAT_DATE_STR = dt.Rows[i]["BE_MAT_DATE"].ToString();
                    p.BE_ALLOT_PATRAREGNO = dt.Rows[i]["BE_ALLOT_PATRAREGNO"].ToString();
                    p.SELLER_NAME = dt.Rows[i]["SELLER_NAME"].ToString();
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

        public static object getBEAllotment(string ConnectionString, int BEAllotmentID)
        {
            // String sql = "Select BE_ALLOT_NO, BE_ALLOT_REF_NO, BE_ALLOT_AMT, TO_CHAR(BE_COMF_DATE,'MM/DD/YYYY') as BE_COMF_DATE, TO_CHAR(BE_SETTLE_DATE,'MM/DD/YYYY') as BE_SETTLE_DATE, BE_ALLOT_UNIT, BE_ALLOT_YIELD, NVL(BE_BUYER_ID,'0') as BE_BUYER_ID, BE_ALLOT_DEAL_NO, BE_ALLOT_COMMENT, BESERIESID, NVL(CUSTOMERID,'0') as CUSTOMERID, NVL(NAMEEN,'') as NAMEEN , INCVAT, BE_ALLOT_DEAL_NO, BE_ALLOT_SKSNO, BE_ALLOT_PATRAREGNO, BE_ALLOT_CLIENTNAME, TO_CHAR(BE_ALLOT_DEPOSITDATE,'MM/DD/YYYY') as BE_ALLOT_DEPOSITDATE, TO_CHAR(BE_ALLOT_WITHDRAWDATE,'MM/DD/YYYY') as BE_ALLOT_WITHDRAWDATE , BE_ALLOT_IDCARD, BE_ALLOT_BOOKBANK, BE_ALLOT_OTHER, BE_OWNER_FLAG From OPICINF.KKB_TB_BE_ALLOTMENT LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER ON KKB_TB_BE_ALLOTMENT.BE_BUYER_ID=KKB_TB_CUSTOMER.CUSTOMERID Where BEALLOTMENTID='" + BEAllotmentID + "' ";
            string sql = "SELECT AL.BE_ALLOT_NO, AL.BE_ALLOT_REF_NO, AL.BE_ALLOT_AMT, TO_CHAR(AL.BE_COMF_DATE,'MM/DD/YYYY') as BE_COMF_DATE, TO_CHAR(AL.BE_SETTLE_DATE,'MM/DD/YYYY') as BE_SETTLE_DATE"
                       + ", AL.BE_ALLOT_UNIT, AL.BE_ALLOT_YIELD, NVL(AL.BE_BUYER_ID,'0') as BE_BUYER_ID, AL.BE_ALLOT_DEAL_NO, AL.BE_ALLOT_COMMENT, AL.BESERIESID, NVL(AL.BE_SELLER_ID, '0') as BE_SELLER_ID"
                       + ", NVL(SC.NAMEEN, ' ') as SELLER_NAME, NVL(AL.BE_SELLER_ALLOT_YIELD, 0) as BE_SELLER_ALLOT_YIELD, NVL(SC.TARGETBANK, ' ') as SELLER_TARGETBANK, NVL(SC.TARGETACCT, ' ') as SELLER_TARGETACCT"
                       + ", TO_CHAR(AL.BE_SELLER_COMF_DATE,'MM/DD/YYYY') as BE_SELLER_COMF_DATE, TO_CHAR(AL.BE_SELLER_SETTLE_DATE,'MM/DD/YYYY') as BE_SELLER_SETTLE_DATE, NVL(BC.CUSTOMERID,'0') as CUSTOMERID, NVL(BC.NAMEEN,' ') as NAMEEN"
                       + ", BC.INCVAT, nvl(BC.TARGETBANK, ' ') as BUYER_TARGETBANK, nvl(BC.TARGETACCT, ' ') as BUYER_TARGETACCT, AL.BE_ALLOT_DEAL_NO, AL.BE_ALLOT_SKSNO, AL.BE_ALLOT_PATRAREGNO, AL.BE_ALLOT_CLIENTNAME"
                       + ", TO_CHAR(AL.BE_ALLOT_DEPOSITDATE,'MM/DD/YYYY') as BE_ALLOT_DEPOSITDATE, TO_CHAR(AL.BE_ALLOT_WITHDRAWDATE,'MM/DD/YYYY') as BE_ALLOT_WITHDRAWDATE, AL.BE_ALLOT_IDCARD, AL.BE_ALLOT_BOOKBANK"
                       + ", AL.BE_ALLOT_OTHER, AL.BE_OWNER_FLAG "
                       + "From OPICINF.KKB_TB_BE_ALLOTMENT AL LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER BC ON AL.BE_BUYER_ID=BC.CUSTOMERID LEFT OUTER JOIN OPICINF.KKB_TB_CUSTOMER SC ON AL.BE_SELLER_ID=SC.CUSTOMERID "
                       + "Where AL.BEALLOTMENTID='" + BEAllotmentID + "'";
            Log.Info("-----KKB_TB_BE_ALLOTMENT Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BESeries");

                List<BEAllotment> param = new List<BEAllotment>();
                BEAllotment p = new BEAllotment();
                p.BESERIESID = int.Parse(dt.Rows[0]["BESERIESID"].ToString());
                p.BE_ALLOT_NO = dt.Rows[0]["BE_ALLOT_NO"].ToString();
                p.BE_ALLOT_REF_NO = dt.Rows[0]["BE_ALLOT_REF_NO"].ToString();
                p.BE_ALLOT_AMT = Decimal.Parse(dt.Rows[0]["BE_ALLOT_AMT"].ToString());
                if (!dt.Rows[0]["BE_COMF_DATE"].ToString().Equals(""))
                {
                    p.BE_COMF_DATE = DateTime.ParseExact(dt.Rows[0]["BE_COMF_DATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //p.BE_COMF_DATE = DateTime.Parse(dt.Rows[0]["BE_COMF_DATE"].ToString());
                    p.BE_COMF_DATE_STR = p.BE_COMF_DATE.ToString("dd/MM/yyyy");
                }
                if (!dt.Rows[0]["BE_SETTLE_DATE"].ToString().Equals(""))
                {
                    p.BE_SETTLE_DATE = DateTime.ParseExact(dt.Rows[0]["BE_SETTLE_DATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //p.BE_SETTLE_DATE = DateTime.Parse(dt.Rows[0]["BE_SETTLE_DATE"].ToString());
                    p.BE_SETTLE_DATE_STR = p.BE_SETTLE_DATE.ToString("dd/MM/yyyy");
                }
                if (!dt.Rows[0]["BE_SELLER_COMF_DATE"].ToString().Equals(""))
                {
                    p.BE_SELLER_COMF_DATE = DateTime.ParseExact(dt.Rows[0]["BE_SELLER_COMF_DATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    p.DISPLAY_SELLER_COMF_DATE = p.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy");
                }
                if(!dt.Rows[0]["BE_SELLER_SETTLE_DATE"].ToString().Equals(""))
                {
                    p.BE_SELLER_SETTLE_DATE = DateTime.ParseExact(dt.Rows[0]["BE_SELLER_SETTLE_DATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    p.DISPLAY_SELLER_SETTLE_DATE = p.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy");
                }
                p.BE_ALLOT_UNIT = Decimal.Parse(dt.Rows[0]["BE_ALLOT_UNIT"].ToString());
                p.BE_ALLOT_YIELD = Decimal.Parse(dt.Rows[0]["BE_ALLOT_YIELD"].ToString());
                p.BE_SELLER_ALLOT_YIELD = Decimal.Parse(dt.Rows[0]["BE_SELLER_ALLOT_YIELD"].ToString());
                p.BE_BUYER_ID = dt.Rows[0]["BE_BUYER_ID"].ToString();
                p.BE_ALLOT_COMMENT = dt.Rows[0]["BE_ALLOT_COMMENT"].ToString();
                p.BE_ALLOT_DEAL_NO = dt.Rows[0]["BE_ALLOT_DEAL_NO"].ToString();
                
                p.BE_ALLOT_SKSNO = dt.Rows[0]["BE_ALLOT_SKSNO"].ToString();
                p.BE_ALLOT_PATRAREGNO = dt.Rows[0]["BE_ALLOT_PATRAREGNO"].ToString();
                p.BE_ALLOT_CLIENTNAME = dt.Rows[0]["BE_ALLOT_CLIENTNAME"].ToString();

                if (!dt.Rows[0]["BE_ALLOT_DEPOSITDATE"].ToString().Equals(""))
                {
                    p.BE_ALLOT_DEPOSITDATE = DateTime.Parse(dt.Rows[0]["BE_ALLOT_DEPOSITDATE"].ToString());
                    p.BE_ALLOT_DEPOSITDATE_STR = p.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy");
                }
                if (!dt.Rows[0]["BE_ALLOT_WITHDRAWDATE"].ToString().Equals(""))
                {
                    p.BE_ALLOT_WITHDRAWDATE = DateTime.Parse(dt.Rows[0]["BE_ALLOT_WITHDRAWDATE"].ToString());
                    p.BE_ALLOT_WITHDRAWDATE_STR = p.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy");
                }

                p.BE_ALLOT_IDCARD = dt.Rows[0]["BE_ALLOT_IDCARD"].ToString();
                p.BE_ALLOT_BOOKBANK = dt.Rows[0]["BE_ALLOT_BOOKBANK"].ToString();
                p.BE_ALLOT_OTHER = dt.Rows[0]["BE_ALLOT_OTHER"].ToString();
                p.BE_OWNER = dt.Rows[0]["BE_OWNER_FLAG"].ToString();
                p.BE_SELLER_ID = dt.Rows[0]["BE_SELLER_ID"].ToString();
                p.SELLER_NAME = dt.Rows[0]["SELLER_NAME"].ToString();

                Customer c = new Customer();
                c.CustomerID = int.Parse(dt.Rows[0]["CUSTOMERID"].ToString());
                c.NameEn = dt.Rows[0]["NAMEEN"].ToString();
                c.IncVat = dt.Rows[0]["INCVAT"].ToString();
                c.TargetBank = dt.Rows[0]["BUYER_TARGETBANK"].ToString();
                c.TargetAcct = dt.Rows[0]["BUYER_TARGETACCT"].ToString();
                p.CUSTOMER = c;

                Customer sc = new Customer();
                sc.CustomerID = int.Parse(dt.Rows[0]["BE_SELLER_ID"].ToString());
                sc.NameEn = dt.Rows[0]["SELLER_NAME"].ToString();
                sc.TargetBank = dt.Rows[0]["SELLER_TARGETBANK"].ToString();
                sc.TargetAcct = dt.Rows[0]["SELLER_TARGETACCT"].ToString();
                p.CUSTSELLER = sc;

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
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }

        public static object Create(string ConnectionString, BEAllotment record)
        {

            Log.Info("-----KKB_TB_BE_ALLOTMENT Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                int day = (record.BE_MAT_DATE - record.BE_SETTLE_DATE).Days;
                decimal proceed = Math.Round(100 / (1 + (record.BE_ALLOT_YIELD / 100 * day / 365)), 6) * (record.BE_ALLOT_AMT / 100);
                decimal wht = 0;
                if (record.BE_BUYER_INCVAT == "1")
                    wht = Convert.ToDecimal(record.BE_ALLOT_AMT - proceed) * Convert.ToDecimal(0.15);
                decimal settamt = proceed + wht;
                string sql = "INSERT INTO OPICINF.KKB_TB_BE_ALLOTMENT(BE_ALLOT_NO, BE_ALLOT_REF_NO, BE_ALLOT_AMT, BE_ALLOT_CLEAN, BE_ALLOT_AI, BE_ALLOT_WHT, BE_ALLOT_SETTAMT, BE_ALLOT_UNIT, BE_ALLOT_YIELD, BE_BUYER_ID, BE_ALLOT_COMMENT, BE_ALLOT_DEAL_NO, BE_ALLOT_SKSNO, BE_ALLOT_PATRAREGNO, BE_ALLOT_CLIENTNAME, BE_SELLER_ID, BE_SELLER_ALLOT_YIELD, BE_ALLOT_DEPOSITDATE, BE_ALLOT_WITHDRAWDATE, BE_COMF_DATE, BE_SETTLE_DATE, BE_SELLER_COMF_DATE, BE_SELLER_SETTLE_DATE, BE_ALLOT_IDCARD, BE_ALLOT_BOOKBANK, BE_ALLOT_OTHER, BESERIESID, CREATE_DATE, CREATE_USER, BE_OWNER_FLAG) VALUES(:BE_ALLOT_NO, :BE_ALLOT_REF_NO, :BE_ALLOT_AMT,:BE_ALLOT_CLEAN,:BE_ALLOT_AI,:BE_ALLOT_WHT,:BE_ALLOT_SETTAMT, :BE_ALLOT_UNIT, :BE_ALLOT_YIELD, :BE_BUYER_ID, :BE_ALLOT_COMMENT, :BE_ALLOT_DEAL_NO, :BE_ALLOT_SKSNO, :BE_ALLOT_PATRAREGNO, :BE_ALLOT_CLIENTNAME, :BE_SELLER_ID, :BE_SELLER_ALLOT_YIELD, ";
                // diposit date
                if (!record.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "TO_DATE(:BE_ALLOT_DEPOSITDATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "NULL,";
                }
                // withdraw date
                if (!record.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "TO_DATE(:BE_ALLOT_WITHDRAWDATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "NULL, ";
                }
                // buyer confirm date
                if (!record.BE_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "TO_DATE(:BE_COMF_DATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "NULL, ";
                }
                // buyer settle date
                if (!record.BE_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "TO_DATE(:BE_SETTLE_DATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "NULL, ";
                }
                // seller confirm date
                if (!record.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "TO_DATE(:BE_SELLER_COMF_DATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "NULL, ";
                }
                // seller settle date
                if (!record.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "TO_DATE(:BE_SELLER_SETTLE_DATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "NULL, ";
                }
                sql += ":BE_ALLOT_IDCARD, :BE_ALLOT_BOOKBANK, :BE_ALLOT_OTHER, :BESERIESID, SYSDATE, :CREATE_USER, :BE_OWNER)";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("BE_ALLOT_NO", record.BE_ALLOT_NO);
                o.setInputParameter("BE_ALLOT_REF_NO", record.BE_ALLOT_REF_NO);
                o.setInputParameter("BE_ALLOT_AMT", record.BE_ALLOT_AMT);
                o.setInputParameter("BE_ALLOT_CLEAN", proceed);
                o.setInputParameter("BE_ALLOT_AI", "0");
                o.setInputParameter("BE_ALLOT_WHT", wht);
                o.setInputParameter("BE_ALLOT_SETTAMT", settamt);
                o.setInputParameter("BE_ALLOT_UNIT", record.BE_ALLOT_UNIT);
                o.setInputParameter("BE_ALLOT_YIELD", record.BE_ALLOT_YIELD);
                o.setInputParameter("BE_BUYER_ID", record.BE_BUYER_ID);
                o.setNCharInputParameter("BE_ALLOT_COMMENT", record.BE_ALLOT_COMMENT);
                o.setNCharInputParameter("BE_ALLOT_DEAL_NO", record.BE_ALLOT_DEAL_NO);
                o.setInputParameter("BE_ALLOT_SKSNO", record.BE_ALLOT_SKSNO);
                o.setInputParameter("BE_ALLOT_PATRAREGNO", record.BE_ALLOT_PATRAREGNO);
                o.setNCharInputParameter("BE_ALLOT_CLIENTNAME", record.BE_ALLOT_CLIENTNAME);
                o.setInputParameter("BE_SELLER_ID", record.BE_SELLER_ID);
                o.setInputParameter("BE_SELLER_ALLOT_YIELD", record.BE_SELLER_ALLOT_YIELD);
                if (!record.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                    o.setInputParameter("BE_ALLOT_DEPOSITDATE", record.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy"));
                if (!record.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                    o.setInputParameter("BE_ALLOT_WITHDRAWDATE", record.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy"));
                if (!record.BE_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_COMF_DATE", record.BE_COMF_DATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_SETTLE_DATE", record.BE_SETTLE_DATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_SELLER_COMF_DATE", record.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_SELLER_SETTLE_DATE", record.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy"));
                }
                o.setInputParameter("BE_ALLOT_IDCARD", record.BE_ALLOT_IDCARD);
                o.setInputParameter("BE_ALLOT_BOOKBANK", record.BE_ALLOT_BOOKBANK);
                o.setNCharInputParameter("BE_ALLOT_OTHER", record.BE_ALLOT_OTHER);
                o.setInputParameter("BESERIESID", record.BESERIESID);
                o.setInputParameter("CREATE_USER", record.CREATE_USER);
                o.setInputParameter("BE_OWNER", record.BE_OWNER);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_BE_ALLOTMENT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }

        public static object Update(string ConnectionString, BEAllotment record)
        {
            
            Log.Info("-----KKB_TB_BE_ALLOTMENT Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                int day = (record.BE_MAT_DATE - record.BE_SETTLE_DATE).Days;
                decimal proceed = Math.Round(100 / (1 + (record.BE_ALLOT_YIELD / 100 * day / 365)), 6) * (record.BE_ALLOT_AMT / 100);
                decimal wht = 0;
                if (record.BE_BUYER_INCVAT == "1")
                    wht = Convert.ToDecimal(record.BE_ALLOT_AMT - proceed) * Convert.ToDecimal(0.15);
                decimal settamt = proceed + wht;
                //String sql = "Update OPICINF.KKB_TB_BE_ALLOTMENT SET BE_ALLOT_NO=:BE_ALLOT_NO, BE_ALLOT_REF_NO=:BE_ALLOT_REF_NO, BE_ALLOT_AMT=:BE_ALLOT_AMT, BE_ALLOT_CLEAN=:BE_ALLOT_CLEAN, BE_ALLOT_AI=:BE_ALLOT_AI, BE_ALLOT_WHT=:BE_ALLOT_WHT, BE_ALLOT_SETTAMT=:BE_ALLOT_SETTAMT, BE_COMF_DATE=TO_DATE(:BE_COMF_DATE,'DD/MM/YYYY'), BE_SETTLE_DATE=TO_DATE(:BE_SETTLE_DATE,'DD/MM/YYYY'), BE_ALLOT_UNIT=:BE_ALLOT_UNIT, BE_ALLOT_YIELD=:BE_ALLOT_YIELD, BE_BUYER_ID=:BE_BUYER_ID, BE_ALLOT_COMMENT=:BE_ALLOT_COMMENT, FLAG=:FLAG, BE_ALLOT_DEAL_NO=:BE_ALLOT_DEAL_NO,BE_ALLOT_SKSNO=:BE_ALLOT_SKSNO, BE_ALLOT_PATRAREGNO=:BE_ALLOT_PATRAREGNO, BE_ALLOT_CLIENTNAME=:BE_ALLOT_CLIENTNAME, BE_SELLER_ID = :BE_SELLER_ID, ";
                String sql = "Update OPICINF.KKB_TB_BE_ALLOTMENT SET BE_ALLOT_NO=:BE_ALLOT_NO, BE_ALLOT_REF_NO=:BE_ALLOT_REF_NO, BE_ALLOT_AMT=:BE_ALLOT_AMT, BE_ALLOT_CLEAN=:BE_ALLOT_CLEAN, BE_ALLOT_AI=:BE_ALLOT_AI, BE_ALLOT_WHT=:BE_ALLOT_WHT, BE_ALLOT_SETTAMT=:BE_ALLOT_SETTAMT, BE_ALLOT_UNIT=:BE_ALLOT_UNIT, BE_ALLOT_YIELD=:BE_ALLOT_YIELD, BE_SELLER_ALLOT_YIELD=:BE_SELLER_ALLOT_YIELD, BE_BUYER_ID=:BE_BUYER_ID, BE_ALLOT_COMMENT=:BE_ALLOT_COMMENT, FLAG=:FLAG, BE_ALLOT_DEAL_NO=:BE_ALLOT_DEAL_NO,BE_ALLOT_SKSNO=:BE_ALLOT_SKSNO, BE_ALLOT_PATRAREGNO=:BE_ALLOT_PATRAREGNO, BE_ALLOT_CLIENTNAME=:BE_ALLOT_CLIENTNAME, BE_SELLER_ID = :BE_SELLER_ID, ";
                if (!record.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "BE_ALLOT_DEPOSITDATE=TO_DATE(:BE_ALLOT_DEPOSITDATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "BE_ALLOT_DEPOSITDATE=NULL, ";
                }

                if (!record.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "BE_ALLOT_WITHDRAWDATE=TO_DATE(:BE_ALLOT_WITHDRAWDATE,'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "BE_ALLOT_WITHDRAWDATE=NULL, ";
                }

                if (!record.BE_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "BE_COMF_DATE=TO_DATE(:BE_COMF_DATE, 'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "BE_COMF_DATE=NULL, ";
                }

                if (!record.BE_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "BE_SETTLE_DATE=TO_DATE(:BE_SETTLE_DATE, 'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "BE_SETTLE_DATE=NULL, ";
                }

                if (!record.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "BE_SELLER_COMF_DATE=TO_DATE(:BE_SELLER_COMF_DATE, 'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "BE_SELLER_COMF_DATE=NULL, ";
                }

                if (!record.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    sql += "BE_SELLER_SETTLE_DATE=TO_DATE(:BE_SELLER_SETTLE_DATE, 'DD/MM/YYYY'), ";
                }
                else
                {
                    sql += "BE_SELLER_SETTLE_DATE=NULL, ";
                }
                    
                sql += "BE_ALLOT_IDCARD=:BE_ALLOT_IDCARD, BE_ALLOT_BOOKBANK=:BE_ALLOT_BOOKBANK, BE_ALLOT_OTHER=:BE_ALLOT_OTHER, UPDATE_DATE=SYSDATE, UPDATE_USER=:UPDATE_USER, BE_OWNER_FLAG=:BE_OWNER_FLAG Where BEALLOTMENTID=:BEALLOTMENTID ";

                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("BE_ALLOT_NO", record.BE_ALLOT_NO);
                o.setInputParameter("BE_ALLOT_REF_NO", record.BE_ALLOT_REF_NO);
                o.setInputParameter("BE_ALLOT_AMT", record.BE_ALLOT_AMT);
                o.setInputParameter("BE_ALLOT_CLEAN", proceed);
                o.setInputParameter("BE_ALLOT_AI", "0");
                o.setInputParameter("BE_ALLOT_WHT", wht);
                o.setInputParameter("BE_ALLOT_SETTAMT", settamt);
                //o.setInputParameter("BE_COMF_DATE", record.BE_COMF_DATE.ToString("dd/MM/yyyy"));
                //o.setInputParameter("BE_SETTLE_DATE", record.BE_SETTLE_DATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("BE_ALLOT_UNIT", record.BE_ALLOT_UNIT);
                o.setInputParameter("BE_ALLOT_YIELD", record.BE_ALLOT_YIELD);
                o.setInputParameter("BE_SELLER_ALLOT_YIELD", record.BE_SELLER_ALLOT_YIELD);
                o.setInputParameter("BE_BUYER_ID", record.BE_BUYER_ID);
                o.setNCharInputParameter("BE_ALLOT_COMMENT", record.BE_ALLOT_COMMENT);
                o.setInputParameter("FLAG", record.BE_FLAG);
                o.setNCharInputParameter("BE_ALLOT_DEAL_NO", record.BE_ALLOT_DEAL_NO);
                o.setInputParameter("BE_ALLOT_SKSNO", record.BE_ALLOT_SKSNO);
                o.setInputParameter("BE_ALLOT_PATRAREGNO", record.BE_ALLOT_PATRAREGNO);
                o.setNCharInputParameter("BE_ALLOT_CLIENTNAME", record.BE_ALLOT_CLIENTNAME);
                o.setInputParameter("BE_SELLER_ID", record.BE_SELLER_ID);
                if (!record.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_ALLOT_DEPOSITDATE", record.BE_ALLOT_DEPOSITDATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_ALLOT_WITHDRAWDATE", record.BE_ALLOT_WITHDRAWDATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_COMF_DATE", record.BE_COMF_DATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_SETTLE_DATE", record.BE_SETTLE_DATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_SELLER_COMF_DATE", record.BE_SELLER_COMF_DATE.ToString("dd/MM/yyyy"));
                }
                if (!record.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy").Equals("01/01/0001"))
                {
                    o.setInputParameter("BE_SELLER_SETTLE_DATE", record.BE_SELLER_SETTLE_DATE.ToString("dd/MM/yyyy"));
                }
                o.setInputParameter("BE_ALLOT_IDCARD", record.BE_ALLOT_IDCARD);
                o.setInputParameter("BE_ALLOT_BOOKBANK", record.BE_ALLOT_BOOKBANK);
                o.setNCharInputParameter("BE_ALLOT_OTHER", record.BE_ALLOT_OTHER);
                o.setInputParameter("UPDATE_USER", record.UPDATE_USER);
                o.setInputParameter("BE_OWNER_FLAG", record.BE_OWNER);
                o.setInputParameter("BEALLOTMENTID", record.BEALLOTMENTID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_BE_ALLOTMENT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }

        public static object Delete(string ConnectionString, int BEAllotmentID)
        {

            Log.Info("-----KKB_TB_BE_ALLOTMENT Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("SELECT BE_ALLOT_REF_NO FROM OPICINF.KKB_TB_BE_ALLOTMENT WHERE BEALLOTMENTID=" + BEAllotmentID.ToString(), CommandType.Text);
                string refno = o.executeScalar().ToString().Trim();

                string sql = "";
                if (refno.Equals(""))
                {
                    sql = "DELETE FROM OPICINF.KKB_TB_BE_ALLOTMENT WHERE BEALLOTMENTID='" + BEAllotmentID.ToString() + "'";
                    o.createCommand(sql, CommandType.Text);
                    row = o.executeNonQuery();
                    if (row > 0)
                        return new { Result = "OK" };
                    else
                        return new { Result = "ERROR", Message = "Could not Delete data." };
                }
                else
                    return new { Result = "ERROR", Message = "Could not Delete data.Because The RefNo is not blank." };
                
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_BE_ALLOTMENT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }


        public static int UpdTemplateType(string ConnectionString, int BESeriesID, string type)
        {

            Log.Info("-----KKB_TB_BE_ALLOTMENT UpdTemplateType-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                string sql = "Update OPICINF.KKB_TB_BE_SERIES SET TICKET_TEMPLATE = :TICKET_TEMPLATE WHERE BESERIESID = :BESERIESID ";
                
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("TICKET_TEMPLATE", type);
                o.setInputParameter("BESERIESID", BESeriesID);
               
                row = o.executeNonQuery();
                
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_BE_ALLOTMENT Error-----");
                Log.Error(ex.Message);
                //return new { Result = "ERROR", Message = ex.Message };
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }


        public static object RunningRefNo(string ConnectionString, int BESeriesID)
        {
            Log.Info("-----KKB_TB_BE_ALLOTMENT Start RunningRefNo-----");
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                o.createCommand("OPICINF.KKB_SP_TSS_TR136BOBESERIES", CommandType.StoredProcedure);
                o.setInputParameter("p_category","");
                o.setInputParameter("p_series", BESeriesID);
                o.setInputParameter("p_report", "Running-RefNo");
                o.setInputParameter("p_mdate", "");
                DataSet s = o.executeReader("ref_rpt_cur", "RunningRefNo");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                        return new { Result = "OK" };
                    else
                        return new { Result = "ERROR", Message = "Could not Running RefNo" };
                }
                else
                    return new { Result = "ERROR", Message = "Could not Running RefNo" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Running RefNo KKB_TB_BE_ALLOTMENT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }

        public static object RunningRefNo2(string ConnectionString, int BESeriesID)
        {
            Log.Info("-----KKB_TB_BE_ALLOTMENT Start RunningRefNo-----");
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                o.createCommand("OPICINF.KKB_SP_TSS_TR136BOBESERIES", CommandType.StoredProcedure);
                o.setInputParameter("p_category", "");
                o.setInputParameter("p_series", BESeriesID);
                o.setInputParameter("p_report", "Running-RefNo2");
                o.setInputParameter("p_mdate", "");
                DataSet s = o.executeReader("ref_rpt_cur", "RunningRefNo");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                        return new { Result = "OK" };
                    else
                        return new { Result = "ERROR", Message = "Could not Running RefNo" };
                }
                else
                    return new { Result = "ERROR", Message = "Could not Running RefNo" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Running RefNo KKB_TB_BE_ALLOTMENT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_BE_ALLOTMENT Finish-----");
                o.closeConnection();
            }
        }
    }
}
