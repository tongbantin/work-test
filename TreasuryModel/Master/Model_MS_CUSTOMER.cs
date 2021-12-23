
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
    public class Model_MS_CUSTOMER
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_CUSTOMER));

        public static DataTable GetCustomerByCustomerGroup(string ConnectionString, string CustomerGroup)
        {
            String sql = "Select CNO, SN From OPICS.CUST Where 1=1 ";
            if (!CustomerGroup.Equals(""))
                sql += "AND CNO in (SELECT GRPMEMBER FROM OPICS.CRGM WHERE GRPID='" + CustomerGroup + "') ";
            else
                sql += "AND UCCODE IN (SELECT GRPMEMBER FROM OPICS.CRGM WHERE GRPID='EMIRGROUP') ";
            sql += "Order By SN ";
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
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable GetCustomerGroupByEmir(string ConnectionString)
        {
            String sql = "Select GRPID From OPICS.CRGM Where GRPMEMBER in (SELECT CNO FROM OPICS.CUST WHERE UCCODE IN (SELECT GRPMEMBER FROM OPICS.CRGM WHERE GRPID='EMIRGROUP')) GROUP BY GRPID ";
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
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }
      
        public static DataTable GetCustomerBIC(string ConnectionString, String Name)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select CMNE, BIC, SN, CFN1 || CFN2 as NAME From OPICS.CUST Where CA4 like '%THAILAND%' AND BIC IS NOT NULL AND UPPER(SN) like '%" + Name.ToUpper() + "%'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable GetCustomerAllSource(string ConnectionString, string resource, string findCust)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "";
                string opic = "SELECT CASE WHEN CTYPE = 'I' THEN REPLACE(TRIM(NVL(TAXID,CA5)), ' ', '_') ELSE REPLACE(TRIM(CMNE), ' ', '_') END AS CUSTCODE , TRIM(TO_NCHAR(SN)) AS CUSTNAME , 'OPICS' as SOURCE ";
                       opic += "FROM OPICS.CUST ";
                       opic += "WHERE( UPPER(SN) LIKE UPPER(:findCust) OR UPPER(CMNE) LIKE UPPER(:findCust)) ";
                string tss = "SELECT CASE WHEN CUSTOMERTYPE = 'P' THEN REPLACE(TRIM(TAXID), ' ', '_') ";
                       tss += "WHEN CUSTOMERTYPE = 'C' THEN NVL(REPLACE(TRIM(CODE), ' ', '_'), REPLACE(TRIM(TAXID), ' ', '_')) ";            
                       tss += "ELSE REPLACE(TRIM(CODE), ' ', '_') END AS CUSTCODE , TRIM(TO_NCHAR(NAMEEN)) AS CUSTNAME,  'OPICINF' AS SOURCE ";
                       tss += "FROM OPICINF.KKB_TB_CUSTOMER ";
                       tss += "WHERE (UPPER(NAMETH) LIKE UPPER(:findCust) OR UPPER(NAMEEN) LIKE UPPER(:findCust) OR UPPER(CODE) LIKE UPPER(:findCust) ) ";

                if (resource != null && resource.Equals("opic"))
                {
                    sql = opic; 
                }else if(resource != null && resource.Equals("tss")){
                    sql = tss;
                }
                else {
                    sql = opic + " UNION " + tss;
                }
             
                Log.Info("sql GetCustomerAllSource : " + sql);
                o.openConnection();
                o.createCommand(sql, CommandType.Text);  
                o.setNCharInputParameter("findCust", "%" + findCust.Trim() + "%");
                s = o.executeReader();
                return s.Tables[0];

            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }


    }
}
