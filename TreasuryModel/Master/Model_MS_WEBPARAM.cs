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
    /*
    public enum MTMReport
    { 
        GENERAL = 1,
        CLOSE = 2,
        PHATRA = 3
    }
      */

    public class Model_MS_WEBPARAM
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_WEBPARAM));

        /*
        public int MTMScheduleID { get; set; }
        public string Round { get; set; }
        public string MutualFund { get; set; } 
        public string OpenClose { get; set; }
        public string Source { get; set; }
        public string SpecialSource { get; set; }
        public string Rate { get; set; }
        public string FileType { get; set; }
        public string AllMdate { get; set; }
        public string FileName { get; set; }
        public string Template { get; set; }
        public string Notes { get; set; }
         * */


        public static DataTable getParamValueList(string ConnectionString, string param)
        {
            Log.Info("-----Model_MS_WEBPARAM Start-----");
            Log.Info("getParamValueList : Start");

            String sql = "";
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                if(!param.Equals("")){
                    sql += "SELECT NAME, TRIM(SUBSTR(TRIM(NAME),INSTR(TRIM(NAME),'_',-1)+1)) AS PRODTYPE, VALUE, TRIM(SUBSTR(TRIM(NAME),INSTR(TRIM(NAME),'_',-1)+1))||'|'||trim(VALUE) AS SELVALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE SUBSTR(NAME,1," + param.Length + ") = '" + param + "'";
                }else{
                    sql += "SELECT NAME,VALUE FROM KKB_TB_WEBPARAM ";
                }                 

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                Log.Info("getParamValueList sql :"+sql);
                //s = o.executeReaderToDT("TB_WEBPARAM");
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_WEBPARAM Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public static DataTable getEmailByProdtype(string ConnectionString, string prodtype)
        {
            Log.Info("-----Model_MS_WEBPARAM Start-----");
            Log.Info("getEmailByProdtype : Start");

            String sql = "";
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                sql += "SELECT NAME, VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE TRIM(NAME) = 'EMAIL_" + prodtype +"'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                //s = o.executeReaderToDT("TB_WEBPARAM");
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----getEmailByProdtype Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_WEBPARAM Finish-----");
                o.closeConnection();
            }
            if (s.Tables[0].Rows.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public static DataTable getSubjectByProdtype(string ConnectionString, string prodtype)
        {
            Log.Info("-----Model_MS_WEBPARAM Start-----");
            Log.Info("getSubjectByProdtype : Start");

            String sql = "";
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                sql += "SELECT NAME, VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE TRIM(NAME) = 'SUB_" + prodtype + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                //s = o.executeReaderToDT("TB_WEBPARAM");
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----getSubjectByProdtype Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_WEBPARAM Finish-----");
                o.closeConnection();
            }
            if (s.Tables[0].Rows.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public static DataTable getMessageByProdtype(string ConnectionString, string prodtype)
        {
            Log.Info("-----Model_MS_WEBPARAM Start-----");
            Log.Info("getMessageByProdtype : Start");

            String sql = "";
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                sql += "SELECT NAME, VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE TRIM(NAME) = 'MSG_" + prodtype + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                //s = o.executeReaderToDT("TB_WEBPARAM");
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----getMessageByProdtype Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_WEBPARAM Finish-----");
                o.closeConnection();
            }
            if (s.Tables[0].Rows.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

      
    }
}
