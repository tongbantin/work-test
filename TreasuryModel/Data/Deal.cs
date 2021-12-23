using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Collections;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class Deal
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Deal));
        public int Records { get; set; }
        public int DealInfID { get; set; }
        public string DataDate { get; set; }
        public string Bu { get; set; }
        public string DealNo { get; set; }
        public string DealID { get; set; }
        public string PS { get; set; }
        public string ProdType { get; set; }
        public string CCYPair { get; set; }
        public string CCY { get; set; }
        public decimal Notional { get; set; }
        public string CMNE { get; set; }
        public string FlowDate { get; set; }
        public decimal FlowAmount { get; set; }

        public bool IsExisting { get; set; }

        public static List<Deal> DealList(string ConnectionString, string DataDate)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {   
                string sql = "Select * From OPICINF.KKB_TB_DEALINF Where 1=1 ";
                if (DataDate != "")
                    sql += "And DATADATE=TO_DATE('" + DataDate + "','DD/MM/YYYY')";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("DealINF"); 

                List<Deal> param = new List<Deal>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Deal md = new Deal();
                    md.DealInfID = int.Parse(dt.Rows[i]["DealInfID"].ToString());
                    try
                    {
                        md.DataDate = DateTime.Parse(dt.Rows[i]["DataDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                    catch (Exception ex)
                    {
                        md.DataDate = "01/01/1999";
                    }
                    md.Bu = dt.Rows[i]["BU"].ToString().Trim();
                    md.DealID = dt.Rows[i]["DealID"].ToString().Trim();
                    md.PS = dt.Rows[i]["PS"].ToString().Trim();
                    md.ProdType = dt.Rows[i]["ProdType"].ToString().Trim();
                    md.CCYPair = dt.Rows[i]["CCYPair"].ToString().Trim();
                    md.CCY = dt.Rows[i]["CCY"].ToString().Trim();
                    if (!dt.Rows[i]["Notional"].ToString().Trim().Equals(""))
                        md.Notional = decimal.Parse(dt.Rows[i]["Notional"].ToString().Trim());
                    else
                        md.Notional = 0;
                    md.CMNE = dt.Rows[i]["CMNE"].ToString().Trim();
                    try
                    {
                        md.FlowDate = DateTime.Parse(dt.Rows[i]["FlowDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                    catch (Exception ex)
                    {
                        md.FlowDate = "01/01/1999";
                    }
                    if (!dt.Rows[i]["FlowAmount"].ToString().Trim().Equals(""))
                        md.FlowAmount = decimal.Parse(dt.Rows[i]["FlowAmount"].ToString().Trim());
                    else
                        md.FlowAmount = 0;
                    param.Add(md);
                }

                return param;
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetDealID(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select DISTINCT DEALID From OPICINF.KKB_TB_DEALINF Where DEALID IS NOT NULL ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("DealINF");
                
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public Boolean Delete(string ConnectionString, string key)
        {
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Delete From OPICINF.KKB_TB_DEALINF WHERE DEALINFID in (" + key + ")";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_DEALINF Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_DEALINF Finish-----");
                o.closeConnection();
            }
        }

        public ArrayList save(string ConnectionString, List<Deal> deal)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();
                // Disabled Old Field

                int i = 0;

                foreach (Deal d in deal)
                {
                    try
                    {
                        string sql = "Insert Into OPICINF.KKB_TB_DEALINF(DATADATE, BU, DEALID, PS, PRODTYPE, CCYPAIR, CCY, NOTIONAL, CMNE, FLOWDATE, FLOWAMOUNT, CREATEDATE, ENABLED) ";
                        sql += " Values(TO_DATE(:DATADATE,'DD/MM/YYYY'), :BU, :DEALID, :PS, :PRODTYPE, :CCYPAIR, :CCY, :NOTIONAL, :CMNE, TO_DATE(:FLOWDATE,'DD/MM/YYYY'), :FLOWAMOUNT, Sysdate, '1' )";
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);
                        o.setVarCharInputParameter("DATADATE", d.DataDate.Trim());
                        o.setVarCharInputParameter("BU", d.Bu.Trim());
                        o.setVarCharInputParameter("DEALID", d.DealID.Trim());
                        o.setVarCharInputParameter("PS", d.PS.Trim());
                        o.setVarCharInputParameter("PRODTYPE", d.ProdType.ToUpper().Trim());
                        o.setVarCharInputParameter("CCYPAIR", d.CCYPair.Trim());
                        o.setVarCharInputParameter("CCY", d.CCY.Trim());
                        o.setDecimalInputParameter("NOTIONAL", d.Notional);
                        o.setVarCharInputParameter("CMNE", d.CMNE.Trim());
                        o.setVarCharInputParameter("FLOWDATE", d.FlowDate.Trim());
                        o.setDecimalInputParameter("FLOWAMOUNT", d.FlowAmount);
                        int result = o.executeNonQuery();
                        if (result <= 0)
                        {
                            error.Add(d.Records);
                            i++;
                        }
                    }
                    catch (Exception ex)
                    {
                        error.Add(d.Records);
                        i++;
                    }
                }

                if (error.Count <= 0)
                    o.commitTransaction();
                else
                    o.rollbackTransaction();
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
            return error;
        }
    }
}
