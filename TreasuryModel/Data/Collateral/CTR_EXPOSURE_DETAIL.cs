using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Data.Collateral
{
    public class CTR_EXPOSURE_DETAIL : BaseModel
    {

        public int SETTING_ID { get; set; }
        public int TRANS_ID { get; set; }
        public string TRANS_NAME { get; set; }
        public int SORT_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public int? EXPOSURE_ID { get; set; }
        public string TRANS_DATE { get; set; }
        public double? AMT_KKB { get; set; }
        public double? CNT_KKB { get; set; }
        public string CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        
        public CTR_EXPOSURE_DETAIL()
        {
        }
    }

    public static partial class Extension
    {
        public static void ToOracleParameters(this CTR_EXPOSURE_DETAIL record, ref Oracle command)
        {
            command.setIntegerInputParameter("IN_EXPOSURE_ID", record.EXPOSURE_ID);
            command.setIntegerInputParameter("IN_SETTING_ID", record.SETTING_ID);
            command.setVarCharInputParameter("IN_TRANS_DATE", record.TRANS_DATE);
            command.setVarCharInputParameter("IN_TRANS_NAME", record.TRANS_NAME);
            command.setDecimalInputParameter("IN_AMT_KKB", record.AMT_KKB);
            command.setIntegerInputParameter("IN_CNT_KKB", record.CNT_KKB);
            command.setVarCharInputParameter("IN_UPDATED_BY", record.UPDATED_BY);
        }
        public static CTR_EXPOSURE_DETAIL ToCTR_EXPOSURE_DETAIL(this DataRow row)
        {
            CTR_EXPOSURE_DETAIL result = null;
            try
            {
                result = new CTR_EXPOSURE_DETAIL();

                result.SETTING_ID = (int)row["SETTING_ID"].ToIntegerData();
                result.TRANS_ID = (int)row["TRANS_ID"].ToIntegerData();
                result.TRANS_NAME = row["TRANS_NAME"].ToString();
                result.SORT_NO = (int)row["SORT_NO"].ToIntegerData();
                result.DESCRIPTION = row["DESCRIPTION"].ToString();
                result.EXPOSURE_ID = row["EXPOSURE_ID"].ToIntegerData();
                result.TRANS_DATE = row["TRANS_DATE"].ToStringDateISO();
                result.AMT_KKB = row["AMT_KKB"].ToDoubleData();
                result.CNT_KKB = row["CNT_KKB"].ToIntegerData();

                result.CREATED_DATE = row["CREATED_DATE"].ToStringDateISO();
                result.CREATED_BY = row["CREATED_BY"].ToStringData();
                result.UPDATED_DATE = row["UPDATED_DATE"].ToStringDateISO();
                result.UPDATED_BY = row["UPDATED_BY"].ToStringData();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
    }

}
