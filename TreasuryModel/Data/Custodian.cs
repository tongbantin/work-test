using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using TreasuryModel.Master;
using log4net;

namespace TreasuryModel.Data
{
    public class Custodian
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Custodian));

        //OPICINF
        public int CustodianID { get; set; }
        public string Name { get; set; }
        public string BIC { get; set; }
        public string AccountNo { get; set; }


        public static DataTable getCustodianByID(string ConnectionString, String id)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_CUSTODIAN WHERE CUSTODIANID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Custodian Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Custodian Finish-----");
                o.closeConnection();
            }
        }


        public static DataTable getCustodianByCondition(string ConnectionString, String Name)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_CUSTODIAN WHERE Name like '%" + Name + "%'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("----- Custodian Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("----- Custodian Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Create(string ConnectionString)
        {
            String sql = "Insert Into OPICINF.KKB_TB_CUSTODIAN(Name, BIC, ACCTNO, CreateBy, CreateDate) Values('" + this.Name + "','" + this.BIC + "','" + this.AccountNo + "','',sysdate)";
            Log.Info("-----KKB_TB_CUSTODIAN Start insert-----");
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
                Log.Error("-----Insert KKB_TB_CUSTODIAN Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTODIAN Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Update(string ConnectionString)
        {
            String sql = "Update OPICINF.KKB_TB_CUSTODIAN Set Name='" + this.Name + "', BIC='" + this.BIC + "', ACCTNO='" + this.AccountNo + "' Where CUSTODIANID='" + this.CustodianID + "'";
            Log.Info("-----KKB_TB_CUSTODIAN Start Update-----");
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
                Log.Error("-----Update KKB_TB_CUSTODIAN Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTODIAN Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Delete(string ConnectionString)
        {
            String sql = "Delete From OPICINF.KKB_TB_CUSTODIAN Where CustodianID='" + this.CustodianID + "'";
            Log.Info("-----KKTB_TB_CUSTOMER Start Delete-----");
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
                Log.Error("-----Delete KKB_TB_CUSTODIAN Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTODIAN Finish-----");
                o.closeConnection();
            }
        }
    }
}
