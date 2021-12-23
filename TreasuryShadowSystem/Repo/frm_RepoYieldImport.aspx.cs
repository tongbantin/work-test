using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using TreasuryShadowSystem.Configuration;
using System.Data.OleDb;
using System.Data;
using TreasuryModel.Data;
using System.Collections;
using System.Collections.Generic;
using KKB.Treasury.TreasuryModel.Model;
using CrystalDecisions.CrystalReports.Engine;

namespace TreasuryShadowSystem.Repo
{
    public partial class frm_RepoYieldImport : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_RepoYieldImport));
        private ArrayList error;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                List<RepoRefYield> l = new List<RepoRefYield>(); if ((MyFile.PostedFile.ContentType.Equals("application/octet-stream")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    MyFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=No;\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            Log.Debug("Start Read Data From Excel");
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            string myTableName = _oleConn.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [" + myTableName + "]", _oleConn);
                            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                            oleAdapter.SelectCommand = _oleCmdSelect;
                            DataTable dt = new DataTable();
                            DataColumn dc = new DataColumn("ID", typeof(int));
                            dc.AutoIncrement = true;
                            dc.AutoIncrementSeed = 1;
                            dc.AutoIncrementStep = 1;
                            dt.Columns.Add(dc);

                            oleAdapter.FillSchema(dt, SchemaType.Source);
                            oleAdapter.Fill(dt);

                            Session["RepoRefYield"] = null;
                            DateTime DataDate;
                            DateTime.TryParse(dt.Rows[0][1].ToString(),out DataDate);
                            Log.Debug("Start Add Data To Object");
                            for (int i = 2; i < dt.Rows.Count; i++)
                            {
                                if(!dt.Rows[i][8].ToString().Trim().Equals(""))
                                {
                                    RepoRefYield yield = new RepoRefYield();
                                    DateTime _maturity_date, _settlement_date;
                                    Decimal _avg_bidding, _govt_interpolated_yield, _ttm_year, _spread_quoted_date, _spread, 
                                        _reference_yield, _ai_percentage, _gross_price, _clean_price, 
                                        _modified_duration, _convexity, _index_ratio;
                                    try
                                    {
                                        yield.Records = int.Parse(dt.Rows[i]["ID"].ToString());
                                        yield.DATA_DATE = DataDate;
                                        yield.SYMBOL = dt.Rows[i][1].ToString(); //n+1

                                        if (DateTime.TryParse(dt.Rows[i][2].ToString(), out _maturity_date))
                                            yield.MATURITY_DATE = _maturity_date;
                                        if (Decimal.TryParse(dt.Rows[i][3].ToString(), out _avg_bidding))
                                            yield.AVG_BIDDING= _avg_bidding;
                                        if (Decimal.TryParse(dt.Rows[i][4].ToString(), out _govt_interpolated_yield))
                                            yield.GOVT_INTERPOLATED_YIELD = _govt_interpolated_yield;
                                        if (Decimal.TryParse(dt.Rows[i][5].ToString(), out _ttm_year))
                                            yield.TTM_YEAR = _ttm_year;
                                        if (Decimal.TryParse(dt.Rows[i][6].ToString(), out _spread_quoted_date))
                                            yield.SPREAD_QUOTED_DATE = _spread_quoted_date;
                                        if (Decimal.TryParse(dt.Rows[i][7].ToString(), out _spread))
                                            yield.SPREAD = _spread;
                                        if (Decimal.TryParse(dt.Rows[i][8].ToString(), out _reference_yield))
                                            yield.REFERENCE_YIELD = _reference_yield;
                                        if (DateTime.TryParse(dt.Rows[i][9].ToString(), out _settlement_date))
                                            yield.SETTLEMENT_DATE = _settlement_date;
                                        if (Decimal.TryParse(dt.Rows[i][10].ToString(), out _ai_percentage))
                                            yield.AI_PERCENTAGE = _ai_percentage;
                                        if (Decimal.TryParse(dt.Rows[i][11].ToString(), out _gross_price))
                                            yield.GROSS_PRICE = _gross_price;
                                        if (Decimal.TryParse(dt.Rows[i][12].ToString(), out _clean_price))
                                            yield.CLEAN_PRICE = _clean_price;
                                        if (Decimal.TryParse(dt.Rows[i][13].ToString(), out _modified_duration))
                                            yield.MODIFIED_DURATION = _modified_duration;
                                        if (Decimal.TryParse(dt.Rows[i][14].ToString(), out _convexity))
                                            yield.CONVEXITY = _convexity;
                                        if (Decimal.TryParse(dt.Rows[i][15].ToString(), out _index_ratio))
                                            yield.INDEX_RATIO = _index_ratio;
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error(ex.Message);
                                    }
                                    l.Add(yield);
                                }
                            }
                            Session["RepoRefYield"] = l;
                            BindGrid();
                            //Save To Database
                            Log.Debug("Start Insert Yield To Database");
                            RepoRefYield repo = new RepoRefYield();
                            error = repo.save(Configuration.Config.ConnectionString, l);
                            if (error.Count <= 0)
                            {
                                Log.Debug("Execute Report : " + Config.GetParameterFile("065BatchFilePath"));
                                //System.Diagnostics.Process.Start(Config.GetParameterFile("065BatchFilePath"));
                                MessageBox.Text = "ระบบสร้างไฟล์เรียบร้อยแล้ว";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                //string reportname = "Repo_Cash_Margin_" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                                //ReportClass rpt = Model_RPT_TR065DBOCM.getReport(Config.ConnectionString, "");
                                //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, Server.MapPath("~/Temporary/"+ reportname));
                                //DownloadFile(reportname, Server.MapPath("~/Temporary/" + reportname));   
                            }
                            else
                            {
                                MessageBox.Text = "ไม่สามารถบันทึกบางรายการได้ กรุณาตรวจสอบข้อมูล";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            MessageBox.Text = "ไม่สามารถบันทึกบางรายการได้ กรุณาตรวจสอบข้อมูล";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        finally
                        {
                            _oleConn.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Text = "ประเภทไฟล์ไม่ถูกต้อง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindGrid()
        {
            try
            {
                List<RepoRefYield> l = (List<RepoRefYield>)Session["RepoRefYield"];
                GridData.DataSource = l;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        //protected void DownloadFile(string filename, string fullpathfile)
        //{
        //    try
        //    {
        //        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //        //response.ClearContent();
        //        response.Clear();
        //        response.ContentType = "application/vnd.ms-excel";
        //        response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ";");
        //        response.TransmitFile(fullpathfile);
        //        response.Flush();
        //        response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //    }
        //}
    }
}
