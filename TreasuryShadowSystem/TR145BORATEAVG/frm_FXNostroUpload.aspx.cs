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
using System.Text;
using System.Net.Mail;
using TreasuryModel.Enhance;
using System.Globalization;

namespace TreasuryShadowSystem.TR145BORATEAVG
{
    public partial class frm_FXNostroUpload : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_FXNostroUpload));
        private ArrayList error;
        protected void Page_Load(object sender, EventArgs e)
        {
            error = new ArrayList();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool hasdeal = true;
                String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                List<FXNostroAvg> l = new List<FXNostroAvg>();
                if ((MyFile.PostedFile.ContentType.Equals("application/octet-stream")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    MyFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        //String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=No;\";";
                        String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [" + ddCurrency.SelectedValue + "$] ", _oleConn);
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
                             
                            
                            for (int i = 7; i < dt.Rows.Count; i++)
                            {
                                FXNostroAvg fx = new FXNostroAvg();
                                try
                                {
                                    fx.ADJDIFSPOTRATE = StringDecimal(dt.Rows[i][12].ToString());
                                    fx.ADJPLTHB = StringDecimal(dt.Rows[i][13].ToString());
                                    fx.BALANCECCY = StringDecimal(dt.Rows[i][14].ToString());
                                    fx.BALANCETHB = StringDecimal(dt.Rows[i][15].ToString());
                                    fx.BUYAMT = StringDecimal(dt.Rows[i][3].ToString());
                                    fx.EXCHRATEAVG = StringDecimal(dt.Rows[i][16].ToString());
                                    fx.NostAvgID = i + 1;
                                    fx.OPICSDATE = dt.Rows[i][7].ToString();
                                    fx.OPICSMOVE = StringDecimal(dt.Rows[i][11].ToString());
                                    fx.OPICSRATE = StringDecimal(dt.Rows[i][8].ToString());
                                    fx.SELLAMT = StringDecimal(dt.Rows[i][4].ToString());
                                    fx.STATEDATE = dt.Rows[i][5].ToString();
                                    fx.STATEMOVE = StringDecimal(dt.Rows[i][10].ToString());
                                    fx.STATERATE = StringDecimal(dt.Rows[i][6].ToString());
                                    fx.VDATE = dt.Rows[i][2].ToString();
                                    fx.CCY = ddCurrency.SelectedValue;
                                }
                                catch (Exception ex)
                                { 
                                    Log.Error(ex.Message);
                                }
                                l.Add(fx);
                            }
                            Session["FXNostroAvg"] = l;
                            BindGrid();
                            btnSave.Visible = true;

                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            MessageBox.Text = ex.Message;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        finally
                        {
                            _oleConn.Close();
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
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<FXNostroAvg> l = (List<FXNostroAvg>)Session["FXNostroAvg"];
 
                FXNostroAvg fx = new FXNostroAvg();
                error = fx.save(Configuration.Config.ConnectionString, l);
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
                Log.Error(ex.Message);
            }
            finally
            {
                if (error.Count <= 0)
                {
                    btnSave.Visible = false; 
                    Session["FXNostroAvg"] = null;
 
                    Session.Remove("FXNostroAvg");
                }
            }
        }

        void BindGrid()
        {
            try
            {
                List<FXNostroAvg> l = (List<FXNostroAvg>)Session["FXNostroAvg"];
                GridData.DataSource = l;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        protected decimal StringDecimal(string numbervar)
        {
            try
            {
                decimal temp;
                NumberStyles style = NumberStyles.AllowParentheses | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowTrailingSign | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite | NumberStyles.AllowLeadingSign;
                if (numbervar.Trim().Equals("-"))
                    return 0;
                else
                {
                    Decimal.TryParse(numbervar, style, CultureInfo.CreateSpecificCulture("en-US"), out temp);
                    return temp;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
