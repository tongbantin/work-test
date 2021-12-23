using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace KKB.Treasury.TreasuryCommon.Common
{
    public static class Extensions
    {
        public static string ToStringDateISO(this object obj)
        {
            string result = null;
            if (obj != DBNull.Value)
            {
                DateTime objDate = DateTime.ParseExact(obj.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                result = objDate.ToString("yyyy-MM-dd");
            }

            return result;
        }
        public static string ToStringData(this object obj)
        {
            string result = null;
            if (obj != DBNull.Value)
                result = obj.ToString();

            return result;
        }
        public static double? ToDoubleData(this object obj)
        {
            double? result = null;
            if (obj != DBNull.Value)
                result = Convert.ToDouble(obj);

            return result;
        }
        public static int? ToIntegerData(this object obj)
        {
            int? result = null;
            if (obj != DBNull.Value)
                result = Convert.ToInt32(obj);

            return result;
        }
    }
}
