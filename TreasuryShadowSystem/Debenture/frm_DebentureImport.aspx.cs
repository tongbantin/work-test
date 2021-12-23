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
using TreasuryModel.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_DebentureImport : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_DebentureImport));
        private ArrayList error, dealnotfound, custidnotfound, secidexists, warning;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridData.RowDataBound += new GridViewRowEventHandler(GridData_RowDataBound);
                error = new ArrayList();
                dealnotfound = new ArrayList();
                custidnotfound = new ArrayList();
                secidexists = new ArrayList();
                warning = new ArrayList();
                btnSave.Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.White;
                    e.Row.Cells[15].BackColor = System.Drawing.Color.Gold;
                }
                if (warning != null)
                {
                    if (warning.Count > 0)
                    {
                        if (warning.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                        {
                            e.Row.Cells[13].BackColor = System.Drawing.Color.Orange;
                        }
                    }
                }

                if (error != null)
                {
                    if (error.Count > 0)
                    {
                        if (error.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                        {
                            e.Row.Cells[13].BackColor = System.Drawing.Color.Orange;
                        }
                    }
                }

                if (dealnotfound != null)
                {
                    if (dealnotfound.Count > 0)
                    {
                        if (dealnotfound.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                        {
                            e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }

                if (custidnotfound != null)
                {
                    if (custidnotfound.Count > 0)
                    {
                        if (custidnotfound.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                        {
                            e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[15].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }

                if (secidexists != null)
                {
                    if (secidexists.Count > 0)
                    {
                        if (secidexists.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                        {
                            e.Row.Cells[2].BackColor = System.Drawing.Color.DarkOrange;
                        }
                    }
                }

                if (error != null)
                {
                    if (error.Count > 0)
                    {
                        if (error.Contains(GridData.DataKeys[e.Row.RowIndex].Value))
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }

                //If not old custid is null
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[15].Text.Replace("&nbsp;", "").Equals(""))
                    {
                        RadioButton rd = (RadioButton)e.Row.Cells[1].FindControl("chkNewCust");
                        if (rd!=null)
                            rd.Checked = true;
                    }
                    else
                    {
                        RadioButton rd = (RadioButton)e.Row.Cells[0].FindControl("chkOldCust");
                        if (rd != null)
                            rd.Checked = true;
                    }
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
                bool hasdeal = true;
                String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                List<Model_MS_Debenture> l = new List<Model_MS_Debenture>();
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
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Sheet1$] WHERE SECID is not null ", _oleConn);
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

                            ViewState["DataTableFile"] = dt; //Get Data From Excel

                        //    /*

                        //    FolderName = dt.Rows[0]["RelatedRef"].ToString() + "_" + DateTime.Now.ToString("yyyyMMdd");
                        //    DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));

                            DataTable opicstb = Model_MS_Debenture.getDebentureOpics(Config.ConnectionString);
                            DataTable customer = Model_MS_Debenture.getCustomerDebenture(Config.ConnectionString);
                            DataTable custtype = Model_MS_Debenture.getCustomerTypeMapping(Config.ConnectionString);
                            DataTable secid = Model_MS_Debenture.getSECIDDebenture(Config.ConnectionString);
                            int LastRound = Model_MS_Debenture.getLastInputRound(Config.ConnectionString);
                            if (LastRound == 0)
                                LastRound = 1;

                            Session["Debenture"] = null;
                            //cut some word
                            string pattern = @Config.GetParameterFile("DebtNameKey");
                            string replacement = "";
                            Regex rgx = new Regex(pattern);
                            
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                    Model_MS_Debenture md = new Model_MS_Debenture();
                                    try
                                    {
                                        md.Records = int.Parse(dt.Rows[i]["ID"].ToString());
                                        try
                                        {
                                            DataRow[] temp = opicstb.Select("TRIM(SECID)='" + dt.Rows[i]["SecID"].ToString().Trim().ToUpper() + "' AND TRIM(CMNE)='" + dt.Rows[i]["CustType"].ToString().Trim().ToUpper() + "'");
                                            md.DealNo = temp.FirstOrDefault().Field<String>("DEALNO").Trim();
                                            md.Custodian = temp.FirstOrDefault().Field<String>("SERIES").Trim();
                                            md.ThaiBMANo = temp.FirstOrDefault().Field<String>("SECALTID").Trim();
                                        }
                                        catch (Exception ex) {
                                            md.DealNo = "";
                                            md.Custodian = "";
                                            md.ThaiBMANo = "";
                                            hasdeal = false;
                                            dealnotfound.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                            Log.Debug("Deal No Mat : " + dt.Rows[i]["SecID"].ToString().Trim().ToUpper() + " " + dt.Rows[i]["CustType"].ToString().Trim().ToUpper() + ":" + ex.Message);
                                        }
                                            md.SecID = dt.Rows[i]["SecID"].ToString().Trim();
                                        try
                                        {
                                            DataRow[] temp = secid.Select("TRIM(SECID)='" + dt.Rows[i]["SecID"].ToString().Trim().ToUpper() + "'");
                                            if (temp != null)
                                            {
                                                if (temp.Count() > 0)
                                                    secidexists.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(ex.Message);
                                        }

                                        if (!dt.Rows[i]["Unit"].ToString().Trim().Equals(""))
                                            md.Unit = int.Parse(dt.Rows[i]["Unit"].ToString().Trim());
                                        else
                                            error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));

                                        if (!dt.Rows[i]["Amount"].ToString().Trim().Equals(""))
                                            md.Amount = int.Parse(dt.Rows[i]["Amount"].ToString().Trim());
                                        else
                                            error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));

                                        if (!dt.Rows[i]["NameThai"].ToString().Trim().Equals(""))
                                            md.NameThai = dt.Rows[i]["NameThai"].ToString().Trim();
                                        else
                                            error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));

                                        if (!dt.Rows[i]["CustID"].ToString().Trim().Equals(""))
                                            md.CustID = dt.Rows[i]["CustID"].ToString().Trim();
                                        //else
                                            //error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));

                                        if (!dt.Rows[i]["CustType"].ToString().Trim().Equals(""))
                                            md.CustType = dt.Rows[i]["CustType"].ToString().Trim();
                                        else
                                            error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));

                                        if (!dt.Rows[i]["BOTType"].ToString().Trim().Equals(""))
                                            md.BOTType = dt.Rows[i]["BOTType"].ToString().Trim();
                                        else
                                            error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));

                                        try
                                        {
                                            if (!dt.Rows[i]["CType"].ToString().Trim().Equals("")) //If Set CType Defaults
                                                md.CType = dt.Rows[i]["CType"].ToString().Trim();
                                            else
                                            {
                                                DataRow[] temp = custtype.Select("TRIM(BOTTYPE)='" + dt.Rows[i]["BOTType"].ToString().Trim().ToUpper().Substring(0, 6) + "'");
                                                if (temp.Count() > 1)
                                                { //If BOTType And SECType mapping have more than 2 and not set default
                                                    md.CType = "";
                                                    warning.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                                }
                                                else
                                                    md.CType = temp.FirstOrDefault().Field<String>("SECTYPE").Trim();
                                            }
                                        }
                                        catch (Exception ex) {
                                            md.CType = "";
                                            warning.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                            Log.Error(ex.Message);
                                        }
                                        md.InputRound = LastRound;

                                        md.NameThaiShort = rgx.Replace(md.NameThai, replacement).Trim();
                                        //if (md.NameThaiShort.IndexOf("โดย")>=0)
                                        //    md.NameThaiShort = md.NameThaiShort.Substring(0, md.NameThaiShort.IndexOf("โดย")).Trim();
                                        try
                                        {
                                            DataRow[] temp = customer.Select("TRIM(NameThaiShort) like '" + md.NameThaiShort.Trim() + "'");
                                            md.RelateCustID = temp.FirstOrDefault().Field<String>("CustID");
                                            md.RelateNameThai = temp.FirstOrDefault().Field<String>("NameThai");
                                        }
                                        catch (Exception ex)
                                        {
                                            md.RelateCustID = "";
                                            md.RelateNameThai = "";
                                            if ((md.CustID.Equals("")) && (md.RelateCustID.Equals("")))
                                                custidnotfound.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                        }

                                        try
                                        {
                                            md.CustomerNumber = dt.Rows[i]["CustomerNumber"].ToString().Trim();
                                        } catch(Exception ex)
                                        {
                                            md.CustomerNumber = "";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        error.Add(int.Parse(dt.Rows[i]["ID"].ToString()));
                                        Log.Error(ex.Message);
                                    }
                                    l.Add(md);
                            }
                            Session["Debenture"] = l;
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
                            MessageBox.Text = "เกิดข้อผิดพลาด ข้อมูลบางรายการไม่มี หรือข้อมูลไม่ถูกต้อง";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        else {
                            Session["Debenture"] = l; 
                            if (secidexists.Count > 0)
                            {
                                MessageBox.Text = "SECID บางรายการเคยมีการนำเข้าแล้ว กรุณาตรวจสอบก่อนนำข้อมูลเข้า";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                btnSave.Visible = true;
                            }
                            else if (dealnotfound.Count>0)
                            {
                                MessageBox.Text = "บางรายการไม่มีเลข DealNo กรุณาตรวจสอบก่อนนำข้อมูลเข้า";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                btnSave.Visible = false;
                            }
                            else if (custidnotfound.Count > 0)
                            {
                                MessageBox.Text = "บางรายการไม่มีเลข CustID กรุณาตรวจสอบก่อนนำข้อมูลเข้า";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                btnSave.Visible = false;
                            }
                            else {
                                btnSave.Visible = true;
                            }
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<Model_MS_Debenture> l = (List<Model_MS_Debenture>)Session["Debenture"];
                ArrayList oldcust = GetCustomerOldData();

                /*IF wrong choose annouce error*/
                for (int i = 0; i < GridData.Rows.Count; i++)
                {
                    if (oldcust.Contains(GridData.DataKeys[i].Value.ToString()))
                    {

                        if (GridData.Rows[i].Cells[15].Text.Replace("&nbsp;", "").Trim().Equals(""))
                        {
                            GridData.Rows[i].Cells[15].BackColor = System.Drawing.Color.Red;
                            error.Add(GridData.DataKeys[i].Value.ToString());                            
                        }
                    }
                    else
                    {
                        if (GridData.Rows[i].Cells[10].Text.Replace("&nbsp;", "").Trim().Equals(""))
                        {
                            GridData.Rows[i].Cells[10].BackColor = System.Drawing.Color.Red;
                            error.Add(GridData.DataKeys[i].Value.ToString());                            
                        }
                    }
                }


                if (error.Count <= 0)
                {
                    Model_MS_Debenture pt = new Model_MS_Debenture();
                    error = pt.save(Configuration.Config.ConnectionString, l, oldcust);
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
                else {

                    MessageBox.Text = "เลือกรายการ CustomerID ไม่ถูกต้อง เนืองจาก CustomerID เป็นค่าว่าง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally {
                if (error.Count <= 0)
                {
                    btnSave.Visible = false;
                    ViewState["DataTableFile"] = null;
                    Session["Debenture"] = null;

                    ViewState.Remove("DataTableFile");
                    Session.Remove("Debenture");
                }
            }
        }

        void BindGrid()
        {
            try
            {
                List<Model_MS_Debenture> l = (List<Model_MS_Debenture>)Session["Debenture"];
                GridData.DataSource = l;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }


        private ArrayList GetCustomerOldData()
        {
            ArrayList arr = new ArrayList();
            CheckBox chkAll = (CheckBox)GridData.HeaderRow.Cells[0].FindControl("chkAllOldCust");
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                    {
                        arr.Add(GridData.DataKeys[i].Value.ToString());
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)GridData.Rows[i].Cells[0].FindControl("chkOldCust");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Add(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                    else
                    {
                        if (arr.Contains(GridData.DataKeys[i].Value.ToString()))
                        {
                            arr.Remove(GridData.DataKeys[i].Value.ToString());
                        }
                    }
                }
            }
            return arr;
        }

    }
}
