using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;
using System.Collections;
namespace TreasuryModel.Data
{
    public class RepoRefYield
    {
        public int Records { get; set; }
        public DateTime? DATA_DATE { get; set; }
        public String SYMBOL { get; set; }
        public DateTime? MATURITY_DATE { get; set; }
        public Decimal? AVG_BIDDING  { get; set; }
        public Decimal? GOVT_INTERPOLATED_YIELD { get; set; }
        public Decimal? TTM_YEAR { get; set; }
        public Decimal? SPREAD_QUOTED_DATE { get; set; }
        public Decimal? SPREAD { get; set; }
        public Decimal? REFERENCE_YIELD { get; set; }
        public DateTime? SETTLEMENT_DATE { get; set; }
        public Decimal? AI_PERCENTAGE { get; set; }
        public Decimal? GROSS_PRICE { get; set; }
        public Decimal? CLEAN_PRICE { get; set; }
        public Decimal? MODIFIED_DURATION { get; set; }
        public Decimal? CONVEXITY { get; set; }
        public Decimal? INDEX_RATIO { get; set; }

        private static ILog Log = log4net.LogManager.GetLogger(typeof(RepoRefYield));

        public ArrayList save(string ConnectionString, List<RepoRefYield> repo)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();

                int i = 0;

                string del = "Delete From OPICINF.KKB_TRO_TB_REPO_REF_YIELD";
                o.createCommand(del, CommandType.Text);
                o.executeNonQuery();

                DateTime row = (DateTime)repo[0].DATA_DATE;
                del = "Delete From OPICINF.KKB_TRO_TB_REPO_REF_YIELD_HIST WHERE DATA_DATE=TO_DATE('" + row.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
                o.createCommand(del, CommandType.Text);
                o.executeNonQuery();

                foreach (RepoRefYield r in repo)
                {
                    try
                    {
                        string sql = "Insert Into OPICINF.KKB_TRO_TB_REPO_REF_YIELD(DATA_DATE,  SYMBOL , MATURITY_DATE, AVG_BIDDING, GOVT_INTERPOLATED_YIELD, TTM_YEAR, SPREAD_QUOTED_DATE, SPREAD, REFERENCE_YIELD, SETTLEMENT_DATE, AI_PERCENTAGE, GROSS_PRICE, CLEAN_PRICE, MODIFIED_DURATION, CONVEXITY, INDEX_RATIO) ";
                        sql += " Values(TO_DATE(:DATA_DATE,'DD/MM/YYYY'), :SYMBOL, TO_DATE(:MATURITY_DATE,'DD/MM/YYYY'), :AVG_BIDDING, :GOVT_INTERPOLATED_YIELD, :TTM_YEAR, :SPREAD_QUOTED_DATE, :SPREAD, :REFERENCE_YIELD, TO_DATE(:SETTLEMENT_DATE,'DD/MM/YYYY'), :AI_PERCENTAGE, :GROSS_PRICE, :CLEAN_PRICE, :MODIFIED_DURATION, :CONVEXITY, :INDEX_RATIO)";
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);                        
                        o.setVarCharInputParameter("DATA_DATE", DateTime.Parse(r.DATA_DATE.ToString()).ToString("dd/MM/yyyy"));
                        o.setVarCharInputParameter("SYMBOL", r.SYMBOL);
                        o.setVarCharInputParameter("MATURITY_DATE", DateTime.Parse(r.MATURITY_DATE.ToString()).ToString("dd/MM/yyyy"));
                        o.setDecimalInputParameter("AVG_BIDDING", r.AVG_BIDDING);
                        o.setDecimalInputParameter("GOVT_INTERPOLATED_YIELD", r.GOVT_INTERPOLATED_YIELD);
                        o.setDecimalInputParameter("TTM_YEAR", r.TTM_YEAR);
                        o.setDecimalInputParameter("SPREAD_QUOTED_DATE", r.SPREAD_QUOTED_DATE);
                        o.setDecimalInputParameter("SPREAD", r.SPREAD);
                        o.setDecimalInputParameter("REFERENCE_YIELD", r.REFERENCE_YIELD);
                        o.setVarCharInputParameter("SETTLEMENT_DATE", DateTime.Parse(r.SETTLEMENT_DATE.ToString()).ToString("dd/MM/yyyy"));
                        o.setDecimalInputParameter("AI_PERCENTAGE", r.AI_PERCENTAGE);
                        o.setDecimalInputParameter("GROSS_PRICE", r.GROSS_PRICE);
                        o.setDecimalInputParameter("CLEAN_PRICE", r.CLEAN_PRICE);
                        o.setDecimalInputParameter("MODIFIED_DURATION", r.MODIFIED_DURATION);
                        o.setDecimalInputParameter("CONVEXITY", r.CONVEXITY);
                        o.setDecimalInputParameter("INDEX_RATIO", r.INDEX_RATIO);                        
                        o.executeNonQuery();

                        sql = "Insert Into OPICINF.KKB_TRO_TB_REPO_REF_YIELD_HIST(DATA_DATE,  SYMBOL , MATURITY_DATE, AVG_BIDDING, GOVT_INTERPOLATED_YIELD, TTM_YEAR, SPREAD_QUOTED_DATE, SPREAD, REFERENCE_YIELD, SETTLEMENT_DATE, AI_PERCENTAGE, GROSS_PRICE, CLEAN_PRICE, MODIFIED_DURATION, CONVEXITY, INDEX_RATIO) ";
                        sql += " Values(TO_DATE(:DATA_DATE,'DD/MM/YYYY'), :SYMBOL, TO_DATE(:MATURITY_DATE,'DD/MM/YYYY'), :AVG_BIDDING, :GOVT_INTERPOLATED_YIELD, :TTM_YEAR, :SPREAD_QUOTED_DATE, :SPREAD, :REFERENCE_YIELD, TO_DATE(:SETTLEMENT_DATE,'DD/MM/YYYY'), :AI_PERCENTAGE, :GROSS_PRICE, :CLEAN_PRICE, :MODIFIED_DURATION, :CONVEXITY, :INDEX_RATIO)";
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);
                        o.setVarCharInputParameter("DATA_DATE", DateTime.Parse(r.DATA_DATE.ToString()).ToString("dd/MM/yyyy"));
                        o.setVarCharInputParameter("SYMBOL", r.SYMBOL);
                        o.setVarCharInputParameter("MATURITY_DATE", DateTime.Parse(r.MATURITY_DATE.ToString()).ToString("dd/MM/yyyy"));
                        o.setDecimalInputParameter("AVG_BIDDING", r.AVG_BIDDING);
                        o.setDecimalInputParameter("GOVT_INTERPOLATED_YIELD", r.GOVT_INTERPOLATED_YIELD);
                        o.setDecimalInputParameter("TTM_YEAR", r.TTM_YEAR);
                        o.setDecimalInputParameter("SPREAD_QUOTED_DATE", r.SPREAD_QUOTED_DATE);
                        o.setDecimalInputParameter("SPREAD", r.SPREAD);
                        o.setDecimalInputParameter("REFERENCE_YIELD", r.REFERENCE_YIELD);
                        o.setVarCharInputParameter("SETTLEMENT_DATE", DateTime.Parse(r.SETTLEMENT_DATE.ToString()).ToString("dd/MM/yyyy"));
                        o.setDecimalInputParameter("AI_PERCENTAGE", r.AI_PERCENTAGE);
                        o.setDecimalInputParameter("GROSS_PRICE", r.GROSS_PRICE);
                        o.setDecimalInputParameter("CLEAN_PRICE", r.CLEAN_PRICE);
                        o.setDecimalInputParameter("MODIFIED_DURATION", r.MODIFIED_DURATION);
                        o.setDecimalInputParameter("CONVEXITY", r.CONVEXITY);
                        o.setDecimalInputParameter("INDEX_RATIO", r.INDEX_RATIO);  
                        o.executeNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error.Add(r.Records);
                        i++;
                    }
                }

                if (error.Count <= 0)
                    o.commitTransaction();
                else
                    o.rollbackTransaction();
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
                error.Add(0);
            }
            finally
            {
                o.closeConnection();
            }
            return error;
        }
    }
}
