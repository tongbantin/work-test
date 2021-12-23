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

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR141FIXRATE
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR141FIXRATE));
        public Model_RPT_TR141FIXRATE()
        {

        }
        public static ReportClass getReport(string ConnectionString, string ExecuteDate, int NumDays)
        {
            Log.Info("-----Model_RPT_TR141FIXRATE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR141FIXRATE.Owner);

            RPT_TR141FIXRATE report = new RPT_TR141FIXRATE();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR141FIXRATE.Owner + ".KKB_SP_TSS_TR141FIXRATE", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_numdays", NumDays);
                s = o.executeReader("ref_rpt_cur", "TB_TR141FIXRATE");

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
                Log.Info("-----Model_RPT_TR141FIXRATE Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
