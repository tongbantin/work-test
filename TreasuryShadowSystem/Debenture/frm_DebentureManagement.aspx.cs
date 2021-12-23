using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using TreasuryModel.Data;
using TreasuryShadowSystem.Configuration;
using log4net;
using TreasuryModel.Enum;
using System.Web.Services;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_DebentureManagement : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_DebentureManagement));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    GridDebenture.PageIndexChanging += new GridViewPageEventHandler(GridDebenture_PageIndexChanging);
                    GridDebenture.RowEditing += new GridViewEditEventHandler(GridDebenture_RowEditing);
                    GridDebenture.RowCancelingEdit += new GridViewCancelEditEventHandler(GridDebenture_RowCancelingEdit);
                    GridDebenture.RowCommand += new GridViewCommandEventHandler(GridDebenture_RowCommand);
                    GridDebenture.RowDeleting += new GridViewDeleteEventHandler(GridDebenture_RowDeleting);

                    if (!IsPostBack)
                    {
                        //BindBond();
                        BindCustodian();
                        BindBOTType();
                        BindInputDate();
                    }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);  
            }
        }

        //void BindBond()
        //{
        //    try
        //    {
        //        BondSearchBox.DataSource = Model_MS_Debenture.getDebentureDistinct(Config.ConnectionString);
        //        BondSearchBox.DataTextField = "SECID";
        //        BondSearchBox.DataValueField = "SECID";
        //        BondSearchBox.DataBind();

        //        BondSearchBox.Items.Insert(0, new ListItem("ทั้งหมด", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //    }
        //}

        void BindCustodian()
        {
            try
            {
                CustodianSearchBox.DataSource = TreasuryModel.Data.Custodian.getCustodianByCondition(Config.ConnectionString, "");
                CustodianSearchBox.DataTextField = "Name";
                CustodianSearchBox.DataValueField = "Name";
                CustodianSearchBox.DataBind();

                CustodianSearchBox.Items.Insert(0, new ListItem("ทั้งหมด", ""));

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        
        void BindBOTType()
        {
            try
            {
                BOTTypeBox.DataSource = Model_MS_Debenture.getCustomerTypeMapping(Config.ConnectionString);
                BOTTypeBox.DataTextField = "BOTTYPEDESC";
                BOTTypeBox.DataValueField = "BOTTYPE";
                BOTTypeBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridDebenture_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            try
            {

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridDebenture_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindDebenture(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + DealNo.ClientID + "').val($('.dealnobox').val());";
                    EditScript += "$('#" + SecID.ClientID + "').val($('.secidbox').val());";
                    EditScript += "$('#" + Unit.ClientID + "').val($('.unitbox').val());";
                    EditScript += "$('#" + Amount.ClientID + "').val($('.amountbox').val());";
                    EditScript += "$('#" + Custodian.ClientID + "').val($('.custodianbox').val());";
                    EditScript += "$('#" + NameThai.ClientID + "').val($('.namethaibox').val());";
                    EditScript += "$('#" + CustID.ClientID + "').val($('.custidbox').val());";
                    EditScript += "$('#" + ThaiBMA.ClientID + "').val($('.thaibmabox').val());";
                    EditScript += "$('#" + CustType.ClientID + "').val($('.custtypebox').val());";
                    EditScript += "$('#" + CType.ClientID + "').val($('.ctypebox').val());";
                    EditScript += "$('#" + BOTType.ClientID + "').val($('.bottypebox').val());";
                    EditScript += "$('#" + CustomerNumber.ClientID + "').val($('.customernumberbox').val());";
                    EditScript += "event.preventDefault();";
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

        void Delete(string key)
        {
            try
            {
                Model_MS_Debenture m = new Model_MS_Debenture();
                m.DebentureID = int.Parse(key);
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
        void BindDebenture(string id)
        {
            try
            {
                string key = GridDebenture.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = Model_MS_Debenture.getDebenture(Config.ConnectionString, key);
                DebentureID.Value = key;
                ActionBox.Value = "Edit";
                DealNoBox.Value = dt.Rows[0]["DealNo"].ToString().Trim();
                SecIDBox.Value = dt.Rows[0]["SecID"].ToString().Trim();
                UnitBox.Value = dt.Rows[0]["Unit"].ToString().Trim();
                AmountBox.Value = dt.Rows[0]["Amount"].ToString().Trim();
                CustodianBox.Value = dt.Rows[0]["Custodian"].ToString();
                NameThaiBox.Value = dt.Rows[0]["NameThai"].ToString();
                CustomerBox.Value = dt.Rows[0]["CustID"].ToString();
                ThaiBMANoBox.Value = dt.Rows[0]["ThaiBMANo"].ToString();
                CustTypeBox.Value = dt.Rows[0]["CustType"].ToString();
                BOTTypeBox.Value = dt.Rows[0]["BOTType"].ToString();
                BindCTypeByBOTType(BOTTypeBox.Value);
                CTypeBox.Value = dt.Rows[0]["CType"].ToString();
                CustomerNumberBox.Value = dt.Rows[0]["CustomerNumber"].ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridDebenture_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
            try
            {
                GridDebenture.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridDebenture_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            try
            {
                GridDebenture.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridDebenture_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            try
            {
                GridDebenture.PageIndex = e.NewPageIndex;
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
                string InputDate = "";
                string Round = "";
                if (!InputDateRoundSearchBox.SelectedValue.Equals(""))
                { 
                    InputDate = InputDateRoundSearchBox.SelectedValue.Substring(0, 10);
                    Round = InputDateRoundSearchBox.SelectedValue.Substring(11);
                }
                DataTable debttb = Model_MS_Debenture.getDebentureList(Config.ConnectionString, DealNoSearchBox.Text.Trim(), BondSearchBox.Text.Trim(), CustodianSearchBox.SelectedValue, InputDate, Round, StartDateSearchBox.Text, ToDateSearchBox.Text);
                GridDebenture.DataSource = debttb;
                GridDebenture.DataBind();

                TotalRecords.Text = debttb.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindInputDate()
        {
            try
            {
                InputDateRoundSearchBox.DataSource = Model_MS_Debenture.getInputDateRound(Config.ConnectionString);
                InputDateRoundSearchBox.DataTextField = "InputDateDisplay";
                InputDateRoundSearchBox.DataValueField = "InputDateValue";
                InputDateRoundSearchBox.DataBind();

                InputDateRoundSearchBox.Items.Insert(0, new ListItem("ทั้งหมด", ""));
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
                Model_MS_Debenture d = new Model_MS_Debenture();
                d.DealNo = DealNo.Value;
                d.SecID = SecID.Value;
                d.Unit = Int64.Parse(Unit.Value);
                d.Amount = Int64.Parse(Amount.Value);
                d.Custodian = Custodian.Value;
                d.NameThai = NameThai.Value;
                d.CustID = CustID.Value;
                d.ThaiBMANo = ThaiBMA.Value;
                d.CustType = CustType.Value;
                d.CType = CType.Value;
                d.BOTType = BOTType.Value;
                d.CustomerNumber = CustomerNumber.Value;
                if (ActionBox.Value.Equals("Edit"))
                {
                    d.DebentureID = int.Parse(DebentureID.Value);

                    if (d.Update(Config.ConnectionString))
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
                    if (d.Create(Config.ConnectionString))
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

                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                BindGrid();
                btnDeleteSelected.Visible = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {

            try
            {
                string key = "";
                for (int i = 0; i < GridDebenture.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridDebenture.Rows[i].Cells[0].FindControl("chkDeleteDocument");
                    if (chk.Checked)
                    {
                        key += " " + GridDebenture.DataKeys[i].Value.ToString();
                    }
                }

                key = key.Trim().Replace(' ', ',');

                Model_MS_Debenture m = new Model_MS_Debenture();
                if (m.Delete(Config.ConnectionString, key))
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

        void BindCTypeByBOTType(string BOTType)
        {
            try
            {
                CTypeBox.DataSource = Model_MS_Debenture.getCustomerTypeMapping(Config.ConnectionString, BOTType);
                CTypeBox.DataValueField = "Value";
                CTypeBox.DataTextField = "Key";
                CTypeBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SecTypeList(string BOTType)
        {
            List<KeyValuePair<string,string>> sectype= Model_MS_Debenture.getCustomerTypeMapping(Config.ConnectionString, BOTType);
            return new { records = sectype };
        }
    }
}
