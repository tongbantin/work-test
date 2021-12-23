using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using TreasuryModel.SanctionService;
using TreasuryModel.LookupService;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace TreasuryModel.Data
{
    public class PartyMsg
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(PartyMsg));

        public static int TRANSACTION_COUNTER { get; set; }

        public int PARTYMSGID { get; set; }
        public string REFNO { get; set; }
        public string MSG { get; set; }
        public string FILENAME { get; set; }
        public string STATUS { get; set; }
        public string DECISIONBY { get; set; }
        public string APPROVEBY { get; set; }
                
        //Check Sanction Property
        public string CONNECTIONSTRING { get; set; }
        public string CHECKINGID { get; set; }
        public string SANCTIONHOST { get; set; }
        public string NAME { get; set; }
        public string COUNTRY { get; set; }
        public string TRANSACTIONDATE { get; set; }
        public string MINTHRESHOLD { get; set; }
        public string WATCHLISTSOURCE { get; set; }
        public Dictionary<string, PARTYMsgChk> RESULT { get; set; }


        //Money Transfer Msg Detail
        //RIA Correspondent ID	
        public string CORRESPONDENT_ID { get; set; }
        //Paying Agent Sequence ID	
        public string PAYING_SEQUENCE_ID { get; set; }
        //RIA Order ID	
        public string ORDER_ID { get; set; }
        //Order Number	
        public string ORDER_NO { get; set; }
        //Order Date	
        public string ORDER_DATE { get; set; }
        //Currency to Pay	
        public string CCY_PAY { get; set; }
        //Amount to Pay	
        public string AMT_PAY { get; set; }
        //Clave	
        public string CLAVE { get; set; }
        //Correspondent Branch	
        public string CORRESPONDENT_BRANCH { get; set; }
        //Correspondent Branch Number	
        public string CORRESPONDENT_BRANCH_NO { get; set; }
        //Customer First Name	
        public string CUST_FIRSTNAME { get; set; }
        //Customer Last Name 1 (Patemo)	
        public string CUST_LASTNAME1 { get; set; }
        //Customer Last Name 2 (Matemo)	
        public string CUST_LASTNAME2 { get; set; }
        //Customer Tel No	
        public string CUST_TEL_NO { get; set; }
        //Customer Address	
        public string CUST_ADDRESS { get; set; }
        //Customer City	
        public string CUST_CITY { get; set; }
        //Customer State	
        public string CUST_STATE { get; set; }
        //Customer Zip Code	
        public string CUST_ZIPCODE { get; set; }
        //Customer Country	
        public string CUST_COUNTRY { get; set; }
        //Beneficiary First Name	
        public string BENEF_FIRSTNAME { get; set; }
        //Beneficiary Last Name 1 (Patemo)	
        public string BENEF_LASTNAME1 { get; set; }
        //Beneficiary Last Name 2 (Matemo)	
        public string BENEF_LASTNAME2 { get; set; }
        //Beneficiary Tel No	
        public string BENEF_TELNO { get; set; }
        //Beneficiary Address	
        public string BENEF_ADDRESS { get; set; }
        //Beneficiary City	
        public string BENEF_CITY { get; set; }
        //Beneficiary State	
        public string BENEF_STATE { get; set; }
        //Beneficiary Zip Code	
        public string BENEF_ZIPCODE { get; set; }
        //Beneficiary Country	
        public string BENEF_COUNTRY { get; set; }
        //Message for Beneficiary	
        public string MESSAGE_FOR_BENEF { get; set; }
        //Delivery Method	
        public string DELIVERY_METHOD { get; set; }
        //Bank Account Number			
        public string BANK_ACCOUNT_NO { get; set; }
        
        public static Boolean SpliteMessage(string ConnectionString, string FileName, string [] msg, string NotExists)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {            
                //Review Only Not Exists (Default)
                string sql = "INSERT INTO OPICINF.KKB_TB_PARTYMSG(REFNO, MSG, FILENAME, STATUS, LOG_INSERTBYUSERID, LOG_INSERTDATE) ";
                sql += "SELECT :REFNO, :MSG, :FILENAME, '" + MessageStatus.WAITING_TO_SEND + "', 'ADMIN', SYSDATE FROM DUAL ";
                sql += "WHERE NOT EXISTS (SELECT 1 FROM OPICINF.KKB_TB_PARTYMSG WHERE REFNO=:KEY)";

                o.openConnection();
                o.beginTransaction();

                o.createTransaction("DELETE FROM OPICINF.KKB_TB_PARTYMSG WHERE STATUS='" + MessageStatus.WAITING_TO_SEND + "'", CommandType.Text);
                o.executeNonQuery();

                if (!NotExists.Equals("NOT EXISTS"))
                {
                    o.createTransaction("DELETE FROM OPICINF.KKB_TB_PARTYMSG WHERE FILENAME LIKE '" + FileName + "'", CommandType.Text);
                    o.executeNonQuery();
                }


                for (int i = 0; i < msg.Length; i++)
                {
                    o.createTransaction(sql, CommandType.Text);
                    o.setCharInputParameter("REFNO", msg[i].Split('|')[3]);
                    o.setCharInputParameter("MSG", msg[i]);
                    o.setCharInputParameter("FILENAME", FileName);
                    o.setCharInputParameter("KEY", msg[i].Split('|')[3]);
                    int row = o.executeNonQuery();
                }
                //Return result to jTable
                o.commitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                o.rollbackTransaction();
                return false;
            }
            finally {
                o.closeConnection();
            }  
        }

        public static DataTable PartyList(string ConnectionString, string FileName)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_PARTYMSG WHERE FILENAME='" + FileName + "'";
                
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable ExtractMessage(string[] msg)
        {
            try
            {
                DataTable dt = new DataTable();
                //Create Column
                string[] colName = new string[31] { "CORRESPONDENT_ID"
                    , "PAYING_AGENT_SEQUENCE_ID"
                    , "RIA_ORDER_ID"
                    , "ORDER_NUMBER"
                    , "ORDER_DATE"
                    , "CURRENCY_TO_PAY"
                    , "AMOUNT_TO_PAY"
                    , "CLAVE"
                    , "CORRESPONDENT_BRANCH"
                    , "CORRESPONDENT_BRANCH_NUMBER"
                    , "CUSTOMER_FIRST_NAME"
                    , "CUSTOMER_LAST_NAME_1"
                    , "CUSTOMER_LAST_NAME_2"
                    , "CUSTOMER_TEL_NO"
                    , "CUSTOMER_ADDRESS"
                    , "CUSTOMER_CITY"
                    , "CUSTOMER_STATE"
                    , "CUSTOMER_ZIP_CODE"
                    , "CUSTOMER_COUNTRY"
                    , "BENEFICIARY_FIRST_NAME"
                    , "BENEFICIARY_LAST_NAME_1"
                    , "BENEFICIARY_LAST_NAME_2"
                    , "BENEFICIARY_TEL_NO"
                    , "BENEFICIARY_ADDRESS"
                    , "BENEFICIARY_CITY"
                    , "BENEFICIARY_STATE"
                    , "BENEFICIARY_ZIP_CODE"
                    , "BENEFICIARY_COUNTRY"
                    , "MESSAGE_FOR_BENEFICIARY"
                    , "DELIVERY_METHOD"
                    , "BANK_ACCOUNT_NUMBER"
                };

                dt.Columns.Add("Records");
                for (int c = 0; c < colName.Length; c++)
                    dt.Columns.Add(colName[c]);

                for (int i = 0; i < msg.Length; i++)
                {
                    string[] fieldval = msg[i].Split('|');
                    DataRow dr = dt.NewRow();
                    dr["Records"] = i+1;
                    for(int j = 0; j< colName.Length; j++)
                        dr[dt.Columns[j+1].ColumnName] = fieldval[j];
                    dt.Rows.Add(dr);
                }

                //Return result to jTable
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable ScanMessageList(string ConnectionString, string SanctionHost, string FileName, string CheckColumn, string MinThreshold, string Concurrent)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                int _concurrent = int.Parse(Concurrent);
                string sql = "SELECT PARTYMSGID, REFNO, MSG, TO_CHAR(LOG_INSERTDATE,'YYYYMMDDHHMISS') LOG_INSERTDATE FROM OPICINF.KKB_TB_PARTYMSG WHERE FILENAME='" + FileName + "' AND STATUS='WAITING_TO_SEND'";

                o.openConnection();

                o.createCommand("UPDATE OPICINF.KKB_TB_PARTYMSG SET STATUS='CANCELED' WHERE STATUS='WAITING_TO_SEND' AND LOG_INSERTDATE<(SYSDATE-1) ", CommandType.Text);
                o.executeNonQuery();

                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");

                string[] rowloop = CheckColumn.Split(';');
                TRANSACTION_COUNTER = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < rowloop.Length; j++)
                    {
                        string[] cols = rowloop[j].Split(',');
                        string[] fieldval = dt.Rows[i]["MSG"].ToString().Split('|');
                        string Name = fieldval[int.Parse(cols[0])].Trim() + " " + fieldval[int.Parse(cols[1])].Trim();
                        if (!Name.Trim().Equals(""))
                        {
                            o.createTransaction("INSERT INTO OPICINF.KKB_TB_PARTYMSG_CHK(PARTYMSGID, COND_NAME, COND_COUNTRY, LOG_INSERTBYUSERID, LOG_INSERTDATE) VALUES(:PARTYMSGID, :COND_NAME, :COND_COUNTRY, 'SYSTEM', SYSDATE)", CommandType.Text);
                            o.setVarCharInputParameter("PARTYMSGID", dt.Rows[i]["PartyMsgID"].ToString());
                            o.setVarCharInputParameter("COND_NAME", Name);
                            o.setVarCharInputParameter("COND_COUNTRY", fieldval[int.Parse(cols[2])].Trim());
                            o.executeNonQuery();

                            //if (Name.Trim().LastIndexOf(' ') >= 0)
                            //{
                            //    o.createTransaction("INSERT INTO OPICINF.KKB_TB_PARTYMSG_CHK(PARTYMSGID, COND_NAME, COND_COUNTRY, LOG_INSERTBYUSERID, LOG_INSERTDATE) VALUES(:PARTYMSGID, :COND_NAME, :COND_COUNTRY, 'SYSTEM', SYSDATE)", CommandType.Text);
                            //    o.setVarCharInputParameter("PARTYMSGID", dt.Rows[i]["PartyMsgID"].ToString());
                            //    o.setVarCharInputParameter("COND_NAME", Name.Trim().Substring(Name.Trim().LastIndexOf(' ')).Trim() + "," + Name.Trim().Substring(0, Name.Trim().LastIndexOf(' ')).Trim());
                            //    o.setVarCharInputParameter("COND_COUNTRY", fieldval[int.Parse(cols[2])].Trim());
                            //    o.executeNonQuery();
                            //}
                        }
                    }
                    ////Waiting To Next Round
                    if ((i % 40) == 0)
                        Thread.Sleep(2000);
                }

                //Get Condition Message For Check Sanction
                o.createCommand("SELECT CHECKINGID, PARTYMSGID, COND_NAME, COND_COUNTRY, LOG_INSERTDATE FROM OPICINF.KKB_TB_PARTYMSG_CHK WHERE PARTYMSGID IN (SELECT PARTYMSGID FROM OPICINF.KKB_TB_PARTYMSG WHERE STATUS='WAITING_TO_SEND') ", CommandType.Text);
                DataTable dtMessage = o.executeReaderToDT("PARTYMSGCHK");
                o.closeConnection();

                Dictionary<string, PARTYMsgChk> ResResult = new Dictionary<string, PARTYMsgChk>(); 
                //Asynchronus Message Check
                for (int i = 0; i < dtMessage.Rows.Count; i++)
                {
                    try
                    {
                        PartyMsg psg = new PartyMsg();
                        psg.CONNECTIONSTRING = ConnectionString;
                        psg.SANCTIONHOST = SanctionHost;
                        psg.CHECKINGID = dtMessage.Rows[i]["CHECKINGID"].ToString();
                        psg.PARTYMSGID = int.Parse(dtMessage.Rows[i]["PARTYMSGID"].ToString());
                        psg.NAME = dtMessage.Rows[i]["COND_NAME"].ToString();
                        psg.COUNTRY = dtMessage.Rows[i]["COND_COUNTRY"].ToString();
                        psg.MINTHRESHOLD = MinThreshold;
                        psg.RESULT = ResResult;
                        
                        if (_concurrent == 1)
                            psg.CheckSanction(psg);
                        else
                        {
                            //ThreadPool.QueueUserWorkItem(new WaitCallback(CheckSanction), psg);
                            Thread thread = new Thread(() => psg.CheckSanction(psg));
                            thread.Start();
                            Log.Debug("Thread " + psg.PARTYMSGID + " : Started");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(dtMessage.Rows[i]["COND_NAME"].ToString() + "=>" + ex.Message);
                    }


                    //Waiting before send to queue
                    if (_concurrent > 1)
                    {
                        if (i >= _concurrent)
                        {
                            int queue = (i - TRANSACTION_COUNTER) % _concurrent;
                            while (queue == 0)
                            {
                                Thread.Sleep(1000);
                                queue = (i - TRANSACTION_COUNTER) % _concurrent;
                            }
                        }
                    }
                }


                //Waiting for Thread  
                if (_concurrent > 1)
                {
                    do
                    {
                        Thread.Sleep(2000);

                    } while (TRANSACTION_COUNTER < dtMessage.Rows.Count);
                }

                o.openConnection();
                for (int i = 0; i < dtMessage.Rows.Count; i++)
                {
                    try
                    {
                        Log.Debug("RESULT : " + ResResult[dtMessage.Rows[i]["CHECKINGID"].ToString()].STATUS);
                        PARTYMsgChk chk = ResResult[dtMessage.Rows[i]["CHECKINGID"].ToString()];
                        //Step 1 Insert Result Check Sanction
                        o.createCommand("UPDATE OPICINF.KKB_TB_PARTYMSG_CHK SET STATUS=:STATUS, MSG=:MSG, LOG_UPDATEBYUSERID='SYSTEM', LOG_UPDATEDATE=SYSDATE WHERE CHECKINGID=:CHECKINGID", CommandType.Text);
                        o.setVarCharInputParameter("STATUS", chk.STATUS);
                        o.setVarCharInputParameter("MSG", chk.MSG);
                        o.setVarCharInputParameter("CHECKINGID", dtMessage.Rows[i]["CHECKINGID"].ToString());
                        o.executeNonQuery();

                        if (chk.STATUS.Equals("CWL-I-1000"))
                        {
                            o.createCommand("INSERT INTO OPICINF.KKB_TB_PARTYMSG_MATCH(CHECKINGID, MSG, LOG_INSERTBYUSERID, LOG_INSERTDATE) VALUES(:CHECKINGID, :MSG, 'SYSTEM', SYSDATE)", CommandType.Text);
                            o.setVarCharInputParameter("CHECKINGID", dtMessage.Rows[i]["CHECKINGID"].ToString());
                            o.setCLOBInputParameter("MSG", chk.DETAIL);
                            o.executeNonQuery();
                        }                        
                    }
                    catch (Exception ex)
                    {
                        Log.Error("CHECKINGID " + dtMessage.Rows[i]["CHECKINGID"].ToString() + " : " + ex.Message);
                    }
                }
                string sqlReview = "UPDATE OPICINF.KKB_TB_PARTYMSG ";
                sqlReview = sqlReview + "SET STATUS='" + MessageStatus.WAITING_TO_REVIEW + "' ";
                sqlReview = sqlReview + "WHERE FILENAME='" + FileName + "' AND STATUS='WAITING_TO_SEND' ";
                sqlReview = sqlReview + "AND PARTYMSGID IN (SELECT PARTYMSGID FROM OPICINF.KKB_TB_PARTYMSG_CHK WHERE STATUS='CWL-I-1000') ";
                o.createCommand(sqlReview, CommandType.Text);
                int rowsReview = o.executeNonQuery();

                string sqlTransfer = "UPDATE OPICINF.KKB_TB_PARTYMSG ";
                sqlTransfer = sqlTransfer + "SET STATUS='" + MessageStatus.TRANSFER_TO_SWIFT + "' ";
                sqlTransfer = sqlTransfer + "WHERE FILENAME='" + FileName + "' AND STATUS='WAITING_TO_SEND' ";
                sqlTransfer = sqlTransfer + "AND (SELECT COUNT(*) FROM OPICINF.KKB_TB_PARTYMSG_CHK WHERE KKB_TB_PARTYMSG_CHK.PARTYMSGID=KKB_TB_PARTYMSG.PARTYMSGID)=(SELECT COUNT(*) FROM OPICINF.KKB_TB_PARTYMSG_CHK WHERE KKB_TB_PARTYMSG_CHK.PARTYMSGID=KKB_TB_PARTYMSG.PARTYMSGID AND KKB_TB_PARTYMSG_CHK.STATUS='CWL-I-1003') ";
                o.createCommand(sqlTransfer, CommandType.Text);
                int rowsTransfer = o.executeNonQuery();

                string sqlFail = "UPDATE OPICINF.KKB_TB_PARTYMSG ";
                sqlFail = sqlFail + "SET STATUS='" + MessageStatus.FAIL_TO_SEND + "' ";
                sqlFail = sqlFail + "WHERE FILENAME='" + FileName + "' AND STATUS='WAITING_TO_SEND' ";
                sqlFail = sqlFail + "AND PARTYMSGID IN (SELECT PARTYMSGID FROM OPICINF.KKB_TB_PARTYMSG_CHK WHERE STATUS NOT IN ('CWL-I-1000','CWL-I-1003')) ";
                o.createCommand(sqlFail, CommandType.Text);
                int rowsFail = o.executeNonQuery();

                o.closeConnection();

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally {
                TRANSACTION_COUNTER = 0;
            }
        }

        private void CheckSanction(PartyMsg psg) 
        {
            //PartyMsg psg = a as PartyMsg;

            try
            {
                searchRequest request = new searchRequest();
                
                TreasuryModel.SanctionService.HeaderType headtype = new TreasuryModel.SanctionService.HeaderType();
                //Fixed
                headtype.ChannelID = "OPICS";
                headtype.serviceName = "search";
                headtype.systemCode = "OPICS";
                //Ref
                headtype.referenceNo = psg.CHECKINGID.ToString();
                headtype.transactionDateTime = DateTime.Now.ToString("yyyyMMddHHmmss"); 

                IDCheckType idc = new IDCheckType();
                idc.documentType = DocumentTypeType.UNSPECIFIED;
                idc.idNumber = "";
                idc.issuingState = "";
                
                UnspecifiedType unspec = new UnspecifiedType();
                unspec.names = psg.NAME;
                unspec.maxResult = "20";
                unspec.minScoreThreshold = psg.MINTHRESHOLD;
                unspec.nameTransposition = OptionType.N;
                unspec.ei = EIType.X;
                                
                unspec.idCheck = idc;

                request.header = headtype;
                request.Item = unspec;

                SearchService srch = new SearchService();
                srch.Url = psg.SANCTIONHOST + "/sanction/services/SearchService/";
                searchResponse response = new searchResponse();
                try
                {
                    response = srch.search(request);                   
                }
                catch (Exception ex)
                {
                    Log.Error("Request Error : " + ex.Message);
                } 
                finally
                {
                    srch.Dispose();
                }

                string ResultMessage = ResponseStatus.DATA_NOT_FOUND;
                Dictionary<string, LookupRecordType> lookrec = new Dictionary<string, LookupRecordType>();

                if (response.responseStatus.responseCode.Equals(ResponseStatus.PROCESSED_SUCCESSFUL))
                {
                    ResultMessage = ResponseStatus.PROCESSED_SUCCESSFUL;
                    //if (response.record != null)
                    //{
                    //    for (int j = 0; j < response.record.Count(); j++)
                    //    {
                    //        RecordType record = response.record[j];   
                            
                    //        if (record.sourceWatchlist.Equals("WC1"))
                    //        {
                    //            lookupRequest lookrequest = new lookupRequest();
                    //            TreasuryModel.LookupService.HeaderType lookhead = new TreasuryModel.LookupService.HeaderType();
                    //            //Fixed
                    //            lookhead.ChannelID = "OPICS";
                    //            lookhead.serviceName = "lookup";
                    //            lookhead.systemCode = "OPICS";
                    //            //Ref
                    //            lookhead.referenceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //            lookhead.transactionDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                    //            lookrequest.header = lookhead;
                    //            lookrequest.uid = record.uid;

                    //            LookupService.LookupService looksrch = new LookupService.LookupService();
                    //            looksrch.Url = psg.SANCTIONHOST + "/sanction/services/LookupService/";
                    //            try
                    //            {
                    //                lookupResponse lookresponse = looksrch.lookup(lookrequest);
                    //                LookupRecordType lookrecord = lookresponse.lookupRecord;
                    //                lookrec.Add(record.uid, lookrecord);
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                Log.Error("Lookup Request Error : " + ex.Message);
                    //            }
                    //            finally
                    //            {
                    //                looksrch.Dispose();
                    //            }
                    //        }
                    //        else if (record.sourceWatchlist.Equals("CDD"))
                    //        {
                    //            LookupCDDService.lookupCDDRequest lookrequest = new LookupCDDService.lookupCDDRequest();
                    //            LookupCDDService.HeaderType lookhead = new LookupCDDService.HeaderType();
                    //            //Fixed
                    //            lookhead.ChannelID = "OPICS";
                    //            lookhead.serviceName = "lookupCDD";
                    //            lookhead.systemCode = "OPICS";
                    //            //Ref
                    //            lookhead.referenceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //            lookhead.transactionDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                    //            lookrequest.header = lookhead;
                    //            lookrequest.entityID = record.uid;

                    //            LookupCDDService.LookupCDDService looksrch = new LookupCDDService.LookupCDDService();
                    //            looksrch.Url = psg.SANCTIONHOST + "/sanction/services/LookupCDDService/";
                    //            try
                    //            {
                    //                LookupCDDService.lookupCDDResponse lookresponse = looksrch.lookup(lookrequest);
                    //                LookupCDDService.LookupCDDRecordType lookrecord = lookresponse.lookupCDDRecord;
                    //                //lookrec.Add(record.uid, lookrecord);
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                Log.Error("Lookup Request Error : " + ex.Message);
                    //            }
                    //            finally
                    //            {
                    //                looksrch.Dispose();
                    //            }
                    //        }
                    //    }
                    //}
                }
                else if (!response.responseStatus.responseCode.Equals(ResponseStatus.DATA_NOT_FOUND))
                    ResultMessage = response.responseStatus.responseCode.ToString();
                               
                

                //Step 2 If Match
                StringWriter strMsg = new StringWriter();
                //StringWriter lookMsg = new StringWriter();
                if (response.record != null)
                {
                    string checkingid = psg.CHECKINGID;
                    for (int k = 0; k < response.record.Count(); k++)
                    {
                        XmlSerializer mySerializer;
                        try
                        {
                            mySerializer = new XmlSerializer(typeof(RecordType));
                            mySerializer.Serialize(strMsg, response.record[k]);
                            Log.Debug(strMsg.ToString());
                            strMsg.Write("<Record />");
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }

                        //try
                        //{
                        //    XmlSerializer mySerializer = new XmlSerializer(typeof(LookupRecordType));
                        //    mySerializer.Serialize(lookMsg, lookrec[response.record[k].uid]);
                        //    Log.Debug(lookMsg.ToString());
                        //}
                        //catch (Exception ex)
                        //{
                        //    Log.Error(ex.Message);
                        //}   
                        
                    }
                }
                        //connect.closeConnection();
                PARTYMsgChk chk = new PARTYMsgChk();
                chk.CHECKINGID = int.Parse(psg.CHECKINGID);
                chk.STATUS =  response.responseStatus.responseCode.Trim();
                chk.MSG = response.responseStatus.responseMessage.Trim();
                chk.DETAIL = strMsg.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");                
                psg.RESULT.Add(psg.CHECKINGID, chk);
 
                    
            }
            catch (Exception ex)
            {
                Log.Error("CONDITION ERROR : " + psg.PARTYMSGID + ":" + psg.NAME + ":" + ex.Message); 
            }
            finally {
                TRANSACTION_COUNTER++; 
            }
        }

        public static object MoneyTransferList(string ConnectionString, string fileload, string transno, string execdate, string status, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_PARTYMSG WHERE 1=1 ";
                if (!fileload.Trim().Equals(""))
                {
                    sql += "AND FILENAME LIKE '%" + fileload.Trim() + "%' ";
                }
                if (!transno.Trim().Equals(""))
                {
                    sql += "AND REFNO LIKE '%" + transno.Trim() + "%' ";
                }
                if (!execdate.Trim().Equals(""))
                {
                    sql += "AND TO_CHAR(LOG_INSERTDATE,'DD/MM/YYYY')='" + execdate + "' ";
                }
                if (!status.Trim().Equals(""))
                {
                    sql += "AND STATUS like '" + status + "' ";
                }
                sql += "ORDER BY REFNO DESC";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");

                List<PartyMsg> swift = new List<PartyMsg>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PartyMsg s = new PartyMsg();
                    s.PARTYMSGID = int.Parse(dt.Rows[i]["PARTYMSGID"].ToString());
                    s.REFNO = dt.Rows[i]["REFNO"].ToString();
                    s.MSG = dt.Rows[i]["MSG"].ToString();
                    s.STATUS = dt.Rows[i]["STATUS"].ToString();
                    swift.Add(s);
                }

                //Return result to jTable
                return new { Result = "OK", Records = jtPageSize > 0 ? swift.Skip(jtStartIndex).Take(jtPageSize).ToList() : swift, TotalRecordCount = swift.Count };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static PartyMsg getMessageByID(string ConnectionString, string id)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_PARTYMSG Where PARTYMsgID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");

                PartyMsg s = new PartyMsg();
                s.PARTYMSGID = int.Parse(dt.Rows[0]["PARTYMSGID"].ToString());
                s.REFNO = dt.Rows[0]["REFNO"].ToString();
                s.MSG = dt.Rows[0]["MSG"].ToString();
                s.STATUS = dt.Rows[0]["STATUS"].ToString();

                //Splite Message to object
                string[] data = dt.Rows[0]["MSG"].ToString().Split('|');
                s.AMT_PAY = data[6];
                s.BANK_ACCOUNT_NO = data[30];
                s.BENEF_ADDRESS = data[23];
                s.BENEF_CITY = data[24];
                s.BENEF_COUNTRY = data[27];
                s.BENEF_FIRSTNAME = data[19];
                s.BENEF_LASTNAME1 = data[20];
                s.BENEF_LASTNAME2 = data[21];
                s.BENEF_STATE = data[25];
                s.BENEF_TELNO = data[22];
                s.BENEF_ZIPCODE = data[26];
                s.CCY_PAY = data[5];
                s.CLAVE = data[7];
                s.CORRESPONDENT_BRANCH = data[8];
                s.CORRESPONDENT_BRANCH_NO = data[9];
                s.CORRESPONDENT_ID = data[0];
                s.CUST_ADDRESS = data[14];
                s.CUST_CITY = data[15];
                s.CUST_COUNTRY = data[18];
                s.CUST_FIRSTNAME = data[10];
                s.CUST_LASTNAME1 = data[11];
                s.CUST_LASTNAME2 = data[12];
                s.CUST_STATE = data[16];
                s.CUST_TEL_NO = data[13];
                s.CUST_ZIPCODE = data[17];
                s.DELIVERY_METHOD = data[29];
                s.MESSAGE_FOR_BENEF = data[28];
                s.ORDER_DATE = data[4];
                s.ORDER_ID = data[2];
                s.ORDER_NO = data[3];
                s.PAYING_SEQUENCE_ID = data[1];
                
                //Return result to jTable
                return s;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object GetUIDDetail(int id)
        {
            try
            {                
                lookupRequest lookrequest = new lookupRequest();
                TreasuryModel.LookupService.HeaderType lookhead = new TreasuryModel.LookupService.HeaderType();
                lookhead.ChannelID = "OPICS";
                lookhead.serviceName = "lookup";
                lookhead.systemCode = "OPICS";
                lookhead.referenceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                lookhead.transactionDateTime = DateTime.Now.ToString("yyyyMMddHHmmss"); 
                lookrequest.header = lookhead;
                lookrequest.uid = id.ToString();

                lookupResponse lookresponse = new lookupResponse();
                LookupService.LookupService lookservice = new TreasuryModel.LookupService.LookupService();
                lookresponse = lookservice.lookup(lookrequest);

                if (lookresponse.lookupRecord != null)
                    return new { Result = "OK", Record = lookresponse.lookupRecord };
                else
                    return new { Result = "ERROR", Message = "UID Not Found" };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static Boolean AcceptTransaction(string ConnectionString, string id, string Remark)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Update OPICINF.KKB_TB_PARTYMSG Set Status='" + MessageStatus.APPROVE + "', Remark='" + Remark + "' Where PARTYMSGID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                int row = o.executeNonQuery();

                //Return result to jTable
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean RejectTransaction(string ConnectionString, string id, string Remark)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Update OPICINF.KKB_TB_PARTYMSG Set Status='" + MessageStatus.DISAPPROVE + "', Remark='" + Remark + "' Where PARTYMSGID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                int row = o.executeNonQuery();

                //Return result to jTable
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean RecheckTransaction(string ConnectionString, string id, string Remark)
        {
            try
            {

                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    string sql = "Update OPICINF.KKB_TB_PARTYMSG Set Status='" + MessageStatus.WAITING_TO_SEND + "', Remark='" + Remark + "' Where PARTYMSGID='" + id + "'";
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    int row = o.executeNonQuery();

                    //Return result to jTable
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetMessageSanctionResult(string ConnectionString, string FileName)
        {
            try
            {
                string sql = "SELECT KKB_TB_PARTYMSG.PARTYMSGID, KKB_TB_PARTYMSG.MSG, KKB_TB_PARTYMSG_CHK.COND_NAME , KKB_TB_PARTYMSG_MATCH.MSG AS RESULTMSG  ";
                sql += "FROM OPICINF.KKB_TB_PARTYMSG LEFT OUTER JOIN OPICINF.KKB_TB_PARTYMSG_CHK ON KKB_TB_PARTYMSG.PARTYMSGID=KKB_TB_PARTYMSG_CHK.PARTYMSGID ";
                sql += "LEFT OUTER JOIN OPICINF.KKB_TB_PARTYMSG_MATCH ON KKB_TB_PARTYMSG_CHK.CHECKINGID=KKB_TB_PARTYMSG_MATCH.CHECKINGID ";
                sql += "WHERE KKB_TB_PARTYMSG_CHK.STATUS='CWL-I-1000' AND FILENAME='" + FileName + "' ";

                Oracle o = Oracle.getInstance(ConnectionString);
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");
                o.closeConnection();

                string strMsg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strMsg = strMsg + "<tr>";
                    //Extract Message
                    string[] str = dt.Rows[i]["MSG"].ToString().Split('|');

                    //Extract Result Message 
                    //RecordType[] record = new RecordType[]{};
                     
                    try
                    {
                        XmlSerializer mySerializer = new XmlSerializer(typeof(RecordType));
                        string[] rd = dt.Rows[i]["RESULTMSG"].ToString().Split(new string[] { "<Record />" }, StringSplitOptions.None);
                        using (TextReader reader = new StringReader(rd[0]))
                        { 
                            var record = (RecordType)mySerializer.Deserialize(reader);

                            if (record != null)
                            {
                                strMsg = strMsg + "<td>Money Transfer (TRO)</td>";
                                strMsg = strMsg + "<td>" + str[2] + "</td>"; //RIA Order ID;
                                strMsg = strMsg + "<td>&nbsp;</td>";
                                strMsg = strMsg + "<td>" + dt.Rows[i]["COND_NAME"].ToString() + "</td>";
                                strMsg = strMsg + "<td>&nbsp;</td>";
                                strMsg = strMsg + "<td>&nbsp;</td>";
                                strMsg = strMsg + "<td>" + String.Join(",", record.keyword) + "</td>";
                                strMsg = strMsg + "<td>" + record.category + "</td>";
                                strMsg = strMsg + "<td>" + String.Join(",", record.types) + "</td>";
                                strMsg = strMsg + "<td>" + record.subCategory + "</td>";
                                if (record.aliasMatched != null)
                                    strMsg = strMsg + "<td>" + record.aliasMatched + "</td>";
                                else
                                    strMsg = strMsg + "<td>" + record.name + "</td>";
                                strMsg = strMsg + "<td>" + record.ruleDesc + "</td>";
                                strMsg = strMsg + "<td>" + record.ruleType + "</td>";
                            }
                            else
                            {
                                strMsg = strMsg + "<td>Money Transfer (TRO)</td>";
                                strMsg = strMsg + "<td>" + str[2] + "</td>"; //RIA Order ID;
                                strMsg = strMsg + "<td>&nbsp;</td>";
                                strMsg = strMsg + "<td>" + dt.Rows[i]["COND_NAME"].ToString() + "</td>";
                                strMsg = strMsg + "<td colspan='9'> Get Detail Error!! Please check on World check</td>";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        //record = null;
                    }
                    
                    strMsg = strMsg + "</tr>";
                }
                return strMsg;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable MessageResultList(string ConnectionString, DataTable dtFind)
        {
            try
            {
                string id = "";
                for (int i = 0; i < dtFind.Rows.Count; i++)
                {
                    id += dtFind.Rows[i]["PARTYMSGID"] + " ";
                }
                id = id.Trim().Replace(' ', ',');

                if (!id.Equals(""))
                {
                    string sql = "SELECT STATUS, COUNT(*) AS RESULT FROM OPICINF.KKB_TB_PARTYMSG WHERE PARTYMSGID  IN (" + id + ") GROUP BY STATUS ";

                    Oracle o = Oracle.getInstance(ConnectionString);
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    DataTable dt = o.executeReaderToDT("PARTYMSGRESULT");
                    o.closeConnection();
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable getFileLoad(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT FILENAME FROM OPICINF.KKB_TB_PARTYMSG WHERE 1=1 GROUP BY FILENAME ORDER BY MAX(LOG_INSERTDATE) DESC";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("FILELOADMSG");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetMessagePassResult(string ConnectionString, string FileName)
        {
            try
            {
                string sql = "SELECT MSG FROM OPICINF.KKB_TB_PARTYMSG ";
                sql += "WHERE  FILENAME='" + FileName + "' ";
                sql += "AND PARTYMSGID NOT IN (SELECT PARTYMSGID FROM OPICINF.KKB_TB_PARTYMSG_CHK WHERE STATUS!='CWL-I-1003') ";

                Oracle o = Oracle.getInstance(ConnectionString);
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");
                o.closeConnection();

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static DataTable GetMessageBySelected(string ConnectionString, string PartyMsgID)
        {
            try
            {
                string sql = "SELECT MSG FROM OPICINF.KKB_TB_PARTYMSG ";
                sql += "WHERE  PARTYMSGID IN  (" + PartyMsgID + ") ";

                Oracle o = Oracle.getInstance(ConnectionString);
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSG");
                o.closeConnection();

                return dt;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
    }
}
