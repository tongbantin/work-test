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
using System.Collections;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR139PAYS
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR139PAYS));
        
        public static ReportClass getReport(string ConnectionString, string Product, string Condition, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR139PAYS Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR139PAYS.Owner + ".KKB_SP_TSS_TR139PAYS");

            ReportClass report = null;
            string TableName = "";

            if ((Product == "FI") || (Product == "MATURED") || (Product == "EQ") || (Product == "IRDV")) //Fixed Income, Equities, Interest & Dividend
            {
                report = new RPT_TR139PAYSFI();
                TableName = "TB_TR139PAYSFI";
            }
            else if ((Product == "SW") || (Product == "OTHERSWAP"))
            {
                report = new RPT_TR139PAYSSW();
                TableName = "TB_TR139PAYSSW";
            } 
            
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR139PAYS.Owner + ".KKB_SP_TSS_TR139PAYS", CommandType.StoredProcedure);
                o.setInputParameter("p_product", Product);
                o.setInputParameter("p_condition", Condition);
                o.setInputParameter("p_fromdate", FromDate);
                o.setInputParameter("p_todate", ToDate);
                s = o.executeReader("ref_rpt_cur", TableName);

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
                Log.Info("-----Model_RPT_TR139PAYS Finish-----");
                o.closeConnection();
            }
            return report;
        }
    }

}
