using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR137BOCPTYPE
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR137BOCPTYPE));

        public static DataTable getDataSet(string ConnectionString, string ReportType, string StartDate, string FromDate, string OtherBox)
        {
            Log.Info("-----Model_RPT_TR137BOCPTYPE Start-----");
            Log.Info("Data : ReportType=" + ReportType + ", OtherBox=" + OtherBox);

            DataTable dt = null;
            string sql = "";
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                if (ReportType.Equals("RelizedFX")) {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP LEFT JOIN OPICS.FXDH ON ACUP.DEALNO=FXDH.DEALNO LEFT JOIN OPICS.CUST ON FXDH.CUST=CUST.CNO WHERE GLNO IN ('445030002','445030001','445030003') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY ACUP.DEALNO";
                }
                else if (ReportType.Equals("IntPNAsset"))
                {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP LEFT JOIN OPICS.DLDT ON ACUP.DEALNO=DLDT.DEALNO LEFT JOIN OPICS.CUST ON DLDT.CNO=CUST.CNO WHERE GLNO IN ('41000101','41000103','41000104','41000108','41000109','41000110','41000111') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY ACUP.DEALNO";
                }
                else if (ReportType.Equals("IntPNLia"))
                {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP LEFT JOIN OPICS.DLDT ON ACUP.DEALNO=DLDT.DEALNO LEFT JOIN OPICS.CUST ON DLDT.CNO=CUST.CNO WHERE GLNO IN ('50200103','50200104','50200108') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY ACUP.DEALNO";
                }
                else if (ReportType.Equals("IntRepoAsset"))
                {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP LEFT JOIN OPICS.RPRH ON ACUP.DEALNO=RPRH.DEALNO LEFT JOIN OPICS.CUST ON RPRH.CNO=CUST.CNO WHERE GLNO IN ('43000001') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY ACUP.DEALNO";
                }
                else if (ReportType.Equals("IntRepoLia"))
                {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP LEFT JOIN OPICS.RPRH ON ACUP.DEALNO=RPRH.DEALNO LEFT JOIN OPICS.CUST ON RPRH.CNO=CUST.CNO WHERE GLNO IN ('52000001') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY ACUP.DEALNO";
                }
                else if (ReportType.Equals("RelizedIRSCCS"))
                {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP LEFT JOIN OPICS.SWDH ON ACUP.DEALNO=SWDH.DEALNO LEFT JOIN OPICS.CUST ON SWDH.CNO=CUST.CNO WHERE GLNO IN ('447150004','447150006') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') ORDER BY ACUP.DEALNO";
                }
                else if (ReportType.Equals("GainLossBond"))
                {
                    sql = "SELECT ACUP.* ,CASE WHEN TRIM(CUST.CCODE)<>'TH' THEN 'International' ELSE 'Domestic' END as ThirdPartyType FROM OPICS.ACUP WHERE GLNO IN ('62100301','62100302','62100303','62100304') AND EFFDATE BETWEEN TO_DATE('" + StartDate + "','DD/MM/YYYY') AND TO_DATE('" + FromDate + "','DD/MM/YYYY') AND CODE=2480 AND TYPE='FI' AND PRODUCT='SECUR' ORDER BY ACUP.DEALNO";
                }
                else {
                    sql = OtherBox;
                }
                o.createCommand(sql, CommandType.Text);
                if (sql.Substring(0, sql.IndexOf(" ")).ToUpper().Equals("SELECT"))
                    dt = o.executeReaderToDT("TR137BOCPTYPE");
                else
                {
                    o.executeNonQuery();
                    dt = new DataTable();
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR137BOCPTYPE Finish-----");
                o.closeConnection();
            }
            return dt;
        }

    }
}
