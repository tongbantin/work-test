using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Globalization;
namespace TreasuryModel.Data
{
    public class AdjustNostro
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(AdjustNostro));

        public int ADJNOSTROID { get; set; }
        public string REFNO { get; set; }
        public DateTime SETTDATE { get; set; }
        public DateTime POSTDATE { get; set; }
        public int ACCTCODEID { get; set; }
        public int ADJACCTCODEID { get; set; }
        public string DRCR { get; set; }
        public decimal AMOUNT { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime CREATEDATE { get; set; }
        public string CREATEBY { get; set; }
        public DateTime REVIEWDATE { get; set; }
        public string REVIEWBY { get; set; }
        public DateTime AUTHORIZEDATE { get; set; }
        public string AUTHORIZEBY { get; set; }
        public string CANCELDAY { get; set; }
        public string ENABLED { get; set; }

        public static object AdjustNostroList(string ConnectionString, string Account, string FromDate, string ToDate, string Status, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AdjustNostro> param = new List<AdjustNostro>();
            try
            {
                string sql = "Select ADJNOSTROID, REFNO, TO_CHAR(SETTDATE,'MM/DD/YYYY') as SETTDATE, TO_CHAR(POSTDATE,'MM/DD/YYYY') as POSTDATE, ACCTCODEID, ADJACCTCODEID, DRCR, AMOUNT, DESCRIPTION, TO_CHAR(CREATEDATE,'MM/DD/YYYY') as CREATEDATE, TRIM(CREATEBY) as CREATEBY, TO_CHAR(REVIEWDATE,'MM/DD/YYYY') as REVIEWDATE, TRIM(REVIEWBY) as REVIEWBY, TO_CHAR(AUTHORIZEDATE,'MM/DD/YYYY') as AUTHORIZEDATE, TRIM(AUTHORIZEBY) as AUTHORIZEBY, CANCELDAY, ENABLED ";
                sql += " From OPICINF.KKB_TB_ADJNOSTRO Where ADJAcctCodeID='" + Account + "' ";
                if (!Status.Equals(""))
                    sql += " AND ENABLED='" + Status + "' ";
                if (!FromDate.Equals(""))
                    sql += " AND POSTDATE BETWEEN TO_DATE('" + FromDate + "','DD/MM/YYYY') AND TO_DATE('" + ToDate + "','DD/MM/YYYY')";
                sql += " Order By POSTDATE";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("AdjustNostro");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdjustNostro m = new AdjustNostro();
                    m.ADJNOSTROID = int.Parse(dt.Rows[i]["ADJNOSTROID"].ToString());
                    m.REFNO = dt.Rows[i]["REFNO"].ToString();
                    m.SETTDATE = DateTime.ParseExact(dt.Rows[i]["SETTDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    m.POSTDATE = DateTime.ParseExact(dt.Rows[i]["POSTDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    m.ACCTCODEID = int.Parse(dt.Rows[i]["ACCTCODEID"].ToString());
                    m.ADJACCTCODEID = int.Parse(dt.Rows[i]["ADJACCTCODEID"].ToString());
                    m.DRCR = dt.Rows[i]["DRCR"].ToString();
                    m.AMOUNT = Decimal.Parse(dt.Rows[i]["AMOUNT"].ToString());
                    m.DESCRIPTION = dt.Rows[i]["DESCRIPTION"].ToString();
                    m.CREATEBY = dt.Rows[i]["CREATEBY"].ToString().Trim();
                    m.REVIEWBY = dt.Rows[i]["REVIEWBY"].ToString().Trim();
                    m.AUTHORIZEBY = dt.Rows[i]["AUTHORIZEBY"].ToString().Trim();
                    if (!dt.Rows[i]["CREATEDATE"].ToString().Trim().Equals(""))
                        m.CREATEDATE = DateTime.ParseExact(dt.Rows[i]["CREATEDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    if (!dt.Rows[i]["REVIEWDATE"].ToString().Trim().Equals(""))
                        m.REVIEWDATE = DateTime.ParseExact(dt.Rows[i]["REVIEWDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    if (!dt.Rows[i]["AUTHORIZEDATE"].ToString().Trim().Equals(""))
                        m.AUTHORIZEDATE = DateTime.ParseExact(dt.Rows[i]["AUTHORIZEDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    m.CANCELDAY = dt.Rows[i]["CANCELDAY"].ToString();
                    m.ENABLED = dt.Rows[i]["ENABLED"].ToString(); 
                    param.Add(m);
                }

                var rec = param.Count > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList<AdjustNostro>() : param.ToList<AdjustNostro>();

                return new { Result = "OK", Records = rec, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return new { Result = "OK", Records = param, TotalRecordCount = param.Count };
            }
        }

        public static object CreateAdjustNostro(string ConnectionString, AdjustNostro record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_ADJNOSTRO(REFNO, SETTDATE, POSTDATE, ACCTCODEID, ADJACCTCODEID, DRCR, AMOUNT, DESCRIPTION, CREATEDATE, CREATEBY, CANCELDAY, ENABLED) ";
            sql += " Values(:REFNO, TO_DATE(:SETTDATE,'DD/MM/YYYY'), TO_DATE(:POSTDATE,'DD/MM/YYYY'), :ACCTCODEID, :ADJACCTCODEID, :DRCR, :AMOUNT, :DESCRIPTION, SYSDATE, :CREATEBY, :CANCELDAY, '1')";

            Log.Info("-----KKTB_TB_ADJNOSTRO Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("REFNO", "ADJ" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                o.setVarCharInputParameter("SETTDATE", record.SETTDATE.ToString("dd/MM/yyyy"));
                o.setVarCharInputParameter("POSTDATE", record.POSTDATE.ToString("dd/MM/yyyy"));
                o.setVarCharInputParameter("ACCTCODEID", record.ACCTCODEID);
                o.setVarCharInputParameter("ADJACCTCODEID", record.ADJACCTCODEID);
                o.setVarCharInputParameter("DRCR", record.DRCR);
                o.setDecimalInputParameter("AMOUNT", record.AMOUNT);
                o.setNCharInputParameter("DESCRIPTION", record.DESCRIPTION);
                o.setVarCharInputParameter("CREATEBY", record.CREATEBY);
                o.setVarCharInputParameter("CANCELDAY", record.CANCELDAY);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_ADJNOSTRO Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_ADJNOSTRO Finish-----");
                o.closeConnection();
            }
        }

        

        public static object UpdateAdjustNostro(string ConnectionString, AdjustNostro record, String Executor)
        {
            bool IsReview = AdjustNostro.IsReview(ConnectionString, record.ADJNOSTROID);
            bool IsAuthorize = AdjustNostro.IsAuthorize(ConnectionString, record.ADJNOSTROID);

            if ((!IsReview) || (!IsAuthorize))
            {
                String sql = "Update OPICINF.KKB_TB_ADJNOSTRO Set ";
                if (!IsReview)
                    sql += " ReviewDate=SYSDATE, ReviewBy='" + Executor + "' ";
                else if (!IsAuthorize)
                    sql += " AuthorizeDate=SYSDATE, AuthorizeBy='" + Executor + "' ";
                sql += "Where ADJNOSTROID='" + record.ADJNOSTROID + "'";

                Log.Info("-----KKTB_TB_ADJNOSTRO Start update-----");
                Log.Info("SQL Statement : " + sql);

                int row = 0;
                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    row = o.executeNonQuery();
                    if (row > 0)
                        return new { Result = "OK", Record = record };
                    else
                        return new { Result = "ERROR", Message = "Could not insert data." };
                }
                catch (Exception ex)
                {
                    Log.Error("-----Insert KKTB_TB_ADJNOSTRO Error-----");
                    Log.Error(ex.Message);
                    return new { Result = "ERROR", Message = ex.Message };
                }
                finally
                {
                    Log.Info("-----KKTB_TB_ADJNOSTRO Finish-----");
                    o.closeConnection();
                }
            }
            else
            {
                return new { Result = "ERROR", Message = "This Transaction is complete!!" };
            }
        }

        public static object DeleteAdjustNostro(string ConnectionString, int AdjNostroID)
        {
            //String sql = "UPDATE OPICINF.KKB_TB_ADJNOSTRO SET ENABLED='0' Where AdjNostroID=:AdjNostroID ";
            String sql = "DELETE FROM OPICINF.KKB_TB_ADJNOSTRO Where AdjNostroID=:AdjNostroID ";

            Log.Info("-----KKB_TB_MS_LOOKUP Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("AdjNostroID", AdjNostroID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_ADJNOSTRO Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_MS_LOOKUP Finish-----");
                o.closeConnection();
            }
        }

        public static object GetAccountCode(string ConnectionString, string acctgrp)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AdjustNostro> param = new List<AdjustNostro>();
            try
            {
                string sql = "SELECT ACCTCODEID , TRIM(NVL(ACCTCODE,'') || '(' || NVL(ACCTDESC,'') || ')') as ACCTDESC FROM OPICINF.KKB_TB_ACCTCODE WHERE ACCTGRP='" + acctgrp + "' ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("ACCTCODE" + acctgrp);

                var objlist = dt.Select().ToList().Select(c => new { DisplayText = c["ACCTDESC"].ToString(), Value = c["ACCTCODEID"].ToString() });
                //var cities = _repository.CityRepository.GetAllCities().Select(c => new { DisplayText = c.CityName, Value = c.CityId });
                return new { Result = "OK", Options = objlist };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_ACCTCODE Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static DataTable GetAccount(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AdjustNostro> param = new List<AdjustNostro>();
            try
            {
                string sql = "SELECT ACCTCODEID , TRIM(NVL(ACCTCODE,'') || '(' || NVL(ACCTDESC,'') || ')') as ACCTDESC FROM OPICINF.KKB_TB_ACCTCODE WHERE ACCTGRP='1' ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("ACCTCODE : " + 1);

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error("-----Get KKTB_TB_ACCTCODE Error-----");
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable GetCancelNo(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AdjustNostro> param = new List<AdjustNostro>();
            try
            {
                string sql = "SELECT CANCELNO , TRIM(NVL(CANCELNO,'') || '(' || NVL(CANCELBY,'') || ')') as CANCELDESC FROM OPICINF.KKB_TB_NOSTROAVGHIS GROUP BY CANCELNO, CANCELBY ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KKB_TB_NOSTROAVGHIS");

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error("-----Get KKB_TB_NOSTROAVGHIS Error-----");
                Log.Error(ex.Message);
                return null;
            }
        }

        public static bool IsReview(string ConnectionString, int AdjNostroID)
        {
            String sql = "Select count(*) From OPICINF.KKB_TB_ADJNOSTRO Where ADJNOSTROID='" + AdjNostroID + "' and ReviewBy IS NOT NULL ";

            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                int rows = int.Parse(o.executeScalar().ToString());
                if (rows <= 0)
                    return false;
                else
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
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }

        public static bool IsAuthorize(string ConnectionString, int AdjNostroID)
        {
            String sql = "Select count(*) From OPICINF.KKB_TB_ADJNOSTRO Where ADJNOSTROID='" + AdjNostroID + "' and AUTHORIZEBY IS NOT NULL ";

            Log.Info("-----KKB_TB_FXDEAL Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                int rows = int.Parse(o.executeScalar().ToString());
                if (rows <= 0)
                    return false;
                else
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
                Log.Info("-----KKB_TB_FXDEAL Finish-----");
                o.closeConnection();
            }
        }
    }
}
