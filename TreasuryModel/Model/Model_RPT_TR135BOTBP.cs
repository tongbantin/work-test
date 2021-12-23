using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;
using System.Collections;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR135BOTBP
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR135BOTBP));

        public int TBPID { get; set; }
        public string MonthYear { get; set; }
        public decimal Assets { get; set; }
        public decimal Liabilities { get; set; }
        public decimal C970003 { get; set; }
        public decimal C970004 { get; set; }
        public decimal C970005 { get; set; }
        public decimal C970006 { get; set; }
        public decimal C970007 { get; set; }
        public decimal C970008 { get; set; }
        public decimal C970009 { get; set; }
        public decimal C970010 { get; set; }
        public decimal C970011 { get; set; }
        public decimal C970012 { get; set; }

        public Model_RPT_TR135BOTBP()
        {

        }
        public static ReportClass getReport(string ConnectionString, string ExecuteDate)
        {
            Log.Info("-----Model_RPT_TR135BOTBP Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR135BOTBP.Owner + ".KKB_SP_TSS_TR135BOTBP");

            RPT_TR135BOTBP report = new RPT_TR135BOTBP();
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR135BOTBP.Owner + ".KKB_SP_TSS_TR135BOTBP", CommandType.StoredProcedure);
                o.setInputParameter("p_execute_date", ExecuteDate);
                s = o.executeReader("ref_rpt_cur", "TB_TR135BOTBP");

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        report.SetDataSource(s);
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                    }
                    else
                    {
                        report = null;
                    }
                }
                else
                    report = null;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR135BOTBP Finish-----");
                o.closeConnection();
            }
            return report;
        }

        public static object TBPList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select TBPID, TO_CHAR(MONTHYEAR,'DD/MM/YYYY') as MONTHYEAR,";
                sql += "NVL(C970003,0) as C970003,";
                sql += "NVL(C970004,0) as C970004,";
                sql += "NVL(C970005,0) as C970005,";
                sql += "NVL(C970006,0) as C970006,";
                sql += "NVL(C970007,0) as C970007,";
                sql += "NVL(C970008,0) as C970008,";
                sql += "NVL(C970009,0) as C970009,";
                sql += "NVL(C970010,0) as C970010,";
                sql += "NVL(C970011,0) as C970011,";
                sql += "NVL(C970012,0) as C970012,";
                sql += "NVL(Assets,0) as Assets,";
                sql += "NVL(Liabilities,0) as Liabilities ";
                sql += "From OPICINF.KKB_TB_TBP ORDER BY MONTHYEAR DESC";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("WebParam");

                List<Model_RPT_TR135BOTBP> param = new List<Model_RPT_TR135BOTBP>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Model_RPT_TR135BOTBP p = new Model_RPT_TR135BOTBP();
                    p.TBPID = int.Parse(dt.Rows[i]["TBPID"].ToString());
                    p.MonthYear = dt.Rows[i]["MonthYear"].ToString();
                    p.Assets = decimal.Parse(dt.Rows[i]["Assets"].ToString());
                    p.Liabilities = decimal.Parse(dt.Rows[i]["Liabilities"].ToString());
                    p.C970003 = decimal.Parse(dt.Rows[i]["C970003"].ToString());
                    p.C970004 = decimal.Parse(dt.Rows[i]["C970004"].ToString());
                    p.C970005 = decimal.Parse(dt.Rows[i]["C970005"].ToString());
                    p.C970006 = decimal.Parse(dt.Rows[i]["C970006"].ToString());
                    p.C970007 = decimal.Parse(dt.Rows[i]["C970007"].ToString());
                    p.C970008 = decimal.Parse(dt.Rows[i]["C970008"].ToString());
                    p.C970009 = decimal.Parse(dt.Rows[i]["C970009"].ToString());
                    p.C970010 = decimal.Parse(dt.Rows[i]["C970010"].ToString());
                    p.C970011 = decimal.Parse(dt.Rows[i]["C970011"].ToString());
                    p.C970012 = decimal.Parse(dt.Rows[i]["C970012"].ToString());
                    param.Add(p);
                }

                //Get data from database
                //int paramCount = _repository.StudentRepository.GetStudentCountByFilter(name, cityId);
                //List<ParameterModel> param = _repository.StudentRepository.GetStudentsByFilter(name, cityId, jtStartIndex, jtPageSize, jtSorting);

                //Return result to jTable
                return new { Result = "OK", Records = param, TotalRecordCount = param.Count };

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object CreateTBP(string ConnectionString, Model_RPT_TR135BOTBP record)
        {
            String sql = "Insert Into OPICINF.KKB_TB_TBP(MonthYear, Assets, Liabilities, C970003, C970004, C970005, C970006, C970007, C970008, C970009, C970010, C970011, C970012) ";
            sql += " Values( TO_DATE(:MonthYear,'DD/MM/YYYY'), :Assets, :Liabilities, :C970003, :C970004, :C970005, :C970006, :C970007, :C970008, :C970009, :C970010, :C970011, :C970012)";

            Log.Info("-----KKB_TB_TBP Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("MonthYear", record.MonthYear);
                o.setDecimalInputParameter("Assets", record.Assets);
                o.setDecimalInputParameter("Liabilities", record.Liabilities);
                o.setDecimalInputParameter("C970003", record.C970003);
                o.setDecimalInputParameter("C970004", record.C970004);
                o.setDecimalInputParameter("C970005", record.C970005);
                o.setDecimalInputParameter("C970006", record.C970006);
                o.setDecimalInputParameter("C970007", record.C970007);
                o.setDecimalInputParameter("C970008", record.C970008);
                o.setDecimalInputParameter("C970009", record.C970009);
                o.setDecimalInputParameter("C970010", record.C970010);
                o.setDecimalInputParameter("C970011", record.C970011);
                o.setDecimalInputParameter("C970012", record.C970012);
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
                Log.Error("-----Insert KKTB_TB_TBP Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_TBP Finish-----");
                o.closeConnection();
            }
        }

        public static object UpdateTBP(string ConnectionString, Model_RPT_TR135BOTBP record)
        {
            String sql = "Update OPICINF.KKB_TB_TBP Set MonthYear=TO_DATE(:MonthYear,'DD/MM/YYYY'), Assets=:Assets, Liabilities=:Liabilities, C970003=:C970003, C970004=:C970004, C970005=:C970005, C970006=:C970006, C970007=:C970007, C970008=:C970008, C970009=:C970009, C970010=:C970010, C970011=:C970011, C970012=:C970012 Where TBPID=:TBPID ";

            Log.Info("-----KKB_TB_TBP Start update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setVarCharInputParameter("MonthYear", record.MonthYear);
                o.setDecimalInputParameter("Assets", record.Assets);
                o.setDecimalInputParameter("Liabilities", record.Liabilities);
                o.setDecimalInputParameter("C970003", record.C970003);
                o.setDecimalInputParameter("C970004", record.C970004);
                o.setDecimalInputParameter("C970005", record.C970005);
                o.setDecimalInputParameter("C970006", record.C970006);
                o.setDecimalInputParameter("C970007", record.C970007);
                o.setDecimalInputParameter("C970008", record.C970008);
                o.setDecimalInputParameter("C970009", record.C970009);
                o.setDecimalInputParameter("C970010", record.C970010);
                o.setDecimalInputParameter("C970011", record.C970011);
                o.setDecimalInputParameter("C970012", record.C970012);
                o.setIntegerInputParameter("TBPID", record.TBPID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not modify data." };
                //Log.Info(s.GetXml());
                //var addedStudent = _repository.StudentRepository.AddStudent(record);
                //return new { Result = "OK" };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_TBP Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_WEBPARAM Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteTBP(string ConnectionString, int TBPID)
        {
            String sql = "Delete From OPICINF.KKB_TB_TBP Where TBPID=:TBPID ";

            Log.Info("-----KKB_TB_TBP Start delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setIntegerInputParameter("TBPID", TBPID);
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
                Log.Error("-----Delete KKTB_TB_TBP Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKTB_TB_TBP Finish-----");
                o.closeConnection();
            }
        }


        public ArrayList save(string ConnectionString, List<Model_RPT_TR135BOTBP> tbp)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();
                // Disabled Old Field

                int i = 0;

                foreach (Model_RPT_TR135BOTBP record in tbp)
                {
                    try
                    {
                        string sql = "Select Count(*) From OPICINF.KKB_TB_TBP Where MonthYear=TO_DATE('" + record.MonthYear + "','DD/MM/YYYY')";
                        o.createCommand(sql, CommandType.Text);
                        int row = int.Parse(o.executeScalar().ToString());
                        if (row > 0)
                        {
                            sql = "Update OPICINF.KKB_TB_TBP Set C970003=:C970003, C970004=:C970004, C970005=:C970005, C970006=:C970006, C970007=:C970007, C970008=:C970008, C970009=:C970009, C970010=:C970010, C970011=:C970011, C970012=:C970012 Where MonthYear=TO_DATE(:MonthYear,'DD/MM/YYYY')";                            
                        }
                        else {
                            sql = "Insert Into OPICINF.KKB_TB_TBP(MonthYear, C970003, C970004, C970005, C970006, C970007, C970008, C970009, C970010, C970011, C970012) ";
                            sql += " Values( TO_DATE(:MonthYear,'DD/MM/YYYY'), :C970003, :C970004, :C970005, :C970006, :C970007, :C970008, :C970009, :C970010, :C970011, :C970012)";
                        }
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);
                        if (row <= 0)
                            o.setVarCharInputParameter("MonthYear", record.MonthYear);
                        o.setDecimalInputParameter("C970003", record.C970003);
                        o.setDecimalInputParameter("C970004", record.C970004);
                        o.setDecimalInputParameter("C970005", record.C970005);
                        o.setDecimalInputParameter("C970006", record.C970006);
                        o.setDecimalInputParameter("C970007", record.C970007);
                        o.setDecimalInputParameter("C970008", record.C970008);
                        o.setDecimalInputParameter("C970009", record.C970009);
                        o.setDecimalInputParameter("C970010", record.C970010);
                        o.setDecimalInputParameter("C970011", record.C970011);
                        o.setDecimalInputParameter("C970012", record.C970012);
                        if (row > 0)
                            o.setVarCharInputParameter("MonthYear", record.MonthYear);
                        int result = o.executeNonQuery();
                        if (result <= 0)
                        {
                            error.Add(record.TBPID);
                            i++;
                        }
                    }
                    catch (Exception ex)
                    {
                        error.Add(record.TBPID);
                        i++;
                    }
                }

                if (error.Count <= 0)
                    o.commitTransaction();
                else
                    o.rollbackTransaction();
            }
            catch (Exception ex)
            {
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
                error.Add(tbp[0].TBPID);
                o.rollbackTransaction();
            }
            finally
            {
                o.closeConnection();
            }
            return error;
        }
    }
}
