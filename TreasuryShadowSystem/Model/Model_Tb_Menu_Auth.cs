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
using System.Collections;

using KKB.Treasury.TreasuryCommon.Common;
using TreasuryShadowSystem.Configuration;
using log4net;

namespace TreasuryShadowSystem.Model
{
    public class Model_Tb_Menu_Auth
    {
        public string Menu{ get; set; }
        public string MenuName { get; set; }
        public string UserGroup { get; set; }
        public string UserGroupName { get; set; }
        public ArrayList MenuAuth { get; set; }

        protected static ILog Log = LogManager.GetLogger(typeof(Model_Tb_Menu_Auth));

        public static bool IsAccess(DataSet ds, String page)
        {
            try
            {
                string[] p = page.Split('/');
                DataRow[] dr = ds.Tables[0].Select("PATH_URL like '%" + "" + p[p.Count() - 1].ToString() + "'");
                Log.Info("IsAccess : '" + p[p.Count() - 1].ToString() + "'");
                if (dr.Count() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataSet GetAuthenticationMenu(string USERGROUP)
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            string sql = "";
            try
            {
                o.openConnection();
                sql = "Select l.* From " + Config.Owner + ".KKB_TB_MS_LOOKUP l, " + Config.Owner + ".KKB_TB_MS_MENU_AUTH a Where l.ID=a.MENU_ID And l.ENABLED='1' And a.USERGROUP_ID=(SELECT USERGROUP_ID FROM " + Config.Owner + ".KKB_TB_MS_USERGROUP WHERE USERGROUP_NAME = '" + USERGROUP + "') ";
                sql += " Order By l.GroupID, l.SEQ";
                Log.Info(sql);
                o.createCommand(sql, CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }

        public static DataSet GetByUserGroupID(string USERGROUP_ID)
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            string sql = "";
            try
            {
                o.openConnection();
                sql = "Select l.*,  b.USERGROUP_ID, b.USERGROUP_NAME From " + Config.Owner + ".KKB_TB_MS_LOOKUP l, " + Config.Owner + ".KKB_TB_MS_MENU_AUTH a, " + Config.Owner + ".KKB_TB_MS_USERGROUP b Where l.ID=a.MENU_ID And l.ENABLED='1' And a.USERGROUP_ID= b.USERGROUP_ID(+) And a.USERGROUP_ID = " + USERGROUP_ID + " ";
                sql += " Order By l.SEQ";
                o.createCommand(sql, CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }

       

        public bool UpdateData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.beginTransaction();
                Model_Tb_Menu_Auth md = (Model_Tb_Menu_Auth)this.MenuAuth[0];
                string sql1 = "Delete From " + Config.Owner + ".KKB_TB_MS_MENU_AUTH WHERE USERGROUP_ID='" + md.UserGroup + "'";
                Log.Info("Execute : " + sql1);
                o.createTransaction(sql1, CommandType.Text);
                int delresult = o.executeNonQuery();
                foreach (Model_Tb_Menu_Auth m in this.MenuAuth)
                {
                    string sql2 = "Insert Into " + Config.Owner + ".KKB_TB_MS_MENU_AUTH(MENU_ID, USERGROUP_ID, INQUIRY_FLAG, CREATED_BY, CREATED_DATE) values('" + m.Menu + "', '" + m.UserGroup + "', 'Y', 'ADMIN', SYSDATE)";
                    Log.Info("Execute : " + sql2);
                    o.createTransaction(sql2, CommandType.Text);
                    int result = o.executeNonQuery();
                    if (result < 0)
                    {                        
                        o.rollbackTransaction();
                        return false;
                    }
                }
                o.commitTransaction();
                return true;                
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static DataSet GetMenu(string USERGROUP)
        {
            try
            {
                DataSet ds = null;
                Oracle o = Oracle.getInstance(Config.ConnectionString);
                string sql = "";
                try
                {
                    o.openConnection();
                    sql = "Select p.* From (";
                    sql += "Select l.* From " + Config.Owner + ".KKB_TB_MS_LOOKUP l, " + Config.Owner + ".KKB_TB_MS_MENU_AUTH a Where l.ID=a.MENU_ID And l.ENABLED='1' And a.USERGROUP_ID=(SELECT USERGROUP_ID FROM " + Config.Owner + ".KKB_TB_MS_USERGROUP WHERE USERGROUP_NAME = '" + USERGROUP + "') ";
                    sql += "UNION ";
                    sql += "Select * From OPICINF.KKB_TB_MS_LOOKUP WHERE ENABLED='1' AND ID IN (Select DISTINCT GROUPID From " + Config.Owner + ".KKB_TB_MS_LOOKUP l, " + Config.Owner + ".KKB_TB_MS_MENU_AUTH a Where l.ID=a.MENU_ID And l.ENABLED='1' And a.USERGROUP_ID=(SELECT USERGROUP_ID FROM " + Config.Owner + ".KKB_TB_MS_USERGROUP WHERE USERGROUP_NAME = '" + USERGROUP + "')) ";
                    sql += ") p Order By p.GroupID, p.SEQ ";
                    Log.Info(sql);
                    o.createCommand(sql, CommandType.Text);
                    ds = o.executeReader();
                }
                catch (Exception ex)
                {
                    Log.Error("----Start Error-----");
                    Log.Error(ex.Message);
                }
                finally
                {
                    o.closeConnection();
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
