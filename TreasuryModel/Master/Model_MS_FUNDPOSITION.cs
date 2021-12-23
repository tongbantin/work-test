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
    public class Model_MS_FUNDPOSITION
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_FUNDPOSITION));
        public static DataTable GetFundPostion(string ConnectionString, string FromDate, string ToDate)
        {
            String sql = "Select * From OPICINF.KKB_TB_FUNDPOSITION Where 1=1 ";
            if (ToDate.Equals(""))
                sql += "And EFFDATE = TO_DATE('" + FromDate + "','DD/MM/YYYY') ";   
            if (!ToDate.Equals(""))
                sql += "And EFFDATE Between TO_DATE('" + FromDate + "','DD/MM/YYYY') and TO_DATE('" + ToDate + "','DD/MM/YYYY') ";         
            sql += "Order By EFFDATE ";
            Log.Info("-----KKB_TB_FUNDPOSITION Start-----");
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
                Log.Info("-----KKB_TB_FUNDPOSITION Finish-----");
                o.closeConnection();
            }
        }
        public static Boolean AddFundPosition(string ConnectionString, string EffDate, string Amount)
        {
            String sql = "Insert Into OPICINF.KKB_TB_FUNDPOSITION(EffDate, Amount) Values(TO_DATE('" + EffDate + "','DD/MM/YYYY'), '" + Amount + "')";
            Log.Info("-----KKB_TB_FUNDPOSITION Start Update-----");
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
                Log.Info("-----KKB_TB_FUNDPOSITION Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean UpdateFundPosition(string ConnectionString, string FundPositionID, string EffDate, string Amount)
        {
            String sql = "Update OPICINF.KKB_TB_FUNDPOSITION Set EFFDATE=TO_DATE('" + EffDate + "','DD/MM/YYYY'), Amount='" + Amount + "' Where FundPositionID='" + FundPositionID + "'";
            Log.Info("-----KKB_TB_FUNDPOSITION Start Update-----");
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
                Log.Info("-----KKB_TB_FUNDPOSITION Finish-----");
                o.closeConnection();
            }
        }
    }
}
