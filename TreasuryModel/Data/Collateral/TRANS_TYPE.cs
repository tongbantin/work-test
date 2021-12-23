using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;

namespace TreasuryModel.Data.Collateral
{
    public class TRANS_TYPE : BaseModel
    {
        public int? SETTING_ID { get; set; }
        public int? TRANS_ID { get; set; }
        public string TRANS_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public double? AMT { get; set; }
        public int? CNT { get; set; }
        public bool ENABLED { get; set; }
        public int? SORT_NO { get; set; }

        public static IList<TRANS_TYPE> CreateMockup()
        {
            IList<TRANS_TYPE> items = new List<TRANS_TYPE>();

            for (int i = 0; i < 6; i++)
            {
                Random rand = new Random(100);
                TRANS_TYPE item = new TRANS_TYPE();
                item.TRANS_ID = i;
                item.AMT = rand.NextDouble() * 9999;
                item.CNT = i;
                item.SORT_NO = i;
                item.ENABLED = (i % 2) == 0;
                item.TRANS_NAME = string.Format("TRANS {0}", i);
                items.Add(item);

            }
            return items;
        }
    }


    public static partial class Extensions
    {

        public static void ToOracleParameters(this TRANS_TYPE record, ref Oracle command)
        {

            command.setInputParameter("IN_SETTING_ID", record.SETTING_ID);
            command.setInputParameter("IN_TRANS_ID", record.TRANS_ID);
            command.setInputParameter("IN_TRANS_NAME", record.TRANS_NAME);
            command.setInputParameter("IN_DESCRIPTION", record.DESCRIPTION);
            //command.setInputParameter("IN_AMT_KKB", record.AMT);
            //command.setInputParameter("IN_CNT_KKB", record.CNT);
            //command.setInputParameter("IN_ENABLED", record.ENABLED);
            command.setInputParameter("IN_SORT_NO", record.SORT_NO);

            //command.setInputParameter("IN_CREATED_BY", record.CREATED_BY);
            command.setInputParameter("IN_UPDATED_BY", record.UPDATED_BY);

        }
        public static TRANS_TYPE ToTRANS_TYPE(this DataRow row)
        {
            TRANS_TYPE result = new TRANS_TYPE();

            if (row["SETTING_ID"] != DBNull.Value) result.SETTING_ID = Convert.ToInt32(row["SETTING_ID"]);
            if (row["TRANS_ID"] != DBNull.Value) result.TRANS_ID = Convert.ToInt32(row["TRANS_ID"]);

            result.TRANS_NAME = row["TRANS_NAME"].ToString();
            result.DESCRIPTION = row["DESCRIPTION"].ToString();

            if (row["AMT"] != DBNull.Value) result.AMT = Convert.ToDouble(row["AMT"]);
            if (row["CNT"] != DBNull.Value) result.CNT = Convert.ToInt32(row["CNT"]);

            result.ENABLED = Convert.ToBoolean(row["ENABLED"]);

            if (row["SORT_NO"] != DBNull.Value) result.SORT_NO = Convert.ToInt32(row["SORT_NO"]);

            if (row["CREATED_BY"] != DBNull.Value) result.CREATED_BY = row["CREATED_BY"].ToString();
            if (row["CREATED_DATE"] != DBNull.Value) result.CREATED_DATE = row["CREATED_DATE"].ToString();
            if (row["UPDATED_BY"] != DBNull.Value) result.UPDATED_BY = row["UPDATED_BY"].ToString();
            if (row["UPDATED_DATE"] != DBNull.Value) result.UPDATED_DATE = row["UPDATED_DATE"].ToString();

            return result;
        }

    }
}
