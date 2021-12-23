using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TreasuryModel.FX;
using log4net;
using System.Configuration;

namespace TreasuryModel.Api
{
    public class BOTExchangeRate 
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BOTExchangeRate));
        private string connectionstring = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        public List<FXRateDataDetail> GetExchangeRate(List<FXRateDataDetail> detail)
        {
            try
            {
                Log.Debug("Start Insert Exchange Rate");
                if (detail != null && detail[0].period!="")
                {                        
                    int sysid = KKB_TB_TNH_CUR_EXCH_RATE.insertKKB_TB_TNH_CUR_EXCH_RATE(connectionstring, DateTime.Parse(detail[0].period), "SC", "");
                    if (sysid > 0)
                        KKB_TB_TND_CUR_EXCH_RATE.insertKKB_TB_TND_CUR_EXCH_RATE(connectionstring, sysid, detail);
                }
                return detail;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
    }
}
