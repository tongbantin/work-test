using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Collections;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class STNMASTER
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(STNMASTER));
        public int MASSTNID {get; set;}
        public string KKSYMBOL {get; set;}
        public string STN_TYPE {get; set;}
        public int SUM_RECORD {get; set;}
        public decimal SUM_AMOUNT {get; set;}
        public string CREATE_DATE {get; set;}
        public string CREATE_BY {get; set;}
        public string UPDATE_DATE {get; set;}
        public string UPDATE_BY {get; set;} 
        public decimal SETTLE_PRICE1 {get; set;}
        public decimal SETTLE_PRICE2 {get; set;}
        public decimal SETTLE_PRICE3 {get; set;}
        public decimal SETTLE_PRICE4 {get; set;}
        public decimal SETTLE_PRICE5 {get; set;}
        public decimal SETTLE_PRICE6 {get; set;}
        public int SETTLE_DAYS { get; set; }
         public decimal KO_PRICE1 { get; set; }
         public decimal KO_PRICE2 { get; set; }
         public decimal KO_PRICE3 { get; set; }
         public decimal KO_PRICE4 { get; set; }
         public decimal KO_PRICE5 { get; set; }
         public decimal KO_PRICE6 { get; set; }
         public string KO_STOCK_TYPE { get; set; }
         public string KO_DATE { get; set; }
         public decimal UNWIND_PRICE { get; set; }
         public string PRODUCT { get; set; }
        //add 30/05/2021
         public decimal KO_FXRATE { get; set; }
         public string STN_SEQ { get; set; }
         public string STN_SEQDATE { get; set; }
         public decimal SETTLE_FXRATE { get; set; }

        public static DataTable getMasterByID(string key)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT M.MASSTNID, M.KKSYMBOL, M.STN_TYPE, C.STN_TYPE_FULLNAME  PRODUCT, "
                        + " M.SUM_RECORD,M.SUM_AMOUNT,M.CREATE_DATE,M.CREATE_BY,M.UPDATE_DATE,M.UPDATE_BY,M.STN_SEQ,M.STN_SEQDATE "
                        + " FROM OPICINF.KKB_TB_STN_MASTER M INNER JOIN OPICINF.KKB_TB_STN_COMF_CONFIG C ON C.STN_TYPE = M.STN_TYPE  WHERE 1=1 ";

                if (!string.IsNullOrEmpty(key))
                {

                    sql += " AND M.MASSTNID = :MASSTNID ";
                }


                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!string.IsNullOrEmpty(key))
                {

                    o.setIntegerInputParameter("MASSTNID", int.Parse(key));
                }

                DataTable dt = o.executeReaderToDT("STNMASTER");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static DataTable getSTN(int MASSTNID)
        {
            Log.Info("-----STNNAME Start getSTN-----");

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT STN.STNID,STN.ACTION,STN.CURR_DATE,STN.ORDER_NO,STN.ACCT_NO,STN.SYMBOL,STN.KKSYMBOL,STN.STN_TYPE,STN.UNIT,STN.AMOUNT, "
                        + " STN.TITLE_NAME_TH,STN.FIRST_NAME_TH,STN.LAST_NAME_TH,STN.TITLE_NAME_EN,STN.FIRST_NAME_EN,STN.LAST_NAME_EN, "
                        + " STN.TAXID,STN.CUST_BANK_CODE,STN.CUST_BANK_ACCT,STN.TEL,STN.EMAIL,STN.FC_NAME,STN.FC_EMAIL,STN.CUST_TYPE,STN.PAY_TYPE, "
                        + " STN.CUST_TYPE AS CUST_TYPE2, NVL(STN.UNWIND_PRICE,0) AS UNWIND_PRICE , "
                        + " NVL(TRIM(STN.TITLE_NAME_TH)||' '||TRIM(STN.FIRST_NAME_TH)||' '||TRIM(STN.LAST_NAME_TH),TRIM(STN.TITLE_NAME_EN)||' '||TRIM(STN.FIRST_NAME_EN)||' '||TRIM(STN.LAST_NAME_EN)) AS FULLNAME "
                        + " FROM OPICINF.KKB_TB_STN STN  INNER JOIN OPICINF.KKB_TB_STN_MASTER M ON M.KKSYMBOL = STN.KKSYMBOL AND M.MASSTNID = :MASSTNID ";



                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("MASSTNID", MASSTNID);

                DataTable dt = o.executeReaderToDT("KKBSTN");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        
        }

        public static DataTable getMasterType(int STNID)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT M.MASSTNID, M.KKSYMBOL, M.STN_TYPE FROM OPICINF.KKB_TB_STN_MASTER M INNER JOIN OPICINF.KKB_TB_STN STN ON STN.KKSYMBOL = M.KKSYMBOL AND STN.STNID = :STNID ";

                 
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("STNID", STNID);
             

                DataTable dt = o.executeReaderToDT("STNMASTER");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static DataTable Search(string symbol)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT M.MASSTNID, M.KKSYMBOL, M.STN_TYPE,  C.STN_TYPE_FULLNAME  PRODUCT, M.SUM_RECORD,M.SUM_AMOUNT,M.CREATE_DATE,M.CREATE_BY,M.UPDATE_DATE,M.UPDATE_BY,M.SETTLE_PRICE1,M.SETTLE_DAYS,M.SETTLE_FXRATE, "
                         + " M.KO_PRICE1,M.KO_STOCK_TYPE,M.KO_DATE,M.STN_SEQ,M.STN_SEQDATE "
                         + " FROM OPICINF.KKB_TB_STN_MASTER M INNER JOIN OPICINF.KKB_TB_STN_COMF_CONFIG C ON C.STN_TYPE = M.STN_TYPE  WHERE 1=1 ";                

                if (!string.IsNullOrEmpty(symbol))
                {

                    sql += " AND M.KKSYMBOL = :KKSYMBOL ";
                }

                sql += " ORDER BY MASSTNID DESC ";
                 
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!string.IsNullOrEmpty(symbol))
                {

                    o.setVarCharInputParameter("KKSYMBOL", symbol);
                } 

                DataTable dt = o.executeReaderToDT("STNMASTER");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static bool CheckSymbol(string symbol) {

            bool ishave = false;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_STN_MASTER WHERE KKSYMBOL = '" + symbol + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("STNMASTER");

                if (dt.Rows.Count > 0)
                {

                    ishave = true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return ishave;
        
        }

        public static bool AddMaster(STNMASTER master)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            bool issuccess = false; 
            try
            {

                o.openConnection(); 
 
                    try
                    {
                        string sql = "INSERT INTO OPICINF.KKB_TB_STN_MASTER(KKSYMBOL,STN_TYPE,SUM_RECORD,SUM_AMOUNT,CREATE_DATE,CREATE_BY,STN_SEQ,STN_SEQDATE) ";
                        sql += " VALUES(:KKSYMBOL,:STN_TYPE,:SUM_RECORD,:SUM_AMOUNT,sysdate,:CREATE_BY,:STN_SEQ,:STN_SEQDATE) RETURNING MASSTNID INTO :MASSTNID";



                        o.createCommand(sql, CommandType.Text);

                        o.setVarCharInputParameter("KKSYMBOL", master.KKSYMBOL);
                        o.setVarCharInputParameter("STN_TYPE", master.STN_TYPE);
                        o.setIntegerInputParameter("SUM_RECORD", master.SUM_RECORD);
                        o.setDecimalInputParameter("SUM_AMOUNT", master.SUM_AMOUNT);
                        o.setVarCharInputParameter("CREATE_BY", master.CREATE_BY);
                        o.setVarCharInputParameter("STN_SEQ", master.STN_SEQ);
                        o.setNCharInputParameter("STN_SEQDATE", master.STN_SEQDATE);

                        int result = o.executeNonQuery("MASSTNID");


                        if (result > 0)
                        {
                             
                            master.MASSTNID = result;
                            issuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("----Start Error-----");
                        Log.Error(ex.Message);

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

        public static bool UpdateMaster(STNMASTER master)
        {
            String sql = "UPDATE OPICINF.KKB_TB_STN_MASTER SET KKSYMBOL = :KKSYMBOL,STN_TYPE = :STN_TYPE,SUM_RECORD = :SUM_RECORD,SUM_AMOUNT = :SUM_AMOUNT,UPDATE_DATE = sysdate,UPDATE_BY = :UPDATE_BY,STN_SEQ = :STN_SEQ,STN_SEQDATE = :STN_SEQDATE  "
                           + " WHERE MASSTNID = :MASSTNID ";
            

            Log.Info("-----KKB_TB_STN_MASTER Start Update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);

                o.setVarCharInputParameter("KKSYMBOL", master.KKSYMBOL);
                o.setVarCharInputParameter("STN_TYPE", master.STN_TYPE);
                o.setIntegerInputParameter("SUM_RECORD", master.SUM_RECORD);
                o.setDecimalInputParameter("SUM_AMOUNT", master.SUM_AMOUNT);
                o.setVarCharInputParameter("UPDATE_BY", master.UPDATE_BY);
                o.setVarCharInputParameter("STN_SEQ", master.STN_SEQ);
                o.setNCharInputParameter("STN_SEQDATE", master.STN_SEQDATE);
                o.setIntegerInputParameter("MASSTNID", master.MASSTNID);
                row = o.executeNonQuery();

                if (row > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_STN_MASTER Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Update KKB_TB_STN_MASTER Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Delete()
        {
            String sql = "DELETE FROM OPICINF.KKB_TB_STN_MASTER WHERE MASSTNID ='" + this.MASSTNID + "'";
            Log.Info("-----KKB_TB_STN_MASTER Start Delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();

                if (row > 0)
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_STN_MASTER Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_STN_MASTER Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdatePriceClose(STNMASTER master)
        {

            Log.Info("-----KKB_TB_STN_MASTER Start UpdatePriceClose-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection(); 

                string sql = "UPDATE OPICINF.KKB_TB_STN_MASTER SET SETTLE_PRICE1 = :SETTLE_PRICE1,SETTLE_PRICE2 = :SETTLE_PRICE2, "
                            + " SETTLE_PRICE3 = :SETTLE_PRICE3,SETTLE_PRICE4 = :SETTLE_PRICE4,SETTLE_PRICE5 = :SETTLE_PRICE5, "
                            + " SETTLE_PRICE6 = :SETTLE_PRICE6,SETTLE_DAYS = :SETTLE_DAYS, SETTLE_FXRATE = :SETTLE_FXRATE  " 
                            + " WHERE KKSYMBOL = :KKSYMBOL ";

                o.createCommand(sql, CommandType.Text);

                o.setDecimalInputParameter("SETTLE_PRICE1", master.SETTLE_PRICE1);
                o.setDecimalInputParameter("SETTLE_PRICE2", master.SETTLE_PRICE2);
                o.setDecimalInputParameter("SETTLE_PRICE3", master.SETTLE_PRICE3);
                o.setDecimalInputParameter("SETTLE_PRICE4", master.SETTLE_PRICE4);
                o.setDecimalInputParameter("SETTLE_PRICE5", master.SETTLE_PRICE5);
                o.setDecimalInputParameter("SETTLE_PRICE6", master.SETTLE_PRICE6);

                o.setIntegerInputParameter("SETTLE_DAYS", master.SETTLE_DAYS);
                o.setDecimalInputParameter("SETTLE_FXRATE", master.SETTLE_FXRATE);

                o.setNCharInputParameter("KKSYMBOL", master.KKSYMBOL);

                row = o.executeNonQuery();

                if (row > 0)
                {
                    Log.Info("update success return ok");
                    return new { Result = "OK", Message = "Update price close success." };
                }
                else {
                    return new { Result = "ERROR", Message = "Could not update data." };
                }
                    


            }
            catch (Exception ex)
            {
                Log.Error("-----UpdatePriceClose KKB_TB_STN_MASTER Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_STN_MASTER Finish UpdatePriceClose-----");
                o.closeConnection();
            }

        }

        public static object UpdateKnockout(STNMASTER master)
        {

            Log.Info("-----KKB_TB_STN_MASTER Start UpdateKnockout-----");
            int row1 , row2 = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                              
                // Update table KKB_TB_STN_MASTER & KKB_TB_STN
                /*
                string sql = "BEGIN UPDATE OPICINF.KKB_TB_STN_MASTER SET "
                            + " KO_PRICE1 = :KO_PRICE1,KO_PRICE2 = :KO_PRICE2,KO_PRICE3 = :KO_PRICE3, "
                            + " KO_PRICE4 = :KO_PRICE4,KO_PRICE5 = :KO_PRICE5,KO_PRICE6 = :KO_PRICE6, "
                            + " KO_STOCK_TYPE = :KO_STOCK_TYPE,KO_DATE = TO_DATE(:KO_DATE,'DD/MM/YYYY'), KO_FXRATE = :KO_FXRATE "
                            + " WHERE KKSYMBOL = :KKSYMBOL ;"
                            + " UPDATE OPICINF.KKB_TB_STN SET ACTION = 'KO' WHERE KKSYMBOL = :KKSYMBOL ; END; ";
                 */

                //Update table KKB_TB_STN_MASTER
                string sqlmaster = "UPDATE OPICINF.KKB_TB_STN_MASTER SET "
                          + " KO_PRICE1 = :KO_PRICE1,KO_PRICE2 = :KO_PRICE2,KO_PRICE3 = :KO_PRICE3, "
                          + " KO_PRICE4 = :KO_PRICE4,KO_PRICE5 = :KO_PRICE5,KO_PRICE6 = :KO_PRICE6, "
                          + " KO_STOCK_TYPE = :KO_STOCK_TYPE,KO_DATE = TO_DATE(:KO_DATE,'DD/MM/YYYY'), KO_FXRATE = :KO_FXRATE "
                          + " WHERE KKSYMBOL = :KKSYMBOL ";

                o.createCommand(sqlmaster, CommandType.Text);

                o.setDecimalInputParameter("KO_PRICE1", master.KO_PRICE1);
                o.setDecimalInputParameter("KO_PRICE2", master.KO_PRICE2);
                o.setDecimalInputParameter("KO_PRICE3", master.KO_PRICE3);
                o.setDecimalInputParameter("KO_PRICE4", master.KO_PRICE4);
                o.setDecimalInputParameter("KO_PRICE5", master.KO_PRICE5);
                o.setDecimalInputParameter("KO_PRICE6", master.KO_PRICE6);

                o.setNCharInputParameter("KO_STOCK_TYPE", master.KO_STOCK_TYPE);
                o.setNCharInputParameter("KO_DATE", master.KO_DATE);//DateTime.Parse(master.KO_DATE).ToString("dd/MM/yyyy"));
                o.setDecimalInputParameter("KO_FXRATE", master.KO_FXRATE);

                o.setNCharInputParameter("KKSYMBOL", master.KKSYMBOL);
                
                row1 = o.executeNonQuery();

                //Update table KKB_TB_STN
                string sqlstn = "UPDATE OPICINF.KKB_TB_STN SET ACTION = 'KO' WHERE KKSYMBOL = :KKSYMBOL ";

                o.createCommand(sqlstn, CommandType.Text);
                o.setNCharInputParameter("KKSYMBOL", master.KKSYMBOL);

                row2 = o.executeNonQuery();
                
                if (row1 > 0 && row2 > 0)
                    return new { Result = "OK", Message = "Update knock-out success." };
                else
                    return new { Result = "ERROR", Message = "Could not update data." };


            }
            catch (Exception ex)
            {
                Log.Error("-----UpdateKnockout KKB_TB_STN_MASTER Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_STN_MASTER Finish UpdateKnockout-----");
                o.closeConnection();
            }

        }
    }
}
