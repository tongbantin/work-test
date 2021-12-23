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
    public class Model_RPT_TR143PCONFREC
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR143PCONFREC));
        protected Model_RPT_TR143PCONFREC()
        {
            
        }


        public static ReportClass getReport(string ConnectionString, string date)
        {
            Log.Info("-----Model_RPT_TR143PCONFREC Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR143PCONFREC.Owner + ".KKB_SP_TSS_TR143PCONFREC");
            Log.Info("Data date= "+date);

            RPT_TR143PCONFREC report = new RPT_TR143PCONFREC();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR143PCONFREC.Owner + ".KKB_SP_TSS_TR143PCONFREC", CommandType.StoredProcedure);
                o.setInputParameter("p_date", date);
                s = o.executeReader("ref_rpt_cur", "TB_TR143PCONFREC");  

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
                Log.Info("-----Model_RPT_TR143PCONFREC Finish-----");
                o.closeConnection();
            }
            return report;
        }

    }
}
