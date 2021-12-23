using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using KKB.Treasury.TreasuryModel.Configuration;
using log4net;


namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR074OBOFX
    {
        private static string Owner = Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR074OBOFX));

        public Model_RPT_TR074OBOFX()
        {

        }
        public static ReportClass getReport(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR074OBOFX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR074OBOFX.Owner + ".KKB_SP_TSS_TR074OBOFX");
            Log.Info("Data : Deal No=" + DealNo);
                        
            Oracle o = Oracle.getInstance(ConnectionString);
            ReportClass report = new ReportClass();
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR074OBOFX.Owner + ".KKB_SP_TSS_TR074OBOFX", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR074OBOFX");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = s.Tables[0].Rows[0];
                        if (dr["CType"].ToString() == "B")
                        {
                            RPT_TR074OBOFX r74 = new RPT_TR074OBOFX();
                            r74.SetDataSource(s);
                            report = (ReportClass)r74;
                        }
                        else {
                            s.DataSetName = "DS_TR074OBOFXCORP";
                            s.Tables[0].TableName = "TB_TR074OBOFXCORP";
                            RPT_TR074OBOFXCORP r74corp = new RPT_TR074OBOFXCORP();
                            r74corp.SetDataSource(s);
                            report = (ReportClass)r74corp;
                        }                        
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                        Log.Info(s.GetXml());
                    }
                    else
                    {
                        return null;
                    }       
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR074OBOFX Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR074OBOFX Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR074OBOFX.Owner + ".KKB_SP_TSS_TR074OBOFX");
            Log.Info("Data : Deal No=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR074OBOFX.Owner + ".KKB_SP_TSS_TR074OBOFX", CommandType.StoredProcedure);
                o.setInputParameter("p_dealno", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR074OBOFX");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR074OBOFX Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
