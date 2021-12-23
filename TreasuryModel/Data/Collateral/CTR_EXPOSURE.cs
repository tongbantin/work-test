using System;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
namespace TreasuryModel.Data.Collateral
{
    public class CTR_EXPOSURE : CTR_SETTING
    {
        public double? RATE { get; set; }
        public string TRANS_DATE { get; set; }
        public double? TOTAL_AMT_KKB { get; set; }
        public double? TOTAL_AMT_CTR { get; set; }
        public int? TOTAL_CNT_KKB { get; set; }
        public int? TOTAL_CNT_CTR { get; set; }

        public CTR_EXPOSURE() : base()
        {

        }

        public static new CTR_EXPOSURE CreateMockup(int itemNo)
        {
        
            CTR_EXPOSURE result = CTR_SETTING.CreateMockup(itemNo).ToDerived();
            result.TRANS_DATE = String.Format("{0:yyyy-MM-dd}", DateTime.Today);

            Random ranAMT = new Random();
            Random ranCNT = new Random();
            result.RATE = ranAMT.NextDouble();
            result.TOTAL_AMT_KKB = ranAMT.NextDouble() * 99999;
            result.TOTAL_AMT_CTR = ranAMT.NextDouble() * 99999;

            result.TOTAL_CNT_KKB = ranCNT.Next(1, 50);
            result.TOTAL_CNT_CTR = ranCNT.Next(1, 50);

            return result;
        }
    }

   
    public static partial class Extension
    {
        public static CTR_EXPOSURE ToDerived(this CTR_SETTING objBase)
        {
            CTR_EXPOSURE result = new CTR_EXPOSURE();
            result.SETTING_ID = objBase.SETTING_ID;
            result.CTR_NAME = objBase.CTR_NAME;
            result.CONDITION = objBase.CONDITION;
            result.RATE_GRP_NAME = objBase.RATE_GRP_NAME;
            result.CURRENCY = objBase.CURRENCY;
            result.TH_CTR = objBase.TH_CTR;
            result.TH_KKB = objBase.TH_KKB;
            result.MTA_CTR = objBase.MTA_CTR;
            result.MTA_KKB = objBase.MTA_KKB;
            result.CTR_ROUNDING = objBase.CTR_ROUNDING;
            result.CAL_AGENT_KKB = objBase.CAL_AGENT_KKB;
            result.CAL_AGENT_CTR = objBase.CAL_AGENT_CTR;
            result.NOTICE_FREQ_DAILY = objBase.NOTICE_FREQ_DAILY;
            result.NOTICE_FREQ_WEEKLY = objBase.NOTICE_FREQ_WEEKLY;
            result.NOTICE_FREQ_OTHER = objBase.NOTICE_FREQ_OTHER;
            result.RESOLUTION_TIME = objBase.RESOLUTION_TIME;
            result.ATTENTION = objBase.ATTENTION;
            result.TEL = objBase.TEL;
            result.FAX = objBase.FAX;
            result.EMAIL = objBase.EMAIL;
            result.SSI_KKB = objBase.SSI_KKB;
            result.SSI_CTR = objBase.SSI_CTR;
            result.MAR_SETTLE_DATE = objBase.MAR_SETTLE_DATE;
            result.INT_SETTLE_DATE = objBase.INT_SETTLE_DATE;
            result.INDEP_BAL_KKB = objBase.INDEP_BAL_KKB;
            result.INDEP_BAL_CTR = objBase.INDEP_BAL_CTR;
            result.BEGIN_DATE = objBase.BEGIN_DATE;
            result.END_DATE = objBase.END_DATE;
            result.CREATED_DATE = objBase.CREATED_DATE;
            result.CREATED_BY = objBase.CREATED_BY;
            result.UPDATED_DATE = objBase.UPDATED_DATE;
            result.UPDATED_BY = objBase.UPDATED_BY;

            return result;
        }

        public static void ToOracleParameters(this CTR_EXPOSURE record, ref Oracle command)
        {

        }
        public static CTR_EXPOSURE ToCTR_EXPOSURE(this DataRow row)
        {
            CTR_EXPOSURE result = null;
            try
            {
                result = new CTR_EXPOSURE();
                result = row.ToCTR_SETTING().ToDerived();

                result.RATE = Convert.ToDouble(row["RATE"]); ;
                result.TRANS_DATE = row["TRANS_DATE"].ToString();
                if (row["TOTAL_AMT_KKB"] != DBNull.Value) result.TOTAL_AMT_KKB = Convert.ToDouble(row["TOTAL_AMT_KKB"]);
                if (row["TOTAL_AMT_CTR"] != DBNull.Value) result.TOTAL_AMT_CTR = Convert.ToDouble(row["TOTAL_AMT_CTR"]);
                if (row["TOTAL_CNT_KKB"] != DBNull.Value) result.TOTAL_CNT_KKB = Convert.ToInt32(row["TOTAL_CNT_KKB"]);
                if (row["TOTAL_CNT_CTR"] != DBNull.Value) result.TOTAL_CNT_CTR = Convert.ToInt32(row["TOTAL_CNT_CTR"]);
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}
