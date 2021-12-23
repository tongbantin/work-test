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
using KKB.Treasury.TreasuryModel.Model;
using log4net;
using System.IO;
using TreasuryShadowSystem.Configuration;
using System.Web.Services;
using TreasuryModel.Data;
using TreasuryShadowSystem.Model;
using TreasuryModel.Master;
using System.Text;

namespace TreasuryShadowSystem.ForeignExchange
{
    public partial class frm_PositionView : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_PositionView));
        private string SpotForward = "";
        private string Product = "";
        private int rowadd = 0;
        private DataTable dataDetail = new DataTable();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridFXSpot.RowDataBound += new GridViewRowEventHandler(GridFXSpot_RowDataBound);
                GridFXSpot.RowCreated += new GridViewRowEventHandler(GridFXSpot_RowCreated);
                
                GridFXForward.RowDataBound += new GridViewRowEventHandler(GridFXForward_RowDataBound);
                GridFXForward.RowCreated += new GridViewRowEventHandler(GridFXForward_RowCreated);

                GridFXSpotSummary.RowCreated += new GridViewRowEventHandler(GridFXSpotSummary_RowCreated);
                GridFXSpotSummary.RowDataBound += new GridViewRowEventHandler(GridFXSpotSummary_RowDataBound);

                GridFXForwardSummary.RowCreated += new GridViewRowEventHandler(GridFXForwardSummary_RowCreated);
                GridFXForwardSummary.RowDataBound += new GridViewRowEventHandler(GridFXForwardSummary_RowDataBound);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXForwardSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string PS = e.Row.Cells[3].Text.Trim();
                    if (PS == "0")
                        e.Row.Cells[4].Text = "Forward Position-Sell";
                    else if (PS == "1")
                        e.Row.Cells[4].Text = "Forward Position-Buy";
                    else
                        e.Row.Cells[4].Text = "Forward Position-Summary";
                    for (int i = 5; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }

                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXSpotSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string PS = e.Row.Cells[3].Text.Trim();
                    if (PS == "0")
                        e.Row.Cells[4].Text = "Spot Position-Sell";
                    else if (PS == "1")
                        e.Row.Cells[4].Text = "Spot Position-Buy";
                    else
                        e.Row.Cells[4].Text = "Spot Position-Summary";
                    for (int i = 5; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXForwardSummary_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[4].Text = "Proc.";
                    e.Row.Cells[4].Width = 300;
                    for (int i = 5; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Width = 150;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXSpotSummary_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[4].Text = "Proc.";
                    e.Row.Cells[4].Width = 300;
                    for (int i = 5; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Width = 150;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXForward_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[5].Width = 250;
                    for (int i = 6; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Width = 150;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXSpot_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[5].Width = 250;
                    for (int i = 6; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Width = 150;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXForward_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[5].Text = "";
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[5].Text = "Forward-" + e.Row.Cells[3].Text.Trim();
                    string SpotFwdInd = e.Row.Cells[2].Text.Trim();
                    string Product = e.Row.Cells[3].Text.Trim();
                    GridView gd = e.Row.FindControl("GridFXForwardDetail") as GridView;
                    gd.RowCreated += new GridViewRowEventHandler(gd_RowCreated);
                    gd.RowDataBound += new GridViewRowEventHandler(gd_RowDataBound);
                    BindDetail(gd, SpotFwdInd, Product);

                    for (int i = 6; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridFXSpot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[5].Text = "";
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[5].Text = "Spot-" + e.Row.Cells[3].Text.Trim();
                    string SpotFwdInd = e.Row.Cells[2].Text.Trim();
                    string Product = e.Row.Cells[3].Text.Trim();
                    GridView gd = e.Row.FindControl("GridFXSpotDetail") as GridView;
                    gd.RowCreated += new GridViewRowEventHandler(gd_RowCreated);
                    gd.RowDataBound += new GridViewRowEventHandler(gd_RowDataBound);
                    BindDetail(gd, SpotFwdInd, Product);

                    for (int i = 6; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void gd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 6; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void gd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[5].Width = 190;
                    e.Row.Cells[5].Text = "";
                    for (int i = 6; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Width = 150;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        void BindDetail(GridView gd, string SpotFwdInd, string Product)
        {
            try
            {
                DataRow[] drDetail = dataDetail.Select("SPOTFWDIND='" + SpotFwdInd + "' AND TRIM(PRODUCT)='" + Product + "'");
                if (drDetail.Length > 0)
                {
                    DataTable dt = drDetail.CopyToDataTable();
                    gd.DataSource = dt;
                    gd.DataBind();
                }
                else 
                {
                    gd.DataSource = null;
                    gd.DataBind();
                }
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
                DataTable dt = new DataTable();
                dt = Model_RPT_TR097BOPODETAIL.getData(Config.ConnectionString, DataDateSearchBox.Text, "Summary");
                dataDetail = Model_RPT_TR097BOPODETAIL.getDetail(Config.ConnectionString, DataDateSearchBox.Text, "", "");

                DataRow[] drSpot = dt.Select("SPOTFWDIND='S'");
                if (drSpot.Length > 0)
                {
                    DataTable dtSpot = drSpot.CopyToDataTable();
                    dtSpot.DefaultView.Sort = "SPOTFWDIND DESC, PRODUCT, DEALNO";
                    GridFXSpot.DataSource = dtSpot;
                    GridFXSpot.DataBind();
                }
                else {
                    GridFXSpot.DataSource = null;
                    GridFXSpot.DataBind();                
                }

                DataRow[] drForward = dt.Select("SPOTFWDIND='F'");
                if (drForward.Length > 0)
                {
                    DataTable dtForward = drForward.CopyToDataTable();
                    dtForward.DefaultView.Sort = "SPOTFWDIND DESC, PRODUCT, DEALNO";
                    GridFXForward.DataSource = dtForward;
                    GridFXForward.DataBind();
                }
                else
                {
                    GridFXForward.DataSource = null;
                    GridFXForward.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        void BindSummary() {
            try
            {
                DataTable dt = new DataTable();
                dt = Model_RPT_TR097BOPODETAIL.getData(Config.ConnectionString, DataDateSearchBox.Text, "Total");

                DataRow [] drSpot = dt.Select("SPOTFWDIND='S'");
                if (drSpot.Length >0) {
                    DataTable dtSpot = drSpot.CopyToDataTable();
                    dtSpot.DefaultView.Sort = "PS";
                    GridFXSpotSummary.DataSource = dtSpot;
                    GridFXSpotSummary.DataBind();
                }
                else
                {
                    GridFXSpotSummary.DataSource = null;
                    GridFXSpotSummary.DataBind();
                }

                DataRow[] drForward = dt.Select("SPOTFWDIND='F'");
                if (drForward.Length>0)
                {
                    DataTable dtForward = drForward.CopyToDataTable();
                    dtForward.DefaultView.Sort = "PS";
                    GridFXForwardSummary.DataSource = dtForward;
                    GridFXForwardSummary.DataBind();
                }
                else
                {
                    GridFXForwardSummary.DataSource = null;
                    GridFXForwardSummary.DataBind();
                }
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
                BindSummary();
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        [WebMethod(EnableSession = true)]
        public static object GetPositionDetail(int DETAILID)
        {
            return PositionDetail.getPositionDetail(Config.ConnectionString, DETAILID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddPositionDetail(PositionDetail record)
        {
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return PositionDetail.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdatePositionDetail(PositionDetail record)
        {
            record.UPDATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return PositionDetail.Update(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeletePositionDetail(int DETAILID)
        {
            return PositionDetail.Delete(Config.ConnectionString, DETAILID);
        }


        protected void ReGenBOTFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                BOTFXFile box = new BOTFXFile();
                box.ConnectionString = Config.ConnectionString;
                box.XMLPath = Server.MapPath("~/xml/");
                box.ExportPath = Config.GetParameterFile("097ExportPath");
                box.TemplateFile = Server.MapPath("~/Template/Template.xls");
                box.Frequency = ReportTypeBox.SelectedValue;
                string FileName = "BOTReport_" + box.Frequency + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                box.ExportFileName = Server.MapPath("~/" + box.ExportPath + "/" + FileName);
                box.FromDate = ExecuteFromDateBox.Value;
                box.ToDate = ExecuteToDateBox.Value;
                Boolean result = box.GenerateFile();
                
                if (result)
                {
                    DownloadFile(FileName, box.ExportFileName);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถสร้างไฟล์ได้ กรุณาตรวจสอบข้อมูล และทำรายการใหม่";
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

        protected void DownloadFile(string filename, string fullpathfile)
        {
            try
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                //response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ";");
                response.TransmitFile(fullpathfile);
                response.Flush();
                response.End();
            }
            catch (Exception ex) {
                Log.Error(ex.Message);
            }
        }
        
        protected void ReImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean result = PositionDetail.ReImportPositionDetail(Config.ConnectionString, ExecuteDateBox.Value);
                if (result)
                {
                    MessageBox.Text = "ระบบ Rerun ข้อมูลเรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถ Rerun ได้ กรุณาตรวจสอบข้อมูล และทำรายการใหม่";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindExport()
        {
            try
            {
                DataTable dtFX = new DataTable();

                DataTable dtSummary = new DataTable();
                dtSummary = Model_RPT_TR097BOPODETAIL.getData(Config.ConnectionString, DataDateSearchBox.Text, "Total");

                DataTable dtSpot = dtSummary.Select("SPOTFWDIND='S'").CopyToDataTable();
                dtSpot.DefaultView.Sort = "PS";

                dtFX.Merge(dtSpot);

                DataTable dtForward = dtSummary.Select("SPOTFWDIND='F'").CopyToDataTable();
                dtForward.DefaultView.Sort = "PS";

                dtFX.Merge(dtForward);

                GridFX.Visible = true;
                GridFX.DataSource = dtFX;
                GridFX.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void ExportToExcelButton_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dataSummary = Model_RPT_TR097BOPODETAIL.getData(Config.ConnectionString, DataDateSearchBox.Text, "Summary");
                dataDetail = Model_RPT_TR097BOPODETAIL.getDetail(Config.ConnectionString, DataDateSearchBox.Text, "", "");

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=FXPosition" + DataDateSearchBox.Text.Replace("/","") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        //GridFX.RenderControl(hw);
                        Page.Form.Controls.Clear();

                        DataRow[] drSpot = dataSummary.Select("SPOTFWDIND='S'");
                        if (drSpot.Length > 0)
                        {
                            GridFXSpotSummary.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                            GridFXSpotSummary.HeaderStyle.ForeColor = System.Drawing.Color.White;
                            GridFXSpotSummary.HeaderStyle.Font.Bold = true;
                            Page.Form.Controls.Add(GridFXSpotSummary);
                            DataTable dtSpot = drSpot.CopyToDataTable();
                            DataTable dtSpotDetail = dataDetail.Select("SPOTFWDIND='S'").CopyToDataTable();
                            dtSpot.Merge(dtSpotDetail, true, MissingSchemaAction.Ignore);
                            dtSpot.DefaultView.Sort = "SPOTFWDIND DESC, PRODUCT, PS, DEALNO DESC";
                            GridView gdSpotDetail = new GridView();
                            gdSpotDetail.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                            gdSpotDetail.HeaderStyle.ForeColor = System.Drawing.Color.White;
                            gdSpotDetail.HeaderStyle.Font.Bold = true;
                            gdSpotDetail.RowDataBound += new GridViewRowEventHandler(gdDetail_RowDataBound);
                            gdSpotDetail.DataSource = dtSpot;
                            gdSpotDetail.DataBind();
                            Page.Form.Controls.Add(gdSpotDetail);
                        }

                        DataRow[] drForward = dataSummary.Select("SPOTFWDIND='F'");
                        if (drForward.Length > 0)
                        {
                            GridFXForwardSummary.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                            GridFXForwardSummary.HeaderStyle.ForeColor = System.Drawing.Color.White;
                            GridFXForwardSummary.HeaderStyle.Font.Bold = true;
                            Page.Form.Controls.Add(GridFXForwardSummary);
                            DataTable dtForward = drForward.CopyToDataTable();
                            DataTable dtForwardDetail = dataDetail.Select("SPOTFWDIND='F'").CopyToDataTable();
                            dtForward.Merge(dtForwardDetail, true, MissingSchemaAction.Ignore);
                            dtForward.DefaultView.Sort = "SPOTFWDIND DESC, PRODUCT, PS, DEALNO DESC";
                            GridView gdForwardDetail = new GridView();
                            gdForwardDetail.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                            gdForwardDetail.HeaderStyle.ForeColor = System.Drawing.Color.White;
                            gdForwardDetail.HeaderStyle.Font.Bold = true;
                            gdForwardDetail.RowDataBound += new GridViewRowEventHandler(gdDetail_RowDataBound);
                            gdForwardDetail.DataSource = dtForward;
                            gdForwardDetail.DataBind();
                            Page.Form.Controls.Add(gdForwardDetail);
                        }
                        Page.Form.RenderControl(hw);

                        //style to format numbers to string
                        Response.Output.Write(sw.ToString());
                        // Response.Flush();
                        Response.End();
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void gdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[0].Text.Replace("&nbsp;", "").Trim() == "")
                        e.Row.BackColor = System.Drawing.Color.CadetBlue;
                }
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
