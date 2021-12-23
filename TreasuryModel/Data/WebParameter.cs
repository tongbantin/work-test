using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class WebParameter
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(WebParameter));

        public int ParameterID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public static object ParameterList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {   
                string sql = "Select * From OPICINF.KKB_TB_WEBPARAM";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("WebParam"); 

                List<WebParameter> param = new List<WebParameter>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WebParameter p = new WebParameter();
                    p.ParameterID = int.Parse(dt.Rows[i]["ParameterID"].ToString());
                    p.Name = dt.Rows[i]["Name"].ToString();
                    p.Value = dt.Rows[i]["Value"].ToString();
                    param.Add(p);
                }
                
                //param = param.Sort((a, b) => a.Name.CompareTo(b.Name));
                
                //Get data from database
                //int paramCount = _repository.StudentRepository.GetStudentCountByFilter(name, cityId);
                //List<ParameterModel> param = _repository.StudentRepository.GetStudentsByFilter(name, cityId, jtStartIndex, jtPageSize, jtSorting);

                //Return result to jTable
                return new { Result = "OK", Records = jtPageSize > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList() : param, TotalRecordCount = param.Count };
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object CreateParameter(string ConnectionString, WebParameter record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_WEBPARAM(Name, Value) ";
            sql += " Values( :Name, :Value)";

            Log.Info("-----KKB_TB_WEBPARAM Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("Name", record.Name);
                o.setNCharInputParameter("Value", record.Value);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record};
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
                //Log.Info(s.GetXml());
                //var addedStudent = _repository.StudentRepository.AddStudent(record);
                //return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_WEBPARAM Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_WEBPARAM Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdateParameter(string ConnectionString, WebParameter record)
        {
            String sql = "Update OPICINF.KKB_TB_WEBPARAM Set Name=:Name, Value=:Value Where ParameterID=:ParameterID ";

            Log.Info("-----KKB_TB_WEBPARAM Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("Name", record.Name);
                o.setNCharInputParameter("Value", record.Value);
                o.setIntegerInputParameter("ParameterID", record.ParameterID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK"};
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
                //Log.Info(s.GetXml());
                //var addedStudent = _repository.StudentRepository.AddStudent(record);
                //return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_WEBPARAM Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_WEBPARAM Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteParameter(string ConnectionString, int ParameterID)
        {
            String sql = "Delete From OPICINF.KKB_TB_WEBPARAM Where ParameterID=:ParameterID ";

            Log.Info("-----KKB_TB_WEBPARAM Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("ParameterID", ParameterID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
                //Log.Info(s.GetXml());
                //var addedStudent = _repository.StudentRepository.AddStudent(record);
                //return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_WEBPARAM Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_WEBPARAM Finish-----");
                o.closeConnection();
            }
        }

        public static string GetParameterByParam(string ConnectionString, string Param)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select Value From OPICINF.KKB_TB_WEBPARAM WHERE NAME='" + Param + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return o.executeScalar().ToString();
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----WebParameter Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----WebParameter Finish-----");
                o.closeConnection();
            }
        }
    }
}
