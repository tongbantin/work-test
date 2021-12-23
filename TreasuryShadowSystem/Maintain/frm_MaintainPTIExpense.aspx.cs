using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryModel.Master;
using System.Web.UI.HtmlControls;

namespace TreasuryShadowSystem.Maintain
{
    public partial class frm_MaintainPTIExpense : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridViewExpense.PageIndexChanging += new GridViewPageEventHandler(GridViewExpense_PageIndexChanging);
                GridViewExpense.RowEditing += new GridViewEditEventHandler(GridViewExpense_RowEditing);
                GridViewExpense.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewExpense_RowCancelingEdit);
                GridViewExpense.RowUpdating += new GridViewUpdateEventHandler(GridViewExpense_RowUpdating);
            }
            catch (Exception ex)
            {
                
            }
        }

        void GridViewExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewExpense.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            { 
            
            }
        }

        void GridViewExpense_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewExpense.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            { 
            
            }
        }

        void GridViewExpense_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewExpense.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            { 
            
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
            }
        }

        private void BindGrid()
        {
            try
            {
                GridViewExpense.DataSource = Model_MS_DEALFEE.GetDealFee(Configuration.Config.ConnectionString, txtDealNo.Value, txtExecuteDate.Value, txtSettDate.Value);
                GridViewExpense.DataBind();
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!txtDealNo.Value.Trim().Equals("")) || (!txtExecuteDate.Value.Trim().Equals("")) || (!txtSettDate.Value.Trim().Equals("")))
                    BindGrid();
                else {

                    MessageBox.Text = "กรุณาระบุเงื่อนไขในการค้นหาอย่างน้อย 1 ช่อง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        void GridViewExpense_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                DropDownList NewValue = (DropDownList)GridViewExpense.Rows[e.RowIndex].Cells[9].Controls[1];
                string key = GridViewExpense.DataKeys[e.RowIndex].Value.ToString();

                if (Model_MS_DEALFEE.UpdateDealFee(Configuration.Config.ConnectionString, key, NewValue.SelectedValue))
                {
                    GridViewExpense.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    NewValue.BorderColor = Color.Red;
                }

            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void DialogSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDialog())
                {
                    if (Model_MS_DEALFEE.AddDealFee(Configuration.Config.ConnectionString, CostBox.Value, DealNoBox.Value, SettDateBox.Value, ExpenseTypeBox.Value, FeeBox.Value))
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
            catch (Exception ex)
            {
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        private Boolean ValidateDialog()
        {
            try
            {
                decimal fee;
                Decimal.TryParse(FeeBox.Value, out fee);
                if (CostBox.Value.Trim().Equals("")) {
                    MessageBox.Text = "Cost Invalid Data";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                else if (SettDateBox.Value.Trim().Equals(""))
                {
                    MessageBox.Text = "Settlement Date Invalid Data";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                else if (FeeBox.Value.Trim().Equals(""))
                {
                    MessageBox.Text = "Fee Amount Invalid Data";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                else if (fee==0)
                {
                    MessageBox.Text = "Fee Amount Invalid Data";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void ExcelExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewExport.AllowPaging = false;
                GridViewExport.DataSource = Model_MS_DEALFEE.GetDealFee(Configuration.Config.ConnectionString, txtDealNo.Value, txtExecuteDate.Value, txtSettDate.Value);
                GridViewExport.DataBind();
                GridViewExport.Visible = true;

                Response.AddHeader("content-disposition", "attachment;filename=DealFee.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw =new HtmlTextWriter(sw);
                HtmlForm frm =new HtmlForm();


                frm.Attributes["runat"] = "server";
                frm.Controls.Add(GridViewExport);
                GridViewExport.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
                GridViewExport.Visible = false;
            }
            catch (Exception ex)
            { 
                
            }
        }
    }
}
