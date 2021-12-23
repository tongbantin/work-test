using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryShadowSystem.Configuration;
using System.IO;
using log4net;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using CrystalDecisions.Shared;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Threading;
using System.Globalization;
namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR132BODBSHOT : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR132BODBSHOT));
        public string TheUrl = "";
        string reportname = "TR132BODBSHOTF";
        ReportClass rpt = new ReportClass();

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChoiceBox.SelectedValue.Equals("Excel"))
                {
                    if (OpenExcel())
                    {
                        MessageBox.Text = "ระบบสร้างไฟล์เรียบร้อยแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else {
                        MessageBox.Text = "ไม่สามารถสร้างไฟล์ได้ กรุณาตรวจสอบข้อมูล";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                }
                else if (ChoiceBox.SelectedValue.Equals("Cover"))
                {
                    string Report = ChoiceBox.SelectedValue.ToString();
                    string Security = SecurityBox.Text;
                    ReportClass rpt = Model_RPT_TR132BODBSHOT.getReport(Config.ConnectionString, "Cover", FromDateBox.Text, ToDateBox.Text , Security, Config.GetParameterFile("132AutorizeDate"), Config.GetParameterFile("132Registrar"));
                    if (rpt == null)
                    {
                        DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                        rpt = new RPT_MESSAGE();
                        rpt.SetDataSource(ds);
                    }
                    Session["ReportSource"] = rpt;

                    TheUrl = "../View/Viewer.aspx";
                    Page.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void GenInternalRefButton_Click(object sender, EventArgs e)
        {
            try
            {
                string Report = "Generate";
                if (Model_RPT_TR132BODBSHOT.genInternalRef(Config.ConnectionString, Report, GenFromDateBox.Text, GenToDateBox.Text, ""))
                {
                    MessageBox.Text = "สร้าง Internal Reference เรียบร้อยแล้ว";
                } else {
                    MessageBox.Text = "ไม่สามารถทำรายการได้ กรุณาทำรายการใหม่";
                }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        private bool OpenExcel()
        {
            try
            {
                string filename = "SECDebenture.xls";
                string Report = ChoiceBox.SelectedValue.ToString();
                string Security = SecurityBox.Text;

                HSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(Server.MapPath("~/" + Config.GetParameterFile("132TemplatePath") + "/" + filename), FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new HSSFWorkbook(file);
                    file.Close();
                }

                DataTable dt = Model_RPT_TR132BODBSHOT.getData(Config.ConnectionString, "Feature", FromDateBox.Text, ToDateBox.Text);
                hssfwb = WriteFeature(hssfwb, dt);

                dt = Model_RPT_TR132BODBSHOT.getData(Config.ConnectionString, "Summary", FromDateBox.Text, ToDateBox.Text);
                hssfwb = WriteSellingResult(hssfwb, dt);

                dt = Model_RPT_TR132BODBSHOT.getData(Config.ConnectionString, "ByNationality", FromDateBox.Text, ToDateBox.Text);
                hssfwb = WriteSellingByNationality(hssfwb, dt);

                dt = Model_RPT_TR132BODBSHOT.getData(Config.ConnectionString, "ByCustomer", FromDateBox.Text, ToDateBox.Text);
                hssfwb = WriteSellingByCustomer(hssfwb, dt);

                string FullFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + filename;
                string FullPathFileName = Server.MapPath("~/" + Config.GetParameterFile("132ExportPath") + "/" + FullFileName);
                using (FileStream file = new FileStream(FullPathFileName , FileMode.OpenOrCreate, FileAccess.Write))
                {
                    hssfwb.Write(file);
                    file.Close();
                }
                DownloadFile(FullFileName, FullPathFileName);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error Export File : " + ex.Message);
                return false;
            }
        }

        private HSSFWorkbook WriteFeature(HSSFWorkbook hssfwb, DataTable dt)
        {
            int rowindx = 0;
            int colindx = 0;
            try
            {

                ISheet sheet = hssfwb.GetSheet("Feature");

                int sheetrow = 5;
                sheet.CreateRow(dt.Rows.Count + 5);

                IRow firstrow = sheet.GetRow(0);
                ICell firstcell = firstrow.CreateCell(0);
                firstcell.SetCellValue(dt.Rows[0]["DocDate"].ToString());

                for (rowindx = 0; rowindx < dt.Rows.Count; rowindx++)
                {
                    IRow row = sheet.GetRow(sheetrow);
                    for (colindx = 0; colindx < dt.Columns.Count; colindx++)
                    {
                        ICell cell = row.CreateCell(colindx);
                        if (colindx == 0)
                            cell.SetCellValue("Data");
                        else if (colindx == 2)
                        {
                            string d = dt.Rows[rowindx][6].ToString();
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                            DateTime mdate = new DateTime(int.Parse(d.Substring(0,4))-543, int.Parse(d.Substring(4,2)), int.Parse(d.Substring(6,2)));
                            string thaidate = mdate.ToString("dd MMMM yyyy");
                            string debtthai = "หุ้นกู้ระยะสั้น ธนาคารเกียรตินาคิน จำกัด (มหาชน) ครั้งที่ " + dt.Rows[rowindx][2].ToString() + " มูลค่า " + dt.Rows[rowindx][3].ToString() + " ล้านบาท ครบกำหนดไถ่ถอนวันที่ " + thaidate;
                            cell.SetCellValue(debtthai);
                        }
                        else if (colindx == 3)
                        {
                            string d = dt.Rows[rowindx][6].ToString();
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                            DateTime mdate = new DateTime(int.Parse(d.Substring(0, 4))-543, int.Parse(d.Substring(4, 2)), int.Parse(d.Substring(6, 2)));
                            string thaidate = mdate.ToString("MMMM dd, yyyy");
                            string debteng = "Short-term Debenture of KIATNAKIN BANK PUBLIC COMPANY LIMITED No." + dt.Rows[rowindx][2].ToString() + " worth of THB " + dt.Rows[rowindx][3].ToString() + " mln. due " + thaidate;

                            cell.SetCellValue(debteng);
                        }
                        else
                        {
                            int num1;
                            bool res = int.TryParse(dt.Rows[rowindx][colindx].ToString(), out num1);
                            if (res == false)
                                cell.SetCellValue(dt.Rows[rowindx][colindx].ToString());
                            else
                                cell.SetCellValue(num1);
                        }
                    }
                    sheetrow += 1;
                }
                return hssfwb;
            }
            catch (Exception ex)
            {
                Log.Error("Error Export File Feature : " + ex.Message);
                return hssfwb;
            }
            finally {

            }
        }
        
        private HSSFWorkbook WriteSellingResult(HSSFWorkbook hssfwb, DataTable dt)
        {
            int rowindx = 0;
            int colindx = 0;
            try
            {
                ISheet sheet = hssfwb.GetSheet("SellingResult");

                int sheetrow = 5;
                sheet.CreateRow(dt.Rows.Count + 5);

                IRow firstrow = sheet.GetRow(0);
                ICell firstcell = firstrow.CreateCell(0);
                firstcell.SetCellValue(dt.Rows[0]["DocDate"].ToString());

                for (rowindx = 0; rowindx < dt.Rows.Count; rowindx++)
                {
                    IRow row = sheet.GetRow(sheetrow);
                    for (colindx = 0; colindx < dt.Columns.Count; colindx++)
                    {
                        ICell cell = row.CreateCell(colindx);
                        if (colindx == 0)
                            cell.SetCellValue("Data");
                        else
                        {
                            int num1;
                            bool res = int.TryParse(dt.Rows[rowindx][colindx].ToString(), out num1);
                            if (res == false)
                                cell.SetCellValue(dt.Rows[rowindx][colindx].ToString());
                            else
                                cell.SetCellValue(num1);
                        }
                    }
                    sheetrow += 1;
                }
                return hssfwb;
            }
            catch (Exception ex)
            {
                Log.Error("Error Export File SellingResult : " + ex.Message);
                return hssfwb;
            }
        }

        private HSSFWorkbook WriteSellingByNationality(HSSFWorkbook hssfwb, DataTable dt)
        {
            int rowindx = 0;
            int colindx = 0;
            try
            {
                ISheet sheet = hssfwb.GetSheet("SellingByNationality");

                int sheetrow = 5;
                sheet.CreateRow(dt.Rows.Count + 5);

                IRow firstrow = sheet.GetRow(0);
                ICell firstcell = firstrow.CreateCell(0);
                firstcell.SetCellValue(dt.Rows[0]["DocDate"].ToString());

                for (rowindx = 0; rowindx < dt.Rows.Count; rowindx++)
                {
                    IRow row = sheet.GetRow(sheetrow);
                    for (colindx = 0; colindx < dt.Columns.Count; colindx++)
                    {
                        ICell cell = row.CreateCell(colindx);
                        if (colindx == 0)
                            cell.SetCellValue("Data");
                        else
                        {
                            int num1;
                            bool res = int.TryParse(dt.Rows[rowindx][colindx].ToString(), out num1);
                            if (res == false)
                                cell.SetCellValue(dt.Rows[rowindx][colindx].ToString());
                            else
                                cell.SetCellValue(num1); 
                        }
                    }
                    sheetrow += 1;
                }
                return hssfwb;
            }
            catch (Exception ex)
            {
                Log.Error("Error Export File Selling by Nationality: " + ex.Message);
                return hssfwb;
            }
        }

        private HSSFWorkbook WriteSellingByCustomer(HSSFWorkbook hssfwb, DataTable dt)
        {
            int rowindx = 0;
            int colindx = 0;
            try
            {

                ISheet sheet = hssfwb.GetSheet("SellingByCorporationType");

                int sheetrow = 5;
                sheet.CreateRow(dt.Rows.Count + 5);

                IRow firstrow = sheet.GetRow(0);
                ICell firstcell = firstrow.CreateCell(0);
                firstcell.SetCellValue(dt.Rows[0]["DocDate"].ToString());

                for (rowindx = 0; rowindx < dt.Rows.Count; rowindx++)
                {
                    IRow row = sheet.GetRow(sheetrow);
                    for (colindx = 0; colindx < dt.Columns.Count; colindx++)
                    {
                        ICell cell = row.CreateCell(colindx);
                        if (colindx == 0)
                            cell.SetCellValue("Data");
                        else
                        {
                            int num1;
                            bool res = int.TryParse(dt.Rows[rowindx][colindx].ToString(), out num1);
                            if (res == false)
                                cell.SetCellValue(dt.Rows[rowindx][colindx].ToString());
                            else
                                cell.SetCellValue(num1); 
                        }
                    }
                    sheetrow += 1;
                }
                return hssfwb;
            }
            catch (Exception ex)
            {
                Log.Error("Error Export File Selling by Customer Type: " + ex.Message);
                return hssfwb;
            }
        }

        protected void DownloadFile(string filename, string fullpathfile)
        {
            try
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                //response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ";");
                response.TransmitFile(fullpathfile);
                response.Flush();
                response.End();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
