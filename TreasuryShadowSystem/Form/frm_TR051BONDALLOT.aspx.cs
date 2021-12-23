using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Master;
using TreasuryModel.Data;

using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR051BONDALLOT : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR051BONDALLOT));
        DataTable dt = new DataTable();
        Boolean IsEdit = true;
        Model_MS_PTITRANS pti = new Model_MS_PTITRANS();

        protected void Page_Init(object sender, EventArgs e)
        {             
                GridViewAllotment.PageIndexChanging += new GridViewPageEventHandler(GridViewAllotment_PageIndexChanging);
                GridViewAllotment.RowEditing += new GridViewEditEventHandler(GridViewAllotment_RowEditing);
                GridViewAllotment.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewAllotment_RowCancelingEdit);
                GridViewAllotment.RowCommand += new GridViewCommandEventHandler(GridViewAllotment_RowCommand);
                GridViewAllotment.RowDeleting += new GridViewDeleteEventHandler(GridViewAllotment_RowDeleting);
                GridViewAllotment.RowDataBound += new GridViewRowEventHandler(GridViewAllotment_RowDataBound);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                //    Response.Redirect("~/SessionExpired.aspx");
                                

            }   
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewAllotment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (!IsEdit)
                {
                    e.Row.Cells[0].FindControl("DeleteButton").Visible = false;
                    ImageButton edit = (ImageButton)e.Row.Cells[1].Controls[0];
                    edit.Visible = false;
                }
            }
            catch (Exception ex)
            { 
            
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
                    BindAllotment(e.CommandArgument.ToString());
                    string EditScript = "$('#dialoglot').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {'Submit': function() {";

                    EditScript += "var faceamt = $('#" + FaceAmtLotBox.ClientID + "').val() - $('#" + OldFaceAmtBox.ClientID + "').val(); ";
                    EditScript += "if (($('#" + RemainLotBox.ClientID + "').val() - (faceamt*1000000)) < 0){";
                    EditScript += "    alert('This is Over FaceAmount!!'); ";
                    EditScript += "    return false;} else {";
                    
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + DealNo.ClientID + "').val($('.dealnolot').val());";
                    EditScript += "if($('.customer :selected').val().trim()!='') {";
                    EditScript += "$('#" + Customer.ClientID + "').val($('.customer :selected').val());";
                    EditScript += "} else {";
                    EditScript += "$('#" + Customer.ClientID + "').val('');";
                    EditScript += "}";
                    EditScript += "$('#" + FaceAmt.ClientID + "').val($('.faceamtlot').val());event.preventDefault();";
                    EditScript += "$('input#" + DialogSubmit.ClientID + "').click();}";
                    EditScript += "},'Close': function() {$(this).dialog('close');}}}); ";

                    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);
                }
                else if (e.CommandName.Equals("Delete"))
                {
                    Delete(e.CommandArgument.ToString());
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
                RemainLotBox.Value = dt.Rows[0]["Remain"].ToString();
                FaceAmtLotBox.Value = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["FaceAmt"].ToString())/1000000);
                OldFaceAmtBox.Value = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["FaceAmt"].ToString()) / 1000000);
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
                Model_MS_PTITRANS m = new Model_MS_PTITRANS();
                if (m.isGenerate(Config.ConnectionString, DealNoSearchBox.Text.Trim()) > 0)
                    IsEdit = false;
                dt = Model_MS_PTITRANS.getTransLogByDealNo(Config.ConnectionString, DealNoSearchBox.Text);
                GridViewAllotment.DataSource = dt;
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
                    if (ActionBox.Value.Equals("Edit"))
                    {
                        Model_MS_PTITRANS m = new Model_MS_PTITRANS();
                        m.SenderRef = DealNo.Value.Trim();
                        m.RelateRef = DealNo.Value.Trim();
                        m.Customer = Customer.Value.Trim();
                        m.FaceAmt = FaceAmt.Value.Trim();
                        m.SettAmt = "0";
                        m.CostAmt = "0";
                        m.MsgType = "BAHTNET";
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
                        Model_MS_PTITRANS m = new Model_MS_PTITRANS();
                        m.SenderRef = DealNo.Value.Trim();
                        m.RelateRef = DealNo.Value.Trim();
                        m.Customer = Customer.Value.Trim();
                        m.FaceAmt = FaceAmt.Value.Trim();
                        //m.MsgType = "BAHTNET";

                        if (PayType.Value.Equals("BN") || PayType.Value.ToUpper().Equals("BAHTNET"))
                        {
                            m.MsgType = "BAHTNET";
                        }
                        else m.MsgType = "CHEQUE";

                        if (m.isGenerate(Config.ConnectionString, m.RelateRef) == 0)
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
                        else {
                            MessageBox.Text = "ไม่สามารถบันทึกข้อมูลได้ Deal อาจจะถูก Generate แล้ว กรณีต้องการเพิ่ม แก้ไข หรือลบรายการกรุณาแจ้ง Back Office";
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
    }
}
