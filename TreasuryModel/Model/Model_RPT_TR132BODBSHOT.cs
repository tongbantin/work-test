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
    public class Model_RPT_TR132BODBSHOT
    {

        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR132BODBSHOT));
                
        public Model_RPT_TR132BODBSHOT()
        {

        }

        public static ReportClass getReport(string ConnectionString, string Report, string FromDate, string ToDate, string Security, string AutorizeDate, string Registrar)
        {
            Log.Info("-----Model_RPT_TR132BODBSHOT Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR132BODBSHOT.Owner + ".KKB_SP_TSS_TR132BODBSHOT"); 

            ReportClass report = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR132BODBSHOT.Owner + ".KKB_SP_TSS_TR132BODBSHOT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_fromdate", FromDate);
                o.setInputParameter("p_todate", ToDate);
                o.setInputParameter("p_security", Security);
                s = o.executeReader("ref_rpt_cur", "TB_TR132BODBSHOT");

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        switch (Report)
                        { 
                            case "Cover":
                                s.DataSetName = "DS_TR132BODBSHOTDOC";
                                s.Tables[0].TableName = "TB_TR132BODBSHOTDOC";
                                for (int i = 0; i < s.Tables[0].Rows.Count; i++ )
                                {
                                    s.Tables[0].Rows[i]["AutorizeDate"] = AutorizeDate;
                                    s.Tables[0].Rows[i]["Registrar"] = Registrar;
                                }
                                report = new RPT_TR132BODBSHOTDOC();
                                break;

                        }
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
                Log.Info("-----Model_RPT_TR132BODBSHOT Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static DataTable getData(string ConnectionString, string Report, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR132BODBSHOT Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR132BODBSHOT.Owner + ".KKB_SP_TSS_TR132BODBSHOT"); 

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR132BODBSHOT.Owner + ".KKB_SP_TSS_TR132BODBSHOT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_fromdate", FromDate);
                o.setInputParameter("p_todate", ToDate);
                o.setInputParameter("p_security", "");
                s = o.executeReader("ref_rpt_cur", "TB_TR132BODBSHOT");

                return s.Tables[0];
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR132BODBSHOT Finish-----");
                o.closeConnection();
            }
        }

        public static bool genInternalRef(string ConnectionString, string Report, string FromDate, string ToDate, string Security)
        {
            Log.Info("-----Model_RPT_TR132BODBSHOT Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR132BODBSHOT.Owner + ".KKB_SP_TSS_TR132BODBSHOT");

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR132BODBSHOT.Owner + ".KKB_SP_TSS_TR132BODBSHOT", CommandType.StoredProcedure);
                o.setInputParameter("p_report", Report);
                o.setInputParameter("p_fromdate", FromDate);
                o.setInputParameter("p_todate", ToDate);
                o.setInputParameter("p_security", Security);
                s = o.executeReader("ref_rpt_cur", "TB_TR132BODBSHOT");

                if (s != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR132BODBSHOT Finish-----");
                o.closeConnection();
            }
        }
   
    }
}
