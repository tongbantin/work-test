using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;


namespace TreasuryModel.Data
{
    public class SWIFTMsgChk
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(SWIFTMsgChk));

        public int CHECKINGID { get; set; }
        public int SWIFTMSGID { get; set; }
        public int MATCHINGID { get; set; }
        public string STATUS { get; set; }
        public string MSG { get; set; }
        public string DETAIL { get; set; }
        public string UIDDETAIL { get; set; }
        public string COND_NAME { get; set; }
        public string SOURCE { get; set; }

        public static List<SWIFTMsgChk> getCheckSanctionByMessageID(string ConnectionString, string id)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_SWIFTMSG_CHK Where SWIFTMsgID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("SWIFTMSGCHK");

                List<SWIFTMsgChk> sc = new List<SWIFTMsgChk>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SWIFTMsgChk s = new SWIFTMsgChk();
                    s.CHECKINGID = int.Parse(dt.Rows[i]["CHECKINGID"].ToString());
                    s.SWIFTMSGID = int.Parse(dt.Rows[i]["SWIFTMSGID"].ToString());
                    s.STATUS = dt.Rows[i]["STATUS"].ToString();
                    s.MSG = dt.Rows[i]["MSG"].ToString();
                    s.DETAIL = dt.Rows[i]["DETAIL"].ToString();
                    s.COND_NAME = dt.Rows[i]["COND_NAME"].ToString();
                    sc.Add(s);
                }
                //Return result to jTable
                return sc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<SWIFTMsgChk> getSanctionDetailByMessageID(string ConnectionString, string id)
        {
            try
            {
                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    string sql = "Select * From OPICINF.KKB_TB_SWIFTMSG_MATCH Where CHECKINGID='" + id + "'";
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    DataTable dt = o.executeReaderToDT("SWIFTMSGMATCH");

                    List<SWIFTMsgChk> sc = new List<SWIFTMsgChk>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SWIFTMsgChk s = new SWIFTMsgChk();
                        s.MATCHINGID = int.Parse(dt.Rows[i]["MATCHINGID"].ToString());
                        s.CHECKINGID = int.Parse(dt.Rows[i]["CHECKINGID"].ToString());
                        s.DETAIL = dt.Rows[i]["MSG"].ToString();
                        s.UIDDETAIL = dt.Rows[i]["DETAIL"].ToString();

                        sc.Add(s);
                    }
                    //Return result to jTable
                    return sc;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
    }
}
