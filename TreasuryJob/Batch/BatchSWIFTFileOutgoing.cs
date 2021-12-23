using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;
using System.Configuration;
using System.Net;
using System.IO;
using System.Security;
using TreasuryJob.Model;

namespace TreasuryJob.Batch
{
    public class BatchSWIFTFileOutgoing : IJob
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BatchSWIFTFileOutgoing));

        #region IJob Members

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Log.Debug("Start Execute Batch Job ");
                string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
                Log.Debug(ConnectionString);
                string[] series = ConfigurationManager.AppSettings["SWIFTFILE_SERIES"].ToString().Split(',');
                Log.Debug(series.Length);
                SWIFTFile.DownloadFileMessage(ConnectionString, ConfigurationManager.AppSettings["SWIFTFILE_OUTGOING_FOLDER"].ToString(), "Outgoing", series);
                SWIFTFile.DownloadFileMessage(ConnectionString, ConfigurationManager.AppSettings["SWIFTFILE_ACK_FOLDER"].ToString(), "ACK", series);
                SWIFTFile.DownloadFileMessage(ConnectionString, ConfigurationManager.AppSettings["SWIFTFILE_NAK_FOLDER"].ToString(), "NAK", series);
                Log.Debug("DownloadFileMessage");
                SWIFTFile.opicsSwift300Matching(ConnectionString, DateTime.Now.ToString("dd/MM/yyyy"));
                Log.Debug("300Matching");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        #endregion
    }
}
