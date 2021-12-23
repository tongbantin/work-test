using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using TreasuryShadowSystem.Configuration;
using System.Data.OleDb;
using System.Data;
using KKB.Treasury.TreasuryModel.Model;
using System.Collections;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR135BOTBPUPD : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR135BOTBPUPD));
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
            
            try
            {
                if (error != null)
                {
                    if (error.Count > 0)
                    {
                        if (error.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                            e.Row.BackColor = System.Drawing.Color.Red;
                    }
                }
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
                List<Model_RPT_TR135BOTBP> l = new List<Model_RPT_TR135BOTBP>();
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
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Details$] WHERE DataSetDate is not null Order By Month ", _oleConn);
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

                            ViewState["DataTableFile"] = dt;

                            //    /*
                            //    SeqStart = Model_MS_PTITRANS.getNextSequence(Config.ConnectionString, dt.Rows[0]["RelatedRef"].ToString());

                            Session["TBP"] = null;

                            //    FolderName = dt.Rows[0]["RelatedRef"].ToString() + "_" + DateTime.Now.ToString("yyyyMMdd");
                            //    DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));

                            int j = 1;
                            Model_RPT_TR135BOTBP md = new Model_RPT_TR135BOTBP();
                            md.TBPID = j;
                            string data = Convert.ToDateTime(dt.Rows[0]["DataSetDate"].ToString()).ToString("dd/MM/yyyy");
                            int last = DateTime.DaysInMonth(int.Parse(data.Substring(6, 4)), int.Parse(dt.Rows[0]["Month"].ToString()));
                            DateTime lastday = new DateTime(int.Parse(data.Substring(6, 4)), int.Parse(dt.Rows[0]["Month"].ToString()), last);
                            md.MonthYear = lastday.ToString("dd/MM/yyyy");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {
                                    string bookno = dt.Rows[i]["TradingBookPosition"].ToString();
                                    switch (bookno)
                                    { 
                                        case "970003" :
                                            md.C970003 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970004":
                                            md.C970004 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970005":
                                            md.C970005 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970006":
                                            md.C970006 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970007":
                                            md.C970007 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970008":
                                            md.C970008 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970009":
                                            md.C970009 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970010":
                                            md.C970010 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970011":
                                            md.C970011 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                        case "970012":
                                            md.C970012 = decimal.Parse(dt.Rows[i]["OutstandingAmount"].ToString());
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                    Log.Error(ex.Message);
                                }

                                if (dt.Rows.Count - 1 > i)
                                {
                                    if (!dt.Rows[i]["Month"].ToString().Equals(dt.Rows[i + 1]["Month"].ToString()))
                                    {
                                        l.Add(md);
                                        md = new Model_RPT_TR135BOTBP();
                                        md.TBPID = j;
                                        string datenext = Convert.ToDateTime(dt.Rows[i+1]["DataSetDate"].ToString()).ToString("dd/MM/yyyy");
                                        int inlast = DateTime.DaysInMonth(int.Parse(datenext.Substring(6, 4)), int.Parse(dt.Rows[i + 1]["Month"].ToString()));
                                        DateTime inlastday = new DateTime(int.Parse(datenext.Substring(6, 4)), int.Parse(dt.Rows[i + 1]["Month"].ToString()), inlast);
                                        md.MonthYear = inlastday.ToString("dd/MM/yyyy");
                                        j++;
                                    }
                                }
                            }
                            l.Add(md);
                            BindGrid();

                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
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
                            Session["TBP"] = l;
                            btnSave.Visible = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Text = "ประเภทไฟล์ไม่ถูกต้อง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }

                //ViewState["PTIXML"] = PTI.ExcelToXML(l);                
                //lblXML.Text = PTI.ExcelToXML();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<Model_RPT_TR135BOTBP> l = (List<Model_RPT_TR135BOTBP>)Session["TBP"];

                Model_RPT_TR135BOTBP pt = new Model_RPT_TR135BOTBP();
                error = pt.save(Configuration.Config.ConnectionString, l);
                if (error.Count <= 0)
                {
                    BindGrid();
                    MessageBox.Text = "บันทึกข้อมูลเรียบร้อยแล้ว";
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
            finally
            {
                if (error.Count <= 0)
                {
                    btnSave.Visible = false;
                    ViewState["DataTableFile"] = null;
                    Session["TBP"] = null;

                    ViewState.Remove("DataTableFile");
                    Session.Remove("TBP");
                }
            }
        }

        void BindGrid()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["DataTableFile"];
                GridData.DataSource = dt;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
            }

        }
    }
}
