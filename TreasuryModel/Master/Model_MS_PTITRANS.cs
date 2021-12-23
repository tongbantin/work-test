using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using System.Collections;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;

namespace TreasuryModel.Master
{
    public class Model_MS_PTITRANS
    {
        public int TransLogID { get; set; } 
        public string TransLogNo { get; set; } 
        public string TransLogDate { get; set; }
        public string SecID { get; set; }
        public string PartialID { get; set; }
        public string MsgType { get; set; }
        public string Priority { get; set; }
        public string SenderRef { get; set; }
        public string RelateRef { get; set; }
        public string PlaceCode { get; set; }
        public string SettDate { get; set; }
        public string TradeDate { get; set; }
        public string PercentType { get; set; }
        public string DealPrice { get; set; }
        public string ISIN { get; set; }
        public string FaceAmt { get; set; }
        public string AcctNo { get; set; }
        public string CashAcct { get; set; }
        public string BusinessType { get; set; }
        public string SettType { get; set; }
        public string PS { get; set; }
        public string SettPlace { get; set; }
        public int AttnID { get; set; }
        public string Attn { get; set; }
        public string AgentBIC { get; set; }
        public string AgentCA { get; set; }
        public string AgentAcctNo { get; set; } 
        public string SettAmt { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string PrinAmt { get; set; }
        public string IntAmt { get; set; }
        public string CostAmt { get; set; }
        public string VatAmt { get; set; }
        public string DealPriceAmt { get; set; }

        public string FaceAmount { get; set; }
        public string Customer { get; set; }

        public string ProductCode { get; set; }
        public string ProductType { get; set; }

        //Object for upload excel
        public string ExcelDealno { get; set; }
        public string ExcelAttn { get; set; }
        public string ExcelFaceAmt { get; set; }
        public string ExcelCostAmt { get; set; }

        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_PTITRANS));

        public Model_MS_PTITRANS()
        {
            try
            {
                this.PartialID = "513";
                this.Priority = "8";
                this.PlaceCode = "OTCO";
                this.PercentType = "YIEL";
                this.AcctNo = "5130000000110";
                this.CashAcct = "0010043357";
                this.BusinessType = "IST";
                this.SettType = "T";
                this.SettPlace = "TSDCTHBK";
            }
            catch (Exception ex)
            { 
            
            }
        }

        public static DataSet getProductFromDealNo(string ConnectionString, string DealNo)
        {
            String sql = "Select * From OPICS.SPSH WHere DealNo='" + DealNo + "'";
            Log.Info("-----Model_MS_PTITRANS Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_COUNTERPARTY Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static int getNextSequence(string ConnectionString, string Dealno)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "SELECT NVL(count(*),0) + 1 as LASTROW FROM OPICINF.KKB_TB_MS_PTITRANS_LOG WHERE RELATEREF='" + Dealno + "'";
                o.createCommand(sql, CommandType.Text);
                int SeqNo = Int32.Parse(o.executeScalar().ToString());
                Log.Info("SeqNo Return : " + SeqNo);
                return SeqNo;
            }
            catch (Exception ex)
            {
                Log.Error("Function GetNextSequence" + ex.Message);
                return -1;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static DataTable getTransLogByID(string ConnectionString, string id)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Select *, (SELECT NVL(SUM(NVL(FACEAMT,0)),0) FROM OPICS.SPSH WHERE TRIM(DEALNO)=TRIM(KKB_TB_MS_PTITRANS_LOG.RELATEREF)) - (SELECT NVL(SUM(NVL(PTI.FACEAMT,0)),0) FROM OPICINF.KKB_TB_MS_PTITRANS_LOG PTI WHERE TRIM(PTI.RELATEREF)=TRIM(KKB_TB_MS_PTITRANS_LOG.RELATEREF)) as Remain, (Select AssetCompany From OPICINF.KKB_TB_CUSTOMER WHERE CUSTOMERID=KKB_TB_MS_PTITRANS_LOG.ATTNID) AssetCompany From OPICINF.KKB_TB_MS_PTITRANS_LOG Where TransLogID='" + id + "'";
                o.createCommand(sql, CommandType.Text);
                return o.executeReaderToDT("TBTRANSLOG"); 
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static DataTable getTransLogByDealNo(string ConnectionString, string DealNo)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Select *, (Select NameEn From OPICINF.KKB_TB_CUSTOMER WHERE CustomerID=KKB_TB_MS_PTITRANS_LOG.AttnID) AttnName From OPICINF.KKB_TB_MS_PTITRANS_LOG Where TRIM(RelateRef)='" + DealNo.Trim() + "' ORDER BY RELATEREF , TRANSLOGID ASC ";
                o.createCommand(sql, CommandType.Text);
                Log.Info("getTransLogByDealNo SQL :" + sql );
                return o.executeReaderToDT("TBTRANSLOG");
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static DataTable getTransLogBySettleDate(string ConnectionString, string SettleDate , string SearchDeal)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                string sql = "", subsql = "" , order ="";                

                if (!SearchDeal.Equals("") && SearchDeal != null) {
                    subsql = " AND RELATEREF IN (" + SearchDeal + ")";                    
                } 
                sql = "Select *, (Select NameEn From OPICINF.KKB_TB_CUSTOMER WHERE CustomerID=KKB_TB_MS_PTITRANS_LOG.AttnID) AttnName From OPICINF.KKB_TB_MS_PTITRANS_LOG Where TRIM(SETTDATE)='" + SettleDate.Trim() + "' ";
                order = " ORDER BY RELATEREF , TRANSLOGID ASC ";
                
                o.openConnection();
                o.createCommand(sql+subsql+order , CommandType.Text);
                Log.Info("getTransLogBySettleDate SQL :" + sql + subsql + order);

                return o.executeReaderToDT("TBTRANSLOG");
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }


        public static DataTable getTransLogBySelected(string ConnectionString, string key)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Select *, (Select NameEn From OPICINF.KKB_TB_CUSTOMER WHERE CustomerID=KKB_TB_MS_PTITRANS_LOG.AttnID) AttnName From OPICINF.KKB_TB_MS_PTITRANS_LOG WHERE TRANSLOGID in (" + key + ")";
                o.createCommand(sql, CommandType.Text);
                return o.executeReaderToDT("TBTRANSLOG");
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }


        public int isGenerate(string ConnectionString, string dealno)
        {
            string sql = "Select count(*) as IsGen From OPICINF.KKB_TB_MS_PTITRANS_LOG Where TRIM(RelateRef)='" + dealno.Trim() + "' And GenXML='1'";
            int rows = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                rows = Convert.ToInt32(o.executeScalar().ToString()); 
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                rows = 1;
            }
            finally
            {
                Log.Info("-----Model_MS_COUNTERPARTY Finish-----");
                o.closeConnection();
            }
            return rows;
        }

        public bool setGenerate(string ConnectionString, string DealNo)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Update OPICINF.KKB_TB_MS_PTITRANS_LOG Set GenXML='1' Where RelateRef='" + DealNo + "' ";
                Log.Info("Execute : " + sql);
                o.createCommand(sql, CommandType.Text);
                int result = o.executeNonQuery();
                if (result < 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                o.closeConnection();
            }
        }
                
        public bool save(string ConnectionString, List<Model_MS_PTITRANS> l)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.beginTransaction();
                // Disabled Old Field
                Model_MS_PTITRANS mclear = l[0];
                string clearsql = "Update OPICINF.KKB_TB_MS_PTITRANS_LOG SET ENABLED='0' WHERE RELATEREF='" + mclear.RelateRef + "'";
                o.createTransaction(clearsql, CommandType.Text);
                int clearresult = o.executeNonQuery();

                foreach (Model_MS_PTITRANS m in l)
                {
                    string sql = "Insert Into OPICINF.KKB_TB_MS_PTITRANS_LOG(TRANSLOGNO, TRANSLOGDATE, SECID, PARTIALID, MSGTYPE, PRIORITY, SENDERREF, RELATEREF, PLACECODE, SETTDATE, TRADEDATE, PERCENTTYPE, DEALPRICE, ISIN, FACEAMT, ACCTNO, CASHACCT, BUSINESSTYPE, SETTTYPE, PS, SETTPLACE, ATTN, AGENTBIC, AGENTCA, AGENTACCTNO, PRINAMT, INTAMT, SETTAMT, CREATEDATE, CREATEBY, ENABLED ) ";
                    sql += "values('" + m.TransLogNo + "',TO_DATE('" + m.TransLogDate + "','DD/MM/YYYY'),'" + m.SecID + "', '" + m.PartialID + "', '" + m.MsgType + "', '" + m.Priority + "' , '" + m.SenderRef + "' , '" + m.RelateRef + "' , '" + m.PlaceCode + "' , '" + m.SettDate + "' , '" + m.TradeDate + "' , '" + m.PercentType + "' , '" + m.DealPrice + "' , '" + m.ISIN + "' , '" + m.FaceAmt + "' , '" + m.AcctNo + "' , '" + m.CashAcct + "' , '" + m.BusinessType + "' , '" + m.SettType + "' , '" + m.PS + "', '" + m.SettPlace + "' , '" + m.Attn + "' , '" + m.AgentBIC + "' , '" + m.AgentCA + "' , '" + m.AgentAcctNo + "' , '" + m.PrinAmt + "', '" + m.IntAmt + "', '" + m.SettAmt + "', SYSDATE  , 'ADMIN' , '1')";
                    Log.Info("Execute : " + sql);
                    o.createTransaction(sql, CommandType.Text);
                    int result = o.executeNonQuery();
                    if (result < 0)
                    {
                        o.rollbackTransaction();
                        return false;
                    }
                }
                o.commitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static DataSet get(string ConnectionString, string DealNo, string TransDate)
        {
            String sql = "Select * From OPICINF.KKB_TB_MS_PTITRANS_LOG Where ENABLED='1' ";
            if (DealNo.Trim() != "")
                sql += "And Trim(RelateRef) = '" + DealNo.Trim()  + "' ";
            if (TransDate.Trim() != "")
                sql += " And TransLogDate=TO_DATE('" + TransDate.Trim() + "','DD/MM/YYYY') ";
            else
                sql += " And TransLogDate>=TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY') ";
            sql += "Order By TRANSLOGID DESC ";
            Log.Info("-----Model_MS_PTITRANS Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                s = null;
            }
            finally
            {
                Log.Info("-----Model_MS_COUNTERPARTY Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public Boolean Create(string ConnectionString)
        {

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string[] faceamt = this.FaceAmt.Split(',');
                string[] customer = this.Customer.Split(',');
                ArrayList ptiid = new ArrayList();

                for (int i = 0; i < faceamt.Length; i++)
                {
                    Log.Debug("Action : " + "insert");
                    Log.Debug("TransLog : " + "");
                    Log.Debug("DealNo : " + this.RelateRef);
                    Log.Debug("MsgType : " + this.MsgType);
                    Log.Debug("Attn : " + customer[i]);
                    Log.Debug("FaceAmt : " + faceamt[i]);
                    Log.Debug("SettAmt : " + 0);
                    Log.Debug("CostAmt : " + this.CostAmt);
                    o.createCommand("OPICINF.KKB_SP_TSS_TR051ALLOT", CommandType.StoredProcedure);
                    o.setInputParameter("p_action", "insert");
                    o.setInputParameter("p_translog", "");
                    o.setInputParameter("p_dealno", this.RelateRef);
                    o.setInputParameter("p_msgtype", this.MsgType.ToString());
                    o.setInputParameter("p_attn", customer[i]);
                    o.setInputParameter("p_faceamt", faceamt[i]);
                    o.setInputParameter("p_settamt", "0");
                    o.setInputParameter("p_prinamt", "0");
                    o.setInputParameter("p_costamt", this.CostAmt);
                    o.setInputParameter("p_intamt", "");
                    o.setInputParameter("p_vat", "");
                    o.setInputParameter("p_dealprice", "");
                    row = o.executeNonQuery("r_rows");
                    Log.Debug("Result : " + row);
                    if (row > 0)
                        ptiid.Add(row);
                    else {
                        for (int j = 0; j < ptiid.Count; j++)
                        {
                            o.createCommand("OPICINF.KKB_SP_TSS_TR051ALLOT", CommandType.StoredProcedure);
                            o.setInputParameter("p_action", "delete");
                            o.setInputParameter("p_translog", ptiid[j].ToString());
                            o.setInputParameter("p_dealno", "0");
                            o.setInputParameter("p_msgtype", "0");
                            o.setInputParameter("p_attn", "0");
                            o.setInputParameter("p_faceamt", "0");
                            o.setInputParameter("p_settamt", "0");
                            o.setInputParameter("p_prinamt", "0");
                            o.setInputParameter("p_costamt", "0");
                            o.setInputParameter("p_intamt", "0");
                            o.setInputParameter("p_vat", "0");
                            o.setInputParameter("p_dealprice", "0");
                            row = o.executeNonQuery("r_rows");
                        }
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_MS_PTITRANS_LOG Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_MS_PTITRANS_LOG Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Update(string ConnectionString)
        {
            Decimal row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                Log.Debug("Action : " + "update");
                Log.Debug("TransLog : " + "");
                Log.Debug("DealNo : " + this.RelateRef);
                Log.Debug("MsgType : " + this.MsgType);
                Log.Debug("Attn : " + this.Customer);
                Log.Debug("FaceAmt : " + this.FaceAmt);
                Log.Debug("SettAmt : " + this.SettAmt);
                Log.Debug("PrinAmt : " + this.PrinAmt);
                Log.Debug("CostAmt : " + this.CostAmt);
                Log.Debug("SettAmt : " + this.SettAmt);
                Log.Debug("PrinAmt : " + this.PrinAmt);
                Log.Debug("CostAmt : " + this.CostAmt);
                Log.Debug("IntAmt : " + this.IntAmt);
                Log.Debug("VatAmt : " + this.VatAmt);
                Log.Debug("DealPriceAmt : " + this.DealPriceAmt);


                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_TR051ALLOT", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "update");
                o.setInputParameter("p_translog", this.TransLogID);
                o.setInputParameter("p_dealno", this.RelateRef);
                o.setInputParameter("p_msgtype", this.MsgType);
                o.setInputParameter("p_attn", this.Customer);
                o.setInputParameter("p_faceamt", this.FaceAmt);
                o.setInputParameter("p_settamt", this.SettAmt);
                o.setInputParameter("p_prinamt", this.PrinAmt);
                o.setInputParameter("p_costamt", this.CostAmt);
                o.setInputParameter("p_intamt", this.IntAmt);
                o.setInputParameter("p_vat", this.VatAmt);
                o.setInputParameter("p_dealprice", this.DealPriceAmt);
                row = o.executeNonQuery("r_rows");

                Log.Debug("Result : " + row);
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    //Log.Error(result.Find(t => t.Key == "error").Value);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_MS_PTITRANS_LOG Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_MS_PTITRANS_LOG Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Delete(string ConnectionString)
        {
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_TR051ALLOT", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "delete");
                o.setInputParameter("p_translog", this.TransLogID);
                o.setInputParameter("p_dealno", "");
                o.setInputParameter("p_msgtype", "");
                o.setInputParameter("p_attn", "");
                o.setInputParameter("p_faceamt", "");
                o.setInputParameter("p_settamt", "");
                o.setInputParameter("p_prinamt", "");
                o.setInputParameter("p_costamt", "");
                o.setInputParameter("p_intamt", "");
                o.setInputParameter("p_vat", "");
                o.setInputParameter("p_dealprice", "");
                row = o.executeNonQuery("r_rows");
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    //Log.Error(result.Find(t => t.Key == "error").Value);
                    return false;
                }
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_MS_PTITRANS_LOG Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_MS_PTITRANS_LOG Finish-----");
                o.closeConnection();
            }
        }
    }
}
