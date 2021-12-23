using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasuryModel.Api;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Loan
{
    public class LOAN_INTEREST_RATE
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(LOAN_INTEREST_RATE));

        public static bool insertLOAN_INTEREST_RATE(string ConnectionString, List<LoanIRDataDetail> datadetail)
        {
            Log.Info("-----KKB_TB_CBS_LOAN_INTERESTRATE Start insert-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Insert into OPICINF.KKB_TB_CBS_LOAN_INTERESTRATE(REFGROUP, PERIOD, BANK_TYPE_NAME_TH, BANK_TYPE_NAME_ENG, BANK_NAME_TH, BANK_NAME_ENG, MOR, MLR, MRR, CEILING_RATE ,DEFAULT_RATE, CREDITCARD_MIN, CREDITCARD_MAX, LOG_INSERTDATE) ";
                sql += "values(:REFGROUP, :PERIOD, :BANK_TYPE_NAME_TH, :BANK_TYPE_NAME_ENG, :BANK_NAME_TH, :BANK_NAME_ENG, :MOR, :MLR, :MRR, :CEILING_RATE, :DEFAULT_RATE, :CREDITCARD_MIN, :CREDITCARD_MAX, SYSDATE)";
                o.openConnection();
                o.beginTransaction();
                string refgroup = DateTime.Now.ToString("yyyyMMddhhmmsss");
                for (int i = 0; i < datadetail.Count; i++)
                {
                    try
                    {
                        Log.Debug("Start Write Bank Loan IR : " + datadetail[i].bank_name_eng);
                        o.createTransaction(sql, CommandType.Text);
                        o.setInputParameter("REFGROUP", refgroup);
                        o.setInputParameter("PERIOD", datadetail[i].period);
                        o.setNCharInputParameter("BANK_TYPE_NAME_TH", datadetail[i].bank_type_name_th);
                        o.setInputParameter("BANK_TYPE_NAME_ENG", datadetail[i].bank_type_name_eng);
                        o.setNCharInputParameter("BANK_NAME_TH", datadetail[i].bank_name_th);
                        o.setInputParameter("BANK_NAME_ENG", datadetail[i].bank_name_eng);
                        o.setInputParameter("MOR", datadetail[i].mor);
                        o.setInputParameter("MLR", datadetail[i].mlr);
                        o.setInputParameter("MRR", datadetail[i].mrr);
                        o.setInputParameter("CEILING_RATE", datadetail[i].ceiling_rate);
                        o.setInputParameter("DEFAULT_RATE", datadetail[i].default_rate);
                        o.setInputParameter("CREDITCARD_MIN", datadetail[i].creditcard_min);
                        o.setInputParameter("CREDITCARD_MAX", datadetail[i].creditcard_max);
                        row = o.executeNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error Bank Loan IR " + datadetail[i].bank_name_eng + " : " + ex.Message);
                    }
                }

                o.commitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_CBS_LOAN_INTERESTRATE Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_LOAN_INTERESTRATE Finish-----");
                o.closeConnection();
            }

        }
    }
}
