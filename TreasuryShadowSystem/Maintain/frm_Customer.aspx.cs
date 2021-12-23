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
    public partial class frm_Customer : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_Customer));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                GridViewCustomer.PageIndexChanging += new GridViewPageEventHandler(GridViewCustomer_PageIndexChanging);
                GridViewCustomer.RowEditing += new GridViewEditEventHandler(GridViewCustomer_RowEditing);
                GridViewCustomer.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewCustomer_RowCancelingEdit);
                GridViewCustomer.RowCommand += new GridViewCommandEventHandler(GridViewCustomer_RowCommand);
                GridViewCustomer.RowDeleting += new GridViewDeleteEventHandler(GridViewCustomer_RowDeleting);

                if (!IsPostBack)
                {
                    BindCustodian();
                    BindIssuerType();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
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
                Customer m = new Customer();
                m.CustomerID = int.Parse(key);
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

        void GridViewCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindCustomer(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:750, modal: true ";
                    EditScript += ", open: function(){ ";
                    EditScript += "if($('[id$=Issuer]').val() == '1') { $('#custIssuer').prop('checked', true); } ";
                    EditScript += "if($('[id$=Seller]').val() == '1') { $('#custSeller').prop('checked', true); } } ";
                    EditScript += ",'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + CustomerType.ClientID + "').val($('#" + CustomerTypeBox.ClientID + "').val());";
                    EditScript += "$('#" + Code.ClientID + "').val($('#" + CodeBox.ClientID + "').val());";
                    EditScript += "$('#" + NameTh.ClientID + "').val($('#" + NameThBox.ClientID + "').val());";
                    EditScript += "$('#" + NameEn.ClientID + "').val($('#" + NameEnBox.ClientID + "').val());";
                    EditScript += "$('#" + SegregateAcct.ClientID + "').val($('#" + SegregateAcctBox.ClientID + "').val());";
                    EditScript += "$('#" + CustodianID.ClientID + "').val($('#" + CustodianBox.ClientID + "').val());";
                    EditScript += "$('#" + AssetCompany.ClientID + "').val($('#" + AssetCompanyBox.ClientID + "').val());";
                    EditScript += "$('#" + TaxID.ClientID + "').val($('#" + TaxIDBox.ClientID + "').val());";
                    EditScript += "$('#" + GIIN.ClientID + "').val($('#" + GIINBox.ClientID + "').val());";
                    EditScript += "$('#" + Address.ClientID + "').val($('#" + AddressBox.ClientID + "').val());";
                    EditScript += "$('#" + Contact.ClientID + "').val($('#" + ContactBox.ClientID + "').val());";
                    EditScript += "$('#" + Tel.ClientID + "').val($('#" + TelBox.ClientID + "').val());";
                    EditScript += "$('#" + BuildingName.ClientID + "').val($('#" + BuildingNameBox.ClientID + "').val());";
                    EditScript += "$('#" + FloorNo.ClientID + "').val($('#" + FloorNoBox.ClientID + "').val());";
                    EditScript += "$('#" + RoomNo.ClientID + "').val($('#" + RoomNoBox.ClientID + "').val());";
                    EditScript += "$('#" + VillageName.ClientID + "').val($('#" + VillageNameBox.ClientID + "').val());";
                    EditScript += "$('#" + AddressNo.ClientID + "').val($('#" + AddressNoBox.ClientID + "').val());";
                    EditScript += "$('#" + MooNo.ClientID + "').val($('#" + MooNoBox.ClientID + "').val());";
                    EditScript += "$('#" + Soi.ClientID + "').val($('#" + SoiBox.ClientID + "').val());";
                    EditScript += "$('#" + StreetName.ClientID + "').val($('#" + StreetNameBox.ClientID + "').val());";
                    EditScript += "$('#" + Tumbon.ClientID + "').val($('#" + TumbonBox.ClientID + "').val());";
                    EditScript += "$('#" + Amphur.ClientID + "').val($('#" + AmphurBox.ClientID + "').val());";
                    EditScript += "$('#" + Province.ClientID + "').val($('#" + ProvinceBox.ClientID + "').val());";
                    EditScript += "$('#" + PostalCode.ClientID + "').val($('#" + PostalCodeBox.ClientID + "').val());";
                    EditScript += "$('#" + IssuerRemark.ClientID + "').val($('#" + IssuerRemarkBox.ClientID + "').val());";
                    EditScript += "$('#" + AnnualRegistText.ClientID + "').val($('#" + AnnualRegistTextBox.ClientID + "').val());";
                    EditScript += "$('#" + TargetBank.ClientID + "').val($('#" + TargetBankBox.ClientID + "').val());";
                    EditScript += "$('#" + TargetAcct.ClientID + "').val($('#" + TargetAcctBox.ClientID + "').val());";
                    EditScript += "if($('#" + IncVatBox.ClientID + "').is(':checked')) ";
                    EditScript += "$('#" + IncVat.ClientID + "').val(1); ";
                    EditScript += " else ";
                    EditScript += "$('#" + IncVat.ClientID + "').val(0);";
                    EditScript += "if($('[name$=custTypeRadioList]:checked').val()) { ";
                    EditScript += "if($('[name$=custTypeRadioList]:checked').val().toUpperCase() == 'SELLER') { ";
                    EditScript += "$('#" + Seller.ClientID + "').val(1); ";
                    EditScript += "$('#" + Issuer.ClientID + "').val(0); ";
                    EditScript += "$('#" + IssuerType.ClientID + "').val(''); ";
                    EditScript += "} else {";
                    EditScript += "$('#" + Issuer.ClientID + "').val(1); ";
                    EditScript += "$('#" + Seller.ClientID + "').val(0); ";
                    EditScript += "$('#" + IssuerType.ClientID + "').val($('#" + IssuerTypeBox.ClientID + "').val()); } } ";
                    EditScript += "if($('#" + AnnualRegistBox.ClientID + "').is(':checked')) ";
                    EditScript += "$('#" + AnnualRegist.ClientID + "').val(1); ";
                    EditScript += " else ";
                    EditScript += "$('#" + AnnualRegist.ClientID + "').val(0);";
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

        void BindCustomer(string id)
        {
            try
            {
                string key = GridViewCustomer.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = Customer.getCustomerByID(Config.ConnectionString, key);
                CustomerID.Value = key;
                ActionBox.Value = "Edit";
                CustomerTypeBox.SelectedValue = dt.Rows[0]["CustomerType"].ToString().Trim();
                CodeBox.Text = dt.Rows[0]["Code"].ToString().Trim();
                NameThBox.Text = dt.Rows[0]["NameTh"].ToString().Trim();
                NameEnBox.Text = dt.Rows[0]["NameEn"].ToString().Trim();
                SegregateAcctBox.Text = dt.Rows[0]["SegregateAcct"].ToString().Trim();
                CustodianBox.SelectedValue = dt.Rows[0]["CustodianID"].ToString();
                AssetCompanyBox.Text = dt.Rows[0]["AssetCompany"].ToString().Trim();
                TaxIDBox.Text = dt.Rows[0]["TaxID"].ToString().Trim();
                AddressBox.Text = dt.Rows[0]["Address"].ToString().Trim();
                GIINBox.Text = dt.Rows[0]["GIIN"].ToString().Trim();
                ContactBox.Text = dt.Rows[0]["Contact"].ToString().Trim();
                TelBox.Text = dt.Rows[0]["Tel"].ToString().Trim();
                BuildingNameBox.Text = dt.Rows[0]["BuildingName"].ToString().Trim();
                FloorNoBox.Text = dt.Rows[0]["FloorNo"].ToString().Trim();
                RoomNoBox.Text = dt.Rows[0]["RoomNo"].ToString().Trim();
                VillageNameBox.Text = dt.Rows[0]["VillageName"].ToString().Trim();
                AddressNoBox.Text = dt.Rows[0]["AddressNo"].ToString().Trim();
                MooNoBox.Text = dt.Rows[0]["MooNo"].ToString().Trim();
                SoiBox.Text = dt.Rows[0]["Soi"].ToString().Trim();
                StreetNameBox.Text = dt.Rows[0]["StreetName"].ToString().Trim();
                TumbonBox.Text = dt.Rows[0]["Tumbon"].ToString().Trim();
                AmphurBox.Text = dt.Rows[0]["Amphur"].ToString().Trim();
                ProvinceBox.Text = dt.Rows[0]["Province"].ToString().Trim();
                PostalCodeBox.Text = dt.Rows[0]["PostalCode"].ToString().Trim();
                TargetBankBox.Text = dt.Rows[0]["TargetBank"].ToString().Trim();
                TargetAcctBox.Text = dt.Rows[0]["TargetAcct"].ToString().Trim();
                if (dt.Rows[0]["IncVat"].ToString().Equals("1"))
                {
                    IncVatBox.Checked = true;
                }
                else
                {
                    IncVatBox.Checked = false;
                }
                // issuer
                if (dt.Rows[0]["Issuer"].ToString() == "1")
                {
                    Issuer.Value = dt.Rows[0]["Issuer"].ToString();
                    IssuerTypeBox.Text = dt.Rows[0]["IssuerType"].ToString().Trim();
                }
                else
                {
                    IssuerTypeBox.Enabled = false;
                }
                // seller
                if(dt.Rows[0]["Seller"].ToString() == "1")
                {
                    Seller.Value = dt.Rows[0]["Seller"].ToString();
                }

                IssuerRemarkBox.Text = dt.Rows[0]["IssuerRemark"].ToString().Trim();
                if (dt.Rows[0]["AnnualRegist"].ToString().Equals("1"))
                {
                    AnnualRegistBox.Checked = true;
                }
                else
                {
                    AnnualRegistBox.Checked = false;
                }
                AnnualRegistTextBox.Text = dt.Rows[0]["AnnualRegistText"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustomer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
            try
            {
                GridViewCustomer.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustomer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            try
            {
                GridViewCustomer.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            try
            {
                GridViewCustomer.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindCustodian()
        {
            try
            {
                CustodianBox.DataSource = Custodian.getCustodianByCondition(Config.ConnectionString, "");
                CustodianBox.DataTextField = "Name";
                CustodianBox.DataValueField = "CustodianID";
                CustodianBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindIssuerType()
        {
            try
            {

                DataTable issueType = new DataTable();
                issueType.Columns.Add("NAME", typeof(string));
                issueType.Columns.Add("TYPE", typeof(string));

                issueType.Rows.Add("None", "");
                issueType.Rows.Add("PP10", "PP10");
                issueType.Rows.Add("II", "II");
                issueType.Rows.Add("II-HNW", "II-HNW");
                issueType.Rows.Add("HNW", "HNW");
                issueType.Rows.Add("Other", "Other");              

                IssuerTypeBox.DataSource = issueType;
                IssuerTypeBox.DataTextField = "NAME";
                IssuerTypeBox.DataValueField = "TYPE";
                IssuerTypeBox.DataBind();

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
                GridViewCustomer.DataSource = Customer.getCustomerByCondition(Config.ConnectionString, NameSearchBox.Text);
                GridViewCustomer.DataBind();
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
                Customer c = new Customer();
                c.CustomerType = CustomerType.Value;
                c.Code = Code.Value;
                c.NameTh = NameTh.Value;
                c.NameEn = NameEn.Value;
                c.SegregateAcct = SegregateAcct.Value;
                c.CustodianID = Int32.Parse(CustodianID.Value);
                c.AssetCompany = AssetCompany.Value;
                c.TaxID = TaxID.Value;
                c.Address = Address.Value;
                c.GIIN = GIIN.Value;
                c.Contact = Contact.Value;
                c.Tel = Tel.Value;
                c.BuildingName = BuildingName.Value;
                c.FloorNo = FloorNo.Value;
                c.RoomNo = RoomNo.Value;
                c.VillageName = VillageName.Value;
                c.AddressNo = AddressNo.Value;
                c.MooNo = MooNo.Value;
                c.Soi = Soi.Value;
                c.StreetName = StreetName.Value;
                c.Tumbon = Tumbon.Value;
                c.Amphur = Amphur.Value;
                c.Province = Province.Value;
                c.PostalCode = PostalCode.Value;                
                c.IncVat = IncVat.Value;
                c.TargetBank = TargetBank.Value;
                c.TargetAcct = TargetAcct.Value;
                c.Issuer = Issuer.Value;
                c.Seller = Seller.Value;
                c.IssuerType = IssuerType.Value;
                c.IssuerRemark = IssuerRemark.Value;
                c.AnnualRegist = AnnualRegist.Value;
                c.AnnualRegistText = AnnualRegistText.Value;

                if (ValidateDialog(c))
                {
                    if (ActionBox.Value.Equals("Edit"))
                    {
                        c.CustomerID = int.Parse(CustomerID.Value);

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

        private Boolean ValidateDialog(Customer m)
        {
            try
            {
                if (m.NameTh.Trim().Equals(""))
                {
                    MessageBox.Text = "Name (Th) is require!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                if (m.NameEn.Trim().Equals(""))
                {
                    MessageBox.Text = "Name (En) is require!!";
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
