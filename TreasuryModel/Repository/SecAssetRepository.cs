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
    public class SecAssetRepository
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(SecAssetRepository));
        public static List<SecAsset> SecAssetList(string ConnectionString, string secid, string assetgroupid, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<SecAsset> param = new List<SecAsset>();
            try
            {
                string sql = "Select SECASSETID, SECID, QTY, DEPOSITQTY, TO_CHAR(MDATE,'DD/MM/YYYY') as MDATE, ASSETGROUPID, SCRIPLESS, SECTYPE, ENABLED From OPICINF.KKB_TB_SECASSET WHERE SECID like '%" + secid + "%' ";
                if (assetgroupid.Trim()=="0")
                    sql += "AND ASSETGROUPID IS NULL ";
                else if (assetgroupid.Trim()!="")
                    sql += "AND ASSETGROUPID='" + assetgroupid.Trim() + "' ";
                sql += "AND ROWNUM<250 ";
                sql += " Order By SECID ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("KB_TB_SECASSET");

                List<SecAsset> l = new List<SecAsset>();
                if (dt != null)
                {
                    l = dt.AsEnumerable().Select(row => new SecAsset
                    {
                        SECASSETID = int.Parse(row["SECASSETID"].ToString()),
                        SECID = row[1].ToString(),
                        QTY = decimal.Parse(row["QTY"].ToString()),
                        DEPOSITQTY = decimal.Parse(row["DEPOSITQTY"].ToString()),
                        MDATE = (row["MDATE"]==null) || (row["MDATE"].ToString().Length<10) ? new DateTime(1900,1,1) : new DateTime(int.Parse(row["MDATE"].ToString().Substring(6, 4)), int.Parse(row["MDATE"].ToString().Substring(3, 2)), int.Parse(row["MDATE"].ToString().Substring(0, 2))),
                        ASSETGROUPID = int.Parse((row["ASSETGROUPID"]==null) || (row["ASSETGROUPID"].ToString().Equals("")) ? "0" : row["ASSETGROUPID"].ToString()),
                        SCRIPLESS = row["SCRIPLESS"].ToString(),
                        SECTYPE = row["SECTYPE"].ToString(),
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

        public static int CreateSecAsset(string ConnectionString, SecAsset record)
        {
            String sql = "INSERT INTO OPICINF.KKB_TB_SECASSET(SECID, QTY, DEPOSITQTY, MDATE, ASSETGROUPID, SECTYPE, CREATEBY, CREATEDATE, ENABLED) ";
            sql += " VALUES( :SECID, :QTY, :DEPOSITQTY, TO_DATE(:MDATE,'DD/MM/YYYY'), :ASSETGROUPID, :SECTYPE, :CREATEBY, TO_DATE(:CREATEDATE,'DD/MM/YYYY HH:MI:SS'), :ENABLED)";

            Log.Info("-----KKB_TB_SECASSET Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setCharInputParameter("SECID", record.SECID);
                o.setDecimalInputParameter("QTY", record.QTY);
                o.setDecimalInputParameter("DEPOSITQTY", record.DEPOSITQTY);
                o.setVarCharInputParameter("MDATE", record.MDATE.ToString("dd/MM/yyyy"));
                o.setIntegerInputParameter("ASSETGROUPID", record.ASSETGROUPID);
                o.setVarCharInputParameter("SECTYPE", record.SECTYPE);
                o.setVarCharInputParameter("CREATEBY", record.CREATEBY);
                o.setVarCharInputParameter("CREATEDATE", record.CREATEDATE.ToString("dd/MM/yyyy hh:mm:ss"));
                o.setCharInputParameter("ENABLED", record.ENABLED);
                row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_SECASSET Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_SECASSET Finish-----");
                o.closeConnection();
            }
        }

        public static int UpdateSecAsset(string ConnectionString, SecAsset record)
        {
            String sql = "Update OPICINF.KKB_TB_SECASSET Set SECID=:SECID, QTY=:QTY, DEPOSITQTY=:DEPOSITQTY, MDATE=TO_DATE(:MDATE,'DD/MM/YYYY'), ASSETGROUPID=:ASSETGROUPID, SCRIPLESS=:SCRIPLESS, SECTYPE=:SECTYPE, UPDATEBY=:UPDATEBY, UPDATEDATE=TO_DATE(:UPDATEDATE,'DD/MM/YYYY HH:MI:SS'), ENABLED=:ENABLED Where SECASSETID=:SECASSETID";

            Log.Info("-----KKB_TB_SECASSET Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setCharInputParameter("SECID", record.SECID.Trim());
                o.setDecimalInputParameter("QTY", record.QTY);
                o.setDecimalInputParameter("DEPOSITQTY", record.DEPOSITQTY);
                o.setVarCharInputParameter("MDATE", record.MDATE.ToString("dd/MM/yyyy"));
                o.setIntegerInputParameter("ASSETGROUPID", record.ASSETGROUPID);
                o.setCharInputParameter("SCRIPLESS", record.SCRIPLESS);
                o.setVarCharInputParameter("SECTYPE", record.SECTYPE);
                o.setVarCharInputParameter("UPDATEBY", record.UPDATEBY);
                o.setVarCharInputParameter("UPDATEDATE", record.UPDATEDATE.ToString("dd/MM/yyyy hh:mm:ss"));
                o.setCharInputParameter("ENABLED", record.ENABLED);
                o.setIntegerInputParameter("SECASSETID", record.SECASSETID);
                row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_SECASSET Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_SECASSET Finish-----");
                o.closeConnection();
            }
        }

        public static int DeleteSecAsset(string ConnectionString, int SECASSETID)
        {
            String sql = "Delete From OPICINF.KKB_TB_SECASSET Where SECASSETID=:SECASSETID ";

            Log.Info("-----KKB_TB_SECASSET Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("SECASSETID", SECASSETID);
                row = o.executeNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_SECASSET Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return 0;
            }
            finally
            {
                Log.Info("-----KKB_TB_SECASSET Finish-----");
                o.closeConnection();
            }
        }

        public static int LoadSecAsset(string ConnectionString)
        {
            String sql = "OPICINF.KKB_SP_TSS_TR130TRANSFER";

            Log.Info("-----KKB_TB_SECASSET Start Load-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.StoredProcedure);
                row = o.executeNonQuery();
                Log.Error("EXECUTE KKB_TB_SECASSET=" + row);
                return row;
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_SECASSET Error-----");
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return -99;
            }
            finally
            {
                Log.Info("-----KKB_TB_SECASSET Finish-----");
                o.closeConnection();
            }
        }
    }
}
