using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using System.Globalization;
using log4net;

namespace TreasuryModel.FX
{
    public class KKB_TB_TNH_CUR_EXCH_RATE
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(KKB_TB_TNH_CUR_EXCH_RATE));

        /* ***********Insert TNH************* */        
        public static int insertKKB_TB_TNH_CUR_EXCH_RATE(string connectionstring, DateTime period, string status, string remark)
        { 
            int maxOrder = 0;
            int syshdExchRateID = 0;
            int row = 0;
            Oracle o = Oracle.getInstance(connectionstring);
            try
            {
                string readsql = "SELECT OPICINF.SYSHD_EXCH_RATE_ID_SEQ.NEXTVAL AS SEQ,ORDER_NO"
                                + " FROM("
                                    + "SELECT MAX(ORDER_NO) AS ORDER_NO"
                                    + " FROM OPICINF.KKB_TB_TNH_CUR_EXCH_RATE WHERE TRUNC(TXN_DATE) = TRUNC(SYSDATE))";


                string sql = "Insert into OPICINF.KKB_TB_TNH_CUR_EXCH_RATE values(:SYSHD_EXCH_RATE_ID, trunc(sysdate), :TXN_STATUS, :ORDER_NO, to_char(sysdate,'yyyymmddhh24miss'), to_date(:AS_OF_DATE,'ddmmyyyy'), -999,sysdate,-999,sysdate, :REMARK)";
                Log.Debug("Log Read & Insert SQL " + sql);
                o.openConnection();
                o.createCommand(readsql, CommandType.Text);
                DataTable dtResult = o.executeReaderToDT("LAST_ORDER");
                syshdExchRateID = Convert.ToInt32(dtResult.Rows[0]["SEQ"].ToString());
                if (dtResult == null)
                    maxOrder = 1;
                else
                {
                    if (dtResult.Rows[0]["ORDER_NO"].ToString().Trim().Equals(""))
                        maxOrder = 1;
                    else
                        maxOrder = Convert.ToInt32(dtResult.Rows[0]["ORDER_NO"].ToString()) + 1;
                }

                o.createTransaction(sql, CommandType.Text);
                o.setInputParameter("SYSHD_EXCH_RATE_ID", syshdExchRateID);
                o.setInputParameter("TXN_STATUS", status);
                o.setInputParameter("ORDER_NO", maxOrder);
                o.setInputParameter("AS_OF_DATE", period.ToString("ddMMyyyy", CultureInfo.CreateSpecificCulture("en-US")));
                o.setInputParameter("REMARK", remark); 
                row = o.executeNonQuery();
                return syshdExchRateID;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_TNH_CUR_EXCH_RATE Error-----");
                Log.Error(ex.Message);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_TNH_CUR_EXCH_RATE Finish-----");
                o.closeConnection();
            }
            
        }
    }
}
