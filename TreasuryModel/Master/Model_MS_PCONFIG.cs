using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Globalization;

namespace KKB.Treasury.TreasuryModel.Master
{
    public class Model_MS_PCONFIG
    {

        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_PCONFIG));

        public int PCONFIG_ID { get; set; }
        public string BROKNAME { get; set;}
        public string SELLCODE { get; set; }
        public string BUYCODE { get; set; }
        public string VDATECODE { get; set; }
        public string TDATECODE { get; set; }
        public string RATECODE { get; set; }
        public string DATEFORMAT { get; set; }
        public string NOTE { get; set; }
        public string TYPESTYLE { get; set; }
        public string NF { get; set; }

        public static object Create(string ConnectionString, Model_MS_PCONFIG record)
        {
            Log.Info("-----KKB_TB_PCONF_CONFIG Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "INSERT INTO OPICINF.KKB_TB_PCONF_CONFIG ( BROKNAME, SELLCODE, BUYCODE,  VDATECODE, TDATECODE, RATECODE, DATEFORMAT, NOTE, TYPESTYLE , NF )";
                sql += " VALUES ( :BROKNAME, :SELLCODE, :BUYCODE , :VDATECODE ,:TDATECODE, :RATECODE , :DATEFORMAT , :NOTE, :TYPESTYLE, :NF )";

                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("BROKNAME", record.BROKNAME);
                o.setInputParameter("SELLCODE", record.SELLCODE);
                o.setInputParameter("BUYCODE", record.BUYCODE);
                o.setInputParameter("VDATECODE", record.VDATECODE);
                o.setInputParameter("TDATECODE", record.TDATECODE);
                o.setInputParameter("RATECODE", record.RATECODE);
                o.setInputParameter("DATEFORMAT", record.DATEFORMAT);
                o.setInputParameter("NOTE", record.NOTE);
                o.setInputParameter("TYPESTYLE", record.TYPESTYLE);
                o.setInputParameter("NF", record.NF);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_PCONF_CONFIG Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not insert data." };
            }
            finally
            {
                Log.Info("-----KKB_TB_PCONF_CONFIG Finish-----");
                o.closeConnection();
            }
        }

        public static List<Model_MS_PCONFIG> PConfigList(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT PCONFIG_ID ,BROKNAME , TYPESTYLE, NF, SELLCODE, BUYCODE,  VDATECODE, TDATECODE, RATECODE, DATEFORMAT, NOTE FROM OPICINF.KKB_TB_PCONF_CONFIG WHERE 1=1 ";
                /*
                if (!BrokName.Equals(""))
                {
                    sql += " AND TRIM(BROKNAME) = TRIM('"+BrokName+"')";
                }
                */
                sql += " ORDER BY BROKNAME ASC";

                Log.Info("----- PreConfig List -----");
                Log.Info("SQL Statement : " + sql);

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PConfig");

                List<Model_MS_PCONFIG> param = new List<Model_MS_PCONFIG>();
                int rownum = dt.Rows.Count > 100 ? 100 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    Model_MS_PCONFIG p = new Model_MS_PCONFIG();
                    p.PCONFIG_ID = int.Parse(dt.Rows[i]["PCONFIG_ID"].ToString());
                    p.BROKNAME = dt.Rows[i]["BROKNAME"].ToString();
                    p.TYPESTYLE = dt.Rows[i]["TYPESTYLE"].ToString();
                    p.NF = dt.Rows[i]["NF"].ToString();
                    p.SELLCODE = dt.Rows[i]["SELLCODE"].ToString();
                    p.BUYCODE = dt.Rows[i]["BUYCODE"].ToString();
                    p.VDATECODE = dt.Rows[i]["VDATECODE"].ToString();
                    p.TDATECODE = dt.Rows[i]["TDATECODE"].ToString();    
                    p.RATECODE = dt.Rows[i]["RATECODE"].ToString();                   
                    p.DATEFORMAT = dt.Rows[i]["DATEFORMAT"].ToString();                    
                    p.NOTE = dt.Rows[i]["NOTE"].ToString();

                    param.Add(p);
                }

                //Return result to jTable
                return param;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object getPConfig(string ConnectionString, int PCONFIG_ID)
        {
            String sql = "SELECT PCONFIG_ID ,BROKNAME ,TYPESTYLE, NF, SELLCODE, BUYCODE,  VDATECODE, TDATECODE, RATECODE, DATEFORMAT, NOTE FROM OPICINF.KKB_TB_PCONF_CONFIG WHERE PCONFIG_ID = " + PCONFIG_ID;
            Log.Info("----- getPreConf Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PConfig");

                List<Model_MS_PCONFIG> param = new List<Model_MS_PCONFIG>();
                Model_MS_PCONFIG p = new Model_MS_PCONFIG();
                p.PCONFIG_ID = int.Parse(dt.Rows[0]["PCONFIG_ID"].ToString());
                p.BROKNAME = dt.Rows[0]["BROKNAME"].ToString();
                p.TYPESTYLE = dt.Rows[0]["TYPESTYLE"].ToString();
                p.NF = dt.Rows[0]["NF"].ToString();
                p.SELLCODE = dt.Rows[0]["SELLCODE"].ToString();
                p.BUYCODE = dt.Rows[0]["BUYCODE"].ToString();
                p.VDATECODE = dt.Rows[0]["VDATECODE"].ToString();
                p.TDATECODE = dt.Rows[0]["TDATECODE"].ToString();
                p.RATECODE = dt.Rows[0]["RATECODE"].ToString();
                p.DATEFORMAT = dt.Rows[0]["DATEFORMAT"].ToString();
                p.NOTE = dt.Rows[0]["NOTE"].ToString();

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
                Log.Info("-----getPreConf Finish-----");
                o.closeConnection();
            }
        }

        public static object Update(string ConnectionString, Model_MS_PCONFIG record)
        {

            Log.Info("-----KKB_TB_PCONF_CONFIG Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_PCONF_CONFIG Set BROKNAME=:BROKNAME ,TYPESTYLE=:TYPESTYLE ,NF=:NF, SELLCODE=:SELLCODE, BUYCODE=:BUYCODE, VDATECODE=:VDATECODE ,TDATECODE=:TDATECODE , RATECODE=:RATECODE, DATEFORMAT=:DATEFORMAT, NOTE=:NOTE  Where PCONFIG_ID=:PCONFIG_ID ";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("BROKNAME", record.BROKNAME);
                o.setInputParameter("TYPESTYLE", record.TYPESTYLE);
                o.setInputParameter("NF", record.NF);
                o.setInputParameter("SELLCODE", record.SELLCODE);
                o.setInputParameter("BUYCODE", record.BUYCODE);
                o.setInputParameter("VDATECODE", record.VDATECODE);
                o.setInputParameter("TDATECODE", record.TDATECODE);    
                o.setInputParameter("RATECODE", record.RATECODE);
                o.setInputParameter("DATEFORMAT", record.DATEFORMAT);
                o.setInputParameter("NOTE", record.NOTE);
                o.setInputParameter("PCONFIG_ID", record.PCONFIG_ID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_PCONF_CONFIG Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_PCONF_CONFIG Finish-----");
                o.closeConnection();
            }
        }

        public static object Delete(string ConnectionString, int PCONFIG_ID)
        {

            Log.Info("-----KKB_TB_PCONF_CONFIG Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_PCONF_CONFIG WHERE PCONFIG_ID='" + PCONFIG_ID.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_PCONF_CONFIG Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_PCONF_CONFIG Finish-----");
                o.closeConnection();
            }
        }
    }
}
