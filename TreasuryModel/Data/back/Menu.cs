using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;

namespace TreasuryModel.Data
{
    public class Menu
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Menu));

        public int ID { get; set; }
        public string Data_Type { get; set; }
        public string Data_Code { get; set; }
        public string Data_Name_Th { get; set; }
        public string Data_Name_Eng { get; set; }
        public string File_Type { get; set; }
        public int ParentID { get; set; }
        public string Path_URL { get; set; }
        public int Seq { get; set; }
        public string Enabled { get; set; }
        public int GroupID { get; set; }

        public static object MenuList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<Menu> param = new List<Menu>();
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_MS_LOOKUP Order By ID";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("Menu");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Menu m = new Menu();
                    m.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    m.Data_Type = dt.Rows[i]["Data_Type"].ToString();
                    m.Data_Code = dt.Rows[i]["Data_Code"].ToString();
                    m.Data_Name_Th = dt.Rows[i]["Data_Name_Th"].ToString();
                    m.Data_Name_Eng = dt.Rows[i]["Data_Name_Eng"].ToString();
                    m.File_Type = dt.Rows[i]["File_Type"].ToString();
                    if (dt.Rows[i]["ParentID"] == null)
                        m.ParentID = 0;
                    else
                    {
                        if (dt.Rows[i]["ParentID"].ToString().Trim().Equals(""))
                            m.ParentID = 0;
                        else
                            m.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                    }
                    m.Path_URL = dt.Rows[i]["Path_URL"].ToString();
                    if (dt.Rows[i]["Seq"] == null)
                        m.Seq = 0;
                    else
                    {
                        if (dt.Rows[i]["Seq"].ToString().Trim().Equals(""))
                            m.Seq = 0;
                        else
                            m.Seq = int.Parse(dt.Rows[i]["Seq"].ToString());
                    }
                    m.Enabled = dt.Rows[i]["Enabled"].ToString();
                    if (dt.Rows[i]["GroupID"] == null)
                        m.GroupID = 0;
                    else
                    {
                        if (dt.Rows[i]["GroupID"].ToString().Trim().Equals(""))
                            m.GroupID = 0;
                        else
                            m.GroupID = int.Parse(dt.Rows[i]["GroupID"].ToString());
                    }
                    param.Add(m);
                }

                var rec = param.Count > 0 ? param.Skip(jtStartIndex).Take(jtPageSize).ToList<Menu>() : param.ToList<Menu>();  

                return new { Result = "OK", Records = rec, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return new { Result = "OK", Records = param, TotalRecordCount = param.Count };
            }
        }

        public static List<Menu> ParentList(string ConnectionString)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            List<Menu> param = new List<Menu>();
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_MS_LOOKUP Where ParentID=1 Order By ID";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("Menu");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Menu m = new Menu();
                    m.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    m.Data_Type = dt.Rows[i]["Data_Type"].ToString();
                    m.Data_Code = dt.Rows[i]["Data_Code"].ToString();
                    m.Data_Name_Th = dt.Rows[i]["Data_Name_Th"].ToString();
                    m.Data_Name_Eng = dt.Rows[i]["Data_Name_Eng"].ToString();
                    m.File_Type = dt.Rows[i]["File_Type"].ToString();
                    if (dt.Rows[i]["ParentID"] == null)
                        m.ParentID = 0;
                    else
                    {
                        if (dt.Rows[i]["ParentID"].ToString().Trim().Equals(""))
                            m.ParentID = 0;
                        else
                            m.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
                    }
                    m.Path_URL = dt.Rows[i]["Path_URL"].ToString();
                    if (dt.Rows[i]["Seq"] == null)
                        m.Seq = 0;
                    else
                    {
                        if (dt.Rows[i]["Seq"].ToString().Trim().Equals(""))
                            m.Seq = 0;
                        else
                            m.Seq = int.Parse(dt.Rows[i]["Seq"].ToString());
                    }
                    m.Enabled = dt.Rows[i]["Enabled"].ToString();
                    if (dt.Rows[i]["GroupID"] == null)
                        m.GroupID = 0;
                    else
                    {
                        if (dt.Rows[i]["GroupID"].ToString().Trim().Equals(""))
                            m.GroupID = 0;
                        else
                            m.GroupID = int.Parse(dt.Rows[i]["GroupID"].ToString());
                    }
                    param.Add(m);
                }

                return param;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object CreateMenu(string ConnectionString, Menu record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_MS_LOOKUP(Data_Type, Data_Code, Data_Name_Th, Data_Name_Eng, ParentID, Path_URL, Seq, Enabled, GroupID) ";
            sql += " Values( :DataType, :DataCode, :DataNameTh, :DataNameEng, :ParentID, :PathURL, :Seq, :Enabled, :GroupID)";

            Log.Info("-----KKB_TB_MS_LOOKUP Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("DataType", record.Data_Type);
                o.setVarCharInputParameter("DataCode", record.Data_Code);
                o.setVarCharInputParameter("DataNameTh", record.Data_Name_Th);
                o.setVarCharInputParameter("DataNameEng", record.Data_Name_Eng);
                o.setIntegerInputParameter("ParentID", record.ParentID);
                o.setVarCharInputParameter("PathURL", record.Path_URL);
                o.setIntegerInputParameter("Seq", record.Seq);
                o.setIntegerInputParameter("Enabled", record.Enabled);
                o.setIntegerInputParameter("GroupID", record.GroupID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not insert data." };
                //Log.Info(s.GetXml());
                //var addedStudent = _repository.StudentRepository.AddStudent(record);
                //return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_MS_LOOKUP Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_MS_LOOKUP Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdateMenu(string ConnectionString, Menu record)
        {
            String sql = "Update OPICINF.KKB_TB_MS_LOOKUP Set Data_Type=:DataType, Data_Code=:DataCode, Data_Name_Th=:DataNameTh, Data_Name_Eng=:DataNameEng, ParentID=:ParentID, Path_URL=:PathURL, Seq=:Seq, Enabled=:Enabled, GroupID=:GroupID Where ID=:MenuID ";

            Log.Info("-----KKB_TB_MS_LOOKUP Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("DataType", record.Data_Type);
                o.setVarCharInputParameter("DataCode", record.Data_Code);
                o.setVarCharInputParameter("DataNameTh", record.Data_Name_Th);
                o.setVarCharInputParameter("DataNameEng", record.Data_Name_Eng);
                o.setIntegerInputParameter("ParentID", record.ParentID);
                o.setVarCharInputParameter("PathURL", record.Path_URL);
                o.setIntegerInputParameter("Seq", record.Seq);
                o.setVarCharInputParameter("Enabled", record.Enabled);
                o.setIntegerInputParameter("GroupID", record.GroupID);
                o.setIntegerInputParameter("MenuID", record.ID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_MS_LOOKUP Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_WEBPARAM Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteMenu(string ConnectionString, int MenuID)
        {
            String sql = "Delete From OPICINF.KKB_TB_MS_LOOKUP Where ID=:MenuID ";

            Log.Info("-----KKB_TB_MS_LOOKUP Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("MenuID", MenuID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_MS_LOOKUP Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_MS_LOOKUP Finish-----");
                o.closeConnection();
            }
        }
    }
}
