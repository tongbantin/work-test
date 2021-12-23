using System;
using System.Configuration;
using log4net;

namespace TreasuryModel.Repository
{
    public abstract class BaseCollateralRepository<T> where T :class
    {
        protected static ILog Log = log4net.LogManager.GetLogger(typeof(T));
        protected static string ConnectionString 
        {
            get { return ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString(); }
        }
    }
}
