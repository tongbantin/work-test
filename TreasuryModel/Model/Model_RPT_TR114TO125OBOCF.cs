using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using log4net;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR114TO125OBOCF
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR114TO125OBOCF));

        public Model_RPT_TR114TO125OBOCF()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo, string CustomerType)
        {
            Log.Info("-----Model_RPT_TR114TO125OBOCF Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR114TO125OBOCF.Owner + ".KKB_SP_TSS_TR114TO125OBOCF");
            Log.Info("Data : DealNo=" + DealNo + ", Customer Type=" + CustomerType);

            ReportClass report = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR114TO125OBOCF.Owner + ".KKB_SP_TSS_TR114TO125OBOCF", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                o.setInputParameter("p_customertype", CustomerType);
                s = o.executeReader("ref_rpt_cur", "TB_TR114TO125OBOCF");

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        switch(s.Tables[0].Rows[0]["REPORTNAME"].ToString())
                        {
                            case "TR114OBOIRCFCP":
                                s.Tables[0].TableName = "TB_TR114OBOIRCFCP";
                                report = new RPT_TR114OBOIRCFCP();
                                break;
                            case "TR115OBOIRCFLI":
                                s.Tables[0].TableName = "TB_TR115OBOIRCFLI";
                                report = new RPT_TR115OBOIRCFLI();
                                break;
                            case "TR116OBOIRCFMF":
                                s.Tables[0].TableName = "TB_TR116OBOIRCFMF";
                                report = new RPT_TR116OBOIRCFMF();
                                break;
                            case "TR117OBOIRCFNL":
                                s.Tables[0].TableName = "TB_TR117OBOIRCFNL";
                                report = new RPT_TR117OBOIRCFNL();
                                break;
                            case "TR118OBOIRCFPF":
                                s.Tables[0].TableName = "TB_TR118OBOIRCFPF";
                                report = new RPT_TR118OBOIRCFPF();
                                break;
                            case "TR119OBOIRCFSH":
                                s.Tables[0].TableName = "TB_TR119OBOIRCFSH";
                                report = new RPT_TR119OBOIRCFSH();
                                break;
                            case "TR120OBOCCCFCP":
                                s.Tables[0].TableName = "TB_TR120OBOCCCFCP";
                                report = new RPT_TR120OBOCCCFCP();
                                break;
                            case "TR121OBOCCCFLI":
                                s.Tables[0].TableName = "TB_TR121OBOCCCFLI";
                                report = new RPT_TR121OBOCCCFLI();
                                break;
                            case "TR122OBOCCCFMF":
                                s.Tables[0].TableName = "TB_TR122OBOCCCFMF";
                                report = new RPT_TR122OBOCCCFMF();
                                break;
                            case "TR123OBOCCCFNL":
                                s.Tables[0].TableName = "TB_TR123OBOCCCFNL";
                                report = new RPT_TR123OBOCCCFNL();
                                break;
                            case "TR124OBOCCCFPF":
                                s.Tables[0].TableName = "TB_TR124OBOCCCFPF";
                                report = new RPT_TR124OBOCCCFPF();
                                break;
                            case "TR125OBOCCCFSH":
                                s.Tables[0].TableName = "TB_TR125OBOCCCFSH";
                                report = new RPT_TR125OBOCCCFSH();
                                break;
                        }
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
                Log.Info("-----Model_RPT_TR114TO125OBOCF Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }
}
