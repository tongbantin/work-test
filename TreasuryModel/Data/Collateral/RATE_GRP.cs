using System;
using System.Collections.Generic;
using System.Linq;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Data.Collateral
{
    public class RATE_GRP : BaseModel
    {

        public int? RATE_GRP_ID { get; set; }
        public string RATE_GRP_NAME { get; set; }
        public RATE_GRP()
        {

        }

        public static RATE_GRP CreateMockup(int itemNo)
        {
            RATE_GRP item = new RATE_GRP();
            item.RATE_GRP_ID = itemNo;
            item.RATE_GRP_NAME = string.Format("GRP {0}", itemNo);
            item.CREATED_DATE = DateTime.Today.ToString("yyyy-MM-dd");
            item.CREATED_BY = "xercise";
            return item;
        }

    }

    public static partial class Extensions
    {

        public static void ToOracleParameters(this RATE_GRP record, ref Oracle command)
        {
            command.setIntegerInputParameter("IN_RATE_GRP_ID", record.RATE_GRP_ID);
            command.setVarCharInputParameter("IN_RATE_GRP_NAME", record.RATE_GRP_NAME);

            command.setVarCharInputParameter("IN_CREATED_BY", record.CREATED_BY);
            command.setVarCharInputParameter("IN_CREATED_DATE", record.CREATED_DATE);
            command.setVarCharInputParameter("IN_UPDATED_BY", record.UPDATED_BY);
            command.setVarCharInputParameter("IN_UPDATED_DATE", record.UPDATED_DATE);
        }
        public static RATE_GRP ToRATE_GRP(this DataRow row)
        {
            RATE_GRP result = new RATE_GRP();
            if (row["RATE_GRP_ID"] != DBNull.Value)
                result.RATE_GRP_ID = Convert.ToInt32(row["RATE_GRP_ID"]);
            result.RATE_GRP_NAME = row["RATE_GRP_NAME"].ToString();

            result.CREATED_BY = row["CREATED_BY"].ToString();
            result.CREATED_DATE = row["CREATED_DATE"].ToString();
            result.UPDATED_BY = row["UPDATED_BY"].ToString();
            result.UPDATED_DATE = row["UPDATED_DATE"].ToString();
            return result;
        }

    }

}
