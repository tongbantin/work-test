using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net.Mime;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Master;
using TreasuryModel.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using log4net;
using System.Text;
using TreasuryBusiness.ThirdParty;
using System.Collections;
using System.Data.OleDb;
using System.Web.Services;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR051GENERATE : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR051GENERATE));
        private const long BUFFER_SIZE = 4096;
        String FolderName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                //    Response.Redirect("~/SessionExpired.aspx");

                GridViewAllotment.PageIndexChanging += new GridViewPageEventHandler(GridViewAllotment_PageIndexChanging);
                GridViewAllotment.RowEditing += new GridViewEditEventHandler(GridViewAllotment_RowEditing);
                GridViewAllotment.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewAllotment_RowCancelingEdit);
                GridViewAllotment.RowCommand += new GridViewCommandEventHandler(GridViewAllotment_RowCommand);
                GridViewAllotment.RowDeleting += new GridViewDeleteEventHandler(GridViewAllotment_RowDeleting);

                //if (!IsPostBack)
                //{
                //    BindAttn();
                //}

                PrincipalAmtLotLable.Visible = false;
                PrincipalAmtLotBox.Visible = false;
                InterestAmtLotLable.Visible = false;
                InterestAmtLotBox.Visible = false;
                SettlementAmtLotLable.Visible = false;
                SettlementAmtLotBox.Visible = false;
                VatLotLable.Visible = false;
                VatLotBox.Visible = false;
                DealPriceLotLable.Visible = false;
                DealPriceLotBox.Visible = false;   
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewAllotment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void Delete(string key)
        {
            try
            {
                Model_MS_PTITRANS m = new Model_MS_PTITRANS();
                m.TransLogID = int.Parse(key);
                if (m.Delete(Config.ConnectionString))
                {
                    MessageBox.Text = "ลบรายการเรียบร้อย";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบข้อมูล";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewAllotment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    PrincipalAmtLotLable.Visible = true;
                    PrincipalAmtLotBox.Visible = true;
                    InterestAmtLotLable.Visible = true;
                    InterestAmtLotBox.Visible = true;
                    SettlementAmtLotLable.Visible = true;
                    SettlementAmtLotBox.Visible = true;
                    //add input text vat and dealprice
                    VatLotLable.Visible = true;
                    VatLotBox.Visible = true;
                    DealPriceLotLable.Visible = true;
                    DealPriceLotBox.Visible = true;                    

                    BindAllotment(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {'Submit': function() {";
                    EditScript += "var faceamt = $('#" + FaceAmtLotBox.ClientID + "').val() - $('#" + OldFaceAmtBox.ClientID + "').val(); ";
                    EditScript += "if (($('#" + RemainLotBox.ClientID + "').val() - faceamt) < 0){";
                    EditScript += "    alert('This is Over FaceAmount!!'); ";
                    EditScript += "    return false;} else {";
                    
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + DealNo.ClientID + "').val($('.dealnolot').val());";
                    EditScript += "if($('.customer').val().trim()!='') {";
                    EditScript += "$('#" + Customer.ClientID + "').val($('.customer :selected').val());";
                    EditScript += "} else {";
                    EditScript += "$('#" + Customer.ClientID + "').val('');";
                    EditScript += "}";
                    EditScript += "$('#" + FaceAmt.ClientID + "').val($('.faceamtlot').val());";
                    EditScript += "$('#" + SettlementAmt.ClientID + "').val($('.settamtlot').val());";
                    EditScript += "$('#" + PrinAmt.ClientID + "').val($('.prinamtlot').val());";
                    EditScript += "$('#" + CostAmt.ClientID + "').val($('.costamtlot').val());";
                    EditScript += "$('#" + IntAmt.ClientID + "').val($('.intamtlot').val());";
                    EditScript += "$('#" + VatAmt.ClientID + "').val($('.vatlot').val());";
                    EditScript += "$('#" + DealPriceAmt.ClientID + "').val($('.dealpricelot').val());event.preventDefault();";                
                    EditScript += "$('input#" + DialogSubmit.ClientID + "').click();}";
                    EditScript += "},'Close': function() {$(this).dialog('close');}}}); ";

                    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);
                }
                else if (e.CommandName.Equals("Delete"))
                {
                    Delete(e.CommandArgument.ToString());
                }
                else
                {
                    PrincipalAmtLotLable.Visible = false;
                    PrincipalAmtLotBox.Visible = false;
                    InterestAmtLotLable.Visible = false;
                    InterestAmtLotBox.Visible = false;
                    SettlementAmtLotLable.Visible = false;
                    SettlementAmtLotBox.Visible = false;
                    VatLotLable.Visible = false;
                    VatLotBox.Visible = false;
                    DealPriceLotLable.Visible = false;
                    DealPriceLotBox.Visible = false;                    

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindAllotment(string id)
        {
            try
            {
                string key = GridViewAllotment.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = Model_MS_PTITRANS.getTransLogByID(Config.ConnectionString, key);
                TransLogID.Value = key;
                ActionBox.Value = "Edit";
                DealNoLotBox.Value = dt.Rows[0]["RelateRef"].ToString().Trim();
                CustomerLotBox.Value = dt.Rows[0]["AttnID"].ToString().Trim();
                //CustomerNameBox.Value = dt.Rows[0]["Attn"].ToString().Trim();

                //BindCustomer
                xcustomer.DataSource = TreasuryModel.Data.Customer.getCustomerByID(Config.ConnectionString, dt.Rows[0]["AttnID"].ToString().Trim());
                xcustomer.DataTextField = "NameEn";
                xcustomer.DataValueField = "CustomerID";
                xcustomer.DataBind();
                xcustomer.SelectedIndex = 0;

                AssetCompanyBox.Value = dt.Rows[0]["AssetCompany"].ToString().Trim();
                FaceAmtLotBox.Value = Convert.ToString(Convert.ToInt64(dt.Rows[0]["FaceAmt"].ToString()));
                CostAmtLotBox.Value = dt.Rows[0]["CostValue"].ToString();
                SettlementAmtLotBox.Value = dt.Rows[0]["SettAmt"].ToString();
                PrincipalAmtLotBox.Value = dt.Rows[0]["PrinAmt"].ToString();
                InterestAmtLotBox.Value = dt.Rows[0]["IntAmt"].ToString();
                RemainLotBox.Value = dt.Rows[0]["Remain"].ToString();
                OldFaceAmtBox.Value = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["FaceAmt"].ToString()));
                VatLotBox.Value = dt.Rows[0]["VAT"].ToString();
                DealPriceLotBox.Value = dt.Rows[0]["DEALPRICE"].ToString();
                MsgType.Value = dt.Rows[0]["MSGTYPE"].ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewAllotment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewAllotment.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewAllotment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewAllotment.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewAllotment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewAllotment.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindGrid()
        {
            try
            {
                GridViewAllotment.DataSource = Model_MS_PTITRANS.getTransLogByDealNo(Config.ConnectionString, DealNoSearchBox.Text);
                GridViewAllotment.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void DialogSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ValidateDialog(m))
                //{
                    Model_MS_PTITRANS m = new Model_MS_PTITRANS();
                    m.SenderRef = DealNo.Value;
                    m.RelateRef = DealNo.Value;
                    m.Customer = Customer.Value;
                    m.FaceAmt = Convert.ToString(Convert.ToDecimal(FaceAmt.Value)/1000000);
                    m.SettAmt = SettlementAmt.Value;
                    m.PrinAmt = PrinAmt.Value;
                    m.CostAmt = CostAmt.Value;
                    m.IntAmt = IntAmt.Value;
                    m.VatAmt = VatAmt.Value;
                    m.DealPriceAmt = DealPriceAmt.Value;

                    if (ActionBox.Value.Equals("Add"))
                    {
                        if (PayType.Value.Equals("BN") || PayType.Value.ToUpper().Equals("BAHTNET") )
                        {
                            m.MsgType = "BAHTNET";
                        }
                        else m.MsgType = "CHEQUE";
                    }
                    else {
                        if (MsgType.Value.Equals("541") || MsgType.Value.Equals("543"))
                        {
                            m.MsgType = "BAHTNET";
                        }
                        else m.MsgType = "CHEQUE";
                    }
                   
                    if (ActionBox.Value.Equals("Edit"))
                    {
                        m.TransLogID = int.Parse(TransLogID.Value);

                        if (m.Update(Config.ConnectionString))
                        {
                            MessageBox.Text = "บันทึกรายการเรียบร้อย";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        else
                        {
                            MessageBox.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบข้อมูล";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                    }
                    else
                    {
                        if (m.Create(Config.ConnectionString))
                        {
                            MessageBox.Text = "บันทึกรายการเรียบร้อย";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        else
                        {
                            MessageBox.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณากรอกข้อมูลให้ถูกต้อง";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                    }
                //}
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }
        
        //private Boolean ValidateDialog(Model_MS_PTITRANS m)
        //{
        //    try
        //    {
        //        if (m.SenderRef.Trim().Equals(""))
        //        {
        //            MessageBox.Text = "DealNo is require!!";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
        //            return false;
        //        }
        //        if (m.FaceAmt.Trim().Equals(""))
        //        {
        //            MessageBox.Text = "Face Amount is require!!";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return false;
        //    }
        //}

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
                if (GridViewAllotment.Rows.Count > 0)
                {
                    btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void UploadSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList error = new ArrayList();
                String filename = DateTime.Now.ToString("yyyyMMdd") + UploadFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                String pathFolder = Server.MapPath("~/Temporary") + "/";
                String showMessage = "";

                // Check Excel file type
                List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();
                if ((UploadFile.PostedFile.ContentType.Equals("application/octet-stream")) || (UploadFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (UploadFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    //Write file to temporary folder
                    UploadFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=No;\";";
                        //String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 12.0;IMEX=1;HDR=No;\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            Log.Debug("Start Read Data Allot PTI From Excel");
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Sheet1$] ", _oleConn);
                            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                            oleAdapter.SelectCommand = _oleCmdSelect;
                            DataTable dt = new DataTable();

                            oleAdapter.FillSchema(dt, SchemaType.Source);
                            oleAdapter.Fill(dt);

                            Log.Debug("Start Add Data To Object");
                            int sumfaceamt = 0;
                            for (int i = 1; i < dt.Rows.Count; i++)
                            {
                                Model_MS_PTITRANS pti = new Model_MS_PTITRANS();
                                try
                                {
                                    pti.ExcelDealno = dt.Rows[i][0].ToString();
                                    pti.ExcelAttn = dt.Rows[i][1].ToString();
                                    pti.ExcelFaceAmt = dt.Rows[i][2].ToString().Replace(",","");
                                    pti.ExcelCostAmt = dt.Rows[i][3].ToString();

                                    if (pti.ExcelFaceAmt.Equals("")) pti.ExcelFaceAmt = "0";
                                    sumfaceamt = sumfaceamt + int.Parse(pti.ExcelFaceAmt);
                                        
                                    //Check requeire data
                                    if(pti.ExcelDealno == null || pti.ExcelDealno.Equals("")){
                                        showMessage += "กรุณาระบุ Dealno. ให้ครบถ้วน  <br>";
                                        //break;
                                    } 
                                    else if(pti.ExcelAttn == null || pti.ExcelAttn.Equals("")){
                                        showMessage += "กรุณาระบุ Attn. ให้ครบถ้วน  <br>";
                                        //break;
                                    } 
                                    else if(pti.ExcelFaceAmt == null || pti.ExcelFaceAmt.Equals("")){
                                        showMessage += "กรุณาระบุ Face Amount ให้ครบถ้วน  <br>";
                                        //break;
                                    } 
                                   
                                }
                                catch (Exception ex)
                                {
                                    Log.Error(ex.Message);
                                }
                                    l.Add(pti);
                             }
                                                         
                            if(showMessage.Equals("")){

                                Model_MS_PTITRANS distdeal = (Model_MS_PTITRANS)l[0];
                                string dealfind = distdeal.ExcelDealno.ToString();
                                
                                //Get Deal Info.
                                DataTable dtdeal = Model_MS_BOND.getBondByDealNo(Configuration.Config.ConnectionString, dealfind);
                                if (dtdeal != null)
                                {
                                    if(sumfaceamt.ToString().Equals(dtdeal.Rows[0]["TOTAL"].ToString())){
                                                                               
                                        foreach (Model_MS_PTITRANS m in l)
                                        {
                                            //Get Customer no.
                                            DataTable dtcust = TreasuryModel.Data.Customer.getCustomerForName(Configuration.Config.ConnectionString, m.ExcelAttn);

                                            //New object to save data
                                            Model_MS_PTITRANS record = new Model_MS_PTITRANS();
                                            record.RelateRef = dtdeal.Rows[0]["DEALNO"].ToString();
                                            if (dtdeal.Rows[0]["PAYTYPE"].ToString().Equals("BN"))
                                            {
                                                record.MsgType = "BAHTNET";
                                            }
                                            else record.MsgType = "CHEQUE";
                                            
                                            record.Customer = dtcust.Rows[0]["CustomerID"].ToString();
                                            record.FaceAmt = Convert.ToString(Convert.ToDecimal(m.ExcelFaceAmt.Replace(",","")) / 1000000);
                                            record.CostAmt = m.ExcelCostAmt.Replace(",", "");

                                            record.Create(Config.ConnectionString);
                                        }

                                        MessageBox.Text = "ระบบบันทึกข้อมูลเรียบร้อยแล้ว";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                    
                                    }else{
                                        MessageBox.Text = "ผลรวม Face Amount ไม่เท่าจำนวนยอดคงเหลือ";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);                              
                                    }                                                              
                                
                                }else{
                                    MessageBox.Text = "Dealno ไม่ถูกต้อง";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);                            
                                }                            
                            
                            }else{
                                MessageBox.Text = showMessage;
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
                            //Close Connection
                            _oleConn.Close();

                            //Clear File Excel
                            if (File.Exists(pathFolder + filename))
                                File.Delete(pathFolder + filename);
                            
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
                MessageBox.Text = "ไม่สามารถทำการอัพโหลดไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        /*
        [WebMethod(EnableSession = true)]
        public static object UploadExcel(HttpContext context)
        {
            
            if (context.Request.Files.Count > 0) {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fname = context.Server.MapPath("~/uploads/" + file.FileName);
                    //file.SaveAs(fname);
                }    
            
            }

            try
            {
                ArrayList error = new ArrayList();
                String filename = DateTime.Now.ToString("yyyyMMdd") + UploadFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                String pathFolder = Server.MapPath("~/Temporary") + "/";
                String showMessage = "";
                
                // Check Excel file type
                List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();
                if ((UploadFile.PostedFile.ContentType.Equals("application/octet-stream")) || (UploadFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (UploadFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    //Write file to temporary folder
                    UploadFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        //String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=No;\";";
                        String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 12.0;IMEX=1;HDR=No;\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            Log.Debug("Start Read Data Allot PTI From Excel");
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Sheet1$] ", _oleConn);
                            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                            oleAdapter.SelectCommand = _oleCmdSelect;
                            DataTable dt = new DataTable();

                            oleAdapter.FillSchema(dt, SchemaType.Source);
                            oleAdapter.Fill(dt);

                            Log.Debug("Start Add Data To Object");
                            int sumfaceamt = 0;
                            for (int i = 1; i < dt.Rows.Count; i++)
                            {
                                Model_MS_PTITRANS pti = new Model_MS_PTITRANS();
                                try
                                {
                                    pti.ExcelDealno = dt.Rows[i][0].ToString();
                                    pti.ExcelAttn = dt.Rows[i][1].ToString();
                                    pti.ExcelFaceAmt = dt.Rows[i][2].ToString().Replace(",", "");
                                    pti.ExcelCostAmt = dt.Rows[i][3].ToString();

                                    if (pti.ExcelFaceAmt.Equals("")) pti.ExcelFaceAmt = "0";
                                    sumfaceamt = sumfaceamt + int.Parse(pti.ExcelFaceAmt);

                                    //Check requeire data
                                    if (pti.ExcelDealno == null || pti.ExcelDealno.Equals(""))
                                    {
                                        showMessage += "กรุณาระบุ Dealno. ให้ครบถ้วน  <br>";
                                        //break;
                                    }
                                    else if (pti.ExcelAttn == null || pti.ExcelAttn.Equals(""))
                                    {
                                        showMessage += "กรุณาระบุ Attn. ให้ครบถ้วน  <br>";
                                        //break;
                                    }
                                    else if (pti.ExcelFaceAmt == null || pti.ExcelFaceAmt.Equals(""))
                                    {
                                        showMessage += "กรุณาระบุ Face Amount ให้ครบถ้วน  <br>";
                                        //break;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Log.Error(ex.Message);
                                }
                                l.Add(pti);
                            }

                            if (showMessage.Equals(""))
                            {

                                Model_MS_PTITRANS distdeal = (Model_MS_PTITRANS)l[0];
                                string dealfind = distdeal.ExcelDealno.ToString();

                                //Get Deal Info.
                                DataTable dtdeal = Model_MS_BOND.getBondByDealNo(Configuration.Config.ConnectionString, dealfind);
                                if (dtdeal != null)
                                {
                                    if (sumfaceamt.ToString().Equals(dtdeal.Rows[0]["TOTAL"].ToString()))
                                    {

                                        foreach (Model_MS_PTITRANS m in l)
                                        {
                                            //Get Customer no.
                                            DataTable dtcust = TreasuryModel.Data.Customer.getCustomerForName(Configuration.Config.ConnectionString, m.ExcelAttn);

                                            //New object to save data
                                            Model_MS_PTITRANS record = new Model_MS_PTITRANS();
                                            record.RelateRef = dtdeal.Rows[0]["DEALNO"].ToString();
                                            record.MsgType = "BAHTNET";
                                            record.Customer = dtcust.Rows[0]["CustomerID"].ToString();
                                            record.FaceAmt = Convert.ToString(Convert.ToDecimal(m.ExcelFaceAmt.Replace(",", "")) / 1000000);
                                            record.CostAmt = m.ExcelCostAmt.Replace(",", "");

                                            record.Create(Config.ConnectionString);
                                        }

                                        MessageBox.Text = "ระบบบันทึกข้อมูลเรียบร้อยแล้ว";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);

                                    }
                                    else
                                    {
                                        MessageBox.Text = "ผลรวม Face Amount ไม่เท่าจำนวนยอดคงเหลือ";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                    }

                                }
                                else
                                {
                                    MessageBox.Text = "Dealno ไม่ถูกต้อง";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                }

                            }
                            else
                            {
                                MessageBox.Text = showMessage;
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
                            //Close Connection
                            _oleConn.Close();

                            //Clear File Excel
                            if (Directory.Exists(pathFolder + filename))
                            {
                                string[] f = Directory.GetFiles(pathFolder + filename);
                                for (int i = 0; i < f.Length; i++)
                                {
                                    File.Delete(f[i]);
                                }
                                Directory.Delete(pathFolder + filename);
                            }
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
                MessageBox.Text = "ไม่สามารถทำการอัพโหลดไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }       
        }
        */
       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();

                //FolderName = dt.Rows[0]["RelatedRef"].ToString() + "_" + DateTime.Now.ToString("yyyyMMdd");
                //DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                //    md.TransLogNo = "PTI" + dt.Rows[i]["RelatedRef"].ToString() + DateTime.Now.ToString("yyyyMMdd");
                //    md.TransLogDate = DateTime.Now.ToString("dd/MM/yyyy");
                //    md.SecID = secid;
                //    md.PartialID = dt.Rows[i]["PartiID"].ToString();
                //    md.MsgType = dt.Rows[i]["MsgType"].ToString();
                //    md.Priority = dt.Rows[i]["Priority"].ToString();
                //    md.SenderRef = gdData.Rows[i].Cells[3].Text;
                //    md.RelateRef = gdData.Rows[i].Cells[4].Text;
                //    md.PlaceCode = dt.Rows[i]["PlaceCode"].ToString();
                //    md.SettDate = dt.Rows[i]["SettlementDate"].ToString();
                //    md.TradeDate = dt.Rows[i]["TradeDate"].ToString();
                //    md.PercentType = dt.Rows[i]["PercentageType"].ToString();
                //    md.DealPrice = dt.Rows[i]["DealPrice"].ToString();
                //    md.ISIN = dt.Rows[i]["ISIN"].ToString();
                //    md.FaceAmt = dt.Rows[i]["FaceAmount"].ToString();
                //    md.AcctNo = dt.Rows[i]["AcctNo"].ToString();
                //    md.CashAcct = dt.Rows[i]["CashAcct"].ToString();
                //    md.BusinessType = dt.Rows[i]["BusinessType"].ToString();
                //    md.SettType = dt.Rows[i]["SettlementType"].ToString();
                //    md.PS = dt.Rows[i]["PS"].ToString();
                //    md.SettPlace = dt.Rows[i]["SettlementPlace"].ToString();
                //    md.Attn = dt.Rows[i]["Attn"].ToString().Replace("'", "''");
                //    md.AgentBIC = dt.Rows[i]["AgentBIC"].ToString();
                //    md.AgentCA = dt.Rows[i]["AgentCA"].ToString();
                //    md.AgentAcctNo = dt.Rows[i]["AgentAcctNo"].ToString();
                //    md.PrinAmt = dt.Rows[i]["PrinAmt"].ToString();
                //    md.IntAmt = dt.Rows[i]["IntAmt"].ToString();
                //    md.SettAmt = dt.Rows[i]["SettlementAmt"].ToString();

                //    l.Add(md);

                //    FileStream f = File.Create(Server.MapPath("~/Temporary/" + FolderName) + "/PTI" + md.MsgType + md.SecID + md.SenderRef + ".xml");
                //    Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                //    f.Write(info, 0, info.Length);
                //    f.Close();

                //}
                //Session["FolderName"] = FolderName;
                //Session["PTITrans"] = l;
                //Compress();

                //btnDownload.Visible = false;
                //btnGoTo.Visible = false;
                //btnSave.Visible = true;
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
                List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();
                Model_MS_PTITRANS pt = new Model_MS_PTITRANS();

                FolderName = DealNoSearchBox.Text + "_" + DateTime.Now.ToString("yyyyMMdd");
                if(!Directory.Exists(Server.MapPath("~/Temporary/" + FolderName + "/")))
                    Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));

                //Get From Database
                DataTable dt = Model_MS_PTITRANS.getTransLogByDealNo(Config.ConnectionString, DealNoSearchBox.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                    md.TransLogNo = dt.Rows[i]["TransLogNo"].ToString().Trim();
                    md.TransLogDate = dt.Rows[i]["TransLogDate"].ToString().Trim();
                    md.SecID = dt.Rows[i]["SecID"].ToString().Trim();
                    md.PartialID = dt.Rows[i]["PartialID"].ToString().Trim();
                    md.MsgType = dt.Rows[i]["MsgType"].ToString().Trim();
                    md.Priority = dt.Rows[i]["Priority"].ToString().Trim();
                    md.SenderRef = dt.Rows[i]["SenderRef"].ToString().Trim();
                    md.RelateRef = dt.Rows[i]["RelateRef"].ToString().Trim();
                    md.PlaceCode = dt.Rows[i]["PlaceCode"].ToString().Trim();
                    md.SettDate = dt.Rows[i]["SettDate"].ToString().Trim();
                    md.TradeDate = dt.Rows[i]["TradeDate"].ToString().Trim();
                    md.PercentType = dt.Rows[i]["PercentType"].ToString().Trim();
                    md.DealPrice = dt.Rows[i]["DealPrice"].ToString().Trim();
                    md.ISIN = dt.Rows[i]["ISIN"].ToString().Trim();
                    md.FaceAmt = dt.Rows[i]["FaceAmt"].ToString().Trim();
                    md.AcctNo = dt.Rows[i]["AcctNo"].ToString().Trim();
                    md.CashAcct = dt.Rows[i]["CashAcct"].ToString().Trim();
                    md.BusinessType = dt.Rows[i]["BusinessType"].ToString().Trim();
                    md.SettType = dt.Rows[i]["SETTTYPE"].ToString().Trim();
                    md.PS = dt.Rows[i]["PS"].ToString().Trim();
                    md.SettPlace = dt.Rows[i]["SETTPLACE"].ToString().Trim();
                    md.Attn = dt.Rows[i]["Attn"].ToString().Replace("'", "''").Trim();
                    md.AgentBIC = dt.Rows[i]["AgentBIC"].ToString().Trim();
                    md.AgentCA = dt.Rows[i]["AgentCA"].ToString().Trim();
                    md.AgentAcctNo = dt.Rows[i]["AgentAcctNo"].ToString().Trim();
                    md.PrinAmt = dt.Rows[i]["PrinAmt"].ToString().Trim();
                    md.IntAmt = dt.Rows[i]["IntAmt"].ToString().Trim();
                    md.SettAmt = dt.Rows[i]["SettAmt"].ToString().Trim();

                    l.Add(md);
                    string filename = Server.MapPath("~/Temporary/" + FolderName) + "/PTI" + dt.Rows[i]["MsgType"].ToString().Trim() + dt.Rows[i]["SecID"].ToString().Trim() + dt.Rows[i]["SenderRef"].ToString().Trim() + string.Format("{0:00000}",i+1) + ".xml";
                    FileStream f = File.Create(filename);
                    Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                    f.Write(info, 0, info.Length);
                    f.Flush();
                    f.Close();
                }
                Compress();
                
                Session["FolderName"] = FolderName;
                if (pt.setGenerate(Config.ConnectionString, DealNoSearchBox.Text))
                {
                    btnSave.Visible = false;
                    btnDownload.Visible = true;
                    btnGoTo.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    btnDownload.Visible = false;
                    btnGoTo.Visible = false;
                    MessageBox.Text = "ไม่สามารถสร้างไฟล์ได้ กรุณาทำรายการใหม่";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnGoTo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Form/frm_TR051TRANSLIST.aspx");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Temporary/" + Session["FolderName"].ToString() + ".zip", false);
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
                using (Package Zip = Package.Open(Server.MapPath("~/Temporary/") + FolderName + ".zip", FileMode.OpenOrCreate))
                {
                    foreach (string F in Directory.GetFiles(Server.MapPath("~/Temporary/" + FolderName + "/"), "PTI*.xml", SearchOption.AllDirectories))
                    {
                        string F1 = F.Replace(' ', '_'); //ZipPackage can't handle spaces in filenames.
                        Uri URI = new Uri(F1.Substring(Server.MapPath("~/Temporary/" + FolderName + "/").Length), UriKind.Relative);

                        PackagePart ZipPart = Zip.CreatePart(PackUriHelper.CreatePartUri(URI), MediaTypeNames.Application.Zip, CompressionOption.Maximum);
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
