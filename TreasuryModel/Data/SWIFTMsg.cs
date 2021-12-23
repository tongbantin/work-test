using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class SWIFTMsg
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(SWIFTMsg));

        public int SWIFTMSGID { get; set; }
        public string REFNO { get; set; }
        public string MSG { get; set; }
        public string STATUS { get; set; }
        public string DECISIONBY { get; set; }
        public string APPROVEBY { get; set; }

        public static Dictionary<string, string[]> SpliteMessage(string swiftMessage)
        {
            try
            {
                Dictionary<string, string[]> dictmsg = new Dictionary<string, string[]>();
                String[] series = swiftMessage.Replace("{", "").Split('}');  //Delete { and split with } 
                foreach (var msgline in series)
                {
                    if ((!msgline.Trim().Equals("")) && (msgline.IndexOf(':')>0))
                    {
                        String[] msg = msgline.Split(':'); //Split each row 
                        try
                        {
                            Log.Debug("Message Length : " + msg.Length);
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

        public static object SWIFTList(string ConnectionString, string transno, string execdate, string status, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT * FROM OPICINF.KKB_TB_SWIFTMSG WHERE 1=1 ";
                if (!transno.Trim().Equals(""))
                {
                    sql += "AND REFNO LIKE '%" + transno.Trim() + "%'";
                }
                if (!execdate.Trim().Equals(""))
                {
                    sql += "AND TO_CHAR(LOG_INSERTDATE,'DD/MM/YYYY')='" + execdate + "'";
                }
                if (!status.Trim().Equals(""))
                {
                    sql += "AND STATUS like '" + status + "'";
                }
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("SWIFTMSG");

                List<SWIFTMsg> swift = new List<SWIFTMsg>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SWIFTMsg s = new SWIFTMsg();
                    s.SWIFTMSGID = int.Parse(dt.Rows[i]["SWIFTMSGID"].ToString());
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

        public static SWIFTMsg getMessageByID(string ConnectionString, string id)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_SWIFTMSG Where SWIFTMsgID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("SWIFTMSG");

                SWIFTMsg s = new SWIFTMsg();
                s.SWIFTMSGID = int.Parse(dt.Rows[0]["SWIFTMSGID"].ToString());
                s.REFNO = dt.Rows[0]["REFNO"].ToString();
                s.MSG = dt.Rows[0]["MSG"].ToString();
                s.STATUS = dt.Rows[0]["STATUS"].ToString(); 

                //Return result to jTable
                return s;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static Boolean AcceptTransaction(string ConnectionString, string id, string Remark)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Update OPICINF.KKB_TB_SWIFTMSG Set Status='" + MessageStatus.APPROVE + "', Remark='" + Remark + "', LOG_UPDATEDATE=SYSDATE Where SWIFTMSGID='" + id + "'";
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
                string sql = "Update OPICINF.KKB_TB_SWIFTMSG Set Status='" + MessageStatus.DISAPPROVE + "', Remark='" + Remark + "', LOG_UPDATEDATE=SYSDATE Where SWIFTMSGID='" + id + "'";
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
                    string sql = "Update OPICINF.KKB_TB_SWIFTMSG Set Status='" + MessageStatus.WAITING_TO_SEND + "', Remark='" + Remark + "' Where SWIFTMSGID='" + id + "'";
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
    }
}
