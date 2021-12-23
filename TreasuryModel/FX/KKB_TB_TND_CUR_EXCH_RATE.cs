using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasuryModel.Api;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.FX
{
    public class KKB_TB_TND_CUR_EXCH_RATE
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(KKB_TB_TND_CUR_EXCH_RATE));

        public static bool insertKKB_TB_TND_CUR_EXCH_RATE(string ConnectionString, int sysid, List<FXRateDataDetail> datadetail)
        {
            Log.Info("-----KKB_TB_TND_CUR_EXCH_RATE Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Insert into OPICINF.KKB_TB_TND_CUR_EXCH_RATE values (:SYSHD_EXCH_RATE_ID, :COUNTRYTH, :COUNTRYEN, :CURRENCY, :BUYING_SIGHT_RATE, :BUYING_TRANSFER_RATE, :SELLING_RATE, :MID_RATE, :UNIT, :DISP_ORDER)";
                o.openConnection();
                o.beginTransaction();
                for (int i = 0; i < datadetail.Count; i++)
                {
                    try
                    {

                        Log.Debug("Start Write Currency:" + datadetail[i].currency_id);
                        string unit = datadetail[i].currency_id.Equals("JPY") ? "100" : datadetail[i].currency_id.Equals("IDR") ? "1000" : "1";
                        o.createTransaction(sql, CommandType.Text);
                        o.setInputParameter("SYSHD_EXCH_RATE_ID", sysid);
                        o.setInputParameter("COUNTRYTH", datadetail[i].currency_name_eng);
                        o.setInputParameter("COUNTRYEN", datadetail[i].currency_name_eng);
                        o.setInputParameter("CURRENCY", datadetail[i].currency_id);
                        o.setInputParameter("BUYING_SIGHT_RATE", datadetail[i].buying_sight.Trim().Equals("") == true ? "0" : datadetail[i].buying_sight);
                        o.setInputParameter("BUYING_TRANSFER_RATE", datadetail[i].buying_transfer.Trim().Equals("") == true ? "0" : datadetail[i].buying_transfer);
                        o.setInputParameter("SELLING_RATE", datadetail[i].selling.Trim().Equals("") == true ? "0" : datadetail[i].selling);
                        o.setInputParameter("MID_RATE", datadetail[i].mid_rate.Trim().Equals("") == true ? "0" : datadetail[i].mid_rate);
                        o.setInputParameter("UNIT", unit);
                        o.setInputParameter("DISP_ORDER", i + 1);
                        row = o.executeNonQuery();
                    }
                    catch (Exception ex) {
                        Log.Error("Error Currency " + datadetail[i].currency_id + " : " + ex.Message);
                    }
                }
                
                o.commitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_TND_CUR_EXCH_RATE Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_TND_CUR_EXCH_RATE Finish-----");
                o.closeConnection();
            }

        }
    }
}
