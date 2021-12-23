using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class BrokerageFee
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BrokerageFee));

        public int DEALFEEID { get; set; }
        public string BR { get; set; }
        public string COST { get; set; }
        public string PORT { get; set; }
        public string DEALNO { get; set; }
        public string PRODUCT { get; set; }
        public string PRODTYPE { get; set; }
        public string TENOR { get; set; }
        public string DEALDATE { get; set; }
        public string SETTDATE { get; set; }
        public string PS { get; set; }
        public string SECID { get; set; }
        public string CUST { get; set; }
        public string NOTIONAL { get; set; }
        public string CCY { get; set; }
        public string CTRNOTIONAL { get; set; }
        public string CTRCCY { get; set; }
        public string METHOD { get; set; }
        public string BROK { get; set; }
        public string MDATE { get; set; }
        public string NEARSETTDATE { get; set; }
        public string DEALNOTE { get; set; }
        public string DURATION { get; set; }
        public string FEE { get; set; }
        public string TSD_FEE { get; set; }
        public string FREEFEE { get; set; }

        public static object BrokerageFeeList(string ConnectionString, string DealNo, string DealDate, string Brok, string ProdType, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_DEALFEE Where 1=1 ";
                if (DealNo != "")
                    sql += "And TRIM(DealNo)='" + DealNo.Trim() + "' ";
                if (DealDate != "")
                    sql += "And DealDate=TO_DATE('" + DealDate + "','DD/MM/YYYY') ";
                if (Brok != "")
                    sql += "And TRIM(Brok)='" + Brok.ToUpperInvariant() + "' ";
                if (ProdType != "")
                    sql += "And PRODTYPE='" + ProdType.ToUpperInvariant() + "' ";
                else
                    sql += "And PRODTYPE IN ('FX','SW')";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("DealFee");

                List<BrokerageFee> param = new List<BrokerageFee>();
                int rownum = dt.Rows.Count > 500 ? 500 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    BrokerageFee p = new BrokerageFee();
                    p.DEALFEEID = int.Parse(dt.Rows[i]["DEALFEEID"].ToString());
                    p.BR = dt.Rows[i]["BR"].ToString();
                    p.COST = dt.Rows[i]["COST"].ToString();
                    p.PORT = dt.Rows[i]["PORT"].ToString();
                    p.DEALNO = dt.Rows[i]["DEALNO"].ToString();
                    p.PRODUCT = dt.Rows[i]["PRODUCT"].ToString();
                    p.PRODTYPE = dt.Rows[i]["PRODTYPE"].ToString();
                    p.TENOR = dt.Rows[i]["TENOR"].ToString();
                    p.DEALDATE = dt.Rows[i]["DEALDATE"].ToString();
                    p.SETTDATE = dt.Rows[i]["SETTDATE"].ToString();
                    p.PS = dt.Rows[i]["PS"].ToString();
                    p.SECID = dt.Rows[i]["SECID"].ToString();
                    p.CUST = dt.Rows[i]["CUST"].ToString();
                    p.NOTIONAL = dt.Rows[i]["NOTIONAL"].ToString();
                    p.CCY = dt.Rows[i]["CCY"].ToString();
                    p.CTRNOTIONAL = dt.Rows[i]["CTRNOTIONAL"].ToString();
                    p.CTRCCY = dt.Rows[i]["CTRCCY"].ToString();
                    p.METHOD = dt.Rows[i]["METHOD"].ToString();
                    p.BROK = dt.Rows[i]["BROK"].ToString().Trim();
                    p.MDATE = dt.Rows[i]["MDATE"].ToString();
                    p.NEARSETTDATE = dt.Rows[i]["NEARSETTDATE"].ToString();
                    p.DEALNOTE = dt.Rows[i]["DEALNOTE"].ToString();
                    p.DURATION = dt.Rows[i]["DURATION"].ToString();
                    p.FEE = dt.Rows[i]["FEE"].ToString();
                    p.TSD_FEE = dt.Rows[i]["TSD_FEE"].ToString();
                    p.FREEFEE = dt.Rows[i]["FREEFEE"].ToString(); 
                    
                    param.Add(p);
                }

                if (jtSorting.Equals("DEALNO ASC"))
                {
                    param = param.OrderBy(p => p.DEALNO).ToList<BrokerageFee>();
                }
                else if (jtSorting.Equals("DEALNO DESC"))
                {
                    param = param.OrderByDescending(p => p.DEALNO).ToList<BrokerageFee>();
                }

                //Return result to jTable
                return new { Result = "OK", Records = jtPageSize > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList() : param, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object CreateBrokerageFee(string ConnectionString, BrokerageFee record)
        {
            
            Log.Info("-----KKB_TB_WEBPARAM Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_TR100OBOBRMA", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "insert");
                o.setInputParameter("p_dealno", record.DEALNO);
                o.setInputParameter("p_broker", record.BROK);
                o.setInputParameter("p_dealfee", "");
                o.setInputParameter("p_duration", "");
                o.setInputParameter("p_fee", "");
                row = o.executeNonQuery("r_rows");
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

        public static object UpdateBrokerageFee(string ConnectionString, BrokerageFee record)
        {
            Log.Info("-----KKB_TB_DEALFEE Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                o.createCommand("OPICINF.KKB_SP_TSS_TR100OBOBRMA", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "update");
                o.setInputParameter("p_dealno", record.DEALNO);
                o.setInputParameter("p_broker", record.BROK);
                o.setInputParameter("p_dealfee", record.DEALFEEID);
                o.setInputParameter("p_duration", record.DURATION);
                o.setInputParameter("p_fee", record.FEE);
                row = o.executeNonQuery("r_rows");
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_DEALFEE Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteBrokerageFee(string ConnectionString, int DealFeeID)
        {
            
            Log.Info("-----KKB_TB_DEALFEE Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                o.createCommand("OPICINF.KKB_SP_TSS_TR100OBOBRMA", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "delete");
                o.setInputParameter("p_dealno", "");
                o.setInputParameter("p_broker", "");
                o.setInputParameter("p_dealfee", DealFeeID);
                o.setInputParameter("p_duration", "");
                o.setInputParameter("p_fee", "");
                row = o.executeNonQuery("r_rows");
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_DEALFEE Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

        public static int CalculateFee(string ConnectionString, string Product, string FromDate, string ToDate)
        {
            string StoreProcedure = "";
            Log.Info("-----Brokerage Fee Start-----");
            if (Product.Equals("FI")) {
                Log.Info("Store Procedure : OPICINF.KKB_SP_TSS_CALCFEE_FI");
                StoreProcedure = "OPICINF.KKB_SP_TSS_CALCFEE_FI";
            }else if (Product.Equals("FX")) {
                Log.Info("Store Procedure : OPICINF.KKB_SP_TSS_CALCFEE_FX");
                StoreProcedure = "OPICINF.KKB_SP_TSS_CALCFEE_FX";
            }
            Log.Info("Data : Execute Date=" + FromDate + " - " + ToDate);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(StoreProcedure, CommandType.StoredProcedure);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                int row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return 0;
            }
            finally
            {
                Log.Info("-----Brokerage Fee Finish-----");
                o.closeConnection();
            }
        }

        public static object NoFeeBrokerageFee(string ConnectionString, string DealFee)
        {
            String sql = "Update OPICINF.KKB_TB_DEALFEE Set FEE=0, FREEFEE='1', EDITING='1' Where DEALFEEID IN (" + DealFee + ") ";

            Log.Info("-----KKB_TB_DEALFEE Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Set NoFree." };
            }
            catch (Exception ex)
            {
                Log.Error("-----FreeFee KKTB_TB_DEALFEE Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_DEALFEE Finish-----");
                o.closeConnection();
            }
        }

    }
}
