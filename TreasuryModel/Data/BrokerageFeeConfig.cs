using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class BrokerageFeeConfig
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BrokerageFeeConfig));

        public int DEALFEECONFID { get; set; } 
        public string BROK { get; set; }
        public string SECTYPE { get; set; }
        public string LEG { get; set; }
        public string PERUNIT { get; set; }
        public string MINAGE { get; set; }
        public string MAXAGE { get; set; }
        public string BYAGE { get; set; }
        public string BYUNIT { get; set; }
        public string FEE { get; set; }
        public string PRODTYPE { get; set; }
        public string EFFSTARTDATE { get; set; }
        public string EFFENDDATE { get; set; }
        public string PAYPERIOD { get; set; }
        public string ENABLED { get; set; }


        public static object ConfigList(string ConnectionString, string brok, string prodtype, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_DEALFEECONF WHERE BROK like '" + brok + "%' AND PRODTYPE='" + prodtype + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("DealFeeConf");

                List<BrokerageFeeConfig> param = new List<BrokerageFeeConfig>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BrokerageFeeConfig p = new BrokerageFeeConfig();
                    p.DEALFEECONFID = int.Parse(dt.Rows[i]["DEALFEECONFID"].ToString());
                    p.BROK = dt.Rows[i]["BROK"].ToString();
                    p.SECTYPE = dt.Rows[i]["SECTYPE"].ToString();
                    p.LEG = dt.Rows[i]["LEG"].ToString();
                    p.PERUNIT = dt.Rows[i]["PERUNIT"].ToString();
                    p.MINAGE = dt.Rows[i]["MINAGE"].ToString();
                    p.MAXAGE = dt.Rows[i]["MAXAGE"].ToString();
                    p.BYAGE = dt.Rows[i]["BYAGE"].ToString();
                    p.BYUNIT = dt.Rows[i]["BYUNIT"].ToString();
                    p.FEE = dt.Rows[i]["FEE"].ToString();
                    p.PRODTYPE = dt.Rows[i]["PRODTYPE"].ToString();
                    p.EFFSTARTDATE = dt.Rows[i]["EFFSTARTDATE"].ToString();
                    p.EFFENDDATE = dt.Rows[i]["EFFENDDATE"].ToString();
                    p.ENABLED = dt.Rows[i]["ENABLED"].ToString();
                    
                    param.Add(p);
                }

                //Return result to jTable
                return new { Result = "OK", Records = jtPageSize > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList() : param, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object CreateConfig(string ConnectionString, BrokerageFeeConfig record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_DEALFEECONF(BROK, SECTYPE, LEG, PERUNIT, MINAGE, MAXAGE, BYAGE, BYUNIT, FEE, PRODTYPE, EFFSTARTDATE, EFFENDDATE, ENABLED, PAYPERIOD) ";
            sql += " Values( :BROK, :SECTYPE, :LEG, :PERUNIT, :MINAGE, :MAXAGE, :BYAGE, :BYUNIT, :FEE, :PRODTYPE, :EFFSTARTDATE, :EFFENDDATE, :ENABLED, :PAYPERIOD)";

            Log.Info("-----KKB_TB_DEALFEECONF Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("BROK", record.BROK);
                o.setVarCharInputParameter("SECTYPE", record.SECTYPE);
                o.setVarCharInputParameter("LEG", record.LEG);
                o.setVarCharInputParameter("PERUNIT", record.PERUNIT);
                o.setVarCharInputParameter("MINAGE", record.MINAGE);
                o.setVarCharInputParameter("MAXAGE", record.MAXAGE);
                o.setVarCharInputParameter("BYAGE", record.BYAGE);
                o.setVarCharInputParameter("BYUNIT", record.BYUNIT);
                o.setVarCharInputParameter("FEE", record.FEE);
                o.setVarCharInputParameter("PRODTYPE", record.PRODTYPE);
                o.setVarCharInputParameter("EFFSTARTDATE", record.EFFSTARTDATE);
                o.setVarCharInputParameter("EFFENDDATE", record.EFFENDDATE);
                o.setVarCharInputParameter("ENABLED", record.ENABLED);
                o.setVarCharInputParameter("PAYPERIOD", record.PAYPERIOD);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_DEALFEE Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdateConfig(string ConnectionString, BrokerageFeeConfig record)
        {
            String sql = "Update OPICINF.KKB_TB_DEALFEECONF Set BROK=:BROK, SECTYPE=:SECTYPE, LEG=:LEG, PERUNIT=:PERUNIT, MINAGE=:MINAGE, MAXAGE=:MAXAGE, BYAGE=:BYAGE, BYUNIT=:BYUNIT, FEE=:FEE, PRODTYPE=:PRODTYPE, EFFSTARTDATE=:EFFSTARTDATE, EFFENDDATE=:EFFENDDATE, ENABLED=:ENABLED, PAYPERIOD=:PAYPERIOD Where DEALFEECONFID=:DEALFEECONFID ";

            Log.Info("-----KKB_TB_DEALFEECONF Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("BROK", record.BROK);
                o.setVarCharInputParameter("SECTYPE", record.SECTYPE);
                o.setVarCharInputParameter("LEG", record.LEG);
                o.setVarCharInputParameter("PERUNIT", record.PERUNIT);
                o.setVarCharInputParameter("MINAGE", record.MINAGE);
                o.setVarCharInputParameter("MAXAGE", record.MAXAGE);
                o.setVarCharInputParameter("BYAGE", record.BYAGE);
                o.setVarCharInputParameter("BYUNIT", record.BYUNIT);
                o.setVarCharInputParameter("FEE", record.FEE);
                o.setVarCharInputParameter("PRODTYPE", record.PRODTYPE);
                o.setVarCharInputParameter("EFFSTARTDATE", record.EFFSTARTDATE);
                o.setVarCharInputParameter("EFFENDDATE", record.EFFENDDATE);
                o.setVarCharInputParameter("ENABLED", record.ENABLED);
                o.setVarCharInputParameter("PAYPERIOD", record.PAYPERIOD);
                o.setIntegerInputParameter("DEALFEECONFID", record.DEALFEECONFID);
                
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_DEALFEECONF Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEECONF Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteConfig(string ConnectionString, int DEALFEECONFID)
        {
            String sql = "Delete From OPICINF.KKB_TB_DEALFEECONF Where DEALFEECONFID=:DEALFEECONFID ";

            Log.Info("-----KKB_TB_DEALFEECONF Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("DEALFEECONFID", DEALFEECONFID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_DEALFEECONF Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEECONF Finish-----");
                o.closeConnection();
            }
        }
    }
}
