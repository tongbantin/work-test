using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryModel.Master;
using System.Data;
using System.Text;
using System.IO;
using TreasuryBusiness.ThirdParty;
//using TreasuryModel.Master;
using System.Web.UI.HtmlControls;
using log4net;

using System.Net;
using System.Net.Mail;
using TreasuryShadowSystem.Configuration;

using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using CrystalDecisions.Shared;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR051TRANSLIST : System.Web.UI.Page
    {
        public const string REPORT_USERCODE = "TRPTIBONTH";
        public string TheUrl = "";
        string ExportType = "";
        string PathDownload = "";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR051TRANSLIST));

        ReportClass rpt = new ReportClass();

        DataSet PTIResultSession
        {
            get
            {
                ViewState["PTIResultSession"] = Model_MS_PTITRANS.get(Configuration.Config.ConnectionString, txtDealNo.Text, txtTransDate.Value);
                return (DataSet)ViewState["PTIResultSession"];
            }
            set
            {
                ViewState["PTIResultSession"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridData.RowCommand += new GridViewCommandEventHandler(GridData_RowCommand);
                GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            try
            {
                GridData.PageIndex = e.NewPageIndex;
                BindGrid();
                SetData();
                SetDataRevise();
                SetDataNoSign();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                int row = Int32.Parse(e.CommandArgument.ToString());
                string key = GridData.DataKeys[row].Value.ToString();
                if (e.CommandName == "XML")
                {
                    ExportType = "XML";
                    GenerateXML(key);
                }
                /*else if (e.CommandName == "Print")
                {
                    ExportType = "PRINT";
                    GeneratePDF(key, "", "false");
                }
                else if (e.CommandName == "PrintRevise")
                {
                    ExportType = "Print";
                    GeneratePDF(key, "", "true");
                }*/
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                //    Response.Redirect("~/SessionExpired.aspx");
                if (IsPostBack)
                {
                    GetData();
                    GetDataRevise();
                    GetDataNoSign();
                }
                else {
                    txtTransDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
                ViewState["SelectedRecords"] = new ArrayList();
                ViewState["SelectedReviseRecords"] = new ArrayList();
                ViewState["SelectedNoSignRecords"] = new ArrayList();
                btnDownloadSelected.Visible = true;
                btnSendMailSelected.Visible = true;
                //Page.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void BindGrid()
        {
            try
            {
                GridData.DataSource = PTIResultSession;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void GenerateXML(string key)
        {
            try
            {
                DataTable dt = Model_MS_PTITRANS.getTransLogByID(Configuration.Config.ConnectionString, key);
                Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                md.TransLogNo = dt.Rows[0]["TransLogNo"].ToString().Trim();
                md.TransLogDate = dt.Rows[0]["TransLogDate"].ToString().Trim();
                md.SecID = dt.Rows[0]["SecID"].ToString().Trim();
                md.PartialID = dt.Rows[0]["PartialID"].ToString().Trim();
                md.MsgType = dt.Rows[0]["MsgType"].ToString().Trim();
                md.Priority = dt.Rows[0]["Priority"].ToString().Trim();
                md.SenderRef = dt.Rows[0]["SenderRef"].ToString().Trim();
                md.RelateRef = dt.Rows[0]["RelateRef"].ToString().Trim();
                md.PlaceCode = dt.Rows[0]["PlaceCode"].ToString().Trim();
                md.SettDate = dt.Rows[0]["SettDate"].ToString().Trim();
                md.TradeDate = dt.Rows[0]["TradeDate"].ToString().Trim();
                md.PercentType = dt.Rows[0]["PercentType"].ToString().Trim();
                md.DealPrice = dt.Rows[0]["DealPrice"].ToString().Trim();
                md.ISIN = dt.Rows[0]["ISIN"].ToString().Trim();
                md.FaceAmt = dt.Rows[0]["FaceAmt"].ToString().Trim();
                md.AcctNo = dt.Rows[0]["AcctNo"].ToString().Trim();
                md.CashAcct = dt.Rows[0]["CashAcct"].ToString().Trim();
                md.BusinessType = dt.Rows[0]["BusinessType"].ToString().Trim();
                md.SettType = dt.Rows[0]["SettType"].ToString().Trim();
                md.SettPlace = dt.Rows[0]["SettPlace"].ToString().Trim();
                md.AgentBIC = dt.Rows[0]["AgentBIC"].ToString().Trim();
                md.AgentAcctNo = dt.Rows[0]["AgentAcctNo"].ToString().Trim();
                md.SettAmt = dt.Rows[0]["SettAmt"].ToString().Trim();

                PathDownload = Server.MapPath("~/Temporary/PTIDownload/PTI" + md.MsgType.Trim() + md.SecID.Trim() + md.SenderRef.Trim() + ".xml");
                FileStream f = File.Create(PathDownload);
                Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                f.Write(info, 0, info.Length);
                f.Close();
                ExportFile("~/Temporary/PTIDownload/PTI" + md.MsgType.Trim() + md.SecID.Trim() + md.SenderRef.Trim() + ".xml");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void GeneratePDF(string key, string dealno, string revise, string nosign)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenConfirmation", "window.open(\"../View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&p=" + key + "&d=" + dealno + "&r=" + revise + "&s=" + nosign + "\");", true);
                //Response.Redirect("~/View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&p=" + key, false );
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void ExportFile(string str)
        {
            try
            {
                string result = Path.GetFileName(str);
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + result);
                Response.TransmitFile(Server.MapPath(str));
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void GetData()
        {
            ArrayList arr;
            if (ViewState["SelectedRecords"] != null)
                arr = (ArrayList)ViewState["SelectedRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[0].FindControl("chkAllPrintDocument");
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                    {
                        arr.Add(GridData.DataKeys[i].Value.ToString());
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)GridData.Rows[i].Cells[0].FindControl("chkPrintDocument");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Add(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                    else
                    {
                        if (arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Remove(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
        }

        private void SetData()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[0].FindControl("chkAllPrintDocument");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                RadioButton chk = (RadioButton)GridData.Rows[i].Cells[0].FindControl("chkPrintDocument");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(GridData.DataKeys[i].Value.ToString());
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
            hfCount.Value = (arr.Count - currentCount).ToString(); 
        }

        private void GetDataRevise()
        {
            ArrayList arr;
            if (ViewState["SelectedReviseRecords"] != null)
                arr = (ArrayList)ViewState["SelectedReviseRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[0].FindControl("chkAllPrintReviseDocument");
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                    {
                        arr.Add(GridData.DataKeys[i].Value.ToString());
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)GridData.Rows[i].Cells[0].FindControl("chkPrintReviseDocument");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Add(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                    else
                    {
                        if (arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Remove(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                }
            }
            ViewState["SelectedReviseRecords"] = arr;
        }

        private void SetDataRevise()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[0].FindControl("chkAllPrintReviseDocument");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedReviseRecords"];
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                RadioButton chk = (RadioButton)GridData.Rows[i].Cells[0].FindControl("chkPrintReviseDocument");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(GridData.DataKeys[i].Value.ToString());
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
            hfCount.Value = (arr.Count - currentCount).ToString();
            Log.Debug("Selected Revise Document : " + arr.Count);
        }

        private void GetDataNoSign()
        {
            ArrayList arr;
            if (ViewState["SelectedNoSignRecords"] != null)
                arr = (ArrayList)ViewState["SelectedNoSignRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[1].FindControl("chkAllPrintNoSignDocument");
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                    {
                        arr.Add(GridData.DataKeys[i].Value.ToString());
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)GridData.Rows[i].Cells[1].FindControl("chkPrintNoSignDocument");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Add(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                    else
                    {
                        if (arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Remove(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                }
            }
            ViewState["SelectedNoSignRecords"] = arr;
        }

        private void SetDataNoSign()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[1].FindControl("chkAllPrintNoSignDocument");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedNoSignRecords"];
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridData.Rows[i].Cells[1].FindControl("chkPrintNoSignDocument");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(GridData.DataKeys[i].Value.ToString());
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
        }

        protected void btnDownloadSelected_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportName = REPORT_USERCODE + DateTime.Now.ToString("yyyyMMddHHmmss");
                ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
                ArrayList rarr = (ArrayList)ViewState["SelectedReviseRecords"];
                ArrayList sign = (ArrayList)ViewState["SelectedNoSignRecords"];
                if ((arr.Count > 0) || (rarr.Count > 0))
                {
                    rpt = Model_RPT_TRPTIBONTH.getReport(Config.ConnectionString, arr, rarr, sign);
                    rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, ReportName);
                }
                else {
                    MessageBox.Text = "ยังไม่ได้เลือกรายการ กรุณาทำรายการใหม่";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSendMailSelected_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportName = REPORT_USERCODE + DateTime.Now.ToString("yyyyMMddHHmmss");
                ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
                ArrayList rarr = (ArrayList)ViewState["SelectedReviseRecords"];
                ArrayList sign = (ArrayList)ViewState["SelectedNoSignRecords"];

                if ((arr.Count > 0) || (rarr.Count > 0))
                {
                    rpt = Model_RPT_TRPTIBONTH.getReport(Config.ConnectionString, arr, rarr, sign);
                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/Temporary/" + ReportName + ".pdf"));

                    if (rpt != null)
                    {                      
                        //Send mail
                        Attachment attach = new Attachment(Server.MapPath("~/Temporary/" + ReportName + ".pdf"));
                        attach.Name = ReportName + ".pdf";
                        SendMail(attach);
                    }
                    else
                    {
                        Log.Error("Could Not Create File");
                    }
                }
                else {
                    MessageBox.Text = "ยังไม่ได้เลือกรายการ กรุณาทำรายการใหม่";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }

                string[] f = Directory.GetFiles(Server.MapPath("~/Temporary/"), "*.pdf");
                for (int i = 0; i < f.Length; i++)
                {
                    try
                    {
                        File.Delete(f[i]);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnDownloadAll_Click(object sender, EventArgs e)
        {
            string revise = ChoiceConditionBox.SelectedValue.Equals("Revise") ? "true" : "false";
            string nosign = NoSignBox.Checked == true ? "1" : "0";
            GeneratePDF("", DealNoBox.Text.Trim(), revise, nosign);
        }

        protected void btnViewPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string[] prefixEncrypt;
                DataSet ds = Model_RPT_TRPTIBONTH.getDataSet(Config.ConnectionString, "", DealNoBox.Text.Trim());
                if (ds != null)
                {
                    prefixEncrypt = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        prefixEncrypt[i] = ds.Tables[0].Rows[i]["COUNTERPARTY"].ToString();

                    }

                    //Encrypt file with pasword
                    string[] uniqueItems = prefixEncrypt.Select(x => x.ToUpper()).Distinct<string>().ToArray();
                    int j = 0; string message = "";
                    foreach (string item in uniqueItems)
                    {
                        message += item  + Config.GetParameterFile("051ENCRYPT_BOND") + "<br>";
                        j++;                     
                    }

                    MessageBox.Text = message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่พบ Password";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }          

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportName = REPORT_USERCODE + DateTime.Now.ToString("yyyyMMddHHmmss");
                string revise = ChoiceConditionBox.SelectedValue.Equals("Revise") ? "true" : "false";
                string nosign = NoSignBox.Checked == true ? "1" : "0"; //Update 2018-02-23                
                rpt = Model_RPT_TRPTIBONTH.getReport(Config.ConnectionString, "", DealNoBox.Text.Trim(), revise, nosign);

                //rpt.SetParameterValue("NoSign", nosign); //Update 2018-02-23
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/Temporary/" + ReportName + ".pdf"));

                //Find prefix encrypt 
                string[] prefixEncrypt;
                DataSet ds = Model_RPT_TRPTIBONTH.getDataSet(Config.ConnectionString, "", DealNoBox.Text.Trim());
                if (ds != null)
                {
                    prefixEncrypt = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    { 
                        prefixEncrypt[i] = ds.Tables[0].Rows[i]["COUNTERPARTY"].ToString();

                    }

                    //Encrypt file with pasword
                    string[] uniqueItems = prefixEncrypt.Select(x => x.ToUpper()).Distinct<string>().ToArray();
                    Encryption(ReportName, uniqueItems);
                
                }

                if (rpt != null)
                {
                    Attachment attach = new Attachment(Server.MapPath("~/Temporary/" + ReportName + ".zip"));
                    attach.Name = "Confirmation" + DealNoBox.Text.Trim() + ".zip";
                    SendMail(attach);
                }
                else
                {
                    Log.Error("Could Not Create File");
                }

                string[] f = Directory.GetFiles(Server.MapPath("~/Temporary/"), "*.pdf");
                for (int i = 0; i < f.Length; i++)
                {
                    try
                    {
                        File.Delete(f[i]);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SendMail(Attachment Attach)
        {
            try
            {
                //Parameter
                string to = Config.GetParameterFile("051To");
                bool isHtml = true;
                string message = Config.GetParameterFile("051Message");

                MailMessage mail = new MailMessage();
                // Set the to and from addresses.
                // The from address must be your GMail account
                mail.From = new MailAddress(Config.GetParameterFile("051From"));
                string[] tto = to.Split(',');
                for (int i = 0; i < tto.Count(); i++)
                {
                    mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                }
                string Cc = Config.GetParameterFile("051Cc");
                if (!Cc.Trim().Equals(""))
                {
                    string[] ccto = Cc.Split(',');
                    for (int j = 0; j < ccto.Count(); j++)
                    {
                        mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                    }
                }
                // Define the message
                mail.Subject = Config.GetParameterFile("051Subject") + DateTime.Now.ToString("dd/MM/yyyy");
                mail.IsBodyHtml = isHtml;
                mail.Body = message;
                //foreach (Attachment a in Attach)
                //{
                mail.Attachments.Add(Attach);
                //}

                // Create a new Smpt Client using Google's servers
                var mailclient = new SmtpClient();
                //mailclient.Host = "smtp.gmail.com";//ForGmail
                mailclient.Host = Config.GetParameterFile("051SMTP");
                mailclient.Port = int.Parse(Config.GetParameterFile("051SMTPPort")); ;

                // This is the critical part, you must enable SSL
                //mailclient.EnableSsl = true;//ForGmail
                mailclient.EnableSsl = false;
                mailclient.UseDefaultCredentials = true;

                mailclient.Send(mail);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public void Encryption(string filename, string[] uniqueItems)
        {
            try
            {
                string Path = Server.MapPath("~/Temporary/" + filename);

                for (int i = 0; i < uniqueItems.Length; i++)
                {
                    if (File.Exists(Path + ".zip"))
                        File.Delete(Path + ".zip");


                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = uniqueItems[i]  + Config.GetParameterFile("051ENCRYPT_BOND");
                        zip.AddFile(Path + ".pdf", "");

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(Path + ".zip");
                    }

                    Log.Info("encrypt pass : " + uniqueItems[i] + Config.GetParameterFile("051ENCRYPT_BOND"));
                    Log.Info("encrypt path : " + Path);
                                    
                    //Clear File After Zip
                    if (Directory.Exists(Path))
                    {
                        string[] f = Directory.GetFiles(Path);
                        for (int l = 0; l < f.Length; l++)
                        {
                            File.Delete(f[l]);
                        }
                        Directory.Delete(Path);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
