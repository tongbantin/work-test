using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using TreasuryModel.Data;
namespace TreasuryModel.Repository
{
    public class AssetGroupRepository
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(AssetGroupRepository));

        public static List<AssetGroup> AssetGroupList(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AssetGroup> param = new List<AssetGroup>();
            try
            {
                string sql = "Select ASSETGROUPID, NO, NO || ' ' || SUBSTR(DESCRIPTION,0,50) || CASE WHEN SCRIPLESS='y' THEN '(SCRIPLESS)' WHEN SCRIPLESS='n' THEN '(SCRIP)' ELSE '' END as DESCRIPTION  FROM OPICINF.KKB_TB_ASSETGROUP WHERE TLEVEL='3' Order By DESCRIPTION";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KB_TB_ASSETGROUP");

                List<AssetGroup> l = new List<AssetGroup>();
                if (dt != null)
                {
                    l = dt.AsEnumerable().Select(row => new AssetGroup
                    {
                        ASSETGROUPID = int.Parse(row["ASSETGROUPID"].ToString()),
                        NO = row["NO"].ToString(),
                        DESCRIPTION = row["DESCRIPTION"].ToString()
                    }).ToList();
                }
                return l;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return null;
            }
        }

        public static List<AssetGroup> AssetGroupList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AssetGroup> param = new List<AssetGroup>();
            try
            {
                string sql = "Select ASSETGROUPID, NO, DESCRIPTION, SUBREPORTFIX, PARENT, SUBREPORTFIX, RANGEYEAR, SCRIPLESS, TLEVEL, SUBGROUP, ENABLED From OPICINF.KKB_TB_ASSETGROUP Order By PARENT, TO_NUMBER(NVL(TRIM(NO),0))";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KB_TB_ASSETGROUP");

                List<AssetGroup> l = new List<AssetGroup>();
                if (dt != null)
                {
                    l = dt.AsEnumerable().Select(row => new AssetGroup
                    {
                        ASSETGROUPID = int.Parse(row["ASSETGROUPID"].ToString()),
                        NO = row["NO"].ToString(),
                        DESCRIPTION = row["DESCRIPTION"].ToString(),
                        SUBREPORTFIX = row["SUBREPORTFIX"].ToString(),
                        PARENT = row["PARENT"].ToString(),
                        RANGEYEAR = row["RANGEYEAR"].ToString(),
                        SCRIPLESS = row["SCRIPLESS"].ToString(),
                        TLEVEL = int.Parse(row["TLEVEL"].ToString()),
                        SUBGROUP = int.Parse(row["SUBGROUP"].ToString()),
                        ENABLED = row["ENABLED"].ToString()
                    }).ToList();
                }
                return l;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return null;
            }
        }

        public static List<AssetGroup> AssetGroupList(string ConnectionString, int TLEVEL)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AssetGroup> param = new List<AssetGroup>();
            try
            {
                string sql = "Select ASSETGROUPID, NO, DESCRIPTION, TLEVEL, PARENT, SCRIPLESS From OPICINF.KKB_TB_ASSETGROUP WHERE 1=1 ";
                if (TLEVEL>0)
                sql += "AND TLEVEL=" + TLEVEL + " ";
                sql += " Order By PARENT, TO_NUMBER(NVL(TRIM(NO),0))";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KB_TB_ASSETGROUP");

                List<AssetGroup> l = new List<AssetGroup>();
                if (dt != null)
                {
                    l = dt.AsEnumerable().Select(row => new AssetGroup
                    {
                        ASSETGROUPID = int.Parse(row["ASSETGROUPID"].ToString()),
                        NO = row["NO"].ToString(),
                        DESCRIPTION = row["DESCRIPTION"].ToString(),
                        PARENT = row["PARENT"].ToString(),
                        SCRIPLESS = row["SCRIPLESS"].ToString(),
                        TLEVEL = int.Parse(row["TLEVEL"].ToString())
                    }).ToList();                    
                }
                return l;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return null;
            }
        }

        public static AssetGroup AssetGroupByID(string ConnectionString, string AssetGroupID)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<AssetGroup> param = new List<AssetGroup>();
            try
            {
                string sql = "Select ASSETGROUPID, NO, DESCRIPTION, TLEVEL, PARENT, SCRIPLESS From OPICINF.KKB_TB_ASSETGROUP WHERE AssetGroupID='" + AssetGroupID + "' ORDER BY PARENT, TO_NUMBER(NVL(TRIM(NO),0)) ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KB_TB_ASSETGROUP");

                List<AssetGroup> l = new List<AssetGroup>();
                if (dt != null)
                {
                    l = dt.AsEnumerable().Select(row => new AssetGroup
                    {
                        ASSETGROUPID = int.Parse(row["ASSETGROUPID"].ToString()),
                        NO = row["NO"].ToString(),
                        DESCRIPTION = row["DESCRIPTION"].ToString(),
                        PARENT = row["PARENT"].ToString(),
                        SCRIPLESS = row["SCRIPLESS"].ToString(),
                        TLEVEL = int.Parse(row["TLEVEL"].ToString())
                    }).ToList();
                }
                return l[0];
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return null;
            }
        }

        public static int CreateAssetGroup(string ConnectionString, AssetGroup record)
        {
            String sql = "INSERT INTO OPICINF.KKB_TB_ASSETGROUP(NO, DESCRIPTION, SUBREPORTFIX, PARENT, RANGEYEAR, SCRIPLESS, TLEVEL, SUBGROUP, CREATEBY, CREATEDATE, ENABLED) ";
            sql += " VALUES( :NO, :DESCRIPTION, :SUBREPORTFIX, :PARENT, :RANGEYEAR, :SCRIPLESS, :TLEVEL, :SUBGROUP, :CREATEBY, TO_DATE(:CREATEDATE,'DD/MM/YYYY HH:MI:SS'), :ENABLED)";

            Log.Info("-----KKB_TB_ASSETGROUP Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("NO", record.NO);
                o.setNCharInputParameter("DESCRIPTION", record.DESCRIPTION);
                o.setCharInputParameter("SUBREPORTFIX", record.SUBREPORTFIX);
                o.setCharInputParameter("PARENT", record.PARENT);
                o.setVarCharInputParameter("RANGEYEAR", record.RANGEYEAR);
                o.setCharInputParameter("SCRIPLESS", record.SCRIPLESS);
                o.setIntegerInputParameter("TLEVEL", record.TLEVEL);
                o.setIntegerInputParameter("SUBGROUP", record.SUBGROUP);
                o.setVarCharInputParameter("CREATEBY", record.CREATEBY);
                o.setVarCharInputParameter("CREATEDATE", record.CREATEDATE.ToString("dd/MM/yyyy hh:mm:ss"));
                o.setCharInputParameter("ENABLED", record.ENABLED);
                row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_ASSETGROUP Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_ASSETGROUP Finish-----");
                o.closeConnection();
            }
        }

        public static int UpdateAssetGroup(string ConnectionString, AssetGroup record)
        {
            String sql = "Update OPICINF.KKB_TB_ASSETGROUP Set NO=:NO, DESCRIPTION=:DESCRIPTION, SUBREPORTFIX=:SUBREPORTFIX, PARENT=:PARENT, RANGEYEAR=:RANGEYEAR, SCRIPLESS=:SCRIPLESS, TLEVEL=:TLEVEL, SUBGROUP=:SUBGROUP, UPDATEBY=:UPDATEBY, UPDATEDATE=TO_DATE(:UPDATEDATE,'DD/MM/YYYY HH:MI:SS'), ENABLED=:ENABLED Where ASSETGROUPID=:ASSETGROUPID";

            Log.Info("-----KKB_TB_ASSETGROUP Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("NO", record.NO);
                o.setNCharInputParameter("DESCRIPTION", record.DESCRIPTION);
                o.setCharInputParameter("SUBREPORTFIX", record.SUBREPORTFIX);
                o.setCharInputParameter("PARENT", record.PARENT);
                o.setVarCharInputParameter("RANGEYEAR", record.RANGEYEAR);
                o.setCharInputParameter("SCRIPLESS", record.SCRIPLESS);
                o.setIntegerInputParameter("TLEVEL", record.TLEVEL);
                o.setIntegerInputParameter("SUBGROUP", record.SUBGROUP);
                o.setVarCharInputParameter("UPDATEBY", record.UPDATEBY);
                o.setVarCharInputParameter("UPDATEDATE", record.UPDATEDATE.ToString("dd/MM/yyyy hh:mm:ss"));
                o.setCharInputParameter("ENABLED", record.ENABLED);
                o.setIntegerInputParameter("ASSETGROUPID", record.ASSETGROUPID);
                row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_ASSETGROUP Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_ASSETGROUP Finish-----");
                o.closeConnection();
            }
        }

        public static int DeleteAssetGroup(string ConnectionString, int ASSETGROUPID)
        {
            String sql = "Delete From OPICINF.KKB_TB_ASSETGROUP Where ASSETGROUPID=:ASSETGROUPID ";

            Log.Info("-----KKB_TB_ASSETGROUP Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("ASSETGROUPID", ASSETGROUPID);
                row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_ASSETGROUP Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_ASSETGROUP Finish-----");
                o.closeConnection();
            }
        }
    }
}
