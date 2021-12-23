using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;

using log4net;

namespace TreasuryShadowSystem.Maintain
{
    public partial class frm_Custodian : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_Custodian));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                GridViewCustodian.PageIndexChanging += new GridViewPageEventHandler(GridViewCustodian_PageIndexChanging);
                GridViewCustodian.RowEditing += new GridViewEditEventHandler(GridViewCustodian_RowEditing);
                GridViewCustodian.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewCustodian_RowCancelingEdit);
                GridViewCustodian.RowCommand += new GridViewCommandEventHandler(GridViewCustodian_RowCommand);
                GridViewCustodian.RowDeleting += new GridViewDeleteEventHandler(GridViewCustodian_RowDeleting);

                if (!IsPostBack)
                {
                    //BindAttn();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustodian_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                Custodian c = new Custodian();
                c.CustodianID = int.Parse(key);
                if (c.Delete(Config.ConnectionString))
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

        void GridViewCustodian_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindAllotment(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + Name.ClientID + "').val($('#" + NameBox.ClientID + "').val());";
                    EditScript += "$('#" + Bic.ClientID + "').val($('#" + BicBox.ClientID + "').val());";
                    EditScript += "$('#" + AcctNo.ClientID + "').val($('#" + AcctNoBox.ClientID + "').val());";
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

        void BindAllotment(string id)
        {
            try
            {
                string key = GridViewCustodian.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = Custodian.getCustodianByID(Config.ConnectionString, key);
                CustodianID.Value = key;
                ActionBox.Value = "Edit";
                NameBox.Text = dt.Rows[0]["Name"].ToString();
                BicBox.Text = dt.Rows[0]["Bic"].ToString();
                AcctNoBox.Text = dt.Rows[0]["AcctNo"].ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustodian_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewCustodian.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustodian_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewCustodian.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustodian_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewCustodian.PageIndex = e.NewPageIndex;
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
                GridViewCustodian.DataSource = Custodian.getCustodianByCondition(Config.ConnectionString, NameSearchBox.Text);
                GridViewCustodian.DataBind();
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
                Custodian c = new Custodian();
                c.Name = Name.Value;
                c.BIC = Bic.Value;
                c.AccountNo = AcctNo.Value;

                if (ValidateDialog(c))
                {
                    if (ActionBox.Value.Equals("Edit"))
                    {
                        c.CustodianID = int.Parse(CustodianID.Value);

                        if (c.Update(Config.ConnectionString))
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
                        if (c.Create(Config.ConnectionString))
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


        private Boolean ValidateDialog(Custodian m)
        {
            try
            {
                if (m.Name.Trim().Equals(""))
                {
                    MessageBox.Text = "Name is require!!";
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
    }
}
