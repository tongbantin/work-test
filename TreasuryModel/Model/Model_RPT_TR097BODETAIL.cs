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
    public class Model_RPT_TR097BOPODETAIL
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR097BOPODETAIL));
        protected Model_RPT_TR097BOPODETAIL()
        {

        }
        
        public static DataTable getData(string ConnectionString, string ExecuteDate, string Report)
        {
            Log.Info("-----Model_RPT_TR097BOPODETAIL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097BOPODETAIL");
            Log.Info("Data : From ExecuteDate=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097BOPODETAIL", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_spotfwd", "");
                o.setInputParameter("p_product", "");
                o.setInputParameter("p_report", Report);
                s = o.executeReader("ref_rpt_cur", "TB_TR097BOPODETAIL");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR097BOPODETAIL Finish-----");
                o.closeConnection();
            }
            return s.Tables[0];
        }

        public static DataTable getDetail(string ConnectionString, string ExecuteDate, string SpotFwdInd, string Product)
        {
            Log.Info("-----Model_RPT_TR097BOPODETAIL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097BOPODETAIL");
            Log.Info("Data : From ExecuteDate=" + ExecuteDate);

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097BOPODETAIL", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                o.setInputParameter("p_spotfwd", SpotFwdInd);
                o.setInputParameter("p_product", Product);
                o.setInputParameter("p_report", "Detail");
                s = o.executeReader("ref_rpt_cur", "TB_TR097BOPODETAIL");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR097BOPODETAIL Finish-----");
                o.closeConnection();
            }
            return s.Tables[0];
        }


        public static DataTable GetCurrentPosition(string ConnectionString, string Frequency, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR097BOPODETAIL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097OBOFXPO");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097OBOFXPO", CommandType.StoredProcedure);
                o.setInputParameter("p_frequency", Frequency);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_product", ToDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR097BOPODETAIL");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR097BOPODETAIL Finish-----");
                o.closeConnection();
            }
            return s.Tables[0];

        }

        public static DataTable GetCapitalBalance(string ConnectionString, string Frequency, string FromDate, string ToDate)
        {
            Log.Info("-----Model_RPT_TR097BOPODETAIL Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097BOPODETAIL");

            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR097BOPODETAIL.Owner + ".KKB_SP_TSS_TR097CAPITAL", CommandType.StoredProcedure);
                o.setInputParameter("p_frequency", Frequency);
                o.setInputParameter("p_from_date", FromDate);
                o.setInputParameter("p_product", ToDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR097BOPODETAIL");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR097BOPODETAIL Finish-----");
                o.closeConnection();
            }
            return s.Tables[0];

        }
    }
}
