using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using KKB.Treasury.TreasuryModel.Master;
using TreasuryShadowSystem.Configuration;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net.Mime;
using System.Net;
using System.Net.Mail;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR089OTHMTM : System.Web.UI.Page
    {
        public string TheUrl = "";
        public DataTable dt = new DataTable();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR089OTHMTM));
        protected void Page_Init(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
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
                if (!IsPostBack)
                {
                    ViewState["Schedule"] = Model_MS_MTMSCHEDULE.getScheduleData(Config.ConnectionString);
                    BindFund();
                    BindGrid();
                    txtFileDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    DateBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ToMailBox.Text = Config.GetParameterFile("089To");
                    CcMailBox.Text = Config.GetParameterFile("089Cc");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindFund()
        {
            try
            {
                dt = (DataTable)ViewState["Schedule"];
                string condition = "Round='" + RoundBox.SelectedValue + "'";
                DataTable t = dt.Select(condition).AsEnumerable().CopyToDataTable();
                MutualFundsBox.Items.Clear();
                MutualFundsBox.DataSource = t.DefaultView.ToTable(true, "MUTUALFUND");
                MutualFundsBox.DataTextField = "MUTUALFUND";
                MutualFundsBox.DataValueField = "MUTUALFUND";
                MutualFundsBox.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MutualFundsBox.Items.Clear();
            }
            finally
            {
                MutualFundsBox.Items.Insert(0, new ListItem("All", ""));
            }
        }

        private void BindGrid()
        {
            try
            {
                string[] d = txtFileDate.Value.Split('/');
                DateTime DataDate = Convert.ToDateTime(d[1] + "/" + d[0] + "/" + d[2] + " 12:00:00 PM");
                Log.Debug("Date : " + d[1] + "/" + d[0] + "/" + d[2] + " 12:00:00 PM");
                Log.Debug("~/" + Config.MTMExportPath + "/" + DataDate.DayOfWeek.ToString() + "/" + DataDate.ToString("yyyyMMdd"));
                DataTable dt = FileManage.GetListFileRoot(Server.MapPath("~/" + Config.MTMExportPath + "/" + DataDate.DayOfWeek.ToString() + "/"), Config.MTMExportPath + "/" + DataDate.DayOfWeek.ToString() + "/", DataDate.ToString("yyyyMMdd"));
                GridData.DataSource = dt;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnViewPass_Click(object sender, EventArgs e)
        {
            try
            {
                               
                DataTable t = (DataTable)ViewState["Schedule"];
                string condition = ""; 
                string password = "";

                condition = "Round='" + RoundBox.SelectedValue + "'";
                if (!MutualFundsBox.SelectedValue.Equals(""))
                    condition += "And MutualFund='" + MutualFundsBox.SelectedValue + "' ";

                if (condition != "")
                    dt = t.Select(condition).CopyToDataTable();
                else
                    dt = t;                           

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    password += "MTM" +dt.Rows[i]["Encrypt"].ToString() + Config.GetParameterFile("089ENCRYPT_MTM")+" <BR>";
                }

                MessageBox.Text = password;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                CheckSendMailBox.Checked = false;               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); 
                MessageBox.Text = "เกิดข้อผิดพลาด (" + ex.Message + ")";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] d = DateBox.Text.Split('/');
                DateTime DataDate = Convert.ToDateTime(d[1] + "/" + d[0] + "/" + d[2] + " 12:00:00 PM");
                
                DataTable t = (DataTable)ViewState["Schedule"];
                string condition = "";
                condition = "Round='" + RoundBox.SelectedValue + "'";
                if (!MutualFundsBox.SelectedValue.Equals(""))
                    condition += "And MutualFund='" + MutualFundsBox.SelectedValue + "' ";

                if (condition != "")
                    dt = t.Select(condition).CopyToDataTable();
                else
                    dt = t;

                DataTable dtMutualFund = dt.DefaultView.ToTable(true, "MUTUALFUND");

                //Path Export MTM File
                string ExportPath = Server.MapPath("~/" + Config.MTMExportPath + "/" + DataDate.DayOfWeek.ToString() + "/" + RoundBox.SelectedItem.Text + "/");
                if (Directory.Exists(ExportPath))
                {
                    string[] f = Directory.GetFiles(ExportPath);
                    for (int l = 0; l < f.Length; l++)
                    {
                        File.Delete(f[l]);
                    }
                    Directory.Delete(ExportPath);
                }
                Directory.CreateDirectory(ExportPath);

               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataSet ds = Model_RPT_TR089OTHMTM.getDataSet(Config.ConnectionString, DateBox.Text, dt.Rows[i]["MUTUALFUND"].ToString(), dt.Rows[i]["SOURCE"].ToString(), dt.Rows[i]["OPENCLOSE"].ToString(), dt.Rows[i]["RATE"].ToString(), dt.Rows[i]["SPECIALSOURCE"].ToString(), dt.Rows[i]["ALLMDATE"].ToString());
                    Log.Info("-----Generate MTM File-----");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string FolderName = dt.Rows[i]["FILENAME"].ToString();
                        string FileName = dt.Rows[i]["FILENAME"].ToString() + "_" + DataDate.ToString("yyyyMMdd");
                        string OpenClose = "OpenClose";
                        if (dt.Rows[i]["OPENCLOSE"].ToString().Trim().Equals("1"))
                            OpenClose = "Open";
                        else if (dt.Rows[i]["OPENCLOSE"].ToString().Trim().Equals("0"))
                            OpenClose = "Close";

                        ReportDocument doc = new ReportDocument();
                        if (!dt.Rows[i]["TEMPLATE"].ToString().Equals(""))
                        {
                            string ReportName = Enum.GetName(typeof(MTMReport), Convert.ToInt32(dt.Rows[i]["TEMPLATE"].ToString()));
                            if (ReportName.Equals(MTMReport.PHATRA.ToString()))
                                doc = (ReportDocument)(new RPT_TR089DFMTMPATRA());
                            else if (ReportName.Equals(MTMReport.CLOSE.ToString()))
                                doc = (ReportDocument)(new RPT_TR089DFMTMC());
                            else
                                doc = (ReportDocument)(new RPT_TR089DFMTM());

                            Log.Info("File Type : " + dt.Rows[i]["FileType"].ToString());
                            Log.Info("File Name : " + FileName);
                            Log.Info("Folder Name : " + FolderName);

                            //Path Encrypt MTM File
                            string EncryptPath = Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FolderName + "/");
                            if (Directory.Exists(EncryptPath))
                            {
                                string[] f = Directory.GetFiles(EncryptPath);
                                for (int l = 0; l < f.Length; l++)
                                {
                                    File.Delete(f[l]);
                                }
                                Directory.Delete(EncryptPath);
                            }
                            Directory.CreateDirectory(EncryptPath);             
                

                            //Export MTM File
                            ReportExport.GenerateFile(ds.Tables[0], doc, dt.Rows[i]["FileType"].ToString(), Server.MapPath("~/" + Config.MTMExportPath + "/" + DataDate.DayOfWeek.ToString() + "/" + RoundBox.SelectedItem.Text), FileName);
                            //Export MTM to Encrypt
                            ReportExport.GenerateFile(ds.Tables[0], doc, dt.Rows[i]["FileType"].ToString(), Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FolderName), FileName);
                        }
                        

                        //Send Mail
                        if (CheckSendMailBox.Checked)
                        {
                            string FileNamewithExtension = FileName;
                            if (dt.Rows[i]["FileType"].ToString().ToUpper().Equals("PDF"))
                            {
                                FileNamewithExtension += ".pdf";
                            }
                            else if (dt.Rows[i]["FileType"].ToString().Equals("Excel"))
                            {
                                FileNamewithExtension += ".xls";
                            }
                            
                            string EncryptFile = Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FolderName);

                            //Encrypt file before send
                            Encryption(EncryptFile, FileName, dt.Rows[i]["Encrypt"].ToString());

                            List<Attachment> attach = new List<Attachment>();                            
                            attach.Add(new Attachment(EncryptFile + ".zip"));
                            
                            //Send email as attach                     
                            SendMail(Config.GetParameterFile("089To"), Config.GetParameterFile("089Cc"), FileName, attach);
                        }
                        
                    }
                    Log.Info("-----End Generate File-----");
                
                }

                //Compress File
                //Compress(DataDate.DayOfWeek.ToString());                                     
               

                MessageBox.Text = "ระบบสร้างไฟล์เรียบร้อยแล้ว";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);

                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); 
                MessageBox.Text = "เกิดข้อผิดพลาด (" + ex.Message + ")";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SearchBUtton_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void RoundBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFund();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        #region Methods

        public void Compress(string FolderName)
        {
            try
            {
                string[] d = DateBox.Text.Split('/');
                DateTime DataDate = Convert.ToDateTime(d[1] + "/" + d[0] + "/" + d[2] + " 12:00:00 PM");

                if (File.Exists(Server.MapPath("~/" + Config.MTMExportPath + "/" + FolderName + "/" + RoundBox.SelectedItem.Text + "_" + DataDate.ToString("yyyyMMdd") + ".zip")))
                    File.Delete(Server.MapPath("~/" + Config.MTMExportPath + "/") + FolderName + "/" + RoundBox.SelectedItem.Text + "_" + DataDate.ToString("yyyyMMdd") + ".zip");
                using (Package Zip = Package.Open(Server.MapPath("~/" + Config.MTMExportPath + "/" + FolderName + "/") + RoundBox.SelectedItem.Text + "_" + DataDate.ToString("yyyyMMdd") + ".zip", FileMode.OpenOrCreate))
                {
                    foreach (string F in Directory.GetFiles(Server.MapPath("~/" + Config.MTMExportPath + "/" + FolderName + "/" + RoundBox.SelectedItem.Text + "/"), "*.*", SearchOption.AllDirectories))
                    {
                        string F1 = F.Replace(' ', '_'); //ZipPackage can't handle spaces in filenames.
                        Uri URI = new Uri(F1.Substring(Server.MapPath("~/" + Config.MTMExportPath + "/" + FolderName + "/" + RoundBox.SelectedItem.Text + "/").Length), UriKind.Relative);

                        PackagePart ZipPart = Zip.CreatePart(PackUriHelper.CreatePartUri(URI), MediaTypeNames.Application.Zip, CompressionOption.Maximum);
                        byte[] B = File.ReadAllBytes(F);
                        ZipPart.GetStream().Write(B, 0, B.Length);
                    }
                }

                //Clear file after zip 
                string ExportPath = Server.MapPath("~/" + Config.MTMExportPath + "/" + DataDate.DayOfWeek.ToString() + "/" + RoundBox.SelectedItem.Text + "/");
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
                string message = "Please kindly recheck and feedback.";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Config.GetParameterFile("089From"));
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
                    mailclient.Host = Config.GetParameterFile("089SMTP");
                    mailclient.Port = int.Parse(Config.GetParameterFile("089SMTPPort"));
                    mailclient.EnableSsl = false;
                    mailclient.UseDefaultCredentials = true;

                    mailclient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }
        
        public void Encryption(string pathfile, string filename, string uniqueItems)
        {
            try
            {
             
                if (File.Exists(pathfile + ".zip"))
                    File.Delete(pathfile + ".zip");


                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = "MTM" + uniqueItems + Config.GetParameterFile("089ENCRYPT_MTM");
                        zip.AddDirectory(pathfile);

                        //zip by file
                        //zip.AddFile(Path + "/" + DateTime.Now.ToString("yyyyMMdd")+ "/" + prefixEncrypt[i] + "_" + filename + ".pdf","" );

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(pathfile + ".zip");
                    }

                    Log.Info("encrypt pass : " + "MTM" + uniqueItems + Config.GetParameterFile("089ENCRYPT_MTM"));
                    Log.Info("encrypt path : " + pathfile);

                    //Clear File After Zip
                    if (Directory.Exists(pathfile))
                    {
                        string[] f = Directory.GetFiles(pathfile);
                        for (int l = 0; l < f.Length; l++)
                        {
                            File.Delete(f[l]);
                        }
                        Directory.Delete(pathfile);
                    }
               // }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        #endregion
    }
}
