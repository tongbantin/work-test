using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using TreasuryModel.Master;
using log4net;

namespace TreasuryModel.Master
{
    public class Model_MS_BOND
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_BOND));
        public static DataTable getBondByDealNo(string ConnectionString, String DealNo)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "SELECT DEAL.* , TRIM(PUBDATA) AS PAYTYPE  FROM ( Select DealNo, FaceAmt, To_CHAR(Dealdate,'DD/MM/YYYY') DealDate, TO_CHAR(SettDate,'DD/MM/YYYY') SETTDATE, FACEAMT - (SELECT NVL(SUM(NVL(FACEAMT,0)),0) FROM OPICINF.KKB_TB_MS_PTITRANS_LOG WHERE TRIM(RELATEREF)=TRIM(SPSH.DEALNO)) as TOTAL From OPICS.SPSH Where RevDate is null And Dealno like '" + DealNo + "%' )DEAL JOIN OPICS.PUBS ON TRIM(SUBSTR(PUBS.PUBNAME,3,8)) = '"+DealNo+"'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable getSecID(string p, string secid)
        {
            throw new NotImplementedException();
        }
    }
}
