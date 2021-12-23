using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;
using TreasuryModel.Enhance;

namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_SanctionBatch : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_SanctionBatch));
        private int totalRows = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            GridResult.RowDataBound += new GridViewRowEventHandler(GridResult_RowDataBound);
        }

        void GridResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string status = e.Row.Cells[0].Text.Trim();
                    string resultText = "";
                    if (status.Equals("WAITING_TO_SEND"))
                        resultText = "Sending";
                    else if (status.Equals("CHECKING_SANCTION"))
                        resultText = "Checking";
                    else if (status.Equals("WAITING_TO_REVIEW"))
                        resultText = "Please Review!!";
                    else if (status.Equals("APPROVE"))
                        resultText = "Approved";
                    else if (status.Equals("DISAPPROVE"))
                        resultText = "Disapproved";
                    else if (status.Equals("TRANSFER_TO_SWIFT"))
                        resultText = "Passed";

                    e.Row.Cells[0].Text = resultText;

                    int records = int.Parse(e.Row.Cells[1].Text.Trim());
                    totalRows += records;
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "Total&nbsp;";
                    e.Row.Cells[1].Text = totalRows.ToString() + "&nbsp;";

                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                totalRows = 0;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                GridResult.Visible = false;

                bool hasdeal = true;
                String filename = MyFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                MyFile.SaveAs(pathFile);
                MyFileLoad.Value = pathFile;

                Encoding coding = Encoding.UTF8;
                if (MySource.SelectedValue.Equals("RIA-Unicode"))
                    coding = Encoding.Unicode;                

                string[] lines = File.ReadAllLines(pathFile, coding);
                DataTable dt = PartyMsg.ExtractMessage(lines);

                BindGrid(dt);

                btnSave.Visible = true;
                btnDownloadPass.Visible = false;
            }
            catch (Exception ex)
            {
                btnSave.Visible = false;
                Log.Error(ex.Message);
            }
        }

        void BindGrid(DataTable dt)
        {
            try
            {
                GridData.DataSource = dt;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 

                string sourcefile = Config.GetParameterFile("147RIACOLCHK");
                Encoding coding = Encoding.UTF8;
                if (MySource.SelectedValue.Equals("RIA-Unicode"))
                    coding = Encoding.Unicode; 
                //else if (SourceFileItem.SelectedValue.Equals("DOLLARSMART"))
                //{
                //    sourcefile = Config.GetParameterFile("147DOLLARCOLCHK");
                //}
                //else if (SourceFileItem.SelectedValue.Equals("MERCHANTS"))
                //{
                //    sourcefile = Config.GetParameterFile("147MERCHCOLCHK");
                //}

                string[] lines = File.ReadAllLines(MyFileLoad.Value, coding);
                if (PartyMsg.SpliteMessage(Config.ConnectionString, Path.GetFileName(MyFileLoad.Value), lines, Config.GetParameterFile("147PARTYCLEAR")))
                {
                    DataTable dtFind = PartyMsg.ScanMessageList(Config.ConnectionString, Config.GetParameterFile("147SANCTIONHOST"), Path.GetFileName(MyFileLoad.Value), sourcefile, Config.GetParameterFile("147THRESHOLD"), Config.GetParameterFile("147CONCURRENT"));
                    BindResult(dtFind);
                    MessageMail(Path.GetFileName(MyFileLoad.Value));
                    MessageBox.Text = "ระบบตรวจสอบข้อมูลเรียบร้อยแล้ว";
                    btnDownloadPass.Visible = true;
                    btnSave.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);

                }
                else
                {
                    MessageBox.Text = "เกิดข้อมูลผิดพลาด ไม่สามารถโหลดไฟล์ได้ กรุณาตรวจสอบ";
                    btnDownloadPass.Visible = false;
                    btnSave.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = "ระบบไม่สามารถทำรายการได้ " + ex.Message;
                btnDownloadPass.Visible = false;
                btnSave.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
            finally {
                GridData.DataSource = null;
                GridData.DataBind();
            }
        }

        protected void BindResult(DataTable dtFind)
        {
            try
            {
                DataTable dtResult = PartyMsg.MessageResultList(Config.ConnectionString, dtFind);
                GridResult.DataSource = dtResult;
                GridResult.DataBind();
                GridResult.Visible = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void MessageMail(string Filename)
        {
            try
            {
                String Msg = File.ReadAllText(Server.MapPath("~/Template/SanctionMail.html"));
                string rowMsg = PartyMsg.GetMessageSanctionResult(Config.ConnectionString, Filename);
                Msg = Msg.Replace("{Sanction_Result}", rowMsg);

                if (!rowMsg.Trim().Equals(""))
                    SendMail.Send(
                        Config.GetParameterFile("147MAILFROM")
                        , Config.GetParameterFile("147MAILTO")
                        , Config.GetParameterFile("147MAILCC")
                        , Config.GetParameterFile("147MAILSUBJECT")
                        , Msg, new List<Attachment>(), Config.GetParameterFile("089SMTP")
                        , int.Parse(Config.GetParameterFile("089SMTPPort")));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); 
            }
        }

        protected void btnDownloadPass_Click(object sender, EventArgs e)
        {
            try
            {
                string Filename = Path.GetFileName(MyFileLoad.Value);
                DataTable dtMessage = PartyMsg.GetMessagePassResult(Config.ConnectionString, Filename);
                string[] msg = dtMessage.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                Encoding encoding = Encoding.UTF8;
                if (MySource.SelectedValue.Equals("RIA"))
                    encoding = Encoding.Unicode;

                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                string FullFilename = Server.MapPath("~/Temporary/MoneyTransfer/" + fname);
                File.WriteAllLines(FullFilename, msg, encoding);

                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fname);
                Response.TransmitFile(FullFilename);
                Response.End();                
            }
            catch (Exception ex)
            { 
                Log.Error(ex.Message);
            }
        }

    }
}
