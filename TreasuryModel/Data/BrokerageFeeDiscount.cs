using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Threading;
using System.Globalization;

namespace TreasuryModel.Data
{
    public class BrokerageFeeDiscount
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BrokerageFeeDiscount));
        public int DISCOUNTID { get; set; } 
        public string BROK { get; set; } 
        public string PLAN { get; set; } 
        public string DISCTYPE { get; set; } 
        public string DISCOUNT { get; set; } 
        public string MINAMT { get; set; } 
        public string MAXAMT { get; set; } 
        public string PRODTYPE { get; set; } 
        public DateTime EFFSTARTDATE { get; set; }
        public DateTime EFFENDDATE { get; set; }
        public string ENABLED { get; set; }

        public static object DiscountList(string ConnectionString, string brok, string prodtype, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select DISCOUNTID, BROK, PLAN, DISCTYPE, DISCOUNT, MINAMT, MAXAMT, PRODTYPE, EFFSTARTDATE , EFFENDDATE, ENABLED From OPICINF.KKB_TB_DEALFEE_DISC WHERE BROK like '" + brok + "%' AND PRODTYPE='" + prodtype + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("DealFeeDisc");

                List<BrokerageFeeDiscount> param = new List<BrokerageFeeDiscount>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BrokerageFeeDiscount p = new BrokerageFeeDiscount();
                    p.DISCOUNTID = int.Parse(dt.Rows[i]["DISCOUNTID"].ToString());
                    p.BROK = dt.Rows[i]["BROK"].ToString();
                    p.PLAN = dt.Rows[i]["PLAN"].ToString();
                    p.DISCTYPE = dt.Rows[i]["DISCTYPE"].ToString();
                    p.DISCOUNT = dt.Rows[i]["DISCOUNT"].ToString();
                    p.MINAMT = dt.Rows[i]["MINAMT"].ToString();
                    p.MAXAMT = dt.Rows[i]["MAXAMT"].ToString();
                    p.PRODTYPE = dt.Rows[i]["PRODTYPE"].ToString();
                    if (!dt.Rows[i]["EFFSTARTDATE"].ToString().Equals(""))
                        p.EFFSTARTDATE = Convert.ToDateTime(dt.Rows[i]["EFFSTARTDATE"].ToString());
                    if (!dt.Rows[i]["EFFENDDATE"].ToString().Equals(""))
                        p.EFFENDDATE = Convert.ToDateTime(dt.Rows[i]["EFFENDDATE"].ToString());
                    p.ENABLED = dt.Rows[i]["ENABLED"].ToString();

                    param.Add(p);
                }

                //Return result to jTable
                return new { Result = "OK", Records = jtPageSize > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList() : param, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                //Update 2017-02-08
                Log.Error("-----Insert KKTB_TB_DEALFEEDISC Error-----");
                Log.Error(ex.Message);
                return null;
            }
        }

        public static object CreateDiscount(string ConnectionString, BrokerageFeeDiscount record)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            String sql = "Insert Into OPICINF.KKB_TB_DEALFEE_DISC(BROK, PLAN, DISCTYPE, DISCOUNT, MINAMT, MAXAMT, PRODTYPE, EFFSTARTDATE, EFFENDDATE, ENABLED) ";
            sql += " Values(:BROK, :PLAN, :DISCTYPE, :DISCOUNT, :MINAMT, :MAXAMT, :PRODTYPE, TO_DATE(:EFFSTARTDATE,'YYYY-MM-DD'), TO_DATE(:EFFENDDATE, 'YYYY-MM-DD'), :ENABLED)";

            Log.Info("-----KKB_TB_DEALFEECONF Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("BROK", record.BROK);
                o.setVarCharInputParameter("PLAN", record.PLAN);
                o.setVarCharInputParameter("DISCTYPE", record.DISCTYPE);
                o.setVarCharInputParameter("DISCOUNT", record.DISCOUNT);
                o.setVarCharInputParameter("MINAMT", record.MINAMT);
                o.setVarCharInputParameter("MAXAMT", record.MAXAMT);
                o.setVarCharInputParameter("PRODTYPE", record.PRODTYPE);
                o.setVarCharInputParameter("EFFSTARTDATE", record.EFFSTARTDATE.ToString("yyyy-MM-dd"));
                o.setVarCharInputParameter("EFFENDDATE", record.EFFENDDATE.ToString("yyyy-MM-dd"));
                o.setVarCharInputParameter("ENABLED", record.ENABLED);
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

        public static object UpdateDiscount(string ConnectionString, BrokerageFeeDiscount record)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            String sql = "Update OPICINF.KKB_TB_DEALFEE_DISC Set BROK=:BROK, PLAN=:PLAN, DISCTYPE=:DISCTYPE, DISCOUNT=:DISCOUNT, MINAMT=:MINAMT, MAXAMT=:MAXAMT, PRODTYPE=:PRODTYPE, EFFSTARTDATE=TO_DATE(:EFFSTARTDATE,'YYYY-MM-DD'), EFFENDDATE=TO_DATE(:EFFENDDATE, 'YYYY-MM-DD'), ENABLED=:ENABLED Where DISCOUNTID=:DISCOUNTID ";

            Log.Info("-----KKB_TB_DEALFEE_DISC Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text); 
                o.setVarCharInputParameter("BROK", record.BROK);
                o.setVarCharInputParameter("PLAN", record.PLAN);
                o.setVarCharInputParameter("DISCTYPE", record.DISCTYPE);
                o.setVarCharInputParameter("DISCOUNT", record.DISCOUNT);
                o.setVarCharInputParameter("MINAMT", record.MINAMT);
                o.setVarCharInputParameter("MAXAMT", record.MAXAMT);
                o.setVarCharInputParameter("PRODTYPE", record.PRODTYPE);
                o.setVarCharInputParameter("EFFSTARTDATE", record.EFFSTARTDATE.ToString("yyyy-MM-dd"));
                o.setVarCharInputParameter("EFFENDDATE", record.EFFENDDATE.ToString("yyyy-MM-dd"));
                o.setVarCharInputParameter("ENABLED", record.ENABLED);
                o.setIntegerInputParameter("DISCOUNTID", record.DISCOUNTID);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_DEALFEE_DISC Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE_DISC Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteDiscount(string ConnectionString, int DiscountID)
        {
            String sql = "Delete From OPICINF.KKB_TB_DEALFEE_DISC Where DiscountID=:DiscountID ";

            Log.Info("-----KKB_TB_DEALFEE_DISC Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("DealFeeConfID", DiscountID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_DEALFEE_DISC Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE_DISC Finish-----");
                o.closeConnection();
            }
        }
    }
}
