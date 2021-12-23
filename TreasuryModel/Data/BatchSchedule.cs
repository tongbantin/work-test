using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common; 
using System.Data;
using log4net;
using System.Configuration;

namespace TreasuryModel.Data
{
    public class BatchSchedule
    {
        public int SCHEDULEID { get; set; }
        public string SCHEDULETIME { get; set; }
        public string NAME { get; set; }
        public string ENABLED { get; set; }

        private static ILog Log = log4net.LogManager.GetLogger(typeof(BatchSchedule));
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        
        public static List<BatchSchedule> GetBatchSchedule()
        {
            Log.Debug("Get Schedule");
            Log.Debug(ConnectionString);
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_BATCH_SCHEDULE WHERE ENABLED='1'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("BATCH_SCHEDULE");

                List<BatchSchedule> param = new List<BatchSchedule>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BatchSchedule p = new BatchSchedule();
                    p.SCHEDULEID = int.Parse(dt.Rows[i]["SCHEDULEID"].ToString());
                    p.SCHEDULETIME = dt.Rows[i]["SCHEDULETIME"].ToString();
                    p.NAME = dt.Rows[i]["NAME"].ToString();
                    p.ENABLED = dt.Rows[i]["ENABLED"].ToString();

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
    }
}
