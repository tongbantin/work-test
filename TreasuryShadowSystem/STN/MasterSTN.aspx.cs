using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net.Mail;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;



namespace TreasuryShadowSystem.STN
{
    public partial class MasterSTN : System.Web.UI.Page
    {
        public string TheUrl = "";
        public string TheUrl2 = "";

        private static ILog Log = log4net.LogManager.GetLogger(typeof(MasterSTN));

        #region Action 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridViewReport.PageIndexChanging += new GridViewPageEventHandler(GridViewReport_PageIndexChanging);
                GridViewReport.RowDataBound += new GridViewRowEventHandler(GridViewReport_RowDataBound);
                GridViewReport.RowEditing += new GridViewEditEventHandler(GridViewReport_RowEditing);
                GridViewReport.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewReport_RowCancelingEdit);
                GridViewReport.RowCommand += new GridViewCommandEventHandler(GridViewReport_RowCommand);
                GridViewReport.RowDeleting += new GridViewDeleteEventHandler(GridViewReport_RowDeleting);
                //sub gridview
                //GridPerson.RowCommand += new GridViewCommandEventHandler(GridPerson_RowCommand);

               
                if (!IsPostBack)
                {
                    BindGridReport();
                    BindProductddl();



                }
                else
                {

                    iReport.Attributes["src"] = "";
                    iReport2.Attributes["src"] = "";

                }




            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }


        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindGridReport();
        }

        protected void DialogSubmit_Click(object sender, EventArgs e)
        {

            try
            {

                STNMASTER master = new STNMASTER();

                if (ActionBox.Value.Equals("Edit"))
                {
                    master.MASSTNID = int.Parse(hdMASSTNID.Value);
                    master.KKSYMBOL = hdSymbol.Value;
                    master.STN_TYPE = hdProduct.Value;
                    master.SUM_RECORD = int.Parse(hdRecord.Value);
                    master.SUM_AMOUNT = decimal.Parse(hdAmount.Value);
                    master.UPDATE_BY = HttpContext.Current.Session["UserName"].ToString();
                    master.STN_SEQ = hdSeq.Value;
                    master.STN_SEQDATE = hdSeqDate.Value;

                    bool issuccess = false;
                    issuccess = STNMASTER.UpdateMaster(master);

                    if (issuccess)
                    {
                        BindGridReport();
                        MessageBox.Text = "แก้ไขข้อมูลเรียบร้อยแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else
                    {
                        MessageBox.Text = "ระบบไม่สามารถแก้ไขข้อมูลได้ กรุณาทำรายการใหม่";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }

                } 
                //Insert
                else
                {
                    bool haveSymbol = false;

                    master.KKSYMBOL = hdSymbol.Value;
                    master.STN_TYPE = hdProduct.Value;
                    master.SUM_RECORD = int.Parse(hdRecord.Value);
                    master.SUM_AMOUNT = decimal.Parse(hdAmount.Value);
                    master.CREATE_BY = HttpContext.Current.Session["UserName"].ToString();
                    master.STN_SEQ = hdSeq.Value;
                    master.STN_SEQDATE = hdSeqDate.Value;

                    haveSymbol = STNMASTER.CheckSymbol(master.KKSYMBOL);

                    if (!haveSymbol)
                    {

                        bool issuccess = false;
                        issuccess = STNMASTER.AddMaster(master);

                        if (issuccess)
                        {
                            BindGridReport();
                            MessageBox.Text = "ระบบนำเข้าข้อมูลเรียบร้อยแล้ว";
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

                        MessageBox.Text = "มีข้อมูล Symbol นี้ในระบบแล้ว";
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

        #endregion

        #region Events

        void GridViewReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        void GridViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string MASSTNID = GridViewReport.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gd = e.Row.FindControl("GridPerson") as GridView;    
                    BindPerson(gd, MASSTNID);

                    //GridPerson.RowCommand += new GridViewCommandEventHandler(GridPerson_RowCommand);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
     
        
        void GridViewReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GridViewReport.PageIndex = e.NewPageIndex;
                BindGridReport();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        void GridViewReport_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            try
            {
                GridViewReport.EditIndex = -1;
                BindGridReport();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewReport_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                GridViewReport.EditIndex = e.NewEditIndex;
                BindGridReport();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridPerson_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("Select"))
            {
                Log.Info("test");
            }
        }

        void GridViewReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindMaster(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:750, modal: true ";
                    EditScript += ", open: function(){ } ";
                    EditScript += ",'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + hdSymbol.ClientID + "').val($('#" + txtsymbol.ClientID + "').val());";
                    EditScript += "$('#" + hdRecord.ClientID + "').val($('#" + txtrecord.ClientID + "').val());";
                    EditScript += "$('#" + hdAmount.ClientID + "').val($('#" + txtamount.ClientID + "').val());";
                    EditScript += "$('#" + hdProduct.ClientID + "').val($('#" + productddl.ClientID + "').val());";
                    EditScript += "$('#" + hdSeq.ClientID + "').val($('#" + txtseq.ClientID + "').val());";
                    EditScript += "$('#" + hdSeqDate.ClientID + "').val($('#" + txtseqdate.ClientID + "').val());";
                    EditScript += "$('input#" + DialogSubmit.ClientID + "').click();";
                    EditScript += "},'Close': function() {$(this).dialog('close'); $('#" + txtsymbol.ClientID + "').val('');$('#" + txtrecord.ClientID + "').val('');$('#" + txtamount.ClientID + "').val('');$('#" + productddl.ClientID + "').val(''); }}}); ";

                    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);
                }
                else if (e.CommandName.Equals("Delete"))
                {

                    Delete(e.CommandArgument.ToString());

                }
                else if (e.CommandName.Equals("PriceClose"))
                {
                    Log.Info("Click Closed Price");

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];

                    //rptpriceclose.DataSource = null;
                    //rptpriceclose.DataBind();

                    DataTable dt = KKBSTN.getUnderlyingByKKSymbol(symbol);

                    if(dt.Rows.Count > 0){

                        txtsettledays.Text = dt.Rows[0]["SETTLE_DAYS"].ToString();
                        txtsettlefx.Text = dt.Rows[0]["SETTLE_FXRATE"].ToString();

                        //show & hide row knockout fxrate on popup
                        if (!dt.Rows[0]["UNDERLYING_CURR"].ToString().Equals("THB"))
                        { settlerate.Style.Add("display", "block"); }
                        else
                        { settlerate.Style.Add("display", "none"); }        

                        
                        rptpriceclose.DataSource = dt;
                        rptpriceclose.DataBind();

                        
                        Log.Info("##dialogpriceclose start");

                        string EditScript = "$('#dialogpriceclose').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:750, modal: true ";
                        EditScript += ", open: function(){ } ";
                        EditScript += ",'buttons': {'Submit': function() { "; 
                        EditScript += "$('#" + hdSettledays.ClientID + "').val($('#" + txtsettledays.ClientID + "').val());";
                        EditScript += "$('#" + hdkkSymbol.ClientID + "').val('" + symbol + "');";
                        EditScript += "$('#" + hdSettleFx.ClientID + "').val($('#" + txtsettlefx.ClientID + "').val());"; ;

                        EditScript += "$('input#" + btnSubmitpriceclose.ClientID + "').click(); ";
                         
                        EditScript += "},'Close': function() { ";
                        EditScript += " $(this).dialog('close');  ";
                        EditScript += " $('.txtpriceclose').val(''); ";
                        EditScript += " $('.txtsettledays').val('');  ";
                        EditScript += " $('.txtsettlefx').val('');  ";
                        EditScript += " }}}); "; 
                        ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);

                        Log.Info("##dialogpriceclose end");

                    }
                    else
                    {

                        MessageBox.Text = "No person in this symbol!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);


                    }
                    
                }
                else if (e.CommandName.Equals("PriceKnockout"))
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];

                    rptpriceko.DataSource = null;
                    rptpriceko.DataBind();

                    DataTable dt = KKBSTN.getUnderlyingToKnockout(symbol);

                    if (dt.Rows.Count > 0)
                    {

                        //rdostocktype.SelectedValue = dt.Rows[0]["KO_STOCK_TYPE"].ToString();
                        //disable radio button can't select just show
                        rdostocktype.Enabled = false;
                        rdostocktype.Attributes.Remove("onClick");

                        txtdateko.Text = dt.Rows[0]["KO_DATE"].ToString();
                        txtfxrateko.Text = dt.Rows[0]["KO_FXRATE"].ToString();

                        //show & hide row knockout fxrate on popup
                        if (!dt.Rows[0]["UNDERLYING_CURR"].ToString().Equals("THB"))
                        { 
                            korate.Style.Add("display", "block");
                            rdostocktype.SelectedValue = "1";
                        
                        }
                        else 
                        { 
                            korate.Style.Add("display", "none");
                            rdostocktype.SelectedValue = "0";
                        
                        }                        

                        rptpriceko.DataSource = dt;
                        rptpriceko.DataBind();

                        string EditScript = "$('#dialogpriceko').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:750, modal: true ";
                        EditScript += ", open: function(){ } ";
                        EditScript += ",'buttons': {'Submit': function() {";
                        EditScript += "$('#" + hdkkSymbol.ClientID + "').val('" + symbol + "');";
                        EditScript += "$('input#" + btnSubmitKO.ClientID + "').click(); ";
                        EditScript += "},'Close': function() {$(this).dialog('close');  }}}); ";
                        ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);

                    }
                    else {

                        MessageBox.Text = "No person in this symbol!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                 
                        
                    }
                
                }

                else if (e.CommandName.Equals("ConfirmPrice"))
                {//Send Mail Confirm & Pricing                  

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    ConfirmPrice(symbol, type);

                }
                else if (e.CommandName.Equals("OpenConfirmPrice"))
                {//Open File Confirm & Pricing                  

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    OpenConfirmPrice(symbol, type);

                }                
                else if (e.CommandName.Equals("Confirm"))
                {//Confirm                   

                    string[] args = e.CommandArgument.ToString().Split(new char[] {','});
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    Confirm(symbol,type);
                    
                }
                else if (e.CommandName.Equals("Pricing"))
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    Pricing(symbol, type);
                }
                else if (e.CommandName.Equals("Settlement"))
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    Settlement(symbol, type);
                }
                    
                else if (e.CommandName.Equals("OpenSettlement"))
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    OpenSettlement(symbol, type);
                }
                else if (e.CommandName.Equals("Knockout"))
                {

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    Knockout(symbol, type);
                }
                else if (e.CommandName.Equals("OpenKnockout"))
                {

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    OpenKnockout(symbol, type);
                }                  
                else if (e.CommandName.Equals("ViewPassword"))
                {

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    //string stnid = args[2];
                    ViewPassword(symbol, type);
                }
                /*
                else if (e.CommandName.Equals("Unwind"))
                {

                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string symbol = args[0];
                    string type = args[1];
                    string stnid = args[2];
                    Unwind(symbol, type, stnid);
                } 
                 */
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindPerson(GridView gd, string MASSTNID)
        {
            try
            {
                gd.DataSource = STNMASTER.getSTN(int.Parse(MASSTNID));
                gd.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        private void BindProductddl()
        {

            try
            {

                List<STNCONFIG> ConfigList = new List<STNCONFIG>();

                DataTable dt = new DataTable();
                dt = STNCONFIG.Search("");

                ConfigList = STNCONFIG.ConvertDataTableToListConfig(dt);

                productddl.Items.Clear();
                productddl.DataSource = ConfigList;
                productddl.DataValueField = "STN_TYPE";
                productddl.DataTextField = "STN_TYPE_FULLNAME";
                productddl.DataBind();


            }
            catch (Exception ex) { }
        }
        
        private void BindGridReport()
        {

            try
            {

                Log.Info("BindGridReport MasterSTN");

                List<STNMASTER> MasterList = new List<STNMASTER>();

                string symbol = txtsymbols.Text;
                DataTable dt = new DataTable();
                dt = STNMASTER.Search(symbol);

                MasterList = ConvertDataTableToListMaster(dt);


                GridViewReport.DataSource = MasterList;
                GridViewReport.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindMaster(string id)
        {
            try
            {
                string key = GridViewReport.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = STNMASTER.getMasterByID(key);
                hdMASSTNID.Value = key;
                ActionBox.Value = "Edit";
                productddl.SelectedValue = dt.Rows[0]["STN_TYPE"].ToString().Trim();
                txtsymbol.Text = dt.Rows[0]["KKSYMBOL"].ToString().Trim();
                txtrecord.Text = dt.Rows[0]["SUM_RECORD"].ToString().Trim();
                txtamount.Text = dt.Rows[0]["SUM_AMOUNT"].ToString().Trim();
                txtseq.Text = dt.Rows[0]["STN_SEQ"].ToString().Trim();
                txtseqdate.Text = dt.Rows[0]["STN_SEQDATE"].ToString().Trim();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        #endregion

        #region Method

        protected void ConfirmPrice(string symbol, string type)
        {
            try
            {
                string pathfile = Server.MapPath("~/Temporary/STNReport/");
                string folder = "";
                string filename_conf = "Confirm_";
                string filename_price = "Pricing Supplement_";                               
                ReportClass reportConf = null , reportPrice = null;
                string stncc = Config.GetParameterFile("STNCC");
                string stnto = Config.GetParameterFile("STNTO");
                string stnfrom = Config.GetParameterFile("STNFROM");

                //Get Data from KKB_SP_TSS_STNCONF
                DataSet ds = Model_RPT_CONFIRMREPORT.getData(Config.ConnectionString, symbol, type);
                
                switch (type)
                {
                    case "SFIN":
                        reportConf = new RPT_STNSHFN();
                        reportPrice = new RPT_STNSHFN_PRICING_SUPPLEMENT();                      
                        break;

                    case "DUAL":
                        reportConf = new RPT_STNDUCN();
                        reportPrice = new RPT_STNDUCN_PRICING_SUPPLEMENT();
                        break;

                    case "DRAN":
                        reportConf = new RPT_STNDRAN();
                        reportPrice = new RPT_STNDRAN_PRICING_SUPPLEMENT();
                        break;

                    case "BODN":
                        reportConf = new RPT_STNBODN();
                        reportPrice = new RPT_STNBODN_PRICING_SUPPLEMENT();
                        break;
                }

                if (ds != null)
                {
                    string[] encrypt = new string[ds.Tables[0].Rows.Count];
                    string[] firstname = new string[ds.Tables[0].Rows.Count];
                    string[] email = new string[ds.Tables[0].Rows.Count];
                    string[] fcmail = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encrypt[i] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                        //edit cut taxid 5 digit for STN 26.10.2021
                        encrypt[i] = encrypt[i].Substring(encrypt[i].Length, -5);
                        firstname[i] = ds.Tables[0].Rows[i]["FIRST_NAME_EN"].ToString();
                        email[i] = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        fcmail[i] = ds.Tables[0].Rows[i]["FC_EMAIL"].ToString();

                        folder = symbol + "_" + firstname[i];

                        DataTable tb = ds.Tables[0].Clone();
                        DataRow row = ds.Tables[0].Rows[i];
                        tb.TableName = "TB_STNCONF";
                        tb.ImportRow(row);

                        //Create folders
                        FileManage.ManageDirectory(pathfile + folder);

                        //Save Files Confirmation
                        reportConf.SetDataSource(tb);
                        reportConf.ExportToDisk(ExportFormatType.PortableDocFormat, pathfile + folder + "/" + filename_conf  + firstname[i] + ".pdf");
                        
                        //Save Files Pricing Supplement
                        reportPrice.SetDataSource(tb);
                        reportPrice.ExportToDisk(ExportFormatType.PortableDocFormat, pathfile + folder + "/" + filename_price  + firstname[i] + ".pdf");


                        //Encrypt File with password
                        Encryption.EncryptByFolder(pathfile, folder, encrypt[i]);

                        //SendMail
                        string from = stnfrom;
                        string to = stnto;
                        string cc ="";
                        string subject = symbol + " นำส่งใบยืนยันคำสั่งซื้อ / ใบเสร็จรับเงิน ";
                        string message = "To : " + email[i] + "<br>"
                                       + "cc : " + stncc + fcmail[i] + "<br>"
                                       + " (Confidential) <br>"
                                       + " เรียน ผู้ถือหุ้นกู้ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) <br>"
                                       + " ตามที่ท่านได้จองซื้อหุ้นกู้ที่มีอนุพันธ์แฝง ออกโดย ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) " + symbol + "<br>"
                                       + " ทางธนาคารฯ  ขอนำส่งใบยืนยันคำสั่งซื้อ / ใบเสร็จรับเงิน, Pricing Supplement  โดยมีรายละเอียดตามเอกสารแนบค่ะ   <br> <br> <br> <br>"
                                       + " **เพื่อให้เป็นไปตามเกณฑ์ PDPA ไฟล์ดังกล่าวจะถูกเข้ารหัส โดยใช้รหัสผ่านนี้ในการเปิดไฟล์ จนกว่าจะมีการแจ้งเปลี่ยนแปลง**  <br>"
                                       + " Password : เลขประจำตัวผู้เสียภาษีของท่าน ";                       

                        List<Attachment> attach = new List<Attachment>();
                        attach.Add(new Attachment(pathfile + folder + ".zip"));
                        Encryption.SendMail(Config.GetParameterFile("SMTP"), int.Parse(Config.GetParameterFile("SMTPPort")), from, to, cc, subject, message, attach);   
                    
                    }                    

                    MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);
                    
                }
           
            }
            catch (Exception ex)
            {
                MessageBox.Text = "ไม่สามารถส่งไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                Log.Error(ex.Message);
            }
        }

        protected void Confirm(string symbol, string type)
        {
            try
            {

                TheUrl = "../View/ReportViewer2.aspx?report=CONFIRMREPORT";

                TheUrl += "&symbol=" + symbol + "&producttype=" + type + "&isreport=true&reportname=ConfirmReport_" + symbol;


                Page.DataBind();

                BindGridReport();



            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            //finally {


            //    string EditScript = " $('#divLoading').removeClass('show');$('#divLoading').addClass('hide'); ";
            //    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);
            //}
        }

        protected void Pricing(string symbol, string type)
        {
            try
            {
                TheUrl = "../View/ReportViewer2.aspx?report=PRICINGREPORT";
                TheUrl += "&symbol=" + symbol + "&producttype=" + type + "&isreport=true&reportname=PricingReport_" + symbol;
                Page.DataBind();

                BindGridReport(); 
            
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void OpenConfirmPrice(string symbol, string type)
        {
            try
            {
                TheUrl = "../View/ReportViewer2.aspx?report=PRICINGREPORT";
                TheUrl += "&symbol=" + symbol + "&producttype=" + type + "&isreport=true&reportname=PricingReport_" + symbol;

                TheUrl2 = "../View/ReportViewer2.aspx?report=CONFIRMREPORT";
                TheUrl2 += "&symbol=" + symbol + "&producttype=" + type + "&isreport=true&reportname=ConfirmReport_" + symbol;
                
                
                
                Page.DataBind();

                BindGridReport();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Settlement(string symbol, string type)
        {
            try
            {
                string pathfile = Server.MapPath("~/Temporary/STNReport/");
                string folder = "";
                string filename_settle = "";
                string stncc = Config.GetParameterFile("STNCC");
                string stnto = Config.GetParameterFile("STNTO");
                string stnfrom = Config.GetParameterFile("STNFROM");
           
                ReportClass reportSettle = null;

                //Get Data from KKB_SP_TSS_STNCONF
                DataSet ds = Model_RPT_SETTLEMENTREPORT.getData(Config.ConnectionString, symbol, type);

                switch (type)
                {
                    case "SFIN":
                        reportSettle = new RPT_STNSHFN_SETTLEMENT();                       
                        break;

                    case "DUAL":
                        reportSettle = new RPT_STNDUCN_SETTLEMENT();                       
                        break;

                    case "DRAN":
                        reportSettle = new RPT_STNDRAN_SETTLEMENT();                        
                        break;

                    case "BODN":
                        reportSettle = new RPT_STNBODN_SETTLEMENT();                        
                        break;
                }

                if (ds != null)
                {
                    string[] encrypt = new string[ds.Tables[0].Rows.Count];
                    string[] firstname = new string[ds.Tables[0].Rows.Count];
                    string[] email = new string[ds.Tables[0].Rows.Count];
                    string[] fcmail = new string[ds.Tables[0].Rows.Count];
                    string[] matdate = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encrypt[i] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                        //edit cut taxid 5 digit for STN 26.10.2021
                        encrypt[i] = encrypt[i].Substring(encrypt[i].Length, -5);

                        firstname[i] = ds.Tables[0].Rows[i]["FIRST_NAME_EN"].ToString();
                        email[i] = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        fcmail[i] = ds.Tables[0].Rows[i]["FC_EMAIL"].ToString();
                        matdate[i] = ds.Tables[0].Rows[i]["MATURITY_DATE"].ToString();                        

                        folder = symbol + "_" + firstname[i];
                        filename_settle = firstname[i];

                        DataTable tb = ds.Tables[0].Clone();
                        DataRow row = ds.Tables[0].Rows[i];
                        tb.TableName = "TB_STNCONF";
                        tb.ImportRow(row);

                        //Create folders
                        FileManage.ManageDirectory(pathfile + folder);

                        //Save Files Settlement
                        reportSettle.SetDataSource(tb);
                        reportSettle.ExportToDisk(ExportFormatType.PortableDocFormat, pathfile + folder + "/" + filename_settle + ".pdf");
                        
                        //Encrypt File with password
                        Encryption.EncryptByFolder(pathfile, folder, encrypt[i]);

                        //SendMail
                        string from = stnfrom;
                        string to = stnto;
                        string cc = "";
                        string subject = symbol + " -  นำส่งใบยืนยันการไถ่ถอนหุ้นกู้ที่มีอนุพันธ์แฝง";
                        string message = "To : " + email[i] + "<br>"
                                       + "cc : " + stncc + fcmail[i] + "<br>"
                                       + " (Confidential) <br>"
                                       + " เรียน ผู้ถือหุ้นกู้ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) <br>"
                                       + " ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) ขอนำส่งใบยืนยันการไถ่ถอนหุ้นกู้ที่มีอนุพันธ์แฝง " + symbol + "<br>"
                                       + " ซึ่งจะครบกำหนดในวันที่ " + matdate[i] +" รายละเอียดตามไฟล์แนบค่ะ    <br> <br> <br> <br>"
                                       + " **เพื่อให้เป็นไปตามเกณฑ์ PDPA ไฟล์ดังกล่าวจะถูกเข้ารหัส โดยใช้รหัสผ่านนี้ในการเปิดไฟล์ จนกว่าจะมีการแจ้งเปลี่ยนแปลง**  <br>"
                                       + " Password : เลขประจำตัวผู้เสียภาษีของท่าน ";

                        List<Attachment> attach = new List<Attachment>();
                        attach.Add(new Attachment(pathfile + folder + ".zip"));
                        Encryption.SendMail(Config.GetParameterFile("SMTP"), int.Parse(Config.GetParameterFile("SMTPPort")), from, to, cc, subject, message, attach);   
                    
                    }

                   
                    MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Text = "ไม่สามารถส่งไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                Log.Error(ex.Message);
            }
        }

        protected void OpenSettlement(string symbol, string type)
        {
            try
            {
                Log.Info("Get Report Settlement");

                TheUrl = "../View/ReportViewer2.aspx?report=SETTLEMENTREPORT";
                TheUrl += "&symbol=" + symbol + "&producttype=" + type + "&isreport=true&reportname=SettlementReport_" + symbol;
                Page.DataBind();

                BindGridReport();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Knockout(string symbol, string type)
        {
            try
            {
                string pathfile = Server.MapPath("~/Temporary/STNReport/");
                string folder = "";
                string filename_ko = "";
                string stncc = Config.GetParameterFile("STNCC");
                string stnto = Config.GetParameterFile("STNTO");
                string stnfrom = Config.GetParameterFile("STNFROM");
               
                ReportClass reportKO = null;               

                //Get Data from KKB_SP_TSS_STNCONF
                DataSet ds = Model_RPT_KNOCKOUTREPORT.getData(Config.ConnectionString, symbol, type);               

                if (ds != null)
                {
                    string[] encrypt = new string[ds.Tables[0].Rows.Count];
                    string[] firstname = new string[ds.Tables[0].Rows.Count];
                    string[] email = new string[ds.Tables[0].Rows.Count];
                    string[] fcmail = new string[ds.Tables[0].Rows.Count];
                    string[] kodate = new string[ds.Tables[0].Rows.Count];
                    string[] curr = new string[ds.Tables[0].Rows.Count];
                    string[] matdate = new string[ds.Tables[0].Rows.Count];                         

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encrypt[i] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                        //edit cut taxid 5 digit for STN 26.10.2021
                        encrypt[i] = encrypt[i].Substring(encrypt[i].Length, -5);

                        firstname[i] = ds.Tables[0].Rows[i]["FIRST_NAME_EN"].ToString();
                        email[i] = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        fcmail[i] = ds.Tables[0].Rows[i]["FC_EMAIL"].ToString();
                        kodate[i] = ds.Tables[0].Rows[i]["KO_DATE"].ToString();
                        curr[i] = ds.Tables[0].Rows[i]["UNDERLYING_CURR"].ToString();
                        matdate[i] = ds.Tables[0].Rows[i]["MATURITY_DATE"].ToString();  

                        folder = symbol + "_" + firstname[i];
                        filename_ko = firstname[i];

                        //set report after check currency 
                        switch (type)
                        {
                            case "SFIN":
                                if (!curr[i].Trim().Equals("THB")){
                                    reportKO = new RPT_STNSHFN_KO();
                                }
                                else {
                                    reportKO = new RPT_STNSHFN_SETTLEMENT();
                                }                                
                                break;

                            case "DUAL":
                                reportKO = new RPT_STNDUCN_SETTLEMENT();
                                break;

                            case "DRAN":
                                reportKO = new RPT_STNDRAN_SETTLEMENT();
                                break;

                            case "BODN":
                                reportKO = new RPT_STNBODN_SETTLEMENT();
                                break;
                        }
                        //set report after check currency 


                        DataTable tb = ds.Tables[0].Clone();
                        DataRow row = ds.Tables[0].Rows[i];
                        tb.TableName = "TB_STNCONF";
                        tb.ImportRow(row);

                        //Create folders
                        FileManage.ManageDirectory(pathfile + folder);

                        //Save Files Knockout
                        reportKO.SetDataSource(tb);
                        reportKO.ExportToDisk(ExportFormatType.PortableDocFormat, pathfile+ folder + "/" + filename_ko + ".pdf");

                        //Encrypt File with password
                        Encryption.EncryptByFolder(pathfile, folder, encrypt[i]);

                        //SendMail
                        string from = stnfrom;
                        string to = stnto;
                        string cc = "", subject = "", message= "";
                        if (!curr[i].Trim().Equals("THB"))
                        {
                            subject = symbol + " - ใบแจ้งการเกิดเหตุการณ์ของหุ้นกู้ที่มีอนุพันธ์แฝง";
                            message = "To : " + email[i] + "<br>"
                                       + "cc : " + stncc + fcmail[i] + "<br>"
                                       + " (Confidential) <br>"
                                       + " เรียน ผู้ถือหุ้นกู้ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) <br>"
                                       + " ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน)ขอนำส่งใบแจ้งการเกิดเหตุการณ์ของหุ้นกู้ที่มีอนุพันธ์แฝง " + symbol + "<br>"
                                       + " ณ วันที่ " + kodate[i] + " รายละเอียดตามไฟล์แนบค่ะ    <br> <br> <br> <br>"
                                       + " **เพื่อให้เป็นไปตามเกณฑ์ PDPA ไฟล์ดังกล่าวจะถูกเข้ารหัส โดยใช้รหัสผ่านนี้ในการเปิดไฟล์ จนกว่าจะมีการแจ้งเปลี่ยนแปลง**  <br>"
                                       + " Password : เลขประจำตัวผู้เสียภาษีของท่าน ";

                        }
                        else 
                        {
                            subject = symbol + " - (Knock-Out)";
                            message = "To : " + email[i] + "<br>"
                                       + "cc : " + stncc + fcmail[i] + "<br>"
                                       + " (Confidential) <br>"
                                       + " เรียน ผู้ถือหุ้นกู้ธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) <br>"
                                       + " ตามที่ท่านได้ซื้อหุ้นกู้ที่มีอนุพันธ์แฝง " + symbol + " ที่ออกโดยธนาคารเกียรตินาคินภัทร จำกัด (มหาชน) ณ วันที่ " + kodate[i]
                                       + " เกิดเหตุการณ์ Knock-Out ธนาคารฯ ขอแจ้งรายละเอียดผลตอบแทนและยอดการไถ่ถอนหุ้นกู้ฯ  ณ วันที่ " + matdate[i] + "<br>"
                                       + " รายละเอียดตามไฟล์แนบค่ะ    <br> <br> <br> <br>"
                                       + " **เพื่อให้เป็นไปตามเกณฑ์ PDPA ไฟล์ดังกล่าวจะถูกเข้ารหัส โดยใช้รหัสผ่านนี้ในการเปิดไฟล์ จนกว่าจะมีการแจ้งเปลี่ยนแปลง**  <br>"
                                       + " Password : เลขประจำตัวผู้เสียภาษีของท่าน ";                              
                        }
                        

                        List<Attachment> attach = new List<Attachment>();
                        attach.Add(new Attachment(pathfile + folder + ".zip"));
                        Encryption.SendMail(Config.GetParameterFile("SMTP"), int.Parse(Config.GetParameterFile("SMTPPort")), from, to, cc, subject, message, attach);   
                                              
                    }

                  
                    MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Text = "ไม่สามารถส่งไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                Log.Error(ex.Message);
            }
        }

        protected void OpenKnockout(string symbol, string type)
        {
            try
            {
                TheUrl = "../View/ReportViewer2.aspx?report=KNOCKOUTREPORT";
                TheUrl += "&symbol=" + symbol + "&producttype=" + type + "&isreport=true&reportname=KnockOutReport_" + symbol;
                Page.DataBind();

                BindGridReport();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        /*
        protected void Unwind(string symbol ,string type , string stnid)
        {
            try
            {
                Log.Info("Get Report Unwind");

                TheUrl = "../View/ReportViewer2.aspx?report=UNWINDREPORT";
                TheUrl += "&symbol=" + symbol + "&producttype=" + type + "&stnid=" + stnid + "&isreport=true&reportname=SettlementReport_" + symbol;
                Page.DataBind();

                BindGridReport();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        */

        protected void Delete(string key)
        {
            try
            {
                STNMASTER m = new STNMASTER();
                m.MASSTNID = int.Parse(key);
                if (m.Delete())
                {
                    MessageBox.Text = "ลบรายการเรียบร้อย";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบข้อมูล";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                BindGridReport();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private List<STNMASTER> ConvertDataTableToListMaster(DataTable dt)
        {
            Log.Info("MasterSTN ConvertDataTableToListMaster");

            List<STNMASTER> result = new List<STNMASTER>();

            try {
               
                STNMASTER STNMaster = new STNMASTER();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    STNMaster = new STNMASTER();

                    STNMaster.MASSTNID = string.IsNullOrEmpty(dt.Rows[i]["MASSTNID"].ToString()) ? 0 : int.Parse(dt.Rows[i]["MASSTNID"].ToString());
                    STNMaster.KKSYMBOL = string.IsNullOrEmpty(dt.Rows[i]["KKSYMBOL"].ToString()) ? "" : dt.Rows[i]["KKSYMBOL"].ToString();
                    STNMaster.STN_TYPE = string.IsNullOrEmpty(dt.Rows[i]["STN_TYPE"].ToString()) ? "" : dt.Rows[i]["STN_TYPE"].ToString();
                    STNMaster.SUM_RECORD = string.IsNullOrEmpty(dt.Rows[i]["SUM_RECORD"].ToString()) ? 0 : int.Parse(dt.Rows[i]["SUM_RECORD"].ToString());
                    STNMaster.SUM_AMOUNT = string.IsNullOrEmpty(dt.Rows[i]["SUM_AMOUNT"].ToString()) ? 0 : Decimal.Parse(dt.Rows[i]["SUM_AMOUNT"].ToString());
                    STNMaster.CREATE_DATE = string.IsNullOrEmpty(dt.Rows[i]["CREATE_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["CREATE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    STNMaster.CREATE_BY = string.IsNullOrEmpty(dt.Rows[i]["CREATE_BY"].ToString()) ? "" : dt.Rows[i]["CREATE_BY"].ToString();
                    STNMaster.UPDATE_DATE = string.IsNullOrEmpty(dt.Rows[i]["UPDATE_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["UPDATE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    STNMaster.UPDATE_BY = string.IsNullOrEmpty(dt.Rows[i]["UPDATE_BY"].ToString()) ? "" : dt.Rows[i]["UPDATE_BY"].ToString();
                    STNMaster.PRODUCT = string.IsNullOrEmpty(dt.Rows[i]["PRODUCT"].ToString()) ? "" : dt.Rows[i]["PRODUCT"].ToString();
                    STNMaster.SETTLE_DAYS = string.IsNullOrEmpty(dt.Rows[i]["SETTLE_DAYS"].ToString()) ? 0 : int.Parse(dt.Rows[i]["SETTLE_DAYS"].ToString());
                    STNMaster.SETTLE_PRICE1 = string.IsNullOrEmpty(dt.Rows[i]["SETTLE_PRICE1"].ToString()) ? 0 : Decimal.Parse(dt.Rows[i]["SETTLE_PRICE1"].ToString());
                    STNMaster.SETTLE_FXRATE = string.IsNullOrEmpty(dt.Rows[i]["SETTLE_FXRATE"].ToString()) ? 0 : Decimal.Parse(dt.Rows[i]["SETTLE_FXRATE"].ToString());
                    STNMaster.KO_PRICE1 = string.IsNullOrEmpty(dt.Rows[i]["KO_PRICE1"].ToString()) ? 0 : Decimal.Parse(dt.Rows[i]["KO_PRICE1"].ToString());
                    STNMaster.KO_STOCK_TYPE = string.IsNullOrEmpty(dt.Rows[i]["KO_STOCK_TYPE"].ToString()) ? "" : dt.Rows[i]["KO_STOCK_TYPE"].ToString();
                    STNMaster.KO_DATE = string.IsNullOrEmpty(dt.Rows[i]["KO_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["KO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    STNMaster.STN_SEQ = string.IsNullOrEmpty(dt.Rows[i]["STN_SEQ"].ToString()) ? "" : dt.Rows[i]["STN_SEQ"].ToString();
                    STNMaster.STN_SEQDATE = string.IsNullOrEmpty(dt.Rows[i]["STN_SEQDATE"].ToString()) ? "" : dt.Rows[i]["STN_SEQDATE"].ToString();

                    result.Add(STNMaster);
                    Log.Info(result.Count());

                }
                

            }catch(Exception ex){
                Log.Debug(ex.Message);
            }


            return result;
        }

      
        /*
        public void Encryption(string symbol, string[] encrypt)
        {
            try
            {
                Log.Info(" STN Document Encryption ");

                //string Path = Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/");
                //string Path = Server.MapPath("~/Temporary/STNReport/" + symbol);

                //Encrypt file with password
                for (int i = 0; i < encrypt.Length; i++)
                {
                    string ExportPath = Server.MapPath("~/Temporary/STNReport/" + symbol+"_"+i);

                    if (File.Exists(ExportPath + ".zip"))
                        File.Delete(ExportPath + ".zip");

                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = encrypt[i];
                        zip.AddDirectory(ExportPath);

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(ExportPath + ".zip");
                    }

                    Log.Info("encrypt pass : " + encrypt[i]);
                    Log.Info("encrypt path : " + encrypt);

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
                string message = "Please kindly recheck and feedback." + "<br>";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Config.GetParameterFile("136From"));
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
       
      */

        protected void ViewPassword(string symbol, string type)
        {
            try
            {
                Log.Info("MasterSTN ViewPassword");
                //Get Data from KKB_SP_TSS_STNCONF
                DataSet ds = Model_RPT_CONFIRMREPORT.getData(Config.ConnectionString, symbol, type);            
    
                if (ds != null)
                {
                    string message = "";
                    string[] encrypt = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encrypt[i] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                        //edit cut taxid 5 digit for STN 26.10.2021
                        encrypt[i] = encrypt[i].Substring(encrypt[i].Length, -5);

                        message += encrypt[i] + "<br>";
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


        #endregion


        //EditPerson
        [WebMethod(EnableSession = true)]
        public static object EditPerson(int STNID)
        {
            Log.Info("-----MasterSTN EditPerson-----");
            return KKBSTN.getSTN(STNID);
        }

        //DeletePerson
        [WebMethod(EnableSession = true)]
        public static object DeletePerson(int STNID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return KKBSTN.DeletePerson(STNID);
        }


        //EditUnwind
        [WebMethod(EnableSession = true)]
        public static object EditUnwind(int STNID)
        {
            Log.Info("-----MasterSTN EditPerson-----");
            return KKBSTN.getSTN(STNID);
        }
        

        //SubmitUnwind
        [WebMethod(EnableSession = true)]
        public static object UnwindPerson(int STNID, string PRICERETURN, string DATERETURN, string PAYDATE)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return KKBSTN.UnwindPerson(STNID, PRICERETURN, DATERETURN, PAYDATE);

            //return new { Result = "OK", Message = "" };
        }


        //OpenUnwindReport
        [WebMethod(EnableSession = true)]
        public static object OpenUnwindReport(int STNID)
        { 

            try
            {

                if (HttpContext.Current.Session["UserName"] == null)
                    return new { Result = "ERROR", Message = "Login Session Expired" };

                string TheUrl = "";
                DataTable dt = STNMASTER.getMasterType(STNID);

                string type = dt.Rows[0]["STN_TYPE"].ToString();
                string symbol = dt.Rows[0]["KKSYMBOL"].ToString();


                TheUrl = "../View/ReportViewer2.aspx?report=UNWINDREPORT";
                //TheUrl = "http://" + HttpContext.Current.Request.Url.Authority  + HttpContext.Current.Request.ApplicationPath + "/View/ReportViewer2.aspx?report=UNWINDREPORT";

                TheUrl += "&symbol=" + symbol + "&stnid=" + STNID + "&producttype=" + type + "&isreport=true&reportname=UnwindReport_" + symbol  ;

                Log.Info("Url UnwindReport : " + TheUrl);

                //return null;
                return new { Result = "OK", Message = TheUrl }; 

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return new { Result = "ERROR", Message = "Could not update data." };
            }
        }


        /*
        //MailUnwindReport
        [WebMethod(EnableSession = true)]
        public static object MailUnwindReport(int STNID)
        {

            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return KKBSTN.UnwindPerson(STNID);

        }
        */

        //UpdateKnockout
        [WebMethod(EnableSession = true)]
        public static object UpdateKnockout(string kksymbol, string stocktype, string priceko, string dateko, string fxrateko)
        {
            string[] arrprice = priceko.Split(',');
           // string[] arrdate = dateko.Split(',');

            STNMASTER master = new STNMASTER();
            master.KKSYMBOL = kksymbol;
            master.KO_STOCK_TYPE = stocktype;
            master.KO_DATE = dateko;
            master.KO_FXRATE = fxrateko == "" ? 0 : decimal.Parse(fxrateko); 

            int i = 0;
            foreach (string price in arrprice)
            {
                if (!string.IsNullOrEmpty(price)) { 
                    if (i < arrprice.Count())
                    {

                        switch (i)
                        {

                            case 0: master.KO_PRICE1 = decimal.Parse(price); break;
                            case 1: master.KO_PRICE2 = decimal.Parse(price); break;
                            case 2: master.KO_PRICE3 = decimal.Parse(price); break;
                            case 3: master.KO_PRICE4 = decimal.Parse(price); break;
                            case 4: master.KO_PRICE5 = decimal.Parse(price); break;
                            case 5: master.KO_PRICE6 = decimal.Parse(price); break;

                        }

                    }

                    i++;
                }
            }

            return STNMASTER.UpdateKnockout(master);

        }


        //UpdatePriceClose
        [WebMethod(EnableSession = true)]
        public static object UpdatePriceClose(string kksymbol,string pricelist,string days,string fxsettle)
        {

            Log.Info("UpldatePriceClose webmetode");

                string[] arrprice = pricelist.Split(',');


                STNMASTER master = new STNMASTER();
                master.KKSYMBOL = kksymbol;
                master.SETTLE_DAYS = days == "" ? 0 : int.Parse(days);
                master.SETTLE_FXRATE = fxsettle == "" ? 0 : decimal.Parse(fxsettle);
           

                int i = 0;
                foreach (string price in arrprice)
                {

                    if (!string.IsNullOrEmpty(price))
                    {
                        if (i < arrprice.Count())
                        {

                            switch (i)
                            {

                                case 0: master.SETTLE_PRICE1 = decimal.Parse(price); break;
                                case 1: master.SETTLE_PRICE2 = decimal.Parse(price); break;
                                case 2: master.SETTLE_PRICE3 = decimal.Parse(price); break;
                                case 3: master.SETTLE_PRICE4 = decimal.Parse(price); break;
                                case 4: master.SETTLE_PRICE5 = decimal.Parse(price); break;
                                case 5: master.SETTLE_PRICE6 = decimal.Parse(price); break;

                            }

                        }

                        i++;
                    }
                }

                return STNMASTER.UpdatePriceClose(master);
        }


        //UpdatePerson
        [WebMethod(EnableSession = true)]
        public static object UpdatePerson(int stnid, string titlenameth, string firstnameth, string lastnameth, string titlenameen, string firstnameen, string lastnameen, string taxid, string custbankcode, string custbankacct, string tel, string email, string fcname, string fcemail,string custtype, string paytype)
        {

            KKBSTN p = new KKBSTN();
            p.STNID = stnid;
            p.TITLE_NAME_TH = titlenameth;
            p.FIRST_NAME_TH = firstnameth;
            p.LAST_NAME_TH = lastnameth;
            p.TITLE_NAME_EN = titlenameen;
            p.FIRST_NAME_EN = firstnameen;
            p.LAST_NAME_EN = lastnameen;
            p.TAXID = taxid;
            p.CUST_BANK_CODE = custbankcode;
            p.CUST_BANK_ACCT = custbankacct;
            p.TEL = tel;
            p.EMAIL = email;
            p.FC_NAME = fcname;
            p.FC_EMAIL = fcemail;
            p.CUST_TYPE = custtype;
            p.PAY_TYPE = paytype;
                

            return KKBSTN.UpdatePerson(p);

        }

  
    }
}
