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
    public class Model_MS_CURRENTPOSITION
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_CURRENTPOSITION));

        public int PositionID { get; set; }
        public string ReportDate { get; set; }
        public string CCY { get; set; }
        public string Product { get; set; }
        public decimal TodayBaseSpot { get; set; }
        public decimal TodayBaseForward { get; set; }
        public decimal BaseSpot { get; set; }
        public decimal USDSpot { get; set; }
        public decimal BaseForward { get; set; }
        public decimal USDForward { get; set; }
        public decimal SpotRate { get; set; }
        public decimal USDSpotRate { get; set; }


        public static DataTable GetCurrentPostion(string ConnectionString, string FromDate, string ToDate)
        {
            String sql = "Select * From OPICINF.KKB_TB_CURRENTPOSITION Where 1=1 ";
            if (ToDate.Equals(""))
                sql += "And REPORTDATE = TO_DATE('" + FromDate + "','DD/MM/YYYY') ";
            if (!ToDate.Equals(""))
                sql += "And REPORTDATE Between TO_DATE('" + FromDate + "','DD/MM/YYYY') and TO_DATE('" + ToDate + "','DD/MM/YYYY') ";
            sql += "Order By REPORTDATE, CCY, PRODUCT ";
            Log.Info("-----KKB_TB_CURRENTPOSITION Start-----");
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

        public static Boolean ActionCurrentPosition(string ConnectionString, string Action, Model_MS_CURRENTPOSITION obj)
        {
            Log.Info("-----Model_MS_CurrentPosition Start-----");
            Log.Info("Store Procedure : " + "OPICINF.KKB_SP_TSS_TR097ACTPO");

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_TR097ACTPO", CommandType.StoredProcedure);
                o.setInputParameter("p_action", Action);
                o.setInputParameter("p_position", obj.PositionID);
                o.setInputParameter("p_reportdate", obj.ReportDate);
                o.setInputParameter("p_ccy", obj.CCY);
                o.setInputParameter("p_product", obj.Product);
                o.setInputParameter("p_tdbasespot", obj.TodayBaseSpot);
                o.setInputParameter("p_tdbaseforward", obj.TodayBaseForward);
                o.setInputParameter("p_spotrate", obj.SpotRate);
                //o.setOutputParameter("result_step");
                o.executeNonQuery();
                
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
                Log.Info("-----Model_RPT_TR093OBOFXCF Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean AddCurrentPosition(string ConnectionString, Model_MS_CURRENTPOSITION obj)
        {
            String sql = "Insert Into OPICINF.KKB_TB_CURRENTPOSITION(ReportDate, CCY, Product, TodayBaseSpot, TodayBaseForward, CreateDate) Values('" + obj.ReportDate + "','" + obj.CCY + "','" + obj.Product + "','" + obj.TodayBaseSpot + "','" + obj.TodayBaseForward + "',sysdate)";
            Log.Info("-----KKB_TB_FUNDPOSITION Start Update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.StoredProcedure);                
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

        public static Boolean UpdateBaseAmount(string ConnectionString, Model_MS_CURRENTPOSITION obj)
        {
            String sql = "Update OPICINF.KKB_TB_CURRENTPOSITION Set BASESPOT='" + obj.TodayBaseSpot + "', BASEFORWARD='" + obj.BaseForward + "' Where ReportDate=TO_DATE('" + obj.ReportDate + "','DD/MM/YYYY') and CCY='" + obj.CCY + "' and Product='" + obj.Product + "' ";
            Log.Info("-----KKB_TB_CURRENTPOSITION Start Update-----");
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
                Log.Info("-----KKB_TB_CURRENTPOSITION Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean UpdateSpotRate(string ConnectionString, Model_MS_CURRENTPOSITION obj)
        {
            String sql = "Update OPICINF.KKB_TB_CURRENTPOSITION Set SPOT_RATE='" + obj.SpotRate + "' Where ReportDate=TO_DATE('" + obj.ReportDate + "','DD/MM/YYYY') and CCY='" + obj.CCY + "' and Product='" + obj.Product + "' ";
            Log.Info("-----KKB_TB_CURRENTPOSITION Start Update-----");
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
                Log.Info("-----KKB_TB_CURRENTPOSITION Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean ActionSummaryPosition(string ConnectionString, string FromDate, string ToDate)
        {
            Log.Info("-----Model_MS_CurrentPosition Start-----");
            Log.Info("Store Procedure : " + "OPICINF.KKB_SP_TSS_CALCPOSITIONSUMMARY");

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_CALCPOSITIONSUMMARY", CommandType.StoredProcedure);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                //o.setOutputParameter("result_step");
                o.executeNonQuery();

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
                Log.Info("-----KKB_TB_CURRENTPOSITION Finish-----");
                o.closeConnection();
            }
        }
    }
}
