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

using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryModel.Data;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryReport.Report;
using System.Web.Services;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.IO.Packaging;
using System.Net.Mime;

namespace TreasuryShadowSystem.TR146OBOPFXCF
{
    public partial class frm_PreConfirm : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_PreConfirm));
        public string TheUrl = "";
        string reportname = "TR146OBOPFXCF";
        ReportClass rpt = new ReportClass();

        protected void Page_Init(object sender, EventArgs e)
        {

            GridPreConfirm.PageIndexChanging += new GridViewPageEventHandler(GridPreConfirm_PageIndexChanging);
            GridPreConfirm.RowCommand += new GridViewCommandEventHandler(GridPreConfirm_RowCommand);
        }

        void GridPreConfirm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.CommandName == "Download")
                {
                    int row = Int32.Parse(e.CommandArgument.ToString());
                    string key = GridPreConfirm.DataKeys[row].Value.ToString();
                    DownloadPDF(key);
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindFund();
                    EmailBox.Text = Config.GetParameterFile("146MAILTO").Trim();
                    txtExecuteDate.Value = ((DateTime)Session["BranchDate"]).ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        void GridPreConfirm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridPreConfirm.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void BindFund()
        {
            try
            {
                DataTable t = TreasuryModel.Data.PreConfirmation.GetFund(Config.ConnectionString, Config.GetParameterFile("146PARTY"));
                FundBox.Items.Clear();
                FundBox.DataSource = t;
                FundBox.DataTextField = "GRPID";
                FundBox.DataValueField = "GRPID";
                FundBox.DataBind();

                FundBox.Items.Insert(0, new ListItem("--ALL--", ""));
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void BindGrid()
        {
            try
            {
                string WithoutParty = Config.GetParameterFile("146WITHOUTPARTY");
                DataSet ds = Model_RPT_TR146OBOPFXCF.getPreConfirm(Config.ConnectionString, WithoutParty, FundBox.SelectedValue.ToString(), FromDealNoBox.Text.Trim(), ToDealNoBox.Text.Trim(), txtExecuteDate.Value.Trim());
                GridPreConfirm.DataSource = ds.Tables[0];
                GridPreConfirm.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
            
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
            //ExportReport();
        }

        protected void ExportReport()
        {
            
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateTenor(string DealNo, string Tenor)
        {
            string[] args = { Tenor, DealNo };

            return TreasuryModel.Data.PreConfirmation.Update(Config.ConnectionString, EnumPreConfirm.TENOR, args, TreasuryModel.Data.PreConfirmation.IsExisting(Config.ConnectionString, DealNo));
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateSwapPoint(string DealNo, string SwapPoint)
        {
            string[] args = { SwapPoint, DealNo };

            return TreasuryModel.Data.PreConfirmation.Update(Config.ConnectionString, EnumPreConfirm.SWAP, args, TreasuryModel.Data.PreConfirmation.IsExisting(Config.ConnectionString, DealNo));
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateDescription(string DealNo, string NearCode, string NearDescription, string FarCode, string FarDescription, string Tenor, string Remark)
        {
            string[] args = { NearCode, NearDescription, FarCode, FarDescription, Tenor, Remark, DealNo };

            return TreasuryModel.Data.PreConfirmation.Update(Config.ConnectionString, EnumPreConfirm.DESCRIPTION, args, TreasuryModel.Data.PreConfirmation.IsExisting(Config.ConnectionString, DealNo));
        }

        [WebMethod(EnableSession = true)]
        public static object GetDescription(string DealNo)
        {
            return TreasuryModel.Data.PreConfirmation.GetDescription(Config.ConnectionString, DealNo);
        }

        protected void SendMailSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList deallist = GetData();
                SendMail(deallist);
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void DownloadPDF(string DealNo)
        {
            try
            {
                try
                {
                    string Product = Model_RPT_TR146OBOPFXCF.getProduct(Config.ConnectionString, DealNo);
                    ReportDocument rpt = Model_RPT_TR146OBOPFXCF.getReport(Config.ConnectionString, FundBox.SelectedValue.ToString(), Product, DealNo);
                    rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, DealNo.Trim());
                    //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/Temporary/" + DealNo + ".pdf"));
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
                //Response.Redirect("~/View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&p=" + key, false );
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private ArrayList GetData()
        {
            ArrayList arr = new ArrayList();

            try
            {
                //Clear Folder
                string[] f = Directory.GetFiles(Server.MapPath("~/Temporary/PreConfirmation/"), "*.pdf");
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

                for (int i = 0; i < GridPreConfirm.Rows.Count; i++)
                {
                    try
                    {
                        CheckBox chk = (CheckBox)GridPreConfirm.Rows[i].Cells[0].FindControl("chkDocument");
                        if (chk.Checked)
                        {
                            arr.Add(GridPreConfirm.DataKeys[i].Value.ToString());
                            string DealNo = GridPreConfirm.DataKeys[i].Value.ToString();
                            string Product = Model_RPT_TR146OBOPFXCF.getProduct(Config.ConnectionString, DealNo);
                            ReportDocument rpt = Model_RPT_TR146OBOPFXCF.getReport(Config.ConnectionString, FundBox.SelectedValue.ToString(), Product, DealNo);
                            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/Temporary/PreConfirmation/" + DealNo + ".pdf"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Could not create Preconfirmation DealNo : " + GridPreConfirm.DataKeys[i].Value.ToString());
                    }
                }
                return arr;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        protected void SendMail(ArrayList DealList)
        {
            try
            {
               
                //Parameter
                string to = Config.GetParameterFile("146MAILTO");
                bool isHtml = true;
                string message = Config.GetParameterFile("146MESSAGE");

                MailMessage mail = new MailMessage();
                // Set the to and from addresses.
                // The from address must be your GMail account
                mail.From = new MailAddress(Config.GetParameterFile("146FROM"));
                string[] tto = to.Split(',');
                for (int i = 0; i < tto.Count(); i++)
                {
                    mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                }
                string Cc = Config.GetParameterFile("146CC");
                if (!Cc.Trim().Equals(""))
                {
                    string[] ccto = Cc.Split(',');
                    for (int j = 0; j < ccto.Count(); j++)
                    {
                        mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                    }
                }
                // Define the message
                mail.Subject = Config.GetParameterFile("146SUBJECT");
                mail.Subject = mail.Subject.Replace("{Date}",DateTime.Now.ToString("dd MMM yyyy"));
                //mail.Subject = mail.Subject.Replace("{Party}", FundBox.SelectedValue);
                if (FundBox.SelectedValue.Equals("CORPORATE") || FundBox.SelectedValue.Equals(""))
                {
                    string CMNE = PreConfirmation.GetCMNE(Config.ConnectionString, DealList[0].ToString()).Trim();
                    mail.Subject = mail.Subject.Replace("{Party}", CMNE);
                }
                else
                {
                    string[] party = Config.GetParameterFile("146PARTY").Split(',');
                    string[] nickname = Config.GetParameterFile("146PARTYNICKNAME").Split(',');
                    var result = party.Select((s, i) => new { Position = i, Text = s }).Where(o => o.Text.Equals(FundBox.SelectedValue) == true).FirstOrDefault();
                    if (result != null)
                        mail.Subject = mail.Subject.Replace("{Party}", nickname[result.Position]);
                }
                mail.Subject = mail.Subject.Replace("{Party}", "");
                mail.IsBodyHtml = isHtml;
                mail.Body = message;

                string[] f = Directory.GetFiles(Server.MapPath("~/Temporary/PreConfirmation/"), "*.pdf");
                for (int i = 0; i < f.Length; i++)
                {
                    try
                    {
                        Attachment attach = new Attachment(f[i]);
                        mail.Attachments.Add(attach);
                    }
                    catch (Exception ex)
                    {

                    }
                }

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

        protected void btnDownloadSelected_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList arr = GetData();

                if (arr.Count > 0)
                {
                    Compress();
                    string FolderName = Config.GetParameterFile("146ExportPath");
                    string str = "~/Temporary/" + FolderName + DateTime.Now.ToString("yyyyMMdd") + ".zip";
                    try
                    {
                        Response.ContentType = "application/zip";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FolderName + DateTime.Now.ToString("yyyyMMdd") + ".zip");
                        Response.TransmitFile(Server.MapPath(str));
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Text = "ยังไม่ได้เลือกรายการ กรุณาทำรายการใหม่";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public void Compress()
        {
            try
            {
                string FolderName = Config.GetParameterFile("146ExportPath");
                File.Delete(Server.MapPath("~/Temporary/") + FolderName + DateTime.Now.ToString("yyyyMMdd") + ".zip");

                using (Package Zip = Package.Open(Server.MapPath("~/Temporary/") + FolderName + DateTime.Now.ToString("yyyyMMdd") + ".zip", FileMode.Create))
                {
                    foreach (string F in Directory.GetFiles(Server.MapPath("~/Temporary/" + FolderName + "/"),
                                                            "*.pdf",
                                                            SearchOption.AllDirectories))
                    {
                        string F1 = F.Replace(' ', '_'); //ZipPackage can't handle spaces in filenames.
                        Uri URI = new Uri(F1.Substring(Server.MapPath("~/Temporary/" + FolderName + "/").Length), UriKind.Relative);
                        PackagePart ZipPart = Zip.CreatePart(PackUriHelper.CreatePartUri(URI),
                                                             "",
                                                             CompressionOption.Normal);
                        byte[] B = File.ReadAllBytes(F);
                        ZipPart.GetStream().Write(B, 0, B.Length);
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
