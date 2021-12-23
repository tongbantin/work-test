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
    public class NUPDCheck
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(NUPDCheck));
             
        public static DataTable getDataNUPD(string ConnectionString, string today, string[] prodcode)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                o.openConnection();
                o.createCommand(NUPDCheck.Owner + ".KKB_SP_TSS_NostroDaily", CommandType.StoredProcedure);
                o.setInputParameter("p_trans", "NUPDCHK");
                //o.setInputParameter("p_date", today);
                o.setInputParameter("p_date1", today);
                o.setInputParameter("p_date2", "");
                o.setInputParameter("p_date3", "");
                o.setInputParameter("p_date4", "");
                o.setInputParameter("p_date5", "");
                o.setInputParameter("p_curr", "");
                o.setInputParameter("p_nosacct", "");

                DataSet ds = o.executeReader("ref_rpt_cur", "TB_NUPDDATA");

                //Filter with prodcode 
                var query = ds.Tables[0].AsEnumerable().Where(r => prodcode.Contains(r.Field<string>("PRODUCT").Trim()));

                if (query.Count() > 0)
                {
                  dt = query.CopyToDataTable();
                }       
             
            }
            catch (Exception ex)
            {
                Log.Error("-----Model getNUPDTrans Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----getNUPDTrans finish-----");
                o.closeConnection();
            }
            return dt;

        }

        public static DataTable getDataFX(string ConnectionString, string today, string[] prodcode)
        {
            
            Oracle o = Oracle.getInstance(ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                o.openConnection();
                o.createCommand(NUPDCheck.Owner + ".KKB_SP_TSS_NostroDaily", CommandType.StoredProcedure); ;
                o.setInputParameter("p_trans", "NTSUMM");
                //o.setInputParameter("p_date", today);
                o.setInputParameter("p_date1", today);
                o.setInputParameter("p_date2", "");
                o.setInputParameter("p_date3", "");
                o.setInputParameter("p_date4", "");
                o.setInputParameter("p_date5", "");
                o.setInputParameter("p_curr", "");
                o.setInputParameter("p_nosacct", "");

                DataSet ds = o.executeReader("ref_rpt_cur", "TB_FXDATA");

                //Filter with prodcode
                var query = ds.Tables[0].AsEnumerable().Where(r => prodcode.Contains(r.Field<string>("PRODCODE").Trim()));

                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                }  
            }
            catch (Exception ex)
            {
                Log.Error("----- GetDataFX Error -----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----GetDataFX finish-----");
                o.closeConnection();
            }
            return dt;
           
        }
    


    }
}
