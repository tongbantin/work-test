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
    public class Model_MS_MTMMAPPING
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_MS_MTMMAPPING));

        public int MTMMappingID { get; set; }
        public string Name { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }

        public static DataTable getMappingData(string ConnectionString)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMMapping : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From OPICINF.KKB_TB_MTMMAPPING", CommandType.Text);
                s = o.executeReaderToDT("TB_MTMMAPPING");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMMAPPING Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataTable getMappingDataByName(string ConnectionString, string Name)
        {
            Log.Info("-----Model_MS_MTMSCHEDULE Start-----");
            Log.Info("Get MTMMapping : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From OPICINF.KKB_TB_MTMMAPPING Where Name like '%" + Name + "%' ", CommandType.Text);
                s = o.executeReaderToDT("TB_MTMMAPPING");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMMAPPING Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public static DataTable getMapping(string ConnectionString, int ID)
        {
            Log.Info("-----Model_MS_MTMMAPPING Start-----");
            Log.Info("Get MTMMapping : Start");

            DataTable s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                Log.Info("SQL Statement : Select * From OPICINF.KKB_TB_MTMMAPPING Where MTMMappingID='" + ID + "'");
                o.createCommand("Select * From OPICINF.KKB_TB_MTMMAPPING Where MTMMappingID='" + ID + "'", CommandType.Text);
                s = o.executeReaderToDT("TB_MTMMAPPING");
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_MS_MTMMAPPING Finish-----");
                o.closeConnection();
            }
            return s;
        }

        public Boolean AddMTMMapping(string ConnectionString)
        {
            String sql = "Insert Into OPICINF.KKB_TB_MTMMAPPING(Name, Data_1, Data_2, Data_3, Data_4) Values('" + this.Name + "','" + this.Data1.Replace("'", "''") + "','" + this.Data2.Replace("'", "''") + "','" + this.Data3.Replace("'", "''") + "','" + this.Data4.Replace("'", "''") + "')";
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
                //Log.Info(s.GetXml());
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

        public Boolean UpdateMTMMapping(string ConnectionString)
        {
            String sql = "Update OPICINF.KKB_TB_MTMMAPPING  Set Name='" + this.Name + "', Data_1='" + this.Data1.Replace("'", "''") + "', Data_2='" + this.Data2.Replace("'", "''") + "', Data_3='" + this.Data3.Replace("'", "''") + "', Data_4='" + this.Data4.Replace("'", "''") + "' Where MTMMappingID='" + this.MTMMappingID + "'";
            Log.Info("-----Model_MS_MTMMAPPING Start Update-----");
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
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Update Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_MS_MTMMAPPING Finish-----");
                o.closeConnection();
            }
        }

        public Boolean DeleteMTMMapping(string ConnectionString)
        {
            String sql = "Delete From OPICINF.KKB_TB_MTMMAPPING Where MTMMappingID='" + this.MTMMappingID + "'";
            Log.Info("-----Model_MS_MTMMAPPING Start Delete-----");
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
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete Model Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----Model_MS_MTMMAPPING Finish-----");
                o.closeConnection();
            }
        }
    }
}
