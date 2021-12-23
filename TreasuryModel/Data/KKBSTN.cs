using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Collections;
using System.Data;
using log4net;

/*
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
 */


namespace TreasuryModel.Data
{
    public class KKBSTN
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(KKBSTN));

        public int STNID { get; set; }
        public string ACTION { get; set; }
        public string CURR_DATE { get; set; }
        public string ORDER_NO { get; set; }
        public string ACCT_NO { get; set; }
        public string SYMBOL { get; set; }
        public string KKSYMBOL { get; set; }
        public decimal UNIT { get; set; }
        public decimal AMOUNT { get; set; }
        public string TITLE_NAME_TH { get; set; }
        public string FIRST_NAME_TH { get; set; }
        public string LAST_NAME_TH { get; set; }
        public string TITLE_NAME_EN { get; set; }
        public string FIRST_NAME_EN { get; set; }
        public string LAST_NAME_EN { get; set; }
        public string TAXID { get; set; }
        public string CUST_BANK_CODE { get; set; }
        public string CUST_BANK_ACCT { get; set; }
        public string TEL { get; set; }
        public string EMAIL { get; set; }
        public string FC_NAME { get; set; }
        public string FC_EMAIL { get; set; }
        public string STN_TYPE { get; set; }
        public string NOTE_SERIES { get; set; }
        public string UNDERLYING1 { get; set; }
        public string UNDERLYING2 { get; set; }
        public string UNDERLYING3 { get; set; }
        public string UNDERLYING4 { get; set; }
        public string UNDERLYING5 { get; set; }
        public string UNDERLYING6 { get; set; }
        /*
        public decimal STRIKE_PRICE1 { get; set; }
        public decimal STRIKE_PRICE2 { get; set; }
        public decimal STRIKE_PRICE3 { get; set; }
        public decimal STRIKE_PRICE4 { get; set; }
        public decimal STRIKE_PRICE5 { get; set; }
        public decimal STRIKE_PRICE6 { get; set; }        
        public decimal BARRIER_PRICE1 { get; set; }
        public decimal BARRIER_PRICE2 { get; set; }
        public decimal BARRIER_PRICE3 { get; set; }
        public decimal BARRIER_PRICE4 { get; set; }
        public decimal BARRIER_PRICE5 { get; set; }
        public decimal BARRIER_PRICE6 { get; set; }
        */
        public string STRIKE_PRICE1 { get; set; }
        public string STRIKE_PRICE2 { get; set; }
        public string STRIKE_PRICE3 { get; set; }
        public string STRIKE_PRICE4 { get; set; }
        public string STRIKE_PRICE5 { get; set; }
        public string STRIKE_PRICE6 { get; set; }
        public string BARRIER_PRICE1 { get; set; }
        public string BARRIER_PRICE2 { get; set; }
        public string BARRIER_PRICE3 { get; set; }
        public string BARRIER_PRICE4 { get; set; }
        public string BARRIER_PRICE5 { get; set; }
        public string BARRIER_PRICE6 { get; set; }

        public decimal KI_PRICE { get; set; }
        /******/
        public decimal PROTECT_LEVELP { get; set; }
        public int TTM { get; set; }
        public int TRADING_DAY { get; set; }
        public string EXERCISE_TYPE { get; set; }
        public decimal PAR_VALUE { get; set; }
        public decimal OFFER_PRICEP { get; set; }
        public decimal OFFER_PRICE_AMT { get; set; }
        public decimal INITIAL_PRICEP { get; set; }
        //public decimal INITIAL_PRICE_AMT { get; set; }
        public string INITIAL_PRICE_AMT { get; set; }
        public decimal STRIKE_PRICE1P { get; set; }
        public decimal STRIKE_PRICE2P { get; set; }
        public decimal STRIKE_PRICE3P { get; set; }
        public decimal STRIKE_PRICE4P { get; set; }
        public decimal STRIKE_PRICE5P { get; set; }
        public decimal STRIKE_PRICE6P { get; set; }
        public decimal BARRIER_PRICE1P { get; set; }
        public decimal BARRIER_PRICE2P { get; set; }
        public decimal BARRIER_PRICE3P { get; set; }
        public decimal BARRIER_PRICE4P { get; set; }
        public decimal BARRIER_PRICE5P { get; set; }
        public decimal BARRIER_PRICE6P { get; set; }

        /*** 20210319**/
        public decimal KK_PRICEP { get; set; }
        /******/
        public decimal PR1 { get; set; }
        public decimal PR2 { get; set; }
        public decimal MIN_YIELD1P { get; set; }
        public decimal MIN_YIELD2P { get; set; }
        public decimal MAX_YIELD1P { get; set; }
        public decimal MAX_YIELD2P { get; set; }
        public decimal REBATE_AMT { get; set; }
        public decimal REBATEP { get; set; }
        public string NOTE_CURR { get; set; }
        public string UNDERLYING_CURR { get; set; }
        //public decimal FX_DELIVERY_AMT { get; set; }
        //public decimal SURPLUS_CASH { get; set; }
        public string FX_DELIVERY_AMT { get; set; }
        public string SURPLUS_CASH { get; set; }
        public string SUB_BEGIN_DATE { get; set; }
        public string SUB_END_DATE { get; set; }
        public string TRADE_DATE { get; set; }
        public string EFFECTIVE_DATE { get; set; }
        public string SETTLE_DATE { get; set; }
        public string ISSUE_DATE { get; set; }
        public string FIXING_DATE { get; set; }
        public string MATURITY_DATE { get; set; }
        public string OBS_DATE1 { get; set; }
        public string OBS_DATE2 { get; set; }
        public string OBS_DATE3 { get; set; }
        public string OBS_DATE4 { get; set; }
        public string OBS_DATE5 { get; set; }
        public string OBS_DATE6 { get; set; }
        public string OBS_DATE7 { get; set; }
        public string OBS_DATE8 { get; set; }
        public string OBS_DATE9 { get; set; }
        public string OBS_DATE10 { get; set; }
        public string OBS_DATE11 { get; set; }
        public string OBS_DATE12 { get; set; }
        public string OBS_DATE13 { get; set; }
        public string OBS_DATE14 { get; set; }
        public string OBS_DATE15 { get; set; }
        public string OBS_DATE16 { get; set; }
        public string OBS_DATE17 { get; set; }
        public string OBS_DATE18 { get; set; }
        public string OBS_DATE19 { get; set; }
        public string OBS_DATE20 { get; set; }
        public int SETTLE_PERIOD { get; set; }
        public decimal EXCRATE_TRADEDATE { get; set; }

        /*** 20210319**/
        public string EXCHANGE1 { get; set; }
        public string EXCHANGE2 { get; set; }
        public string EXCHANGE3 { get; set; }
        /***************/

        /***** 20210405 ***/
        public string CUST_TYPE { get; set; }
        public string PAY_TYPE { get; set; }

        /******************/
        public string CUST_TYPE2 { get; set; }
        public string FULLNAME { get; set; }

        /***** 20210527 ***/
        public decimal UNWIND_PRICE { get; set; }
        public string UNWIND_DEALDATE { get; set; }
        public string UNWIND_PAYDATE { get; set; }


        public static bool AddSTN(List<KKBSTN> STNList)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            bool issuccess = false;
            bool flagerror = false;
            try
            {
               
                o.openConnection();
                
                o.beginTransaction();



                foreach (KKBSTN STN in STNList)
                {
                    try
                    {
                        string sql = "INSERT INTO OPICINF.KKB_TB_STN(ACTION,CURR_DATE,ORDER_NO,ACCT_NO,SYMBOL,KKSYMBOL,UNIT,AMOUNT,TITLE_NAME_TH,FIRST_NAME_TH,LAST_NAME_TH, "
                             
                                    + " TITLE_NAME_EN,FIRST_NAME_EN,LAST_NAME_EN,TAXID,CUST_BANK_CODE,CUST_BANK_ACCT,TEL,EMAIL,FC_NAME,FC_EMAIL,STN_TYPE,NOTE_SERIES, "
                                    + " UNDERLYING1,UNDERLYING2,UNDERLYING3,UNDERLYING4,UNDERLYING5,UNDERLYING6, " 
                                    + " STRIKE_PRICE1,STRIKE_PRICE2,STRIKE_PRICE3,STRIKE_PRICE4, STRIKE_PRICE5,STRIKE_PRICE6, "
                                    + " BARRIER_PRICE1,BARRIER_PRICE2,BARRIER_PRICE3,BARRIER_PRICE4,BARRIER_PRICE5,BARRIER_PRICE6,KI_PRICE, "
                                    + " PROTECT_LEVELP,TTM,TRADING_DAY,EXERCISE_TYPE,PAR_VALUE,OFFER_PRICEP,OFFER_PRICE_AMT,INITIAL_PRICEP,INITIAL_PRICE_AMT, "
                                    + " STRIKE_PRICE1P,STRIKE_PRICE2P,STRIKE_PRICE3P,STRIKE_PRICE4P,STRIKE_PRICE5P,STRIKE_PRICE6P,BARRIER_PRICE1P,BARRIER_PRICE2P,BARRIER_PRICE3P,BARRIER_PRICE4P, "
                                    + " BARRIER_PRICE5P,BARRIER_PRICE6P,KK_PRICEP,PR1,PR2,MIN_YIELD1P,MIN_YIELD2P,MAX_YIELD1P,MAX_YIELD2P,REBATE_AMT,REBATEP,NOTE_CURR,UNDERLYING_CURR,FX_DELIVERY_AMT,SURPLUS_CASH, "
                                    + " SUB_BEGIN_DATE,SUB_END_DATE,TRADE_DATE,EFFECTIVE_DATE,SETTLE_DATE,ISSUE_DATE,FIXING_DATE,MATURITY_DATE,OBS_DATE1,OBS_DATE2,OBS_DATE3,OBS_DATE4, "
                                    + " OBS_DATE5,OBS_DATE6,OBS_DATE7,OBS_DATE8,OBS_DATE9,OBS_DATE10,OBS_DATE11,OBS_DATE12,OBS_DATE13,OBS_DATE14,OBS_DATE15,OBS_DATE16,OBS_DATE17,OBS_DATE18,OBS_DATE19,OBS_DATE20, "
                                    + " SETTLE_PERIOD,EXCRATE_TRADEDATE,EXCHANGE1,EXCHANGE2,EXCHANGE3,CUST_TYPE, PAY_TYPE) "; 
                        sql += " VALUES(:ACTION, TO_DATE(:CURR_DATE,'DD/MM/YYYY'), :ORDER_NO, :ACCT_NO, :SYMBOL, :KKSYMBOL, :UNIT, :AMOUNT, :TITLE_NAME_TH, :FIRST_NAME_TH, :LAST_NAME_TH "
                        + " , :TITLE_NAME_EN, :FIRST_NAME_EN, :LAST_NAME_EN, :TAXID, :CUST_BANK_CODE, :CUST_BANK_ACCT, :TEL, :EMAIL, :FC_NAME, :FC_EMAIL, :STN_TYPE, :NOTE_SERIES, :UNDERLYING1, :UNDERLYING2, :UNDERLYING3, :UNDERLYING4, :UNDERLYING5, :UNDERLYING6 "
                        + " , :STRIKE_PRICE1, :STRIKE_PRICE2, :STRIKE_PRICE3, :STRIKE_PRICE4, :STRIKE_PRICE5, :STRIKE_PRICE6 "
                        + " , :BARRIER_PRICE1,:BARRIER_PRICE2,:BARRIER_PRICE3,:BARRIER_PRICE4,:BARRIER_PRICE5,:BARRIER_PRICE6,:KI_PRICE "
                        + " , :PROTECT_LEVELP, :TTM, :TRADING_DAY, :EXERCISE_TYPE, :PAR_VALUE, :OFFER_PRICEP, :OFFER_PRICE_AMT, :INITIAL_PRICEP, :INITIAL_PRICE_AMT "
                        + " , :STRIKE_PRICE1P, :STRIKE_PRICE2P, :STRIKE_PRICE3P, :STRIKE_PRICE4P, :STRIKE_PRICE5P, :STRIKE_PRICE6P, :BARRIER_PRICE1P, :BARRIER_PRICE2P, :BARRIER_PRICE3P, :BARRIER_PRICE4P, :BARRIER_PRICE5P, :BARRIER_PRICE6P, :KK_PRICEP,:PR1, :PR2, :MIN_YIELD1P "
                        + " , :MIN_YIELD2P, :MAX_YIELD1P, :MAX_YIELD2P, :REBATE_AMT, :REBATEP, :NOTE_CURR, :UNDERLYING_CURR, :FX_DELIVERY_AMT, :SURPLUS_CASH "
                        + " , TO_DATE(:SUB_BEGIN_DATE,'DD/MM/YYYY'), TO_DATE(:SUB_END_DATE,'DD/MM/YYYY'), TO_DATE(:TRADE_DATE,'DD/MM/YYYY'), TO_DATE(:EFFECTIVE_DATE,'DD/MM/YYYY'), TO_DATE(:SETTLE_DATE,'DD/MM/YYYY'), TO_DATE(:ISSUE_DATE,'DD/MM/YYYY'), TO_DATE(:FIXING_DATE,'DD/MM/YYYY') "
                        + " , TO_DATE(:MATURITY_DATE,'DD/MM/YYYY'), TO_DATE(:OBS_DATE1,'DD/MM/YYYY'), TO_DATE(:OBS_DATE2,'DD/MM/YYYY'), TO_DATE(:OBS_DATE3,'DD/MM/YYYY'), TO_DATE(:OBS_DATE4,'DD/MM/YYYY') "
                        + " , TO_DATE(:OBS_DATE5,'DD/MM/YYYY'), TO_DATE(:OBS_DATE6,'DD/MM/YYYY'), TO_DATE(:OBS_DATE7,'DD/MM/YYYY'), TO_DATE(:OBS_DATE8,'DD/MM/YYYY'), TO_DATE(:OBS_DATE9,'DD/MM/YYYY'), TO_DATE(:OBS_DATE10,'DD/MM/YYYY') "
                        + " , TO_DATE(:OBS_DATE11,'DD/MM/YYYY'), TO_DATE(:OBS_DATE12,'DD/MM/YYYY'), TO_DATE(:OBS_DATE13,'DD/MM/YYYY'), TO_DATE(:OBS_DATE14,'DD/MM/YYYY'), TO_DATE(:OBS_DATE15,'DD/MM/YYYY'), TO_DATE(:OBS_DATE16,'DD/MM/YYYY'), TO_DATE(:OBS_DATE17,'DD/MM/YYYY') "
                        + " , TO_DATE(:OBS_DATE18,'DD/MM/YYYY'), TO_DATE(:OBS_DATE19,'DD/MM/YYYY'), TO_DATE(:OBS_DATE20,'DD/MM/YYYY'), :SETTLE_PERIOD, :EXCRATE_TRADEDATE, :EXCHANGE1, :EXCHANGE2, :EXCHANGE3 ,:CUST_TYPE, :PAY_TYPE"
                        + " ) RETURNING STNID INTO :STNID"; //
   

                        
                        o.createCommand(sql, CommandType.Text);

                        o.setVarCharInputParameter("ACTION", STN.ACTION);
                         o.setVarCharInputParameter("CURR_DATE", string.Format("{0:DD/MM/yyyy}", STN.CURR_DATE));
                         o.setVarCharInputParameter("ORDER_NO", STN.ORDER_NO);
                         o.setVarCharInputParameter("ACCT_NO", STN.ACCT_NO);
                         o.setVarCharInputParameter("SYMBOL", STN.SYMBOL);
                         o.setVarCharInputParameter("KKSYMBOL", STN.KKSYMBOL);
                         o.setDecimalInputParameter("UNIT", STN.UNIT);
                         o.setDecimalInputParameter("AMOUNT", STN.AMOUNT);
                         o.setNCharInputParameter("TITLE_NAME_TH", STN.TITLE_NAME_TH);
                         o.setNCharInputParameter("FIRST_NAME_TH", STN.FIRST_NAME_TH);
                         o.setNCharInputParameter("LAST_NAME_TH", STN.LAST_NAME_TH);
                         o.setVarCharInputParameter("TITLE_NAME_EN", STN.TITLE_NAME_EN);
                         o.setVarCharInputParameter("FIRST_NAME_EN", STN.FIRST_NAME_EN);
                         o.setVarCharInputParameter("LAST_NAME_EN", STN.LAST_NAME_EN);
                         o.setVarCharInputParameter("TAXID", STN.TAXID);
                         o.setVarCharInputParameter("CUST_BANK_CODE", STN.CUST_BANK_CODE);
                         o.setVarCharInputParameter("CUST_BANK_ACCT", STN.CUST_BANK_ACCT);
                         o.setVarCharInputParameter("TEL", STN.TEL);
                         o.setVarCharInputParameter("EMAIL", STN.EMAIL);
                         o.setVarCharInputParameter("FC_NAME", STN.FC_NAME);
                         o.setVarCharInputParameter("FC_EMAIL", STN.FC_EMAIL);
                         o.setVarCharInputParameter("STN_TYPE", STN.STN_TYPE);
                         o.setVarCharInputParameter("NOTE_SERIES", STN.NOTE_SERIES);
                         o.setVarCharInputParameter("UNDERLYING1", STN.UNDERLYING1);
                         o.setVarCharInputParameter("UNDERLYING2", STN.UNDERLYING2);
                         o.setVarCharInputParameter("UNDERLYING3", STN.UNDERLYING3);
                         o.setVarCharInputParameter("UNDERLYING4", STN.UNDERLYING4);
                         o.setVarCharInputParameter("UNDERLYING5", STN.UNDERLYING5);
                         o.setVarCharInputParameter("UNDERLYING6", STN.UNDERLYING6);
                         o.setVarCharInputParameter("STRIKE_PRICE1", STN.STRIKE_PRICE1);
                         o.setVarCharInputParameter("STRIKE_PRICE2", STN.STRIKE_PRICE2);
                         o.setVarCharInputParameter("STRIKE_PRICE3", STN.STRIKE_PRICE3);
                         o.setVarCharInputParameter("STRIKE_PRICE4", STN.STRIKE_PRICE4);
                         o.setVarCharInputParameter("STRIKE_PRICE5", STN.STRIKE_PRICE5);
                         o.setVarCharInputParameter("STRIKE_PRICE6", STN.STRIKE_PRICE6);

                         o.setVarCharInputParameter("BARRIER_PRICE1", STN.BARRIER_PRICE1);
                         o.setVarCharInputParameter("BARRIER_PRICE2", STN.BARRIER_PRICE2);
                         o.setVarCharInputParameter("BARRIER_PRICE3", STN.BARRIER_PRICE3);
                         o.setVarCharInputParameter("BARRIER_PRICE4", STN.BARRIER_PRICE4);
                         o.setVarCharInputParameter("BARRIER_PRICE5", STN.BARRIER_PRICE5);
                         o.setVarCharInputParameter("BARRIER_PRICE6", STN.BARRIER_PRICE6);
                         o.setDecimalInputParameter("KI_PRICE", STN.KI_PRICE);

                         o.setDecimalInputParameter("PROTECT_LEVELP", STN.PROTECT_LEVELP);
                         o.setIntegerInputParameter("TTM", STN.TTM);
                         o.setIntegerInputParameter("TRADING_DAY", STN.TRADING_DAY);
                         o.setVarCharInputParameter("EXERCISE_TYPE", STN.EXERCISE_TYPE);
                         o.setDecimalInputParameter("PAR_VALUE", STN.PAR_VALUE);
                         o.setDecimalInputParameter("OFFER_PRICEP", STN.OFFER_PRICEP);
                         o.setDecimalInputParameter("OFFER_PRICE_AMT", STN.OFFER_PRICE_AMT);
                         o.setDecimalInputParameter("INITIAL_PRICEP", STN.INITIAL_PRICEP);
                         o.setVarCharInputParameter("INITIAL_PRICE_AMT", STN.INITIAL_PRICE_AMT);
                         o.setDecimalInputParameter("STRIKE_PRICE1P", STN.STRIKE_PRICE1P);
                         o.setDecimalInputParameter("STRIKE_PRICE2P", STN.STRIKE_PRICE2P);
                         o.setDecimalInputParameter("STRIKE_PRICE3P", STN.STRIKE_PRICE3P);
                         o.setDecimalInputParameter("STRIKE_PRICE4P", STN.STRIKE_PRICE4P);
                         o.setDecimalInputParameter("STRIKE_PRICE5P", STN.STRIKE_PRICE5P);
                         o.setDecimalInputParameter("STRIKE_PRICE6P", STN.STRIKE_PRICE6P);
                         o.setDecimalInputParameter("BARRIER_PRICE1P", STN.BARRIER_PRICE1P);
                         o.setDecimalInputParameter("BARRIER_PRICE2P", STN.BARRIER_PRICE2P);
                         o.setDecimalInputParameter("BARRIER_PRICE3P", STN.BARRIER_PRICE3P);
                         o.setDecimalInputParameter("BARRIER_PRICE4P", STN.BARRIER_PRICE4P);
                         o.setDecimalInputParameter("BARRIER_PRICE5P", STN.BARRIER_PRICE5P);
                         o.setDecimalInputParameter("BARRIER_PRICE6P", STN.BARRIER_PRICE6P);
                         o.setDecimalInputParameter("KK_PRICEP", STN.KK_PRICEP);

                         o.setDecimalInputParameter("PR1", STN.PR1);
                         o.setDecimalInputParameter("PR2", STN.PR2);
                         o.setDecimalInputParameter("MIN_YIELD1P", STN.MIN_YIELD1P);
                         o.setDecimalInputParameter("MIN_YIELD2P", STN.MIN_YIELD2P);
                         o.setDecimalInputParameter("MAX_YIELD1P", STN.MAX_YIELD1P);
                         o.setDecimalInputParameter("MAX_YIELD2P", STN.MAX_YIELD2P);
                         o.setDecimalInputParameter("REBATE_AMT", STN.REBATE_AMT);
                         o.setDecimalInputParameter("REBATEP", STN.REBATEP);
                         o.setVarCharInputParameter("NOTE_CURR", STN.NOTE_CURR);
                         o.setVarCharInputParameter("UNDERLYING_CURR", STN.UNDERLYING_CURR);
                         o.setVarCharInputParameter("FX_DELIVERY_AMT", STN.FX_DELIVERY_AMT);
                         o.setVarCharInputParameter("SURPLUS_CASH", STN.SURPLUS_CASH);
                         o.setVarCharInputParameter("SUB_BEGIN_DATE", string.Format("{0:DD/MM/yyyy}", STN.SUB_BEGIN_DATE));
                         o.setVarCharInputParameter("SUB_END_DATE", string.Format("{0:DD/MM/yyyy}", STN.SUB_END_DATE));
                         o.setVarCharInputParameter("TRADE_DATE", string.Format("{0:DD/MM/yyyy}", STN.TRADE_DATE));
                         o.setVarCharInputParameter("EFFECTIVE_DATE", string.Format("{0:DD/MM/yyyy}", STN.EFFECTIVE_DATE));
                         o.setVarCharInputParameter("SETTLE_DATE", string.Format("{0:DD/MM/yyyy}", STN.SETTLE_DATE));
                         o.setVarCharInputParameter("ISSUE_DATE", string.Format("{0:DD/MM/yyyy}", STN.ISSUE_DATE));
                         o.setVarCharInputParameter("FIXING_DATE", string.Format("{0:DD/MM/yyyy}", STN.FIXING_DATE));
                         o.setVarCharInputParameter("MATURITY_DATE", string.Format("{0:DD/MM/yyyy}", STN.MATURITY_DATE));
                         o.setVarCharInputParameter("OBS_DATE1", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE1));
                         o.setVarCharInputParameter("OBS_DATE2", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE2));
                         o.setVarCharInputParameter("OBS_DATE3", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE3));
                         o.setVarCharInputParameter("OBS_DATE4", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE4));
                         o.setVarCharInputParameter("OBS_DATE5", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE5));
                         o.setVarCharInputParameter("OBS_DATE6", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE6));
                         o.setVarCharInputParameter("OBS_DATE7", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE7));
                         o.setVarCharInputParameter("OBS_DATE8", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE8));
                         o.setVarCharInputParameter("OBS_DATE9", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE9));
                         o.setVarCharInputParameter("OBS_DATE10", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE10));
                         o.setVarCharInputParameter("OBS_DATE11", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE11));
                         o.setVarCharInputParameter("OBS_DATE12", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE12));
                         o.setVarCharInputParameter("OBS_DATE13", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE13));
                         o.setVarCharInputParameter("OBS_DATE14", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE14));
                         o.setVarCharInputParameter("OBS_DATE15", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE15));
                         o.setVarCharInputParameter("OBS_DATE16", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE16));
                         o.setVarCharInputParameter("OBS_DATE17", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE17));
                         o.setVarCharInputParameter("OBS_DATE18", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE18));
                         o.setVarCharInputParameter("OBS_DATE19", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE19));
                         o.setVarCharInputParameter("OBS_DATE20", string.Format("{0:DD/MM/yyyy}", STN.OBS_DATE20));
                         o.setIntegerInputParameter("SETTLE_PERIOD", STN.SETTLE_PERIOD);
                         o.setDecimalInputParameter("EXCRATE_TRADEDATE", STN.EXCRATE_TRADEDATE);
                         o.setVarCharInputParameter("EXCHANGE1", STN.EXCHANGE1);
                         o.setVarCharInputParameter("EXCHANGE2", STN.EXCHANGE2);
                         o.setVarCharInputParameter("EXCHANGE3", STN.EXCHANGE3);
                         o.setNCharInputParameter("CUST_TYPE", STN.CUST_TYPE);
                         o.setNCharInputParameter("PAY_TYPE", STN.PAY_TYPE);
                     


                        int result = o.executeNonQuery("STNID");


                        if (result <= 0)
                        {
                            flagerror = true;

                        }
                        else
                        {


                            STN.STNID = result;

                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("----Start Error-----");
                        Log.Error(ex.Message);

                    }
                }

                if (flagerror)
                {
                    o.rollbackTransaction();
                }
                else
                {
                    o.commitTransaction();
                    issuccess = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                o.closeConnection();
            }
            return issuccess;
        }

        public static bool havekkSymbol(string kksymbol)
        {
            bool ishave = false;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_STN WHERE KKSYMBOL = '" + kksymbol + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KKBSTN");

                if(dt.Rows.Count > 0){

                    ishave = true;
                }

            }
            catch (Exception ex)
            {
                return false ;
            }

            return ishave;
        }

        //removeRecordBySymbol
        public static Boolean removeRecordBykkSymbol(string kksymbol)
        {

            Log.Info("-----KKB_TB_STN Start removeRecordBykkSymbol-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_STN WHERE KKSYMBOL = '" + kksymbol + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete removeRecordBykkSymbol Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----removeRecordBykkSymbol Finish-----");
                o.closeConnection();
            }

        }

        //reconcile(kksym, sum, record)
        public static object reconcile(string kksym,decimal sum,int record)
        {
            bool ispass = false;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_STN_MASTER WHERE KKSYMBOL = :KKSYMBOL ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);

                o.setVarCharInputParameter("KKSYMBOL", kksym);
                //o.setDecimalInputParameter("SUM_AMOUNT", sum);
                //o.setIntegerInputParameter("SUM_RECORD", record);

                //KKSYMBOL,SUM_AMOUNT,SUM_RECORD 

                DataTable dt = o.executeReaderToDT("STNMASTER");

                if (dt.Rows.Count > 0)
                {
                    decimal amount = decimal.Parse(dt.Rows[0]["SUM_AMOUNT"].ToString());
                    int totalrecord = int.Parse(dt.Rows[0]["SUM_RECORD"].ToString());

                    if (amount != sum) {

                        return new { result = false, Message = "Amount of symbol not equal amount of data. ( KKSYMBOL = " + kksym + ", Amount = " + sum + ")" };
                    
                    }

                    if (totalrecord != record)
                    {

                        return new { result = false, Message = "Total record of symbol not equal total record of data. ( KKSYMBOL = " + kksym + ", Record = " + record + ")" };
                    }

                    return new { result = true, Message = "" };
                     
                }
                else {

                    return new { result = false, Message = "Don't have symbol (" + kksym + ") in master " };
                }

            }
            catch (Exception ex)
            {
                return new { result = false, Message = ex.Message };
            }

            
        }


        public static object getSTN(int STNID)
        {
            Log.Info("-----KKBSTN Start getSTN-----");
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT STN.STNID,STN.TITLE_NAME_TH,STN.FIRST_NAME_TH,STN.LAST_NAME_TH,STN.TITLE_NAME_EN,STN.FIRST_NAME_EN,STN.LAST_NAME_EN, "
                        + " STN.TAXID,STN.CUST_BANK_CODE,STN.CUST_BANK_ACCT,STN.TEL,STN.EMAIL,STN.FC_NAME,STN.FC_EMAIL,STN.CUST_TYPE,STN.PAY_TYPE, "
                        + " STN.UNWIND_PRICE, TO_CHAR(STN.UNWIND_DEALDATE,'DD/MM/YYYY') AS UNWIND_DEALDATE, TO_CHAR(STN.UNWIND_PAYDATE,'DD/MM/YYYY') AS UNWIND_PAYDATE "
                        + " FROM OPICINF.KKB_TB_STN STN  WHERE STN.STNID = :STNID ";
                
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("STNID", STNID);

                DataTable dt = o.executeReaderToDT("KKBSTN");


                List<KKBSTN> param = new List<KKBSTN>();
                KKBSTN p = new KKBSTN();
                p.STNID = int.Parse(dt.Rows[0]["STNID"].ToString());
                p.TITLE_NAME_TH = dt.Rows[0]["TITLE_NAME_TH"].ToString().Trim();
                p.FIRST_NAME_TH = dt.Rows[0]["FIRST_NAME_TH"].ToString().Trim();
                p.LAST_NAME_TH = dt.Rows[0]["LAST_NAME_TH"].ToString().Trim();
                p.TITLE_NAME_EN = dt.Rows[0]["TITLE_NAME_EN"].ToString().Trim();
                p.FIRST_NAME_EN = dt.Rows[0]["FIRST_NAME_EN"].ToString().Trim();
                p.LAST_NAME_EN = dt.Rows[0]["LAST_NAME_EN"].ToString().Trim();
                p.TAXID = dt.Rows[0]["TAXID"].ToString().Trim();
                p.CUST_BANK_CODE = dt.Rows[0]["CUST_BANK_CODE"].ToString().Trim();
                p.CUST_BANK_ACCT = dt.Rows[0]["CUST_BANK_ACCT"].ToString().Trim();
                p.TEL = dt.Rows[0]["TEL"].ToString().Trim();
                p.EMAIL = dt.Rows[0]["EMAIL"].ToString().Trim();
                p.FC_NAME = dt.Rows[0]["FC_NAME"].ToString().Trim();
                p.FC_EMAIL = dt.Rows[0]["FC_EMAIL"].ToString().Trim();
                p.CUST_TYPE = dt.Rows[0]["CUST_TYPE"].ToString().Trim();
                p.PAY_TYPE = dt.Rows[0]["PAY_TYPE"].ToString().Trim();
                p.UNWIND_PRICE = dt.Rows[0]["UNWIND_PRICE"].ToString().Trim() == "" ? 0 : Decimal.Parse(dt.Rows[0]["UNWIND_PRICE"].ToString().Trim());
                p.UNWIND_DEALDATE = dt.Rows[0]["UNWIND_DEALDATE"].ToString().Trim();
                p.UNWIND_PAYDATE = dt.Rows[0]["UNWIND_PAYDATE"].ToString().Trim();

                param.Add(p);


                return param;

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public static DataTable getUnderlyingByKKSymbol(string kksym)
        {
            
            Oracle o = Oracle.getInstance(ConnectionString);

           
            try
            {
                //string sql = "SELECT RTRIM((UNDERLYING1 || ',' || UNDERLYING2 || ',' || UNDERLYING3 || ',' || UNDERLYING4 || ',' || UNDERLYING5 || ',' || UNDERLYING6 ),',') as UNDERLYING "
                //        + " FROM OPICINF.KKB_TB_STN WHERE KKSYMBOL = :KKSYMBOL GROUP BY UNDERLYING1,UNDERLYING2,UNDERLYING3,UNDERLYING4,UNDERLYING5,UNDERLYING6 ";


                string sql = "SELECT A.UNDERLYING,A.UNDERLYING_CURR,RTRIM((M.SETTLE_PRICE1 || ',' || M.SETTLE_PRICE2 || ',' || M.SETTLE_PRICE3 || ',' || M.SETTLE_PRICE4 || ',' || M.SETTLE_PRICE5 || ',' || M.SETTLE_PRICE6),',') as SETTLE_PRICE ,M.SETTLE_DAYS, M.SETTLE_FXRATE "
                             + " FROM (SELECT S.KKSYMBOL, S.UNDERLYING_CURR, RTRIM((S.UNDERLYING1 || ',' || S.UNDERLYING2 || ',' || S.UNDERLYING3 || ',' || S.UNDERLYING4 || ',' || S.UNDERLYING5 || ',' || S.UNDERLYING6 ),',') as UNDERLYING  "
                             + " FROM OPICINF.KKB_TB_STN S WHERE S.KKSYMBOL = :KKSYMBOL  "
                             + " GROUP BY S.KKSYMBOL,S.UNDERLYING_CURR,S.UNDERLYING1,S.UNDERLYING2,S.UNDERLYING3,S.UNDERLYING4,S.UNDERLYING5,S.UNDERLYING6) A "
                             + " INNER JOIN OPICINF.KKB_TB_STN_MASTER M ON M.KKSYMBOL = A.KKSYMBOL ";


                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("KKSYMBOL", kksym);

                DataSet ds = o.executeReader();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("UNDERLYING"));
                dt.Columns.Add(new DataColumn("SETTLE_PRICE"));
                dt.Columns.Add(new DataColumn("SETTLE_DAYS"));
                dt.Columns.Add(new DataColumn("SETTLE_FXRATE"));
                dt.Columns.Add(new DataColumn("UNDERLYING_CURR"));
                    

                if (ds.Tables[0].Rows.Count > 0) { 

                    string u = ds.Tables[0].Rows[0]["UNDERLYING"].ToString(); 
                    string[] arrunderlying = u.Split(',');

                    int i = 0;
                    string p = ds.Tables[0].Rows[0]["SETTLE_PRICE"].ToString();
                    string[] arrsettleprice = p.Split(',');

                    int days = ds.Tables[0].Rows[0]["SETTLE_DAYS"].ToString() == "" ? 0 :int.Parse(ds.Tables[0].Rows[0]["SETTLE_DAYS"].ToString());
                    double fxrate = ds.Tables[0].Rows[0]["SETTLE_FXRATE"].ToString() == "" ? 0 : double.Parse(ds.Tables[0].Rows[0]["SETTLE_FXRATE"].ToString());
                    string undercurr = ds.Tables[0].Rows[0]["UNDERLYING_CURR"].ToString();

                    foreach (string underlying in arrunderlying) {

                        DataRow dr = dt.NewRow();
                        dr["UNDERLYING"] = underlying;
                        dr["UNDERLYING_CURR"] = undercurr;

                        if (i < arrsettleprice.Count())
                        {
                            if (!string.IsNullOrEmpty(arrsettleprice[i]))
                            {
                                dr["SETTLE_PRICE"] = arrsettleprice[i];

                            }
                            else
                            {

                                dr["SETTLE_PRICE"] = "";

                            }
                        }
                        else
                        { 
                            dr["SETTLE_PRICE"] = "";
                        }

                        if ((days != null)&&(days > 0))
                        {
                            dr["SETTLE_DAYS"] = days;

                        }
                        else
                        {

                            dr["SETTLE_DAYS"] = 0;

                        }

                        if ((fxrate != null) && (fxrate > 0))
                        {
                            dr["SETTLE_FXRATE"] = fxrate;

                        }
                        else
                        {

                            dr["SETTLE_FXRATE"] = 0;

                        }

                        dt.Rows.Add(dr);

                        i++;
                    }
                
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

            

        }

        public static DataTable getUnderlyingToKnockout(string kksym)
        {

            Oracle o = Oracle.getInstance(ConnectionString);


            try
            {
                //string sql = "SELECT RTRIM((UNDERLYING1 || ',' || UNDERLYING2 || ',' || UNDERLYING3 || ',' || UNDERLYING4 || ',' || UNDERLYING5 || ',' || UNDERLYING6 ),',') as UNDERLYING "
                //        + " FROM OPICINF.KKB_TB_STN WHERE KKSYMBOL = :KKSYMBOL GROUP BY UNDERLYING1,UNDERLYING2,UNDERLYING3,UNDERLYING4,UNDERLYING5,UNDERLYING6 ";


                string sql = "SELECT A.UNDERLYING,A.UNDERLYING_CURR,RTRIM((M.KO_PRICE1 || ',' || M.KO_PRICE2 || ',' || M.KO_PRICE3 || ',' || M.KO_PRICE4 || ',' || M.KO_PRICE5 || ',' || M.KO_PRICE6),',') as KO_PRICE ,M.KO_DATE ,M.KO_STOCK_TYPE ,M.KO_FXRATE  "
                             + " FROM (SELECT S.KKSYMBOL, S.UNDERLYING_CURR, RTRIM((S.UNDERLYING1 || ',' || S.UNDERLYING2 || ',' || S.UNDERLYING3 || ',' || S.UNDERLYING4 || ',' || S.UNDERLYING5 || ',' || S.UNDERLYING6 ),',') as UNDERLYING  "
                             + " FROM OPICINF.KKB_TB_STN S WHERE S.KKSYMBOL = :KKSYMBOL  "
                             + " GROUP BY S.KKSYMBOL,S.UNDERLYING_CURR,S.UNDERLYING1,S.UNDERLYING2,S.UNDERLYING3,S.UNDERLYING4,S.UNDERLYING5,S.UNDERLYING6) A "
                             + " INNER JOIN OPICINF.KKB_TB_STN_MASTER M ON M.KKSYMBOL = A.KKSYMBOL ";


                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("KKSYMBOL", kksym);

                DataSet ds = o.executeReader();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("UNDERLYING"));
                dt.Columns.Add(new DataColumn("KO_PRICE"));
                dt.Columns.Add(new DataColumn("KO_DATE"));
                dt.Columns.Add(new DataColumn("KO_STOCK_TYPE"));
                dt.Columns.Add(new DataColumn("KO_FXRATE"));
                dt.Columns.Add(new DataColumn("UNDERLYING_CURR"));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    string u = ds.Tables[0].Rows[0]["UNDERLYING"].ToString();
                    string[] arrunderlying = u.Split(',');

                    int i = 0;
                    string p = ds.Tables[0].Rows[0]["KO_PRICE"].ToString();
                    string[] arrkoprice = p.Split(',');

                    string kodate =  ds.Tables[0].Rows[0]["KO_DATE"].ToString() == "" ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["KO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    string type = ds.Tables[0].Rows[0]["KO_STOCK_TYPE"].ToString();
                    string fxrate = ds.Tables[0].Rows[0]["KO_FXRATE"].ToString();
                    string undercurr = ds.Tables[0].Rows[0]["UNDERLYING_CURR"].ToString();


                    foreach (string underlying in arrunderlying)
                    {

                        DataRow dr = dt.NewRow();
                        dr["UNDERLYING"] = underlying;

                        if (i < arrkoprice.Count())
                        {
                            if (!string.IsNullOrEmpty(arrkoprice[i]))
                            {
                                dr["KO_PRICE"] = arrkoprice[i];

                            }
                            else
                            {

                                dr["KO_PRICE"] = "";

                            }
                        }
                        else
                        {
                            dr["KO_PRICE"] = "";
                        }


                        dr["KO_DATE"] = kodate;
                        dr["KO_STOCK_TYPE"] = type;
                        dr["KO_FXRATE"] = fxrate;
                        dr["UNDERLYING_CURR"] = undercurr;

                        dt.Rows.Add(dr);

                        i++;
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }



        }


        //Delete By Person
        public static object DeletePerson(int STNID)
        {

            Log.Info("-----KKB_TB_STN Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                string sql = "DELETE FROM OPICINF.KKB_TB_STN WHERE STNID = :STNID ";
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("STNID", STNID);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };


            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_STN Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_STN Finish-----");
                o.closeConnection();
            }
        }

        //Update By Person
        public static object UpdatePerson(KKBSTN p)
        {

            Log.Info("-----KKB_TB_STN Start UpdatePerson-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                string sql = "UPDATE OPICINF.KKB_TB_STN SET TITLE_NAME_TH = :TITLE_NAME_TH	,FIRST_NAME_TH = :FIRST_NAME_TH	,LAST_NAME_TH = :LAST_NAME_TH , "
                            + " TITLE_NAME_EN = :TITLE_NAME_EN ,FIRST_NAME_EN = :FIRST_NAME_EN	,LAST_NAME_EN = :LAST_NAME_EN, "
                            + " TAXID = :TAXID	,CUST_BANK_CODE = :CUST_BANK_CODE ,CUST_BANK_ACCT = :CUST_BANK_ACCT ,TEL = :TEL	,EMAIL = :EMAIL	,FC_NAME = :FC_NAME	,FC_EMAIL = :FC_EMAIL, "
                            + " CUST_TYPE = :CUST_TYPE , PAY_TYPE = :PAY_TYPE "
                            + " WHERE STNID = :STNID ";

                o.createCommand(sql, CommandType.Text);

                o.setNCharInputParameter("TITLE_NAME_TH", p.TITLE_NAME_TH);
                o.setNCharInputParameter("FIRST_NAME_TH", p.FIRST_NAME_TH);
                o.setNCharInputParameter("LAST_NAME_TH", p.LAST_NAME_TH);
                o.setNCharInputParameter("TITLE_NAME_EN", p.TITLE_NAME_EN);
                o.setNCharInputParameter("FIRST_NAME_EN", p.FIRST_NAME_EN);
                o.setNCharInputParameter("LAST_NAME_EN", p.LAST_NAME_EN);

                o.setNCharInputParameter("TAXID", p.TAXID);
                o.setNCharInputParameter("CUST_BANK_CODE", p.CUST_BANK_CODE);
                o.setNCharInputParameter("CUST_BANK_ACCT", p.CUST_BANK_ACCT);
                o.setNCharInputParameter("TEL", p.TEL);
                o.setNCharInputParameter("EMAIL", p.EMAIL);
                o.setNCharInputParameter("FC_NAME", p.FC_NAME);
                o.setNCharInputParameter("FC_EMAIL", p.FC_EMAIL);
                o.setNCharInputParameter("CUST_TYPE", p.CUST_TYPE);
                o.setNCharInputParameter("PAY_TYPE", p.PAY_TYPE);

                o.setIntegerInputParameter("STNID", p.STNID);

                row = o.executeNonQuery();

                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not update data." };


            }
            catch (Exception ex)
            {
                Log.Error("-----UpdatePerson KKB_TB_STN Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_STN Finish UpdatePerson-----");
                o.closeConnection();
            }

        }

        //UnwindPerson
        public static object UnwindPerson(int STNID, string PRICERETURN, string DATERETURN, string PAYDATE)
        {

            Log.Info("-----KKB_TB_STN Start UnwindPerson-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                string sql = "UPDATE OPICINF.KKB_TB_STN SET ACTION = 'UNWIND', UNWIND_PRICE = :UNWIND_PRICE,UNWIND_DEALDATE = TO_DATE(:UNWIND_DEALDATE,'DD/MM/YYYY'),UNWIND_PAYDATE = TO_DATE(:UNWIND_PAYDATE,'DD/MM/YYYY')  WHERE STNID = :STNID ";



                o.createCommand(sql, CommandType.Text);
                o.setDecimalInputParameter("UNWIND_PRICE", PRICERETURN);
                o.setVarCharInputParameter("UNWIND_DEALDATE", string.Format("{0:DD/MM/yyyy}", DATERETURN));
                o.setVarCharInputParameter("UNWIND_PAYDATE", string.Format("{0:DD/MM/yyyy}", PAYDATE));
                o.setIntegerInputParameter("STNID", STNID);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not update data." };


            }
            catch (Exception ex)
            {
                Log.Error("-----UnwindPerson KKB_TB_STN Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_STN Finish UnwindPerson-----");
                o.closeConnection();
            }
        }

        /*
        //MailUnwind
        public static object MailUnwind(int STNID) {

            try
            {
                string pathfile = Server.MapPath("~/Temporary/STNReport/");
                string folder = "";
                string filename_settle = "";
                string stncc = Config.GetParameterFile("STNCC");

                ReportClass reportSettle = null;

                //Get Data from KKB_SP_TSS_STNCONF
                DataSet ds = Model_RPT_SETTLEMENTREPORT.getData(Config.ConnectionString, symbol, type);

                switch (type)
                {
                    case "SFIN":
                        reportSettle = new RPT_STNSHFN_SETTLEMENT();
                        break;

                    case "DUAL":
                        reportSettle = new RPT_STNDUCN_SETTLEMENT();
                        break;

                    case "DRAN":
                        reportSettle = new RPT_STNDRAN_SETTLEMENT();
                        break;

                    case "BODN":
                        reportSettle = new RPT_STNBODN_SETTLEMENT();
                        break;
                }

                if (ds != null)
                {
                    string[] encrypt = new string[ds.Tables[0].Rows.Count];
                    string[] firstname = new string[ds.Tables[0].Rows.Count];
                    string[] email = new string[ds.Tables[0].Rows.Count];
                    string[] fcmail = new string[ds.Tables[0].Rows.Count];
                    string[] matdate = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encrypt[i] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                        firstname[i] = ds.Tables[0].Rows[i]["FIRST_NAME_EN"].ToString();
                        email[i] = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        fcmail[i] = ds.Tables[0].Rows[i]["FC_EMAIL"].ToString();
                        matdate[i] = ds.Tables[0].Rows[i]["MATURITY_DATE"].ToString();

                        folder = symbol + "_" + firstname[i];
                        filename_settle = firstname[i];

                        DataTable tb = ds.Tables[0].Clone();
                        DataRow row = ds.Tables[0].Rows[i];
                        tb.TableName = "TB_STNCONF";
                        tb.ImportRow(row);

                        //Create folders
                        FileManage.ManageDirectory(pathfile + folder);

                        //Save Files Settlement
                        reportSettle.SetDataSource(tb);
                        reportSettle.ExportToDisk(ExportFormatType.PortableDocFormat, pathfile + folder + "/" + filename_settle + ".pdf");

                        //Encrypt File with password
                        Encryption.EncryptByFolder(pathfile, folder, encrypt[i]);

                        //SendMail
                        string from = "ITtest@kiatnakin.co.th";
                        string to = "traininguser1@kiatnakin.co.th";
                        string cc = "";
                        string subject = symbol + " -  นำส่งใบยืนยันการไถ่ถอนหุ้นกู้ที่มีอนุพันธ์แฝง";
                        string message = "To : " + email[i] + "<br>"
                                       + "cc : " + stncc + fcmail[i] + "<br>"
                                       + " (Confidential) <br>"
                                       + " เรียน ผู้ถือหุ้นกู้ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) <br>"
                                       + " ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) ขอนำส่งใบยืนยันการไถ่ถอนหุ้นกู้ที่มีอนุพันธ์แฝง " + symbol + "<br>"
                                       + " ซึ่งจะครบกำหนดในวันที่ " + matdate[i] + " รายละเอียดตามไฟล์แนบค่ะ    <br> <br> <br> <br>"
                                       + " **เพื่อให้เป็นไปตามเกณฑ์ PDPA ไฟล์ดังกล่าวจะถูกเข้ารหัส โดยใช้รหัสผ่านนี้ในการเปิดไฟล์ จนกว่าจะมีการแจ้งเปลี่ยนแปลง**  <br>"
                                       + " Password : เลขประจำตัวผู้เสียภาษีของท่าน ";

                        List<Attachment> attach = new List<Attachment>();
                        attach.Add(new Attachment(pathfile + folder + ".zip"));
                        Encryption.SendMail(Config.GetParameterFile("SMTP"), int.Parse(Config.GetParameterFile("SMTPPort")), from, to, cc, subject, message, attach);

                    }


                    MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Text = "ไม่สามารถส่งไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                Log.Error(ex.Message);
            }
        
        
        }
        */


    }
}
