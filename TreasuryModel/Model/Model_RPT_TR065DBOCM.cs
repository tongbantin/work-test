using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR065DBOCM
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR065DBOCM));
        protected Model_RPT_TR065DBOCM()
        {

        }
        public static ReportClass getReport(string ConnectionString, string date)
        {
            RPT_TR065DBOCM report = new RPT_TR065DBOCM();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR065DBOCM.Owner + ".KKB_SP_TSS_TR065DBOCM", CommandType.StoredProcedure);
                o.setInputParameter("p_date", date);
                s = o.executeReader("ref_rpt_cur", "TB_TR065DBOCM");

                o.createCommand(Model_RPT_TR065DBOCM.Owner + ".KKB_SP_TSS_TR065DBOCMSUB", CommandType.StoredProcedure);
                o.setInputParameter("p_date", date);
                DataSet s2 = o.executeReader("ref_rpt_cur", "TB_TR065DBOCMSUB");

                if (s2 == null)
                {
                    s2 = new DataSet();
                    DataTable dt = new DataTable("TB_TR065DBOCMSUB");
                    dt.Columns.Add(new DataColumn("CM_CMNE"));
                    dt.Columns.Add(new DataColumn("CM_MDATE"));
                    dt.Columns.Add(new DataColumn("CM_BALANCE"));
                    dt.Columns.Add(new DataColumn("CM_ACCROUTST"));
                    dt.Columns.Add(new DataColumn("CM_TOTAL"));
                    DataRow dr = dt.NewRow();
                    dr["CM_CMNE"] = "";
                    dr["CM_MDATE"] = "01/01/2011";
                    dr["CM_BALANCE"] = "0";
                    dr["CM_ACCROUTST"] = "0";
                    dr["CM_TOTAL"] = "0";
                    dt.Rows.Add(dr);
                    s2.Tables.Add(dt);
                }

                if (s != null)
                {
                    if (s2 != null)
                        s.Merge(s2.Tables[0]);
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report.SetDataSource(s);
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                    }
                    else
                    {
                        report = null;
                    }       
                }
                else
                    report = null;
             
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR065DBOCM Finish-----");
                o.closeConnection();
            }
            
            return report;
        }
    }
}
