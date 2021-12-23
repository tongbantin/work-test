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
    public class Model_RPT_TR082DBOBE
    {   
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR082DBOBE));

        public Model_RPT_TR082DBOBE()
        {

        }
        public static ReportClass getReport(string ConnectionString, string Branch, string DealNo, string NewDealNo, string IssueDate, string RefNo, string CreateBy)
        {
            Log.Info("-----Model_RPT_TR082DBOBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR082DBOBE.Owner + ".KKB_SP_TSS_TR082DBOBE");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : DealNo=" + DealNo);

            RPT_TR082DBOBE report = new RPT_TR082DBOBE();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR082DBOBE.Owner + ".KKB_SP_TSS_TR082DBOBE", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_deal_no", DealNo);
                o.setInputParameter("p_new_deal", NewDealNo);
                o.setInputParameter("p_execute_date", IssueDate);
                o.setInputParameter("p_ref_no", RefNo);
                o.setInputParameter("p_create_by", CreateBy);
                s = o.executeReader("ref_rpt_cur", "TB_TR082DBOBE");
                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        if (NewDealNo.Trim() != "")
                        {
                            s.Tables[0].Rows[0]["DEALNO"] = NewDealNo;
                        }
                        if (IssueDate.Trim() != "")
                        {
                            string[] d = IssueDate.Split('/');
                            DateTime dt = new DateTime(Convert.ToInt32(d[2]), Convert.ToInt32(d[1]), Convert.ToInt32(d[0]));
                            s.Tables[0].Rows[0]["ISSUE_DATE"] = dt.ToString("dd MMM yyyy");
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
                Log.Info("-----Model_RPT_TR082DBOBE Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Branch, string DealNo, string NewDealNo)
        {
            Log.Info("-----Model_RPT_TR082DBOBE Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR082DBOBE.Owner + ".KKB_SP_TSS_TR082DBOBE");
            Log.Info("Data : Branch=" + Branch);
            Log.Info("Data : DealNo=" + DealNo);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR082DBOBE.Owner + ".KKB_SP_TSS_TR082DBOBE", CommandType.StoredProcedure);
                o.setInputParameter("p_branch", Branch);
                o.setInputParameter("p_deal_no", DealNo);
                s = o.executeReader("ref_rpt_cur", "TB_TR082DBOBE");
                Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR082DBOBE Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
