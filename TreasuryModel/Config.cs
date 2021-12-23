using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace KKB.Treasury.TreasuryModel.Configuration
{
    public class Config
    {
        public static string Owner = ConfigurationManager.AppSettings["Owner"].ToString();
    }
}
