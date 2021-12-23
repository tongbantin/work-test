using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasuryModel.Data.Collateral;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Repository
{
    public class RateGroupRepository : BaseCollateralRepository<RateGroupRepository>
    {
        public static List<RATE_GRP> GetAll()
        {

            string cmdText = "OPICINF.KKB_PKG_COLL_RATE_GRP.GET_CTR_SETTING";
            Log.Info("-----KKB_PKG_COLL_RATE_GRP Start Select -----");
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            List<RATE_GRP> result = new List<RATE_GRP>();
            try
            {
                string tableName = "GET_RATE_GRP";
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);

                DataSet ds = o.executeReader("OUT_RET", tableName);
                foreach (DataRow row in ds.Tables[tableName].Rows)
                {
                    RATE_GRP conf = row.ToRATE_GRP();
                    result.Add(conf);
                }
            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Error Select-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Finish Select-----");
                o.closeConnection();
            }
            return result;
        }
        public static RATE_GRP GetById(int Id)
        {
            return GetAll().SingleOrDefault(x => x.RATE_GRP_ID == Id);
        }
        public static int Create(RATE_GRP record)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_RATE_GRP.INS_CTR_SETTING";

            Log.Info("-----KKB_PKG_COLL_CTR_CONF Start Insert-----");
            Log.Info("SQL Statement : " + cmdText);

            int effected = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                record.ToOracleParameters(ref o);
                effected = o.executeNonQuery();

            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Error Insert-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Finish Insert-----");
                o.closeConnection();
            }
            return effected;
        }
        public static int Update(RATE_GRP record)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_RATE_GRP.UPD_CTR_SETTING";

            Log.Info("-----KKB_PKG_COLL_CTR_CONF Start Update-----");
            Log.Info("SQL Statement : " + cmdText);

            int effected = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                record.ToOracleParameters(ref o);
                effected = o.executeNonQuery();

            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Error Update-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Finish Update-----");
                o.closeConnection();
            }
            return effected;
        }
        public static int Delete(int Id)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_RATE_GRP.DEL_CTR_SETTING";

            Log.Info("-----KKB_PKG_COLL_CTR_CONF Start Delete-----");
            Log.Info("SQL Statement : " + cmdText);

            int effected = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                o.setDecimalInputParameter("IN_RATE_GRP_ID", Id);
                effected = o.executeNonQuery();

            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Error Delete-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_RATE_GRP Finish Delete-----");
                o.closeConnection();
            }
            return effected;
        }
    }
}
