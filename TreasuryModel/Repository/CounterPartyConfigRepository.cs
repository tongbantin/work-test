using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using TreasuryModel.Data.Collateral;
using System.Data;
namespace TreasuryModel.Repository
{
    public class CounterPartyConfigRepository : BaseCollateralRepository<CounterPartyConfigRepository>
    {   
        public static List<CTR_SETTING> GetAll()
        {

            string cmdText = "OPICINF.KKB_PKG_COLL_CTR_CONF.GET_CTR_SETTING";
            Log.Info("-----KKB_PKG_COLL_CTR_CONF Start Select -----");
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            List<CTR_SETTING> result = new List<CTR_SETTING>();
            try
            {
                string tableName = "CTR_SETTING";
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                DataSet ds = o.executeReader("OUT_RET", tableName);
                foreach (DataRow row in ds.Tables[tableName].Rows)
                {
                    CTR_SETTING conf = row.ToCTR_SETTING();
                    result.Add(conf);
                }
            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Error Select-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Finish Select-----");
                o.closeConnection();
            }
            return result;
        }
        public static CTR_SETTING GetById(int Id)
        {
            return GetAll().SingleOrDefault(x => x.SETTING_ID == Id);
        }
        public static int Create(CTR_SETTING record)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_CTR_CONF.INS_CTR_SETTING";

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
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Error Insert-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Finish Insert-----");
                o.closeConnection();
            }
            return effected;
        }
        public static int Update(CTR_SETTING record)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_CTR_CONF.UPD_CTR_SETTING";

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
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Error Update-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Finish Update-----");
                o.closeConnection();
            }
            return effected;
        }
        public static int Delete(int Id)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_CTR_CONF.DEL_CTR_SETTING";

            Log.Info("-----KKB_PKG_COLL_CTR_CONF Start Delete-----");
            Log.Info("SQL Statement : " + cmdText);

            int effected = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {

                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                o.setDecimalInputParameter("IN_SETTING_ID", Id);
                effected = o.executeNonQuery();

            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Error Delete-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_CTR_CONF Finish Delete-----");
                o.closeConnection();
            }
            return effected;
        }

    }
}
