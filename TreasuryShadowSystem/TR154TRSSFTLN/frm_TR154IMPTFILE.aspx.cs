using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasuryModel.Opics;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Globalization;
using log4net;
using System.Collections;
using System.Text;

namespace TreasuryShadowSystem.TR154TRSSFTLN
{
    public partial class frm_TR154IMPTFILE : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR154IMPTFILE));
        private ArrayList error;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                try
                { 
                    String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
                    String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                    List<SoftLoan> l = new List<SoftLoan>();
                    if ((MyFile.PostedFile.ContentType.Equals("application/octet-stream")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                    {
                        MyFile.SaveAs(pathFile);

                        if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                        {
                            //String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=Yes;\";";
                            String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";  
                            OleDbConnection _oleConn = new OleDbConnection();
                            try
                            {
                                _oleConn.ConnectionString = ConnectionString;
                                if (_oleConn.State == ConnectionState.Closed) 
                                    _oleConn.Open();
                                OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Sheet1$] ", _oleConn);
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


                                for (int i = 2; i < dt.Rows.Count; i++)
                                {
                                    decimal amount, pdealamt, ppayamt, intrate;
                                    DateTime bookdate, matdate, pdealdate, pmatdate, pvaluedate, valuedate;
                                    DateTime.TryParse(dt.Rows[i][15].ToString(), out valuedate);
                                    if (valuedate!=DateTime.Parse("01/01/0001"))
                                    {
                                        SoftLoan sl = new SoftLoan();
                                        try
                                        {
                                            Decimal.TryParse(dt.Rows[i][17].ToString(), out amount);
                                            sl.AMOUNT = amount;
                                            DateTime.TryParse(dt.Rows[i][5].ToString(), out bookdate);
                                            if (bookdate == default(DateTime))
                                                sl.BOOKDATE = null;
                                            else
                                                sl.BOOKDATE = bookdate; 
                                            sl.BOOKNO = dt.Rows[i][4].ToString();
                                            sl.CTPYCODE = dt.Rows[i][3].ToString();
                                            sl.CUSTNAME = dt.Rows[i][7].ToString();
                                            sl.DEALNO = dt.Rows[i][14].ToString();
                                            sl.IMPDATE = DateTime.Now;
                                            Decimal.TryParse(dt.Rows[i][18].ToString(), out intrate);
                                            sl.INTRATE = intrate;
                                            sl.LOANTYPE = dt.Rows[i][6].ToString();
                                            sl.LSTUPDATE = DateTime.Now;
                                            DateTime.TryParse(dt.Rows[i][16].ToString(), out matdate);
                                            if (matdate == default(DateTime))
                                                sl.MATDATE = null;
                                            else
                                                sl.MATDATE = matdate;
                                            Decimal.TryParse(dt.Rows[i][12].ToString(), out pdealamt);
                                            sl.PDEALAMT = pdealamt;
                                            DateTime.TryParse(dt.Rows[i][10].ToString(), out pdealdate);
                                            if (pdealdate == default(DateTime))
                                                sl.PDEALDATE = null;
                                            else
                                                sl.PDEALDATE = pdealdate;
                                            sl.PDEALNO = dt.Rows[i][9].ToString();
                                            DateTime.TryParse(dt.Rows[i][11].ToString(), out pmatdate);
                                            if (pmatdate == default(DateTime))
                                                sl.PMATDATE = null;
                                            else
                                                sl.PMATDATE = pmatdate;
                                            Decimal.TryParse(dt.Rows[i][13].ToString(), out ppayamt);
                                            sl.PPAYAMT = ppayamt;
                                            DateTime.TryParse(dt.Rows[i][8].ToString(), out pvaluedate);
                                            if (pvaluedate == default(DateTime))
                                                sl.PVALUEDATE = null;
                                            else
                                                sl.PVALUEDATE = pvaluedate;
                                            sl.RECID = int.Parse(DateTime.Now.ToString("yyyy") + i.ToString("0000"));
                                            sl.RECNO = dt.Rows[i][2].ToString();
                                            sl.REFERDIV = dt.Rows[i][20].ToString();
                                            sl.REFERPROJ = dt.Rows[i][19].ToString();
                                            if (valuedate == default(DateTime))
                                                sl.VALUEDATE = null;
                                            else
                                                sl.VALUEDATE = valuedate;
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(ex.Message);
                                        }
                                        l.Add(sl);
                                    }
                                }
                                Session["SoftLoan"] = l;
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
            catch (Exception ex)
            { 
            
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<SoftLoan> l = (List<SoftLoan>)Session["SoftLoan"];

                string round = DateTime.Now.ToString("yyyyMMddHHmmss");
                SoftLoan sf = new SoftLoan();
                error = sf.save(Configuration.Config.ConnectionString, round, l);
                if (error.Count <= 0)
                {
                    BindGrid();
                    List<SoftLoan> sloan = sf.SoftLoanData(Configuration.Config.ConnectionString, round);
                    DownloadFile(sloan);
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
                    Session["SoftLoan"] = null;

                    Session.Remove("SoftLoan");
                }
            }
        }

        void BindGrid()
        {
            try
            {
                List<SoftLoan> l = (List<SoftLoan>)Session["SoftLoan"];
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

        void DownloadFile(List<SoftLoan> sloan)
        {
            try
            {
                List<SoftLoan> l = sloan;

                StringBuilder sb = new StringBuilder();
                
                foreach(SoftLoan loan in l)
                {
                    sb.Append("\"\",");
                    sb.Append("\"TBFI\",");
                    sb.Append("\"DL\",");
                    sb.Append("\"RAT1\",");

                    if (loan.VALUEDATE != null)
                        sb.Append("\"" + ((DateTime)loan.VALUEDATE).ToString("yyyyMMdd") + "\",");
                    else
                        sb.Append("\"\",");

                    if (loan.MATDATE != null)
                        sb.Append("\"" + ((DateTime)loan.MATDATE).ToString("yyyyMMdd") + "\",");
                    else
                        sb.Append("\"\",");

                    if (loan.CTPYCODE.Equals("BOT"))
                        sb.Append("\"178406\",");
                    else if (loan.CTPYCODE.Equals("GSB"))
                        sb.Append("\"114903\",");
                    else
                        sb.Append("\"\",");

                    sb.Append("\"THB\",");
                    if (loan.AMOUNT!=null)
                        sb.Append("\"" + loan.AMOUNT.Value.ToString() + "\",");
                    else
                        sb.Append("\"\",");
                    sb.Append("\"MMFIXED\",");
                    sb.Append("\"A365\",");
                    if (loan.INTRATE !=null)
                        sb.Append("\"" + loan.INTRATE.Value.ToString() + "\",");
                    else
                        sb.Append("\"\",");
                    sb.Append("\"0.000\",");
                    sb.Append("\"\",");
                    sb.Append("\"\",");
                    sb.Append("\"D\",");
                    sb.Append("\"\",");
                    sb.Append("\"\",");
                    sb.Append("\"\",");
                    sb.Append("\"MMPT\",");
                    sb.Append("\"340\",");
                    if (loan.BOOKDATE!=null)
                        sb.Append("\"" + ((DateTime)loan.BOOKDATE).ToString("yyyyMMdd") + "\",");
                    else
                        sb.Append("\"\",");
                    sb.Append("\"99\",");
                    sb.Append("\"\",");
                    sb.Append("\"\\SOFTLOAN" + loan.RECID.ToString("000000") + "\",");
                    sb.Append("\"\",");
                    sb.Append("\"N\",");
                    sb.Append("\"Y\",");
                    sb.Append("\"N\",");
                    sb.Append("\"N\",");
                    sb.Append("\"N\",");
                    for (int i = 0; i < 92; i++)
                    {
                        sb.Append("\"\"");
                        if (i < 91)
                            sb.Append(",");
                    }
                    sb.Append("\r\n");
                }

                //string PathDownload = "~/Temporary/SoftLoan/SoftLoan" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                //FileStream f = File.Create(Server.MapPath(PathDownload));
                string PathDownload = Configuration.Config.GetParameterFile("SOFTLOAN_FILEPATH") + "IDLD" + ".txt";
                FileStream f = File.Create(PathDownload);
                Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                f.Write(info, 0, info.Length);
                f.Close();
                //ExportFile(PathDownload);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        //private void ExportFile(string str)
        //{
        //    try
        //    {
        //        string result = Path.GetFileName(str);
        //        Response.ContentType = "text/plain";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + result);
        //        Response.TransmitFile(Server.MapPath(str));
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //    }
        //}
    }
}
