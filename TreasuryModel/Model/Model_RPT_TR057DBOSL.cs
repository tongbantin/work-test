﻿using System;
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
    public class Model_RPT_TR057DBOSL
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR057DBOSL));

        public Model_RPT_TR057DBOSL()
        {

        }
        public static ReportClass getReport(string ConnectionString, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR057DBOSL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR057DBOSL.Owner + ".KKB_SP_TSS_TR057DBOSL");
            Log.Info("Data : Execute Date=" + ExecuteDate);

            RPT_TR057DBOSL report = new RPT_TR057DBOSL();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR057DBOSL.Owner + ".KKB_SP_TSS_TR057DBOSL", CommandType.StoredProcedure);
                 o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR057DBOSL");
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
                Log.Info("-----Model_RPT_TR057DBOSL Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR057DBOSL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR057DBOSL.Owner + ".KKB_SP_TSS_TR057DBOSL");
            Log.Info("Data : Execute Date=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR057DBOSL.Owner + ".KKB_SP_TSS_TR057DBOSL", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR057DBOSL");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR057DBOSL Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
