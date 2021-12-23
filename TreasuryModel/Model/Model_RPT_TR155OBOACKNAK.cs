using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;
using System.Collections;
using TreasuryModel.Data;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR155OBOACKNAK
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR155OBOACKNAK));

        public static ReportClass getReport(string ConnectionString, string FromDate)
        {
            Log.Info("-----Model_RPT_TR155OBOACKNAK Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR155OBOACKNAK.Owner + ".KKB_SP_TSS_TR155OBOACKNAK");

            ReportClass report = null;
            string TableName = "";
            
            report = new RPT_TR155OBOACKNAK();
            TableName = "TB_TR155OBOACKNAK";

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR155OBOACKNAK.Owner + ".KKB_SP_TSS_TR155OBOACKNAK", CommandType.StoredProcedure); 
                o.setInputParameter("p_fromdate", FromDate); 
                s = o.executeReader("ref_rpt_cur", TableName);

                if (s != null)
                {
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
                Log.Info("-----Model_RPT_TR155OBOACKNAK Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }

}
