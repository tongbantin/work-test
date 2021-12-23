using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TreasuryModel.Loan;
using log4net;
using System.Configuration;

namespace TreasuryModel.Api
{
    public class BOTLoanInterestRate  
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BOTLoanInterestRate));
        private string connectionstring = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        public List<LoanIRDataDetail> GetLoanInterestRate(List<LoanIRDataDetail> detail)
        {
            try
            {
                if (detail != null)
                    LOAN_INTEREST_RATE.insertLOAN_INTEREST_RATE(connectionstring, detail);
                return detail;
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
    }
}
