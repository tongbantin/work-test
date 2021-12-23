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
    public enum EnumPreConfirm
    { 
        TENOR,
        SWAP,
        DESCRIPTION,
        PRINT
    }

    public class PreConfirmation
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(PreConfirmation));

        public string DEALNO { get; set; }
        public string TENOR { get; set; }
        public string SWAPPOINT { get; set; }
        public DateTime TENORUPDATE { get; set; }
        public DateTime SWAPUPDATE { get; set; }
        public string NEARPURPOSE { get; set; }
        public string NEARPURPOSEDESC { get; set; }
        public string FARPURPOSE { get; set; }
        public string FARPURPOSEDESC { get; set; }
        public string TENORVAL { get; set; }
        public string DESCRIPTION { get; set; }
        public string REMARK { get; set; }
        public string PRINTBY { get; set; }
        public string PRINTDATE { get; set; }
        public string SWAPDEAL { get; set; }

        public static bool IsExisting(string ConnectionString, string DealNo)
        {
            String sql = "Select DEALNO From OPICINF.KKB_TB_FXDEAL Where TRIM(DEALNO)=TRIM('" + DealNo + "') ";

            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                object dealno = o.executeScalar().ToString().Trim();
                if (dealno == null)
                    return false;
                else if (dealno == "")
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
        public static object Update(string ConnectionString, EnumPreConfirm Action, string[] args, bool IsExisting)
        {

            Log.Info("-----KKB_TB_FXDEAL Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "";
                switch (Action)
                { 
                    case EnumPreConfirm.TENOR :
                        if (IsExisting)
                            sql = "UPDATE OPICINF.KKB_TB_FXDEAL SET TENOR=:PARAM1, TENORUPDATE=SYSDATE WHERE TRIM(DEALNO)=:PARAM2";
                        else
                            sql = "INSERT INTO OPICINF.KKB_TB_FXDEAL(TENOR, DEALNO, TENORUPDATE) VALUES(:PARAM1, :PARAM2, SYSDATE)";
                        break;
                    case EnumPreConfirm.SWAP:
                        if (IsExisting)
                            sql = "UPDATE OPICINF.KKB_TB_FXDEAL SET SWAPPOINT=:PARAM1, SWAPUPDATE=SYSDATE WHERE TRIM(DEALNO)=:PARAM2";
                        else
                            sql = "INSERT INTO OPICINF.KKB_TB_FXDEAL(SWAPPOINT, DEALNO, SWAPUPDATE) VALUES(:PARAM1, :PARAM2, SYSDATE)";
                        break;
                    case EnumPreConfirm.DESCRIPTION:
                        if (IsExisting)
                            sql = "UPDATE OPICINF.KKB_TB_FXDEAL SET NEARPURPOSE=:PARAM1, NEARPURPOSEDESC=:PARAM2, FARPURPOSE=:PARAM3, FARPURPOSEDESC=:PARAM4, TENORVAL=:PARAM5, REMARK=:PARAM6 WHERE TRIM(DEALNO)=:PARAM7";
                        else
                            sql = "INSERT INTO OPICINF.KKB_TB_FXDEAL(NEARPURPOSE, NEARPURPOSEDESC, FARPURPOSE, FARPURPOSEDESC, TENORVAL, REMARK, DEALNO) VALUES(:PARAM1,:PARAM2,:PARAM3,:PARAM4,:PARAM5, :PARAM6, :PARAM7)";
                        break;
                    case EnumPreConfirm.PRINT:
                        if (IsExisting)
                            sql = "UPDATE OPICINF.KKB_TB_FXDEAL SET PRINTBY=:PARAM1, PRINTDATE=SYSDATE WHERE TRIM(DEALNO)=:PARAM2";
                        else
                            sql = "INSERT INTO OPICINF.KKB_TB_FXDEAL(PRINTBY, DEALNO, PRINTDATE) VALUES(:PARAM1, :PARAM2, SYSDATE)";
                        break;
                }

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (Action.Equals(EnumPreConfirm.DESCRIPTION))
                {
                    o.setInputParameter("PARAM1", args[0].Trim());
                    o.setNCharInputParameter("PARAM2", args[1].Trim());
                    o.setInputParameter("PARAM3", args[2].Trim());
                    o.setNCharInputParameter("PARAM4", args[3].Trim());
                    o.setInputParameter("PARAM5", args[4].Trim());
                    o.setNCharInputParameter("PARAM6", args[5].Trim());
                    o.setInputParameter("PARAM7", args[6].Trim());
                }
                else 
                {
                    o.setInputParameter("PARAM1", args[0].Trim());
                    o.setInputParameter("PARAM2", args[1].Trim());
                }
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
        public static object GetDescription(string ConnectionString, string DealNo)
        {
            String sql = "Select FXDH.DealNo, KKB_TB_FXDEAL.NEARPURPOSE as NEARPURPOSE, KKB_TB_FXDEAL.NEARPURPOSEDESC, KKB_TB_FXDEAL.FARPURPOSE as FARPURPOSE, KKB_TB_FXDEAL.FARPURPOSEDESC, NVL(KKB_TB_FXDEAL.TENORVAL, ";
            sql += "(SELECT  ";
            sql += "    CASE WHEN iFXDH.SWAPDEAL<>'0' THEN ";
            sql += "        TO_NUMBER(FAR.VDATE - iFXDH.VDATE) ";
            sql += "    WHEN iFXDH.SPOTFWDIND='F' THEN  ";
            sql += "        TO_NUMBER(iFXDH.VDATE - (iFXDH.DEALDATE + 2)) ";
            sql += "    ELSE ";
            sql += "        TO_NUMBER(iFXDH.VDATE - iFXDH.DEALDATE)  ";
            sql += "    END ";
            sql += "From OPICS.FXDH iFXDH LEFT OUTER JOIN OPICS.FXDH FAR ON TRIM(iFXDH.SWAPDEAL)=TRIM(FAR.DEALNO)  ";
            sql += "WHERE iFXDH.DEALNO=FXDH.DEALNO) ";
            sql += ")  AS TENORVAL , KKB_TB_FXDEAL.Remark, FXDH.SWAPDEAL ";
            sql += "From OPICS.FXDH LEFT OUTER JOIN OPICS.FXDT ON FXDH.DEALNO=FXDT.DEALNO LEFT OUTER JOIN OPICINF.KKB_TB_FXDEAL ON FXDH.DEALNO=KKB_TB_FXDEAL.DEALNO WHERE TRIM(FXDH.DEALNO)=TRIM('" + DealNo + "') ";
            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("FXDEAL");

                List<PreConfirmation> param = new List<PreConfirmation>();
                PreConfirmation p = new PreConfirmation();
                p.DEALNO = dt.Rows[0]["DEALNO"].ToString().Trim();
                p.NEARPURPOSE = dt.Rows[0]["NEARPURPOSE"].ToString().Trim();
                p.NEARPURPOSEDESC = dt.Rows[0]["NEARPURPOSEDESC"].ToString().Trim();
                p.FARPURPOSE = dt.Rows[0]["FARPURPOSE"].ToString().Trim();
                p.FARPURPOSEDESC = dt.Rows[0]["FARPURPOSEDESC"].ToString().Trim();
                p.TENORVAL = dt.Rows[0]["TENORVAL"].ToString().Trim();
                p.REMARK = dt.Rows[0]["REMARK"].ToString().Trim();
                p.SWAPDEAL = dt.Rows[0]["SWAPDEAL"].ToString().Trim();
                
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
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }
        public static DataTable GetFund(string ConnectionString, string Party)
        {
            String sql = "SELECT TO_CHAR(REGEXP_SUBSTR('" + Party + "','[^,]+', 1, LEVEL)) as GRPID FROM DUAL CONNECT BY REGEXP_SUBSTR('" + Party + "', '[^,]+', 1, LEVEL) IS NOT NULL";
            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("Fund");

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }
        public static string GetCMNE(string ConnectionString, string DealNo)
        {

            String sql = "SELECT CMNE FROM OPICS.CUST WHERE CNO IN (SELECT CUST FROM OPICS.FXDH WHERE TRIM(DEALNO)='" + DealNo.Trim() + "')";
            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return o.executeScalar().ToString();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }
    }
}
