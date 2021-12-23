
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;
using System.Collections;

namespace TreasuryModel.Data
{
    public class Model_MS_Debenture
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_Debenture));

        public Int32 Records { get; set; }
        public Int64 DebentureID { get; set; }
        public string InputDate { get; set; }
        public string DealNo { get; set; }
        public string SecID { get; set; }
        public Int64 Unit { get; set; }
        public Int64 Amount { get; set; }
        public string Custodian { get; set; }
        public string NameThai { get; set; }
        public string CustID { get; set; }
        public string ThaiBMANo { get; set; }
        public string InternalRef { get; set; }
        public string CustType { get; set; }
        public string CType { get; set; }
        public string Enabled { get; set; }
        public string NameThaiShort { get; set; }
        public string RelateCustID { get; set; }
        public string RelateNameThai { get; set; }
        public string BOTType { get; set; }
        public int InputRound { get; set; }
        public string CustomerNumber { get; set; }

        public static DataTable getDebentureDistinct(string ConnectionString)
        {
            String sql = "Select distinct SECID From OPICINF.KKB_TB_DEBENTURE ORDER BY SECID ";

            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public static DataTable getDebentureList(string ConnectionString, string DealNo, string Bond, string Custodian, string InputDate, string Round, string StartDate, string ToDate)
        {
            String sql = "Select * From OPICINF.KKB_TB_DEBENTURE WHere Enabled=1 ";
            if (!DealNo.Trim().Equals(""))
                sql += "And TRIM(DealNo)='" + DealNo.Trim() + "' ";
            if (!Bond.Trim().Equals(""))
                sql += "And TRIM(SECID)='" + Bond.ToUpperInvariant().Trim() + "' ";
            if (!Custodian.Trim().Equals(""))            
                sql += "And TRIM(Custodian) like '%" + Custodian + "%' ";
            if (!InputDate.Trim().Equals(""))
                sql += "And TO_CHAR(InputDate,'DD/MM/YYYY') = '" + InputDate + "' ";
            if (!Round.Trim().Equals(""))
                sql += "And INPUTROUND=" + Round + " ";
            if (!StartDate.Trim().Equals(""))
                sql += "And TRIM(DEALNO) IN (SELECT TRIM(DEALNO) FROM OPICS.SPSH WHERE SETTDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + ToDate + "','DD/MM/YYYY')) ";
            sql += "Order By TO_NUMBER(SUBSTR(THAIBMANO,0,INSTR(THAIBMANO,'/',1,1)-1)) ";
            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public static DataTable getDebenture(string ConnectionString, string DebentureID)
        {
            String sql = "Select * From OPICINF.KKB_TB_DEBENTURE Where DebentureID='" + DebentureID + "' ";
            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }


        public static DataTable getDebentureCustomer(string ConnectionString, string name)
        {
            String sql = "Select Distinct NameThai, CustID From OPICINF.KKB_TB_DEBENTURE Where NameThai like :NameThai ";
            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text); 
                o.setNCharInputParameter("NameThai", "" + name.ToUpperInvariant().Trim() + "%");
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public static DataTable getDebentureOpics(string ConnectionString)
        {
            String sql = "SELECT TRIM(SPSH.DEALNO) as DEALNO, TRIM(SPSH.SECID) as SECID, TRIM(CUST.CNO) as CNO, TRIM(CUST.CMNE) as CMNE, TRIM(SECM.SERIES) as SERIES, NVL((SELECT SECALTID FROM OPICS.ASID WHERE ROWNUM=1 AND SECIDTYPE='TBMAID' AND ASID.SECID=SPSH.SECID),' ') as SECALTID ";
            sql += "FROM OPICS.SPSH LEFT JOIN OPICS.SECM ON SPSH.SECID=SECM.SECID LEFT JOIN OPICS.CUST ON SPSH.CNO=CUST.CNO  ";
            sql += "WHERE SPSH.REVDATE IS NULL ";
            //sql += "AND SPSH.SECID IN (SELECT SECID FROM OPICS.SECM WHERE ACCTNGTYPE IN ('DBIS','SDIS','STN'))  ";
            sql += "AND SPSH.DEALNO NOT IN (SELECT DEALNO FROM OPICS.SPSH WHERE DEALTEXT LIKE '%REDEMPTION%' AND REVDATE IS NULL) ";
            sql += "GROUP BY SPSH.DEALNO, SPSH.SECID, SECM.SERIES, CUST.CNO, CUST.CMNE ";
            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }
        public static DataTable getCustomerDebenture(string ConnectionString)
        {
            String sql = "SELECT DEBENTUREID, NAMETHAI, REGEXP_REPLACE(CASE WHEN INSTR(NAMETHAI,(SELECT VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE NAME='DebtNameCut'),1,1)>0 THEN SUBSTR(NAMETHAI,0,INSTR(NAMETHAI,(SELECT VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE NAME='DebtNameCut'),1,1)-1) ELSE NAMETHAI END, (SELECT VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE NAME='DebtNameKey'), '') as NAMETHAISHORT, CUSTID FROM OPICINF.KKB_TB_DEBENTURE WHERE DEBENTUREID IN (SELECT MIN(DEBENTUREID) FROM (";
            sql += "SELECT DEBENTUREID, REGEXP_REPLACE(CASE WHEN INSTR(NAMETHAI,(SELECT VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE NAME='DebtNameCut'),1,1)>0 THEN SUBSTR(NAMETHAI,0,INSTR(NAMETHAI,(SELECT VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE NAME='DebtNameCut'),1,1)-1) ELSE NAMETHAI END, (SELECT VALUE FROM OPICINF.KKB_TB_WEBPARAM WHERE NAME='DebtNameKey'), '') as NAMETHAISHORT ";
            sql += "FROM OPICINF.KKB_TB_DEBENTURE)  ";
            sql += "GROUP BY NAMETHAISHORT)";
            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }
        public static DataTable getCustomerTypeMapping(string ConnectionString)
        {
            String sql = "SELECT * FROM OPICINF.KKB_TB_CUSTTYPE_MAPPING";
            Log.Info("-----KKB_TB_CUSTTYPEMAPPING Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPEMAPPING Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }        
        public static List<KeyValuePair<string,string>> getCustomerTypeMapping(string ConnectionString, string BOTType)
        {
            String sql = "SELECT * FROM OPICINF.KKB_TB_CUSTTYPE_MAPPING WHERE 1=1 ";
            if(!BOTType.Equals(""))
                sql += "And BOTType='" + BOTType + "'";
            Log.Info("-----KKB_TB_CUSTTYPEMAPPING Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            List<KeyValuePair<string, string>> CustList = new List<KeyValuePair<string, string>>();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();

                for (int i = 0; i < s.Tables[0].Rows.Count; i++)
                {
                    KeyValuePair<string, string> sectype = new KeyValuePair<string, string>(s.Tables[0].Rows[i]["SECTYPE"].ToString(), s.Tables[0].Rows[i]["SECTYPEDESC"].ToString());
                    CustList.Add(sectype);
                }
            }   
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPEMAPPING Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return CustList;
            else
                return null;
        }
        public static DataTable getSECIDDebenture(string ConnectionString)
        {
            String sql = "SELECT DISTINCT TRIM(SECID) as SECID ";
            sql += "FROM OPICINF.KKB_TB_DEBENTURE WHERE ENABLED='1' ";
            Log.Info("-----Model_MS_DEBENTURE Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }
        public static int getLastInputRound(string ConnectionString)
        {
            String sql = "SELECT NVL(COUNT(DISTINCT INPUTROUND),0) + 1 as LastInputRound FROM OPICINF.KKB_TB_DEBENTURE WHERE TO_CHAR(INPUTDATE,'DD/MM/YYYY')=TO_CHAR(SYSDATE, 'DD/MM/YYYY') GROUP BY TO_CHAR(INPUTDATE,'DD/MM/YYYY')";
            Log.Info("-----KKB_TB_CUSTTYPEMAPPING Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return Convert.ToInt16(o.executeScalar().ToString());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPEMAPPING Finish-----");
                o.closeConnection();
            }
        }
        public static DataTable getInputDateRound(string ConnectionString)
        {
            String sql = "SELECT INPUTDATE || ' No.' || NVL(INPUTROUND,1) as InputDateDisplay, INPUTDATE || ' ' || INPUTROUND as InputDateValue FROM (SELECT TO_CHAR(INPUTDATE,'DD/MM/YYYY') as INPUTDATE, INPUTROUND FROM OPICINF.KKB_TB_DEBENTURE GROUP BY TO_CHAR(INPUTDATE,'DD/MM/YYYY'), INPUTROUND) ORDER BY TO_DATE(INPUTDATE,'DD/MM/YYYY') DESC, INPUTROUND DESC ";
            Log.Info("-----KKB_TB_CUSTTYPEMAPPING Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPEMAPPING Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return s.Tables[0];
            else
                return null;
        }

        public Boolean Create(string ConnectionString)
        {

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                Log.Debug("Action : " + "Insert");
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_DEBTMANAGEMENT", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "insert");
                o.setInputParameter("p_debenture", "");
                o.setInputParameter("p_dealno", this.DealNo);
                o.setInputParameter("p_secid", this.SecID);
                o.setInputParameter("p_unit", this.Unit);
                o.setInputParameter("p_amount", this.Amount);
                o.setInputParameter("p_custodian", this.Custodian);
                o.setNCharInputParameter("p_namethai", this.NameThai);
                o.setNCharInputParameter("p_custid", this.CustID);
                o.setInputParameter("p_thaibma", this.ThaiBMANo);
                o.setInputParameter("p_custtype", this.CustType);
                o.setInputParameter("p_ctype", this.CType);
                o.setInputParameter("p_bottype", this.BOTType);
                o.setInputParameter("p_customernumber", this.CustomerNumber);
                row = o.executeNonQuery("r_rows");

                Log.Debug("Result : " + row);
                if (row > 0)
                    return true;
                else
                    return false;
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
                o.openConnection();
                o.createCommand("OPICINF.KKB_SP_TSS_DEBTMANAGEMENT", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "update");
                o.setInputParameter("p_debenture", this.DebentureID);
                o.setInputParameter("p_dealno", this.DealNo);
                o.setInputParameter("p_secid", this.SecID);
                o.setInputParameter("p_unit", this.Unit);
                o.setInputParameter("p_amount", this.Amount);
                o.setInputParameter("p_custodian", this.Custodian);
                o.setNCharInputParameter("p_namethai", this.NameThai);
                o.setNCharInputParameter("p_custid", this.CustID);
                o.setInputParameter("p_thaibma", this.ThaiBMANo);
                o.setInputParameter("p_custtype", this.CustType);
                o.setInputParameter("p_ctype", this.CType);
                o.setInputParameter("p_bottype", this.BOTType);
                o.setInputParameter("p_customernumber", this.CustomerNumber);
                row = o.executeNonQuery("r_rows");

                Log.Debug("Result : " + row);
                if (row > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_MS_DEBENTURE Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_MS_DEBENTURE Finish-----");
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
                o.createCommand("OPICINF.KKB_SP_TSS_DEBTMANAGEMENT", CommandType.StoredProcedure);
                o.setInputParameter("p_action", "delete");
                o.setInputParameter("p_debenture", this.DebentureID);
                o.setInputParameter("p_dealno", "");
                o.setInputParameter("p_secid", "");
                o.setInputParameter("p_unit", "");
                o.setInputParameter("p_amount", "");
                o.setInputParameter("p_custodian", "");
                o.setNCharInputParameter("p_namethai", "");
                o.setNCharInputParameter("p_custid", "");
                o.setInputParameter("p_thaibma", "");
                o.setInputParameter("p_custtype", "");
                o.setInputParameter("p_ctype", "");
                o.setInputParameter("p_bottype", "");
                o.setInputParameter("p_customernumber", "");
                row = o.executeNonQuery("r_rows");
                if (row > 0)
                    return true;
                else
                    return false;
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


        public Boolean Delete(string ConnectionString, string key)
        {
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Delete From OPICINF.KKB_TB_DEBENTURE WHERE DEBENTUREID in (" + key + ")";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_DEBENTURE Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_MS_DEBENTURE Finish-----");
                o.closeConnection();
            }
        }

        public ArrayList save(string ConnectionString, List<Model_MS_Debenture> debt, ArrayList oldcust)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();
                // Disabled Old Field

                int i = 0;
                /*
                string del = "Delete From OPICINF.KKB_TB_DEBENTURE Where DealNo in ";
                del += "(Select DealNo FROM OPICS.SPSH LEFT JOIN OPICS.SECM ON SPSH.SECID=SECM.SECID ";
                del += "WHERE SPSH.REVDATE IS NULL AND TO_CHAR(SECM.ISSDATE,'YYYYMM')='" + MonthYear + "') ";
                //del += " AND SPSH.SECID IN (SELECT SECID FROM OPICS.SECM WHERE ACCTNGTYPE IN ('DBIS','SDIS','STN')) ";
                //del += "AND SPSH.PORT IN ('STDP','LTDP') AND SPSH.TRAD NOT LIKE '%REDE%')";
                o.createCommand(del, CommandType.Text);
                o.executeNonQuery();
                */
                foreach (Model_MS_Debenture d in debt)
                {
                    try
                    {
                        string sql = "Insert Into OPICINF.KKB_TB_DEBENTURE(INPUTDATE, DEALNO, SECID, UNIT, AMOUNT, CUSTODIAN, NAMETHAI, CUSTID, THAIBMANO, CUSTTYPE, CTYPE, BOTTYPE, INPUTROUND, CUSTOMERNUMBER, UPDATEDATE, ENABLED) ";
                        sql += " Values( SysDate, :DealNo, :SecID, :Unit, :Amount, :Custodian , :NameThai, :CustID, :ThaiBMANo, :CustType, :CType, :BOTType, :InputRound, :CustomerNumber, Sysdate, :Enabled )";
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);
                        o.setNCharInputParameter("DealNo", d.DealNo);
                        o.setNCharInputParameter("SecID", d.SecID);
                        o.setNCharInputParameter("Unit", d.Unit);
                        o.setNCharInputParameter("Amount", d.Amount);
                        o.setNCharInputParameter("Custodian", d.Custodian);
                        if (oldcust.Contains(d.Records.ToString()))
                        {
                            if ((d.RelateNameThai.Trim().Equals("")) || (d.RelateCustID.Trim().Equals("")))
                            {
                                o.setNCharInputParameter("NameThai", d.NameThai);
                                o.setNCharInputParameter("CustID", d.CustID);
                            } else
                            {
                                o.setNCharInputParameter("NameThai", d.RelateNameThai);
                                o.setNCharInputParameter("CustID", d.RelateCustID);
                            }
                        }
                        else
                        {
                            o.setNCharInputParameter("NameThai", d.NameThai);
                            o.setNCharInputParameter("CustID", d.CustID);
                        }
                        o.setNCharInputParameter("ThaiBMANo", d.ThaiBMANo);
                        o.setNCharInputParameter("CustType", d.CustType);
                        o.setNCharInputParameter("CType", d.CType);
                        o.setNCharInputParameter("BOTType", d.BOTType.Substring(0, 6));
                        o.setNCharInputParameter("InputRound", d.InputRound);
                        o.setNCharInputParameter("CustomerNumber", d.CustomerNumber);
                        o.setIntegerInputParameter("Enabled", 1);
                        o.executeNonQuery();

                        //Get last seq
                        sql = "Select MAX(DEBENTUREID) as MaxDebentureID FROM OPICINF.KKB_TB_DEBENTURE";
                        o.createCommand(sql, CommandType.Text);
                        int debentureid = int.Parse(o.executeScalar().ToString());

                        //Add Debenture Transaction
                        sql = "Insert Into OPICINF.KKB_TB_DEBENTURE_TRANS(DEBENTUREID, INPUTDATE, DEALNO, SECID, UNIT, AMOUNT, CUSTODIAN, NAMETHAI, CUSTID, THAIBMANO, CUSTTYPE, CTYPE, BOTTYPE, INPUTROUND, CUSTOMERNUMBER, UPDATEDATE, ENABLED) ";
                        sql += " Values( :DebentureID, SysDate, :DealNo, :SecID, :Unit, :Amount, :Custodian , :NameThai, :CustID, :ThaiBMANo, :CustType, :CType, :BOTType, :InputRound, :CustomerNumber, Sysdate, :Enabled )";
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);
                        o.setIntegerInputParameter("DebentureID", debentureid);
                        o.setNCharInputParameter("DealNo", d.DealNo);
                        o.setNCharInputParameter("SecID", d.SecID);
                        o.setNCharInputParameter("Unit", d.Unit);
                        o.setNCharInputParameter("Amount", d.Amount);
                        o.setNCharInputParameter("Custodian", d.Custodian);
                        o.setNCharInputParameter("NameThai", d.NameThai);
                        o.setNCharInputParameter("CustID", d.CustID);
                        o.setNCharInputParameter("ThaiBMANo", d.ThaiBMANo);
                        o.setNCharInputParameter("CustType", d.CustType);
                        o.setNCharInputParameter("CType", d.CType);
                        o.setNCharInputParameter("BOTType", d.BOTType.Substring(0, 6));
                        o.setNCharInputParameter("InputRound", d.InputRound);
                        o.setNCharInputParameter("CustomerNumber", d.CustomerNumber);
                        o.setIntegerInputParameter("Enabled", 1);
                        o.executeNonQuery();
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
