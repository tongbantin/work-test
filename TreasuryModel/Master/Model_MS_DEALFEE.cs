using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;

namespace TreasuryModel.Master
{
    public class Model_MS_DEALFEE
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_DEALFEE));

        public static DataTable GetDealFee(string ConnectionString, string DealNo, string DealDate, string SettDate)
        {
            String sql = "Select * From OPICINF.KKB_TB_DEALFEE Where ((TRIM(PRODUCT)='SECUR' AND TRIM(PRODTYPE)='FI') OR (TRIM(PRODUCT) IN ('RPFI','RPSC') AND TRIM(PRODTYPE) IN ('RS','RB')) OR (PRODUCT IS NULL AND PRODTYPE IS NULL)) ";
            if (!DealNo.Equals(""))
                sql += "And DealNo='" + DealNo + "' ";
            if (!DealDate.Equals(""))
                sql += "And DealDate=TO_DATE('" + DealDate + "','DD/MM/YYYY') ";
            if (!SettDate.Equals(""))
                sql += "And SettDate=TO_DATE('" + SettDate + "','DD/MM/YYYY') ";
            sql += "Order By DealNo ";
            Log.Info("-----KKB_TB_DEALFEE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----KKB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }
        public static Boolean AddDealFee(string ConnectionString, string Cost, string DealNo, string SettDate, string ExpenseType, string FeeAmount)
        {
            String sql = "Insert Into OPICINF.KKB_TB_DEALFEE(BR, Cost, DealNo, DealDate, SettDate, Method, TSD_Fee) Values('01', '" + Cost + "','" + DealNo + "',TO_DATE('" + SettDate + "','DD/MM/YYYY'),TO_DATE('" + SettDate + "','DD/MM/YYYY'),'" + ExpenseType + "', '" + FeeAmount + "')";
            Log.Info("-----KKB_TB_DEALFEE Start Update-----");
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
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean UpdateDealFee(string ConnectionString, string DealFeeID, string Method)
        {
            String sql = "Update OPICINF.KKB_TB_DEALFEE Set Method='" + Method + "', TSD_FEE=OPICINF.KKB_FN_TSDFIFEE('" + Method + "') Where DealFeeID='" + DealFeeID + "'";
            Log.Info("-----KKB_TB_DEALFEE Start Update-----");
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
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Update Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }
    }
}
