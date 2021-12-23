using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class PositionDetail
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BESeries));

        public int DETAILID { get; set; }
        public string PRODUCT { get; set; }
        public string DEALNO { get; set; }
        public string SPOTFWDIND { get; set; }
        public DateTime DEALDATE { get; set; }
        public DateTime SETTLEDATE { get; set; }
        public DateTime REPORTDATE { get; set; }
        public string CCY { get; set; }
        public string PS { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal ADJAMOUNT { get; set; }
        public string PRODTYPE { get; set; }
        public string REMARK { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }

        public string DEALDATE_STR { get; set; }
        public string SETTLEDATE_STR { get; set; }
        public string REPORTDATE_STR { get; set; }

        public static List<PositionDetail> PositionDetailList(string ConnectionString, string DealNo, string ValueDate)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * FROM OPICINF.KKB_TB_POSITIONDETAIL WHERE 1=1 ";
                if (DealNo != "")
                    sql += "AND TRIM(DEALNO)='" + DealNo.Trim() + "'";
                if (ValueDate != "")
                    sql += "AND REPORTDATE=TO_DATE('" + ValueDate.Trim() + "','DD/MM/YYYY')";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PositionDetail");

                List<PositionDetail> param = new List<PositionDetail>();
                int rownum = dt.Rows.Count > 500 ? 500 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    PositionDetail p = new PositionDetail();
                    p.DETAILID = int.Parse(dt.Rows[i]["DETAILID"].ToString());
                    p.CCY = dt.Rows[i]["CCY"].ToString(); 
                    p.PS = dt.Rows[i]["PS"].ToString(); 
                    p.AMOUNT = decimal.Parse(dt.Rows[i]["AMOUNT"].ToString());
                    p.PRODTYPE = dt.Rows[i]["PRODTYPE"].ToString();
                    p.REMARK = dt.Rows[i]["REMARK"].ToString();
                    p.PRODUCT = dt.Rows[i]["PRODUCT"].ToString(); 
                    p.SPOTFWDIND = dt.Rows[i]["SPOTFWDIND"].ToString();
                    p.DEALNO = dt.Rows[i]["DEALNO"].ToString();
                    if (!dt.Rows[i]["DEALDATE"].ToString().Equals(""))
                    {
                        p.DEALDATE = DateTime.Parse(dt.Rows[i]["DEALDATE"].ToString());
                        p.DEALDATE_STR = p.DEALDATE.ToString("dd/MM/yyyy");
                    }
                    if (!dt.Rows[i]["SETTLEDATE"].ToString().Equals(""))
                    {
                        p.SETTLEDATE = DateTime.Parse(dt.Rows[i]["SETTLEDATE"].ToString());
                        p.SETTLEDATE_STR = p.SETTLEDATE.ToString("dd/MM/yyyy");
                    }
                    if (!dt.Rows[i]["REPORTDATE"].ToString().Equals(""))
                    {
                        p.REPORTDATE = DateTime.Parse(dt.Rows[i]["REPORTDATE"].ToString());
                        p.REPORTDATE_STR = p.REPORTDATE.ToString("dd/MM/yyyy");
                    }
                    if(!dt.Rows[i]["ADJAMOUNT"].ToString().Equals(""))                        
                        p.ADJAMOUNT = decimal.Parse(dt.Rows[i]["ADJAMOUNT"].ToString());
                    
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

        public static object getPositionDetail(string ConnectionString, int PositionDetailID)
        {
            String sql = "Select * FROM OPICINF.KKB_TB_POSITIONDETAIL WHERE DETAILID=" + PositionDetailID.ToString();
            Log.Info("-----KKB_TB_POSITIONDETAIL Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PositionDetail");

                List<PositionDetail> param = new List<PositionDetail>();
                PositionDetail p = new PositionDetail();
                p.DETAILID = int.Parse(dt.Rows[0]["DETAILID"].ToString());
                p.CCY = dt.Rows[0]["CCY"].ToString();
                p.PS = dt.Rows[0]["PS"].ToString();
                p.AMOUNT = decimal.Parse(dt.Rows[0]["AMOUNT"].ToString());
                p.PRODTYPE = dt.Rows[0]["PRODTYPE"].ToString();
                p.REMARK = dt.Rows[0]["REMARK"].ToString();
                p.PRODUCT = dt.Rows[0]["PRODUCT"].ToString();
                p.SPOTFWDIND = dt.Rows[0]["SPOTFWDIND"].ToString();
                p.DEALNO = dt.Rows[0]["DEALNO"].ToString();
                if (!dt.Rows[0]["DEALDATE"].ToString().Equals(""))
                {
                    p.DEALDATE = DateTime.Parse(dt.Rows[0]["DEALDATE"].ToString());
                    p.DEALDATE_STR = p.DEALDATE.ToString("dd/MM/yyyy");
                }
                if (!dt.Rows[0]["SETTLEDATE"].ToString().Equals(""))
                {
                    p.SETTLEDATE = DateTime.Parse(dt.Rows[0]["SETTLEDATE"].ToString());
                    p.SETTLEDATE_STR = p.SETTLEDATE.ToString("dd/MM/yyyy");
                }
                if (!dt.Rows[0]["REPORTDATE"].ToString().Equals(""))
                {
                    p.REPORTDATE = DateTime.Parse(dt.Rows[0]["REPORTDATE"].ToString());
                    p.REPORTDATE_STR = p.REPORTDATE.ToString("dd/MM/yyyy");
                }
                if (!dt.Rows[0]["ADJAMOUNT"].ToString().Equals(""))
                    p.ADJAMOUNT = decimal.Parse(dt.Rows[0]["ADJAMOUNT"].ToString());

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
                Log.Info("-----KKB_TB_POSITIONDETAIL Finish-----");
                o.closeConnection();
            }
        }

        public static object Create(string ConnectionString, PositionDetail record)
        {

            Log.Info("-----KKB_TB_POSITIONDETAIL Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "INSERT INTO OPICINF.KKB_TB_POSITIONDETAIL(DEALDATE, SETTLEDATE, EXECUTEDATE, REPORTDATE, PRODUCT, PRODTYPE, SPOTFWDIND, CCY, PS, AMOUNT, ADJAMOUNT, REMARK, CREATE_DATE, CREATE_USER) VALUES(TO_DATE(:DEALDATE,'DD/MM/YYYY'), TO_DATE(:SETTLEDATE,'DD/MM/YYYY'), TO_DATE(:EXECUTEDATE,'DD/MM/YYYY'), TO_DATE(:REPORTDATE,'DD/MM/YYYY'), :PRODUCT, :PRODTYPE, :SPOTFWDIND, :CCY, :PS, :AMOUNT, :ADJAMOUNT, :REMARK, SYSDATE, :CREATE_USER)";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("DEALDATE", record.DEALDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("SETTLEDATE", record.SETTLEDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("EXECUTEDATE", record.REPORTDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("REPORTDATE", record.REPORTDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("PRODUCT", "Adj");
                o.setInputParameter("PRODTYPE", "Adj");
                o.setInputParameter("SPOTFWDIND", record.SPOTFWDIND);
                o.setInputParameter("CCY", record.CCY.ToUpperInvariant());
                o.setInputParameter("PS", record.AMOUNT>0 ? 'P' : 'S');
                o.setInputParameter("AMOUNT", record.AMOUNT);
                o.setInputParameter("ADJAMOUNT", record.ADJAMOUNT.ToString().Equals("0") == true ? DBNull.Value.ToString() : record.ADJAMOUNT.ToString());
                o.setInputParameter("REMARK", record.REMARK);
                o.setInputParameter("CREATE_USER", record.CREATE_USER);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_POSITIONDETAIL Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_POSITIONDETAIL Finish-----");
                o.closeConnection();
            }
        }

        public static object Update(string ConnectionString, PositionDetail record)
        {

            Log.Info("-----KKB_TB_POSITIONDETAIL Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_POSITIONDETAIL SET DEALDATE=TO_DATE(:DEALDATE,'DD/MM/YYYY'), SETTLEDATE=TO_DATE(:SETTLEDATE,'DD/MM/YYYY'), EXECUTEDATE=TO_DATE(:EXECUTEDATE,'DD/MM/YYYY'),REPORTDATE=TO_DATE(:REPORTDATE,'DD/MM/YYYY'), SPOTFWDIND=:SPOTFWDIND, CCY=:CCY, PS=:PS, AMOUNT=:AMOUNT, ADJAMOUNT=:ADJAMOUNT, REMARK=:REMARK, UPDATE_DATE=SYSDATE, UPDATE_USER=:UPDATE_USER Where DETAILID=:DETAILID ";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("DEALDATE", record.DEALDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("SETTLEDATE", record.SETTLEDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("EXECUTEDATE", record.REPORTDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("REPORTDATE", record.REPORTDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("SPOTFWDIND", record.SPOTFWDIND);
                o.setInputParameter("CCY", record.CCY);
                o.setInputParameter("PS", record.AMOUNT > 0 ? 'P' : 'S');
                o.setInputParameter("AMOUNT", record.AMOUNT);
                o.setInputParameter("ADJAMOUNT", record.ADJAMOUNT.ToString().Equals("0")==true ? DBNull.Value.ToString() : record.ADJAMOUNT.ToString());
                o.setInputParameter("REMARK", record.REMARK);
                o.setInputParameter("UPDATE_USER", record.UPDATE_USER);
                o.setInputParameter("DETAILID", record.DETAILID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_POSITIONDETAIL Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_POSITIONDETAIL Finish-----");
                o.closeConnection();
            }
        }

        public static object Delete(string ConnectionString, int DETAILID)
        {

            Log.Info("-----KKB_TB_POSITIONDETAIL Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_POSITIONDETAIL WHERE DETAILID='" + DETAILID.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_POSITIONDETAIL Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_POSITIONDETAIL Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean ReImportPositionDetail(string ConnectionString, string ExecuteDate)
        {

            Log.Info("-----Position Detail Start Start-----");
            Log.Info("Store Procedure : " + "OPICINF.KKB_SP_TSS_CALCPOSITIONDETAIL");

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_CALCPOSITIONDETAIL", CommandType.StoredProcedure);
                o.setInputParameter("p_executedate", ExecuteDate);
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
