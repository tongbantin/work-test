using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;

namespace KKB.Treasury.TreasuryModel.Master
{
    public class Model_MS_COUNTERPARTY
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_COUNTERPARTY));

        public static DataSet getDataSet(string ConnectionString)
        {
            String sql = "Select TRIM(CNO) as CNO, CMNE || '(' || CFN1 || ')' as COUNTERPARTY From OPICS.CUST Order By CMNE";
            Log.Info("-----Model_MS_COUNTERPARTY Start-----");
            Log.Info("SQL Statement : ");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_COUNTERPARTY Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
