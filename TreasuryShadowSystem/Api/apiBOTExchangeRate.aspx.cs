using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasuryModel.Api;
using log4net;

namespace TreasuryShadowSystem.API
{
    public partial class apiBOTExchangeRate : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(apiBOTExchangeRate));
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["start_period"] != null)
                    FromDateID.Value = Request.QueryString["start_period"];
                else
                    FromDateID.Value = DateTime.Now.ToString("yyyy-MM-dd");


                if (Request.QueryString["end_period"] != null)
                    ToDateID.Value = Request.QueryString["end_period"];
                else
                    ToDateID.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [System.Web.Services.WebMethod]
        public static object ExchangeRate(List<FXRateDataDetail> result)
        {
            try
            {
                Log.Debug("Start Snap Exchange Rate");
                Log.Debug("Result : " + result.Count);
                BOTExchangeRate api = new BOTExchangeRate();
                List<FXRateDataDetail> resFXRate = api.GetExchangeRate(result);
                return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}
