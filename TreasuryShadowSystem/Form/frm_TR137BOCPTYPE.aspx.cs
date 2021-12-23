using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using System.Web.UI.HtmlControls;
using System.IO;
using log4net;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR137BOCPTYPE : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR137BOCPTYPE));
        protected void Page_Load(object sender, EventArgs e)
        {
            GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);
        }

        void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridData.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["ACUPData"];

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
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindGrid()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["ACUPData"];
                GridData.DataSource = dt;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Model_RPT_TR137BOCPTYPE.getDataSet(Config.ConnectionString, ThirdPartyTypeBox.Value, FromDateBox.Text, ToDateBox.Text, OtherBox.Value.Trim());
                if (dt.Rows.Count > 0)
                {
                    ViewState["ACUPData"] = dt;
                    BindGrid();
                }
                else if (dt != null)
                {
                    MessageBox.Text = "ระบบทำรายการเรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถทำรายการได้";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = "ไม่สามารถทำรายการได้ " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel(sender, e);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
