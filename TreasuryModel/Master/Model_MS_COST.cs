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
    public class Model_MS_COST
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_COST));

        public static DataSet getDataSet(string ConnectionString)
        {
            String sql = "Select TRIM(COSTCENT) as COSTCENT, COSTCENT as COSTDESC From OPICS.COST Order By COSTCENT DESC";
            Log.Info("-----Model_MS_COST Start-----");
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
                Log.Info("-----Model_MS_COST Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
