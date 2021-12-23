using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace TreasuryModel.Repository.OPXRPT
{
    public class BaseExcelReader<T>
    {
        protected static ILog Log = log4net.LogManager.GetLogger(typeof(T));
        protected const string excelPath = @"D:\X\Collateral\12-20Feb18\12-20Feb18\";
        protected const string excelFile = @"CSA+option(v1.7)_12022018.xlsm";
    }
}
