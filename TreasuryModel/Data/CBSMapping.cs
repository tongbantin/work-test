using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Globalization; 

namespace TreasuryModel.Data
{
    public enum CBSDESTINATION
    { 
        OPICS,
        TDS
    }
    public enum CBSMappingField
    {   //NAME MUST MATCH IN DATABASE
        CUSTOMER_CATEGORY,
        CUSTOMER_SUBTYPE,
        ID_TYPE_CODE,
        TITLEEN,
        TITLETH
    }

    public class CBSMapping
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(CBSCustomer));

        public static DataTable GetMappingCustomerCategory(string ConnectionString, CBSDESTINATION sys)
        {
            try
            {
                return GetTROValue(ConnectionString, CBSMappingField.CUSTOMER_CATEGORY, sys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable GetMappingCustomerSubType(string ConnectionString, CBSDESTINATION sys)
        {
            try
            {
                return GetTROValue(ConnectionString, CBSMappingField.CUSTOMER_SUBTYPE, sys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable GetMappingIDTypeCode(string ConnectionString, CBSDESTINATION sys)
        {
            try
            {
                return GetTROValue(ConnectionString, CBSMappingField.ID_TYPE_CODE, sys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable GetMappingTitleEn(string ConnectionString, CBSDESTINATION sys)
        {
            try
            {
                return GetTROValue(ConnectionString, CBSMappingField.TITLEEN, sys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
        public static DataTable GetMappingTitleTh(string ConnectionString, CBSDESTINATION sys)
        {
            try
            {
                return GetTROValue(ConnectionString, CBSMappingField.TITLETH, sys);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
        /*
        private static DataTable GetTROValue(string ConnectionString, CBSMappingField mapping, CBSDESTINATION sys)
        {
            string DBLink = "";
            if (ConnectionString.Contains("TDS"))
                DBLink = "@TOTRODB";
            String sql = "Select TROID, TROCODE, CBSCODE, CBSDESCRIPTION From OPICINF.KKB_TB_CBS_MAPPING" + DBLink + " Where SYSDEST='" + sys.ToString() + "' AND CBSCATEGORY='" + mapping.ToString() + "' ";
            Log.Info("-----KKB_TB_CBS_MAPPING " + mapping.ToString() + " Start-----");
            Log.Info("SQL Statement : " + sql);
                        
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return o.executeReaderToDT(mapping.ToString());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_MAPPING " + mapping.ToString() + " Finish-----");
                o.closeConnection();
            }
        }
        */

        private static DataTable GetTROValue(string ConnectionString, CBSMappingField mapping, CBSDESTINATION sys)
        {
            String sql = "Select TROID, TROCODE, CBSCODE, CBSDESCRIPTION From OPICINF.KKB_TB_CBS_MAPPING Where SYSDEST='" + sys.ToString() + "' AND CBSCATEGORY='" + mapping.ToString() + "' ";
            Log.Info("-----KKB_TB_CBS_MAPPING " + mapping.ToString() + " Start-----");
            Log.Info("SQL Statement : " + sql);

            try
            {
                return Oracle.getDataAdapter(sql, ConnectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable GetMappingCountry(string ConnectionString)
        {
            String sql = "Select * From TDS.MS_BOT Where MSBOTTYPEID=(SELECT MSBOTTYPEID FROM TDS.MS_BOTTYPE WHERE USERCODE='COUNTRYID') ";
            Log.Info("-----TDS_MS_BOT Country Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return o.executeReaderToDT("Country");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----TDS_MS_BOT Country Finish-----");
                o.closeConnection();
            }
        }
    }

}
