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
    public class Model_Tb_Menu 
    {
        public String MenuID { get; set; }
        public String DataCode { get; set; }
        public String DataNameEng { get; set; }
        public String DataType { get; set; }
        public String MasterMenu { get; set; }
        public String MenuLevel { get; set; }
        public String MenuMappID { get; set; }
        public String UnderMenu { get; set; }
        public String PathUrl { get; set; }
        public String Seq { get; set; }
        public String Enabled { get; set; }

        ILog Log = LogManager.GetLogger(typeof(Model_Tb_Menu));
        public Model_Tb_Menu()
        {

        }
        public static DataRow getMenu(String ID)
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From " + Config.Owner + ".KKB_TB_MS_LOOKUP Where ID = '" + ID + "'", CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                o.closeConnection();
            }
            return ds.Tables[0].Select()[0];
        }
        
        public static DataSet getMenus()
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select m.*, (Select DATA_NAME_ENG From " + Config.Owner + ".KKB_TB_MS_LOOKUP Where ID=m.PARENTID) PARENT_MENU From " + Config.Owner + ".KKB_TB_MS_LOOKUP m Where DATA_TYPE IN ('REPORT','MENU') Order BY ID", CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }
        public static DataSet getMenus(string MenuType)
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select * From " + Config.Owner + ".KKB_TB_MS_LOOKUP Where DATA_TYPE='" + MenuType + "' Order BY ID", CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }

        public static DataSet getMenusByParentId(string MenuId)
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                if (String.IsNullOrEmpty(MenuId)||MenuId=="-1")
                    o.createCommand("Select * From " + Config.Owner + ".KKB_TB_MS_LOOKUP Where GROUPID=1 AND GROUPID IS NOT NULL Order BY ID", CommandType.Text);
                else
                    o.createCommand("Select * From " + Config.Owner + ".KKB_TB_MS_LOOKUP Where GROUPID=" + MenuId + " AND GROUPID IS NOT NULL Order BY ID", CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }
        public static DataSet getMenusWithoutSelf(string Menu)
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                string sql = "Select l.* From " + Config.Owner + ".KKB_TB_MS_LOOKUP l  Order BY SEQ";
                if (Menu!="")
                    sql += "Where ID != " + Menu;
                o.createCommand(sql, CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }
        public static DataSet getMasterMenus()
        {
            DataSet ds = null;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Select m.MENU_MAPP_ID, l.DATA_NAME_ENG From " + Config.Owner + ".KKB_TB_MS_LOOKUP l, " + Config.Owner + ".KKB_TB_MS_MENU_RPT_MAPP m Where m.MENU_ID=l.ID AND l.DATA_TYPE='MENU' AND m.MASTER_MENU IS NULL Order BY ID", CommandType.Text);
                ds = o.executeReader();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                o.closeConnection();
            }
            return ds;
        }


        public bool InsertData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.beginTransaction();
                o.createTransaction("Insert Into " + Config.Owner + ".KKB_TB_MS_LOOKUP(DATA_CODE, DATA_NAME_ENG, DATA_TYPE, PATH_URL, PARENTID, SEQ, CREATED_BY, CREATED_DATE, ENABLED, ID) values('" + this.DataCode + "', '" + this.DataNameEng + "', '" + this.DataType + "', '" + this.PathUrl + "', '" + this.UnderMenu + "', " + this.Seq + ", 'ADMIN', SYSDATE, '" + this.Enabled + "', " + Config.Owner + ".KKB_TB_MS_LOOKUP_SEQ.nextval)", CommandType.Text);
                int result = o.executeNonQuery();
                if (result > 0)
                {
                    o.commitTransaction();
                    return true;
                }
                else
                {
                    o.rollbackTransaction();
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


        public bool UpdateData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Update " + Config.Owner + ".KKB_TB_MS_LOOKUP Set DATA_CODE='" + this.DataCode + "', DATA_NAME_ENG='" + this.DataNameEng + "', DATA_TYPE='" + this.DataType + "', PATH_URL='" + this.PathUrl + "', PARENTID='" + this.UnderMenu + "', SEQ=" + this.Seq + ", UPDATED_BY='ADMIN', LASTUPDATED_DATE=SYSDATE, ENABLED='" + this.Enabled + "' WHERE ID='" + this.MenuID + "'", CommandType.Text);
                int result = o.executeNonQuery();
                if (result>0)
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

        public bool DeleteData()
        {
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("Delete From " + Config.Owner + ".KKB_TB_MS_LOOKUP WHERE ID='" + this.MenuID + "'", CommandType.Text);
                int result = o.executeNonQuery();
                if (result>0) 
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
