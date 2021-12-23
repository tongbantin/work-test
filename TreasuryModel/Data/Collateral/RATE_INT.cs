using System;
using System.Collections.Generic;
using System.Linq;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Data.Collateral
{
    public class RATE_INT : BaseModel
    {
        public int? RATE_INT_ID { get; set; }
        public int? RATE_GRP_ID { get; set; }
        public string RATE_GRP_NAME { get; set; }
        public string RATE_TYPE { get; set; }
        public string CURRENCY { get; set; }
        public double RATE { get; set; }

        public string BEGIN_DATE { get; set; }
        public string END_DATE { get; set; }


        public static RATE_INT CreateMockup(int itemNo)
        {
            string strUserBy = "xercise";
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");

            Random r = new Random();
            int rInt = r.Next(0, 100);
            int range = 100;
            Double rDouble = r.NextDouble() * range;

            RATE_INT item = new RATE_INT();
            item.RATE_INT_ID = itemNo;
            item.RATE_GRP_ID = 1;
            item.RATE_GRP_NAME = "GRP 1";
            item.RATE_TYPE = "SPOT RATE";
            item.CURRENCY = "USD";
            item.RATE = double.Parse(rDouble.ToString());
            item.BEGIN_DATE = strDate;
            item.END_DATE = new DateTime(9999, 12, 31).ToString("yyyy-MM-dd");
            item.CREATED_DATE = strDate;
            item.CREATED_BY = strUserBy;
            item.UPDATED_DATE = null;
            item.UPDATED_BY = null;
            return item;
        }

    }

    public static partial class Extensions
    {

        public static void ToOracleParameters(this RATE_INT record, ref Oracle command)
        {
            command.setIntegerInputParameter("IN_RATE_INT_ID", record.RATE_INT_ID);
            command.setIntegerInputParameter("IN_RATE_GRP_ID", record.RATE_GRP_ID);
            command.setVarCharInputParameter("IN_RATE_GRP_NAME", record.RATE_GRP_NAME);
            command.setVarCharInputParameter("IN_RATE_TYPE", record.RATE_TYPE);
            command.setVarCharInputParameter("IN_CURRENCY", record.CURRENCY);
            command.setDecimalInputParameter("IN_RATE", record.RATE);
            command.setVarCharInputParameter("IN_BEGIN_DATE", record.BEGIN_DATE);
            command.setVarCharInputParameter("IN_END_DATE", record.END_DATE);

            command.setVarCharInputParameter("IN_CREATED_BY", record.CREATED_BY);
            command.setVarCharInputParameter("IN_CREATED_DATE", record.CREATED_DATE);
            command.setVarCharInputParameter("IN_UPDATED_BY", record.UPDATED_BY);
            command.setVarCharInputParameter("IN_UPDATED_DATE", record.UPDATED_DATE);
        }
        public static RATE_INT ToRATE_INT(this DataRow row)
        {
            RATE_INT result = new RATE_INT();
            if (row["RATE_INT_ID"] != DBNull.Value)
                result.RATE_INT_ID = Convert.ToInt32(row["RATE_INT_ID"]);
            if (row["RATE_GRP_ID"] != DBNull.Value)
                result.RATE_INT_ID = Convert.ToInt32(row["RATE_GRP_ID"]);

            result.BEGIN_DATE = row["BEGIN_DATE"].ToString();
            result.END_DATE = row["END_DATE"].ToString();

            result.RATE_GRP_NAME = row["RATE_GRP_NAME"].ToString();
            result.RATE_TYPE = row["RATE_TYPE"].ToString();
            result.CURRENCY = row["CURRENCY"].ToString();
            result.RATE = Convert.ToDouble(row["RATE"]);


            result.CREATED_BY = row["CREATED_BY"].ToString();
            result.CREATED_DATE = row["CREATED_DATE"].ToString();
            result.UPDATED_BY = row["UPDATED_BY"].ToString();
            result.UPDATED_DATE = row["UPDATED_DATE"].ToString();
            return result;
        }

    }

}
