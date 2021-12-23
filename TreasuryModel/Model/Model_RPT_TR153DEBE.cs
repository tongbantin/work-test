using System;
using System.Collections.Generic;
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
    public class Model_RPT_TR153DEBE
    {

        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR153DEBE));

        public Model_RPT_TR153DEBE()
        {

        }
        public static ReportClass getReport(string ConnectionString, string secid)
        {
            /*
            Log.Info("-----Model_RPT_TR153DEBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR153DEBE.Owner + ".Model_RPT_TR153DEBE");
            Log.Info("SECID : SECID=" + secid);

            RPT_TR153DEBE report = new RPT_TR153DEBE();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR153DEBE.Owner + ".KKB_SP_TSS_TR153DEBE", CommandType.StoredProcedure);
                o.setInputParameter("p_secid", secid);
                s = o.executeReader("ref_rpt_cur", "TB_TR153DEBE");

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
                Log.Info("-----Model_RPT_TR153DEBE Finish-----");
                o.closeConnection();
            }
            return report;
           
             */

            Log.Info("-----Model_RPT_TR153DEBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR153DEBE.Owner + ".Model_RPT_TR153DEBE");
            Log.Info("SECID : SECID=" + secid);

            ReportClass report = null;
            report = new RPT_TR153DEBE();
            
            Oracle o = Oracle.getInstance(ConnectionString);
            DataSet s = null;
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR153DEBE.Owner + ".KKB_SP_TSS_TR153DEBE", CommandType.StoredProcedure);
                o.setInputParameter("p_secid", secid);
                s = o.executeReader("ref_rpt_cur", "TB_TR153DEBE");

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
                Log.Info("-----Model_RPT_TR153DEBE Finish-----");
                o.closeConnection();
            }

            return report;

        }
        public static DataSet getDataSet(string ConnectionString, string secid)
        {
            Log.Info("-----Model_RPT_TR153DEBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR153DEBE.Owner + ".KKB_SP_TSS_TR153DEBE");
            Log.Info("Data : secid=" + secid);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR153DEBE.Owner + ".KKB_SP_TSS_TR153DEBE", CommandType.StoredProcedure);
                o.setInputParameter("p_secid", secid);
                s = o.executeReader("ref_rpt_cur", "TB_TR153DEBE");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR153DEBE Finish-----");
                o.closeConnection();
            }
            return s;
        }

    }
}
