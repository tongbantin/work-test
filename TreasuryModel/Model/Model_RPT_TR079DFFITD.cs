using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR079DFFITD
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR079DFFITD));

        public Model_RPT_TR079DFFITD()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR079DFFITD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR079DFFITD.Owner + ".KKB_SP_TSS_TR079DFFITD");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : From Date=" + FromDate);
            Log.Info("Data : To Date=" + ToDate);

            RPT_TR079DFFITD report = new RPT_TR079DFFITD();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR079DFFITD.Owner + ".KKB_SP_TSS_TR079DFFITD", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR079DFFITD");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report.SetDataSource(s);
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                        Log.Info(s.GetXml());
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
                Log.Info("-----Model_RPT_TR079DFFITD Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR079DFFITD Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR079DFFITD.Owner + ".KKB_SP_TSS_TR079DFFITD");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : From Date=" + FromDate);
            Log.Info("Data : To Date=" + ToDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR079DFFITD.Owner + ".KKB_SP_TSS_TR079DFFITD", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_to_date", ToDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR079DFFITD");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR079DFFITD Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
