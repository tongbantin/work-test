using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;


namespace TreasuryModel.Data
{
    public class PARTYMsgChk
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(PARTYMsgChk));

        public int CHECKINGID { get; set; }
        public int PARTYMSGID { get; set; }
        public int MATCHINGID { get; set; }
        public string STATUS { get; set; }
        public string MSG { get; set; }
        public string DETAIL { get; set; }
        public string UIDDETAIL { get; set; }
        public string COND_NAME { get; set; }
        public string SOURCE { get; set; }

        public static List<PARTYMsgChk> getCheckSanctionByMessageID(string ConnectionString, string id)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_PARTYMSG_CHK Where PARTYMsgID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("PARTYMSGCHK");

                List<PARTYMsgChk> sc = new List<PARTYMsgChk>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PARTYMsgChk s = new PARTYMsgChk();
                    s.CHECKINGID = int.Parse(dt.Rows[i]["CHECKINGID"].ToString());
                    s.PARTYMSGID = int.Parse(dt.Rows[i]["PARTYMSGID"].ToString());
                    s.STATUS = dt.Rows[i]["STATUS"].ToString();
                    s.MSG = dt.Rows[i]["MSG"].ToString();
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

        public static List<PARTYMsgChk> getSanctionDetailByMessageID(string ConnectionString, string id)
        {
            try
            {
                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    string sql = "Select * From OPICINF.KKB_TB_PARTYMSG_MATCH Where CHECKINGID='" + id + "'";
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    DataTable dt = o.executeReaderToDT("PARTYMSGMATCH");

                    List<PARTYMsgChk> sc = new List<PARTYMsgChk>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PARTYMsgChk s = new PARTYMsgChk();
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
