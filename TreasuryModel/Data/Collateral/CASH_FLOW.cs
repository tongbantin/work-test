using System;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
namespace TreasuryModel.Data.Collateral
{
    public class CASH_FLOW : BaseModel
    {
        public int? CASH_FLOW_ID { get; set; }
        public string SETTLE_DATE { get; set; }
        public string CTR_NAME { get; set; }
        public string CURRENCY { get; set; }
        public decimal? CCY_KK_PAY { get; set; }
        public decimal? CCY_KK_RECEIVE { get; set; }
        public decimal? OWN_CALL_VALUE { get; set; }
        public decimal? CTR_CALL_VALUE { get; set; }

        public static CASH_FLOW CreateMockup(int itemNo)
        {
            CASH_FLOW item = new CASH_FLOW();

            item.CASH_FLOW_ID = itemNo;
            item.SETTLE_DATE = DateTime.Today.ToString("yyyy-MM-dd");
            item.CTR_NAME = "CTR 1";
            item.CURRENCY = "USD";
            item.CCY_KK_PAY = itemNo;
            item.CCY_KK_RECEIVE = itemNo;
            item.OWN_CALL_VALUE = itemNo;
            item.CTR_CALL_VALUE = itemNo;

            item.CREATED_DATE = DateTime.Today.ToString("yyyy-MM-dd");
            item.CREATED_BY = "xercise";

            return item;
        }

    }

    public static partial class Extensions
    {

        public static void ToOracleParameters(this CASH_FLOW record, ref Oracle command)
        {
        }
        public static CASH_FLOW ToCASH_FLOW(this DataRow row)
        {
            throw new NotImplementedException();
        }

    }

}
