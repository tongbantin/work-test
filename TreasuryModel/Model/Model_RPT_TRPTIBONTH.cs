using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using log4net;
using System.Collections;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TRPTIBONTH
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TRPTIBONTH));

        public Model_RPT_TRPTIBONTH()
        {

        }

        
        public static ReportClass getReport(string ConnectionString, ArrayList doc, ArrayList revisedoc, ArrayList nosigndoc)
        {
            Log.Info("-----Model_RPT_TRPTIBONTH Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TRPTIBONTH.Owner + ".KKB_SP_TSS_TRPTIBONTH");

            //Join string
            string docjoin = string.Join(",", (string[])doc.ToArray(Type.GetType("System.String")));
            string revisedocjoin = string.Join(",", (string[])revisedoc.ToArray(Type.GetType("System.String")));
            string nosigndocjoin = string.Join(",", (string[])nosigndoc.ToArray(Type.GetType("System.String")));

            RPT_TR051BONTH report = new RPT_TR051BONTH();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TRPTIBONTH.Owner + ".KKB_SP_TSS_TRPTIBONTH", CommandType.StoredProcedure);
                o.setInputParameter("p_translog", "");
                o.setInputParameter("p_dealno", "");
                o.setInputParameter("p_document", docjoin);
                o.setInputParameter("p_revisedocument", revisedocjoin);
                o.setInputParameter("p_nosigndocument", nosigndocjoin);
                s = o.executeReader("ref_rpt_cur", "TB_TR051BONTH");
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
                Log.Info("-----Model_RPT_TRPTIBONTH Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static ReportClass getReport(string ConnectionString, string TransLogID, string DealNo, string Revise, string NoSign)
        {
            Log.Info("-----Model_RPT_TRPTIBONTH Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TRPTIBONTH.Owner + ".KKB_SP_TSS_TRPTIBONTH");
            Log.Info("Data : Transaction Log=" + TransLogID);
            Log.Info("Data : Deal No=" + DealNo);

            RPT_TR051BONTH report = new RPT_TR051BONTH();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TRPTIBONTH.Owner + ".KKB_SP_TSS_TRPTIBONTH", CommandType.StoredProcedure);
                o.setInputParameter("p_translog", TransLogID);
                o.setInputParameter("p_dealno", DealNo);
                o.setInputParameter("p_document", "");
                o.setInputParameter("p_revisedocument", "");
                o.setInputParameter("p_nosigndocument", NoSign);
                s = o.executeReader("ref_rpt_cur", "TB_TR051BONTH");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        s.Tables[0].Columns.Add("Revise");
                        for (int i = 0; i < s.Tables[0].Rows.Count; i++)
                        {
                            if (Revise.Equals("true"))
                                s.Tables[0].Rows[i]["Revise"] = Revise;
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
                Log.Info("-----Model_RPT_TRPTIBONTH Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string TranLogID, string DealNo)
        {
            Log.Info("-----Model_RPT_TRPTIBONTH Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TRPTIBONTH.Owner + ".KKB_SP_TSS_TRPTIBONTH");
            Log.Info("Data : TransLogID=" + TranLogID);
            Log.Info("Data : Deal No=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TRPTIBONTH.Owner + ".KKB_SP_TSS_TRPTIBONTH", CommandType.StoredProcedure);
                o.setInputParameter("p_translog", TranLogID);
                o.setInputParameter("p_dealno", DealNo);
                o.setInputParameter("p_document", "");
                o.setInputParameter("p_revisedocument", "");
                o.setInputParameter("p_nosigndocument", "");
                s = o.executeReader("ref_rpt_cur", "TB_TR051BONTH");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TRPTIBONTH Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
