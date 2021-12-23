using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class CustTypeMapping
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(CustTypeMapping));

        public int CUSTTYPEMAPPINGID { get; set; }
        public string BOTTYPE { get; set; }
        public string BOTTYPEDESC { get; set; }
        public string SECTYPE { get; set; }
        public string SECTYPEDESC { get; set; }

        public static object CustTypeMappingList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<CustTypeMapping> param = new List<CustTypeMapping>();
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_CUSTTYPE_MAPPING Order By BOTTYPE";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("CustTypeMapping");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CustTypeMapping m = new CustTypeMapping();
                    m.CUSTTYPEMAPPINGID = int.Parse(dt.Rows[i]["CUSTTYPEMAPPINGID"].ToString());
                    m.BOTTYPE = dt.Rows[i]["BOTTYPE"].ToString();
                    m.BOTTYPEDESC = dt.Rows[i]["BOTTYPEDESC"].ToString();
                    m.SECTYPE = dt.Rows[i]["SECTYPE"].ToString();
                    m.SECTYPEDESC = dt.Rows[i]["SECTYPEDESC"].ToString();
                    param.Add(m);
                }

                var rec = param.Count > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList<CustTypeMapping>() : param.ToList<CustTypeMapping>();

                return new { Result = "OK", Records = rec, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return new { Result = "OK", Records = param, TotalRecordCount = param.Count };
            }
        }

        public static object CreateCustTypeMapping(string ConnectionString, CustTypeMapping record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_CUSTTYPE_MAPPING(BOTTYPE, BOTTYPEDESC, SECTYPE, SECTYPEDESC) ";
            sql += " Values( :BOTTYPE, :BOTTYPEDESC, :SECTYPE, :SECTYPEDESC)";

            Log.Info("-----KKB_TB_CUSTTYPE_MAPPING Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("BOTTYPE", record.BOTTYPE);
                o.setVarCharInputParameter("BOTTYPEDESC", record.BOTTYPEDESC);
                o.setVarCharInputParameter("SECTYPE", record.SECTYPE);
                o.setVarCharInputParameter("SECTYPEDESC", record.SECTYPEDESC);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKB_TB_CUSTTYPE_MAPPING Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPE_MAPPING Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdateCustTypeMapping(string ConnectionString, CustTypeMapping record)
        {
            String sql = "Update OPICINF.KKB_TB_CUSTTYPE_MAPPING Set BOTTYPE=:BOTTYPE, BOTTYPEDESC=:BOTTYPEDESC, SECTYPE=:SECTYPE, SECTYPEDESC=:SECTYPEDESC Where CUSTTYPEMAPPINGID=:CUSTTYPEMAPPINGID ";

            Log.Info("-----KKB_TB_CUSTTYPE_MAPPING Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("BOTTYPE", record.BOTTYPE);
                o.setVarCharInputParameter("BOTTYPEDESC", record.BOTTYPEDESC);
                o.setVarCharInputParameter("SECTYPE", record.SECTYPE);
                o.setVarCharInputParameter("SECTYPEDESC", record.SECTYPEDESC);
                o.setIntegerInputParameter("CUSTTYPEMAPPINGID", record.CUSTTYPEMAPPINGID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKB_TB_CUSTTYPE_MAPPING Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPE_MAPPING Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteCustTypeMapping(string ConnectionString, int CUSTTYPEMAPPINGID)
        {
            String sql = "Delete From OPICINF.KKB_TB_CUSTTYPE_MAPPING Where CUSTTYPEMAPPINGID=:CUSTTYPEMAPPINGID ";

            Log.Info("-----KKB_TB_CUSTTYPE_MAPPING Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("CUSTTYPEMAPPINGID", CUSTTYPEMAPPINGID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_CUSTTYPE_MAPPING Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_CUSTTYPE_MAPPING Finish-----");
                o.closeConnection();
            }
        }
    }
}
