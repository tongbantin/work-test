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

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR075OBOFXSW
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR075OBOFXSW));

        public static ReportClass getReport(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR075OBOFXSW Start-----");
            Log.Info("Store Procedure : " + SW.StoreProcedure);
            Log.Info("Data : DealNo=" + DealNo);

            RPT_TR075OBOFXSW report = new RPT_TR075OBOFXSW();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                s = DealTicket.getReport(ConnectionString, FXSW.StoreProcedure, FXSW.TableDataSet, "", "", "", DealNo);
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
                Log.Info("-----" + FXSW.StoreProcedure + " Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string DealNo)
        {
            Log.Info("-----Model_RPT_TR075OBOFXSW Start-----");
            Log.Info("Store Procedure : " + FXSW.StoreProcedure);
            Log.Info("Data : DealNo=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                s = DealTicket.getReport(ConnectionString, FXSW.StoreProcedure, FXSW.TableDataSet, "", "", "", DealNo);
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----" + FXSW.StoreProcedure + " Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
