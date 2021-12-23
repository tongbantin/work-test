using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasuryModel.Data.Collateral;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;

namespace TreasuryModel.Repository
{
    public class ExposureRepository : BaseCollateralRepository<ExposureRepository>
    {
        public static List<CTR_EXPOSURE> GetList(string counterPartyName, string transDate)
        {

            string cmdText = "OPICINF.KKB_PKG_COLL_EXPOSURE.GET_EXPOSURES";
            Log.Info("-----KKB_PKG_COLL_EXPOSURE Start Select -----");
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            List<CTR_EXPOSURE> result = new List<CTR_EXPOSURE>();
            try
            {
                string tableName = @"GET_EXPOSURES";
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                o.setInputParameter("IN_CTR_NAME", counterPartyName);
                o.setInputParameter("IN_TRANS_DATE", transDate);
                DataSet ds = o.executeReader("OUT_RET", tableName);
                foreach (DataRow row in ds.Tables[tableName].Rows)
                {
                    CTR_EXPOSURE conf = row.ToCTR_EXPOSURE();
                    result.Add(conf);
                }
            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_EXPOSURE Error Select-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_EXPOSURE Finish Select-----");
                o.closeConnection();
            }
            return result;
        }

        public static CTR_EXPOSURE_DETAIL GetDetail(string counterPartyName, string transDate)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_EXPOSURE.GET_EXPOSURE_DETAIL";
            Log.Info("-----KKB_PKG_COLL_EXPOSURE Start Select -----");
            Log.Info("SQL Statement : " + cmdText);

            Oracle o = Oracle.getInstance(ConnectionString);
            List<CTR_EXPOSURE_DETAIL> result = new List<CTR_EXPOSURE_DETAIL>();
            try
            {
                string tableName = @"GET_EXPOSURE_DETAIL";
                o.openConnection();
                o.createCommand(cmdText, CommandType.Text);
                o.setInputParameter("IN_CTR_NAME", counterPartyName);
                o.setInputParameter("IN_TRANS_DATE", transDate);
                DataSet ds = o.executeReader("OUT_RET", tableName);
                foreach (DataRow row in ds.Tables[tableName].Rows)
                {
                    CTR_EXPOSURE_DETAIL conf = row.ToCTR_EXPOSURE_DETAIL();
                    result.Add(conf);
                }
            }
            catch (Exception ex)
            {
                Log.Info("----- KKB_PKG_COLL_EXPOSURE Error Select-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_EXPOSURE Finish Select-----");
                o.closeConnection();
            }
            return result.FirstOrDefault();
        }
        public static int SaveDetail(CTR_EXPOSURE_DETAIL record)
        {
            string cmdText = "OPICINF.KKB_PKG_COLL_EXPOSURE.UPD_EXPOSURE_DETAIL";

            Log.Info("-----KKB_PKG_COLL_EXPOSURE Start Update-----");
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
                Log.Info("----- KKB_PKG_COLL_EXPOSURE Error Update-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("----- KKB_PKG_COLL_EXPOSURE Finish Update-----");
                o.closeConnection();
            }
            return effected;
        }
    }
}
