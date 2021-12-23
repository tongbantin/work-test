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
    public class Model_RPT_TR113OBOFXCFC
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR113OBOFXCFC));
        public Model_RPT_TR113OBOFXCFC()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR113OBOFXCFC Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR113OBOFXCFC.Owner + ".KKB_SP_TSS_TR113OBOFXCFC");
            Log.Info("Data : DealNo=" + DealNo);

            RPT_TR113OBOFXCFC report = new RPT_TR113OBOFXCFC();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR113OBOFXCFC.Owner + ".KKB_SP_TSS_TR113OBOFXCFC", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR113OBOFXCFC");

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
                Log.Info("-----Model_RPT_TR113OBOFXCFC Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR113OBOFXCFC Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR113OBOFXCFC.Owner + ".KKB_SP_TSS_TR113OBOFXCFC");
            Log.Info("Data : DealNo=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR113OBOFXCFC.Owner + ".KKB_SP_TSS_TR113OBOFXCFC", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR113OBOFXCFC");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR113OBOFXCFC Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
