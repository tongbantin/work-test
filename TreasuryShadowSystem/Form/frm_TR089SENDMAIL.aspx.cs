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
using System.Net;
using System.Net.Mail;

using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class CustodianFund
    {
        public string Fund { get; set; }
        public string Custodian { get; set; }
        public int NumFiles { get; set; }
    }

    public partial class frm_TR089SENDMAIL : System.Web.UI.Page
    {
        private DataTable dt = new DataTable();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR089SENDMAIL));

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);
                GridData.RowCommand += new GridViewCommandEventHandler(GridData_RowCommand);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    dt = Model_MS_MTMSCHEDULE.getScheduleData(Config.ConnectionString);
                    DataTable t = dt.Select("Fund='" + GridData.Rows[int.Parse(e.CommandArgument.ToString())].Cells[2].Text + "' AND Custodian='" + GridData.Rows[int.Parse(e.CommandArgument.ToString())].Cells[3].Text + "'").CopyToDataTable();
                    BindEmail(t);

                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {";
                    EditScript += "'Close': function() {$(this).dialog('close');}}}); ";

                    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable tb = Model_MS_MTMSCHEDULE.getScheduleData(Config.ConnectionString);
                    tb.Columns.Add(new DataColumn("NumFiles"));
                    ViewState["Schedule"] = GetFileList(tb);
                    BindCustodian();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private DataTable GetFileList(DataTable tb)
        {
            try
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    DataTable temp = FileManage.GetListFile(Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + tb.Rows[i]["MutualFund"].ToString().Replace("/", "_")), Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + tb.Rows[i]["MutualFund"].ToString().Replace("/", "_"), DateTime.Now.ToString("yyyyMMdd"));
                    if (temp != null)
                        tb.Rows[i]["NumFiles"] = temp.Rows.Count;
                    else
                        tb.Rows[i]["NumFiles"] = 0;
                }  

                return tb;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return tb;
            }
        }

        private void BindCustodian()
        {
            try
            {
                dt = (DataTable)ViewState["Schedule"];
                DataTable t = dt.Select("Round='" + RoundBox.SelectedValue + "'").AsEnumerable().GroupBy(r => r.Field<string>("Custodian")).Select(g => g.First()).CopyToDataTable();
                CustodianBox.Items.Clear();
                CustodianBox.DataSource = t;
                CustodianBox.DataTextField = "Custodian";
                CustodianBox.DataValueField = "Custodian";
                CustodianBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                CustodianBox.Items.Clear();
            }
            finally
            {
                CustodianBox.Items.Insert(0, new ListItem("All", ""));
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridData.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                        if (chkRow.Checked)
                        {
                            dt = (DataTable)ViewState["Schedule"];
                            DataTable t = dt.Select("Fund='" + row.Cells[2].Text + "' And Custodian='" + row.Cells[3].Text + "' ").CopyToDataTable();
                            if (t != null)
                            {
                                List<Attachment> attach = new List<Attachment>();
                                for (int i = 0; i < t.Rows.Count; i++)
                                {
                                    Log.Info("Get File for Attached From : " + Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_")));
                                    DataTable local = FileManage.GetListFile(Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_")), Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_"), DateTime.Now.ToString("yyyyMMdd"));
                                    if (local != null)
                                    {
                                        Log.Info("Attached File(s) : " + local.Rows.Count.ToString());
                                        for (int j = 0; j < local.Rows.Count; j++)
                                        {
                                            attach.Add(new Attachment(Server.MapPath("~/" + local.Rows[j]["Path"].ToString() + "/" + local.Rows[j]["Filename"].ToString())));
                                        }
                                    }
                                }
                                if (attach.Count > 0)
                                {
                                    Log.Info("Send Mail To : " + t.Rows[0]["Email"].ToString() + ", CC : " + t.Rows[0]["EmailCc"].ToString());
                                    SendMail(t.Rows[0]["Email"].ToString(), t.Rows[0]["EmailCc"].ToString(), attach);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
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

        protected void FundsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindCustodian();
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
                dt = (DataTable)ViewState["Schedule"];                
                string condition = "Round='" + RoundBox.SelectedValue + "'";
                if (!CustodianBox.SelectedValue.Equals(""))
                    condition += " And Custodian='" + CustodianBox.SelectedValue + "'";
                condition += " And NumFiles>0";

                int numsfile = this.NumFiles(dt.Select(condition).CopyToDataTable());

                DataTable t = dt.Select(condition).AsEnumerable().GroupBy(r => new { Fund = r.Field<string>("Fund"), Custodian = r.Field<string>("Custodian") }).Select(g => g.First()).CopyToDataTable();
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    DataRow [] dr = dt.Select(condition + " And Fund='" + t.Rows[i]["Fund"] + "' And Custodian='" + t.Rows[i]["Custodian"] + "'");
                    int rowsNum = 0;
                    for (int j = 0; j < dr.Count(); j++)
                    {
                        rowsNum += int.Parse(dr[j]["NumFiles"].ToString());
                    }
                    t.Rows[i]["NumFiles"] = rowsNum;
                }

                GridData.DataSource = t;
                GridData.DataBind();
                TotalBox.Text = numsfile.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                GridData.DataSource = null;
                GridData.DataBind();
                TotalBox.Text = "0";
            }
         }

        int NumFiles(DataTable t)
        {
            try
            {
                DataTable dt = new DataTable();
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    DataTable temp = FileManage.GetListFile(Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_")), Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_"), DateTime.Now.ToString("yyyyMMdd"));
                    if (temp != null)
                        dt.Merge(temp);
                }
                if (dt != null)
                    return dt.Rows.Count;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return 0;
            }
        }

        void BindEmail(DataTable t)
        {
            try
            {
                CustodianView.Text = t.Rows[0]["Custodian"].ToString();
                EmailView.Text = t.Rows[0]["Email"].ToString();
                EmailCcView.Text = t.Rows[0]["EmailCc"].ToString();                
                DataTable dt = new DataTable();
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    DataTable temp = FileManage.GetListFile(Server.MapPath("~/" + Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_")), Config.MTMExportPath + "/" + DateTime.Now.DayOfWeek.ToString() + "/" + t.Rows[i]["MutualFund"].ToString().Replace("/", "_"), DateTime.Now.ToString("yyyyMMdd"));
                    if (temp!=null)
                        dt.Merge(temp);
                }
                if (dt != null)
                {
                    FileAttachView.DataSource = dt;
                    FileAttachView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                FileAttachView.DataSource = null;
                FileAttachView.DataBind();
            }
        }

        protected void SendMail(string To, string Cc, List<Attachment> Attach)
        {
            try
            {
                //Parameter
                string to = To;
                string subject = "Mark-To-Market File";
                bool isHtml = true;
                string message = "Please kindly recheck and feedback.";

                MailMessage mail = new MailMessage();
                // Set the to and from addresses.
                // The from address must be your GMail account
                mail.From = new MailAddress(Config.GetParameterFile("089From"));
                string[] tto = to.Split(',');
                for (int i = 0; i < tto.Count(); i++)
                {
                    mail.To.Add(new MailAddress(tto[i].Replace("\n","")));
                }
                if (!Cc.Trim().Equals(""))
                {
                    string[] ccto = Cc.Split(',');
                    for (int j = 0; j < ccto.Count(); j++)
                    {
                        mail.CC.Add(new MailAddress(ccto[j].Replace("\n","")));
                    }
                }
                // Define the message
                mail.Subject = subject;
                mail.IsBodyHtml = isHtml;
                mail.Body = message;
                foreach (Attachment a in Attach)
                {
                    mail.Attachments.Add(a);
                }

                // Create a new Smpt Client using Google's servers
                var mailclient = new SmtpClient();
                //mailclient.Host = "smtp.gmail.com";//ForGmail
                mailclient.Host = Config.GetParameterFile("089SMTP");
                mailclient.Port = int.Parse(Config.GetParameterFile("089SMTPPort")); ;

                // This is the critical part, you must enable SSL
                //mailclient.EnableSsl = true;//ForGmail
                mailclient.EnableSsl = false;
                mailclient.UseDefaultCredentials = true;

                // Specify your authentication details
                //mailclient.Credentials = new System.Net.NetworkCredential("SomeGmailAccount@gmail.com", "PaswordXYX123");//ForGmail
                //mailclient.Credentials = new System.Net.NetworkCredential("noreply@gmail.com", "PaSsWaRd");

                mailclient.Send(mail);
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
                BindCustodian();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
