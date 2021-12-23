using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using System.Collections;
using System.Globalization;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class NostroDaily
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(NostroDaily));

        //fill data variable
        public int ADJ_ID { get; set; }
        public string ADJ_CCY { get; set; }
        public string ADJ_ACCTNO { get; set; }
        public string ADJ_CUST { get; set; }
        public string ADJ_PRODTYPE { get; set; }
        public string ADJ_COST { get; set; }
        public decimal ADJ_AMT { get; set; }
        public DateTime ADJ_VDATE { get; set; }
        public DateTime ADJ_MDATE { get; set; }
        public string ADJ_VDATE_STR { get; set; }
        public string ADJ_MDATE_STR { get; set; }
        public string ADJ_REMARK { get; set; }
        public string ADJ_PD { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_USER { get; set; }


        public static object Create(string ConnectionString, NostroDaily record)
        {
            Log.Info("-----KKB_TB_NOSTROADJ Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "INSERT INTO OPICINF.KKB_TB_NOSTROADJ ( ADJ_CCY, ADJ_COST, ADJ_ACCTNO, ADJ_CUST, ADJ_PRODTYPE, ADJ_AMT, ADJ_VDATE, ADJ_MDATE, ADJ_REMARK, ADJ_PD, CREATE_DATE, CREATE_BY) VALUES ( :ADJ_CCY, :ADJ_COST, :ADJ_ACCTNO, :ADJ_CUST, :ADJ_PRODTYPE, :ADJ_AMT, TO_DATE(:ADJ_VDATE,'DD/MM/YYYY'), TO_DATE(:ADJ_MDATE,'DD/MM/YYYY'), :ADJ_REMARK, :ADJ_PD, SYSDATE, :CREATE_USER ) ";

                //get account id and cust id
                string[] adj = record.ADJ_ACCTNO.Split('|');

                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("ADJ_CCY", record.ADJ_CCY);
                o.setInputParameter("ADJ_COST", record.ADJ_COST);
                o.setInputParameter("ADJ_ACCTNO", adj[0]);
                o.setInputParameter("ADJ_CUST", adj[1]);
                o.setInputParameter("ADJ_PRODTYPE", record.ADJ_PRODTYPE);
                o.setInputParameter("ADJ_AMT", record.ADJ_AMT);
                o.setInputParameter("ADJ_VDATE", record.ADJ_VDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("ADJ_MDATE", record.ADJ_MDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("ADJ_REMARK", record.ADJ_REMARK);
                o.setInputParameter("ADJ_PD", record.ADJ_PD);
                o.setInputParameter("CREATE_USER", record.CREATE_USER);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_NOSTROADJ Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not insert data." };
            }
            finally
            {
                Log.Info("-----KKB_TB_NOSTROADJ Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable getProcessDate(string ConnectionString)
        {
            Log.Info("NostroDaily : Get Process Date Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("SELECT TO_CHAR(PREVBRANPRCDATE,'DD/MM/YYYY') as PREVBRANPRCDATE, TO_CHAR(BRANPRCDATE,'DD/MM/YYYY') as BRANPRCDATE, TO_CHAR(NEXTBRANPRCDATE,'DD/MM/YYYY') as NEXTBRANPRCDATE FROM OPICS.BRPS WHERE BR = '01' ", CommandType.Text);
                s = o.executeReaderToDT("TB_BRPS");
            }
            catch (Exception ex)
            {
                Log.Error("-----getProcessDate Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getProcessDate finish-----");
                o.closeConnection();
            }
            return s;
          
        }

        public static DataTable getDataPrevDate(string ConnectionString, string prevbrprcdate)
        {
            Log.Info("NostroDaily : Get data previous date");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(NostroDaily.Owner + ".KKB_SP_TSS_NOSTRODAILY", CommandType.StoredProcedure);
                o.setInputParameter("p_trans", "NTPREV");
                o.setInputParameter("p_date1", prevbrprcdate);
                o.setInputParameter("p_date2", "");
                o.setInputParameter("p_date3", "");
                o.setInputParameter("p_date4", "");
                o.setInputParameter("p_date5", "");
                o.setInputParameter("p_curr", "");
                o.setInputParameter("p_nosacct", "");

                s = o.executeReader("ref_rpt_cur", "TB_NostroDaily");
            }
            catch (Exception ex)
            {
                Log.Error("-----getDataPrevDate Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getDataPrevDate finish-----");
                o.closeConnection();
            }
            return s.Tables[0];

        }

        public static DataTable getHolidayData(string ConnectionString)
        {
            Log.Info("-----getHolidayData Start-----");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT CALENDARID AS CCY ,TO_CHAR(HOLIDATE,'DD/MM/YYYY') AS HOLIDAY FROM OPICS.HLDY WHERE CALENDARID IN (SELECT CCY FROM OPICS.NOST) ";
                sql += " AND (TO_CHAR(HOLIDATE,'YYYY') = (SELECT TO_CHAR(SYSDATE,'YYYY')-1 FROM DUAL) OR  TO_CHAR(HOLIDATE,'YYYY') = (SELECT TO_CHAR(SYSDATE,'YYYY') FROM DUAL) OR TO_CHAR(HOLIDATE,'YYYY') = (SELECT TO_CHAR(SYSDATE,'YYYY')+1 FROM DUAL)) ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReaderToDT("TB_MTMSCHEDULE");
            }
            catch (Exception ex)
            {
                Log.Error("-----getHolidayData Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getHolidayData Finish-----");
                o.closeConnection();
            }
            return s;
        }
        
        public static DataTable getSumbyPort(string ConnectionString, string prevbrprcdate, string brprcdate, string nextbrprcdate, string next2date, string next3date, string currency, string nostacct)
        {
            Log.Info("NostroDaily : getSumbyPort");
            Log.Info(prevbrprcdate + " / " + brprcdate + " / " + nextbrprcdate + " / " + next2date + " / " + next3date + " / " + currency + " / " + nostacct);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(NostroDaily.Owner + ".KKB_SP_TSS_NOSTRODAILY", CommandType.StoredProcedure);
                o.setInputParameter("p_trans", "NTPORT");
                o.setInputParameter("p_date1", prevbrprcdate);
                o.setInputParameter("p_date2", brprcdate);
                o.setInputParameter("p_date3", nextbrprcdate);
                o.setInputParameter("p_date4", next2date);
                o.setInputParameter("p_date5", next3date);
                o.setInputParameter("p_curr", currency);
                o.setInputParameter("p_nosacct", nostacct);

                s = o.executeReader("ref_rpt_cur", "TB_NostroDaily");
            }
            catch (Exception ex)
            {
                Log.Error("-----getSumbyPort Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getSumbyPort finish-----");
                o.closeConnection();
            }
            return s.Tables[0];

        }

        public static DataTable getSumTrans(string ConnectionString, string searchdate, string[] prodcode)
        {
            Log.Info("NostroDaily :getSumTrans :" + searchdate);

            DataTable dt = new DataTable();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(NostroDaily.Owner + ".KKB_SP_TSS_NostroDaily", CommandType.StoredProcedure);
                o.setInputParameter("p_trans", "NTSUMM");
                o.setInputParameter("p_date1", searchdate);
                o.setInputParameter("p_date2", "");
                o.setInputParameter("p_date3", "");
                o.setInputParameter("p_date4", "");
                o.setInputParameter("p_date5", "");
                o.setInputParameter("p_curr", "");
                o.setInputParameter("p_nosacct", "");

                DataSet ds = o.executeReader("ref_rpt_cur", "TB_NostroDaily");

                //Filter with prodcode
                var query = ds.Tables[0].AsEnumerable();
                for (int i = 0; i < prodcode.Count(); i++ )
                {
                    query.Where(r => ((string)r["PRODCODE"]).Equals(prodcode[i]));
                }
                
                //Group and Sum by currency,cost,accountno
                var queryFinal = query.Select(x => 
                                     new
                                        {
                                            Curr = x["CURR"],
                                            Cost = x["COST"],
                                            Acctno = x["ACCTNO"],
                                            Amount = x["AMOUNT"],
                                        }                               
                                    )
                                  .GroupBy (s => new {s.Curr, s.Cost, s.Acctno} )
                                  .Select (g => 
                                     new {
                                            Curr = g.Key.Curr,
                                            Cost = g.Key.Cost,
                                            Acctno = g.Key.Acctno,
                                            Amount = g.Sum (x => Math.Round(Convert.ToDecimal(x.Amount), 2)),
                                        }
                                    ).ToList();

                
                dt.Columns.Add("CURR", typeof(string));
                dt.Columns.Add("COST", typeof(string));
                dt.Columns.Add("ACCTNO", typeof(string));
                dt.Columns.Add("BALANCE", typeof(decimal));

                foreach (var item in queryFinal)
                {
                    dt.Rows.Add(item.Curr, item.Cost, item.Acctno, item.Amount);
                }
 
            }
            catch (Exception ex)
            {
                Log.Error("-----getSumTrans error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getSumTrans finish-----");
                o.closeConnection();
            }
            return dt;

        }

        public static DataTable getDailyTrans(string ConnectionString, string TransDate, string[] prodcode)
        {
            Log.Info("-----NostroDaily Start getDailyTrans-----");
            Oracle o = Oracle.getInstance(ConnectionString);
            DataSet ds = null;
            try
            {
                DataTable dt = new DataTable();
                o.openConnection();
                o.createCommand(NostroDaily.Owner + ".KKB_SP_TSS_NostroDaily", CommandType.StoredProcedure); ;
                o.setInputParameter("p_trans", "NTDAILY");
                //o.setInputParameter("p_date", TransDate);
                o.setInputParameter("p_date1", TransDate);
                o.setInputParameter("p_date2", "");
                o.setInputParameter("p_date3", "");
                o.setInputParameter("p_date4", "");
                o.setInputParameter("p_date5", "");
                o.setInputParameter("p_curr", "");
                o.setInputParameter("p_nosacct", "");

                ds = o.executeReader("ref_rpt_cur", "TB_NostroDaily");

                //Filter with prodcode
                var query = ds.Tables[0].AsEnumerable().Where(r => prodcode.Contains(r.Field<string>("PRODCODE").Trim()));
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                }

                return dt;

            }
            catch (Exception ex)
            {
                Log.Error("-----getDailyTranso TB_NostroDaily Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----getDailyTranso finish-----");
                o.closeConnection();
            }
        }

        public static DataTable getNostCurrency(string ConnectionString) {

            Log.Info("--- go into getNostroCurr ---");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("SELECT CCY , NOS FROM OPICS.NOST WHERE TRIM(CCY) <> 'THB' GROUP BY CCY , NOS ORDER BY CCY , NOS", CommandType.Text);
                s = o.executeReaderToDT("NOST_CURR");
            }
            catch (Exception ex)
            {
                Log.Error("-----getNostCurrency Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getNostroCurr finish-----");
                o.closeConnection();
            }
            return s;
          
        }

        public static List<KeyValuePair<string, string>> getNostAccount(string ConnectionString, string CURRVAL)
        {
            //String sql = "SELECT TRIM(NOS)as NOS FROM OPICS.NOST WHERE 1=1 ";
            string sql = "SELECT TRIM(NOS)as NOS, TRIM(CUST) as CUSTID, TRIM(CMNE) as CUSTNAME FROM OPICS.NOST LEFT JOIN OPICS.CUST ON TRIM(NOST.CUST) = TRIM(CUST.CNO) WHERE 1=1 ";
                    
            if (!CURRVAL.Equals(""))
                sql += " AND CCY ='" + CURRVAL + "' ORDER BY NOS ASC ";

            DataSet s = null;
            List<KeyValuePair<string, string>> AcctList = new List<KeyValuePair<string, string>>();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();

                for (int i = 0; i < s.Tables[0].Rows.Count; i++)
                {
                    KeyValuePair<string, string> acctlist = new KeyValuePair<string, string>(s.Tables[0].Rows[i]["NOS"].ToString() + "|" + s.Tables[0].Rows[i]["CUSTID"].ToString(), s.Tables[0].Rows[i]["NOS"].ToString());
                    AcctList.Add(acctlist);
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----getNostAccount Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getNostAccount Finish-----");
                o.closeConnection();
            }
            if (s.Tables.Count > 0)
                return AcctList;
            else
                return null;
        }

        public static DataTable getAdjustList(string ConnectionString, String vdate)
        {
            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = " SELECT ADJ_ID , ADJ_CCY as CURR, ADJ_COST as COST, ADJ_ACCTNO as ACCTNO, ADJ_AMT as BALANCE, ADJ_PRODTYPE as PRODTYPE, TO_CHAR(ADJ_VDATE,'DD/MM/YYYY') as VDATE, TO_CHAR(ADJ_MDATE,'DD/MM/YYYY') as MDATE, ADJ_REMARK as MARK FROM OPICINF.KKB_TB_NOSTROADJ WHERE 1=1 ";
                if (!vdate.Equals("") && vdate != "")
                {
                    sql += " AND TO_CHAR(ADJ_VDATE , 'DD/MM/YYYY') = :VDATE "; 
                }
                
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("VDATE", vdate);
                s = o.executeReaderToDT("NOSTROADJ");
                return s;
            }
            catch (Exception ex)
            {
                Log.Error("-----getAdjustTrans Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----getAdjustTrans Finish-----");
                o.closeConnection();
            }
        }

        public static object getAdjustRow(string ConnectionString, int ADJ_ID)
        {
            String sql = "SELECT ADJ_ID, ADJ_CCY, ADJ_COST, ADJ_ACCTNO, ADJ_CUST, ADJ_AMT, ADJ_PRODTYPE, TO_CHAR(ADJ_VDATE,'DD/MM/YYYY') as ADJ_VDATE, TO_CHAR(ADJ_MDATE,'DD/MM/YYYY') as ADJ_MDATE, ADJ_REMARK , ADJ_PD FROM OPICINF.KKB_TB_NOSTROADJ WHERE ADJ_ID ='" + ADJ_ID + "' ";
            Log.Info("-----KKB_TB_NOSTROADJ Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("NOSTROADJ");

                List<NostroDaily> param = new List<NostroDaily>();
                NostroDaily p = new NostroDaily();
                p.ADJ_ID = int.Parse(dt.Rows[0]["ADJ_ID"].ToString());
                p.ADJ_CCY = dt.Rows[0]["ADJ_CCY"].ToString();
                p.ADJ_COST = dt.Rows[0]["ADJ_COST"].ToString();
                //p.ADJ_ACCTNO = dt.Rows[0]["ADJ_ACCTNO"].ToString() +"|"+ dt.Rows[0]["ADJ_CUST"].ToString();
                p.ADJ_ACCTNO = dt.Rows[0]["ADJ_ACCTNO"].ToString();
                p.ADJ_CUST = dt.Rows[0]["ADJ_CUST"].ToString();
                p.ADJ_AMT = decimal.Parse(dt.Rows[0]["ADJ_AMT"].ToString());
                p.ADJ_PRODTYPE = dt.Rows[0]["ADJ_PRODTYPE"].ToString();
                p.ADJ_VDATE = DateTime.ParseExact(dt.Rows[0]["ADJ_VDATE"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                p.ADJ_MDATE = DateTime.ParseExact(dt.Rows[0]["ADJ_MDATE"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                p.ADJ_REMARK = dt.Rows[0]["ADJ_REMARK"].ToString();
                p.ADJ_PD = dt.Rows[0]["ADJ_PD"].ToString();

                p.ADJ_VDATE_STR = p.ADJ_VDATE.ToString("dd/MM/yyyy");
                p.ADJ_MDATE_STR = p.ADJ_MDATE.ToString("dd/MM/yyyy");

                param.Add(p);

                //Return result to jTable
                return param;
            }
            catch (Exception ex)
            {
                Log.Error("-----getAdjustRow Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----getAdjustRow Finish-----");
                o.closeConnection();
            }
        }

        public static object Update(string ConnectionString, NostroDaily record)
        {

            Log.Info("-----KKB_TB_NOSTROADJ Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string[] adj = record.ADJ_ACCTNO.Split('|');

                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_NOSTROADJ Set ADJ_CCY=:ADJ_CCY, ADJ_ACCTNO=:ADJ_ACCTNO,ADJ_CUST=:ADJ_CUST, ADJ_COST=:ADJ_COST ,ADJ_AMT=:ADJ_AMT ,ADJ_VDATE=TO_DATE(:ADJ_VDATE, 'DD/MM/YYYY') ,ADJ_MDATE=TO_DATE(:ADJ_MDATE, 'DD/MM/YYYY'), ADJ_REMARK=:ADJ_REMARK, ADJ_PD=:ADJ_PD , UPDATE_DATE=SYSDATE, UPDATE_BY=:UPDATE_USER Where ADJ_ID=:ADJ_ID ";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("ADJ_CCY", record.ADJ_CCY);
                o.setInputParameter("ADJ_ACCTNO", adj[0]);
                o.setInputParameter("ADJ_CUST", adj[1]);
                o.setInputParameter("ADJ_COST", record.ADJ_COST);
                o.setInputParameter("ADJ_AMT", record.ADJ_AMT);
                o.setInputParameter("ADJ_VDATE", record.ADJ_VDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("ADJ_MDATE", record.ADJ_VDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("ADJ_REMARK", record.ADJ_REMARK);
                o.setInputParameter("ADJ_PD", record.ADJ_PD);                
                o.setInputParameter("UPDATE_BY", record.UPDATE_USER);                
                o.setInputParameter("ADJ_ID", record.ADJ_ID);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_NOSTROADJ Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Update KKB_TB_NOSTROADJ Finish-----");
                o.closeConnection();
            }
        }

        public static object Delete(string ConnectionString, int ADJ_ID)
        {

            Log.Info("-----KKB_TB_NOSTROADJ Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_NOSTROADJ WHERE ADJ_ID='" + ADJ_ID.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_NOSTROADJ Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----Delete KKB_TB_NOSTROADJ  Finish-----");
                o.closeConnection();
            }
        }

        public static Boolean checkCurrPort(string ConnectionString, string curr, string port, string acctno, string portdate)
        {
            Log.Info("NostroDaily : checkCurrPort");

            Boolean result  = false;
            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                string sql = " SELECT CURR ,PORT ,NOS ,INITVAL ,TO_CHAR(PORT_DATE,'DD/MM/YYYY') FROM OPICINF.KKB_TB_NOSTROPORT WHERE CURR = :CURR AND PORT = :PORT AND NOS = :ACCTNO AND TO_CHAR(PORT_DATE,'DD/MM/YYYY') = :PORTDATE";                                       
                
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("CURR", curr);
                o.setInputParameter("PORT", port);
                o.setInputParameter("ACCTNO", acctno);
                o.setInputParameter("PORTDATE", portdate);
                s = o.executeReaderToDT("TB_PORT");                

                if (s != null && s.Rows.Count>0) result = true;               
            }
            catch (Exception ex)
            {
                Log.Error("-----checkCurrPort Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----checkCurrPort finish-----");
                o.closeConnection();
            }
            return result;

        }

        public static object updCurrPort(string ConnectionString, string curr, string port, string acctno, string portdate, decimal amount)
        {

            Log.Info("-----KKB_TB_NOSTROPORT updCurrPort-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_NOSTROPORT Set INITVAL=:INITVAL Where CURR=:CURR and PORT =:PORT and NOS =:NOS and TO_CHAR(PORT_DATE,'DD/MM/YYYY') =:PORTDATE";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("INITVAL", amount);
                o.setInputParameter("CURR", curr);
                o.setInputParameter("PORT", port);
                o.setInputParameter("NOS", acctno);
                o.setInputParameter("PORT_DATE", portdate);

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK"};
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_NOSTROPORT Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Update KKB_TB_NOSTROPORT Finish-----");
                o.closeConnection();
            }
        }

        public static object insertCurrPort(string ConnectionString, string curr, string port, string acctno, string portdate, decimal amount)
        {
            Log.Info("-----KKB_TB_NOSTROPORT Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "INSERT INTO OPICINF.KKB_TB_NOSTROPORT (CURR, PORT, NOS, INITVAL, PORT_DATE, CREATE_DATE, CREATE_BY) VALUES ( :CURR, :PORT, :NOS, :INITVAL, TO_DATE(:PORTDATE,'DD/MM/YYYY'), SYSDATE, :SYSTEM ) ";
                 
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("CURR", curr);
                o.setInputParameter("PORT", port);
                o.setInputParameter("NOS", acctno);
                o.setInputParameter("INITVAL", amount);
                o.setInputParameter("PORTDATE", portdate);
                o.setInputParameter("SYSTEM", "SYSTEM");

                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK"};
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_NOSTROPORT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not insert data." };
            }
            finally
            {
                Log.Info("-----KKB_TB_NOSTROPORT Finish-----");
                o.closeConnection();
            }
        }
    }
}
