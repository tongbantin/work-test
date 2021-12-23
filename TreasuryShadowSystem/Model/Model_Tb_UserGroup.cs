using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KKB.Treasury.TreasuryCommon.Common;
using TreasuryShadowSystem.Configuration;
using log4net;

namespace TreasuryShadowSystem.Model
{
    public class Model_Tb_UserGroup
    {
        public string UserGroupID { get; set; }
        public string UserGroupName { get; set; }
        public string DepCode { get; set; }
        public string BU { get; set; }
        ILog Log = LogManager.GetLogger(typeof(Model_Tb_UserGroup));

        public static DataSet getUserGroups()
        {
            DataSet ds = new DataSet();
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From " + Config.Owner + ".KKB_TB_MS_USERGROUP Order By USERGROUP_ID", CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                
            }
            finally {
                o.closeConnection();
            }
            return ds;
        }

        public bool HasData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Select USERGROUP_ID From " + Config.Owner + ".KKB_TB_MS_USERGROUP Where USERGROUP_NAME='" + this.UserGroupName.Trim() + "' ORDER BY USERGROUP_ID";
                o.createCommand(sql, CommandType.Text);
                DataSet ds = o.executeReader();
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return true;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public bool InsertData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Insert Into " + Config.Owner + ".KKB_TB_MS_USERGROUP(USERGROUP_NAME, DEP_CODE, BU, CREATED_BY, CREATED_DATE) values( '" + this.UserGroupName + "', '" + this.DepCode + "', '" + this.BU + "', 'ADMIN', SYSDATE)";
                o.createCommand(sql, CommandType.Text);
                int result = o.executeNonQuery();
                if (result > 0)
                    return true;
                else
                {
                    Log.Error(sql);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public bool DeleteData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Delete From " + Config.Owner + ".KKB_TB_MS_USERGROUP WHERE USERGROUP_ID='" + this.UserGroupID + "'", CommandType.Text);
                int result = o.executeNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public bool UpdateData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Update " + Config.Owner + ".KKB_TB_MS_USERGROUP Set USERGROUP_NAME='" + this.UserGroupName + "', DEP_CODE='" + this.DepCode + "', BU='" + this.BU + "', UPDATED_BY='ADMIN', LASTUPDATED_DATE=SYSDATE WHERE USERGROUP_ID='" + this.UserGroupID + "'", CommandType.Text);
                int result = o.executeNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                o.closeConnection();
            }
        }

    }
}
