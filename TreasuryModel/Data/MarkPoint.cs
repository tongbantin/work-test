using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;


namespace TreasuryModel.Data
{
    public class MarkPoint
    {
        public int CONFIGPOINTID { get; set; }
        public string FUND { get; set; }
        public string CCY { get; set; }
        public string CTRCCY { get; set; }
        public int SWAP_MID_POINT { get; set; }
        public int SPOT_MID_POINT { get; set; }
        public int OUTRIGHT_MID_POINT { get; set; }

        private static ILog Log = log4net.LogManager.GetLogger(typeof(MarkPoint));


        public static object PointList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_MTMCONFIGPOINT";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("MTMConfigPoint");

                List<MarkPoint> param = new List<MarkPoint>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MarkPoint m = new MarkPoint();
                    m.CONFIGPOINTID = int.Parse(dt.Rows[i]["CONFIGPOINTID"].ToString());
                    m.FUND = dt.Rows[i]["FUND"].ToString();
                    m.CCY = dt.Rows[i]["CCY"].ToString();
                    m.CTRCCY = dt.Rows[i]["CTRCCY"].ToString();
                    m.SWAP_MID_POINT = int.Parse(dt.Rows[i]["SWAP_MID_POINT"].ToString());
                    m.SPOT_MID_POINT = int.Parse(dt.Rows[i]["SPOT_MID_POINT"].ToString());
                    m.OUTRIGHT_MID_POINT = int.Parse(dt.Rows[i]["OUTRIGHT_MID_POINT"].ToString());
                    param.Add(m);
                }

                return new { Result = "OK", Records = jtPageSize > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList() : param, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object CreatePoint(string ConnectionString, MarkPoint record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_MTMCONFIGPOINT(FUND, CCY, CTRCCY, SWAP_MID_POINT, SPOT_MID_POINT, OUTRIGHT_MID_POINT) ";
            sql += " Values( :FUND, :CCY, :CTRCCY, :SWAP_MID_POINT, :SPOT_MID_POINT, :OUTRIGHT_MID_POINT)";

            Log.Info("-----KKB_TB_MTMCONFIGPOINT Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("FUND", record.FUND);
                o.setVarCharInputParameter("CCY", record.CCY);
                o.setVarCharInputParameter("CTRCCT", record.CTRCCY);
                o.setIntegerInputParameter("SWAP_MID_POINT", record.SWAP_MID_POINT);
                o.setIntegerInputParameter("SPOT_MID_POINT", record.SPOT_MID_POINT);
                o.setIntegerInputParameter("OUTRIGHT_MID_POINT", record.OUTRIGHT_MID_POINT);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_MTMCONFIGPOINT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_MTMCONFIGPOINT Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdatePoint(string ConnectionString, MarkPoint record)
        {
            String sql = "Update OPICINF.KKB_TB_MTMCONFIGPOINT Set FUND=:FUND, CCY=:CCY, CTRCCY=:CTRCCY, SWAP_MID_POINT=:SWAP_MID_POINT, SPOT_MID_POINT=:SPOT_MID_POINT, OUTRIGHT_MID_POINT=:OUTRIGHT_MID_POINT Where CONFIGPOINTID=:CONFIGPOINTID ";

            Log.Info("-----KKB_TB_WEBPARAM Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("FUND", record.FUND);
                o.setVarCharInputParameter("CCY", record.CCY);
                o.setVarCharInputParameter("CTRCCT", record.CTRCCY);
                o.setIntegerInputParameter("SWAP_MID_POINT", record.SWAP_MID_POINT);
                o.setIntegerInputParameter("SPOT_MID_POINT", record.SPOT_MID_POINT);
                o.setIntegerInputParameter("OUTRIGHT_MID_POINT", record.OUTRIGHT_MID_POINT);
                o.setIntegerInputParameter("CONFIGPOINTID", record.CONFIGPOINTID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_MTMCONFIGPOINT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_MTMCONFIGPOINT Finish-----");
                o.closeConnection();
            }
        }

        public static object DeletePoint(string ConnectionString, int CONFIGPOINTID)
        {
            String sql = "Delete From OPICINF.KKB_TB_MTMCONFIGPOINT Where CONFIGPOINTID=:CONFIGPOINTID ";

            Log.Info("-----KKB_TB_MTMCONFIGPOINT Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("CONFIGPOINTID", CONFIGPOINTID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_MTMCONFIGPOINT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_MTMCONFIGPOINT Finish-----");
                o.closeConnection();
            }
        }
    }
}

