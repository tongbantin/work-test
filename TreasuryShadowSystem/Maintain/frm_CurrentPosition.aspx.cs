using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryShadowSystem.Configuration;
using TreasuryModel.Master;
using System.Web.UI.HtmlControls;
using System.IO;

namespace TreasuryShadowSystem.Maintain
{
    public partial class frm_CurrentPosition : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridViewFund.PageIndexChanging += new GridViewPageEventHandler(GridViewFund_PageIndexChanging);
                GridViewFund.RowEditing += new GridViewEditEventHandler(GridViewFund_RowEditing);
                GridViewFund.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewFund_RowCancelingEdit);
                GridViewFund.RowUpdating += new GridViewUpdateEventHandler(GridViewFund_RowUpdating);
                GridViewFund.Sorting += new GridViewSortEventHandler(GridViewFund_Sorting);
            }
            catch (Exception ex)
            {

            }
        }

        void GridViewFund_Sorting(object sender, GridViewSortEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                ViewState["SortExpression"] = e.SortExpression;
                ViewState["SortDirection"] = SetSortDirection(ViewState["SortDirection"].ToString());
                BindGrid();
            }
            catch (Exception ex)
            { 
            
            }
        }

        private string SetSortDirection(string _sortDirection)
        {
            if (_sortDirection == "ASC")
            {
                return "DESC";
            }
            else
            {
                return "ASC";
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
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "ReportDate";
                ViewState["SortDirection"] = "ASC";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!txtFromDate.Value.Trim().Equals("")) || (!txtToDate.Value.Trim().Equals("")))
                {
                    DataTable dt = Model_MS_CURRENTPOSITION.GetCurrentPostion(Configuration.Config.ConnectionString, txtFromDate.Value, txtToDate.Value);
                    ViewState["Data"] = dt;
                    BindGrid();
                }
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
                DataTable dt = (DataTable)ViewState["Data"];
                dt.DefaultView.Sort = ViewState["SortExpression"] + " " + ViewState["SortDirection"];
                GridViewFund.DataSource = dt;
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
                Model_MS_CURRENTPOSITION c = new Model_MS_CURRENTPOSITION();
                c.ReportDate = ReportDateBox.Value;
                c.CCY = CCYBox.Value.ToUpper();
                c.Product = ProductBox.Value.ToUpper();

                if(ActionBox.Value.Equals("Add")) {
                    if (ValidateDialog())
                    {
                        c.TodayBaseSpot = decimal.Parse(TDBaseSpotBox.Value);
                        c.TodayBaseForward = decimal.Parse(TDBaseForwardBox.Value);
                        c.SpotRate = decimal.Parse(SpotRateBox.Value);
                        if (Model_MS_CURRENTPOSITION.ActionCurrentPosition(Config.ConnectionString, "ADD", c))
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
                } else if (ActionBox.Value.Equals("EditBalance"))
                {
                    c.PositionID = int.Parse(PositionBox.Value);
                    c.TodayBaseSpot = decimal.Parse(TDBaseSpotBox.Value);
                    c.TodayBaseForward = decimal.Parse(TDBaseForwardBox.Value);
                    if (Model_MS_CURRENTPOSITION.ActionCurrentPosition(Config.ConnectionString, "EDIT", c))
                    {
                        MessageBox.Text = "บันทึกรายการเรียบร้อย";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else 
                    {
                        MessageBox.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณากรอกข้อมูลให้ถูกต้อง";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                } else if (ActionBox.Value.Equals("EditRate"))
                {
                    c.PositionID = int.Parse(PositionBox.Value);
                    c.SpotRate = decimal.Parse(SpotRateBox.Value);
                    if (Model_MS_CURRENTPOSITION.ActionCurrentPosition(Config.ConnectionString, "EDIT", c))
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
                //decimal fee;
                //Decimal.TryParse(AmountBox.Value, out fee);
                //if (fee == 0)
                //{
                //    MessageBox.Text = "Amount Invalid Data";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                //    return false;
                //}
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel(sender, e);
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["Data"];
            dt.DefaultView.Sort = ViewState["SortExpression"] + " " + ViewState["SortDirection"];

            GridExport.Visible = true;
            GridExport.AllowPaging = false;
            GridExport.DataSource = dt;
            GridExport.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Position" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            HtmlForm frm = new HtmlForm();

            frm.Attributes["runat"] = "server";
            frm.Controls.Add(GridExport);
            GridExport.GridLines = GridLines.Both;
            GridExport.HeaderStyle.Font.Bold = true;
            GridExport.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();      
        }
    }
}
