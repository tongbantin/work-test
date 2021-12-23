using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net;
using System.IO;
using System.Text.RegularExpressions;
using Oracle.DataAccess.Client;
using System.Configuration;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TreasuryJob.Model
{
    public class SWIFTFile
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(SWIFTFile));

        public int SWIFTFILEID { get; set; }
        public string FILENAME { get; set; }
        public string MSGCODE { get; set; }
        public string DEALNO { get; set; }
        public string MSG { get; set; }
        public string REMARK { get; set; }

        public static Boolean DownloadFileMessage(string ConnectionString, string DownloadFolder, string folder, string [] series)
        { 
            try
            {
                using (OracleConnection connect = new OracleConnection(ConnectionString))
                {
                    //Get Files From Initial Folder
                    Log.Debug(DownloadFolder);
                    string[] files = Directory.GetFiles(DownloadFolder);
                    if (connect.State == ConnectionState.Closed)
                        connect.Open();

                    string sql = "SELECT FILENAME FROM OPICINF.KKB_TB_SWIFTMSG_FILE";
                    DataTable dtMsg = new DataTable();
                    using (OracleCommand command = new OracleCommand(sql, connect))
                    {
                        //o.createCommand("SELECT FILENAME FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE SUBSTR(MSG,INSTR(MSG, ':30T:')+5,8)='" + DateTime.Now.ToString("yyyyMMdd") + "'", CommandType.Text);
                        dtMsg = new DataTable("SWIFTMSG_FILES");
                        OracleDataAdapter da = new OracleDataAdapter(command);
                        da.Fill(dtMsg);
                        da.Dispose();

                    }

                    Array.Sort(files);
                    foreach (string sourceFileName in files)
                    {
                        try
                        {
                            string filename = new FileInfo(sourceFileName).Name;


                            DataRow existsRow = dtMsg.Select().FirstOrDefault(t => t["FILENAME"].ToString().Equals(filename) == true);
                            if (existsRow == null)
                            {
                                
                                Log.Debug("File Exists " + sourceFileName);
                                String messageSwift = File.ReadAllText(sourceFileName);
                                //splite swift message
                                String[] message = messageSwift.Split('$');

                                using(OracleCommand command = new OracleCommand()){
                                    foreach (String msg in message)
                                    {
                                        if (!msg.Trim().Equals(""))
                                        {
                                            Dictionary<string, string[]> swiftmsg = new Dictionary<string, string[]>();
                                            if (msg.IndexOf("{1:F01") >= 0)
                                                swiftmsg = SpliteSWIFTMsg(msg.Substring(msg.IndexOf("{1:F01")));
                                            else
                                                swiftmsg = SpliteSWIFTMsg(msg);
                                            string msgCode = swiftmsg["2"][1].Substring(1, 3);
                                            string dealno = "";
                                            if (Array.IndexOf(swiftmsg["4"], "20") >= 0)
                                            {
                                                Log.Debug("IndexOf 20 : " + Array.IndexOf(swiftmsg["4"], "20"));
                                                if (!Regex.Match(swiftmsg["4"][Array.IndexOf(swiftmsg["4"], "20") + 1], "[^a-z]|[^A-Z]", RegexOptions.IgnoreCase).Success)
                                                    dealno = dealno.Substring(2, 7);
                                            }

                                            //Splite Message
                                            StringBuilder strHeader = new StringBuilder();
                                            StringBuilder strMessage = new StringBuilder();

                                            if (msg.IndexOf("{1:F01") >= 0)
                                            {
                                                strHeader.Append(msg.Substring(0, msg.IndexOf("{1:F01")));
                                                strMessage.Append(msg.Substring(msg.IndexOf("{1:F01")));
                                            }
                                            else
                                            {
                                                strHeader.Append("");
                                                strMessage.Append(msg);
                                            }

                                            //Insert each swift message to database
                                            sql = "INSERT INTO OPICINF.KKB_TB_SWIFTMSG_FILE(FILENAME, FULLPATH, FOLDER, MSGCODE, DEALNO, HEADER1, HEADER2, MSG1, MSG2, MSG3, MSG4, MSG5, MSG6, MSG7, MSG8, REMARK, LOG_INSERTBYUSERID, LOG_INSERTDATE, INCOMINGDATE) VALUES(:FILENAME, :FULLPATH, :FOLDER, :MSGCODE, :DEALNO, :HEADER1, :HEADER2, :MSG1, :MSG2, :MSG3, :MSG4, :MSG5, :MSG6, :MSG7, :MSG8, :REMARK, 'SYSTEM', SYSDATE, :INCOMINGDATE)";

                                            command.Connection = connect;
                                            command.CommandType = CommandType.Text;
                                            command.CommandText = sql;
                                            command.Parameters.Add("FILENAME", OracleDbType.Varchar2, filename, ParameterDirection.Input);
                                            command.Parameters.Add("FULLPATH", OracleDbType.Varchar2, sourceFileName, ParameterDirection.Input);
                                            command.Parameters.Add("FOLDER", OracleDbType.Varchar2, folder, ParameterDirection.Input);
                                            command.Parameters.Add("MSGCODE", OracleDbType.Varchar2, msgCode, ParameterDirection.Input);
                                            command.Parameters.Add("DEALNO", OracleDbType.Char, dealno, ParameterDirection.Input);
                                            command.Parameters.Add("HEADER1", OracleDbType.Char, strHeader.Length == 0 ? "" : strHeader.Length < 1000 ? strHeader.ToString().Substring(0, strHeader.Length) : strHeader.ToString().Substring(0, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("HEADER2", OracleDbType.Char, strHeader.Length < 1000 ? "" : strHeader.ToString().Substring(1000, strHeader.Length - 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG1", OracleDbType.Char, strMessage.Length < 1000 ? strMessage.ToString().Substring(0, strMessage.Length) : strMessage.ToString().Substring(0, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG2", OracleDbType.Char, strMessage.Length <= 1000 ? "" : strMessage.Length >= 1001 && strMessage.Length < 2000 ? strMessage.ToString().Substring(1000, strMessage.Length - 1000) : strMessage.ToString().Substring(1000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG3", OracleDbType.Char, strMessage.Length <= 2000 ? "" : strMessage.Length >= 2001 && strMessage.Length < 3000 ? strMessage.ToString().Substring(2000, strMessage.Length - 2000) : strMessage.ToString().Substring(2000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG4", OracleDbType.Char, strMessage.Length <= 3000 ? "" : strMessage.Length >= 3001 && strMessage.Length < 4000 ? strMessage.ToString().Substring(3000, strMessage.Length - 3000) : strMessage.ToString().Substring(3000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG5", OracleDbType.Char, strMessage.Length <= 4000 ? "" : strMessage.Length >= 4001 && strMessage.Length < 5000 ? strMessage.ToString().Substring(4000, strMessage.Length - 4000) : strMessage.ToString().Substring(4000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG6", OracleDbType.Char, strMessage.Length <= 5000 ? "" : strMessage.Length >= 5001 && strMessage.Length < 6000 ? strMessage.ToString().Substring(5000, strMessage.Length - 5000) : strMessage.ToString().Substring(5000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG7", OracleDbType.Char, strMessage.Length <= 6000 ? "" : strMessage.Length >= 6001 && strMessage.Length < 7000 ? strMessage.ToString().Substring(6000, strMessage.Length - 6000) : strMessage.ToString().Substring(6000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("MSG8", OracleDbType.Char, strMessage.Length <= 6000 ? "" : strMessage.Length >= 6001 && strMessage.Length < 7000 ? strMessage.ToString().Substring(6000, strMessage.Length - 6000) : strMessage.ToString().Substring(6000, 1000), ParameterDirection.Input);
                                            command.Parameters.Add("REMARK", OracleDbType.Varchar2, "", ParameterDirection.Input);
                                            command.Parameters.Add("INCOMINGDATE", OracleDbType.Date, new FileInfo(sourceFileName).LastWriteTime , ParameterDirection.Input);
                                            int row = command.ExecuteNonQuery();

                                            if (msgCode == "300")
                                            {
                                                Dictionary<string, string[]> msglocal = SpliteSWIFTMsg(strMessage.ToString());
                                                string CtpyBic = msglocal["4"][Array.IndexOf(msglocal["4"], "82A") + 1].Replace("\r\n", "");
                                                CtpyBic = CtpyBic.Substring(0, CtpyBic.Length - 3);
                                                string TradeDate = msglocal["4"][Array.IndexOf(msglocal["4"], "30T") + 1].Replace("\r\n", "");
                                                string ValueDate = msglocal["4"][Array.IndexOf(msglocal["4"], "30V") + 1].Replace("\r\n", "");
                                                decimal ExchRate = decimal.Parse(msglocal["4"][Array.IndexOf(msglocal["4"], "36") + 1].Replace("\r\n", "").Replace(",", "."));
                                                string BuyCCY = msglocal["4"][Array.IndexOf(msglocal["4"], "32B") + 1].Replace("\r\n", "").Substring(0, 3);
                                                decimal BuyAmt = decimal.Parse(msglocal["4"][Array.IndexOf(msglocal["4"], "32B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                                                string SellCCY = msglocal["4"][Array.IndexOf(msglocal["4"], "33B") + 1].Replace("\r\n", "").Substring(0, 3);
                                                decimal SellAmt = decimal.Parse(msglocal["4"][Array.IndexOf(msglocal["4"], "33B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                                                string SwiftNo = msglocal["4"][Array.IndexOf(msglocal["4"], "20") + 1].Replace("\r\n", "");
                                                string MsgType = msglocal["4"][Array.IndexOf(msglocal["4"], "22A") + 1].Replace("\r\n", "");
                                                string RefNo = msglocal["4"][Array.IndexOf(msglocal["4"], "21") + 1].Replace("\r\n", "");
                                                string FromBIC = msglocal["4"][Array.IndexOf(msglocal["4"], "82A") + 1].Replace("\r\n", ""); 

                                                string sqlAddSwiftDetail = "INSERT INTO OPICINF.KKB_TB_SWIFTMSG_DETAIL(FILENAME, SWIFTNO, MSGTYPE, REFNO, MSGCODE, DEALDATE, VDATE, CCYRATE_8, CCY, CCYAMT, CTRCCY, CTRAMT, SWIFTFILEID, FROM_BIC) SELECT '" + sourceFileName + "','" + SwiftNo + "','" + MsgType + "','" + RefNo + "','" + msgCode + "',TO_DATE('" + TradeDate + "','YYYYMMDD'),TO_DATE('" + ValueDate + "','YYYYMMDD'),'" + ExchRate + "','" + SellCCY + "','" + SellAmt + "','" + BuyCCY + "','" + BuyAmt + "', OPICINF.KKB_TB_SWIFTMSG_FILE_SEQ.currval, '" + FromBIC + "' from dual";
                                                command.CommandText = sqlAddSwiftDetail;
                                                command.CommandType = CommandType.Text;
                                                command.ExecuteNonQuery();
                                            }
                                            else if (msgCode == "202")
                                            {
                                                Dictionary<string, string[]> msglocal = SpliteSWIFTMsg(strMessage.ToString());
                                                string RefCode = msglocal["4"][Array.IndexOf(msglocal["4"], "20") + 1].Replace("\r\n", "");
                                                string RelateRefCode = msglocal["4"][Array.IndexOf(msglocal["4"], "21") + 1].Replace("\r\n", "");
                                                string ValueDate = msglocal["4"][Array.IndexOf(msglocal["4"], "32A") + 1].Replace("\r\n", "").Substring(0, 6);
                                                string CCY = msglocal["4"][Array.IndexOf(msglocal["4"], "32A") + 1].Replace("\r\n", "").Substring(6, 3);
                                                decimal Amount = decimal.Parse(msglocal["4"][Array.IndexOf(msglocal["4"], "32A") + 1].Replace("\r\n", "").Replace(",", ".").Substring(9) + "0");

                                                string sqlAddSwiftDetail = "INSERT INTO OPICINF.KKB_TB_SWIFTMSG_202(FILENAME, REFCODE, RELATEREFCODE, VDATE, CCY, CCYAMT, SWIFTFILEID) SELECT '" + sourceFileName + "','" + RefCode + "','" + RelateRefCode + "' ,TO_DATE('" + ValueDate + "','YYMMDD'),'" + CCY + "','" + Amount + "', OPICINF.KKB_TB_SWIFTMSG_FILE_SEQ.currval from dual";
                                                command.CommandText = sqlAddSwiftDetail;
                                                command.CommandType = CommandType.Text;
                                                command.ExecuteNonQuery();


                                                if (folder == "ACK")
                                                {
                                                    //File.Copy(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_ACK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".ack");
                                                    CreatePDF(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_ACK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".pdf");
                                                }
                                                else if (folder == "NAK")
                                                {
                                                    //File.Copy(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_NAK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".nak");
                                                    CreatePDF(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_NAK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".pdf");
                                                }



                                            }
                                            else if (msgCode == "103")
                                            {
                                                Dictionary<string, string[]> msglocal = SpliteSWIFTMsg(strMessage.ToString());
                                                string RefCode = msglocal["4"][Array.IndexOf(msglocal["4"], "20") + 1].Replace("\r\n", ""); 
                                                string ValueDate = msglocal["4"][Array.IndexOf(msglocal["4"], "32A") + 1].Replace("\r\n", "").Substring(0, 6);
                                                string CCY = msglocal["4"][Array.IndexOf(msglocal["4"], "32A") + 1].Replace("\r\n", "").Substring(6, 3);
                                                decimal Amount = decimal.Parse(msglocal["4"][Array.IndexOf(msglocal["4"], "32A") + 1].Replace("\r\n", "").Replace(",", ".").Substring(9) + "0");

                                                string sqlAddSwiftDetail = "INSERT INTO OPICINF.KKB_TB_SWIFTMSG_103(FILENAME, REFCODE, VDATE, CCY, CCYAMT, SWIFTFILEID) SELECT '" + sourceFileName + "','" + RefCode + "' ,TO_DATE('" + ValueDate + "','YYMMDD'),'" + CCY + "','" + Amount + "', OPICINF.KKB_TB_SWIFTMSG_FILE_SEQ.currval from dual";
                                                command.CommandText = sqlAddSwiftDetail;
                                                command.CommandType = CommandType.Text;
                                                command.ExecuteNonQuery();

                                                if (folder == "ACK")
                                                {
                                                    //File.Copy(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_ACK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".ack");
                                                    CreatePDF(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_ACK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".pdf");
                                                }
                                                else if (folder == "NAK")
                                                {
                                                    //File.Copy(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_NAK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".nak");
                                                    CreatePDF(sourceFileName, ConfigurationManager.AppSettings["SWIFTFILE_NAK_OUTPUTFOLDER"].ToString() + "\\" + RefCode + ".pdf");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            } 
        }

        public static Dictionary<string, string[]> SpliteSWIFTMsg(string swiftMessage)
        {
            try
            {
                Dictionary<string, string[]> dictmsg = new Dictionary<string, string[]>();
                String[] series = swiftMessage.Replace("{", "").Split('}');  //Delete { and split with } 
                foreach (var msgline in series)
                {
                    if ((!msgline.Trim().Equals("")) && (msgline.IndexOf(':') > 0))
                    {
                        String[] msg = msgline.Split(':'); //Split each row 
                        try
                        {
                            //Log.Debug("Message Length : " + msg.Length);
                            dictmsg.Add(msg[0], msg);
                        }
                        catch (Exception ex)
                        {
                            if (!dictmsg.ContainsKey(msg[0]))
                                dictmsg.Add(msg[0], null);
                        }
                    }
                }
                return dictmsg;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
        //not use
        public static void swift300Matching(string ConnectionString, string CTPY, string FromDate)
        {
            Log.Info("-----Model_RPT_TR152OBOSWIFT-SWIFT 300 Matching Start-----");
 
            try
            {
                using (OracleConnection connect = new OracleConnection(ConnectionString))
                {
                    if (connect.State == ConnectionState.Open)
                        connect.Close();
                    connect.Open();

                    string sqlOpics = "SELECT TRIM(FXDH.DEALNO) as DEALNO, TRIM(CUST.BIC) as BIC, TO_CHAR(FXDH.DEALDATE,'YYYYMMDD') as DEALDATE, TO_CHAR(FXDH.VDATE,'YYYYMMDD') as VDATE, ABS(FXDH.CCYRATE_8) as CCYRATE_8, TRIM(FXDH.CCY) as CCY, ABS(FXDH.CCYAMT) as CCYAMT, TRIM(FXDH.CTRCCY) as CTRCCY, ABS(FXDH.CTRAMT) as CTRAMT  FROM OPICS.FXDH LEFT OUTER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO WHERE REVDATE IS NULL AND CTYPE='B'  ";
                    if (!CTPY.Trim().Equals(""))
                        sqlOpics += "AND CUST.CMNE='" + CTPY + "' ";
                    //if (!FromDate.Trim().Equals(""))
                    //    sqlOpics += "AND DEALDATE=TO_DATE('" + FromDate + "','DD/MM/YYYY') ";
                    //else
                    //    sqlOpics += "AND DEALDATE=TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','DD/MM/YYYY') ";
                    sqlOpics += "AND TRIM(FXDH.DEALNO) NOT IN (SELECT NVL(TRIM(DEALNO),0) FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE MSGCODE='300') ";
                    DataTable OpicsData = new DataTable("OpicsFXDH");
                    using (OracleCommand command = new OracleCommand(sqlOpics, connect))
                    {
                        OracleDataAdapter da = new OracleDataAdapter(command);
                        da.Fill(OpicsData);
                        da.Dispose();
                    }

                    string sqlSWIFT = "SELECT * FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE MSGCODE='300' ";
                    //if (!FromDate.Trim().Equals(""))
                    //    sqlSWIFT += "AND TO_CHAR(LOG_INSERTDATE,'DD/MM/YYYY')='" + FromDate + "' ";
                    //else
                    //    sqlSWIFT += "AND TO_CHAR(LOG_INSERTDATE,'DD/MM/YYYY')='" + DateTime.Now.ToString("dd/MM/yyyy") + "' ";
                    DataTable SwiftData = new DataTable("SwiftMsg");
                    using (OracleCommand command = new OracleCommand(sqlSWIFT, connect))
                    {
                        OracleDataAdapter da = new OracleDataAdapter(command);
                        da.Fill(SwiftData);
                        da.Dispose();
                    }  
                    List<string> matching = new List<string>();
                    if (SwiftData.Rows.Count > 0)
                    {
                        for (int i = 0; i < SwiftData.Rows.Count; i++)
                        {
                            try
                            {
                                String str = SwiftData.Rows[i]["MSG1"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG2"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG3"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG4"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG5"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG6"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG7"].ToString().Trim()
                                    + SwiftData.Rows[i]["MSG8"].ToString().Trim();
                                Dictionary<string, string[]> msg = SpliteSWIFTMsg(str.Trim());
                                string CtpyBic = msg["4"][Array.IndexOf(msg["4"], "82A") + 1].Replace("\r\n", "");
                                CtpyBic = CtpyBic.Substring(0, CtpyBic.Length - 3);
                                string TradeDate = msg["4"][Array.IndexOf(msg["4"], "30T") + 1].Replace("\r\n", "");
                                string ValueDate = msg["4"][Array.IndexOf(msg["4"], "30V") + 1].Replace("\r\n", "");
                                decimal ExchRate = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "36") + 1].Replace("\r\n", "").Replace(",", "."));
                                string BuyCCY = msg["4"][Array.IndexOf(msg["4"], "32B") + 1].Replace("\r\n", "").Substring(0, 3);
                                decimal BuyAmt = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "32B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                                string SellCCY = msg["4"][Array.IndexOf(msg["4"], "33B") + 1].Replace("\r\n", "").Substring(0, 3);
                                decimal SellAmt = decimal.Parse(msg["4"][Array.IndexOf(msg["4"], "33B") + 1].Replace("\r\n", "").Replace(",", ".").Substring(3) + "0");
                                                                
                                //Log.Debug("TradeDate:" + TradeDate + ", ValueDate:" + ValueDate + ", ExchRate:" + ExchRate + ", BuyCCY:" + BuyCCY + ", BuyAmt:" + BuyAmt + ", SellCCY:" + SellCCY + ", SellAmt:" + SellAmt);
                                List<DataRow> d = OpicsData.Select().Where(t =>
                                        t["BIC"].Equals(CtpyBic) == true
                                        && t["DEALDATE"].Equals(TradeDate) == true
                                        && t["VDATE"].Equals(ValueDate) == true
                                        && t["CCYRATE_8"].Equals(ExchRate) == true
                                        && t["CCY"].ToString().Trim().Equals(SellCCY) == true
                                        && t["CCYAMT"].Equals(SellAmt) == true
                                        && t["CTRCCY"].ToString().Trim().Equals(BuyCCY) == true
                                        && t["CTRAMT"].Equals(BuyAmt) == true
                                    ).ToList<DataRow>();
                                if (d.Count() == 1)
                                {
                                    string sqlUpdateDealNoInSwift = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO='" + d[0]["DEALNO"].ToString() + "' WHERE SWIFTFILEID='" + SwiftData.Rows[i]["SWIFTFILEID"].ToString() + "' AND DEALNO IS NULL ";
                                    using (OracleCommand command = new OracleCommand(sqlUpdateDealNoInSwift, connect))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }
                                else if (d.Count() > 1)
                                {
                                    Log.Debug("Deal Matching more than 1");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex.Message);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("-----Model -SWIFT 300 Matching Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR152OBOSWIFT-SWIFT 300 Matching Finish-----"); 
            }
        }

        public static void opicsSwift300Matching(string ConnectionString, string FromDate)
        {
            Log.Info("-----Model_RPT_TR152OBOSWIFT-SWIFT 300 Matching Start-----");

            try
            {
                using (OracleConnection connect = new OracleConnection(ConnectionString))
                {
                    if (connect.State == ConnectionState.Open)
                        connect.Close();
                    connect.Open();

                    //ดึงเฉพาะรายการที่ยังไม่ Match
                    string sqlOpics = "Select * From (";
                    sqlOpics += "SELECT FXDH.DEALNO, FXDH.DEALDATE, DETAIL.SWIFTFILEID, DETAIL.MSGTYPE, DETAIL.SWIFTNO, DETAIL.REFNO, NVL((SELECT SWIFTFILEID FROM OPICINF.KKB_TB_SWIFTMSG_DETAIL WHERE REFNO=DETAIL.SWIFTNO),'') as OTHERREF FROM OPICS.FXDH INNER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO INNER JOIN OPICINF.KKB_TB_SWIFTMSG_DETAIL DETAIL ON TO_NUMBER(FXDH.CCYRATE_8)=TO_NUMBER(DETAIL.CCYRATE_8) AND TRIM(FXDH.CCY)=TRIM(DETAIL.CTRCCY) AND ABS(TO_NUMBER(FXDH.CCYAMT))=ABS(TO_NUMBER(DETAIL.CTRAMT)) AND TRIM(FXDH.CTRCCY)=TRIM(DETAIL.CCY) AND ABS(TO_NUMBER(FXDH.CTRAMT))=ABS(TO_NUMBER(DETAIL.CCYAMT)) AND FXDH.DEALDATE=DETAIL.DEALDATE AND FXDH.VDATE=DETAIL.VDATE WHERE FXDH.REVDATE IS NULL AND SUBSTR(DETAIL.FROM_BIC,0,8)=SUBSTR(TRIM(CUST.BIC),0,CASE WHEN LENGTH(TRIM(CUST.BIC))>8 THEN 8 ELSE LENGTH(TRIM(CUST.BIC)) END) ";
                    //if (!FromDate.Trim().Equals(""))
                    sqlOpics += "AND FXDH.DEALDATE=TO_DATE('" + FromDate + "','DD/MM/YYYY') AND CUST.CTYPE='B' ";
                    sqlOpics += "AND EXISTS (SELECT SWIFTFILEID FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE KKB_TB_SWIFTMSG_FILE.SWIFTFILEID=DETAIL.SWIFTFILEID AND TO_CHAR(KKB_TB_SWIFTMSG_FILE.INCOMINGDATE,'DD/MM/YYYY')='" + FromDate + "') ";
                    sqlOpics += "UNION ALL ";
                    sqlOpics += "SELECT FXDH.DEALNO, FXDH.DEALDATE, DETAIL.SWIFTFILEID, DETAIL.MSGTYPE, DETAIL.SWIFTNO, DETAIL.REFNO , NVL((SELECT SWIFTFILEID FROM OPICINF.KKB_TB_SWIFTMSG_DETAIL WHERE REFNO=DETAIL.SWIFTNO),'') as OTHERREF FROM OPICS.FXDH INNER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO INNER JOIN OPICINF.KKB_TB_SWIFTMSG_DETAIL DETAIL ON TO_NUMBER(FXDH.CCYRATE_8)=TO_NUMBER(DETAIL.CCYRATE_8) AND TRIM(FXDH.CCY)=TRIM(DETAIL.CCY) AND ABS(TO_NUMBER(FXDH.CCYAMT))=ABS(TO_NUMBER(DETAIL.CCYAMT)) AND TRIM(FXDH.CTRCCY)=TRIM(DETAIL.CTRCCY) AND ABS(TO_NUMBER(FXDH.CTRAMT))=ABS(TO_NUMBER(DETAIL.CTRAMT)) AND FXDH.DEALDATE=DETAIL.DEALDATE AND FXDH.VDATE=DETAIL.VDATE WHERE FXDH.REVDATE IS NULL AND SUBSTR(DETAIL.FROM_BIC,0,8)=SUBSTR(TRIM(CUST.BIC),0,CASE WHEN LENGTH(TRIM(CUST.BIC))>8 THEN 8 ELSE LENGTH(TRIM(CUST.BIC)) END)  ";
                    //if (!FromDate.Trim().Equals(""))
                    sqlOpics += "AND FXDH.DEALDATE=TO_DATE('" + FromDate + "','DD/MM/YYYY') AND CUST.CTYPE='B' ";
                    sqlOpics += "AND EXISTS (SELECT SWIFTFILEID FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE KKB_TB_SWIFTMSG_FILE.SWIFTFILEID=DETAIL.SWIFTFILEID AND TO_CHAR(KKB_TB_SWIFTMSG_FILE.INCOMINGDATE,'DD/MM/YYYY')='" + FromDate + "') ";
                    sqlOpics += ") WHERE DEALDATE=TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY MSGTYPE DESC";
                    DataTable OpicsMatching = new DataTable("OpicsFXDH");
                    using (OracleCommand command = new OracleCommand(sqlOpics, connect))
                    {
                        OracleDataAdapter da = new OracleDataAdapter(command);
                        da.Fill(OpicsMatching);
                        da.Dispose();
                    }

                    string sqlDealNull = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO=NULL WHERE TO_CHAR(INCOMINGDATE,'DD/MM/YYYY')='" + FromDate + "' ";
                    int trows = (new OracleCommand(sqlDealNull, connect)).ExecuteNonQuery();
 
                    for (int i = 0; i < OpicsMatching.Rows.Count; i++)
                    {
                        string sql = String.Empty;

                        //if (OpicsMatching.Rows[i]["MSGTYPE"].Equals("NEWT"))
                        //{
                        //    //Update Transaction that reference
                        //    sql = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO='" + OpicsMatching.Rows[i]["DEALNO"].ToString() + "' WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "' ";
                        //    (new OracleCommand(sql, connect)).ExecuteNonQuery();
                        //}
                        //else if (OpicsMatching.Rows[i]["MSGTYPE"].Equals("AMND"))
                        //{
                            sql = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO='" + OpicsMatching.Rows[i]["DEALNO"].ToString() + "' WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "' ";                            
                            int rows = (new OracleCommand(sql, connect)).ExecuteNonQuery();

                            string sqlClear = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO=NULL WHERE SWIFTFILEID!=(SELECT SWIFTFILEID FROM ( ";
                            sqlClear += "   SELECT DETAIL.SWIFTNO, REFNO, LEVEL, DETAIL.SWIFTFILEID ";
                            sqlClear += "   FROM OPICINF.KKB_TB_SWIFTMSG_DETAIL DETAIL INNER JOIN OPICINF.KKB_TB_SWIFTMSG_FILE SWIFTFILE ON DETAIL.SWIFTFILEID=SWIFTFILE.SWIFTFILEID ";
                            sqlClear += "    WHERE TO_CHAR(SWIFTFILE.INCOMINGDATE,'DD/MM/YYYY')='" + FromDate + "'  AND DEALNO=(SELECT DEALNO FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "') ";
                            sqlClear += "    START WITH DETAIL.MSGTYPE='NEWT' ";
                            sqlClear += "    CONNECT BY NOCYCLE PRIOR DETAIL.SWIFTNO=DETAIL.REFNO ";
                            sqlClear += "    ORDER BY LEVEL DESC ";
                            sqlClear += ") WHERE ROWNUM=1) AND DEALNO=(SELECT DEALNO FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "')";
                            int rws = (new OracleCommand(sqlClear, connect)).ExecuteNonQuery();
                        //}
 
                    }


                    //sqlOpics = "SELECT FXDH.DEALNO, DETAIL.SWIFTFILEID, DETAIL.MSGTYPE, DETAIL.SWIFTNO, DETAIL.REFNO , NVL((SELECT SWIFTFILEID FROM OPICINF.KKB_TB_SWIFTMSG_DETAIL WHERE REFNO=DETAIL.SWIFTNO),'') as OTHERREF FROM OPICS.FXDH INNER JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO INNER JOIN OPICINF.KKB_TB_SWIFTMSG_DETAIL DETAIL ON TO_NUMBER(FXDH.CCYRATE_8)=TO_NUMBER(DETAIL.CCYRATE_8) AND TRIM(FXDH.CCY)=TRIM(DETAIL.CCY) AND ABS(TO_NUMBER(FXDH.CCYAMT))=ABS(TO_NUMBER(DETAIL.CCYAMT)) AND TRIM(FXDH.CTRCCY)=TRIM(DETAIL.CTRCCY) AND ABS(TO_NUMBER(FXDH.CTRAMT))=ABS(TO_NUMBER(DETAIL.CTRAMT)) AND FXDH.DEALDATE=DETAIL.DEALDATE AND FXDH.VDATE=DETAIL.VDATE WHERE FXDH.REVDATE IS NULL AND SUBSTR(DETAIL.FROM_BIC,0,8)=SUBSTR(TRIM(CUST.BIC),0,CASE WHEN LENGTH(TRIM(CUST.BIC))>8 THEN 8 ELSE LENGTH(TRIM(CUST.BIC)) END)  ";
                    //if (!CTPY.Trim().Equals(""))
                    //    sqlOpics += "AND CUST.CMNE='" + CTPY + "' ";
                    ////if (!FromDate.Trim().Equals(""))
                    ////    sqlOpics += "AND DEALDATE=TO_DATE('" + FromDate + "','DD/MM/YYYY') ";
                    ////else
                    ////    sqlOpics += "AND DEALDATE=TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','DD/MM/YYYY') ";
                    ////sqlOpics += "AND DETAIL.MSGTYPE='NEWT' ";
                    //sqlOpics += "AND NOT EXISTS (SELECT DEALNO FROM OPICINF.KKB_TB_SWIFTMSG_FILE WHERE KKB_TB_SWIFTMSG_FILE.DEALNO=FXDH.DEALNO) ORDER BY DETAIL.MSGTYPE DESC ";
                    //OpicsMatching = new DataTable("OpicsFXDH");
                    //using (OracleCommand command = new OracleCommand(sqlOpics, connect))
                    //{
                    //    OracleDataAdapter da = new OracleDataAdapter(command);
                    //    da.Fill(OpicsMatching);
                    //    da.Dispose();
                    //}

                    //for (int i = 0; i < OpicsMatching.Rows.Count; i++)
                    //{
                    //    string sql = string.Empty;
                    //    if (OpicsMatching.Rows[i]["MSGTYPE"].Equals("NEWT"))
                    //    {                            
                    //        //Update Transaction that reference
                    //        sql = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO='" + OpicsMatching.Rows[i]["DEALNO"].ToString() + "' WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "' ";                            
                    //    }
                    //    else if (OpicsMatching.Rows[i]["MSGTYPE"].Equals("AMND"))
                    //    {
                    //        ////Update leaf not self
                    //        //string sqlUpdateDealNoIfIsLast = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO=NULL WHERE SWIFTFILEID<>'" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "' AND EXISTS(SELECT SWIFTFILEID FROM OPICINF.KKB_TB_SWIFTMSG_DETAIL START WITH SWIFTNO = '" + OpicsMatching.Rows[i]["SWIFTNO"].ToString() + "' CONNECT BY PRIOR REFNO = SWIFTNO) ";
                    //        //using (OracleCommand command = new OracleCommand(sqlUpdateDealNoIfIsLast, connect))
                    //        //{
                    //        //    command.ExecuteNonQuery();
                    //        //}

                    //        if (OpicsMatching.Rows[i]["OTHERREF"].ToString().Trim().Equals(""))
                    //        {
                    //            sql = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO='" + OpicsMatching.Rows[i]["DEALNO"].ToString() + "' WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "' ";                                
                    //        }
                    //        else
                    //        {
                    //            sql = "UPDATE OPICINF.KKB_TB_SWIFTMSG_FILE SET DEALNO=NULL WHERE SWIFTFILEID='" + OpicsMatching.Rows[i]["SWIFTFILEID"].ToString() + "' ";
                    //        }
                    //        using (OracleCommand command = new OracleCommand(sql, connect))
                    //        {
                    //            command.ExecuteNonQuery();
                    //        }
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                Log.Error("-----Model -SWIFT 300 Matching Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR152OBOSWIFT-SWIFT 300 Matching Finish-----");
            }
        }

        public static void CreatePDF(string sourceFile, string outputFile)
        {
            try
            {
                StreamReader sr = new StreamReader(sourceFile);
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ConfigurationManager.AppSettings["SWIFTFILE_ACKNAK_LOGO"].ToString());
                jpg.ScaleToFit(121f, 33f);
                jpg.SpacingBefore = 10f;
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_LEFT;
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(outputFile, FileMode.Create));
                doc.Open();
                doc.Add(jpg);
                doc.Add(new Paragraph(sr.ReadToEnd())); 
                doc.Close(); 
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}

