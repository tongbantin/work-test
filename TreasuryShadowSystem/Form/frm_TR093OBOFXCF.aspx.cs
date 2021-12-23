using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryModel.Enhance;
using System.Net.Mail;
using log4net;
using System.IO;
using System.IO.Packaging;
using System.Globalization;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR093OBOFXCF : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR093OBOFXCF));
        public string TheUrl = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Purchase Interest";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Check dealno unser noshow customer 
            string custnorpt = Config.GetParameterFile("FXCUSTNORPT");
            DataSet norpt = Model_RPT_TR093OBOFXCF.checkCustNoReport(Config.ConnectionString, txtDealNo.Text.Trim(), custnorpt);
            if (norpt != null)
            {
                TheUrl = "../View/ReportViewer2.aspx?report=TR093OBOFXCF";
                TheUrl += "&d=0";
            }
            else
            {
                TheUrl = "../View/ReportViewer2.aspx?report=TR093OBOFXCF";
                TheUrl += "&d=" + txtDealNo.Text;
            }

            //TheUrl = "../View/ReportViewer2.aspx?report=TR093OBOFXCF";
            //TheUrl += "&d=" + txtDealNo.Text;
            Page.DataBind();
        }

        protected void btnViewPass_Click(object sender, EventArgs e)
        {
            try
            {
                string Dealno = txtDealNo.Text.Trim();
                DataSet ds = Model_RPT_TR093OBOFXCF.getDataSet(Config.ConnectionString, Dealno);

                string[] encrypt;
                string[] ctype;
                string message = "";

                if (ds != null)
                {
                    encrypt = new string[ds.Tables[0].Rows.Count];
                    ctype = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encrypt[i] = ds.Tables[0].Rows[i]["ENCRYPT"].ToString();
                        ctype[i] = ds.Tables[0].Rows[i]["CTYPE"].ToString();

                        if (!ctype[i].Equals("") && (ctype[i].Equals("I") || ctype[i].Equals("C")))
                        {
                            message += encrypt[i] + "<br>";
                        }
                        else
                        {
                            message += "FX" + encrypt[i] + Config.GetParameterFile("091ENCRYPT_FX") + "<br>";
                        }

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
                string Dealno = txtDealNo.Text.Trim();
                DataSet ds = Model_RPT_TR093OBOFXCF.getDataSet(Config.ConnectionString, Dealno);

                //SET OBJECT TABLE AND REPORT
                string filename = "FX_";
                string tableName = "TB_TR093OBOFXCF";
                ReportClass report = new RPT_TR093OBOFXCF();
                string[] encrypt;
                string[] ctype;
                string[] zipList;

                if (ds != null)
                {
                    encrypt = new string[ds.Tables[0].Rows.Count];
                    ctype = new string[ds.Tables[0].Rows.Count];
                    zipList = new string[ds.Tables[0].Rows.Count]; 

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //Create folders
                        string zipname = "";
                        encrypt[i] = ds.Tables[0].Rows[i]["ENCRYPT"].ToString();
                        ctype[i] = ds.Tables[0].Rows[i]["CTYPE"].ToString();
                        filename += ds.Tables[0].Rows[i]["DEALNO"].ToString();

                        if (!ctype[i].Equals("") && (ctype[i].Equals("I") || ctype[i].Equals("C")))
                        {
                            zipname = DateTime.Now.ToString("yyyyMMdd");
                        }
                        else
                        {
                            zipname = encrypt[i];
                        }
                        zipList[i] = zipname;
                        FileManage.ManageDirectory(Server.MapPath("~/" + Config.GetParameterFile("091ExportPath") + "/" + zipname));

                        DataTable tb = ds.Tables[0].Clone();
                        DataRow row = ds.Tables[0].Rows[i];
                        tb.TableName = tableName;
                        tb.ImportRow(row);

                        //Save files to folder
                        ReportClass doc = report;
                        doc.SetDataSource(tb);
                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/" + Config.GetParameterFile("091ExportPath") + "/" + zipname) + "/" + filename + ".pdf");

                    }

                    Encryption(filename, encrypt, ctype);

                    // Attach file to mail
                    List<Attachment> attach = new List<Attachment>();
                    foreach (string item in zipList)
                    {
                        attach.Add(new Attachment(Server.MapPath("~/" + Config.GetParameterFile("091ExportPath") + "/") + item + ".zip"));
                    }

                    //Send Mail                    
                    SendMail(Config.GetParameterFile("091To"), Config.GetParameterFile("091Cc"), " KKP:FX Confirmation as of " + DateTime.Now.ToString("ddMMyyyy"), attach);
                    MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Text = "เกิดข้อผิดพลาด (" + ex.Message + ")";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);
                Log.Error(ex.Message);
            }
        }

        #region Methods

        public void Encryption(string filename, string[] encrypt, string[] ctype)
        {
            try
            {
                Log.Info(" TR093OBOFXCF Encryption ");

                //Encrypt file with password
                for (int i = 0; i < encrypt.Length; i++)
                {

                    //Set password format and folder by ctype
                    string zipname = "";
                    string password = "";
                    if (!ctype[i].Equals("") && (ctype[i].Equals("I") || ctype[i].Equals("C")))
                    {
                        password = encrypt[i];
                        zipname = DateTime.Now.ToString("yyyyMMdd");
                    }
                    else
                    {
                        password = "FX" + encrypt[i] + Config.GetParameterFile("091ENCRYPT_FX");
                        zipname = encrypt[i];
                    }

                    string ExportPath = Server.MapPath("~/" + Config.GetParameterFile("091ExportPath") + "/" + zipname);

                    if (File.Exists(ExportPath + ".zip"))
                        File.Delete(ExportPath + ".zip");

                 
                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = password;
                        zip.AddDirectory(ExportPath);

                        Log.Info("password : " + password);

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(ExportPath + ".zip");
                    }
                    Log.Info("encrypt pass : " + password);
                    Log.Info("encrypt path : " + ExportPath);

                    //Clear File After Zip
                    if (Directory.Exists(ExportPath))
                    {
                        string[] f = Directory.GetFiles(ExportPath);
                        for (int l = 0; l < f.Length; l++)
                        {
                            File.Delete(f[l]);
                        }
                        Directory.Delete(ExportPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SendMail(string To, string Cc, string Subject, List<Attachment> Attach)
        {
            try
            {
                //Parameter
                string to = To;
                bool isHtml = true;
                string message = "Dear all, <br> The FX confirmation as attached. Please kindly return the signed original copy. <br> Thank you for your prompt response." + "<br>";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Config.GetParameterFile("091From"));
                    string[] tto = to.Split(',');
                    for (int i = 0; i < tto.Count(); i++)
                    {
                        mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                    }
                    if (!Cc.Trim().Equals(""))
                    {
                        string[] ccto = Cc.Split(',');
                        for (int j = 0; j < ccto.Count(); j++)
                        {
                            mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                        }
                    }
                    // Define the message
                    mail.Subject = Subject;
                    mail.IsBodyHtml = isHtml;
                    mail.Body = message;
                    foreach (Attachment a in Attach)
                    {
                        mail.Attachments.Add(a);
                    }

                    var mailclient = new SmtpClient();
                    mailclient.Host = Config.GetParameterFile("SMTP");
                    mailclient.Port = int.Parse(Config.GetParameterFile("SMTPPort"));
                    mailclient.EnableSsl = false;
                    mailclient.UseDefaultCredentials = true;

                    mailclient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw ex;
            }

        }

        #endregion
    }
}
