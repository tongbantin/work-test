using System;
using System.Collections.Generic;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
namespace TreasuryModel.Data.Collateral
{
    public class CTR_SETTING : BaseModel
    {

		public int? SETTING_ID { get; set; }
		public string CTR_NAME { get; set; }
        public virtual IList<TRANS_TYPE> CONDITION { get; set; }
        public string RATE_GRP_NAME { get; set; }
        public string CURRENCY { get; set; }
        public double? TH_CTR { get; set; }
        public double? TH_KKB { get; set; }
        public double? MTA_CTR { get; set; }
        public double? MTA_KKB { get; set; }
        public double? CTR_ROUNDING { get; set; }
        public bool CAL_AGENT_KKB { get; set; }
        public bool CAL_AGENT_CTR { get; set; }
		public bool NOTICE_FREQ_DAILY { get; set; }
		public bool NOTICE_FREQ_WEEKLY { get; set; }
		public string NOTICE_FREQ_OTHER { get; set; }
		public string RESOLUTION_TIME { get; set; }
		public string ATTENTION { get; set; }
		public string TEL { get; set; }
		public string FAX { get; set; }
		public string EMAIL { get; set; }
		public string SSI_KKB { get; set; }
		public string SSI_CTR { get; set; }
		public int? RATE_GRP_ID { get; set; }
		public string MAR_SETTLE_DATE { get; set; }
		public string INT_SETTLE_DATE { get; set; }
		public double? INDEP_BAL_KKB { get; set; }
		public double? INDEP_BAL_CTR { get; set; }
		public string BEGIN_DATE { get; set; }
		public string END_DATE { get; set; }

        public CTR_SETTING()
        {
            CONDITION = new List<TRANS_TYPE>();
        }

        public static CTR_SETTING CreateMockup(int i)
        {

            string strUserBy = "xercise";
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strInfinity = "9999-12-31";
            CTR_SETTING item = new CTR_SETTING();
            item.SETTING_ID = i;
            item.CTR_NAME = string.Format("CTR {0}", i);
            item.CONDITION = TRANS_TYPE.CreateMockup();
            item.RATE_GRP_NAME = RATE_GRP.CreateMockup(i).RATE_GRP_NAME;
            item.CURRENCY = RATE_INT.CreateMockup(i).CURRENCY;
            item.TH_CTR = i;
            item.TH_KKB = i;
            item.MTA_CTR = double.Parse(string.Format("555555555.{0}", i));
            item.MTA_KKB = double.Parse(string.Format("1000.{0}", i));
            item.CTR_ROUNDING = double.Parse(string.Format("123456789.{0}", i));
            item.CAL_AGENT_KKB = true;
            item.CAL_AGENT_CTR = false;
            item.NOTICE_FREQ_DAILY = true;
            item.NOTICE_FREQ_WEEKLY = false;
            item.NOTICE_FREQ_OTHER = null;
            item.RESOLUTION_TIME = null;
            item.ATTENTION = "xercise@hotwave.god";
            item.TEL = null;
            item.FAX = null;
            item.EMAIL = "xercise@hotwave.god";
            item.SSI_KKB = string.Format("12-3456-11{0}", i);
            item.SSI_CTR = string.Format("00-3456-11{0}", i);
            item.MAR_SETTLE_DATE = strDate;
            item.INT_SETTLE_DATE = strDate;
            item.INDEP_BAL_KKB = double.Parse(string.Format("987654321.{0}", i));
            item.INDEP_BAL_CTR = double.Parse(string.Format("123456789.{0}", i));
            item.BEGIN_DATE = "2017-01-01";
            item.END_DATE = strInfinity;
            item.CREATED_DATE = strDate;
            item.CREATED_BY = strUserBy;
            item.UPDATED_DATE = null;
            item.UPDATED_BY = null;

            return item;
        }

    }
    
    public static partial class Extension
    {
        public static void ToOracleParameters(this CTR_SETTING record, ref Oracle command)
        {
            command.setVarCharInputParameter("IN_CTR_NAME", record.CTR_NAME);
            command.setDecimalInputParameter("IN_TH_CTR", record.TH_CTR);
            command.setDecimalInputParameter("IN_TH_KKB", record.TH_KKB);
            command.setDecimalInputParameter("IN_MTA_CTR", record.MTA_CTR);
            command.setDecimalInputParameter("IN_MTA_KKB", record.MTA_KKB);
            command.setDecimalInputParameter("IN_CTR_ROUNDING", record.CTR_ROUNDING);
            command.setVarCharInputParameter("IN_CAL_AGENT_KKB", record.CAL_AGENT_KKB);
            command.setVarCharInputParameter("IN_CAL_AGENT_CTR", record.CAL_AGENT_CTR);
            command.setVarCharInputParameter("IN_NOTICE_FREQ_DAILY", record.NOTICE_FREQ_DAILY);
            command.setVarCharInputParameter("IN_NOTICE_FREQ_WEEKLY", record.NOTICE_FREQ_WEEKLY);
            command.setVarCharInputParameter("IN_NOTICE_FREQ_OTHER", record.NOTICE_FREQ_OTHER);
            command.setVarCharInputParameter("IN_RESOLUTION_TIME", record.RESOLUTION_TIME);
            command.setVarCharInputParameter("IN_ATTENTION", record.ATTENTION);
            command.setVarCharInputParameter("IN_TEL", record.TEL);
            command.setVarCharInputParameter("IN_FAX", record.FAX);
            command.setVarCharInputParameter("IN_EMAIL", record.EMAIL);
            command.setVarCharInputParameter("IN_SSI_KKB", record.SSI_KKB);
            command.setVarCharInputParameter("IN_SSI_CTR", record.SSI_CTR);
            command.setVarCharInputParameter("IN_RATE_GRP_ID", record.RATE_GRP_ID);
            command.setVarCharInputParameter("IN_MAR_SETTLE_DATE", record.MAR_SETTLE_DATE);
            command.setVarCharInputParameter("IN_INT_SETTLE_DATE", record.INT_SETTLE_DATE);
            command.setDecimalInputParameter("IN_INDEP_BAL_KKB", record.INDEP_BAL_KKB);
            command.setDecimalInputParameter("IN_INDEP_BAL_CTR", record.INDEP_BAL_CTR);
            command.setVarCharInputParameter("IN_BEGIN_DATE", record.BEGIN_DATE);
            command.setVarCharInputParameter("IN_END_DATE", record.END_DATE);
            command.setVarCharInputParameter("IN_CREATED_BY", record.CREATED_BY);
        }
        public static CTR_SETTING ToCTR_SETTING(this DataRow row)
        {
            CTR_SETTING result = null;
            try
            {
                result = new CTR_SETTING();
                result.SETTING_ID = Convert.ToInt32(row["SETTING_ID"].ToString()); //key should be have
                result.CTR_NAME = row["CTR_NAME"].ToString();

                if (row["TH_CTR"] != DBNull.Value)
                    result.TH_CTR = Convert.ToDouble(row["TH_CTR"].ToString());
                if (row["TH_KKB"] != DBNull.Value)
                    result.TH_KKB = Convert.ToDouble(row["TH_KKB"].ToString());

                if (row["MTA_CTR"] != DBNull.Value)
                    result.MTA_CTR = Convert.ToDouble(row["MTA_CTR"].ToString());
                if (row["MTA_KKB"] != DBNull.Value)
                    result.MTA_KKB = Convert.ToDouble(row["MTA_KKB"].ToString());

                if (row["CTR_ROUNDING"] != DBNull.Value)
                    result.CTR_ROUNDING = Convert.ToDouble(row["CTR_ROUNDING"].ToString());

                if (row["CAL_AGENT_KKB"] != DBNull.Value)
                    result.CAL_AGENT_KKB = Convert.ToBoolean(row["CAL_AGENT_KKB"]);
                if (row["CAL_AGENT_CTR"] != DBNull.Value)
                    result.CAL_AGENT_CTR = Convert.ToBoolean(row["CAL_AGENT_CTR"]);

                if (row["NOTICE_FREQ_DAILY"] != DBNull.Value)
                    result.NOTICE_FREQ_DAILY = Convert.ToBoolean(row["NOTICE_FREQ_DAILY"]);
                if (row["NOTICE_FREQ_WEEKLY"] != DBNull.Value)
                    result.NOTICE_FREQ_WEEKLY = Convert.ToBoolean(row["NOTICE_FREQ_WEEKLY"]);
                result.NOTICE_FREQ_OTHER = row["NOTICE_FREQ_OTHER"].ToString();
                result.RESOLUTION_TIME = row["RESOLUTION_TIME"].ToString();

                //Contact person (counter party)
                result.ATTENTION = row["ATTENTION"].ToString();
                result.TEL = row["TEL"].ToString();
                result.FAX = row["FAX"].ToString();
                result.EMAIL = row["EMAIL"].ToString();

                //Book accounts
                result.SSI_KKB = row["SSI_KKB"].ToString();
                result.SSI_CTR = row["SSI_CTR"].ToString();

                //Rate group
                if (row["RATE_GRP_ID"] != DBNull.Value)
                    result.RATE_GRP_ID = Convert.ToInt32(row["RATE_GRP_ID"].ToString());
                result.RATE_GRP_NAME = row["RATE_GRP_NAME"].ToString();

                //Settle date
                if (row["MAR_SETTLE_DATE"] != DBNull.Value) 
                    result.MAR_SETTLE_DATE = string.Format("{0:yyyy-MM-dd}", row["MAR_SETTLE_DATE"]);
                if (row["INT_SETTLE_DATE"] != DBNull.Value)
                    result.INT_SETTLE_DATE = string.Format("{0:yyyy-MM-dd}", row["INT_SETTLE_DATE"]);

                //Independent balances
                if (row["INDEP_BAL_KKB"] != DBNull.Value)
                    result.INDEP_BAL_KKB = Convert.ToDouble(row["INDEP_BAL_KKB"]);
                if (row["INDEP_BAL_CTR"] != DBNull.Value)
                    result.INDEP_BAL_CTR = Convert.ToDouble(row["INDEP_BAL_CTR"]);

                //delimit of these config
                if (row["BEGIN_DATE"] != DBNull.Value)
                    result.BEGIN_DATE = string.Format("{0:yyyy-MM-dd}", row["BEGIN_DATE"]);
                if (row["END_DATE"] != DBNull.Value)
                    result.END_DATE = string.Format("{0:yyyy-MM-dd}", row["END_DATE"]);

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return result;
        }
    }
}
