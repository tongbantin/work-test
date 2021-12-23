﻿using System;
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
    public class Model_RPT_TR033DBOAL
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR033DBOAL));
        public Model_RPT_TR033DBOAL()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR033DBOAL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR033DBOAL.Owner + ".KKB_SP_TSS_TR033DBOAL");
            Log.Info("Data : Branch=" + Branch + ", Execute Date=" + ExecuteDate);

            RPT_TR033DBOAL report = new RPT_TR033DBOAL();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR033DBOAL.Owner + ".KKB_SP_TSS_TR033DBOAL", CommandType.StoredProcedure);
                o.setInputParameter("p_data_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR033DBOAL");
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
                Log.Info("-----Model_RPT_TR033DBOAL Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR033DBOAL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR033DBOAL.Owner + ".KKB_SP_TSS_TR033DBOAL");
            Log.Info("Data : Branch=" + Branch + ", Execute Date=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR033DBOAL.Owner + ".KKB_SP_TSS_TR033DBOAL", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR033DBOAL");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR033DBOAL Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
