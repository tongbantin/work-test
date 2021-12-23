using System;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Data.Collateral
{

    public class BALANCE_WITH_SPOT
    {
        public int? BALANCE_ID { get; set; }
        public string TRANS_DATE { get; set; }
        public string CTR_NAME { get; set; }
        public double? RATE_SPOT { get; set; }
        public double? AR_RATE_SPOT_AVG { get; set; }            //Start AR
        public double? AR_USD_MARGIN { get; set; }
        public double? AR_USD_BALANCE { get; set; }
        public double? AR_THB_MARGIN { get; set; }
        public double? AR_THB_BALANCE { get; set; }
        public double? AR_THB_BALANCE_ACCOUNT { get; set; }
        public double? AP_RATE_SPOT_AVG { get; set; }            //Start AP
        public double? AP_USD_MARGIN { get; set; }
        public double? AP_USD_BALANCE { get; set; }
        public double? AP_THB_MARGIN { get; set; }
        public double? AP_THB_BALANCE { get; set; }
        public double? AP_THB_BALANCE_ACCOUNT { get; set; }
        public double? INT_RATE { get; set; }                    //Start Interest
        public double? INT_USD_AR_ACCUMULATE { get; set; }
        public double? INT_USD_AP_ACCUMULATE { get; set; }
        public double? INT_THB_AR_ACCUMULATE { get; set; }
        public double? INT_THB_AP_ACCUMULATE { get; set; }

        public static BALANCE_WITH_SPOT CreateMockup(int i)
        {
            Random rand = new Random(1);
            BALANCE_WITH_SPOT result = new BALANCE_WITH_SPOT();
            result.TRANS_DATE = new DateTime(DateTime.Today.Year, DateTime.Today.Month, i).ToString("yyyy-MM-dd");
            if (i % 2 == 0)
                result.BALANCE_ID = i;
            result.CTR_NAME = "CTR 01";
            result.RATE_SPOT = rand.NextDouble() * 31.2318;
            result.AR_RATE_SPOT_AVG = rand.NextDouble();
            result.AR_USD_MARGIN = rand.NextDouble();
            result.AR_USD_BALANCE = rand.NextDouble();
            result.AR_THB_MARGIN = rand.NextDouble();
            result.AR_THB_BALANCE = rand.NextDouble();
            result.AR_THB_BALANCE_ACCOUNT = rand.NextDouble();
            result.AP_RATE_SPOT_AVG = rand.NextDouble();
            result.AP_USD_MARGIN = rand.NextDouble();
            result.AP_USD_BALANCE = rand.NextDouble();
            result.AP_THB_MARGIN = rand.NextDouble();
            result.AP_THB_BALANCE = rand.NextDouble();
            result.AP_THB_BALANCE_ACCOUNT = rand.NextDouble();
            result.INT_RATE = rand.NextDouble();
            result.INT_USD_AR_ACCUMULATE = rand.NextDouble();
            result.INT_USD_AP_ACCUMULATE = rand.NextDouble();
            result.INT_THB_AR_ACCUMULATE = rand.NextDouble();
            result.INT_THB_AP_ACCUMULATE = rand.NextDouble();
            return result;
        }
    }

    public class BALANCE_WITH_DEFAULT
    {
        public int? BALANCE_ID { get; set; }
        public string TRANS_DATE { get; set; }
        public string CTR_NAME { get; set; }
        public double? AR_THB_MARGIN { get; set; }          //Start AR
        public double? AR_THB_BALANCE { get; set; }
        public double? AP_THB_MARGIN { get; set; }          //Start AP
        public double? AP_THB_BALANCE { get; set; }
        public double? INT_RATE { get; set; }               //Start Int
        public double? INT_DAY { get; set; }
        public double? INT_THB_AR_ACCUMULATE { get; set; }
        public double? INT_THB_AP_ACCUMULATE { get; set; }
        public double? INT_TOTAL { get; set; }

        public static BALANCE_WITH_DEFAULT CreateMockup(int i)
        {
            Random rand = new Random(1);
            BALANCE_WITH_DEFAULT result = new BALANCE_WITH_DEFAULT();
            if (i % 2 == 0)
                result.BALANCE_ID = i;

            result.TRANS_DATE = new DateTime(DateTime.Today.Year, DateTime.Today.Month, i).ToString("yyyy-MM-dd");
            result.CTR_NAME = "CTR 02";
            result.AR_THB_MARGIN = rand.NextDouble();
            result.AR_THB_BALANCE = rand.NextDouble();
            result.AP_THB_MARGIN = rand.NextDouble();
            result.AP_THB_BALANCE = rand.NextDouble();
            result.INT_RATE = rand.NextDouble();
            result.INT_DAY = rand.NextDouble();
            result.INT_THB_AR_ACCUMULATE = rand.NextDouble();
            result.INT_THB_AP_ACCUMULATE = rand.NextDouble();
            result.INT_TOTAL = rand.NextDouble();
            return result;
        }
    }

    public class BALANCE_MOVEMENT
    {
        public int? BALANCE_ID { get; set; }
        public string TRANS_DATE { get; set; }
        public string CTR_NAME { get; set; }
        public double? AR_MARGIN { get; set; }
        public double? AP_MARGIN { get; set; }
    }


    public static partial class Extensions
    {

        public static void ToOracleParameters(this BALANCE_MOVEMENT record, ref Oracle command)
        {
        }
        public static BALANCE_MOVEMENT ToBALANCE_MOVEMENT(this DataRow row)
        {
            throw new NotImplementedException();
        }

    }

}

