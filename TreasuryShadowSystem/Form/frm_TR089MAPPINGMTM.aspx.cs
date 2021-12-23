using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryModel.Master;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using System.Data;
using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR089MAPPINGMTM : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR089MAPPINGMTM));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridViewFund.PageIndexChanging += new GridViewPageEventHandler(GridViewFund_PageIndexChanging);
                GridViewFund.RowEditing += new GridViewEditEventHandler(GridViewFund_RowEditing);
                GridViewFund.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewFund_RowCancelingEdit);
                GridViewFund.RowCommand += new GridViewCommandEventHandler(GridViewFund_RowCommand);
                GridViewFund.RowDeleting += new GridViewDeleteEventHandler(GridViewFund_RowDeleting);
                GridViewFund.RowDataBound += new GridViewRowEventHandler(GridViewFund_RowDataBound);
                if (!IsPostBack)
                    BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_RowDataBound(object sender, GridViewRowEventArgs e)
        { 

        }

        void GridViewFund_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //
        }

        void Delete(string key)
        {
            try
            {
                Model_MS_MTMMAPPING m = new Model_MS_MTMMAPPING();
                m.MTMMappingID = int.Parse(key);
                if (m.DeleteMTMMapping(Config.ConnectionString))
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

        void GridViewFund_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindMapping(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + Name.ClientID + "').val($('#" + txtName.ClientID + "').val());";
                    EditScript += "$('#" + Data1.ClientID + "').val($('#" + Data1Box.ClientID + "').val());";
                    EditScript += "$('#" + Data2.ClientID + "').val($('#" + Data2Box.ClientID + "').val());";
                    EditScript += "$('#" + Data3.ClientID + "').val($('#" + Data3Box.ClientID + "').val());";
                    EditScript += "$('#" + Data4.ClientID + "').val($('#" + Data4Box.ClientID + "').val());";
                    EditScript += "$('input#" + DialogSubmit.ClientID + "').click();";
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

        void BindMapping(string id)
        {
            try
            {
                string key = GridViewFund.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = Model_MS_MTMMAPPING.getMapping(Config.ConnectionString, int.Parse(key));
                ActionBox.Value = "Edit";
                txtName.Text = dt.Rows[0]["Name"].ToString();
                Data1Box.Text = dt.Rows[0]["Data_1"].ToString();
                Data2Box.Text = dt.Rows[0]["Data_2"].ToString();
                Data3Box.Text = dt.Rows[0]["Data_3"].ToString();
                Data4Box.Text = dt.Rows[0]["Data_4"].ToString();

                MTMMapping.Value = key;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewFund.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewFund.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewFund.PageIndex = e.NewPageIndex;
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
                GridViewFund.DataSource = Model_MS_MTMMAPPING.getMappingDataByName(Config.ConnectionString, NameBox.Text);
                GridViewFund.DataBind();
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
                Model_MS_MTMMAPPING m = new Model_MS_MTMMAPPING();
                m.Name = Name.Value;
                m.Data1 = Data1.Value;
                m.Data2 = Data2.Value;
                m.Data3 = Data3.Value;
                m.Data4 = Data4.Value;
                if (ValidateDialog(m))
                {
                    if (ActionBox.Value.Equals("Edit"))
                    {
                        m.MTMMappingID = int.Parse(MTMMapping.Value);

                        if (m.UpdateMTMMapping(Config.ConnectionString))
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
                        if (m.AddMTMMapping(Config.ConnectionString))
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
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }


        private Boolean ValidateDialog(Model_MS_MTMMAPPING m)
        {
            try
            {
                if (m.Name.Trim().Equals(""))
                {
                    MessageBox.Text = "Name is require!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                if (m.Data1.Trim().Equals(""))
                {
                    MessageBox.Text = "Column (Select) is require!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                if (m.Data2.Trim().Equals(""))
                {
                    MessageBox.Text = "Column (Select+) is require!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                if (m.Data3.Trim().Equals(""))
                {
                    MessageBox.Text = "Table (From) is require!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                if (m.Data4.Trim().Equals(""))
                {
                    MessageBox.Text = "Condition (Where) is require!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
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

        protected void SearchButton_Click1(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
