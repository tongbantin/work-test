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

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR149OBOFCDOUT
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR149OBOFCDOUT));

        public static ReportClass getReport(string ConnectionString, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR149OBOFCDOUT Start-----");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = null;
            try
            {
                string TableName = "";
                TableName = "TB_TR149OBOFCDOUT";
                report = new RPT_TR149OBOFCDOUT();
                 
                o.openConnection();
                o.createCommand(Model_RPT_TR149OBOFCDOUT.Owner + ".KKB_SP_TSS_TR149OBOFCDOUT", CommandType.StoredProcedure);
                o.setInputParameter("p_fromdate", FromDate);
                o.setInputParameter("p_todate", ToDate); 
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
                Log.Info("-----Model_RPT_TR149OBOFCDOUT Finish-----");
                o.closeConnection();
            }
            return report;
        }
        
    }

}
