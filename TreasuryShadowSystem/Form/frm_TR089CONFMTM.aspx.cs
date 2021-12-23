using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryModel.Master;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;

using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR089CONFMTM : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR089CONFMTM));
        public DataTable dt = new DataTable();
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
                {
                    ViewState["Schedule"] = Model_MS_MTMSCHEDULE.getScheduleData(Config.ConnectionString);
                    BindFund();
                    BindTemplate();
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        void BindTemplate()
        {
            try
            {
                var ReportTemplate = from MTMReport e in Enum.GetValues(typeof(MTMReport))
                                     select new
                                     {
                                         Value = (int)e,
                                         Key = e.ToString()
                                     };
                TemplateBox.DataSource = ReportTemplate;
                TemplateBox.DataTextField = "Key";
                TemplateBox.DataValueField = "Value";
                TemplateBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            try
            {
                if (e.Row.Cells[2].Text.Trim().Equals("01"))
                    e.Row.Cells[2].Text = "Morning-Everyday";
                else if (e.Row.Cells[2].Text.Trim().Equals("02"))
                    e.Row.Cells[2].Text = "Morning-EOM";
                else if (e.Row.Cells[2].Text.Trim().Equals("03"))
                    e.Row.Cells[2].Text = "Evening-Everyday";
                else if (e.Row.Cells[2].Text.Trim().Equals("04"))
                    e.Row.Cells[2].Text = "Evening-Friday";
                else if (e.Row.Cells[2].Text.Trim().Equals("05"))
                    e.Row.Cells[2].Text = "Evening-EOM";

                if (e.Row.Cells[4].Text.Trim().Equals("1"))
                    e.Row.Cells[4].Text = "Open";
                else if (e.Row.Cells[4].Text.Trim().Equals("0"))
                    e.Row.Cells[4].Text = "Close";
                else if (e.Row.Cells[4].Text.Trim().Equals(""))
                    e.Row.Cells[4].Text = "N/A";

                if (e.Row.Cells[7].Text.Trim().Equals("01"))
                    e.Row.Cells[7].Text = "Morning";
                else if (e.Row.Cells[7].Text.Trim().Equals("02"))
                    e.Row.Cells[7].Text = "Evening";

                if (e.Row.Cells[8].Text.Trim().Equals("PDF"))
                    e.Row.Cells[8].Text = "PDF";
                else if (e.Row.Cells[8].Text.Trim().Equals("Excel"))
                    e.Row.Cells[8].Text = "Excel";

                if (e.Row.Cells[9].Text.Trim().Equals("1"))
                    e.Row.Cells[9].Text = "All";
                else if (e.Row.Cells[9].Text.Trim().Equals("0"))
                    e.Row.Cells[9].Text = "None";

                if (!e.Row.Cells[11].Text.Trim().Equals(""))
                {
                    int value;
                    int.TryParse(e.Row.Cells[11].Text.Trim(), out value);
                    if (value>0)
                        e.Row.Cells[11].Text = Enum.GetName(typeof(MTMReport), value);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                Model_MS_MTMSCHEDULE m = new Model_MS_MTMSCHEDULE();
                m.MTMScheduleID = int.Parse(key);
                if (m.DeleteMTMSchedule(Config.ConnectionString))
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
            
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindSchedule(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:600, modal: true,'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + Round.ClientID + "').val($('#" + RoundBox.ClientID + "').val());";
                    EditScript += "$('#" + MutualFund.ClientID + "').val($('#" + MutualFundBox.ClientID + "').val());";
                    EditScript += "$('#" + OpenClose.ClientID + "').val($('#" + OpenCloseBox.ClientID + "').val());";
                    EditScript += "$('#" + Source.ClientID + "').val($('#" + SourceBox.ClientID + "').val());";
                    EditScript += "$('#" + SpecialSource.ClientID + "').val($('#" + SpecialSourceBox.ClientID + "').val());";
                    EditScript += "$('#" + Rate.ClientID + "').val($('#" + RateBox.ClientID + "').val());";
                    EditScript += "$('#" + FileType.ClientID + "').val($('#" + FileTypeBox.ClientID + "').val());";
                    EditScript += "if($('#" + AllMDateBox.ClientID + "').is(':checked')) ";
                    EditScript += "{$('#" + AllMDate.ClientID + "').val('1');} ";
                    EditScript += "else ";
                    EditScript += "{$('#" + AllMDate.ClientID + "').val('0');} ";
                    EditScript += "$('#" + FileName.ClientID + "').val($('#" + FileNameBox.ClientID + "').val());";
                    EditScript += "$('#" + Template.ClientID + "').val($('#" + TemplateBox.ClientID + "').val());";
                    EditScript += "$('#" + Notes.ClientID + "').val($('#" + NotesBox.ClientID + "').val());";
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

        void BindSchedule(string id)
        {
            try
            {
                string key = GridViewFund.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = Model_MS_MTMSCHEDULE.getSchedule(Config.ConnectionString, int.Parse(key));
                MTMScheduleBox.Value = key;
                ActionBox.Value = "Edit";
                RoundBox.SelectedValue = dt.Rows[0]["Round"].ToString();
                MutualFundBox.Text = dt.Rows[0]["MutualFund"].ToString();
                OpenCloseBox.SelectedValue = dt.Rows[0]["OpenClose"].ToString();
                SourceBox.SelectedValue = dt.Rows[0]["Source"].ToString();
                SpecialSourceBox.Text = dt.Rows[0]["SpecialSource"].ToString();
                RateBox.SelectedValue = dt.Rows[0]["Rate"].ToString();
                FileTypeBox.SelectedValue = dt.Rows[0]["FileType"].ToString();
                if (dt.Rows[0]["AllMDate"].ToString().Equals("1"))
                    AllMDateBox.Checked = true;
                else
                    AllMDateBox.Checked = false;
                FileNameBox.Text = dt.Rows[0]["FileName"].ToString();
                TemplateBox.SelectedValue = dt.Rows[0]["Template"].ToString()=="" ? "1" : dt.Rows[0]["Template"].ToString();
                NotesBox.Text = dt.Rows[0]["Notes"].ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewFund_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
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
                GridViewFund.DataSource = Model_MS_MTMSCHEDULE.getScheduleData(Config.ConnectionString, MutualFundsSearchBox.SelectedValue.ToString(), RoundSearchBox.SelectedValue.ToString());
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
                Model_MS_MTMSCHEDULE m = new Model_MS_MTMSCHEDULE();
                m.Round = Round.Value;
                m.MutualFund = MutualFund.Value;
                m.OpenClose = OpenClose.Value;
                m.Source = Source.Value;
                m.SpecialSource = SpecialSource.Value;
                m.Rate = Rate.Value;
                m.FileType = FileType.Value;
                m.AllMdate = AllMDate.Value;
                m.FileName = FileName.Value;
                m.Template = Template.Value;
                m.Notes = Notes.Value;
                if (ValidateDialog(m))
                {
                    if (ActionBox.Value.Equals("Edit"))
                    {
                        m.MTMScheduleID = int.Parse(MTMScheduleBox.Value);

                        if (m.UpdateMTMSchedule(Config.ConnectionString))
                        {
                            MessageBox.Text = "บันทึกรายการเรียบร้อย";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);                            
                        }
                        else
                        {
                            MessageBox.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบข้อมูล";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                    } else {
                        //if (!Model_MS_MTMSCHEDULE.existsFunds(Config.ConnectionString, m.MutualFund, m.FileType))
                        //{
                            if (m.AddMTMSchedule(Config.ConnectionString))
                            {
                                MessageBox.Text = "บันทึกรายการเรียบร้อย";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                            }
                            else
                            {
                                MessageBox.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณากรอกข้อมูลให้ถูกต้อง";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                            }
                        //}
                        //else {
                        //    MessageBox.Text = "มีรายการกองทุนนี้แล้วในระบบ กรุณาตรวจสอบข้อมูล";
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        //}
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


        private Boolean ValidateDialog(Model_MS_MTMSCHEDULE m)
        {
            try
            {
                if (m.MutualFund.Trim().Equals(""))
                {
                    MessageBox.Text = "Fund is require!!";
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

        private void BindFund()
        {
            try
            {
                dt = (DataTable)ViewState["Schedule"];
                string condition = "Round='" + RoundSearchBox.SelectedValue + "'";
                DataTable t = dt.Select(condition).AsEnumerable().CopyToDataTable();
                MutualFundsSearchBox.Items.Clear();
                MutualFundsSearchBox.DataSource = t.DefaultView.ToTable(true, "MUTUALFUND");
                MutualFundsSearchBox.DataTextField = "MUTUALFUND";
                MutualFundsSearchBox.DataValueField = "MUTUALFUND";
                MutualFundsSearchBox.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MutualFundsSearchBox.Items.Clear();
            }
            finally
            {
                MutualFundsSearchBox.Items.Insert(0, new ListItem("All", ""));
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

        protected void RoundSearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindFund();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
