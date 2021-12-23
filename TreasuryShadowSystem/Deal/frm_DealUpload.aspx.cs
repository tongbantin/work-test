using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using TreasuryModel.Data;
using log4net;
using System.Drawing;
namespace TreasuryShadowSystem.Form
{
    public partial class frm_DealUpload : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_DealUpload));
        private ArrayList error = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridData.RowDataBound += new GridViewRowEventHandler(GridData_RowDataBound);
            }
            catch (Exception ex)
            { 
            
            }
        }

        void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Row.Cells[14].Text == "True")
                    e.Row.BackColor = Color.Orange;
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                List<Deal> l = new List<Deal>();
                if ((MyFile.PostedFile.ContentType.Equals("application/octet-stream")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    MyFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=Yes;\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Sheet1$] WHERE PRODTYPE is not null ", _oleConn);
                            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                            oleAdapter.SelectCommand = _oleCmdSelect;
                            DataTable dt = new DataTable();
                            DataColumn dc = new DataColumn("ID", typeof(int));
                            dc.AutoIncrement = true;
                            dc.AutoIncrementSeed = 1;
                            dc.AutoIncrementStep = 1;
                            dt.Columns.Add(dc);

                            oleAdapter.FillSchema(dt, SchemaType.Source);
                            oleAdapter.Fill(dt);

                            ViewState["DealTableFile"] = dt; //Get Data From Excel

                            //Get Existing Dealno
                            DataTable dtDealID = Deal.GetDealID(Configuration.Config.ConnectionString);
                            
                            Session["DealWithout"] = null;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Deal md = new Deal();
                                try
                                {
                                    md.Records = i + 1;
                                    md.DataDate = DateTime.Parse(dt.Rows[i]["DATADATE"].ToString()).ToString("dd/MM/yyyy");
                                    md.Bu = dt.Rows[i]["BU"].ToString().Trim();
                                    md.DealID = dt.Rows[i]["DEALID"].ToString().Trim();
                                    md.PS = dt.Rows[i]["PS"].ToString().Trim();
                                    md.ProdType = dt.Rows[i]["PRODTYPE"].ToString().Trim();
                                    md.CCYPair = dt.Rows[i]["CCYPAIR"].ToString().Trim();
                                    md.CCY = dt.Rows[i]["CCY"].ToString().Trim();
                                    if (!dt.Rows[i]["NOTIONAL"].ToString().Trim().Equals(""))
                                        md.Notional = decimal.Parse(dt.Rows[i]["NOTIONAL"].ToString().Trim());
                                    else
                                        md.Notional = 0;
                                    md.CMNE = dt.Rows[i]["COUNTERPARTY"].ToString().Trim();
                                    try
                                    {
                                        md.FlowDate = DateTime.Parse(dt.Rows[i]["FLOWDATE"].ToString()).ToString("dd/MM/yyyy");
                                    }
                                    catch (Exception ex)
                                    {
                                        md.FlowDate = "01/01/1999";
                                    }
                                    if (!dt.Rows[i]["FLOWAMOUNT"].ToString().Trim().Equals(""))
                                        md.FlowAmount = decimal.Parse(dt.Rows[i]["FLOWAMOUNT"].ToString().Trim());
                                    else
                                        md.FlowAmount = 0;

                                    DataRow[] dr = dtDealID.Select("DEALID = '" + dt.Rows[i]["DEALID"].ToString() + "'");
                                    if (!dt.Rows[i]["DEALID"].ToString().Equals(""))
                                    {
                                        if (dr.Length != 0)
                                            md.IsExisting = true;
                                        else
                                            md.IsExisting = false;
                                    }
                                    else
                                        md.IsExisting = false;

                                }
                                catch (Exception ex)
                                {
                                    error.Add(int.Parse(dt.Rows[i]["DEALIDID"].ToString()));
                                    Log.Error(ex.Message);
                                }
                                l.Add(md);
                            }
                            Session["DealWithout"] = l;
                            BindGrid();

                        }
                        catch (Exception ex)
                        {
                            error.Add(0);
                        }
                        finally
                        {
                            _oleConn.Close();
                        }

                        if (error.Count > 0)
                        {
                            btnSave.Visible = false;
                            MessageBox.Text = "เกิดข้อผิดพลาด ข้อมูลไม่ถูกต้อง";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        else
                        {
                            btnSave.Visible = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Text = "ประเภทไฟล์ไม่ถูกต้อง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {

            }
        }


        void BindGrid()
        {
            try
            {
                List<Deal> l = (List<Deal>)Session["DealWithout"];
                GridData.DataSource = l;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<Deal> l = (List<Deal>)Session["DealWithout"];
                Deal d = new Deal();
                error = d.save(Configuration.Config.ConnectionString, l);
                if (error.Count <= 0)
                {
                    BindGrid();
                    MessageBox.Text = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    btnSave.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถบันทึกบางรายการได้";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
            
            }
        }
    }
}
