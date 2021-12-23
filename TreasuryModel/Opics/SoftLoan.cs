using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Collections;
using System.Data;
using log4net;
using System.Globalization;

namespace TreasuryModel.Opics
{
    public class SoftLoan
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(SoftLoan));

        public int RECID { get; set; }
        public DateTime IMPDATE { get; set; }
        public DateTime LSTUPDATE { get; set; }
        public string RECNO { get; set; }
        public string CTPYCODE { get; set; }
        public string BOOKNO { get; set; }
        public DateTime? BOOKDATE { get; set; }
        public string LOANTYPE { get; set; }
        public string CUSTNAME { get; set; }
        public DateTime? PVALUEDATE { get; set; }
        public string PDEALNO { get; set; }
        public DateTime? PDEALDATE {get;set;} 
        public DateTime? PMATDATE { get; set; }
        public Decimal? PDEALAMT { get; set; }
        public Decimal? PPAYAMT { get; set; }
        public string DEALNO { get; set; }
        public DateTime? VALUEDATE { get; set; }
        public DateTime? MATDATE { get; set; }
        public Decimal? AMOUNT { get; set; }
        public Decimal? INTRATE { get; set; }
        public string REFERPROJ { get; set; }
        public string REFERDIV { get; set; }

        public ArrayList save(string ConnectionString, string round, List<SoftLoan> sft)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();
                // Disabled Old Field

                int i = 0; 
                foreach (SoftLoan d in sft)
                {
                    try
                    {
                        string sql = "Insert Into OPICINF.KKB_TB_DLSFLN(IMPDATE, LSTUPDATE, RECNO, CPTYCODE, BOOKNO, LOANTYPE, CUSTNAME, PDEALNO, PDEALAMT, PPAYAMT, DEALNO, VALUEDATE, MATDATE, AMOUNT, INTRATE, REFERPROJ, REFERDIV, ROUND) ";
                        //sql += " Values( SYSDATE, SYSDATE, :RECNO, :CPTYCODE, :BOOKNO, :BOOKDATE, :LOANTYPE, :CUSTNAME, :PVALUEDATE, :PDEALNO, :PDEALDATE, :PMATDATE, :PDEALAMT, :PPAYAMT, :DEALNO, :VALUEDATE, :MATDATE, :AMOUNT, :INTRATE, :REFERPROJ, :REFERDIV )";
                        sql += " Values( SYSDATE, SYSDATE, :RECNO, :CPTYCODE, :BOOKNO, :LOANTYPE, :CUSTNAME, :PDEALNO, :PDEALAMT, :PPAYAMT, :DEALNO, :VALUEDATE, :MATDATE, :AMOUNT, :INTRATE, :REFERPROJ, :REFERDIV, :ROUND )";
                        o.createCommand(sql, CommandType.Text);
                        o.setCharInputParameter("RECNO", d.RECNO.Trim());
                        o.setCharInputParameter("CPTYCODE", d.CTPYCODE.Trim());
                        o.setCharInputParameter("BOOKNO", d.BOOKNO.Trim());
                        //if (d.BOOKDATE == null)
                        //    o.setCharInputParameter("BOOKDATE", "NULL");
                        //else
                        //    o.setDateInputParameter("BOOKDATE", d.BOOKDATE);
                        o.setCharInputParameter("LOANTYPE", d.LOANTYPE.Trim());
                        o.setNCharInputParameter("CUSTNAME", d.CUSTNAME.Trim());
                        //if (d.PVALUEDATE == null)
                        //    o.setCharInputParameter("PVALUEDATE", "NULL");
                        //else
                        //    o.setDateInputParameter("PVALUEDATE", d.PVALUEDATE);
                        o.setCharInputParameter("PDEALNO", d.PDEALNO.Trim());
                        //if (d.PDEALDATE == null)
                        //    o.setCharInputParameter("PDEALDATE", "NULL");
                        //else
                        //    o.setDateInputParameter("PDEALDATE", d.PDEALDATE);
                        //if (d.PMATDATE == null)
                        //    o.setCharInputParameter("PMATDATE", "NULL");
                        //else
                        //    o.setDateInputParameter("PMATDATE", d.PMATDATE);
                        o.setDecimalInputParameter("PDEALAMT", d.PDEALAMT.Value);
                        o.setDecimalInputParameter("PPAYAMT", d.PPAYAMT.Value);
                        o.setCharInputParameter("DEALNO", d.DEALNO.Trim());
                        if (d.VALUEDATE == null)
                            o.setCharInputParameter("VALUEDATE", "NULL");
                        else
                            o.setDateInputParameter("VALUEDATE", d.VALUEDATE);
                        if (d.MATDATE == null)
                            o.setCharInputParameter("MATDATE", "NULL");
                        else
                            o.setDateInputParameter("MATDATE", d.MATDATE);
                        o.setDecimalInputParameter("AMOUNT", d.AMOUNT.Value);
                        o.setDecimalInputParameter("INTRATE", d.INTRATE);
                        o.setNCharInputParameter("REFERPROJ", d.REFERPROJ.Trim());
                        o.setNCharInputParameter("REFERDIV", d.REFERDIV.Trim());
                        o.setCharInputParameter("ROUND", round);
                        o.executeNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //error.Add(d.Records);
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
                //Log.Error("----Start Error-----");
                //Log.Error(ex.Message);
            }
            finally
            {
                o.closeConnection();
            }
            return error;
        }

        public List<SoftLoan> SoftLoanData(string ConnectionString, string round)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<SoftLoan> param = new List<SoftLoan>();
            try
            {

                string sql = "Select RECID, RECNO, CPTYCODE, BOOKNO, BOOKDATE , LOANTYPE, CUSTNAME, PVALUEDATE, PDEALNO, PDEALDATE, PMATDATE, PDEALAMT, PPAYAMT, DEALNO, VALUEDATE, MATDATE, AMOUNT, INTRATE, REFERPROJ, REFERDIV From OPICINF.KKB_TB_DLSFLN Where ROUND='" + round + "' ";

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("SoftLoan");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        SoftLoan m = new SoftLoan();
                        m.RECID = int.Parse(dt.Rows[i]["RECID"].ToString());
                        m.RECNO = dt.Rows[i]["RECNO"].ToString();
                        m.BOOKNO = dt.Rows[i]["BOOKNO"].ToString();
                        m.CTPYCODE = dt.Rows[i]["CPTYCODE"].ToString();
                        m.CUSTNAME = dt.Rows[i]["CUSTNAME"].ToString();
                        m.DEALNO = dt.Rows[i]["DEALNO"].ToString();
                        m.LOANTYPE = dt.Rows[i]["LOANTYPE"].ToString();
                        m.PDEALNO = dt.Rows[i]["PDEALNO"].ToString();
                        m.REFERDIV = dt.Rows[i]["REFERDIV"].ToString();
                        m.REFERPROJ = dt.Rows[i]["REFERPROJ"].ToString();

                        if (!dt.Rows[i]["AMOUNT"].ToString().Trim().Equals(""))
                            m.AMOUNT = decimal.Parse(dt.Rows[i]["AMOUNT"].ToString());
                        if (!dt.Rows[i]["INTRATE"].ToString().Trim().Equals(""))
                            m.INTRATE = decimal.Parse(dt.Rows[i]["INTRATE"].ToString());
                        if (!dt.Rows[i]["PDEALAMT"].ToString().Trim().Equals(""))
                            m.PDEALAMT = decimal.Parse(dt.Rows[i]["PDEALAMT"].ToString());
                        if (!dt.Rows[i]["PPAYAMT"].ToString().Trim().Equals(""))
                            m.PPAYAMT = decimal.Parse(dt.Rows[i]["PPAYAMT"].ToString());

                        if (!dt.Rows[i]["BOOKDATE"].ToString().Trim().Equals(""))
                            m.BOOKDATE = DateTime.Parse(dt.Rows[i]["BOOKDATE"].ToString());
                        if (!dt.Rows[i]["VALUEDATE"].ToString().Trim().Equals(""))
                            m.VALUEDATE = DateTime.Parse(dt.Rows[i]["VALUEDATE"].ToString());
                        if (!dt.Rows[i]["MATDATE"].ToString().Trim().Equals(""))
                            m.MATDATE = DateTime.Parse(dt.Rows[i]["MATDATE"].ToString());
                        if (!dt.Rows[i]["PDEALDATE"].ToString().Trim().Equals(""))
                            m.PDEALDATE = DateTime.Parse(dt.Rows[i]["PDEALDATE"].ToString());
                        if (!dt.Rows[i]["PVALUEDATE"].ToString().Trim().Equals(""))
                            m.PVALUEDATE = DateTime.Parse(dt.Rows[i]["PVALUEDATE"].ToString());
                        if (!dt.Rows[i]["PMATDATE"].ToString().Trim().Equals(""))
                            m.PMATDATE = DateTime.Parse(dt.Rows[i]["PMATDATE"].ToString());

                        param.Add(m);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
                return param;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally {
                o.closeConnection();
            }
        }

        public static object SoftLoanList(string valuedate1,string matdate1,string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<SoftLoan> param = new List<SoftLoan>();
            try
            {

                string sql = "Select RECID, RECNO, CPTYCODE, BOOKNO, BOOKDATE , LOANTYPE, CUSTNAME, PVALUEDATE, PDEALNO, PDEALDATE, PMATDATE, PDEALAMT, PPAYAMT, DEALNO, VALUEDATE, MATDATE, AMOUNT, INTRATE, REFERPROJ, REFERDIV From OPICINF.KKB_TB_DLSFLN Where 1=1 ";
                if (valuedate1 != "")
                {
                    sql += " AND VALUEDATE = TO_DATE('" + valuedate1 + "','DD/MM/YYYY') ";
                }
                if (matdate1 != "")
                    sql += " AND MATDATE =TO_DATE('" + matdate1 + "','DD/MM/YYYY') ";
                sql += " Order By RECID";

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("SoftLoan");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        SoftLoan m = new SoftLoan();
                        m.RECID = int.Parse(dt.Rows[i]["RECID"].ToString());
                        m.RECNO = dt.Rows[i]["RECNO"].ToString();
                        m.BOOKNO = dt.Rows[i]["BOOKNO"].ToString();
                        m.CTPYCODE = dt.Rows[i]["CPTYCODE"].ToString();
                        m.CUSTNAME = dt.Rows[i]["CUSTNAME"].ToString();
                        m.DEALNO = dt.Rows[i]["DEALNO"].ToString();
                        m.LOANTYPE = dt.Rows[i]["LOANTYPE"].ToString();
                        m.PDEALNO = dt.Rows[i]["PDEALNO"].ToString();
                        m.REFERDIV = dt.Rows[i]["REFERDIV"].ToString();
                        m.REFERPROJ = dt.Rows[i]["REFERPROJ"].ToString();

                        if (!dt.Rows[i]["AMOUNT"].ToString().Trim().Equals(""))
                            m.AMOUNT = decimal.Parse(dt.Rows[i]["AMOUNT"].ToString());
                        if (!dt.Rows[i]["INTRATE"].ToString().Trim().Equals(""))
                            m.INTRATE = decimal.Parse(dt.Rows[i]["INTRATE"].ToString());
                        if (!dt.Rows[i]["PDEALAMT"].ToString().Trim().Equals(""))
                            m.PDEALAMT = decimal.Parse(dt.Rows[i]["PDEALAMT"].ToString());
                        if (!dt.Rows[i]["PPAYAMT"].ToString().Trim().Equals(""))
                            m.PPAYAMT = decimal.Parse(dt.Rows[i]["PPAYAMT"].ToString());

                        if (!dt.Rows[i]["BOOKDATE"].ToString().Trim().Equals(""))
                            m.BOOKDATE = DateTime.Parse(dt.Rows[i]["BOOKDATE"].ToString());
                        if (!dt.Rows[i]["VALUEDATE"].ToString().Trim().Equals(""))
                            m.VALUEDATE = DateTime.Parse(dt.Rows[i]["VALUEDATE"].ToString());
                        if (!dt.Rows[i]["MATDATE"].ToString().Trim().Equals(""))
                            m.MATDATE = DateTime.Parse(dt.Rows[i]["MATDATE"].ToString());
                        if (!dt.Rows[i]["PDEALDATE"].ToString().Trim().Equals(""))
                            m.PDEALDATE = DateTime.Parse(dt.Rows[i]["PDEALDATE"].ToString());
                        if (!dt.Rows[i]["PVALUEDATE"].ToString().Trim().Equals(""))
                            m.PVALUEDATE = DateTime.Parse(dt.Rows[i]["PVALUEDATE"].ToString());
                        if (!dt.Rows[i]["PMATDATE"].ToString().Trim().Equals(""))
                            m.PMATDATE = DateTime.Parse(dt.Rows[i]["PMATDATE"].ToString());

                        param.Add(m);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }

                var rec = param.Count > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList<SoftLoan>() : param.ToList<SoftLoan>();

                return new { Result = "OK", Records = rec, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return new { Result = "OK", Records = param, TotalRecordCount = param.Count };
            }
            finally
            {
                o.closeConnection();
            }
        }

        //public static object Create(string ConnectionString, SoftLoan record)
        //{
        //    //string sql = "Insert Into OPICINF.KKB_TB_DLSFLN(IMPDATE, LSTUPDATE) ";
        //    //sql += " Values( SYSDATE, SYSDATE, :RECNO, :CPTYCODE, :BOOKNO, :BOOKDATE, :LOANTYPE, :CUSTNAME, :PVALUEDATE, :PDEALNO, :PDEALDATE, :PMATDATE, :PDEALAMT, :PPAYAMT, :DEALNO, :VALUEDATE, :MATDATE, :AMOUNT, :INTRATE, :REFERPROJ, :REFERDIV )";
        //    string sql = "Insert Into OPICINF.KKB_TB_DLSFLN(IMPDATE, LSTUPDATE, RECNO, CPTYCODE, BOOKNO, CUSTNAME, DEALNO, LOANTYPE, PDEALNO, REFERPROJ, REFERDIV, PDEALAMT, PPAYAMT, AMOUNT, INTRATE, BOOKDATE, VALUEDATE, MATDATE, PDEALDATE, PVALUEDATE, PMATDATE) ";
        //    sql += " Values( SYSDATE, SYSDATE, :RECNO, :CPTYCODE, :BOOKNO, :CUSTNAME, :DEALNO, :LOANTYPE, :PDEALNO, :REFERPROJ, :REFERDIV, :PDEALAMT, :PPAYAMT, :AMOUNT, :INTRATE, :BOOKDATE, :VALUEDATE, :MATDATE, :PDEALDATE, :PVALUEDATE, :PMATDATE)";

        //    Log.Info("-----KKB_TB_DLSFLN Start insert-----");
        //    Log.Info("SQL Statement : " + sql);

        //    int row = 0;
        //    Oracle o = Oracle.getInstance(ConnectionString);
        //    try
        //    {
        //        o.openConnection(); 
        //        o.createCommand(sql, CommandType.Text);
        //        o.setNCharInputParameter("RECNO", record.RECNO.Trim());
        //        o.setNCharInputParameter("CPTYCODE", record.CTPYCODE.ToString().Trim());
        //        o.setNCharInputParameter("BOOKNO", record.BOOKNO.ToString().Trim());
        //        o.setNCharInputParameter("CUSTNAME", record.CUSTNAME.ToString().Trim());
        //        o.setNCharInputParameter("DEALNO", record.DEALNO.Trim());
        //        o.setNCharInputParameter("LOANTYPE", record.LOANTYPE.ToString().Trim());
        //        o.setNCharInputParameter("PDEALNO", record.PDEALNO.ToString().Trim());
        //        o.setNCharInputParameter("REFERPROJ", record.REFERPROJ.Trim());
        //        o.setNCharInputParameter("REFERDIV", record.REFERDIV.Trim());

        //        if (record.PDEALAMT == null)
        //            o.setDecimalInputParameter("PDEALAMT", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("PDEALAMT", record.PDEALAMT.Value);
        //        if (record.PPAYAMT == null)
        //            o.setDecimalInputParameter("PPAYAMT", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("PPAYAMT", record.PPAYAMT.Value);
        //        if (record.AMOUNT == null)
        //            o.setDecimalInputParameter("AMOUNT", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("AMOUNT", record.AMOUNT.Value);
        //        if (record.INTRATE == null)
        //            o.setDecimalInputParameter("INTRATE", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("INTRATE", record.INTRATE.Value);

        //        if (record.BOOKDATE == null)
        //            o.setCharInputParameter("BOOKDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("BOOKDATE", record.BOOKDATE);
        //        if (record.VALUEDATE == null)
        //            o.setCharInputParameter("VALUEDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("VALUEDATE", record.VALUEDATE);
        //        if (record.MATDATE == null)
        //            o.setCharInputParameter("MATDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("MATDATE", record.MATDATE);
        //        if (record.PDEALDATE == null)
        //            o.setCharInputParameter("PDEALDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("PDEALDATE", record.PDEALDATE);
        //        if (record.PVALUEDATE == null)
        //            o.setCharInputParameter("PVALUEDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("PVALUEDATE", record.PVALUEDATE);
        //        if (record.PMATDATE == null)
        //            o.setCharInputParameter("PMATDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("PMATDATE", record.PMATDATE);

        //        row = o.executeNonQuery();
        //        if (row > 0)
        //            return new { Result = "OK", Record = record };
        //        else
        //            return new { Result = "ERROR", Message = "Could not insert data." }; 
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("-----Insert KKB_TB_DLSFLN Error-----");
        //        Log.Error(ex.Message);
        //        return new { Result = "ERROR", Message = ex.Message };
        //    }
        //    finally
        //    {
        //        Log.Info("-----KKB_TB_DLSFLN Finish-----");
        //        o.closeConnection();
        //    }
        //}

        //public static object Update(string ConnectionString, SoftLoan record)
        //{
        //    //String sql = "Update OPICINF.KKB_TB_DLSFLN Set RECNO=:RECNO, CPTYCODE=:CPTYCODE, BOOKNO=:BOOKNO, LOANTYPE=:LOANTYPE, CUSTNAME=:CUSTNAME, PDEALNO=:PDEALNO, DEALNO=:DEALNO, REFERPROJ=:REFERPROJ, REFERDIV=:REFERDIV Where RECID=:RECID ";
        //    String sql = "Update OPICINF.KKB_TB_DLSFLN Set RECNO=:RECNO, CPTYCODE=:CPTYCODE, BOOKNO=:BOOKNO, CUSTNAME=:CUSTNAME, DEALNO=:DEALNO, LOANTYPE=:LOANTYPE, PDEALNO=:PDEALNO, REFERPROJ=:REFERPROJ, REFERDIV=:REFERDIV, PDEALAMT=:PDEALAMT, PPAYAMT=:PPAYAMT, AMOUNT=:AMOUNT, INTRATE=:INTRATE, BOOKDATE=:BOOKDATE, VALUEDATE=:VALUEDATE, MATDATE=:MATDATE, PDEALDATE=:PDEALDATE, PVALUEDATE=:PVALUEDATE, PMATDATE=:PMATDATE Where RECID=:RECID ";

        //    Log.Info("-----KKB_TB_DLSFLN Start update-----");
        //    Log.Info("SQL Statement : " + sql);

        //    int row = 0;
        //    Oracle o = Oracle.getInstance(ConnectionString);
        //    try
        //    {
        //        o.openConnection();
        //        o.createCommand(sql, CommandType.Text);
        //        o.setNCharInputParameter("RECNO", record.RECNO);
        //        o.setNCharInputParameter("CPTYCODE", record.CTPYCODE);
        //        o.setNCharInputParameter("BOOKNO", record.BOOKNO);
        //        o.setNCharInputParameter("CUSTNAME", record.CUSTNAME);
        //        o.setNCharInputParameter("DEALNO", record.DEALNO);
        //        o.setNCharInputParameter("LOANTYPE", record.LOANTYPE);
        //        o.setNCharInputParameter("PDEALNO", record.PDEALNO);
        //        o.setNCharInputParameter("REFERPROJ", record.REFERPROJ);
        //        o.setNCharInputParameter("REFERDIV", record.REFERDIV);

        //        if (record.PDEALAMT == null)
        //            o.setDecimalInputParameter("PDEALAMT", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("PDEALAMT", record.PDEALAMT.Value);
        //        if (record.PPAYAMT == null)
        //            o.setDecimalInputParameter("PPAYAMT", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("PPAYAMT", record.PPAYAMT.Value);
        //        if (record.AMOUNT == null)
        //            o.setDecimalInputParameter("AMOUNT", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("AMOUNT", record.AMOUNT.Value);
        //        if (record.INTRATE == null)
        //            o.setDecimalInputParameter("INTRATE", DBNull.Value);
        //        else
        //            o.setDecimalInputParameter("INTRATE", record.INTRATE.Value);

        //        if (record.BOOKDATE == null)
        //            o.setDateInputParameter("BOOKDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("BOOKDATE", record.BOOKDATE);
        //        if (record.VALUEDATE == null)
        //            o.setDateInputParameter("VALUEDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("VALUEDATE", record.VALUEDATE);
        //        if (record.MATDATE == null)
        //            o.setDateInputParameter("MATDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("MATDATE", record.MATDATE);
        //        if (record.PDEALDATE == null)
        //            o.setDateInputParameter("PDEALDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("PDEALDATE", record.PDEALDATE);
        //        if (record.PVALUEDATE == null)
        //            o.setDateInputParameter("PVALUEDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("PVALUEDATE", record.PVALUEDATE);
        //        if (record.PMATDATE == null)
        //            o.setDateInputParameter("PMATDATE", DBNull.Value);
        //        else
        //            o.setDateInputParameter("PMATDATE", record.PMATDATE);

        //        o.setIntegerInputParameter("RECID", record.RECID);
        //        row = o.executeNonQuery();
        //        if (row > 0)
        //            return new { Result = "OK" };
        //        else
        //            return new { Result = "ERROR", Message = "Could not modify data." };
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("-----Update KKB_TB_DLSFLN Error-----");
        //        Log.Error(ex.Message);
        //        return new { Result = "ERROR", Message = ex.Message };
        //    }
        //    finally
        //    {
        //        Log.Info("-----KKB_TB_DLSFLN Finish-----");
        //        o.closeConnection();
        //    }
        //}

        public static void UpdateDealno(string ConnectionString)
        {
            Log.Info("-----KKB_TB_DLSFLN Start Update DealNo-----"); 
 
            DataTable dt = new DataTable();

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                String sql = "SELECT * FROM OPICINF.KKB_TB_DLSFLN WHERE DEALNO IS NULL ";
                o.createCommand(sql, CommandType.Text);
                dt = o.executeReaderToDT("DLSFLN");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string sqlExec = "UPDATE OPICINF.KKB_TB_DLSFLN SET DEALNO=";
                        sqlExec += "NVL((SELECT INTH.DEALNO FROM OPICS.IDLD INNER JOIN OPICS.INTH ON IDLD.BR=INTH.BR AND IDLD.SERVER=INTH.SERVER AND IDLD.FEDEALNO=INTH.FEDEALNO WHERE INTH.DEALNO IS NOT NULL AND INTH.BOODATE IS NOT NULL AND INTH.BOOTIME IS NOT NULL AND TRIM(IDLD.DEALTEXT)='\\SOFTLOAN" + Convert.ToInt32(dt.Rows[i]["RECID"].ToString()).ToString("000000") + "'),NULL) ";
                        sqlExec += "WHERE RECID='" + dt.Rows[i]["RECID"].ToString().Trim() + "'";
                        o.createCommand(sqlExec, CommandType.Text);
                        o.executeNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message + "After Get Deal");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_DLSFLN Error-----");
                Log.Error(ex.Message); 
            }
            finally
            {
                Log.Info("-----KKB_TB_DLSFLN Finish-----");
                o.closeConnection();
            }

            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //try
            //{
            //    using (OracleConnection oracle = new OracleConnection(Config.ConnectionString))
            //    {
            //        if (oracle.State == ConnectionState.Open)
            //            oracle.Close();
            //        oracle.Open();
            //        string sql = "SELECT * FROM OPICINF.KKB_TB_FCDTRY_DEAL WHERE DEALNO IS NULL";
            //        using (OracleCommand command = new OracleCommand(sql, oracle))
            //        {
            //            OracleDataAdapter adapter = new OracleDataAdapter(command);
            //            adapter.Fill(ds);
            //        }

            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            try
            //            {
            //                if (!ds.Tables[0].Rows[i]["DEALTEXT"].ToString().Trim().Equals(""))
            //                {
            //                    string sqlExec = "UPDATE OPICINF.KKB_TB_FCDTRY_DEAL SET DEALNO=";
            //                    sqlExec += "NVL((SELECT INTH.DEALNO FROM OPICS.IFXD INNER JOIN OPICS.INTH ON IFXD.BR=INTH.BR AND IFXD.SERVER=INTH.SERVER AND IFXD.FEDEALNO=INTH.FEDEALNO WHERE INTH.DEALNO IS NOT NULL AND INTH.BOODATE IS NOT NULL AND INTH.BOOTIME IS NOT NULL AND TRIM(IFXD.DEALTEXT)=TRIM(KKB_TB_FCDTRY_DEAL.DEALTEXT)),NULL) ";
            //                    sqlExec += "WHERE TRIM(DEALTEXT)='" + ds.Tables[0].Rows[i]["DEALTEXT"].ToString().Trim() + "'";
            //                    //Log.Debug(sqlExec);
            //                    using (OracleCommand command = new OracleCommand(sqlExec, oracle))
            //                    {
            //                        command.ExecuteNonQuery();
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Log.Error(ex.Message + "After Get Deal");
            //            }
            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex.Message);
            //}
        }

        public static object Delete(string ConnectionString, int RecID)
        {
            String sql = "Delete From OPICINF.KKB_TB_DLSFLN Where RECID=:RECID ";

            Log.Info("-----KKB_TB_DLSFLN Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("RECID", RecID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_DLSFLN Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_DLSFLN Finish-----");
                o.closeConnection();
            }
        }
    }
}
