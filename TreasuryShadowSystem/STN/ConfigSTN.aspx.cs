using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;

using System.Configuration;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using KKB.Treasury.TreasuryModel.Model;
using System.Data;
using log4net;
using System.Data.OleDb;

namespace TreasuryShadowSystem.STN
{
    public partial class ConfigSTN : System.Web.UI.Page
    {

        private static ILog Log = log4net.LogManager.GetLogger(typeof(ConfigSTN));

        protected void Page_Load(object sender, EventArgs e)
        { 
            GridViewConfig.PageIndexChanging += new GridViewPageEventHandler(GridViewConfig_PageIndexChanging);
            GridViewConfig.RowEditing += new GridViewEditEventHandler(GridViewConfig_RowEditing);
            GridViewConfig.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewConfig_RowCancelingEdit);
            GridViewConfig.RowCommand += new GridViewCommandEventHandler(GridViewConfig_RowCommand);
            GridViewConfig.RowDeleting += new GridViewDeleteEventHandler(GridViewConfig_RowDeleting);

            if (!IsPostBack)
            {
                BindGridConfig();
                BindProductddl();
            }

        }


        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindGridConfig();

        }

        void GridViewConfig_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        void GridViewConfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GridViewConfig.PageIndex = e.NewPageIndex;
                BindGridConfig();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        void GridViewConfig_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            try
            {
                GridViewConfig.EditIndex = -1;
                BindGridConfig();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewConfig_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                GridViewConfig.EditIndex = e.NewEditIndex;
                BindGridConfig();
                BindProductddl();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        void GridViewConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    BindConfig(e.CommandArgument.ToString());
                    string EditScript = "$('#dialog').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:1050, modal: true ";
                    EditScript += ", open: function(){ } ";
                    EditScript += ",'buttons': {'Submit': function() {";
                    EditScript += "$('#" + ActionBox.ClientID + "').val('Edit');";
                    EditScript += "$('#" + hdstntype.ClientID + "').val($('#" + txtstntype.ClientID + "').val());";
                    EditScript += "$('#" + hdstntypeshort.ClientID + "').val($('#" + txtstntypeshort.ClientID + "').val());";
                    EditScript += "$('#" + hdstnname.ClientID + "').val($('#" + txtstnname.ClientID + "').val());";
                    EditScript += "$('#" + hdstndesc.ClientID + "').val($('#" + txtstndesc.ClientID + "').val());";
                    EditScript += "$('#" + hdstnreturn.ClientID + "').val($('#" + txtstnreturn.ClientID + "').val());";
                    EditScript += "$('#" + hdstnredeem.ClientID + "').val($('#" + txtstnredeem.ClientID + "').val());";
                    EditScript += "$('input#" + DialogSubmit.ClientID + "').click();";
                    EditScript += "},'Close': function() {$(this).dialog('close');}}}); ";

                    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", EditScript, true);
                }
                else if (e.CommandName.Equals("Delete"))
                {


                    Delete(e.CommandArgument.ToString());

                }
                else if (e.CommandName.Equals("DETAIL"))
                {
                    //string docid = e.CommandArgument.ToString();
                    //DataTable dt = new DataTable();
                    //dt = STNCONFIG.getConfigByID(docid);

                    BindPreview(e.CommandArgument.ToString());
                    string PreviewScript = "$('#dialogdetail').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', width:1050, modal: true ";
                    PreviewScript += ", open: function(){ } ";
                    PreviewScript += ",'buttons': {'Close': function() {$(this).dialog('close');}}}); ";

                    ClientScript.RegisterStartupScript(GetType(), "Kiatnakin Bank", PreviewScript, true);
                     

                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindGridConfig()
        {

            try
            {
               
                List<STNCONFIG> ConfigList = new List<STNCONFIG>();


                string type = productddl.SelectedValue;
                DataTable dt = new DataTable();
                dt = STNCONFIG.Search(type);

                ConfigList = STNCONFIG.ConvertDataTableToListConfig(dt); 

                GridViewConfig.DataSource = ConfigList;
                GridViewConfig.DataBind(); 

            }
            catch (Exception ex) { }
        }
        private void BindProductddl()
        {

            try
            {

                List<STNCONFIG> ConfigList = new List<STNCONFIG>();
                 
                DataTable dt = new DataTable();
                dt = STNCONFIG.Search("");

                ConfigList = STNCONFIG.ConvertDataTableToListConfig(dt);

                productddl.Items.Clear();

                productddl.Items.Insert(0, "");
                productddl.DataSource = ConfigList;
                productddl.DataValueField = "STN_TYPE";
                productddl.DataTextField = "STN_TYPE_FULLNAME";
                productddl.DataBind();

                 
            }
            catch (Exception ex) { }
        }

        private void BindConfig(string id)
        {
            try
            {
                string key = GridViewConfig.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = STNCONFIG.getConfigByID(key);
                hdDOCID.Value = key;
                ActionBox.Value = "Edit";

                txtstntypeshort.Text = dt.Rows[0]["STN_TYPE"].ToString().Trim();
                txtstntype.Text = dt.Rows[0]["STN_TYPE_FULLNAME"].ToString().Trim();
                txtstnname.Text = dt.Rows[0]["STN_NAME"].ToString().Trim();
                txtstndesc.Text = dt.Rows[0]["STN_TYPE_DESC"].ToString().Trim();
                txtstnreturn.Text = dt.Rows[0]["STN_RETURN"].ToString().Trim();
                txtstnredeem.Text = dt.Rows[0]["STN_RULE_REDEEM"].ToString().Trim();

  
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindPreview(string id)
        {
            try
            {
                //string key = GridViewConfig.DataKeys[int.Parse(id)].Value.ToString();
                DataTable dt = STNCONFIG.getConfigByID(id);
                hdDOCID.Value = id;
                ActionBox.Value = "Preview";

                lblstntypeshort.Text = dt.Rows[0]["STN_TYPE"].ToString().Trim();
                lblstntype.Text = dt.Rows[0]["STN_TYPE_FULLNAME"].ToString().Trim();
                lblstnname.Text = dt.Rows[0]["STN_NAME"].ToString().Trim();
                lblstndesc.Text = dt.Rows[0]["STN_TYPE_DESC"].ToString().Trim();
                lblstnreturn.Text = dt.Rows[0]["STN_RETURN"].ToString().Trim();
                lblstnredeem.Text = dt.Rows[0]["STN_RULE_REDEEM"].ToString().Trim();


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        

        protected void Delete(string key)
        {
            try
            {
                STNCONFIG cf = new STNCONFIG();
                cf.DOCID = int.Parse(key);
                if (cf.Delete())
                {
                    MessageBox.Text = "ลบรายการเรียบร้อย";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบข้อมูล";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }

                BindGridConfig();
                BindProductddl();
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

                STNCONFIG config = new STNCONFIG();

                if (ActionBox.Value.Equals("Edit"))
                {
                    config.DOCID = int.Parse(hdDOCID.Value);
                    config.STN_TYPE = hdstntypeshort.Value;
                    config.STN_TYPE_FULLNAME = hdstntype.Value; 
                    config.STN_NAME = hdstnname.Value;
                    config.STN_TYPE_DESC = hdstndesc.Value;
                    config.STN_RETURN = hdstnreturn.Value;
                    config.STN_RULE_REDEEM = hdstnredeem.Value;

                    config.UPDATE_BY = HttpContext.Current.Session["UserName"].ToString();

                    bool issuccess = false;
                    issuccess = STNCONFIG.UpdateConfig(config);

                    if (issuccess)
                    {
                        BindGridConfig();
                        MessageBox.Text = "แก้ไขข้อมูลเรียบร้อยแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else
                    {
                        MessageBox.Text = "ระบบไม่สามารถแก้ไขข้อมูลได้ กรุณาทำรายการใหม่";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }

                }
                else
                {
                    bool havetype = false;

                    config.STN_TYPE = hdstntypeshort.Value;
                    config.STN_TYPE_FULLNAME = hdstntype.Value;
                    config.STN_NAME = hdstnname.Value;
                    config.STN_TYPE_DESC = hdstndesc.Value;
                    config.STN_RETURN = hdstnreturn.Value;
                    config.STN_RULE_REDEEM = hdstnredeem.Value;
                    config.CREATE_BY = HttpContext.Current.Session["UserName"].ToString();

                    havetype = STNCONFIG.CheckSTNType(config.STN_TYPE);

                    if (!havetype)
                    {

                        bool issuccess = false;
                        issuccess = STNCONFIG.AddConfig(config);

                        if (issuccess)
                        {
                            BindGridConfig();
                            MessageBox.Text = "ระบบนำเข้าข้อมูลเรียบร้อยแล้ว";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        else
                        {
                            MessageBox.Text = "ระบบไม่สามารถนำเข้าข้อมูลได้ กรุณาทำรายการใหม่";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }

                    }
                    else
                    {

                        MessageBox.Text = "มีข้อมูลประเภทหุ้นกู้นี้ในระบบแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                }




            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = "เกิดข้อผิดพลาด " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }

        }

       

    }
}
