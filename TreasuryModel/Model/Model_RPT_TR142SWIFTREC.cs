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
    public class Model_RPT_TR142SWIFTREC
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR142SWIFTREC));
        protected Model_RPT_TR142SWIFTREC()
        {
            
        }


        public static ReportClass getReport(string ConnectionString, string swifttype, string date)
        {
            Log.Info("-----Model_RPT_TR142SWIFTREC Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR142SWIFTREC.Owner + ".KKB_SP_TSS_TR142SWIFTREC");
            Log.Info("Data : swifttype=" + swifttype +" date= "+date);

            RPT_TR142SWIFTREC report = new RPT_TR142SWIFTREC();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR142SWIFTREC.Owner + ".KKB_SP_TSS_TR142SWIFTREC", CommandType.StoredProcedure);
                o.setInputParameter("p_swifttype", swifttype);
                o.setInputParameter("p_date", date);
                s = o.executeReader("ref_rpt_cur", "TB_TR142SWIFTREC");  

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
                Log.Info("-----Model_RPT_TR142SWIFTREC Finish-----");
                o.closeConnection();
            }
            return report;
        }

    }
}
