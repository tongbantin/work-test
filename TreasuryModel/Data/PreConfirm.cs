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
    public class PreConfirm
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(PreConfirm));
        public int PCON_ID { get; set; }
        public string PCON_BROK { get; set; }
        public string PCON_BTYPE { get; set; }
        public string PCON_CUST { get; set; }
        public DateTime PCON_VDATE { get; set; }
        public DateTime PCON_TDATE { get; set; }
        public decimal PCON_RATE { get; set; }
        //public string PCON_CURR { get; set; }
        //public decimal PCON_AMT { get; set; }
        public string PCON_NF { get; set; }
        public string PCON_SCUR { get; set; }
        public decimal PCON_SAMT { get; set; }
        public string PCON_BCUR { get; set; }
        public decimal PCON_BAMT { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }

        public string PCON_VDATE_STR { get; set; }
        public string PCON_TDATE_STR { get; set; }

        //fill data variable
        public string F_PCON_BROK { get; set; }
        public string F_PCON_BTYPE { get; set; }
        public string F_PCON_CUST { get; set; }
        public DateTime F_PCON_VDATE { get; set; }
        public DateTime F_PCON_TDATE { get; set; }
        public decimal F_PCON_RATE { get; set; }
        public string F_PCON_NF { get; set; }
        public string F_PCON_SCUR { get; set; }
        public decimal F_PCON_SAMT { get; set; }
        public string F_PCON_BCUR { get; set; }
        public decimal F_PCON_BAMT { get; set; }

        public string F_PCON_VDATE_STR { get; set; }
        public string F_PCON_TDATE_STR { get; set; }
        

        public static object Create(string ConnectionString, PreConfirm record)
        {
            Log.Info("-----KKB_TB_PCONF Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {                
                o.openConnection();
                string sql = "INSERT INTO OPICINF.KKB_TB_PCONF ( PCON_BROK, PCON_BTYPE, PCON_CUST, PCON_VDATE, PCON_DEALDATE, PCON_RATE, PCON_SCUR , PCON_SAMT , PCON_BCUR , PCON_BAMT , PCON_NF , CREATE_DATE , CREATE_USER ) VALUES ( :PCON_BROK ,:PCON_BTYPE ,:PCON_CUST , TO_DATE(:PCON_VDATE,'DD/MM/YYYY'), TO_DATE(:PCON_DEALDATE,'DD/MM/YYYY') ,:PCON_RATE , :PCON_SCUR , :PCON_SAMT , :PCON_BCUR , :PCON_BAMT, :PCON_NF , SYSDATE , :CREATE_USER ) ";

                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("PCON_BROK", record.PCON_BROK);
                o.setInputParameter("PCON_BTYPE", record.PCON_BTYPE);
                o.setInputParameter("PCON_CUST", record.PCON_CUST);
                o.setInputParameter("PCON_VDATE", record.PCON_VDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("PCON_DEALDATE", record.PCON_TDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("PCON_RATE", record.PCON_RATE);
                o.setInputParameter("PCON_SCUR", record.PCON_SCUR);
                o.setInputParameter("PCON_SAMT", record.PCON_SAMT);
                o.setInputParameter("PCON_BCUR", record.PCON_BCUR);
                o.setInputParameter("PCON_BAMT", record.PCON_BAMT);
                o.setInputParameter("PCON_NF", record.PCON_NF);                
                o.setInputParameter("CREATE_USER", record.CREATE_USER);
                
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_PCONF Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not insert data." };
            }
            finally
            {
                Log.Info("-----KKB_TB_PCONF Finish-----");
                o.closeConnection();
            }
        }

        public static List<PreConfirm> PreConfList(string ConnectionString, string dealdate)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT PCON_ID, PCON_BROK, PCON_BTYPE, PCON_CUST, TO_CHAR(PCON_VDATE, 'MM/DD/YYYY') as PCON_VDATE, TO_CHAR(PCON_DEALDATE , 'MM/DD/YYYY') as PCON_TDATE ,PCON_NF , PCON_RATE, PCON_SCUR  , PCON_SAMT , PCON_BCUR , PCON_BAMT FROM OPICINF.KKB_TB_PCONF WHERE 1=1 ";
                if (!dealdate.Equals(""))
                {
                    sql += " AND PCON_DEALDATE = TO_DATE('" + dealdate + "','dd/MM/yyyy')  ";
                }
                sql += " ORDER BY PCON_DEALDATE ASC";

                Log.Info("----- PreConfList -----");
                Log.Info("SQL Statement : " + sql);

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PreConf");

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


                List<PreConfirm> param = new List<PreConfirm>();
                int rownum = dt.Rows.Count > 100 ? 100 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    PreConfirm p = new PreConfirm();
                    p.PCON_ID = int.Parse(dt.Rows[i]["PCON_ID"].ToString());
                    p.PCON_BROK = dt.Rows[i]["PCON_BROK"].ToString();
                    p.PCON_BTYPE = dt.Rows[i]["PCON_BTYPE"].ToString();
                    p.PCON_CUST = dt.Rows[i]["PCON_CUST"].ToString();
                    p.PCON_VDATE = DateTime.Parse(dt.Rows[i]["PCON_VDATE"].ToString());
                    p.PCON_TDATE = DateTime.Parse(dt.Rows[i]["PCON_TDATE"].ToString());
                    //p.PCON_VDATE = DateTime.ParseExact(dt.Rows[i]["PCON_VDATE"].ToString(), "dd/MM/yyyy", CultureInfo.InstalledUICulture);   
                    p.PCON_NF = dt.Rows[i]["PCON_NF"].ToString();
                    p.PCON_RATE = decimal.Parse(dt.Rows[i]["PCON_RATE"].ToString());
                    p.PCON_SCUR = dt.Rows[i]["PCON_SCUR"].ToString();
                    p.PCON_SAMT = decimal.Parse(dt.Rows[i]["PCON_SAMT"].ToString());
                    p.PCON_BCUR = dt.Rows[i]["PCON_BCUR"].ToString();
                    p.PCON_BAMT = decimal.Parse(dt.Rows[i]["PCON_BAMT"].ToString());

                    p.PCON_VDATE_STR = p.PCON_VDATE.ToString("dd/MM/yyyy");
                    p.PCON_TDATE_STR = p.PCON_TDATE.ToString("dd/MM/yyyy");

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
       
        public static object getPreConf(string ConnectionString, int PCON_ID)
                {
                    String sql = "SELECT PCON_ID, PCON_BROK, PCON_BTYPE, PCON_CUST, TO_CHAR(PCON_VDATE,'MM/DD/YYYY') as PCON_VDATE, TO_CHAR(PCON_DEALDATE , 'MM/DD/YYYY') as PCON_TDATE, PCON_RATE, PCON_SCUR , PCON_SAMT , PCON_BCUR , PCON_BAMT , PCON_NF FROM OPICINF.KKB_TB_PCONF WHERE PCON_ID = " + PCON_ID;
                    Log.Info("----- getPreConf Start-----");
                    Log.Info("SQL Statement : " + sql);

                    DataSet s = null;
                    Oracle o = Oracle.getInstance(ConnectionString);
                    try
                    {
                        o.openConnection();
                        o.createCommand(sql, CommandType.Text);
                        DataTable dt = o.executeReaderToDT("PreConfs");

                        List<PreConfirm> param = new List<PreConfirm>();
                        PreConfirm p = new PreConfirm();
                        p.PCON_ID = int.Parse(dt.Rows[0]["PCON_ID"].ToString());
                        p.PCON_BROK = dt.Rows[0]["PCON_BROK"].ToString();
                        p.PCON_BTYPE = dt.Rows[0]["PCON_BTYPE"].ToString();
                        p.PCON_CUST = dt.Rows[0]["PCON_CUST"].ToString();
                        p.PCON_VDATE = DateTime.ParseExact(dt.Rows[0]["PCON_VDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        p.PCON_TDATE = DateTime.ParseExact(dt.Rows[0]["PCON_TDATE"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture); 
                        //p.PCON_VDATE = DateTime.Parse(dt.Rows[0]["PCON_VDATE"].ToString());                                      
                        p.PCON_RATE = decimal.Parse(dt.Rows[0]["PCON_RATE"].ToString());
                        p.PCON_SCUR = dt.Rows[0]["PCON_SCUR"].ToString();
                        p.PCON_SAMT = decimal.Parse(dt.Rows[0]["PCON_SAMT"].ToString());
                        p.PCON_BCUR = dt.Rows[0]["PCON_BCUR"].ToString();
                        p.PCON_BAMT = decimal.Parse(dt.Rows[0]["PCON_BAMT"].ToString());
                        p.PCON_NF = dt.Rows[0]["PCON_NF"].ToString();

                        p.PCON_VDATE_STR = p.PCON_VDATE.ToString("dd/MM/yyyy");
                        p.PCON_TDATE_STR = p.PCON_TDATE.ToString("dd/MM/yyyy");
                      
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
                        Log.Info("-----getPreConf Finish-----");
                        o.closeConnection();
                    }    
 
         } 

        public static object Update(string ConnectionString, PreConfirm record)
        {

            Log.Info("-----KKB_TB_PCONF Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_PCONF Set PCON_BROK=:PCON_BROK, PCON_BTYPE=:PCON_BTYPE, PCON_CUST=:PCON_CUST , PCON_VDATE=TO_DATE(:PCON_VDATE, 'DD/MM/YYYY'), PCON_DEALDATE=TO_DATE(:PCON_TDATE, 'DD/MM/YYYY') , "; 
                     
                       sql += "PCON_RATE=:PCON_RATE, PCON_SCUR=:PCON_SCUR, PCON_SAMT=:PCON_SAMT, PCON_BCUR=:PCON_BCUR, PCON_BAMT=:PCON_BAMT , PCON_NF=:PCON_NF , UPDATE_DATE =TO_DATE(SYSDATE,'DD/MM/YYYY'), UPDATE_USER =:UPDATE_USER  Where PCON_ID=:PCON_ID ";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("PCON_BROK", record.PCON_BROK);
                o.setInputParameter("PCON_BTYPE", record.PCON_BTYPE);
                o.setInputParameter("PCON_CUST", record.PCON_CUST);
                o.setInputParameter("PCON_VDATE", record.PCON_VDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("PCON_DEALDATE", record.PCON_TDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("PCON_RATE", record.PCON_RATE);
                o.setInputParameter("PCON_SCUR", record.PCON_SCUR);
                o.setInputParameter("PCON_SAMT", record.PCON_SAMT);
                o.setInputParameter("PCON_BCUR", record.PCON_BCUR);
                o.setInputParameter("PCON_BAMT", record.PCON_BAMT);
                o.setInputParameter("PCON_NF", record.PCON_NF);
                o.setInputParameter("UPDATE_USER", record.CREATE_USER);

                o.setInputParameter("PCON_ID", record.PCON_ID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_PCONF Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_PCONF Finish-----");
                o.closeConnection();
            }
        }

        public static object Delete(string ConnectionString, int pconid)
        {

            Log.Info("-----KKB_TB_PCONF Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_PCONF WHERE PCON_ID='" + pconid.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_PCONF Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_PCONF Finish-----");
                o.closeConnection();
            }
        }

        public static object Filldata(string ConnectionString, string detail, string allcurrency, string brokname, string seltype, string selnf)
        {
            String sql;
            sql = "SELECT BROKNAME , SELLCODE , BUYCODE , VDATECODE , TDATECODE , RATECODE , DATEFORMAT , TYPESTYLE , NF FROM OPICINF.KKB_TB_PCONF_CONFIG WHERE BROKNAME = '" + brokname + "' AND TYPESTYLE = '" + seltype + "' ";
                   
            if (seltype.Equals("Swap")) {
                sql += " AND NF = '" + selnf + "'";
            }                 

            Log.Info("----- getPreConf Start-----");
            Log.Info("SQL Statement : " + sql);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);

            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable rs = o.executeReaderToDT("PConfig");

                //List<Model_MS_PCONFIG> param = new List<Model_MS_PCONFIG>();
                //Model_MS_PCONFIG p = new Model_MS_PCONFIG();

                string sSellCode = rs.Rows[0]["SELLCODE"].ToString();
                string sBuyCode = rs.Rows[0]["BUYCODE"].ToString();
                string sVdate = rs.Rows[0]["VDATECODE"].ToString();
                string sTdate = rs.Rows[0]["TDATECODE"].ToString();
                string sRate = rs.Rows[0]["RATECODE"].ToString();
                string dateformat = rs.Rows[0]["DATEFORMAT"].ToString();              

                List<PreConfirm> param = new List<PreConfirm>();
                PreConfirm p = new PreConfirm();
                string temp = "";
                
                // BOABKK 
       /*       string sSellCode = "Sells";
                string sBuyCode = "Buys";
                string sVdate = "ValueDate";
                string sRate = "AllInRate";
                string dateformat = "ddMMMyyyy";   

                // JPMchase , LDN KKBANK
 /*             string sSellCode = "SELL";
                string sBuyCode = "BUY";
                string sVdate = "ValueDate";
                string sRate = "AllInRate";
                string dateformat = "dd-MMM-yyyy";     

                // Wallstreet
 /*             string sSellCode = "SoldAmount";
                string sBuyCode = "EquivalentAmount";
                string sVdate = "ValueDate";
                string sRate = "ExchangeRate";
                string dateformat = "dd/MM/yyyy";       

                // BNPPARIS
                string sSellCode = "Sell";
                string sBuyCode = "Buy";
                string sVdate = "ValueDate";
                string sRate = "Rate";
                string dateformat = "dd-MMM-yyyy";  */

               


                string[] lst = detail.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                foreach (string value in lst)
                {
                    string code = value.Replace(":", "").Replace(" ", String.Empty);

                    //sellCode
                    int ss = code.IndexOf(sSellCode);
                    if (ss != -1)
                    {
                        temp = code.Substring(ss + sSellCode.Length);

                        if (allcurrency.IndexOf(temp.Substring(0, 3)) != -1)
                        {
                            p.F_PCON_SCUR = temp.Substring(0, 3);
                            p.F_PCON_SAMT = decimal.Parse(temp.Substring(3));
                        }
                        else {
                            p.F_PCON_SCUR = temp.Substring(temp.Length - 3 , 3);
                            p.F_PCON_SAMT = decimal.Parse(temp.Substring(0, (temp.Length - 3)));
                        }
                    }

                    //buyCode 
                    int sb = code.IndexOf(sBuyCode);
                    if (sb != -1)
                    {
                        temp = code.Substring(sb + sBuyCode.Length);

                        if (allcurrency.IndexOf(temp.Substring(0, 3)) != -1)
                        {
                            p.F_PCON_BCUR = temp.Substring(0, 3);
                            p.F_PCON_BAMT = decimal.Parse(temp.Substring(3));
                        }else{
                            p.F_PCON_BCUR = temp.Substring(temp.Length - 3 , 3);
                            p.F_PCON_BAMT = decimal.Parse(temp.Substring(0, (temp.Length - 3)));
                        }
                    }

                    //sVdate 
                    int sv = code.IndexOf(sVdate);
                    if (sv != -1)
                    {
                        temp = code.Substring(sv + sVdate.Length);
                        DateTime dt = DateTime.ParseExact(temp.ToString(), dateformat , CultureInfo.InvariantCulture);
                        p.F_PCON_VDATE_STR = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                   }

                    //sTdate 
                    int st = code.IndexOf(sTdate);
                    if (st != -1)
                    {
                        DateTime dt;
                        if (sTdate.Equals(""))
                        {
                            dt = DateTime.Now;
                                //DateTime.ParseExact(DateTime.Now.ToString(dateformat), dateformat, CultureInfo.InvariantCulture); 
                        }
                        else {
                            temp = code.Substring(st + sTdate.Length);
                             dt = DateTime.ParseExact(temp.ToString(), dateformat, CultureInfo.InvariantCulture);                        
                        }                        
                        
                        p.F_PCON_TDATE_STR = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    //sRate 
                    int sr = code.IndexOf(sRate);
                    if (sr != -1)
                    {
                        p.F_PCON_RATE = decimal.Parse(code.Substring(sr + sRate.Length));
                    }

                    i++;

                }

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
        
        }
    
    
    }     
}
