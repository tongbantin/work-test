using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasuryModel.Data.Collateral;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;

namespace TreasuryModel.Repository
{
    public class TransactionRepository : BaseCollateralRepository<TransactionRepository>
    {
        const string owner = @"OPICINF";
        const string packageName = @"KKB_PKG_COLL_CTR_CONF_TRANS";

        public static List<TRANS_TYPE> GetTransTypeMaster()
        {
            string processName = @"GET_CONF_TRANS_MASTER";
            string processStart = string.Format(@"-----{0}.{1} Start process -----", packageName, processName);
            string processDone = string.Format(@"-----{0}.{1} Achieve process -----", packageName, processName);
            string processError = string.Format(@"-----{0}.{1} Error process -----", packageName, processName);
            string processFinish = string.Format(@"-----{0}.{1} Finish process -----", packageName, processName);

            string cmdText = string.Format("{0}.{1}.{2}", owner, packageName, processName);
            Log.Info(processStart);
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            List<TRANS_TYPE> result = new List<TRANS_TYPE>();
            try
            {
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                DataTable dt = o.executeReaderToDT("TRANS_TYPE_MASTER");
                foreach (DataRow row in dt.Rows)
                {
                    TRANS_TYPE conf = row.ToTRANS_TYPE();
                    result.Add(conf);
                }
                Log.Info(processDone);
            }
            catch (Exception ex)
            {
                Log.Info(processError);
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info(processFinish);
                o.closeConnection();
            }
            return result;
        }
        public static List<TRANS_TYPE> GetTransTypeConfig(string couterParty, string transDate)
        {

            string processName = @"GET_CONF_TRANS";
            string processStart = string.Format(@"-----{0}.{1} Start process -----", packageName, processName);
            string processDone = string.Format(@"-----{0}.{1} Achieve process -----", packageName, processName);
            string processError = string.Format(@"-----{0}.{1} Error process -----", packageName, processName);
            string processFinish = string.Format(@"-----{0}.{1} Finish process -----", packageName, processName);

            string cmdText = string.Format("{0}.{1}.{2}", owner, packageName, processName);
            Log.Info(processStart);
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            List<TRANS_TYPE> result = new List<TRANS_TYPE>();
            try
            {
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                o.setInputParameter("IN_CTR_NAME", couterParty);
                o.setInputParameter("IN_TRANS_DATE", transDate);
                DataTable dt = o.executeReaderToDT("TRANS_TYPE");
                foreach (DataRow row in dt.Rows)
                {
                    TRANS_TYPE conf = row.ToTRANS_TYPE();
                    result.Add(conf);
                }
                Log.Info(processDone);
            }
            catch (Exception ex)
            {
                Log.Info(processError);
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info(processFinish);
                o.closeConnection();
            }
            return result;
        }
        public static int SaveTransTypeConfig(TRANS_TYPE conf)
        {

            string processName = @"SAV_CONF_TRANS";
            string processStart = string.Format(@"-----{0}.{1} Start process -----", packageName, processName);
            string processDone = string.Format(@"-----{0}.{1} Achieve process -----", packageName, processName);
            string processError = string.Format(@"-----{0}.{1} Error process -----", packageName, processName);
            string processFinish = string.Format(@"-----{0}.{1} Finish process -----", packageName, processName);

            string cmdText = string.Format("{0}.{1}.{2}", owner, packageName, processName);
            Log.Info(processStart);
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            int effected = 0;
            try
            {
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                conf.ToOracleParameters(ref o);
                effected = o.executeNonQuery();

                Log.Info(processDone);
            }
            catch (Exception ex)
            {
                Log.Info(processError);
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info(processFinish);
                o.closeConnection();
            }
            return effected;
        }
        public static int DeleteTransTypeConfig(int transId)
        {

            string processName = @"DEL_CONF_TRANS";
            string processStart = string.Format(@"-----{0}.{1} Start process -----", packageName, processName);
            string processDone = string.Format(@"-----{0}.{1} Achieve process -----", packageName, processName);
            string processError = string.Format(@"-----{0}.{1} Error process -----", packageName, processName);
            string processFinish = string.Format(@"-----{0}.{1} Finish process -----", packageName, processName);

            string cmdText = string.Format("{0}.{1}.{2}", owner, packageName, processName);
            Log.Info(processStart);
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            int effected = 0;
            try
            {
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                o.setInputParameter("IN_TRANS_ID", transId);
                effected = o.executeNonQuery();

                Log.Info(processDone);
            }
            catch (Exception ex)
            {
                Log.Info(processError);
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info(processFinish);
                o.closeConnection();
            }
            return effected;
        }
    }
}
