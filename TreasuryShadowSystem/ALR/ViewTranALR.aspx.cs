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
using TreasuryModel.Data;

namespace TreasuryShadowSystem.ALR
{
    public partial class ViewTranALR : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(ViewTranALR));
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
                /*
                DataTable dt = (DataTable)ViewState["ALRData"];

                GridExport.Visible = true;
                GridExport.AllowPaging = false;
                GridExport.DataSource = dt;
                GridExport.DataBind();

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "TransactionALR" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
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
                 */

                
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment;filename=TransactionALR.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridData.AllowPaging = false;
                    this.BindGrid();

                    GridData.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
                

               /* 
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "TransactionALR.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                GridData.GridLines = GridLines.Both;
                GridData.HeaderStyle.Font.Bold = true;
                GridData.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.Flush();
                //Response.End();      
                */ 
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
                DataTable dt = (DataTable)ViewState["ALRData"];
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
                DataTable dt = TransToALR.getViewTrans(Config.ConnectionString,FromDateBox.Text);

                if (dt != null) {

                    if (dt.Rows.Count > 0)
                    {
                        ViewState["ALRData"] = dt;
                        BindGrid();

                        //MessageBox.Text = "ระบบทำรายการเรียบร้อยแล้ว";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }        
                }else{
                    MessageBox.Text = "ไม่พบรายการที่ค้นหา";
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
    }
}
