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
    public class TransToALR
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(TransToALR));
        
        //For Upload Transaction
        public int TRANSID { get; set; }
        public string TEAM { get; set; }
        public string DEALNO { get; set; }
        public string ID_NO_PART1 { get; set; }
        public string ID_NO_PART2 { get; set; }
        public string SENDER_CUST_TYPE { get; set; }
        public string SENDING_BANK_CODE { get; set; }
        public string SENDING_TYPE { get; set; }
        public string SENDING_AC_NO { get; set; }
        public string SENDING_INFO { get; set; }
        public string RECEIVER_CUST_TYPE { get; set; }
        public string RECEIVING_BANK_CODE { get; set; }
        public string RECEIVING_TYPE { get; set; }
        public string RECEIVING_AC_NO { get; set; }
        public string RECEIVING_INFO { get; set; }
        public Decimal TRANSFER_AMOUNT { get; set; }
        public string SETTLE_DATE { get; set; }
        public string UPLOAD_DATE { get; set; }
        public string CUST_CODE { get; set; }

        //For View Transaction
        public string BU_CODE { get; set; }
        public string SETTDATE { get; set; }
        public string SEND_ACCT_NO { get; set; }
        public string SEND_ACCT_NAME { get; set; }
        public string REV_ACCT_NO { get; set; }
        public string REV_ACCT_NAME { get; set; }
        public Decimal TRANS_AMT { get; set; }
        public string TRANS_DATE { get; set; }
        public string SEND_TAXID { get; set; }
        public string SEND_CUST_TYPE { get; set; }
        public string REV_TAXID { get; set; }
        public string REV_CUST_TYPE { get; set; }
        public string CUST_BUILDINGNAME { get; set; }
        public string CUST_ROOMNO { get; set; }
        public string CUST_FLOORNO { get; set; }
        public string CUST_ADDNO { get; set; }
        public string CUST_MOONO { get; set; }
        public string CUST_SOI { get; set; }
        public string CUST_STREETNAME { get; set; }
        public string CUST_TUMBON { get; set; }
        public string CUST_AMPHUR { get; set; }
        public string CUST_PROVINCE { get; set; }
        public string CUST_POSTCODE { get; set; }
        public string REFER { get; set; }


        
        //For Upload Transaction

        public static DataTable getTransToALRByID(string listtransid)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.TB_TRANS_TO_ALR WHERE TRANSID IN (" + listtransid + ") ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("TransToALR");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable Search(string dealno, string settlementdate)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.TB_TRANS_TO_ALR WHERE 1=1 ";


                if (!string.IsNullOrEmpty(dealno))
                {

                    sql += " AND DEALNO = :DEALNO ";
                }

                if (!string.IsNullOrEmpty(settlementdate))
                {

                    sql += " AND SETTLE_DATE = TO_DATE(:SETTLE_DATE, 'DD/MM/YYYY') ";
                }


                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!string.IsNullOrEmpty(dealno))
                {

                    o.setVarCharInputParameter("DEALNO", dealno);
                }

                if (!string.IsNullOrEmpty(settlementdate))
                {

                    o.setVarCharInputParameter("SETTLE_DATE", settlementdate);
                }


                DataTable dt = o.executeReaderToDT("TransToALR");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static bool AddALR(List<TransToALR> ALRList)
        {
           
            Oracle o = Oracle.getInstance(ConnectionString);
            bool issuccess = false;
            bool flagerror = false;
            try
            {
                o.openConnection();
                o.beginTransaction(); 

                

                foreach (TransToALR ALR in ALRList)
                {
                    try
                    {
                        string sql = "INSERT INTO OPICINF.TB_TRANS_TO_ALR(TEAM,DEALNO,ID_NO_PART1,ID_NO_PART2,SENDER_CUST_TYPE,SENDING_BANK_CODE,SENDING_TYPE,SENDING_AC_NO,SENDING_INFO,RECEIVER_CUST_TYPE,RECEIVING_BANK_CODE,RECEIVING_TYPE,RECEIVING_AC_NO,RECEIVING_INFO,TRANSFER_AMOUNT,SETTLE_DATE,UPLOAD_DATE,CUST_CODE) ";
                        sql += " VALUES(:TEAM,:DEALNO,:ID_NO_PART1,:ID_NO_PART2,:SENDER_CUST_TYPE,:SENDING_BANK_CODE,:SENDING_TYPE,:SENDING_AC_NO,:SENDING_INFO,:RECEIVER_CUST_TYPE,:RECEIVING_BANK_CODE,:RECEIVING_TYPE,:RECEIVING_AC_NO,:RECEIVING_INFO,:TRANSFER_AMOUNT,TO_DATE(:SETTLE_DATE,'DD/MM/YYYY'),Sysdate,:CUST_CODE) RETURNING TRANSID INTO :TRANSID";
 
                        o.createCommand(sql, CommandType.Text);
                        o.setVarCharInputParameter("TEAM", ALR.TEAM);
                        o.setVarCharInputParameter("DEALNO", ALR.DEALNO);
                        o.setVarCharInputParameter("ID_NO_PART1", ALR.ID_NO_PART1);
                        o.setVarCharInputParameter("ID_NO_PART2", ALR.ID_NO_PART2);
                        o.setVarCharInputParameter("SENDER_CUST_TYPE", ALR.SENDER_CUST_TYPE);
                        o.setVarCharInputParameter("SENDING_BANK_CODE", ALR.SENDING_BANK_CODE);
                        o.setVarCharInputParameter("SENDING_TYPE", ALR.SENDING_TYPE);
                        o.setVarCharInputParameter("SENDING_AC_NO", ALR.SENDING_AC_NO);
                        o.setNCharInputParameter("SENDING_INFO", ALR.SENDING_INFO);
                        o.setVarCharInputParameter("RECEIVER_CUST_TYPE", ALR.RECEIVER_CUST_TYPE);
                        o.setVarCharInputParameter("RECEIVING_BANK_CODE", ALR.RECEIVING_BANK_CODE);
                        o.setVarCharInputParameter("RECEIVING_TYPE", ALR.RECEIVING_TYPE);
                        o.setVarCharInputParameter("RECEIVING_AC_NO", ALR.RECEIVING_AC_NO);
                        o.setNCharInputParameter("RECEIVING_INFO", ALR.RECEIVING_INFO);
                        o.setDecimalInputParameter("TRANSFER_AMOUNT", ALR.TRANSFER_AMOUNT);
                        o.setVarCharInputParameter("SETTLE_DATE", DateTime.Parse(ALR.SETTLE_DATE).ToString("dd/MM/yyyy"));
                        //o.setVarCharInputParameter("SETTLE_DATE", ALR.SETTLE_DATE);
                        o.setVarCharInputParameter("CUST_CODE", ALR.CUST_CODE);
                        

                        int result = o.executeNonQuery("TRANSID");


                        if (result <= 0)
                        {
                            flagerror = true;

                        }
                        else {


                            ALR.TRANSID = result;
                            
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

        public static Boolean UpdateALR(TransToALR data)
        {
            String sql = "UPDATE OPICINF.TB_TRANS_TO_ALR SET TEAM = :TEAM "
                + " , DEALNO = :DEALNO , ID_NO_PART1 = :ID_NO_PART1 , ID_NO_PART2 = :ID_NO_PART2 "
                + " , SENDER_CUST_TYPE = :SENDER_CUST_TYPE, SENDING_BANK_CODE = :SENDING_BANK_CODE, SENDING_TYPE = :SENDING_TYPE, SENDING_AC_NO = :SENDING_AC_NO "
                + " , SENDING_INFO = :SENDING_INFO, RECEIVER_CUST_TYPE = :RECEIVER_CUST_TYPE, RECEIVING_BANK_CODE = :RECEIVING_BANK_CODE, RECEIVING_TYPE = :RECEIVING_TYPE "
                + " , RECEIVING_AC_NO = :RECEIVING_AC_NO, RECEIVING_INFO = :RECEIVING_INFO "
                + " , TRANSFER_AMOUNT = :TRANSFER_AMOUNT , SETTLE_DATE = :SETTLE_DATE "
                + " WHERE TRANSID = :TRANSID ";
 



            Log.Info("-----TB_TRANS_TO_ALR Start Update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);

                /*
                o.setNCharInputParameter("TEAM", data.TEAM);
                o.setVarCharInputParameter("DEALNO", data.DEALNO);
                o.setVarCharInputParameter("ID_NO_PART1", data.ID_NO_PART1);
                o.setVarCharInputParameter("ID_NO_PART2", data.ID_NO_PART2);
                o.setVarCharInputParameter("SENDER_CUST_TYPE", data.SENDER_CUST_TYPE);
                o.setVarCharInputParameter("SENDING_BANK_CODE", data.SENDING_BANK_CODE);
                o.setVarCharInputParameter("SENDING_TYPE", data.SENDING_TYPE);
                o.setVarCharInputParameter("SENDING_AC_NO", data.SENDING_AC_NO);
                o.setVarCharInputParameter("SENDING_INFO", data.SENDING_INFO);
                o.setVarCharInputParameter("RECEIVER_CUST_TYPE", data.RECEIVER_CUST_TYPE);
                o.setVarCharInputParameter("RECEIVING_BANK_CODE", data.RECEIVING_BANK_CODE);
                o.setVarCharInputParameter("RECEIVING_TYPE", data.RECEIVING_TYPE);
                o.setVarCharInputParameter("RECEIVING_AC_NO", data.RECEIVING_AC_NO);
                o.setVarCharInputParameter("RECEIVING_INFO", data.RECEIVING_INFO);
                o.setDecimalInputParameter("TRANSFER_AMOUNT", data.TRANSFER_AMOUNT);
                o.setVarCharInputParameter("SETTLE_DATE", data.SETTLE_DATE);
                o.setIntegerInputParameter("TRANSID", data.TRANSID);
                */

                o.setInputParameter("TEAM", data.TEAM);
                o.setInputParameter("DEALNO", data.DEALNO);
                o.setInputParameter("ID_NO_PART1", data.ID_NO_PART1);
                o.setInputParameter("ID_NO_PART2", data.ID_NO_PART2);
                o.setInputParameter("SENDER_CUST_TYPE", data.SENDER_CUST_TYPE);
                o.setInputParameter("SENDING_BANK_CODE", data.SENDING_BANK_CODE);
                o.setInputParameter("SENDING_TYPE", data.SENDING_TYPE);
                o.setInputParameter("SENDING_AC_NO", data.SENDING_AC_NO);
                o.setInputParameter("SENDING_INFO", data.SENDING_INFO);
                o.setInputParameter("RECEIVER_CUST_TYPE", data.RECEIVER_CUST_TYPE);
                o.setInputParameter("RECEIVING_BANK_CODE", data.RECEIVING_BANK_CODE);
                o.setInputParameter("RECEIVING_TYPE", data.RECEIVING_TYPE);
                o.setInputParameter("RECEIVING_AC_NO", data.RECEIVING_AC_NO);
                o.setInputParameter("RECEIVING_INFO", data.RECEIVING_INFO);
                o.setInputParameter("TRANSFER_AMOUNT", data.TRANSFER_AMOUNT);
                o.setInputParameter("SETTLE_DATE", data.SETTLE_DATE);
                o.setInputParameter("TRANSID", data.TRANSID);


                row = o.executeNonQuery();

                if (row > 0)
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                Log.Error("-----Update TB_TRANS_TO_ALR Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Update TB_TRANS_TO_ALR Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteALR(string TRANSID)
        {

            Log.Info("-----TB_TRANS_TO_ALR Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.TB_TRANS_TO_ALR WHERE TRANSID=" + TRANSID;
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete TB_TRANS_TO_ALR Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----TB_TRANS_TO_ALR Finish-----");
                o.closeConnection();
            }
        }

        /*
        public static Boolean DeleteALR(string TRANSID)
        {

            Log.Info("-----TB_TRANS_TO_ALR Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.TB_TRANS_TO_ALR WHERE TRANSID=" + TRANSID;
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete TB_TRANS_TO_ALR Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----TB_TRANS_TO_ALR Finish-----");
                o.closeConnection();
            }

        }
         */

        //View Transaction

        public static DataTable getViewTrans(string ConnectionString, string StartDate)
        {
            Log.Info("-----Model_TransToALR Start-----");
            Log.Info("getViewTrans =" + StartDate);

            DataTable dt = null;
            string sql = "";
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                sql = "SELECT BU_CODE, DEALNO, TO_CHAR(SETTDATE,'DD/MM/YYYY') AS SETTDATE, SEND_ACCT_NO, SEND_ACCT_NAME, REV_ACCT_NO, REV_ACCT_NAME, TRANS_AMT, TO_CHAR(TRANS_DATE,'DD/MM/YYYY') AS TRANS_DATE, SEND_TAXID, SEND_CUST_TYPE, REV_TAXID, REV_CUST_TYPE, ";
                sql += "CUST_CODE, CUST_BUILDINGNAME, CUST_ROOMNO, CUST_FLOORNO, CUST_ADDNO, CUST_MOONO, CUST_SOI, CUST_STREETNAME, CUST_TUMBON, CUST_AMPHUR, CUST_PROVINCE, CUST_POSTCODE, REFER "; 
                sql += "FROM TRODL.KKB_TRO_DIL_TB_V_TOALR WHERE TO_CHAR(DATADATE,'DD/MM/YYYY') = '" +StartDate+ "' ORDER BY DEALNO ASC ";

                o.createCommand(sql, CommandType.Text);
                dt = o.executeReaderToDT("TransToALR");

                //return dt;

            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_TransToALR Finish-----");
                o.closeConnection();
            }
            return dt;
        }
    
    
    }
}
