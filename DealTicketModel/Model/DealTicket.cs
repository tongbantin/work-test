using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.DealTicketReport.Report;
using KKB.Treasury.ThirdParty;

namespace KKB.DealTicketModel.Model
{
    public class DealTicket
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(DealTicket));

        public static DataSet getReport(string StoreProcedure, string TbDataSet, string Branch, string Product, string ProdType, string DealNo)
        {
            //String StoreProcedure = "OPICINF.KKB_DEALS_TICKET_DL";
            //String TbDataSet = "TB_TICKETDL";

            Log.Info("-----Model_RPT_DealsTicket Start-----");
            Log.Info("Store Procedure : " + StoreProcedure);

            DataSet ds = new DataSet();
            ds.Tables.Add(TbDataSet);
            Log.Info("Data : " + ds.GetXml());
            DataSet s = new DataSet();
            //s = null;
            //OracleConnection o = new OracleConnection(ConnectionString);
            IDatabase ora = new SystemOracleClient();
            OracleConnection o = new OracleConnection();
            try
            {
                /*if (o.State == ConnectionState.Open)
                    o.Close();
                o.Open();*/
                o = (OracleConnection)ora.getConnection();
                OracleCommand command = new OracleCommand(StoreProcedure, o);
                Log.Info("p_branch : " + Branch);
                Log.Info("p_product : " + Product);
                Log.Info("p_prodtype : " + ProdType);
                Log.Info("p_dealno : " + DealNo);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("p_branch", Branch);
                command.Parameters.AddWithValue("p_product", Product);
                command.Parameters.AddWithValue("p_prodtype", ProdType);
                command.Parameters.AddWithValue("p_dealno", DealNo);
                OracleParameter outparam = new OracleParameter("ref_rpt_cur", OracleType.Cursor);
                command.Parameters.Add(outparam).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(s);
                ds.Tables[0].Merge(s.Tables[0]);
                return ds;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_DealsTicket Finish-----");
                o.Close();
            }
            return s;
        }

        public static bool UpdateStatus(String Branch, String Product, String ProductType, String DealNo, String Status)
        {
            //String StoreProcedure = "OPICINF.KKB_DEALS_TICKET_DL";
            //String TbDataSet = "TB_TICKETDL";

            Log.Info("-----Model_RPT_DealsTicket KKB_DEALS_TICKET_STATUS Start-----");
            Log.Info("Table : OPICINF.KKB_TB_DEALS_TICKET");

            int result = 0;
            //OracleConnection o = new OracleConnection(ConnectionString);
            IDatabase ora = new SystemOracleClient();
            OracleConnection o = new OracleConnection();
            try
            {
                /*if (o.State == ConnectionState.Open)
                    o.Close();
                o.Open();*/
                o = (OracleConnection)ora.getConnection();
                String sql = " UPDATE OPICS.KKB_TB_DEALS_TICKET D SET D.STATUS = '" + Status + "', D.PRINTDATE=SYSDATE WHERE TRIM(D.BR) = '" + Branch + "' AND TRIM(D.PRODUCT) = '" + Product + "' AND TRIM(D.PRODTYPE) = '" + ProductType + "' AND TRIM(D.DEALNO) = '" + DealNo + "'";
                Log.Info("SQL Update : " + sql);
                OracleCommand command = new OracleCommand(sql, o);
                result = command.ExecuteNonQuery();
                if (result >= 0)
                {
                    Log.Info("Update Success");
                    return true;
                }
                else
                {
                    Log.Info("Update Failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_RPT_DealsTicket Finish-----");
                o.Close();
            }
        }

        public static DataSet getReport(String ConnectionString, string StoreProcedure, string TbDataSet, string Branch, string Product, string ProdType, string DealNo)
        {
            //String StoreProcedure = "OPICINF.KKB_DEALS_TICKET_DL";
            //String TbDataSet = "TB_TICKETDL";

            Log.Info("-----Model_RPT_DealsTicket Start-----");
            Log.Info("Store Procedure : " + StoreProcedure);

            DataSet ds = new DataSet();
            ds.Tables.Add(TbDataSet);
            Log.Info("Data : " + ds.GetXml());
            DataSet s = new DataSet();
            OracleConnection o = new OracleConnection(ConnectionString);
            try
            {
                if (o.State == ConnectionState.Open)
                    o.Close();
                o.Open();
                OracleCommand command = new OracleCommand(StoreProcedure, o);
                Log.Info("p_branch : " + Branch);
                Log.Info("p_product : " + Product);
                Log.Info("p_prodtype : " + ProdType);
                Log.Info("p_dealno : " + DealNo);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("p_branch", Branch);
                command.Parameters.AddWithValue("p_product", Product);
                command.Parameters.AddWithValue("p_prodtype", ProdType);
                command.Parameters.AddWithValue("p_dealno", DealNo);
                OracleParameter outparam = new OracleParameter("ref_rpt_cur", OracleType.Cursor);
                command.Parameters.Add(outparam).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(s);
                ds.Tables[0].Merge(s.Tables[0]);
                return ds;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_DealsTicket Finish-----");
                o.Close();
            }
            return s;
        }

        public static bool UpdateStatus(String ConnectionString, String Branch, String Product, String ProductType, String DealNo, String Status)
        {
            //String StoreProcedure = "OPICINF.KKB_DEALS_TICKET_DL";
            //String TbDataSet = "TB_TICKETDL";

            Log.Info("-----Model_RPT_DealsTicket KKB_DEALS_TICKET_STATUS Start-----");
            Log.Info("Table : OPICINF.KKB_TB_DEALS_TICKET");

            int result = 0;
            OracleConnection o = new OracleConnection(ConnectionString);
            try
            {
                if (o.State == ConnectionState.Open)
                    o.Close();
                o.Open();
                String sql = " UPDATE OPICS.KKB_TB_DEALS_TICKET D SET D.STATUS = '" + Status + "', D.PRINTDATE=SYSDATE WHERE TRIM(D.BR) = '" + Branch + "' AND TRIM(D.PRODUCT) = '" + Product + "' AND TRIM(D.PRODTYPE) = '" + ProductType + "' AND TRIM(D.DEALNO) = '" + DealNo + "'";
                Log.Info("SQL Update : " + sql);
                OracleCommand command = new OracleCommand(sql, o);
                result = command.ExecuteNonQuery();
                if (result >= 0)
                {
                    Log.Info("Update Success");
                    return true;
                }
                else
                {
                    Log.Info("Update Failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_RPT_DealsTicket Finish-----");
                o.Close();
            }
        }
    }
}
