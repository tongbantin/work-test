using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryShadowSystem.Configuration;
using TreasuryModel.Master;
using System.Web.UI.HtmlControls;

namespace TreasuryShadowSystem.Maintain
{
    public partial class frm_FundPosition : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridViewFund.PageIndexChanging += new GridViewPageEventHandler(GridViewFund_PageIndexChanging);
                GridViewFund.RowEditing += new GridViewEditEventHandler(GridViewFund_RowEditing);
                GridViewFund.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewFund_RowCancelingEdit);
                GridViewFund.RowUpdating += new GridViewUpdateEventHandler(GridViewFund_RowUpdating);
            }
            catch (Exception ex)
            {

            }
        }

        void GridViewFund_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                TextBox EffDate = (TextBox)GridViewFund.Rows[e.RowIndex].Cells[1].Controls[1];
                TextBox Amount = (TextBox)GridViewFund.Rows[e.RowIndex].Cells[2].Controls[1];
                string key = GridViewFund.DataKeys[e.RowIndex].Value.ToString();

                if (Model_MS_FUNDPOSITION.UpdateFundPosition(Config.ConnectionString, key, EffDate.Text, Amount.Text))
                {
                    GridViewFund.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    EffDate.BorderColor = Color.Red;
                }

            }
            catch (Exception ex)
            {

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

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!txtFromDate.Value.Trim().Equals("")) || (!txtToDate.Value.Trim().Equals("")))
                    BindGrid();
                else
                {

                    MessageBox.Text = "กรุณาระบุเงื่อนไขในการค้นหาอย่างน้อย 1 ช่อง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid()
        {
            try
            {
                GridViewFund.DataSource = Model_MS_FUNDPOSITION.GetFundPostion(Configuration.Config.ConnectionString, txtFromDate.Value, txtToDate.Value);
                GridViewFund.DataBind();
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
                    if (Model_MS_FUNDPOSITION.AddFundPosition(Config.ConnectionString, EffDateBox.Value, AmountBox.Value))
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
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }


        private Boolean ValidateDialog()
        {
            try
            {
                decimal fee;
                Decimal.TryParse(AmountBox.Value, out fee);
                if (fee == 0)
                {
                    MessageBox.Text = "Amount Invalid Data";
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
    }
}
