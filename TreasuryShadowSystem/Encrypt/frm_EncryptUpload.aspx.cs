using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryCommon.Common;
using TreasuryModel.Master;
using System.Data;
using log4net;
using System.IO;
using System.Drawing;
using System.Net.Mail;

namespace TreasuryShadowSystem.Encrypt
{
    public partial class frm_EncryptUpload : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_EncryptUpload));
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                if (!IsPostBack) {

                    bindCustomer(Source.SelectedValue.Trim(), "");
                    bindProdType();
                    clearInputValue();
               

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSelSource_Click(object sender, EventArgs e)
        {
            try
            {
                string source = Source.SelectedValue.Trim();
                bindCustomer(source, "");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

         protected void btnSearch_Click(object sender, EventArgs e)
        {            
            try
            {
                string searchCust = searchCustomer.Text.Trim();
                string source = Source.SelectedValue.Trim();
                bindCustomer(source, searchCust);
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
                 //Check Encrypt Type
                 string password = "";

                 if (selProdtype.Checked)
                 {
                     string[] prodtype = ProdTypeDDL.SelectedValue.Split('|');
                     string customer = CustomerDDL.SelectedValue.Trim();

                     //edit cut taxid 5 digit for STN 26.10.2021
                     if (prodtype[0].Equals("STN")) {
                         if (customer.Length == 13) 
                             customer = customer.Substring(customer.Length, -5);
                     }                     

                     password = customer + prodtype[1];
                                          
                     MessageBox.Text = password;
                     ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                 }
                 else if (selInput.Checked)
                 {
                     password = fillPassword.Text.Trim();
                                          
                     MessageBox.Text = password;
                     ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                 }
                 else
                 {
                     MessageBox.Text = "กรุณาระบุวิธีการเข้ารหัสผ่าน";
                     ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
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
                Boolean checkEncrypt = false;
                string password = "" , mailTo = "";
                string defMailTo = "kk_tro@kkpfg.com";
                string host = Config.GetParameterFile("089SMTP");
                int port = int.Parse(Config.GetParameterFile("089SMTPPort"));
                string mailFrom = Config.GetParameterFile("089From");
                string subMail = "", msgMail = "";
                string defSubMail = "Zip files from Webtreasury Application";
                string defMsgMail = "Please kindly recheck and feedback.";
                string folder = "";

                //Check Encrypt Type
                if (selProdtype.Checked) {

                    Log.Debug("upload by product");                    
                    string[] prodtype = ProdTypeDDL.SelectedValue.Split('|');
                    string customer = CustomerDDL.SelectedValue.Trim();

                    //edit cut taxid 5 digit for STN 26.10.2021
                    if (prodtype[0].Equals("STN"))
                    {
                        if (customer.Length == 13)
                            customer = customer.Substring(customer.Length, -5);
                    }  

                    password = customer + prodtype[1];
                    Log.Debug("password :" + password);


                    //Find email form product group
                    if(prodtype[0] != null && !prodtype[0].Equals("")){

                        //Get Email Param
                        DataTable dt = Model_MS_WEBPARAM.getEmailByProdtype(Config.ConnectionString, "ENCRYPT_" + prodtype[0]);
                        if(dt != null) { 
                            mailTo = dt.Rows[0][1].ToString();
                        }
                        else mailTo = defMailTo; //Default Email

                        //Get Subject Param
                        DataTable sub = Model_MS_WEBPARAM.getSubjectByProdtype(Config.ConnectionString, "ENCRYPT_" + prodtype[0]);
                        if(sub != null){
                            subMail = sub.Rows[0][1].ToString();
                        }
                        else subMail = defSubMail;                    
                        
                       //Get Msg Param
                        DataTable msg = Model_MS_WEBPARAM.getMessageByProdtype(Config.ConnectionString, "ENCRYPT_" + prodtype[0]);
                        if (msg != null)
                        {
                            msgMail = msg.Rows[0][1].ToString();
                        }
                        else msgMail = defMsgMail;
                        
                    }else{
                        mailTo = defMailTo; //Default Email
                    }

                    checkEncrypt = true;
                    //set folder for file 
                    folder = DateTime.Now.ToString("yyyyMMdd") + "/" + prodtype[0];

                    Log.Debug("mailTo :" + mailTo);
                    Log.Debug("subMail :" + subMail);
                    Log.Debug("msgMail :" + msgMail);
                    Log.Debug("folder :" + folder);

                }
                else if (selInput.Checked){

                    Log.Debug("upload by manual");

                    password = fillPassword.Text.Trim();
                    mailTo = fillEmail.Text.Trim();

                    Log.Debug("password :" + password);

                    if (!password.Equals("") && password != null && !mailTo.Equals("") && mailTo != null)
                    {
                        checkEncrypt = true;
                        //set folder for file 
                        folder = DateTime.Now.ToString("yyyyMMdd") + "/Manual";

                        Log.Debug("mailTo :" + mailTo);
                        Log.Debug("folder :" + folder);

                    }
                    else {
                        checkEncrypt = false;                        
                        MessageBox.Text = " กรุณากรอก รหัสผ่าน และ อีเมลล์ที่ต้องการนำส่ง ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);                    
                    }

                }else{
                    checkEncrypt = false;                    
                    MessageBox.Text = "กรุณาระบุวิธีการเข้ารหัสผ่าน";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }                

                // Browse and save file to folder "Encrypt"
                string path = Server.MapPath("~/Temporary/Encrypt/");
                //string folder = DateTime.Now.ToString("yyyyMMdd");
                Log.Debug("checkEncrypt :" + checkEncrypt);
                if (checkEncrypt)
                {
                    if (FileUpload.PostedFile.ContentLength > 1)
                    {
                        HttpFileCollection hfc = Request.Files;

                        for (int i = 0; i < hfc.Count; i++)
                        {
                            
                            HttpPostedFile hpf = hfc[i];
                            
                            // Check path directory 
                            Log.Debug("path directory : " + path + folder);

                            if (!Directory.Exists(path + folder)) Directory.CreateDirectory(path + folder);
                            if (hpf.ContentLength > 0)
                            {
                                hpf.SaveAs(path + folder + "/" + hpf.FileName);
                            }
                        }

                        int fileCount = Directory.GetFiles(path + folder + "/" , "*.*", SearchOption.TopDirectoryOnly).Length;

                        Log.Debug("fileCount :" + fileCount);
                        Log.Debug("uploadCount :" + hfc.Count);

                        if (fileCount == hfc.Count) //record files correctly
                        {
                            // Zip file with password
                            Encryption.EncryptByFolder(path, folder, password);

                            // Send email
                            List<Attachment> attach = new List<Attachment>();
                            attach.Add(new Attachment(path + folder + ".zip"));
                       
                            //string subject = "Zip files from Webtreasury Application";
                            Encryption.SendMail(host, port, mailFrom, mailTo, "", subMail, msgMail, attach);

                            bindCustomer(Source.SelectedValue.Trim(),"");
                            bindProdType();
                            clearInputValue();

                            MessageBox.Text = "ข้อมูลถูกเข้ารหัส และส่งไปยังอีเมลล์ที่ระบุ เรียบร้อยแล้ว";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                            
                        }
                        else
                        {
                            MessageBox.Text = "ระบบไม่สามารถนำเข้าข้อมูลได้ กรุณาทำรายการใหม่";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                    }
                    else
                    {
                        MessageBox.Text = "กรุณาเลือกไฟล์ที่ต้องการนำเข้า";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = "เกิดข้อผิดพลาด " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }       
        }
        

        private void bindProdType()
        {
            try
            {

                ProdTypeDDL.DataSource = Model_MS_WEBPARAM.getParamValueList(Config.ConnectionString, "ENCRYPT");
                ProdTypeDDL.DataTextField = "PRODTYPE";
                ProdTypeDDL.DataValueField = "SELVALUE";
                ProdTypeDDL.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ProdTypeDDL.Items.Clear();
            }
           
        }

        private void bindCustomer(string resource , string searchCust)
        {
            try
            {
                CustomerDDL.DataSource = Model_MS_CUSTOMER.GetCustomerAllSource(Config.ConnectionString, resource, searchCust);
                CustomerDDL.DataTextField = Server.HtmlDecode("CUSTNAME");
                CustomerDDL.DataValueField = "CUSTCODE";
                CustomerDDL.DataBind();
                CustomerDDL.Attributes["style"] = "width: 200px; max-width: 200px";
               

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                CustomerDDL.Items.Clear();
            }
           
        }

        private void clearInputValue()
        {
            try
            {
                //Clear input value
                selProdtype.Checked = true;
                selInput.Checked = false;

                searchCustomer.Text = "";
                fillPassword.Text = "";
                fillEmail.Text = "";
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        /*
        [WebMethod(EnableSession = true)]
        public static object ViewPassword()
        {
            return TreasuryBusiness.Business.SecAssetBusiness.LoadSecAsset(Config.ConnectionString);

            try
            {
                int obj = SecAssetRepository.LoadSecAsset(ConnectionString);
                if (obj != -99)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Load Data failed" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }

        }
         * */

    }
}
