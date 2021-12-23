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

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR089OTHMTM
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR089OTHMTM));
        protected Model_RPT_TR089OTHMTM()
        {
            
        }
        
        public static DataTable getFund()
        {
            try
            {
                DataTable dt = new DataTable();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getMutualFund(string Fund)
        {
            try
            {
                DataTable dt = new DataTable();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ReportClass getReport(string ConnectionString, string Fund, string MutualFund, string Source, string Time, string Rate)
        {
            Log.Info("-----Model_RPT_TR089OTHMTM Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089OTHMTM");

            ReportClass report;
            if ((Fund.Equals("SCBSFFPLUS")) || (Fund.Equals("SCBRF")))
                report = new RPT_TR089PHATRAMTM();
            else
                report = new RPT_TR089OTHMTM();

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089OTHMTM", CommandType.StoredProcedure);
                o.setInputParameter("p_fund", Fund);
                o.setInputParameter("p_mutualfund", MutualFund);
                o.setInputParameter("p_source", Source);
                o.setInputParameter("p_time", Time);
                o.setInputParameter("p_rate", Rate);
                if ((Fund.Equals("PHATRA")) || (Fund.Equals("SCBSFFPLUS")) || (Fund.Equals("SCBRF")))
                    s = o.executeReader("ref_rpt_cur", "TB_TR089PHATRAMTM");
                else
                    s = o.executeReader("ref_rpt_cur", "TB_TR089OTHMTM");
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
                Log.Info("-----Model_RPT_TR089OTHMTM Finish-----");
                o.closeConnection();
            }
            return report;
        }
        public static DataSet getDataSet(string ConnectionString, string Fund, string MutualFund, string Source, string Time, string Rate, string KKEmail, string KKTel, string Custodian)
        {
            Log.Info("-----Model_RPT_TR089OTHMTM Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089OTHMTM");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089OTHMTM", CommandType.StoredProcedure);
                o.setInputParameter("p_fund", Fund);
                o.setInputParameter("p_mutualfund", MutualFund);
                o.setInputParameter("p_source", Source);
                o.setInputParameter("p_time", Time);
                o.setInputParameter("p_rate", Rate);
                o.setInputParameter("p_kkmail", KKEmail);
                o.setInputParameter("p_kktel", KKTel);
                o.setInputParameter("p_custodian", Custodian);

                if ((Fund.Equals("SCBSFFPLUS")) || (Fund.Equals("SCBRF")))
                    s = o.executeReader("ref_rpt_cur", "TB_TR089PHATRAMTM");
                else
                    s = o.executeReader("ref_rpt_cur", "TB_TR089OTHMTM");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR089OTHMTM Finish-----");
                o.closeConnection();
            }
            return s;
        }
        // Update BY Tanadate ,Update Date 21/07/2015
        public static DataSet getDataSet(string ConnectionString, string MTMMappingID, string Fund, string MutualFund, string Source, string Time, string Rate, string KKEmail, string KKTel, string Custodian)
        {
            Log.Info("-----Model_RPT_TR089OTHMTM Start-----");
            Log.Info("MTMMappingID : " + MTMMappingID);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                Log.Info("SELECT * FROM OPICINF.KKB_TB_MTMMAPPING WHERE MTMMAPPINGID='" + MTMMappingID + "'");
                o.createCommand("SELECT * FROM OPICINF.KKB_TB_MTMMAPPING WHERE MTMMAPPINGID='" + MTMMappingID + "'", CommandType.Text);
                DataTable t = o.executeReaderToDT("TB_MTMMAPPING");

                string Data1 = Model_RPT_TR089OTHMTM.ReplaceVariable(t.Rows[0]["Data_1"].ToString(), Fund, MutualFund, Source, Time, Rate, KKEmail, KKTel, Custodian);
                string Data2 = Model_RPT_TR089OTHMTM.ReplaceVariable(t.Rows[0]["Data_2"].ToString(), Fund, MutualFund, Source, Time, Rate, KKEmail, KKTel, Custodian);
                string Data3 = Model_RPT_TR089OTHMTM.ReplaceVariable(t.Rows[0]["Data_3"].ToString(), Fund, MutualFund, Source, Time, Rate, KKEmail, KKTel, Custodian);
                string Data4 = Model_RPT_TR089OTHMTM.ReplaceVariable(t.Rows[0]["Data_4"].ToString(), Fund, MutualFund, Source, Time, Rate, KKEmail, KKTel, Custodian);
                Log.Info("SELECT " + Data1 + Data2 + " FROM " + Data3 + " WHERE " + Data4);
                o.createCommand("SELECT " + Data1 + Data2 + " FROM " + Data3 + " WHERE " + Data4, CommandType.Text);
                s = o.executeReader();

                if ((Fund.Equals("SCBSFFPLUS")) || (Fund.Equals("SCBRF")) || (Fund.Equals("PHATRA")))
                    s.Tables[0].TableName = "TB_TR089PHATRAMTM";
                else
                    s.Tables[0].TableName = "TB_TR089OTHMTM";
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR089OTHMTM Finish-----");
                o.closeConnection();
            }
            return s;
        }

        private static string ReplaceVariable(string data, string Fund, string MutualFund, string Source, string Time, string Rate, string KKEmail, string KKTel, string Custodian)
        {
            try
            {
                data = data.Replace("||p_fund||", Fund).Replace("||p_mutualfund||", MutualFund).Replace("||p_source||", Source);
                data = data.Replace("||p_time||", Time).Replace("||p_rate||", Rate).Replace("||p_kkmail||", KKEmail).Replace("||p_kktel||", KKTel);
                data = data.Replace("||p_custodian||", Custodian).Replace(System.Environment.NewLine, string.Empty);
                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }
        public static DataSet getDataSet(string ConnectionString, string DataDate, string MutualFund, string Source, string OpenClose, string Rate, string DifSource, string AllMDate)
        {
            Log.Info("-----Model_RPT_TR089OTHMTM Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089DFMTM_V3");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                if (MutualFund.Equals("SCB_VALUE")) 
                    o.createCommand(Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089VALMTM", CommandType.StoredProcedure);
                else
                    o.createCommand(Model_RPT_TR089OTHMTM.Owner + ".KKB_SP_TSS_TR089DFMTM_V3", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", DataDate);
                o.setInputParameter("p_party", MutualFund);
                o.setInputParameter("p_source", Source);
                o.setInputParameter("p_openclose", OpenClose);
                o.setInputParameter("p_rate", Rate);
                o.setInputParameter("p_difsource", DifSource);
                o.setInputParameter("p_allmdate", AllMDate);

                if ((MutualFund.Equals("SCBSFFPLUS")) || (MutualFund.Equals("SCBRF")) || (MutualFund.Equals("PHATRA")))
                    s = o.executeReader("ref_rpt_cur", "TB_TR089PHATRAMTM");
                else
                    s = o.executeReader("ref_rpt_cur", "TB_TR089OTHMTM");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR089OTHMTM Finish-----");
                o.closeConnection();
            }
            return s;
        }
    }
}
