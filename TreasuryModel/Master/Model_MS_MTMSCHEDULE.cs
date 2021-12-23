using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;

namespace KKB.Treasury.TreasuryModel.Master
{
    public enum MTMReport
    { 
        GENERAL = 1,
        CLOSE = 2,
        PHATRA = 3
    }
    public class Model_MS_MTMSCHEDULE
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_MTMSCHEDULE));

        public int MTMScheduleID { get; set; }
        public string Round { get; set; }
        public string MutualFund { get; set; } 
        public string OpenClose { get; set; }
        public string Source { get; set; }
        public string SpecialSource { get; set; }
        public string Rate { get; set; }
        public string FileType { get; set; }
        public string AllMdate { get; set; }
        public string FileName { get; set; }
        public string Template { get; set; }
        public string Notes { get; set; }


        public static DataTable getScheduleData(string ConnectionString)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMSchedule : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                //o.createCommand("SELECT SC.* FROM OPICINF.KKB_TB_MTMSCHEDULE SC ", CommandType.Text);
                o.createCommand("SELECT SC.*, NVL(TRIM(SUBSTR(TRIM(DESCR),INSTR(TRIM(DESCR),' ',-1))),SC.MUTUALFUND) AS ENCRYPT FROM OPICINF.KKB_TB_MTMSCHEDULE SC LEFT JOIN OPICS.CRGH ON TRIM(SC.MUTUALFUND) = TRIM(CRGH.GRPID)", CommandType.Text);
                s = o.executeReaderToDT("TB_MTMSCHEDULE");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataTable getScheduleDataByRound(string ConnectionString, string Round)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMSchedule : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From OPICINF.KKB_TB_MTMSCHEDULE Where Round='" + Round + "'", CommandType.Text);
                s = o.executeReaderToDT("TB_MTMSCHEDULE");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataTable getScheduleData(string ConnectionString, string MutualFund, string Round)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMSchedule : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Select * From OPICINF.KKB_TB_MTMSCHEDULE Where 1=1 ";
                if (MutualFund != "")
                    sql += "And MutualFund='" + MutualFund + "'";
                if (Round != "")
                    sql += "And Round='" + Round + "'";
                o.createCommand(sql, CommandType.Text);
                s = o.executeReaderToDT("TB_MTMSCHEDULE");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataTable getSchedule(string ConnectionString, int ID)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMSchedule : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From OPICINF.KKB_TB_MTMSCHEDULE Where MTMScheduleID='" + ID + "'", CommandType.Text);
                s = o.executeReaderToDT("TB_MTMSCHEDULE");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static Boolean existsFunds(string ConnectionString, string MutualFund, string FileType)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMSchedule : Start");

            String s = "";
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select MutualFund From OPICINF.KKB_TB_MTMSCHEDULE Where MutualFund='" + MutualFund + "' And FileType='" + FileType + "'", CommandType.Text);
                s = o.executeScalar().ToString();
                if (s != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
        }
        
        public Boolean AddMTMSchedule(string ConnectionString)
        {
            String sql = "Insert Into OPICINF.KKB_TB_MTMSCHEDULE(Round, MutualFund, OpenClose, Source, SpecialSource, Rate, FileType, AllMDate, FileName, Template, Notes) Values('" + this.Round + "','" + this.MutualFund + "','" + this.OpenClose + "','" + this.Source + "','" + this.SpecialSource + "','" + this.Rate + "','" + this.FileType + "','" + this.AllMdate + "','" + this.FileName + "','" + this.Template + "','" + this.Notes + "')";
            Log.Info("-----Model_MS_MTMSCHEDULE Start Insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
        }

        public Boolean UpdateMTMSchedule(string ConnectionString)
        {
            String sql = "Update OPICINF.KKB_TB_MTMSCHEDULE Set Round='" + this.Round + "', MutualFund='" + this.MutualFund + "',OpenClose='" + this.OpenClose + "', Source='" + this.Source + "', SpecialSource='" + this.SpecialSource + "', Rate='" + this.Rate + "', FileType='" + this.FileType + "', FileName='" + this.FileName + "', AllMdate='" + this.AllMdate + "', Template='" + this.Template + "', Notes='" + this.Notes + "' Where MTMScheduleID='" + this.MTMScheduleID + "'";
            Log.Info("-----Model_MS_MTMSCHEDULE Start Update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                Log.Error("-----Update Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
        }

        public Boolean DeleteMTMSchedule(string ConnectionString)
        {
            String sql = "Delete From OPICINF.KKB_TB_MTMSCHEDULE Where MTMScheduleID='" + this.MTMScheduleID + "'";
            Log.Info("-----Model_MS_MTMSCHEDULE Start Delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_MS_MTMSCHEDULE Finish-----");
                o.closeConnection();
            }
        }
    }
}
