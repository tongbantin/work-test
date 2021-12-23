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
    public class STNCONFIG
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(STNCONFIG));

        public int DOCID {get; set;}
        public string STN_TYPE {get; set;}
        public string STN_TYPE_FULLNAME { get; set; }
        public string STN_NAME {get; set;}
        public string STN_TYPE_DESC {get; set;}
        public string STN_RETURN {get; set;}
        public string STN_RULE_REDEEM {get; set;}
        public string CREATE_DATE {get; set;}
        public string CREATE_BY {get; set;}
        public string UPDATE_DATE {get; set;}
        public string UPDATE_BY { get; set; }


        public static DataTable Search(string type)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT DOCID,STN_TYPE,STN_TYPE_FULLNAME,STN_NAME,STN_TYPE_DESC,STN_RETURN,STN_RULE_REDEEM,CREATE_DATE,CREATE_BY,UPDATE_DATE,UPDATE_BY "
                          + " FROM OPICINF.KKB_TB_STN_COMF_CONFIG  WHERE 1=1 ";

                if (!string.IsNullOrEmpty(type))
                {

                    sql += " AND STN_TYPE = :STN_TYPE ";
                }

                sql += "ORDER BY DOCID ASC ";

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!string.IsNullOrEmpty(type))
                {

                    o.setVarCharInputParameter("STN_TYPE", type);
                }

                DataTable dt = o.executeReaderToDT("STNCONFIG");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static DataTable getConfigByID(string key)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT DOCID,STN_TYPE,STN_TYPE_FULLNAME,STN_NAME,STN_TYPE_DESC,STN_RETURN,STN_RULE_REDEEM,CREATE_DATE,CREATE_BY,UPDATE_DATE,UPDATE_BY "
                + " FROM OPICINF.KKB_TB_STN_COMF_CONFIG  WHERE 1=1 ";

                if (!string.IsNullOrEmpty(key))
                {

                    sql += " AND DOCID = :DOCID ";
                }


                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!string.IsNullOrEmpty(key))
                {

                    o.setIntegerInputParameter("DOCID", int.Parse(key));
                }

                DataTable dt = o.executeReaderToDT("STNCONFIG");

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static bool CheckSTNType(string type)
        {

            bool ishave = false;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_STN_COMF_CONFIG WHERE STN_TYPE = :STN_TYPE ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("STN_TYPE", type.Trim());

                DataTable dt = o.executeReaderToDT("STNCONFIG");

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

        public static bool AddConfig(STNCONFIG config)
        {

            Oracle o = Oracle.getInstance(ConnectionString);
            bool issuccess = false;
            try
            {

                o.openConnection();

                try
                {
                    string sql = "INSERT INTO OPICINF.KKB_TB_STN_COMF_CONFIG(STN_TYPE,STN_TYPE_FULLNAME,STN_NAME,STN_TYPE_DESC,STN_RETURN,STN_RULE_REDEEM,CREATE_DATE,CREATE_BY) ";
                    sql += " VALUES(:STN_TYPE,:STN_TYPE_FULLNAME,:STN_NAME,:STN_TYPE_DESC,:STN_RETURN,:STN_RULE_REDEEM,sysdate,:CREATE_BY) RETURNING DOCID INTO :DOCID";



                    o.createCommand(sql, CommandType.Text);

                    o.setVarCharInputParameter("STN_TYPE", config.STN_TYPE);
                    o.setVarCharInputParameter("STN_TYPE_FULLNAME", config.STN_TYPE_FULLNAME);
                    o.setNCharInputParameter("STN_NAME", config.STN_NAME);
                    o.setNCharInputParameter("STN_TYPE_DESC", config.STN_TYPE_DESC);
                    o.setNCharInputParameter("STN_RETURN", config.STN_RETURN);
                    o.setNCharInputParameter("STN_RULE_REDEEM", config.STN_RULE_REDEEM);
                    o.setVarCharInputParameter("CREATE_BY", config.CREATE_BY);




                    int result = o.executeNonQuery("DOCID");


                    if (result > 0)
                    {

                        config.DOCID = result;
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

        public static bool UpdateConfig(STNCONFIG config)
        {
            String sql = "UPDATE OPICINF.KKB_TB_STN_COMF_CONFIG SET STN_TYPE = :STN_TYPE,STN_TYPE_FULLNAME = :STN_TYPE_FULLNAME,STN_NAME=:STN_NAME,STN_TYPE_DESC = :STN_TYPE_DESC, "
                            + " STN_RETURN = :STN_RETURN,STN_RULE_REDEEM = :STN_RULE_REDEEM,UPDATE_DATE = sysdate,UPDATE_BY = :UPDATE_BY  "
                            + " WHERE DOCID = :DOCID ";





            Log.Info("-----KKB_TB_STN_COMF_CONFIG Start Update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);

                o.setVarCharInputParameter("STN_TYPE", config.STN_TYPE);
                o.setVarCharInputParameter("STN_TYPE_FULLNAME", config.STN_TYPE_FULLNAME);
                o.setNCharInputParameter("STN_NAME", config.STN_NAME);
                o.setNCharInputParameter("STN_TYPE_DESC", config.STN_TYPE_DESC);
                o.setNCharInputParameter("STN_RETURN", config.STN_RETURN);
                o.setNCharInputParameter("STN_RULE_REDEEM", config.STN_RULE_REDEEM);
                o.setVarCharInputParameter("UPDATE_BY", config.UPDATE_BY);
                o.setIntegerInputParameter("DOCID", config.DOCID);
                row = o.executeNonQuery();

                if (row > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_STN_COMF_CONFIG Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Update KKB_TB_STN_COMF_CONFIG Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Delete()
        {
            String sql = "DELETE FROM OPICINF.KKB_TB_STN_COMF_CONFIG WHERE DOCID ='" + this.DOCID + "'";
            Log.Info("-----KKB_TB_STN_COMF_CONFIG Start Delete-----");
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
                Log.Error("-----Delete KKB_TB_STN_COMF_CONFIG Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_STN_COMF_CONFIG Finish-----");
                o.closeConnection();
            }
        }

        public static List<STNCONFIG> ConvertDataTableToListConfig(DataTable dt)
        {

            List<STNCONFIG> result = new List<STNCONFIG>();
            STNCONFIG STNConfig = new STNCONFIG();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                STNConfig = new STNCONFIG();

                STNConfig.DOCID = string.IsNullOrEmpty(dt.Rows[i]["DOCID"].ToString()) ? 0 : int.Parse(dt.Rows[i]["DOCID"].ToString());
                STNConfig.STN_TYPE = string.IsNullOrEmpty(dt.Rows[i]["STN_TYPE"].ToString()) ? "" : dt.Rows[i]["STN_TYPE"].ToString();
                STNConfig.STN_TYPE_FULLNAME = string.IsNullOrEmpty(dt.Rows[i]["STN_TYPE_FULLNAME"].ToString()) ? "" : dt.Rows[i]["STN_TYPE_FULLNAME"].ToString();
                STNConfig.STN_NAME = string.IsNullOrEmpty(dt.Rows[i]["STN_NAME"].ToString()) ? "" : dt.Rows[i]["STN_NAME"].ToString();
                STNConfig.STN_TYPE_DESC = string.IsNullOrEmpty(dt.Rows[i]["STN_TYPE_DESC"].ToString()) ? "" : dt.Rows[i]["STN_TYPE_DESC"].ToString();
                STNConfig.STN_RETURN = string.IsNullOrEmpty(dt.Rows[i]["STN_RETURN"].ToString()) ? "" : dt.Rows[i]["STN_RETURN"].ToString();
                STNConfig.STN_RULE_REDEEM = string.IsNullOrEmpty(dt.Rows[i]["STN_RULE_REDEEM"].ToString()) ? "" : dt.Rows[i]["STN_RULE_REDEEM"].ToString();
                STNConfig.CREATE_DATE = string.IsNullOrEmpty(dt.Rows[i]["CREATE_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["CREATE_DATE"].ToString()).ToString("dd/MM/yyyy");
                STNConfig.CREATE_BY = string.IsNullOrEmpty(dt.Rows[i]["CREATE_BY"].ToString()) ? "" : dt.Rows[i]["CREATE_BY"].ToString();
                STNConfig.UPDATE_DATE = string.IsNullOrEmpty(dt.Rows[i]["UPDATE_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["UPDATE_DATE"].ToString()).ToString("dd/MM/yyyy");
                STNConfig.UPDATE_BY = string.IsNullOrEmpty(dt.Rows[i]["UPDATE_BY"].ToString()) ? "" : dt.Rows[i]["UPDATE_BY"].ToString();

                result.Add(STNConfig);

            }

            return result;

        }

    }
}
